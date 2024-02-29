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
			new Status(StatusType.Level, 1),
			new Status(StatusType.IP, p => 0),
			new Status(StatusType.Magic, 0),
			new Status(StatusType.XP, 0),
			new Status(StatusType.HP, 0),
			new Status(StatusType.MaxHP, p => (int)Math.Ceiling(p[AttributeType.Strength].Total + p[AttributeType.Constitution].Total / 2d) + p[StatusType.Level].Value),
			new Status(StatusType.Focus, 0),
			new Status(StatusType.Inspiration, 0),
			new Status(StatusType.Faith, 0),
			new Status(StatusType.Initiative, p => p[AttributeType.Agility].Total),
			new Status(StatusType.Movement, p => p[AttributeType.Agility].Total),
			new Status(StatusType.Run, p => p[AttributeType.Constitution].Total * 3),
			new Status(StatusType.Action, p => (int)Math.Ceiling(p[AttributeType.Agility].Total / 20d)),
			new Status(StatusType.DmgBonus, p => p[AttributeType.Strength].Total switch
			{
				1 or 2 => -3,
				3 or 4 => -2,
				4 or 6 or 7 or 8 => -1,
				9 or 10 or 11 or 12 => 0,
				_ => (int)Math.Floor((p[AttributeType.Strength].Total - 13) / 2d)
			})
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

	public IEnumerable<PlayerAction> GetActions()
	{
		yield return new("Communicate");
		yield return new("Interact with an object", "Some objects may take more actions");
		yield return new("Use a skill", "Some skill might take more actions");
		yield return new($"Walk", $"{this[StatusType.Movement].Value} meters");
		yield return new($"Run", $"{this[StatusType.Run].Value} meters and give up dodge");
		var taijutsu = GetSkill<WeaponSkill>("Taijutsu", AttributeType.Dexterity);
		var bonus = this[StatusType.DmgBonus].Value;
		yield return new($"Fight barehand", $"{taijutsu.Total} / {taijutsu.DefenseTotal} 1d3+{bonus}");
		foreach (var weapon in Items.OfType<Weapon>().Where(d => d.Equipped))
		{
			var skill = GetSkill<WeaponSkill>(weapon.Skill!, AttributeType.Dexterity);
			string str = "";
			if (!String.IsNullOrEmpty(weapon.Damage))
				str += $"{Environment.NewLine}One-handed {weapon.Damage}+{bonus}";

			if (!String.IsNullOrEmpty(weapon.TwoHandedDamage))
				str+= $"{Environment.NewLine}Two-handed {weapon.TwoHandedDamage}+{bonus}";

			yield return new($"{weapon.Name} {weapon.Critic}*{weapon.CriticMultiplier}  {skill.Total} / {skill.DefenseTotal}:", str);
		}
	}

	public Player Duplicate()
	{
		Player copy = new()
		{
			Description = this.Description.Duplicate(),
			Name = this.Name,
			Profession = this.Profession,
		};
		foreach (var adv in this.Advantages)
			copy.Advantages.Add(adv.Duplicate());

		foreach (var attr in this.Attributes)
			copy[attr.Type].BaseValue = attr.BaseValue;

		foreach (var mod in this.CustomModifiers)
			copy.CustomModifiers.Add(mod.Duplicate());

		foreach (var item in this.Items)
			copy.Items.Add(item.Duplicate());

		foreach (var skill in this.Skills)
			copy.Skills.Add(skill);

		foreach (var st in this.Status)
			copy[st.Type].Value = st.Value;

		return copy;
	}

	public T GetSkill<T>(string name, AttributeType? basedAttribute = null) where T : PlayerSkill, new()
	{
		T? skill = Skills.OfType<T>()
			.Where(d => d.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase))
			.MaxBy(d => d.TotalInvested);
		if (skill == null)
		{
			int bonus = basedAttribute.HasValue ? this[basedAttribute.Value].Total : 0;
			skill = new T()
			{
				Name = name,
				Value = bonus,
			};
			if (skill is WeaponSkill ws)
				ws.DefenseValue = bonus;
		};

		return skill;
	}
}
