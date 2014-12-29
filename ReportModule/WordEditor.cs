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
    internal class WordEditor : MSEditor
    {
        private Dictionary<string, string> xml_contractors = new Dictionary<string, string>() {
        {"table","tbl"},
        {"row","tr"},
        {"cell","tc"},
        {"paragraph","p"}
        };
       

        //Основной namespace w
        string xmlnsMain = "http://schemas.openxmlformats.org/wordprocessingml/2006/main";

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

        private List<XElement> parse_br_tags(XElement xelement, string tag = "$br$", bool clearInd = false)
        {
            List<XElement> new_xelements = new List<XElement>();
            XElement new_xelement = new XElement(xelement);
            new_xelement.RemoveNodes();
            foreach (var child_element in xelement.Elements())
                if (child_element.Name.LocalName == "r")
                {
                    XElement textElement = child_element.Element(XName.Get("t", xmlnsMain));
                    if (textElement == null)
                    {
                        new_xelement.Add(child_element);
                        continue;
                    }
                    string content = textElement.Value;
                    string[] values = content.Split(new string[] { 
                        tag.ToLower(CultureInfo.CurrentCulture), 
                        tag.ToUpper(CultureInfo.CurrentCulture) }, StringSplitOptions.None);
                    if (values.Length == 1)
                    {
                        new_xelement.Add(child_element);
                        continue;
                    }
                    int i = 0;
                    foreach (string value in values)
                    {
                        if (i == 0)
                        {
                            XElement new_element = new XElement(child_element);
                            new_element.Element(XName.Get("t", xmlnsMain)).Value = value;
                            new_xelement.Add(new_element);
                        }
                        else
                        {
                            xelement.AddBeforeSelf(new_xelement);
                            new_xelements.Add(new_xelement);
                            new_xelement = new XElement(xelement);
                            new_xelement.RemoveNodes();
                            if (xelement.Element(XName.Get("pPr", xmlnsMain)) != null)
                                new_xelement.Add(xelement.Element(XName.Get("pPr", xmlnsMain)));
                            if (clearInd)
                            {
                                XElement ind = new_xelement.Element(XName.Get("pPr", xmlnsMain)).Element(XName.Get("ind", xmlnsMain));
                                if (ind != null)
                                    ind.Remove();
                            }
                            XElement new_element = new XElement(child_element);
                            new_element.Element(XName.Get("t", xmlnsMain)).Value = value;
                            new_xelement.Add(new_element);
                        }
                        i++;
                    }
                }
                else
                    new_xelement.Add(child_element);
            xelement.ReplaceWith(new_xelement);
            new_xelements.Add(new_xelement);
            return new_xelements;
        }

        private void parse_sbr_tags(XElement xelement)
        {
            parse_br_tags(xelement, @"$sbr$", true);
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
            string[] files = headerFiles.Union(new List<string>() { reportContentFile }).ToArray();
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
                    prepair_element(xelement);
                    XElement new_xelement = parse_style_tags(xelement, xmlnsMain);
                    List<XElement> new_xelements = parse_br_tags(new_xelement);
                    foreach (XElement element in new_xelements)
                        parse_sbr_tags(element);
                }
                xdocument.Save(file, SaveOptions.DisableFormatting);
            }
        }
    }
}
