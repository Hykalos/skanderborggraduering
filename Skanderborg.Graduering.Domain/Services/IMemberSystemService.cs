namespace Skanderborg.Graduering.Domain.Services;

public interface IMemberSystemService
{
    /// <summary>
    /// Get the CSV export for members in the system
    /// </summary>
    /// <param name="username">The username to use for exporting</param>
    /// <param name="password">The password to use for exporting</param>
    Task<string> GetMemberExportCsv(string username, string password); 
}
