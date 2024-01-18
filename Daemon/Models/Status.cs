using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace Daemon.Models;

public class Status
{
    private Player player;
    private readonly Func<Player, int> calculationExpression;
    private int initialValue;

    public Status()
    {

    }

    public Status(StatusType type, int initialValue)
    {
        this.Type = type;
        this.initialValue = initialValue;
    }

    public Status(StatusType type, Func<Player, int> calculationExpression)
    {
        this.Type = type;
        this.calculationExpression=calculationExpression;
    }

    public StatusType Type { get; set; }

    public int Value
    {
        get => (IsReadOnly() ? calculationExpression(player) : initialValue) + (player?
            .Modifiers
            .OfType<Modifier<StatusType>>()
            .Where(d => d.ModificationTarget == Type)
            .Sum(d => d.Value) ?? 0);
        set
        {
            initialValue = value;
        }
    }

    public bool IsReadOnly() => calculationExpression != null;


    public class PlayerStatusCollection(Player player) : ICollection<Status>
    {
        List<Status> innerData = [];

        public Status this[int index]
        {
            get => ((IList<Status>)innerData)[index];
            set
            {
                value.player= player;
                ((IList<Status>)innerData)[index]=value;
            }
        }

        public int Count => ((ICollection<Status>)innerData).Count;

        public bool IsReadOnly => ((ICollection<Status>)innerData).IsReadOnly;

        public void Add(Status item)
        {
            var exists = innerData.FirstOrDefault(d => d.Type == item.Type);
            if (exists is not null)
            {
                exists.initialValue = item.Value;
            }
            else
            {

                item.player= player;
                ((ICollection<Status>)innerData).Add(item);
            }
        }

        public void Clear()
        {
            ((ICollection<Status>)innerData).Clear();
        }

        public bool Contains(Status item)
        {
            return ((ICollection<Status>)innerData).Contains(item);
        }

        public void CopyTo(Status[] array, int arrayIndex)
        {
            ((ICollection<Status>)innerData).CopyTo(array, arrayIndex);
        }

        public IEnumerator<Status> GetEnumerator()
        {
            return ((IEnumerable<Status>)innerData).GetEnumerator();
        }

        public int IndexOf(Status item)
        {
            return ((IList<Status>)innerData).IndexOf(item);
        }

        public bool Remove(Status item)
        {
            return ((ICollection<Status>)innerData).Remove(item);
        }

        public void RemoveAt(int index)
        {
            ((IList<Status>)innerData).RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)innerData).GetEnumerator();
        }
    }
}
