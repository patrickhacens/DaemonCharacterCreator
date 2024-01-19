namespace Daemon.Models;

public class CombatSkill : PlayerSkill
{
	protected override int Modifiers => 0;

	public int Cost { get; set; }

	public override int Value
	{
		get => Cost;
		set { }
	}
}