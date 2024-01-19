namespace Daemon.Models;

public class WeaponSkill : PlayerSkill
{
	public int DefenseValue { get; set; }

	public int DefenseTotal => DefenseValue + (BasedAttribute.HasValue ? player[BasedAttribute.Value].Total : 0) + Modifiers;

	public override int TotalInvested => base.TotalInvested + DefenseValue;
}
