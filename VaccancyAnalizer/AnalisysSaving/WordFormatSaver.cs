using System.Reflection;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace VaccancyAnalizer.AnalisysSaving
{
    public class WordFormatSaver : IAnalysisSaver
    {
        public string savePath {  get; private set; }
        public WordFormatSaver(string savePath) 
        { 
            this.savePath = savePath;
        }

        public string SaveAnalysis(Analysis analysis, bool overwrite = true)
        {
            string filePath = $"{savePath}\\{analysis.query}.docx";
            string directory = Path.GetDirectoryName(filePath)!;
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            if (File.Exists(filePath))
            {
                if (overwrite) File.Delete(filePath);
                else throw new IOException($"WordFormatSaver.SaveAnalysis failed. File {filePath} already exists");
            }

            using var wordDoc = WordprocessingDocument.Create(filePath, WordprocessingDocumentType.Document);
            var mainPart = wordDoc.AddMainDocumentPart();
            mainPart.Document = new Document();
            var body = mainPart.Document.AppendChild(new Body());

            DescribeObject(body, "Summary", analysis.summary);

            int index = 1;
            foreach (var job in analysis.vacancies)
            {
                DescribeObject(body, $"Vaccancy #{index++}", job);
            }

            mainPart.Document.Save();
            return filePath;
        }

        private static void DescribeObject(Body body, string title, object objToDescribe)
        {
            body.Append(CreateParagraph(title, isBold: true, fontSize: "28"));
            body.Append(new Paragraph(new Run(new Text(" "))));

            var props = objToDescribe.GetType()
                                     .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                     .Where(p => p.CanRead);

            foreach (var prop in props)
            {
                var valueObj = prop.GetValue(objToDescribe);

                if (valueObj is IDictionary<string, int> dict)
                {
                    body.Append(CreateParagraph($"{prop.Name}:", isBold: false));

                    foreach (var kv in dict.OrderByDescending(kv => kv.Value))
                    {
                        body.Append(CreateParagraph($"• {kv.Key} — {kv.Value}"));
                    }
                }
                else
                {
                    var text = valueObj?.ToString() ?? String.Empty;
                    body.Append(CreateParagraph($"{prop.Name}: {text}"));
                }
            }

            body.Append(new Paragraph(
                new ParagraphProperties(
                    new ParagraphBorders(
                        new BottomBorder
                        {
                            Val = BorderValues.Single,
                            Color = "auto",
                            Size = 4,
                            Space = 1
                        }))));

            body.Append(new Paragraph(new Run(new Text(" "))));
        }

        private static Paragraph CreateParagraph(string text, bool isBold = false, string fontSize = "24")
        {
            var runProps = new RunProperties();
            if (isBold)
                runProps.Append(new Bold());
            runProps.Append(new FontSize { Val = fontSize });

            var run = new Run(runProps, new Text(text) { Space = SpaceProcessingModeValues.Preserve });
            return new Paragraph(run);
        }
    }
}
