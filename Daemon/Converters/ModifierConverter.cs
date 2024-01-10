// See https://aka.ms/new-console-template for more information
using Daemon.Models;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Daemon.Converters;

public class ModifierConverter : JsonConverter<IModifier>
{
    static readonly Assembly Assembly = typeof(Player).Assembly;

    public override IModifier? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        IModifier? result = null;
        int target = 0;
        int value = 0;

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndObject)
                break;
            var propertyName = reader.GetString();
            reader.Read();
            switch (propertyName)
            {
                case nameof(Type):
                    var typeName = reader.GetString();
                    result = (IModifier)Assembly.CreateInstance(typeName);
                    break;

                case nameof(Modifier<object>.ModificationTarget):
                    target = reader.GetInt32();
                    break;

                case nameof(result.Value):
                    value = reader.GetInt32();
                    break;

                default:
                    throw new JsonException($"Unexpected property name {propertyName}");
            }
        }

        if (result is null)
            throw new JsonException("Missing type property");

        result.Value = value;
        result.ModificationTargetObj = target;

        return result;
    }

    public override void Write(Utf8JsonWriter writer, IModifier value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        writer.WriteString(nameof(Type), value.GetType().FullName);
        writer.WriteNumber(nameof(Modifier<object>.ModificationTarget), (int)value.ModificationTargetObj);
        writer.WriteNumber(nameof(value.Value), value.Value);

        writer.WriteEndObject();
    }
}
