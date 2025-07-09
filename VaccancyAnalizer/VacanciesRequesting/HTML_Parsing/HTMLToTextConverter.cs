using DocumentFormat.OpenXml.EMMA;
using HtmlAgilityPack;
using System.Text;
using System.Xml.Linq;

namespace VaccancyAnalizer.VacanciesRequesting.HTML_Parsing
{
    public static class HTMLToTextConverter
    {
        /// <summary>
        /// Returns the text contents of a node, formatting paragraphs, <br>, lists, and headings.
        /// </summary>
        public static string ToFormattedText(this HtmlNode node)
        {
            if (node == null)
                return string.Empty;

            var sb = new StringBuilder();
            ParseNode(node, sb, 0, HTML_ListType.None);
            return sb.ToString().TrimEnd();
        }

        private static void ParseNode(HtmlNode node, StringBuilder sb, int indent, HTML_ListType listContext)
        {
            switch (node.NodeType)
            {
                case HtmlNodeType.Text:
                    ParseTextNode(node, sb, indent, listContext);
                    break;

                case HtmlNodeType.Element:
                    ParseElementNode(node, sb, indent, listContext);
                    break;
            }
        }

        private static void ParseTextNode(HtmlNode node, StringBuilder sb, int indent, HTML_ListType listContext)
        {
            //Cleaning text
            var text = HtmlEntity.DeEntitize(node.InnerText);
            if (!string.IsNullOrWhiteSpace(text))
            {
                var t = text
                    .Replace("\r", "")
                    .Replace("\n", " ")
                    .Trim();

                if (t.Length > 0)
                {
                    sb.Append(t);
                }
            }
        }

        private static void ParseElementNode(HtmlNode node, StringBuilder sb, int indent, HTML_ListType listContext)
        {
            string name = node.Name.ToLower();

            switch (name)
            {
                // Parsing aragraphs
                case "p":
                    {
                        if (sb.Length > 0 && !EndsWithNewLine(sb))
                            sb.AppendLine();

                        foreach (var child in node.ChildNodes)
                            ParseNode(child, sb, indent, HTML_ListType.None);

                        sb.AppendLine();
                        sb.AppendLine();
                    }
                    break;

                // Parsing line breaks
                case "br":
                    sb.AppendLine();
                    break;

                //Parsing headers
                case "h1":
                case "h2":
                case "h3":
                case "h4":
                case "h5":
                case "h6":
                    {
                        var prefix = name switch
                        {
                            "h1" => "# ",
                            "h2" => "## ",
                            "h3" => "### ",
                            "h4" => "#### ",
                            "h5" => "##### ",
                            "h6" => "###### ",
                            _ => ""
                        };

                        if (sb.Length > 0 && !EndsWithNewLine(sb))
                            sb.AppendLine();

                        sb.Append(prefix);

                        foreach (var child in node.ChildNodes)
                            ParseNode(child, sb, indent, HTML_ListType.None);

                        sb.AppendLine();
                        sb.AppendLine();
                    }
                    break;

                // Parsing numbered list
                case "ol":
                    {
                        int counter = 1;

                        if (sb.Length > 0 && !EndsWithNewLine(sb))
                            sb.AppendLine();

                        foreach (var li in node.Elements("li"))
                        {
                            sb.Append(new string(' ', indent * 2));
                            sb.Append($"{counter}. ");

                            foreach (var child in li.ChildNodes)
                                ParseNode(child, sb, indent + 1, HTML_ListType.Ordered);

                            sb.AppendLine();
                            counter++;
                        }

                        sb.AppendLine();
                    }
                    break;

                // Parsing marked list
                case "ul":
                    {

                        if (sb.Length > 0 && !EndsWithNewLine(sb))
                            sb.AppendLine();

                        foreach (var li in node.Elements("li"))
                        {
                            sb.Append(new string(' ', indent * 2));
                            sb.Append("- ");
                            foreach (var child in li.ChildNodes)
                                ParseNode(child, sb, indent + 1, HTML_ListType.Unordered);

                            sb.AppendLine();
                        }

                        sb.AppendLine();
                    }
                    break;

                case "li":
                    {
                        sb.Append(new string(' ', indent * 2));
                        if (listContext == HTML_ListType.Ordered)
                        {
                            sb.Append("1. ");
                        }
                        else
                        {
                            sb.Append("- ");
                        }

                        foreach (var child in node.ChildNodes)
                            ParseNode(child, sb, indent + 1, listContext);

                        sb.AppendLine();
                    }
                    break;

                // Parsing links
                case "a":
                    {
                        // Сначала выводим текст ссылки (InnerText)
                        foreach (var child in node.ChildNodes)
                            ParseNode(child, sb, indent, HTML_ListType.None);

                        // После текста можно добавить сам href в квадратных скобках
                        var href = node.GetAttributeValue("href", null);
                        if (!string.IsNullOrEmpty(href))
                        {
                            sb.Append($" [{href.Trim()}]");
                        }
                    }
                    break;

                // Removing other tags
                default:
                    {
                        foreach (var child in node.ChildNodes)
                            ParseNode(child, sb, indent, listContext);
                    }
                    break;
            }
        }
        
        private static bool EndsWithNewLine(StringBuilder sb)
        {
            if (sb.Length == 0) return false;
            return sb[sb.Length - 1] == '\n';
        }
    }
}
