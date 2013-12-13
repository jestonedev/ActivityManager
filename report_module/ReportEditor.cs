using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using extended_types;
using System.Text.RegularExpressions;

namespace report_module
{
    public class ReportEditor
    {
        protected virtual List<ReportValue> clousers_convert(Dictionary<string, string> xml_clousers, List<ReportValue> values) {
            List<ReportValue> tmp_values = new List<ReportValue>();
            foreach (ReportValue value in values)
            {
                ReportValue tmp_value = value;
                if (tmp_value is TableReportValue)
                {
                    string xml_clouser = (tmp_value as TableReportValue).xml_clouser.ToLower();
                    if (!xml_clousers.ContainsKey(xml_clouser))
                    {
                        ApplicationException exception = new ApplicationException("XML-замыкатель {0} не поддерживается для данного типа отчета");
                        exception.Data.Add("{0}", xml_clouser);
                        throw exception;
                    }
                    (tmp_value as TableReportValue).xml_clouser = xml_clousers[xml_clouser];
                }
                tmp_values.Add(tmp_value);
            }
            return tmp_values;
        }

        protected virtual void report_editing_content_file(string report_content_file, List<ReportValue> values) {
            XDocument xdocument = XDocument.Load(report_content_file, LoadOptions.PreserveWhitespace);
            XElement root = xdocument.Root;
            foreach (ReportValue report_value in values)
                if (report_value is StringReportValue)
                    WriteString((StringReportValue)report_value, xdocument);
                else
                if (report_value is TableReportValue)
                    WriteTable((TableReportValue)report_value, xdocument);
            xdocument.Save(report_content_file, SaveOptions.DisableFormatting);
        }

        protected virtual void WriteString(StringReportValue report_value, XDocument xdocument)
        {
            string root_string = xdocument.Root.ToString(SaveOptions.DisableFormatting);
            root_string = root_string.Replace(report_value.pattern, report_value.value);
            xdocument.Root.Remove();
            xdocument.Add(XElement.Parse(root_string, LoadOptions.PreserveWhitespace));
        }

        protected virtual void WriteTable(TableReportValue report_value, XDocument xdocument)
        {
            IEnumerable<XElement> elements = ReportHelper.find_xelements(xdocument.Root, report_value.xml_clouser);
            List<string> pattern = new List<string>();
            foreach (string column in report_value.table.Columns)
                pattern.Add("$" + column + "$");
            string reg_match_pattern = ReportHelper.get_table_pattern_regex(pattern);
            foreach (XElement element in elements)
            {
                string element_value = element.ToString(SaveOptions.DisableFormatting);
                if (Regex.IsMatch(element_value, reg_match_pattern))
                {
                    List<XElement> new_elements = new List<XElement>();
                    foreach (ReportRow row in report_value.table)
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

        public virtual List<ReportValue> xml_clousers_convert(List<ReportValue> values)
        {
            return clousers_convert(new Dictionary<string, string>(), values);
        }

        public virtual void report_editing(string report_path, List<ReportValue> values)
        {
            report_editing_content_file(Path.Combine(report_path, "content.xml"), values);
        }

        public virtual void special_tag_editing(string report_unzip_path) { }
    }
}
