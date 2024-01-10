// See https://aka.ms/new-console-template for more information
using Daemon.Converters;
using Daemon.Data;
using Daemon.Models;
using System.Text.Json;

JsonSerializerOptions jOptions = new()
{
    WriteIndented = true,
    IgnoreReadOnlyProperties = true,
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    Converters = {
        new PlayerConverter(),
        new ModifierConverter(),
        new AttributeConverter(),
        new SkillConverter(),
    }
};

Player player = new()
{

};
player[AttributeType.Agility].BaseValue = 10;

PlayerAttribute d = new PlayerAttribute(AttributeType.Agility)
{
    BaseValue = 10
};

var sanityCheck = JsonSerializer.Serialize(d, jOptions);
var sanityCheck2 = JsonSerializer.Deserialize<PlayerAttribute>(sanityCheck, jOptions);


var serialization = JsonSerializer.Serialize(player, jOptions);
//var serialization = File.OpenRead(@"C:\Users\patri\Downloads\Manuel (15).json");
var deserialization = JsonSerializer.Deserialize<Player>(serialization, jOptions);
Console.ReadLine();
