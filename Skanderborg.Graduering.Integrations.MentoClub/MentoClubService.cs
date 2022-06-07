namespace Skanderborg.Graduering.Integrations.MentoClub;

public sealed class MentoClubService : IMemberSystemService
{
    private readonly HttpClient _httpClient;

    public MentoClubService(MentoClubConfiguration configuration)
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri(configuration.BaseUrl)
        };
    }

    public Task<string> GetMemberExportCsv(string username, string password)
    {
        throw new NotImplementedException();
    }

    private async Task<string> GetApplicationCookie(string username, string password)
    {
        throw new NotImplementedException();
    }
}
