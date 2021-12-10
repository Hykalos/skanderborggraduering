namespace Skanderborg.Graduering.Models;

public class CsvMember
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public int? MentoClubId { get; set; }
    public string Name {  get; set; }
    public string Email { get; set; }
    public string MainProfile { get; set; }
    public string Belt { get; set; }
    public DateTime? GraduationDate { get; set; }
    public string GraduatedBy { get; set; }
    public int? PassportNumber { get; set; }
    public DateTime? LatestTraining { get; set; }
    public string PaymentGroup { get; set; }
    public bool PaymentMethodAdded { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public int? ZipCode { get; set; }
    public string City { get; set; }
    public string Municipal { get; set; }
    public string Country { get; set; }
    public DateTime? Birthday { get; set; }
    public int? Age { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string[] Teams { get; set; }
    public string[] InactiveTeams { get; set; }
    public string Note { get; set; }
}
