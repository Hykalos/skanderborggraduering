using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Skanderborg.Graduering.Helpers;
using Skanderborg.Graduering.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;

namespace Skanderborg.Graduering.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICsvReader _csvReader;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, ICsvReader csvReader, IMapper mapper)
        {
            _logger = logger;
            _csvReader = csvReader ?? throw new ArgumentNullException(nameof(csvReader));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
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
            var membersString = HttpContext.Session.GetString("GraduationMembers");

            if (string.IsNullOrWhiteSpace(membersString))
                return RedirectToAction(nameof(Index));

            var members = JsonSerializer.Deserialize<IEnumerable<CsvMember>>(membersString);

            return View(members);
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
    }
}
