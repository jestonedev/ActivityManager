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
            string root_string = document.Root.ToString(SaveOptions.DisableFormatting);
            root_string = root_string.Replace(reportValue.Pattern, reportValue.Value);
            document.Root.Remove();
            document.Add(XElement.Parse(root_string, LoadOptions.PreserveWhitespace));
        }

        /// <summary>
        /// Метод, заменяющий табличный шаблон отчета
        /// </summary>
        /// <param name="reportValue">Табличная переменная отчета</param>
        /// <param name="document">Отчет</param>
        protected virtual void WriteTable(TableReportValue reportValue, XDocument document)
        {
            IEnumerable<XElement> elements = ReportHelper.find_xelements(document.Root, reportValue.XmlContractor);
            List<string> pattern = new List<string>();
            foreach (string column in reportValue.Table.Columns)
                pattern.Add("$" + column + "$");
            string reg_match_pattern = ReportHelper.get_table_pattern_regex(pattern);
            foreach (XElement element in elements)
            {
                string element_value = element.ToString(SaveOptions.DisableFormatting);
                if (Regex.IsMatch(element_value, reg_match_pattern))
                {
                    List<XElement> new_elements = new List<XElement>();
                    foreach (ReportRow row in reportValue.Table)
                    {
                        string result_row = element_value;
                        for (int i = 0; i < pattern.Count; i++)
                            result_row = result_row.Replace(pattern[i], row[i].Value);
                        XElement new_element = XElement.Parse(result_row, LoadOptions.PreserveWhitespace);
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
        }

        /// <summary>
        /// Метод, производящий замену специальных тэгов в отчете
        /// </summary>
        /// <param name="reportUnzipPath">Путь до файлов отчета во временной директории</param>
        public virtual void SpecialTagEditing(string reportUnzipPath) { }
    }
}
