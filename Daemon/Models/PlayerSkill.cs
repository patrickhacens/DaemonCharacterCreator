﻿using System.Collections;
using System.ComponentModel;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace Daemon.Models;

public class PlayerSkill
{
    protected Player player = null!;

    public PlayerSkill()
    {
    }

	public PlayerSkill(string name)
	{
		Name=name;
	}

	public AttributeType? BasedAttribute { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual int Value { get; set; }

    public int Total => Value + (BasedAttribute.HasValue ? player[BasedAttribute.Value].Total : 0) + Modifiers;

    protected virtual int Modifiers => player?.Modifiers?
        .OfType<Modifier<PlayerSkill>>()?
        .Where(d => d.ModificationTarget.Name == Name)?
        .Sum(d => d.Value) ?? 0;

    public virtual int TotalInvested => Value;

    public class PlayerSkillCollection(Player player) : IList<PlayerSkill>, INotifyPropertyChanged
    {
        readonly List<PlayerSkill> innerData = [];

		public event PropertyChangedEventHandler? PropertyChanged;

		public int MaxSkillPoints => player.Level * 25 + player[AttributeType.Inteligence].BaseValue * 5 + player.Description.RealAge * 10;

		public int UsedSkillPoints => innerData.Sum(d => d.TotalInvested);

		public PlayerSkill this[int index]
        {
            get => ((IList<PlayerSkill>)innerData)[index];
            set
            {
                value.player = player;
                ((IList<PlayerSkill>)innerData)[index]=value;
            }
        }

        public int Count => ((ICollection<PlayerSkill>)innerData).Count;

        public bool IsReadOnly => ((ICollection<PlayerSkill>)innerData).IsReadOnly;

        public void Add(PlayerSkill item)
        {
            item.player = player;
            ((ICollection<PlayerSkill>)innerData).Add(item);
        }

        public void Clear()
        {
            ((ICollection<PlayerSkill>)innerData).Clear();
        }

        public bool Contains(PlayerSkill item)
        {
            return ((ICollection<PlayerSkill>)innerData).Contains(item);
        }

        public void CopyTo(PlayerSkill[] array, int arrayIndex)
        {
            ((ICollection<PlayerSkill>)innerData).CopyTo(array, arrayIndex);
        }

        public IEnumerator<PlayerSkill> GetEnumerator()
        {
            return ((IEnumerable<PlayerSkill>)innerData).GetEnumerator();
        }

        public int IndexOf(PlayerSkill item)
        {
            return ((IList<PlayerSkill>)innerData).IndexOf(item);
        }

        public void Insert(int index, PlayerSkill item)
        {
            item.player = player;
            ((IList<PlayerSkill>)innerData).Insert(index, item);
        }

        public bool Remove(PlayerSkill item)
        {
            return ((ICollection<PlayerSkill>)innerData).Remove(item);
        }

        public void RemoveAt(int index)
        {
            ((IList<PlayerSkill>)innerData).RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)innerData).GetEnumerator();
        }
        void OnNotifyPropertyChanged([CallerMemberName]string? propertyName = null)
            => PropertyChanged?.Invoke(propertyName, new PropertyChangedEventArgs(propertyName));
    }
}

public class Skill(string Name, string? Description = null, AttributeType? Attribute = null, int? Cost = 0)
{
    public string Name { get; set; } = Name;
    public string? Description { get; set; } = Description;
    public AttributeType? Attribute { get; set; } = Attribute;
    public int Cost { get; set; } = Cost.HasValue ? Cost.Value : 0;
}