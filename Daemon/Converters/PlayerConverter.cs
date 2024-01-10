// See https://aka.ms/new-console-template for more information
using Daemon.Models;
using System.Collections;
using System.Numerics;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Daemon.Converters;

public class PlayerConverter : JsonConverter<Player>
{
    private static PropertyInfo[] properties;

    private static JsonSerializerOptions jOptions;

    static PlayerConverter()
    {
        properties =
        [.. typeof(Player).GetProperties(BindingFlags.Public | BindingFlags.Instance)];
        jOptions = new JsonSerializerOptions
        {
            Converters =
            {
                new AttributeConverter(),
                new ModifierConverter(),
                new SkillConverter(),
                new JsonStringEnumConverter(),
            }
        };

    }
    public override Player? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartObject)
            throw new JsonException();

        Player player = new();

        string? propertyName;
        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndObject)
                return player;

            if (reader.TokenType != JsonTokenType.PropertyName)
                throw new JsonException($"Expected property name got {reader.TokenType}");

            propertyName = reader.GetString();
            reader.Read();
            var property = properties.FirstOrDefault(d => d.Name == propertyName);
            if (property == null)
            {
                continue;
            }

            if (property.PropertyType == typeof(string))
            {
                property.SetMethod?.Invoke(player, new object[] { reader.GetString() });
            }
            else if (property.PropertyType == typeof(int))
            {
                property.SetMethod?.Invoke(player, new object[] { reader.GetInt32() });
            }
            else if (property.PropertyType.IsAssignableTo(typeof(IEnumerable)))
            {
                if (reader.TokenType != JsonTokenType.StartArray)
                    throw new JsonException($"Exceptected start array but found {reader.TokenType}");

                if (property.PropertyType == typeof(PlayerAttribute.PlayerAttributeCollection))
                {
                    ReadAttribute(ref reader, options, player);
                }
                else if (property.PropertyType == typeof(PlayerSkill.PlayerSkillCollection))
                {
                    ReadSkills(ref reader, options, player);
                }
                else if (property.PropertyType == typeof(Status.PlayerStatusCollection))
                {
                    var type = typeof(IEnumerable<Status>);
                    foreach (var item in ((JsonConverter<IEnumerable<Status>>)options.GetConverter(type)).Read(ref reader, type, options))
                    {
                        player.Status.Add(item);
                    }

                }
                else if (property.PropertyType == typeof(List<Advantage>))
                {
                    var type = typeof(List<Advantage>);
                    foreach (var item in ((JsonConverter<List<Advantage>>)options.GetConverter(type)).Read(ref reader, type, options))
                    {
                        player.Advantages.Add(item);
                    }
                }
                else if (property.PropertyType == typeof(List<IModifier>))
                {
                    var type = typeof(List<IModifier>);
                    foreach (var item in ((JsonConverter<List<IModifier>>)options.GetConverter(type)).Read(ref reader, type, options))
                    {
                        player.Modifiers.Add(item);
                    }
                }
            }
            else // should be complex type
            {
                if (reader.TokenType != JsonTokenType.StartObject)
                    throw new JsonException($"Expected start object but found {reader.TokenType}");
                var deserialize = JsonSerializer.Deserialize(ref reader, property.PropertyType, options);
                property.SetValue(player, deserialize);
            }
        }

        throw new JsonException("something is wrong");
    }

    static Type attributeType = typeof(PlayerAttribute);
    private static void ReadAttribute(ref Utf8JsonReader reader, JsonSerializerOptions options, Player player)
    {
        if (reader.TokenType != JsonTokenType.StartArray)
            throw new JsonException($"Expected start array found {reader.TokenType}");

        var converter = (JsonConverter<PlayerAttribute>)options.GetConverter(attributeType);

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.StartObject)
            {
                var attribute = converter.Read(ref reader, attributeType, options);
                player.Attributes.Add(attribute!);
            }

            if (reader.TokenType == JsonTokenType.EndArray)
                break;
        }
    }


    static Type skillType = typeof(PlayerSkill);
    private static void ReadSkills(ref Utf8JsonReader reader, JsonSerializerOptions options, Player player)
    {
        if (reader.TokenType != JsonTokenType.StartArray)
            throw new JsonException($"Expected start array found {reader.TokenType}");

        var converter = (JsonConverter<PlayerSkill>)options.GetConverter(skillType);

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.StartObject)
            {
                var skill = converter.Read(ref reader, skillType, options);
                player.Skills.Add(skill!);
            }

            if (reader.TokenType == JsonTokenType.EndArray)
                break;
        }
    }

    public override void Write(Utf8JsonWriter writer, Player value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, jOptions);
    }
}