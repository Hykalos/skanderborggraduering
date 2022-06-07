namespace Skanderborg.Graduering.Domain.Models;

public class CsvMember
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public int? MentoClubId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string MainProfile { get; set; } = string.Empty;
    public string Belt { get; set; } = string.Empty;
    public DateTime? GraduationDate { get; set; }
    public string GraduatedBy { get; set; } = string.Empty;
    public int? PassportNumber { get; set; }
    public DateTime? LatestTraining { get; set; }
    public string PaymentGroup { get; set; } = string.Empty;
    public bool PaymentMethodAdded { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public int? ZipCode { get; set; }
    public string City { get; set; } = string.Empty;
    public string Municipal { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public DateTime? Birthday { get; set; }
    public int? Age { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string[] Teams { get; set; } = Array.Empty<string>();
    public string[] InactiveTeams { get; set; } = Array.Empty<string>();
    public string Note { get; set; } = string.Empty;
}
