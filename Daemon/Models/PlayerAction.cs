namespace Daemon.Models;

public class PlayerAction(string Name, string? Info = null)
{
	public string Name { get; set; } = Name;

	public string? Info { get; set; } = Info;
}
