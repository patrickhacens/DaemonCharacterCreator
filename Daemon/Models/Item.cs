
namespace Daemon.Models;

public class Item
{
	public Item()
	{
	}

	public string Name { get; set; } = null!;

	public string? Description { get; set; }

	public double Weight { get; set; }

    public double Cost { get; set; }

    public virtual IEnumerable<IModifier> GetModifiers() => [];

	public virtual Item Duplicate() => DuplicateTo(new Item());

	protected virtual Item DuplicateTo(Item item)
	{
		item.Name = Name;
		item.Description = Description;
		item.Weight = Weight;
		return item;
	}
}

public class Equipment : Item
{
	public bool Equipped { get; set; }

	public override Item Duplicate() => DuplicateTo(new Equipment());

	protected override Item DuplicateTo(Item copy)
	{
		var item = (Equipment)copy;
		item.Equipped = Equipped;
		return base.DuplicateTo(item);
	}
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

    public int Range { get; set; }

    public override IEnumerable<IModifier> GetModifiers() => Equipped ?
		[new Modifier<StatusType>(StatusType.Initiative, -InitiativePenalty) { Origin = $"Weapon: {Name}" }] :
		[];

	public override Item Duplicate() => DuplicateTo(new Weapon());

	protected override Item DuplicateTo(Item copy)
	{
		var item = (Weapon)copy;
		item.Damage = Damage;
		item.TwoHandedDamage = TwoHandedDamage;
		item.BonusAttribute = BonusAttribute;
		item.Skill = Skill;
		item.InitiativePenalty = InitiativePenalty;
		return base.DuplicateTo(item);
	}
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
	] : [];

	public override Item Duplicate() => DuplicateTo(new Armor());

	protected override Item DuplicateTo(Item copy)
	{
		var item = (Armor)copy;
		item.IP = IP;
		item.DexPenalty = DexPenalty;
		item.AgiPenalty = AgiPenalty;
		return base.DuplicateTo(item);
	}
}
