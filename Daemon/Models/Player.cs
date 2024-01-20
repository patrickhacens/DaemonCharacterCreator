using System.Collections.ObjectModel;

namespace Daemon.Models;

public class Player
{
	public Player()
	{
		CustomModifiers = [];
		Description = new();

		Attributes = new(this);
		Items = [];
		Items.CollectionChanged += (s, e) => RefreshModifiers();
		CustomModifiers.CollectionChanged += (s, e) => RefreshModifiers();

		foreach (var attr in Enum.GetValues<AttributeType>()
			.Select(type => new PlayerAttribute(type)))
		{
			Attributes.Add(attr);
		}

		Status = new(this)
		{
			new Status(StatusType.MaxHP, p => (int)Math.Ceiling(p[AttributeType.Strength].Total + p[AttributeType.Constitution].Total / 2d) + p[StatusType.Level].Value + GetModifiersFor(StatusType.HP)),
			new Status(StatusType.HP, 0),
			new Status(StatusType.Inspiration, 0),
			new Status(StatusType.Magic, 0),
			new Status(StatusType.Focus, 0),
			new Status(StatusType.Level, 1),
			new Status(StatusType.IP, p => GetModifiersFor(StatusType.IP)),
			new Status(StatusType.XP, 0),
			new Status(StatusType.Initiative, p => p[AttributeType.Agility].Total + GetModifiersFor(StatusType.Initiative)),
			new Status(StatusType.Movement, p => p[AttributeType.Agility].Total + GetModifiersFor(StatusType.Movement)),
			new Status(StatusType.Run, p => p[AttributeType.Constitution].Total * 3 +  GetModifiersFor(StatusType.Run)),
			new Status(StatusType.Action, p => (int)Math.Ceiling(p[AttributeType.Agility].Total / 20d) + GetModifiersFor(StatusType.Action))
		};

		Skills = new(this);
		Advantages = [];
		RefreshModifiers();
	}

	private void RefreshModifiers()
	{
		_calculatedModifiers = Items.SelectMany(d => d.GetModifiers());
		_modifiers = _calculatedModifiers.Concat(CustomModifiers);
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

	public int Level => this[StatusType.Level].Value;

	public int TotalAttributePoints => Attributes.Sum(d => d.BaseValue);

	public int MaxAttributePoints => 100 + Level;



	public int MaxAdvantagePoints => 5 + (int)Math.Ceiling((Level-1)/3d);

	public int UsedAdvantagePoints => Advantages.Sum(d => d.Cost);

	public PlayerDescription Description { get; set; } = null!;

	public Status.PlayerStatusCollection Status { get; }

	public PlayerAttribute.PlayerAttributeCollection Attributes { get; }

	public PlayerSkill.PlayerSkillCollection Skills { get; }

	IEnumerable<IModifier> _modifiers = Enumerable.Empty<IModifier>();
	public IEnumerable<IModifier> Modifiers => _modifiers;

	IEnumerable<IModifier> _calculatedModifiers = Enumerable.Empty<IModifier>();
	public IEnumerable<IModifier> CalculatedModifiers => _calculatedModifiers;

	public ObservableCollection<IModifier> CustomModifiers { get; }

	public List<Advantage> Advantages { get; }

	public ObservableCollection<Item> Items { get; }

	int GetModifiersFor<T>(T target) => Modifiers
		.OfType<Modifier<T>>()
		.Where(d => d.ModificationTarget!.Equals(target))
		.Sum(d => d.Value);
}
