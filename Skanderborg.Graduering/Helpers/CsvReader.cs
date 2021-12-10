namespace Skanderborg.Graduering.Helpers
{
    public class CsvReader : ICsvReader
    {
        private const string MentoclubIdHeader = "ID";
        private const string NameHeader = "Navn";
        private const string EmailHeader = "E-mail";
        private const string MainProfileHeader = "Hovedprofil";
        private const string BeltDegreeHeader = "Bæltegrad";
        private const string GraduationDateHeader = "Gradueringsdato";
        private const string GraduatedByHeader = "Gradueret af";
        private const string PassportNumberHeader = "pasnummer";
        private const string PaymentGroupsHeader = "Betalingsgruppe(er)";
        private const string PaymentMethodAddedHeader = "Betalingkort tilføjet";
        private const string PhoneNumberHeader = "Telefonnummer";
        private const string AddressHeader = "Adresse";
        private const string ZipCodeHeader = "Postnummer";
        private const string CityHeader = "By";
        private const string MunicipalHeader = "Kommune";
        private const string CountryHeader = "Land";
        private const string AgeHeader = "Alder";
        private const string BirthdayHeader = "Fødselsdag";
        private const string CreatedAtHeader = "Oprettelsesdato";
        private const string LatestTrainingHeader = "Seneste træning";
        private const string TeamsHeader = "Hold";
        private const string InactiveTeamsHeader = "Inaktive hold";
        private const string NoteHeader = "Note";

        public IEnumerable<CsvMember> GetMembers(IFormFile file)
        {
            StreamReader reader;
            List<string> lines = new List<string>();
            List<CsvMember> members = new List<CsvMember>();

            using (var stream = new MemoryStream())
            {
                file.CopyTo(stream);

                stream.Position = 0;

                reader = new StreamReader(stream, Encoding.GetEncoding("iso-8859-1"));

                while (reader.Peek() >= 0)
                {
                    lines.Add(reader.ReadLine());
                }
            }

            var headerIndices = GetIndices(lines[0]);

            //Skip first line as it's the headers
            for(var i = 1; i < lines.Count; ++i)
            {
                members.Add(ReadMember(lines[i], headerIndices));
            }

            return members.ToArray();
        }

        private IDictionary<string, int> GetIndices(string headerLine)
        {
            var parts = headerLine.Split(';');
            var dictionary = new Dictionary<string, int>();

            for (var i = 0; i < parts.Length; ++i)
            {
                dictionary.Add(parts[i], i);
            }

            return dictionary;
        }

        private CsvMember ReadMember(string line, IDictionary<string, int> headerIndices)
        {
            var parts = line.Split(';');

            return new CsvMember
            {
                MentoClubId = headerIndices.ContainsKey(MentoclubIdHeader) ? GetInteger(parts[headerIndices[MentoclubIdHeader]]) : null,
                Name = headerIndices.ContainsKey(NameHeader) ? parts[headerIndices[NameHeader]] : string.Empty,
                Email = headerIndices.ContainsKey(EmailHeader) ? parts[headerIndices[EmailHeader]] : string.Empty,
                MainProfile = headerIndices.ContainsKey(MainProfileHeader) ? parts[headerIndices[MainProfileHeader]] : string.Empty,
                Belt = headerIndices.ContainsKey(BeltDegreeHeader) ? GetString(parts[headerIndices[BeltDegreeHeader]]) : string.Empty,
                GraduationDate = headerIndices.ContainsKey(GraduationDateHeader) ? GetDateTime(parts[headerIndices[GraduationDateHeader]]) : null,
                GraduatedBy = headerIndices.ContainsKey(GraduatedByHeader) ? GetString(parts[headerIndices[GraduatedByHeader]]) : string.Empty,
                PassportNumber = headerIndices.ContainsKey(PassportNumberHeader) ? GetInteger(parts[headerIndices[PassportNumberHeader]]) : null,
                LatestTraining = headerIndices.ContainsKey(LatestTrainingHeader) ? GetDateTime(parts[headerIndices[LatestTrainingHeader]], true) : null,
                PaymentGroup = headerIndices.ContainsKey(PaymentGroupsHeader) ? parts[headerIndices[PaymentGroupsHeader]] : string.Empty,
                PaymentMethodAdded = headerIndices.ContainsKey(PaymentMethodAddedHeader) ? GetBoolean(parts[headerIndices[PaymentMethodAddedHeader]]) : false,
                PhoneNumber = headerIndices.ContainsKey(PhoneNumberHeader) ? parts[headerIndices[PhoneNumberHeader]] : string.Empty,
                Address = headerIndices.ContainsKey(AddressHeader) ? parts[headerIndices[AddressHeader]] : string.Empty,
                ZipCode = headerIndices.ContainsKey(ZipCodeHeader) ? GetInteger(parts[headerIndices[ZipCodeHeader]]) : null,
                City = headerIndices.ContainsKey(CityHeader) ? parts[headerIndices[CityHeader]] : string.Empty,
                Country = headerIndices.ContainsKey(CountryHeader) ? parts[headerIndices[CountryHeader]] : string.Empty,
                Municipal = headerIndices.ContainsKey(MunicipalHeader) ? parts[headerIndices[MunicipalHeader]] : string.Empty,
                Birthday = headerIndices.ContainsKey(BirthdayHeader) ? GetDateTime(parts[headerIndices[BirthdayHeader]]) : null,
                Age = headerIndices.ContainsKey(AgeHeader) ? GetInteger(parts[headerIndices[AgeHeader]]) : null,
                CreatedAt = headerIndices.ContainsKey(CreatedAtHeader) ? GetDateTime(parts[headerIndices[CreatedAtHeader]]) : null,
                Teams = headerIndices.ContainsKey(TeamsHeader) ? parts[headerIndices[TeamsHeader]].Split(',') : Array.Empty<string>(),
                InactiveTeams = headerIndices.ContainsKey(InactiveTeamsHeader) ? parts[headerIndices[InactiveTeamsHeader]].Split(',') : Array.Empty<string>(),
                Note = headerIndices.ContainsKey(NoteHeader) ? parts[headerIndices[NoteHeader]] : string.Empty
            };
        }

        private string GetString(string value) => value != null && value.Equals("Ukendt", StringComparison.InvariantCultureIgnoreCase) ? string.Empty : value;

        private int? GetInteger(string value)
        {
            if (int.TryParse(value, out var result))
                return result;

            return null;
        }

        private bool GetBoolean(string value) => value.Equals("x", StringComparison.InvariantCultureIgnoreCase);

        private DateTime? GetDateTime(string value, bool useSubstring = false)
        {
            if (useSubstring && value.Length > 10)
                value = value.Substring(0, 10);

            if (DateTime.TryParseExact(value, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
                return date;

            return null;
        }
    }
}
