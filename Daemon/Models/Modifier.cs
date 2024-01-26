using System.Net.Http.Headers;

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

	public T ModificationTarget { get; set; } = default!;

	public string? Origin { get; set; }

	public int Value { get; set; }

	public IModifier Duplicate() => new Modifier<T>(ModificationTarget, Value)
	{
		Origin = Origin
	};

	public string GetTargetDescription() => ModificationTarget is PlayerSkill skill ? $"Skill {skill.Name}" : ModificationTarget?.ToString() ?? String.Empty;
}

public interface IModifier
{
	int Value { get; set; }
	string? Origin { get; set; }
	IModifier Duplicate();
	string GetTargetDescription();
}