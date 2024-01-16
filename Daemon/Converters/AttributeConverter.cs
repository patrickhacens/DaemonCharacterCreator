// See https://aka.ms/new-console-template for more information
using Daemon.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Daemon.Converters;

public class AttributeConverter : JsonConverter<PlayerAttribute>
{
    public override PlayerAttribute? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        PlayerAttribute? result = new();

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndObject)
                break;
            var propertyName = reader.GetString();
            reader.Read();
            switch (propertyName)
            {
                case nameof(result.Type):
                    if (reader.TokenType == JsonTokenType.String)
                        if (Enum.TryParse<AttributeType>(reader.GetString(), out var type))
                            result.Type = type;
                        else
                            throw new JsonException($"Invalid Attribute");
                    else if (reader.TokenType == JsonTokenType.Number)
                        result.Type = (AttributeType)reader.GetInt32();
                    else
                        throw new JsonException($"Unexpected token '{reader.TokenType}' for property {propertyName}");
                    break;

                case nameof(result.BaseValue):
                    result.BaseValue = reader.GetInt32();
                    break;
                case nameof(result.BaseModifier):
                    result.BaseModifier = reader.GetInt32();
                    break;
                default:
                    throw new JsonException($"Unexpected property name {propertyName}");
            }
        }

        return result;
    }

    public override void Write(Utf8JsonWriter writer, PlayerAttribute value, JsonSerializerOptions options)
    {
        //options.get
        writer.WriteStartObject();

        writer.WriteString(nameof(value.Type), value.Type.ToString());
        writer.WriteNumber(nameof(value.BaseValue), value.BaseValue);
        writer.WriteNumber(nameof(value.BaseModifier), value.BaseModifier);

        writer.WriteEndObject();
    }
}