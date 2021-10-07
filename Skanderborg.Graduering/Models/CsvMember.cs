using System;

namespace Skanderborg.Graduering.Models
{
    public class CsvMember
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name {  get; set; }
        public string Email { get; set; }
        public string SecondaryEmail {  get; set; }
        public string MainProfile { get; set; }
        public Gender Gender {  get; set; }
        public string Belt { get; set; }
        public DateTime? GraduationDate { get; set; }
        public int? PassportNumber { get; set; }
        public string PaymentGroup { get; set; }
        public bool PaymentMethodAdded { get; set; }
        public string PhoneNumber { get; set; }
        public string MobileNumber { get; set; }
        public string Address { get; set; }
        public int ZipCode { get; set; }
        public string City { get; set; }
        public string Municipal { get; set; }
        public DateTime Birthday { get; set; }
        public int Age { get; set; }
        public DateTime CreatedAt { get; set; }
        public string BaseTeam { get; set; }
        public string SpecialTeams { get; set; }
        public string Note { get; set; }
    }
}
