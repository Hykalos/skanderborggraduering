namespace Skanderborg.Graduering.Integrations.MentoClub;

public sealed class MentoClubService : IMemberSystemService
{
    private readonly CookieContainer _cookieContainer;
    private readonly HttpClient _httpClient;

    public MentoClubService(MentoClubConfiguration configuration)
    {
        _cookieContainer = new CookieContainer();
        var handler = new HttpClientHandler
        {
            CookieContainer = _cookieContainer
        };

        _httpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri(configuration.BaseUrl)
        };
    }

    public async Task<string> GetMemberExportCsv(string username, string password)
    {
        var cookie = await GetApplicationCookie(username, password);

        throw new NotImplementedException();
    }

    private async Task<string> GetApplicationCookie(string username, string password)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, "login/login");
        request.Headers.Add("Accept", "*/*");
        request.Headers.Add("Accept-Encoding", "gzip, defaulte, br");
        request.Headers.Add("Connection", "keep-alive");
        request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            {
                "Email", username
            },
            {
                "Password", password
            },
            {
                "RememberMe", "false"
            }
        });

        var response = await _httpClient.SendAsync(request);

        return _cookieContainer.GetAllCookies()[".AspNet.ApplicationCookie"]?.Value ?? string.Empty;
    }
}
