// See https://aka.ms/new-console-template for more information
using Daemon.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Daemon.Converters;

public class SkillConverter : JsonConverter<PlayerSkill>
{
    public override PlayerSkill? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        PlayerSkill? skill = null;
        string? propertyName;
        do
        {
            if (reader.TokenType == JsonTokenType.StartObject)
            {
                skill = new PlayerSkill();
                continue;
            }

            if (reader.TokenType == JsonTokenType.EndObject)
                return skill;

            if (reader.TokenType != JsonTokenType.PropertyName)
                throw new JsonException($"Expected propertyName found {reader.TokenType}");

            propertyName = reader.GetString()!;
            reader.Read();
            if (reader.TokenType != JsonTokenType.Null)
            {
                switch (propertyName)
                {
                    case nameof(skill.Name):
                        skill!.Name = reader.GetString()!;
                        break;
                    case nameof(skill.Description):
                        skill!.Description = reader.GetString();
                        break;
                    case nameof(skill.Value):
                        skill!.Value = reader.GetInt32();
                        break;
                    case nameof(WeaponSkill.DefenseValue):
                        if (skill is not WeaponSkill && skill is not null)
                        {
                            skill = new WeaponSkill()
                            {
                                BasedAttribute = skill.BasedAttribute,
                                Description = skill.Description,
                                Name = skill.Name,
                                Value = skill.Value,
                                DefenseValue = reader.GetInt32()
                            };
                        }
                        else
                            (skill as WeaponSkill)!.DefenseValue = reader.GetInt32();
                        break;
                    case nameof(skill.BasedAttribute):
                        if (reader.TokenType == JsonTokenType.Number)
                            skill!.BasedAttribute = (AttributeType)reader.GetInt32();
                        else
                            skill!.BasedAttribute = Enum.Parse<AttributeType>(reader.GetString()!, true);
                        break;
                    default:
                        break;
                }
            }
        }
        while (reader.Read());

        throw new JsonException("Unexpected EOF");
    }

    public override void Write(Utf8JsonWriter writer, PlayerSkill value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteString(nameof(value.Name), value.Name);
        writer.WriteString(nameof(value.Description), value.Description);
        writer.WriteNumber(nameof(value.Value), value.Value);

        if (value.BasedAttribute.HasValue)
            writer.WriteString(nameof(value.BasedAttribute), value.BasedAttribute.ToString());

        if (value is WeaponSkill wp)
            writer.WriteNumber(nameof(wp.DefenseValue), wp.DefenseValue);

        writer.WriteEndObject();
    }
}