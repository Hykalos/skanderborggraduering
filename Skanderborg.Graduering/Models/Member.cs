namespace Skanderborg.Graduering.Models;

public class Member
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime Birthday { get; set; }
    public string Degree { get; set; } = string.Empty;
    public string BaseTeam { get; set; } = string.Empty;
}
