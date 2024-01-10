namespace Daemon.Models;

public class PlayerDescription
{
    public string? Religion { get; set; }

    public Gender Gender { get; set; }

    public decimal Height { get; set; }

    public decimal Width { get; set; }

    public decimal Weight { get; set; }

    public DateOnly Birthday { get; set; }

    public string BirthPlace { get; set; } = null!;

    public int RealAge { get; set; }

    public int ApparentAge { get; set; }
}
