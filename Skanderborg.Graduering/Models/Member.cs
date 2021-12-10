using System;

namespace Skanderborg.Graduering.Models
{
    public class Member
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public string Degree { get; set; }
        public string BaseTeam { get; set; }
    }
}
