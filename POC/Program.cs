// See https://aka.ms/new-console-template for more information
using CsvHelper;
using CsvHelper.Configuration;
using Daemon.Converters;
using Daemon.Models;
using System.Text.Json;




var csvConfig = new CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture)
{
	IgnoreBlankLines = true,
	HasHeaderRecord = true,
	//HeaderValidated = null,
	//MissingFieldFound = null,
	AllowComments = true,
    Mode = CsvMode.RFC4180,
};

string csv = """
Name,Description,Cost,Attribute
Battoujutsu,"Você pode ao desembanhar a espada fazer um ataque ao mesmo tempo, + 2 de iniciativa",20,Strength
Iaito,"Você pode desembanhar uma katana e fazer um ataque com 40% de bonus, + 4 de iniciativa, requer Battoujutsu",25,
"Itto","Você defere um golpe de cima para baixo com uma katana usando sua defesa como bonus de ataque, você não poderá usar sua defesa até seu próximo turno",20,
""";

using StringReader r = new(csv);
using CsvReader reader = new(r, csvConfig);

reader.Read();
reader.ReadHeader();
reader.Read();

var cost = reader.GetField<int>("Cost");
//reader.GetRecordsAsync

var skills = reader.GetRecords<Skill>().ToArray();




JsonSerializerOptions jOptions = new()
{
    WriteIndented = true,
    IgnoreReadOnlyProperties = true,
    PropertyNameCaseInsensitive = true,
    TypeInfoResolver = new PlayerTypeResolver(),
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    Converters = {
        new PlayerConverter(),
    }
};

Player player = new()
{

};
player.Items.Add(new Item
{
    Name = "this is a item",
    Description = "this is description",
    Weight = 1
});

player.Items.Add(new Weapon
{
    Name = "this is a Weapon",
    Skill = "Some skill",
    Damage = "1d6",
    InitiativePenalty = 2,
    BonusAttribute = AttributeType.Strength,
    Description = "this is description",
    Weight = 2
});

player.Items.Add(new Armor
{
    AgiPenalty = 2,
    DexPenalty = 2,
    IP = 4,
    Weight = 3,
    Description = "someDescription",
    Name = "armor"
});

player.Skills.Add(new PlayerSkill()
{
    BasedAttribute = AttributeType.Strength,
    Name = "name",
    Description = "desc",
    Value = 3,
});

player.Skills.Add(new WeaponSkill()
{
    BasedAttribute = AttributeType.Strength,
    Name = "name",
    Description = "desc",
    Value = 3,
});

var items = player.Items.ToArray();

var s1 = JsonSerializer.Serialize(items, jOptions);
var d1 = JsonSerializer.Deserialize<IEnumerable<Item>>(s1, jOptions);

var serialization = JsonSerializer.Serialize(player, jOptions);
//var serialization = File.OpenRead(@"C:\Users\patri\Downloads\Manuel (15).json");
var deserialization = JsonSerializer.Deserialize<Player>(serialization, jOptions);
Console.ReadLine();
