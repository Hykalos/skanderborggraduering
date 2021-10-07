using Microsoft.AspNetCore.Http;
using Skanderborg.Graduering.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace Skanderborg.Graduering.Helpers
{
    public class CsvReader : ICsvReader
    {
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

            //Skip first line as it's the headers
            for(var i = 1; i < lines.Count; ++i)
            {
                members.Add(ReadMember(lines[i]));
            }

            return members.ToArray();
        }

        private CsvMember ReadMember(string line)
        {
            var parts = line.Split(';');

            return new CsvMember
            {
                Name = parts[0],
                Email = parts[1],
                SecondaryEmail = parts[2],
                MainProfile = parts[3],
                Gender = parts[4] == "K" ? Gender.Female : Gender.Male,
                Belt = parts[5],
                GraduationDate = string.IsNullOrWhiteSpace(parts[6]) ? null : DateTime.ParseExact(parts[6], "dd-MM-yyyy", CultureInfo.CurrentCulture),
                PassportNumber = string.IsNullOrWhiteSpace(parts[7]) ? null : int.Parse(parts[7]),
                PaymentGroup = parts[8],
                PaymentMethodAdded = parts[9] == "JA",
                PhoneNumber = parts[10],
                MobileNumber = parts[11],
                Address = parts[12],
                ZipCode = string.IsNullOrWhiteSpace(parts[13]) ? 0 : int.Parse(parts[13]),
                City = parts[14],
                Municipal = parts[15],
                Birthday = DateTime.ParseExact(parts[16].Substring(0, 10), "dd-MM-yyyy", CultureInfo.CurrentCulture),
                Age = int.Parse(parts[16].Substring(12, parts[16].Length - 13)),
                CreatedAt = DateTime.ParseExact(parts[17], "dd-MM-yyyy", CultureInfo.CurrentCulture),
                BaseTeam = parts[18],
                SpecialTeams = parts[19],
                Note = parts[20]
            };
        }
    }
}
