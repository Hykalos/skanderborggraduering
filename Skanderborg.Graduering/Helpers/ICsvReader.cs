﻿namespace Skanderborg.Graduering.Helpers;

public interface ICsvReader
{
    IEnumerable<CsvMember> GetMembers(IFormFile file);
    IEnumerable<CsvMember> GetMembers(string file);
}
