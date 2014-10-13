using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using ExtendedTypes;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;

namespace ReportModule
{
    /// <summary>
    /// Базовый класс редактора отчета
    /// </summary>
    public class ReportEditor
    {
        /// <summary>
        /// Метод конвертации xml-замыкателей
        /// </summary>
        /// <param name="xmlContractors">Ассоциативный словарь унифицированных и неунифицированных замыкателей</param>
        /// <param name="values">Список переменных отчета</param>
        /// <returns>Возвращает список переменных отчета с неунифицированными замыкателями</returns>
        protected virtual Collection<ReportValue> ContractorsConvert(Dictionary<string, string> xmlContractors, Collection<ReportValue> values)
        {
            Collection<ReportValue> tmp_values = new Collection<ReportValue>();
            foreach (ReportValue value in values)
            {
                TableReportValue table_report_value = value as TableReportValue;
                if (table_report_value != null)
                {
                    string xml_contractor = table_report_value.XmlContractor.ToLower();
                    if (!xmlContractors.ContainsKey(xml_contractor))
                    {
                        ReportException exception = new ReportException("XML-замыкатель {0} не поддерживается для данного типа отчета");
                        exception.Data.Add("{0}", xml_contractor);
                        throw exception;
                    }
                    table_report_value.XmlContractor = xmlContractors[xml_contractor];
                }
                tmp_values.Add(value);
            }
            return tmp_values;
        }

        /// <summary>
        /// Метод, заполняющий файл отчета контентом
        /// </summary>
        /// <param name="reportContentFile">Основной файл отчета</param>
        /// <param name="values">Переменные отчета</param>
        protected virtual void ReportEditingContentFile(string reportContentFile, Collection<ReportValue> values)
        {
            XDocument xdocument = XDocument.Load(reportContentFile, LoadOptions.PreserveWhitespace);
            XElement root = xdocument.Root;
            foreach (ReportValue report_value in values)
            {
                StringReportValue string_report_value = report_value as StringReportValue;
                TableReportValue table_report_value = report_value as TableReportValue;
                if (string_report_value != null)
                    WriteString(string_report_value, xdocument);
                else
                    if (table_report_value != null)
                        WriteTable(table_report_value, xdocument);
            }
            xdocument.Save(reportContentFile, SaveOptions.DisableFormatting);
        }

        /// <summary>
        /// Метод, заменяющий строковый шаблон отчета
        /// </summary>
        /// <param name="reportValue">Строковая переменная отчета</param>
        /// <param name="document">Отчет</param>
        protected virtual void WriteString(StringReportValue reportValue, XDocument document)
        {
            WritePattern(document.Root, reportValue.Pattern, reportValue.Value);
        }

        /// <summary>
        /// Метод, заменяющий шаблонную строку на значение в указанном элементе
        /// </summary>
        /// <param name="element">Элемент</param>
        /// <param name="pattern">Шаблон</param>
        /// <param name="value">Значение</param>
        protected virtual void WritePattern(XElement element, string pattern, string value)
        {
            List<PatternNodeInfoCollection> ppis = new List<PatternNodeInfoCollection>();
            foreach (XNode node in element.Nodes())
            {
                ppis = ReportHelper.GetNodePatternPartsInfo(node, pattern, ppis);
            }
            for (int i = 0; i < ppis.Count; i++)
            {
                if (ppis[i].Items[ppis[i].Items.Count - 1].IsClosingPatternNode)
                {
                    ReportHelper.ReplaceNodePatternPart(ppis[i].Items[0], value);
                    for (int j = 1; j < ppis[i].Items.Count; j++)
                        ReportHelper.ReplaceNodePatternPart(ppis[i].Items[j], "");
                }
            }
        }

        /// <summary>
        /// Метод, заменяющий табличный шаблон отчета
        /// </summary>
        /// <param name="reportValue">Табличная переменная отчета</param>
        /// <param name="document">Отчет</param>
        protected virtual void WriteTable(TableReportValue reportValue, XDocument document)
        {
            IEnumerable<XElement> elements = ReportHelper.FindElementsByTag(document.Root, reportValue.XmlContractor);
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
                
                if ((patterns.Count/2) <= pattern_match_count)
                {
                    List<XElement> new_elements = new List<XElement>();
                    foreach (ReportRow row in reportValue.Table)
                    {
                        XElement new_element = XElement.Parse(element_value, LoadOptions.PreserveWhitespace);
                        //Заменить шаблоны для каждой колонки
                        for (int i = 0; i < patterns.Count; i++)
                            WritePattern(new_element, patterns[i], row[i].Value);
                        new_elements.Add(new_element);
                    }
                    foreach (XElement new_element in new_elements)
                        element.AddBeforeSelf(new_element);
                    element.Remove();
                }
            }
        }

        /// <summary>
        /// Класс конвертации xml-замыкателя
        /// </summary>
        /// <param name="values">Список переменных, в которых надо найти унифицированные замыкатели и заменить на зависимые от типа отчета</param>
        /// <returns>Возвращает список переменных отчета с зависимыми от типа отчета xml-замыкателями</returns>
        public virtual Collection<ReportValue> XmlContractorsConvert(Collection<ReportValue> values)
        {
            return ContractorsConvert(new Dictionary<string, string>(), values);
        }

        /// <summary>
        /// Метод, производящий замену всех шаблонов отчета на значения переменных отчета
        /// </summary>
        /// <param name="reportUnzipPath">Путь до файла отчета</param>
        /// <param name="values">Список переменных</param>
        public virtual void ReportEditing(string reportUnzipPath, Collection<ReportValue> values)
        {
            ReportEditingContentFile(Path.Combine(reportUnzipPath, "content.xml"), values);
            ReportEditingContentFile(Path.Combine(reportUnzipPath, "styles.xml"), values);
        }

        /// <summary>
        /// Метод, производящий замену специальных тэгов в отчете
        /// </summary>
        /// <param name="reportUnzipPath">Путь до файлов отчета во временной директории</param>
        public virtual void SpecialTagEditing(string reportUnzipPath) { }
    }
}
