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
        await Login(username, password);

        var csv = await GetCsv();

        return csv;
    }

    private async Task Login(string username, string password)
    {
        var request = GetRequestMessage(HttpMethod.Post, "login/login");
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

        await _httpClient.SendAsync(request);
    }

    private async Task<string> GetCsv()
    {
        var request = GetRequestMessage(HttpMethod.Post, "members/export");
        request.Content = new StringContent(
            "ExportMemberID=true&ExportMemberID=false&ExportParent=true&ExportParent=false&ExportTeams=true&ExportTeams=false&ExportPaymentGroups=true&ExportPaymentGroups=false&ExportCreditCardStatus=true&ExportCreditCardStatus=false&ExportEmail=true&ExportEmail=false&ExportAge=true&ExportAge=false&ExportSex=true&ExportSex=false&ExportBirthday=true&ExportBirthday=false&ExportCreatedDate=true&ExportCreatedDate=false&ExportLatestTraining=true&ExportLatestTraining=false&ExportAddress=true&ExportAddress=false&ExportZip=true&ExportZip=false&ExportCity=true&ExportCity=false&ExportMunicipality=true&ExportMunicipality=false&ExportCountry=true&ExportCountry=false&ExportPhone=true&ExportPhone=false&ExportRank=true&ExportRank=false&ExportRankDate=true&ExportRankDate=false&ExportRankChangedBy=true&ExportRankChangedBy=false&ExportPassportNumber=true&ExportPassportNumber=false&ExportNote=true&ExportNote=false&IncludeInactive=false",
            System.Text.Encoding.UTF8,
            "application/x-www-form-urlencoded");

        var response = await _httpClient.SendAsync(request);

        var stream = await response.Content.ReadAsStreamAsync();

        StreamReader reader = new StreamReader(stream, Encoding.GetEncoding("iso-8859-1"));
        List<string> lines = new List<string>();

        while (reader.Peek() >= 0)
        {
            var line = reader.ReadLine();

            if(!string.IsNullOrWhiteSpace(line))
                lines.Add(line);
        }

        return string.Join("\r\n", lines);
    }

    private static HttpRequestMessage GetRequestMessage(HttpMethod method, string path)
    {
        var request = new HttpRequestMessage(method, path);
        request.Headers.Add("Accept", "*/*");
        request.Headers.Add("Connection", "keep-alive");

        return request;
    }
}
