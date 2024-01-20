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

public class Equipment : Item
{
	public bool Equipped { get; set; }
}

public class Weapon : Equipment
{
	public Weapon()
	{
	}

	public string? Damage { get; set; }

	public string? TwoHandedDamage { get; set; }

	public AttributeType? BonusAttribute { get; set; }

	public string? Skill { get; set; }

	public int InitiativePenalty { get; set; }

	public override IEnumerable<IModifier> GetModifiers() => Equipped ?
		[new Modifier<StatusType>(StatusType.Initiative, -InitiativePenalty) { Origin = $"Weapon: {Name}" }] :
		Enumerable.Empty<IModifier>();
}

public class Armor : Equipment
{
	public Armor()
	{
	}

	public int IP { get; set; }

	public int DexPenalty { get; set; }

	public int AgiPenalty { get; set; }

	public override IEnumerable<IModifier> GetModifiers() => Equipped ?
	[
		new Modifier<AttributeType>(AttributeType.Dexterity, -DexPenalty) { Origin = $"Armor: {Name}" },
		new Modifier<AttributeType>(AttributeType.Agility, -AgiPenalty) { Origin = $"Armor: {Name}" },
		new Modifier<StatusType>(StatusType.IP, IP) { Origin = $"Armor: {Name}" }
	] : Enumerable.Empty<IModifier>();
}
