using Microsoft.AspNetCore.Hosting;
using PdfSharp.Drawing;
using PdfSharp.Fonts;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using Skanderborg.Graduering.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Skanderborg.Graduering.Helpers
{
    public class PdfGenerator : IPdfGenerator
    {
        private const int X = 240;
        private const int X2 = 235;
        private readonly IWebHostEnvironment _env;

        public PdfGenerator(IWebHostEnvironment env)
        {
            _env = env ?? throw new ArgumentNullException(nameof(env));

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            EZFontResolver fontResolver = EZFontResolver.Get;
            GlobalFontSettings.FontResolver = fontResolver;

            fontResolver.AddFont("Calibri", XFontStyle.Bold, $"{_env.ContentRootPath}/wwwroot/fonts/calibri-bold.ttf");
        }

        public Stream Generate(IEnumerable<Member> members, DateTime graduationDate)
        {
            var template = PdfReader.Open($"{_env.ContentRootPath}/wwwroot/files/Kupcertifikat.pdf", PdfDocumentOpenMode.Import).Pages[0];

            PdfDocument pdf = new PdfDocument();

            foreach(Member member in members)
            {
                var page = pdf.AddPage(template);

                GeneratePage(page, member, graduationDate);
            }

            var stream = new MemoryStream();

            pdf.Save(stream);
            stream.Position = 0;

            return stream;
        }

        private void GeneratePage(PdfPage page, Member member, DateTime graduationDate)
        {
            var gfx = XGraphics.FromPdfPage(page);

            var font = new XFont("Calibri", 14, XFontStyle.Bold);

            gfx.DrawString(member.Name, font, XBrushes.Black, new XRect(X, 200, 55, 0));
            gfx.DrawString(member.Birthday.ToString("dd-MM-yyyy"), font, XBrushes.Black, new XRect(X, 221, 55, 0));
            gfx.DrawString($"{member.Degree} - {graduationDate:dd-MM-yyyy}", font, XBrushes.Black, new XRect(X, 251, 55, 0));


            gfx.DrawString(member.Name, font, XBrushes.Black, new XRect(X2, 487, 55, 0));
            gfx.DrawString(member.Birthday.ToString("dd-MM-yyyy"), font, XBrushes.Black, new XRect(X2, 511, 55, 0));
            gfx.DrawString($"{member.Degree} - {graduationDate:dd-MM-yyyy}", font, XBrushes.Black, new XRect(X2, 560, 55, 0));
        }
    }
}
