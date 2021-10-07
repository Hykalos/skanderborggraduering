using Microsoft.AspNetCore.Http;
using Skanderborg.Graduering.Models;
using System.Collections.Generic;

namespace Skanderborg.Graduering.Helpers
{
    public interface ICsvReader
    {
        IEnumerable<CsvMember> GetMembers(IFormFile file);
    }
}
