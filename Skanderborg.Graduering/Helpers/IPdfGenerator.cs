using Skanderborg.Graduering.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace Skanderborg.Graduering.Helpers
{
    public interface IPdfGenerator
    {
        Stream Generate(IEnumerable<Member> members, DateTime graduationDate);
    }
}
