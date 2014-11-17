using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
using System.Globalization;

namespace ReportModule
{
    class WriterEditor: ReportEditor
    {
        private Dictionary<string, string> xml_contractors = new Dictionary<string, string>() {
        {"table","table"},
        {"row","table-row"},
        {"cell","table-cell"},
        {"paragraph","p"}
        };

        /// <summary>
        /// Список стилей, которые необходимо применить к каждому последующему элементу
        /// </summary>
        private List<Style> styles = new List<Style>();

        public override Collection<ReportValue> XmlContractorsConvert(Collection<ReportValue> values)
        {
            return ContractorsConvert(xml_contractors, values);
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

        private static void prepair_paragraph(XElement xelement, OOStyleSheet style_sheet)
        {
            XElement new_paragraph = new XElement(xelement);
            new_paragraph.RemoveNodes();
            foreach (XNode xnode in xelement.Nodes())
            {
                if (xnode.NodeType == System.Xml.XmlNodeType.Text)
                {
                    string content = ((XText)xnode).Value;
                    XElement span = new XElement(XName.Get("span", OOStyleSheet.XmlnsText),
                           new XText(content));
                    if (xelement.Attribute(XName.Get("style-name", OOStyleSheet.XmlnsText)) != null)
                    {
                        string style_name = style_sheet.CopyStyle(
                            xelement.Attribute(XName.Get("style-name", OOStyleSheet.XmlnsText)).Value, "text");
                        span.Add(new XAttribute(XName.Get("style-name", OOStyleSheet.XmlnsText), style_name));
                    }
                    new_paragraph.Add(span);
                } else
                if (xnode.NodeType == System.Xml.XmlNodeType.Element)
                    new_paragraph.Add(new XElement((XElement)xnode));
            }
            xelement.ReplaceNodes(new_paragraph.Nodes());
        }

        private static SortedDictionary<int, TagInfo> get_style_tags(string text)
        {
            SortedDictionary<int, TagInfo> dic = new SortedDictionary<int, TagInfo>();
            foreach (string spec_tag in Enum.GetNames(typeof(SpecTag)))
            {
                if (Regex.IsMatch(text, Regex.Escape("$b$")))
                {
                    int i = 0;
                    i = i + 1;
                }
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

        private void parse_element_tag(XElement xroot, XElement xelement, OOStyleSheet style_sheet)
        {
            SortedDictionary<int, TagInfo> style_tags = get_style_tags(xelement.Value);
            if (style_tags.Count > 0)
            {
                string content = xelement.Value;
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
                    XElement new_element = new XElement(xelement);
                    new_element.Value = value;
                    string style_name = "";
                    if (new_element.Attribute(
                            XName.Get("style-name", OOStyleSheet.XmlnsText)) != null)
                    {
                        style_name = style_sheet.CopyStyle(new_element.Attribute(
                            XName.Get("style-name", OOStyleSheet.XmlnsText)).Value, "text");
                        new_element.Attribute(XName.Get("style-name",
                            OOStyleSheet.XmlnsText)).Value = style_name;
                    }
                    else
                    {
                        style_name = style_sheet.CreateStyle("text");
                        new_element.Add(new XAttribute(XName.Get("style-name",
                            OOStyleSheet.XmlnsText), style_name));
                    }
                    foreach (Style style in styles)
                        style_sheet.ApplyStyle(style_name, style);
                    xroot.Add(new_element);
                }
            }
            else
            {
                XElement new_element = new XElement(xelement);
                string style_name = "";
                if (new_element.Attribute(
                        XName.Get("style-name", OOStyleSheet.XmlnsText)) != null)
                {
                    style_name = style_sheet.CopyStyle(new_element.Attribute(
                        XName.Get("style-name", OOStyleSheet.XmlnsText)).Value, "text");
                    new_element.Attribute(XName.Get("style-name",
                        OOStyleSheet.XmlnsText)).Value = style_name;
                    foreach (Style style in styles)
                        style_sheet.ApplyStyle(style_name, style);
                }
                xroot.Add(new_element);
            }
        }

        private void parse_style_tags(XElement xroot, OOStyleSheet style_sheet)
        {
            XElement new_xroot = new XElement(xroot);
            new_xroot.RemoveAll();
            foreach (XElement xelement in xroot.Elements())
                parse_element_tag(new_xroot, xelement, style_sheet);
            xroot.ReplaceNodes(new_xroot.Nodes());
        }

        private static void parse_br_tags(XElement xroot)
        {
            XElement new_xroot = new XElement(xroot);
            new_xroot.RemoveAll();
            foreach (XElement xelement in xroot.Elements())
            {
                string[] values = xelement.Value.Split(new string[] { @"$br$", @"$BR$" }, StringSplitOptions.None);
                if (values.Length == 1)
                {
                    XElement new_element = new XElement(xelement);
                    new_xroot.Add(new_element);
                    continue;
                }
                int i = 0;
                foreach (string value in values)
                {
                    if (i == 0)
                    {
                        XElement new_element = new XElement(xelement);
                        new_element.Value = value;
                        new_xroot.Add(new_element);
                    }
                    else
                    {
                        if (xroot.Attribute(XName.Get("style-name", OOStyleSheet.XmlnsText)) != null)
                        {
                            new_xroot.Add(new XAttribute(XName.Get("style-name", OOStyleSheet.XmlnsText),
                                xroot.Attribute(XName.Get("style-name", OOStyleSheet.XmlnsText)).Value));
                        }
                        xroot.AddBeforeSelf(new_xroot);
                        new_xroot = new XElement(xroot);
                        new_xroot.RemoveAll();
                        XElement new_element = new XElement(xelement);
                        new_element.Value = value;
                        new_xroot.Add(new_element);
                    }
                    i++;
                }
            }
            xroot.ReplaceNodes(new_xroot.Nodes());
        }

        private static void parse_sbr_tags(XElement xroot)
        {
            XElement new_xroot = new XElement(xroot);
            new_xroot.RemoveAll();
            foreach (XElement xelement in xroot.Elements())
            {
                string[] values = xelement.Value.Split(new string[] { @"$sbr$", @"$SBR$" }, StringSplitOptions.None);
                if (values.Length == 1)
                {
                    XElement new_element = new XElement(xelement);
                    new_xroot.Add(new_element);
                    continue;
                }
                XElement new_xelement = new XElement(xelement);
                new_xelement.Value = "";
                int i = 0;
                foreach (string value in values)
                {
                    if (i != 0)
                    {
                        XElement break_element = new XElement(XName.Get("line-break", OOStyleSheet.XmlnsText));
                        new_xelement.Add(break_element);
                    }
                    new_xelement.Add(new XText(value));
                    i++;
                }
                new_xroot.Add(new_xelement);
            }
            xroot.ReplaceNodes(new_xroot.Nodes());
        }

        /// <summary>
        /// Метод, производящий замену специальных тэгов в отчете
        /// </summary>
        /// <param name="report_unzip_path">Путь до файлов отчета во временной директории</param>
        public override void SpecialTagEditing(string report_unzip_path)
        {
            XDocument xdocument = XDocument.Load(Path.Combine(report_unzip_path, "content.xml"), 
                LoadOptions.PreserveWhitespace);
            OOStyleSheet style_sheet = new OOStyleSheet(xdocument);
            List<XElement> xelements = ReportHelper.FindElementsByTag(xdocument.Root, "p");
            foreach(XElement xelement in xelements)
            {
                prepair_paragraph(xelement, style_sheet);
                parse_style_tags(xelement, style_sheet);
                parse_br_tags(xelement);
                parse_sbr_tags(xelement);
            }
           xdocument.Save(Path.Combine(report_unzip_path, "content.xml"), SaveOptions.DisableFormatting);
        }
    }
}
