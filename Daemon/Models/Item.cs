namespace Daemon.Models;

public class Item
{
	public Item()
	{
	}

	public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public double Weight { get; set; }

    public virtual IEnumerable<IModifier> GetModifiers() => Enumerable.Empty<IModifier>();
}

public class Weapon : Item
{
	public Weapon()
	{
	}

	public string Damage { get; set; } = null!;

    public string TwoHandedDamage { get; set; }

    public AttributeType? BonusAttribute { get; set; }

    public string? Skill { get; set; }

    public int InitiativePenalty { get; set; }

    public override IEnumerable<IModifier> GetModifiers() =>
        [new Modifier<StatusType>(StatusType.Initiative, -InitiativePenalty)];
}

public class Armor : Item
{
	public Armor()
	{
	}

	public int IP { get; set; }

    public int DexPenalty { get; set; }

    public int AgiPenalty { get; set; }

    public override IEnumerable<IModifier> GetModifiers() =>
    [
        new Modifier<AttributeType>(AttributeType.Dexterity, -DexPenalty),
        new Modifier<AttributeType>(AttributeType.Agility, -AgiPenalty),
        new Modifier<StatusType>(StatusType.IP, IP)
    ];
}
