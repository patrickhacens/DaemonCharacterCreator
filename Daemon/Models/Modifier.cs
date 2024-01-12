namespace Daemon.Models;

public class Modifier<T> : IModifier
{
    public Modifier()
    {
    }

    public Modifier(T modificationTarget, int value)
    {
        ModificationTarget=modificationTarget;
        Value=value;
    }

    public T ModificationTarget
    {
        get => (T)ModificationTargetObj;
        set => ModificationTargetObj = value!;
    }

    public int Value { get; set; }

    public object ModificationTargetObj { get; set; } = null!;
}
public interface IModifier
{
    public object ModificationTargetObj { get; set; }

    int Value { get; set; }
}