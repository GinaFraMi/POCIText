using iText.Html2pdf;
using HandlebarsDotNet;
using iText.Layout.Font;
using iText.Html2pdf.Resolver.Font;
using iText.IO.Font;

namespace POCIText
{
    class Program
    {
        static void Main(string[] args)
        {
            string htmlFilePath = "template.html";
            string outputPdfPath = "documento.pdf";

            if (File.Exists(outputPdfPath))
            {
                File.Delete("documento(anterior).pdf");
                File.Move(outputPdfPath, "documento(anterior).pdf");
            }

            ConvertHtmlToPdf(htmlFilePath, outputPdfPath);

            Console.WriteLine("Se ha generado el archivo PDF con éxito.");
        }

        static void ConvertHtmlToPdf(string htmlFilePath, string outputPdfPath)
        {
            using (FileStream outputStream = new FileStream(outputPdfPath, FileMode.Create))
            {
                string htmlContent = File.ReadAllText(htmlFilePath);

                var handler = Handlebars.Compile(htmlContent);
                string result = handler(new Information
                {
                    certificadeCode = "42738499092812000",
                    points = "10,000",
                    price = "1,000.00"
                });

                string directory = "Fonts";
                var fontPaths = Directory.GetFiles(directory, "*.ttf");
                var fonts = fontPaths.Select(path => FontProgramFactory.CreateFont(path, PdfEncodings.IDENTITY_H, true)).ToList();


                ConverterProperties properties = new ConverterProperties();
                FontProvider fontProvider = new DefaultFontProvider();

                foreach (var fontPath in fontPaths)
                {
                    fontProvider.AddFont(fontPath);
                }

                properties.SetFontProvider(fontProvider);

                HtmlConverter.ConvertToPdf(result, outputStream, properties);
            }
        }
    }
}
