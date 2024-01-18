using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace Daemon.Models;

public class PlayerAttribute
{
    private Player player;

    public PlayerAttribute()
    {
    }

    public PlayerAttribute(AttributeType type) : this()
    {
        Type=type;
    }

    public AttributeType Type { get; set; }

    public int BaseValue { get; set; }

    public int Modifier => player.Modifiers
        .OfType<Modifier<AttributeType>>()
        .Where(d => d.ModificationTarget == Type)
        .Sum(d => d.Value);

    public int Total => BaseValue + Modifier;

    public int Percent => Total * 4;


    public class PlayerAttributeCollection(Player player) : ICollection<PlayerAttribute>
    {
        readonly List<PlayerAttribute> innerData = [];

        public PlayerAttribute this[int index]
        {
            get => ((IList<PlayerAttribute>)innerData)[index];
            set
            {
                value.player = player;
                ((IList<PlayerAttribute>)innerData)[index]=value;
            }
        }

        public int Count => ((ICollection<PlayerAttribute>)innerData).Count;

        public bool IsReadOnly => ((ICollection<PlayerAttribute>)innerData).IsReadOnly;

        public void Add(PlayerAttribute item)
        {
            var exists = player.Attributes.FirstOrDefault(d => d.Type == item.Type);
            if (exists is not null)
            {
                exists.BaseValue = item.BaseValue;
            }
            else
            {
                item.player = player;
                ((ICollection<PlayerAttribute>)innerData).Add(item);
            }
        }

        public void Clear()
        {
            ((ICollection<PlayerAttribute>)innerData).Clear();
        }

        public bool Contains(PlayerAttribute item)
        {
            return ((ICollection<PlayerAttribute>)innerData).Contains(item);
        }

        public void CopyTo(PlayerAttribute[] array, int arrayIndex)
        {
            ((ICollection<PlayerAttribute>)innerData).CopyTo(array, arrayIndex);
        }

        public IEnumerator<PlayerAttribute> GetEnumerator()
        {
            return ((IEnumerable<PlayerAttribute>)innerData).GetEnumerator();
        }

        public int IndexOf(PlayerAttribute item)
        {
            return ((IList<PlayerAttribute>)innerData).IndexOf(item);
        }

        public bool Remove(PlayerAttribute item)
        {
            return ((ICollection<PlayerAttribute>)innerData).Remove(item);
        }

        public void RemoveAt(int index)
        {
            ((IList<PlayerAttribute>)innerData).RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)innerData).GetEnumerator();
        }
    }
}
