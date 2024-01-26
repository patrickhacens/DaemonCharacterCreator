namespace Daemon.Models;

public class Advantage
{
	public Advantage()
	{
	}

	public Advantage(string name, int cost)
	{
		Name=name;
		Cost=cost;
	}

	public Advantage(string name, string description, int cost) : this(name, cost)
	{
		Description=description;
	}

	public Advantage(string name, string description, int cost, string? requirements) : this(name, description, cost)
	{
		Requirements=requirements;
	}

	public string? Requirements { get; set; }

	public string Name { get; set; } = null!;

	public string Description { get; set; } = null!;

	public int Cost { get; set; }

	public Advantage Duplicate() => new()
	{
		Cost = Cost,
		Description = Description,
		Name = Name,
		Requirements = Requirements
	};
}