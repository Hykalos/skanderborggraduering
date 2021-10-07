using Microsoft.AspNetCore.Hosting;
using PdfSharp.Drawing;
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
        private readonly IWebHostEnvironment _env;

        public PdfGenerator(IWebHostEnvironment env)
        {
            _env = env ?? throw new ArgumentNullException(nameof(env));
        }

        public Stream Generate(IEnumerable<Member> members, DateTime graduationDate)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

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

            XFont font = new XFont("Calibri", 14, XFontStyle.Regular);

            gfx.DrawString(member.Name, font, XBrushes.Black, new XRect(5, 10, 55, 0));
        }
    }
}
