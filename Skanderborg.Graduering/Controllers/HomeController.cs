namespace Skanderborg.Graduering.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ICsvReader _csvReader;
    private readonly IMapper _mapper;
    private readonly IMemberSystemService _memberSystemService;
    private readonly IPdfGenerator _generator;

    public HomeController(ILogger<HomeController> logger, ICsvReader csvReader, IMapper mapper, IMemberSystemService memberSystemService, IPdfGenerator generator)
    {
        _logger = logger;
        _csvReader = csvReader;
        _mapper = mapper;
        _memberSystemService = memberSystemService;
        _generator = generator;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> LoginAndFetchFile(LoginDto loginDto)
    {
        var membersCsv = await _memberSystemService.GetMemberExportCsv(loginDto.Username, loginDto.Password);

        // TODO: save members in session

        return RedirectToAction(nameof(MemberSelect));
    }

    [HttpPost]
    public IActionResult UploadFile(IFormFile file)
    {
        if (file == null)
            return RedirectToAction(nameof(Index));

        var members = _csvReader.GetMembers(file);

        HttpContext.Session.SetString("GraduationMembers", JsonSerializer.Serialize(members));

        return RedirectToAction(nameof(MemberSelect));
    }

    public IActionResult MemberSelect()
    {
        var members = GetMembersFromSession();

        if (members == null)
            return RedirectToAction(nameof(Index));

        return View(members);
    }

    public IActionResult GeneratePdf(IEnumerable<Guid> selectedMembers, IEnumerable<string> selectedDegrees, DateTime graduationDate)
    {
        var members = GetMembersFromSession();

        if (members == null || selectedMembers == null || !selectedMembers.Any())
            return RedirectToAction(nameof(MemberSelect));

        var filteredMembers = members.Where(m => selectedMembers.Contains(m.Id)).ToArray();

        var mappedMembers = _mapper.Map<IEnumerable<Member>>(filteredMembers);

        foreach(var mappedMember in mappedMembers)
        {
            mappedMember.Degree = selectedDegrees.First(d => Guid.Parse(d.Split(';')[0]) == mappedMember.Id).Split(';')[1];
        }

        var stream = _generator.Generate(mappedMembers, graduationDate);

        return File(stream, "application/pdf", "Certifikater.pdf");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    private IEnumerable<CsvMember> GetMembersFromSession()
    {
        var membersString = HttpContext.Session.GetString("GraduationMembers");

        if (string.IsNullOrWhiteSpace(membersString))
            return null;

        var members = JsonSerializer.Deserialize<IEnumerable<CsvMember>>(membersString);

        return members;
    }
}
