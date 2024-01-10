namespace Daemon.Models;

public class Player
{
    public Player()
    {
        Modifiers = [];
        Description = new();

        Attributes = new(this);

        foreach (var attr in Enum.GetValues<AttributeType>()
            .Select(type => new PlayerAttribute(type)))
        {
            Attributes.Add(attr);
        }

        Status = new(this)
        {
            new Status(StatusType.MaxHP, p => (int)Math.Ceiling(p[AttributeType.Strength].Total + p[AttributeType.Constitution].Total / 2d) + p[StatusType.Level].Value),
            new Status(StatusType.HP, 0),
            new Status(StatusType.Inspiration, 0 ),
            new Status(StatusType.Magic, 0),
            new Status(StatusType.Focus, 0),
            new Status(StatusType.Level, 1),
            new Status(StatusType.IP, 0),
            new Status(StatusType.XP, 0)
        };

        Skills = new(this);
        Advantages = [];
    }


    public PlayerAttribute this[AttributeType attribute]
    {
        get => Attributes.First(d => d.Type == attribute);
    }

    public Status this[StatusType status]
    {
        get => Status.First(d => d.Type == status);
    }

    public string Name { get; set; } = null!;

    public string Profession { get; set; } = null!;

    public int Level { get; set; }

    public int TotalAttributePoints => Attributes.Sum(d => d.BaseValue);

    public int MaxSkillPoints => Level * 25 + this[AttributeType.Inteligence].BaseValue * 5 + Description.RealAge * 10;

    public int UsedSkillPoints => Skills.Sum(d => d.TotalInvested);

    public int MaxAdvantagePoints { get; set; }

    public int UsedAdvantagePoints => Advantages.Sum(d => d.Cost);

    public PlayerDescription Description { get; set; } = null!;

    public Status.PlayerStatusCollection Status { get; }

    public PlayerAttribute.PlayerAttributeCollection Attributes { get; }

    public PlayerSkill.PlayerSkillCollection Skills { get; }

    public List<IModifier> Modifiers { get; }

    public List<Advantage> Advantages { get; }
}
