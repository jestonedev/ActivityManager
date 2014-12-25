using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections.ObjectModel;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using System.Globalization;

namespace ReportModule
{
    /// <summary>
    /// Редактор отчетов Word
    /// </summary>
    public class WordEditor: ReportEditor
    {
        private Dictionary<string, string> xml_contractors = new Dictionary<string, string>() {
        {"table","tbl"},
        {"row","tr"},
        {"cell","tc"},
        {"paragraph","p"}
        };
        
        //Описание стилей: тип стиля, тэг, атрибуты тэга
        private Dictionary<Style, Dictionary<string, Dictionary<string, string>>> styleTags = new Dictionary<Style, Dictionary<string, Dictionary<string, string>>>() {
            { Style.Bold, new  Dictionary<string, Dictionary<string, string>>() { {"b", new Dictionary<string,string>() } }},
            { Style.Italic, new  Dictionary<string, Dictionary<string, string>>() { {"i", new Dictionary<string,string>() } }},
            { Style.Underline, new  Dictionary<string, Dictionary<string, string>>() { {"u", new Dictionary<string,string>() {{"val", "single"}}} }},
            { Style.Strike, new  Dictionary<string, Dictionary<string, string>>() {}}
        };

        //Основной namespace w
        string w = "http://schemas.openxmlformats.org/wordprocessingml/2006/main";

        /// <summary>
        /// Список стилей, которые необходимо применить к каждому последующему элементу
        /// </summary>
        private List<Style> styles = new List<Style>();

        /// <summary>
        /// Класс конвертации xml-замыкателя
        /// </summary>
        /// <param name="values">Список переменных, в которых надо найти унифицированные замыкатели и заменить на зависимые от типа отчета</param>
        /// <returns>Возвращает список переменных отчета с зависимыми от типа отчета xml-замыкателями</returns>
        public override Collection<ReportValue> XmlContractorsConvert(Collection<ReportValue> values)
        {
            return ContractorsConvert(xml_contractors, values);
        }

        /// <summary>
        /// Метод, производящий замену всех шаблонов отчета на значения переменных отчета
        /// </summary>
        /// <param name="reportUnzipPath">Путь до файла отчета</param>
        /// <param name="values">Список переменных</param>
        public override void ReportEditing(string reportUnzipPath, Collection<ReportValue> values)
        {
            ReportEditingContentFile(Path.Combine(reportUnzipPath, 
                "word"+Path.DirectorySeparatorChar+"document.xml"), values);
            string[] headerFiles = Directory.GetFiles(Path.Combine(reportUnzipPath, "word"), "header*.xml");
            foreach (string file in headerFiles)
                ReportEditingContentFile(file, values);
        }

        /// <summary>
        /// Метод, производящий замену специальных тэгов в отчете
        /// </summary>
        /// <param name="reportUnzipPath">Путь до файлов отчета во временной директории</param>
        public override void SpecialTagEditing(string reportUnzipPath)
        {
            string reportContentFile = Path.Combine(reportUnzipPath,
                "word" + Path.DirectorySeparatorChar + "document.xml");
            string[] headerFiles = Directory.GetFiles(Path.Combine(reportUnzipPath, "word"), "header*.xml");
            string [] files = headerFiles.Union(new List<string>() { reportContentFile }).ToArray();
            foreach (string file in files)
            {
                XDocument xdocument = null;
                if (!cachedDocuments.ContainsKey(file) ||
                    cachedDocuments[file] == null)
                {
                    xdocument = XDocument.Load(file, LoadOptions.PreserveWhitespace);
                    cachedDocuments[file] = xdocument;
                }
                else
                    xdocument = cachedDocuments[file];
                List<XElement> xelements = ReportHelper.FindElementsByTag(xdocument.Root, "p");
                foreach (XElement xelement in xelements)
                {
                    prepair_paragraph(xelement);
                    parse_style_tags(xelement);
                    parse_br_tags(xelement);
                }
                xdocument.Save(file, SaveOptions.DisableFormatting);
            }
        }

        private static void prepair_paragraph(XElement xelement)
        {
            if (xelement == null)
                throw new ReportException("Не задана ссылка на элемент документ шаблона");
            List<string> patterns = new List<string>();
            foreach (string spec_tag in Enum.GetNames(typeof(SpecTag)))
            {
                patterns.Add("$" + spec_tag.ToUpper(CultureInfo.CurrentCulture) + "$");
                patterns.Add("$" + spec_tag.ToLower(CultureInfo.CurrentCulture) + "$");
                patterns.Add("$/" + spec_tag.ToUpper(CultureInfo.CurrentCulture) + "$");
                patterns.Add("$/" + spec_tag.ToLower(CultureInfo.CurrentCulture) + "$");
            }
            foreach (string pattern in patterns)
            {
                List<PatternNodeInfoCollection> ppis = new List<PatternNodeInfoCollection>();
                foreach (XNode node in xelement.Nodes())
                {
                    ppis = ReportHelper.GetNodePatternPartsInfo(node, pattern, ppis);
                }
                for (int i = 0; i < ppis.Count; i++)
                {
                    if (ppis[i].Items[ppis[i].Items.Count - 1].IsClosingPatternNode)
                    {
                        XNode newNode = ReportHelper.ReplaceNodePatternPart(ppis[i].Items[0], pattern);
                        ReportHelper.RebindNodePatternPartsInfo(newNode, ppis[i].Items[0].Node, ppis);
                        for (int j = 1; j < ppis[i].Items.Count; j++)
                        {
                            newNode = ReportHelper.ReplaceNodePatternPart(ppis[i].Items[j], "");
                            ReportHelper.RebindNodePatternPartsInfo(newNode, ppis[i].Items[j].Node, ppis);
                        }
                    }
                }
            }
        }

        private static SortedDictionary<int, TagInfo> get_style_tags(string text)
        {
            SortedDictionary<int, TagInfo> dic = new SortedDictionary<int, TagInfo>();
            foreach (string spec_tag in Enum.GetNames(typeof(SpecTag)))
            {
                Match match = Regex.Match(text, Regex.Escape(@"$" + spec_tag + @"$"), RegexOptions.IgnoreCase);
                while (match.Success)
                {
                    dic.Add(match.Index, new TagInfo((SpecTag)Enum.Parse(typeof(SpecTag), spec_tag),
                        SpecTagType.OpenTag));
                    match = match.NextMatch();
                }
                match = Regex.Match(text, Regex.Escape(@"$/" + spec_tag + @"$"), RegexOptions.IgnoreCase);
                while (match.Success)
                {
                    dic.Add(match.Index, new TagInfo((SpecTag)Enum.Parse(typeof(SpecTag), spec_tag),
                        SpecTagType.CloseTag));
                    match = match.NextMatch();
                }
            }
            return dic;
        }

        private static Style get_style_by_spec_tag(SpecTag spec_tag)
        {
            switch (spec_tag)
            {
                case SpecTag.B: return Style.Bold;
                case SpecTag.I: return Style.Italic;
                case SpecTag.U: return Style.Underline;
                case SpecTag.S: return Style.Strike;
                default: return Style.None;
            }
        }

        private static void parse_br_tags(XElement xelement)
        {
            //throw new NotImplementedException();
        }

        private void parse_style_tags(XElement xelement)
        {
            XElement new_xelement = new XElement(xelement);
            new_xelement.RemoveNodes();
            foreach (var child_element in xelement.Elements())
                if (child_element.Name.LocalName == "r")
                {
                    XElement textElement = child_element.Element(XName.Get("t", w));
                    if (textElement == null)
                    {
                        new_xelement.Add(child_element);
                        continue;
                    }
                    string content = textElement.Value;
                    SortedDictionary<int, TagInfo> style_tags = get_style_tags(content);
                    if (style_tags.Count > 0)
                    {
                        string[] spliters = new string[style_tags.Count * 2];
                        int i = 0;
                        foreach (var spec_tag in style_tags)
                        {
                            if (spec_tag.Value.tag_type == SpecTagType.OpenTag)
                            {
                                spliters[i] = @"$" + spec_tag.Value.tag.ToString().ToLower(CultureInfo.CurrentCulture) + @"$";
                                spliters[i + 1] = @"$" + spec_tag.Value.tag.ToString().ToUpper(CultureInfo.CurrentCulture) + @"$";
                            }
                            else
                            {
                                spliters[i] = @"$/" + spec_tag.Value.tag.ToString().ToLower(CultureInfo.CurrentCulture) + @"$";
                                spliters[i + 1] = @"$/" + spec_tag.Value.tag.ToString().ToUpper(CultureInfo.CurrentCulture) + @"$";
                            }
                            i = i + 2;
                        }
                        string[] values = content.Split(spliters, StringSplitOptions.None);
                        i = 0;
                        foreach (string value in values)
                        {
                            TagInfo spec_tag_info = null;
                            int j = 1;
                            foreach (var spec_tag in style_tags)
                            {
                                if (i == j)
                                {
                                    spec_tag_info = spec_tag.Value;
                                    break;
                                }
                                j++;
                            }
                            if (spec_tag_info != null)
                            {
                                if (spec_tag_info.tag_type == SpecTagType.OpenTag)
                                    styles.Add(get_style_by_spec_tag(spec_tag_info.tag));
                                else
                                    styles.Remove(get_style_by_spec_tag(spec_tag_info.tag));
                            }
                            i++;
                            if (String.IsNullOrEmpty(value))
                                continue;
                            XElement new_element = new XElement(child_element);
                            textElement = new_element.Element(XName.Get("t", w));
                            textElement.Value = value;
                            if (value != value.Trim() && textElement.Attribute(XNamespace.Xml + "space") == null)
                                textElement.Add(new XAttribute(XNamespace.Xml + "space", "preserve"));
                            foreach (Style style in styles)
                                foreach (var styleTag in styleTags[style])
                                {
                                    XElement tag = new XElement(XName.Get(styleTag.Key, w));
                                    XElement rPrElement = child_element.Element(XName.Get("rPr", w));
                                    foreach (var attribute in styleTag.Value)
                                        tag.Add(new XAttribute(XName.Get(attribute.Key, w), attribute.Value));
                                    if (rPrElement == null)
                                        new_element.Add(new XElement(XName.Get("rPr", w)));
                                    new_element.Element(XName.Get("rPr", w)).Add(tag);
                                }
                            new_xelement.Add(new_element);
                        }
                    }
                    else
                    {
                        XElement new_element = new XElement(child_element);
                        foreach (Style style in styles)
                            foreach (var styleTag in styleTags[style])
                            {
                                XElement tag = new XElement(XName.Get(styleTag.Key, w));
                                XElement rPrElement = child_element.Element(XName.Get("rPr", w));
                                foreach (var attribute in styleTag.Value)
                                    tag.Add(new XAttribute(XName.Get(attribute.Key, w), attribute.Value));
                                if (rPrElement == null)
                                    new_element.Add(new XElement(XName.Get("rPr", w)));
                                new_element.Element(XName.Get("rPr", w)).Add(tag);
                            }
                        new_xelement.Add(new_element);
                    }
                } else
                    new_xelement.Add(child_element);
            xelement.ReplaceWith(new_xelement);
        }
    }
}
