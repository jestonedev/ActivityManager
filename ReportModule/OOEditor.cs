using ExtendedTypes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace ReportModule
{
    internal class OOEditor: ReportEditor
    {
        protected Dictionary<string, string> xml_contractors = new Dictionary<string, string>() {
        {"table","table"},
        {"row","table-row"},
        {"cell","table-cell"},
        {"paragraph","p"}
        };

        /// <summary>
        /// Список стилей, которые необходимо применить к каждому последующему элементу
        /// </summary>
        protected List<Style> styles = new List<Style>();

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
                }
                else
                    if (xnode.NodeType == System.Xml.XmlNodeType.Element)
                        new_paragraph.Add(new XElement((XElement)xnode));
            }
            xelement.ReplaceNodes(new_paragraph.Nodes());
        }

        private void parse_element_tag(XElement xroot, XElement xelement, OOStyleSheet style_sheet)
        {
            SortedDictionary<int, TagInfo> style_tags = GetStyleTags(xelement.Value);
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
                            styles.Add(ReportHelper.GetStyleBySpecTag(spec_tag_info.tag));
                        else
                            styles.Remove(ReportHelper.GetStyleBySpecTag(spec_tag_info.tag));
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

        private static List<XElement> parse_br_tags(XElement xroot)
        {
            List<XElement> new_xelements = new List<XElement>();
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
                        new_xelements.Add(new_xroot);
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
            new_xelements.Add(xroot);
            return new_xelements;
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
            ReportEditingContentFile(Path.Combine(reportUnzipPath, "content.xml"), values);
            ReportEditingContentFile(Path.Combine(reportUnzipPath, "styles.xml"), values);
        }

        protected override void WriteTable(TableReportValue reportValue, XDocument document)
        {
            if (reportValue == null)
                throw new ReportException("Не задана ссылка на таблицу подставляемых значений шаблона");
            if (document == null)
                throw new ReportException("Не задана ссылка на документ шаблона");
            List<XElement> elements = ReportHelper.FindElementsByTag(document.Root, reportValue.XmlContractor);
            List<string> patterns = new List<string>();
            foreach (string column in reportValue.Table.Columns)
                patterns.Add("$" + column + "$");
            foreach (XElement element in elements)
            {
                string element_value = element.ToString(SaveOptions.DisableFormatting);
                int pattern_match_count = 0; //Число шаблонов, найденных в элементе
                foreach (string pattern in patterns)
                    if (ReportHelper.MatchesPattern(element, pattern) > 0)
                        pattern_match_count++;
                if (pattern_match_count > 0 && ((double)(patterns.Count) / 2) <= pattern_match_count)
                {
                    List<XElement> new_elements = new List<XElement>();
                    Console.WriteLine("Заполняем табличные данные отчета");
                    int count = 0;
                    foreach (ReportRow row in reportValue.Table)
                    {
                        if (row == null)
                            throw new ReportException("В коллекции Table отсутствует ссылка на объект класса ReportRow");
                        count++;
                        if (count % 500 == 0)
                            Console.WriteLine(String.Format(CultureInfo.CurrentCulture, "Заполнено {0} из {1} строк", count, reportValue.Table.Count));
                        XElement new_element = XElement.Parse(element_value, LoadOptions.PreserveWhitespace);
                        //Заменить шаблоны для каждой колонки
                        for (int i = 0; i < patterns.Count; i++)
                            WritePatternText(new_element, patterns[i], row[i].Value);
                        new_elements.Add(new_element);
                    }
                    Console.WriteLine("Заполнение табличных данных отчета закончено");
                    if (reportValue.XmlContractor == "table-cell")
                    {
                        XElement table = element.Parent.Parent;
                        XElement columnDef = table.Element(XName.Get("table-column", OOStyleSheet.XmlnsTable));
                        if (columnDef == null)
                            continue;
                        XAttribute num_col_rep = columnDef.Attribute(XName.Get("number-columns-repeated", OOStyleSheet.XmlnsTable));
                        if (num_col_rep == null)
                        {
                            num_col_rep = new XAttribute(XName.Get("number-columns-repeated", OOStyleSheet.XmlnsTable), new_elements.Count);
                            columnDef.Add(num_col_rep);
                        }
                        num_col_rep.Value = new_elements.Count.ToString(CultureInfo.CurrentCulture);
                    }
                    foreach (XElement new_element in new_elements)
                        element.AddBeforeSelf(new_element);
                    element.Remove();
                }
            }
        }

        /// <summary>
        /// Метод, производящий замену специальных тэгов в отчете
        /// </summary>
        /// <param name="reportUnzipPath">Путь до файлов отчета во временной директории</param>
        public override void SpecialTagEditing(string reportUnzipPath)
        {
            XDocument xdocument = null;
            XDocument xstyles = null;
            string reportContentFile = Path.Combine(reportUnzipPath, "content.xml");
            string reportStylesFile = Path.Combine(reportUnzipPath, "styles.xml");
            if (!cachedDocuments.ContainsKey(reportContentFile) ||
                cachedDocuments[reportContentFile] == null)
            {
                xdocument = XDocument.Load(reportContentFile, LoadOptions.PreserveWhitespace);
                cachedDocuments[reportContentFile] = xdocument;
            }
            else
                xdocument = cachedDocuments[reportContentFile];
            if (!cachedDocuments.ContainsKey(reportStylesFile) ||
                cachedDocuments[reportStylesFile] == null)
            {
                xstyles = XDocument.Load(reportStylesFile, LoadOptions.PreserveWhitespace);
                cachedDocuments[reportStylesFile] = xdocument;
            }
            else
                xstyles = cachedDocuments[reportStylesFile];
            OOStyleSheet style_sheet = new OOStyleSheet(xdocument);
            List<XElement> xelements = ReportHelper.FindElementsByTag(xdocument.Root, "p").Union(
                                       ReportHelper.FindElementsByTag(xstyles.Root, "p")).ToList();
            foreach (XElement xelement in xelements)
            {
                prepair_paragraph(xelement, style_sheet);
                parse_style_tags(xelement, style_sheet);
                List<XElement> new_xelements = parse_br_tags(xelement);
                foreach (XElement new_xelement in new_xelements)
                    parse_sbr_tags(new_xelement);
            }
            xdocument.Save(Path.Combine(reportUnzipPath, "content.xml"), SaveOptions.DisableFormatting);
            xstyles.Save(Path.Combine(reportUnzipPath, "styles.xml"), SaveOptions.DisableFormatting);
        }
    }
}
