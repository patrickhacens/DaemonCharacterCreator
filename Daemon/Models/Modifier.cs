namespace Daemon.Models;

public class Modifier<T> : IModifier
{
    public T ModificationTarget
    {
        get => (T)ModificationTargetObj;
        set => ModificationTargetObj = value;
    }

    public int Value { get; set; }

    public object ModificationTargetObj { get; set; }
}
public interface IModifier
{
    public object ModificationTargetObj { get; set; }

    int Value { get; set; }
}