// See https://aka.ms/new-console-template for more information
using Daemon.Models;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Daemon.Converters;

public class PlayerConverter : JsonConverter<Player>
{
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

            StringComparison strComparison = StringComparison.InvariantCultureIgnoreCase;

            if (nameof(Player.Advantages).Equals(propertyName, strComparison))
                ReadAdvantages(ref reader, options, player);
            else if (nameof(Player.Attributes).Equals(propertyName, strComparison))
                ReadAttributes(ref reader, options, player);
            else if (nameof(Player.CustomModifiers).Equals(propertyName, strComparison))
                ReadModifiers(ref reader, options, player);
            else if (nameof(Player.Description).Equals(propertyName, strComparison))
                ReadDescription(ref reader, options, player);
            else if (nameof(Player.Items).Equals(propertyName, strComparison))
                ReadItems(ref reader, options, player);
            else if (nameof(Player.Name).Equals(propertyName, strComparison))
                player.Name = reader.GetString()!;
            else if (nameof(Player.Profession).Equals(propertyName, strComparison))
                player.Profession = reader.GetString()!;
            else if (nameof(Player.Skills).Equals(propertyName, strComparison))
                ReadSkills(ref reader, options, player);
            else if (nameof(Player.Status).Equals(propertyName, strComparison))
                ReadStatus(ref reader, options, player);
            else
            {
                Debug.Write($"Unexpected property {propertyName} while deserializing player, ignoring...");
                IgnoreProperty(ref reader);
            }
        }

        throw new JsonException("Unexpected EOF while deserializing player");
    }

    static void IgnoreProperty(ref Utf8JsonReader reader)
    {
        if (reader.TokenType == JsonTokenType.StartArray)
        {
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.StartArray)
                    IgnoreProperty(ref reader);
                if (reader.TokenType == JsonTokenType.EndArray)
                    break;
            }
        }
        else if (reader.TokenType == JsonTokenType.StartObject)
        {
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.StartObject)
                    IgnoreProperty(ref reader);
                if (reader.TokenType == JsonTokenType.EndObject)
                    break;
            }
        }
    }

    readonly static Type statusType = typeof(IEnumerable<Status>);
    private static void ReadStatus(ref Utf8JsonReader reader, JsonSerializerOptions options, Player player)
    {
        foreach (var item in ((JsonConverter<IEnumerable<Status>>)options.GetConverter(statusType)).Read(ref reader, statusType, options)!)
            player.Status.Add(item);
    }

    readonly static Type itemsType = typeof(IEnumerable<Item>);
    private static void ReadItems(ref Utf8JsonReader reader, JsonSerializerOptions options, Player player)
    {
        foreach (var item in ((JsonConverter<IEnumerable<Item>>)options.GetConverter(itemsType)).Read(ref reader, itemsType, options)!)
            player.Items.Add(item);
    }

    readonly static Type playerDescriptionType = typeof(PlayerDescription);
    private static void ReadDescription(ref Utf8JsonReader reader, JsonSerializerOptions options, Player player)
    {
        var converter = (JsonConverter<PlayerDescription>)options.GetConverter(playerDescriptionType);
        var description = converter.Read(ref reader, playerDescriptionType, options)!;
        player.Description = description;
    }

    readonly static Type modifiersType = typeof(List<IModifier>);
    private static void ReadModifiers(ref Utf8JsonReader reader, JsonSerializerOptions options, Player player)
    {
        foreach (var item in ((JsonConverter<List<IModifier>>)options.GetConverter(modifiersType)).Read(ref reader, modifiersType, options)!)
            player.CustomModifiers.Add(item);
    }

    static readonly Type advantagesType = typeof(IEnumerable<Advantage>);
    private static void ReadAdvantages(ref Utf8JsonReader reader, JsonSerializerOptions options, Player player)
    {
        var items = ((JsonConverter<IEnumerable<Advantage>>)options.GetConverter(advantagesType))
            .Read(ref reader, advantagesType, options);
        player.Advantages.AddRange(items!);
    }

    static readonly Type attributeType = typeof(PlayerAttribute);
    private static void ReadAttributes(ref Utf8JsonReader reader, JsonSerializerOptions options, Player player)
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


    static readonly Type skillType = typeof(PlayerSkill);
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
        JsonSerializerOptions rOptions = new(options);
        rOptions.Converters
            .Remove(rOptions.Converters.OfType<PlayerConverter>().FirstOrDefault()!);

        JsonSerializer.Serialize(writer, value, rOptions);
    }
}