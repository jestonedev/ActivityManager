using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using extended_types;

namespace report_module
{
    public class ExcelEditor: ReportEditor
    {
        private SharedXLSXStrings shared_strings;

        private Dictionary<string, string> xml_clousers = new Dictionary<string, string>() {
        {"row","row"},
        {"cell","c"}
        };

        public override List<ReportValue> xml_clousers_convert(List<ReportValue> values)
        {
            return clousers_convert(xml_clousers, values);
        }

        public override void report_editing(string report_unzip_path, List<ReportValue> values)
        {
            string xl_path = Path.Combine(report_unzip_path, "xl");
            shared_strings = new SharedXLSXStrings(Path.Combine(xl_path, "sharedStrings.xml"));
            string[] sheets = Directory.GetFiles(Path.Combine(xl_path, "worksheets"), "*.xml");
            List<XDocument> xdoc_sheets = new List<XDocument>();
            foreach (string sheet in sheets)
                xdoc_sheets.Add(XDocument.Load(sheet));
            foreach (ReportValue report_value in values)
            {
                foreach (XDocument sheet in xdoc_sheets)
                    if (report_value is StringReportValue)
                        WriteString((StringReportValue)report_value, sheet);
                    else
                    if (report_value is TableReportValue)
                        WriteTable((TableReportValue)report_value, sheet);
            }
            for (int i = 0; i < sheets.Length; i++)
                xdoc_sheets[i].Save(sheets[i]);
            shared_strings.Save(Path.Combine(xl_path, "sharedStrings.xml"));
        }

        public override void special_tag_editing(string report_unzip_path)
        {
            base.special_tag_editing(report_unzip_path);
        }

        protected override void WriteString(StringReportValue report_value, XDocument xdocument)
        {
            foreach (XElement element in shared_strings.shared_strings)
            {
                XElement t_element = element.Elements().First();
                string t_element_str = t_element.ToString(SaveOptions.DisableFormatting);
                if (Regex.IsMatch(t_element_str, Regex.Escape(report_value.pattern)))
                {
                    t_element_str = t_element_str.Replace(report_value.pattern, report_value.value);
                    t_element.ReplaceWith(XElement.Parse(t_element_str, LoadOptions.PreserveWhitespace));
                }
            }
        }

        protected override void WriteTable(TableReportValue report_value, XDocument xdocument)
        {
            List<int> excel_templates_ss_indexes = new List<int>();
            foreach (string template in report_value.table.Columns)
                excel_templates_ss_indexes.Add(ss_index("$" + template + "$", shared_strings));
            List<string> excel_templates = new List<string>();
            foreach (int ss_index in excel_templates_ss_indexes)
                excel_templates.Add("<v>" + ss_index.ToString() + "</v>");
            string reg_match_pattern = ReportHelper.get_table_pattern_regex(excel_templates);

            List<XElement> xml_clouser_elements = ReportHelper.find_xelements(xdocument.Root, report_value.xml_clouser);
            int x_increment = 0;        //Инкремент адреса столбца
            int y_increment = 0;        //Инкремент адреса строки

            foreach (XElement element in xml_clouser_elements)
            {
                recalculate_xelement_address(element, x_increment, y_increment);
                string element_value = element.ToString(SaveOptions.DisableFormatting);
                if (Regex.IsMatch(element_value, reg_match_pattern))
                {
                    List<XElement> new_elements = new List<XElement>();
                    foreach (ReportRow row in report_value.table)
                    {
                        string result_row = element_value;
                        for (int i = 0; i < report_value.table.Columns.Count; i++)
                        {
                            result_row = result_row.Replace(
                                "<v>" + ss_index("$" + report_value.table.Columns[i] + "$", shared_strings).ToString() + "</v>",
                                "<v>" + shared_strings.Add(
                                ss_element("$" + report_value.table.Columns[i] + "$", shared_strings).ToString(SaveOptions.DisableFormatting).
                                Replace("$" + report_value.table.Columns[i] + "$", row[i].Value).ToString()) + "</v>");
                        }
                        XElement new_element = XElement.Parse(result_row, LoadOptions.PreserveWhitespace);
                        new_elements.Add(new_element);
                    }
                    foreach (XElement new_element in new_elements)
                    {
                        recalculate_xelement_address(new_element, x_increment, y_increment);
                        element.AddBeforeSelf(new_element);
                        if ((element.Name.LocalName == "row") && (new_elements.Count - 1 > y_increment))
                            y_increment++;
                        else
                            if ((element.Name.LocalName == "c") && (new_elements.Count - 1 > x_increment))
                                x_increment++;
                    }
                    element.Remove();
                    string address = element.Elements().First().Attribute("r").Value;
                    recalculate_merge_cells(xdocument, address, x_increment, y_increment);
                }
            }
        }

        /// <summary>
        /// Получить элемент в файле sharedStrings.xml Excel-отчета
        /// </summary>
        /// <param name="shared_string">Искомая строка</param>
        /// <param name="shared_strings">Класс-обертка sharedStrings.xml</param>
        /// <returns>Найденый элемент. Если ничего не найдено - вызывается исключение</returns>
        private XElement ss_element(string shared_string, SharedXLSXStrings shared_strings)
        {
            for (int i = 0; i < shared_strings.shared_strings.Count; i++)
            {
                foreach (XElement r in shared_strings.shared_strings[i].Elements(XName.Get("r", "http://schemas.openxmlformats.org/spreadsheetml/2006/main")))
                {
                    if (r.Elements(XName.Get("t", "http://schemas.openxmlformats.org/spreadsheetml/2006/main")).
                        First().Value == shared_string)
                        return shared_strings.shared_strings[i];
                }
                if (shared_strings.shared_strings[i].Elements(XName.Get("t", "http://schemas.openxmlformats.org/spreadsheetml/2006/main")).
                        First().Value == shared_string)
                    return shared_strings.shared_strings[i];
            }
            ApplicationException exception = new ApplicationException("Подставляемая строка \"{0}\" не найдена в файле шаблона");
            exception.Data.Add("{0}", shared_string);
            throw exception;
        }

        /// <summary>
        /// Получить индекс строки в файле sharedStrings.xml Excel-отчета
        /// </summary>
        /// <param name="shared_string">Искомая строка</param>
        /// <param name="shared_strings">Класс-обертка sharedStrings.xml</param>
        /// <returns>Индекс найденного элемента. Если ничего не найдено - вызывается исключение</returns>
        private int ss_index(string shared_string, SharedXLSXStrings shared_strings)
        {
            for (int i = 0; i < shared_strings.shared_strings.Count; i++)
            {
                foreach (XElement r in shared_strings.shared_strings[i].Elements(XName.Get("r", "http://schemas.openxmlformats.org/spreadsheetml/2006/main")))
                {
                    if (r.Elements(XName.Get("t", "http://schemas.openxmlformats.org/spreadsheetml/2006/main")).
                        First().Value == shared_string)
                        return i;
                }
                if (shared_strings.shared_strings[i].Elements(XName.Get("t", "http://schemas.openxmlformats.org/spreadsheetml/2006/main")).
                        First().Value == shared_string)
                    return i;
            }
            ApplicationException exception = new ApplicationException("Подставляемая строка \"{0}\" не найдена в файле шаблона");
            exception.Data.Add("{0}", shared_string);
            throw exception;
        }

        /// <summary>
        /// Инкремент адреса ячейки таблицы Excel
        /// </summary>
        /// <param name="x_increment">Инкремент по столбцам</param>
        /// <param name="y_increment">Инкремент по строкам</param>
        /// <param name="address">Исходный адрес ячейки</param>
        /// <returns>Полученный адресс ячейки</returns>
        private string ca_increment(int x_increment, int y_increment, string address)
        {
            Match match_row = Regex.Match(address, "[0-9]+$");
            string row_address = match_row.Value;
            row_address = (Int32.Parse(row_address) + y_increment).ToString();
            Match match_col = Regex.Match(address, "^[A-Z]+");
            string col_address = match_col.Value;
            int base_index = 26;
            int value = 0;
            for (int i = col_address.Length - 1; i >= 0; i--)
            {
                int curr_base_index = (int)Math.Pow(base_index, i);
                value += ((int)col_address[i] - 64) * curr_base_index;
            }
            value += x_increment;
            col_address = "";
            do
            {
                char index_value = (char)((value % base_index) + 64);
                value = (value - index_value) / base_index;
                col_address = index_value + col_address;
            }
            while ((value / base_index) > base_index);
            return col_address + row_address;
        }

        /// <summary>
        /// Пересчитать позиции объединенных ячеек согласно смещениям относительно начальной ячейки
        /// </summary>
        /// <param name="sheet">Excel-лист</param>
        /// <param name="address">Адрес ячейки, относительно которой производится смещение</param>
        /// <param name="x_increment">Смещение по столбцам</param>
        /// <param name="y_increment">Смещение по строкам</param>
        private void recalculate_merge_cells(XDocument sheet, string address, int x_increment, int y_increment)
        {
            XElement merge_cells_element = null;
            foreach (XElement element in sheet.Root.Elements())
            {
                if (element.Name.LocalName == "mergeCells")
                    merge_cells_element = element;
            }
            if (merge_cells_element == null)
                return;
            List<XElement> merge_cells = merge_cells_element.Elements().ToList<XElement>();
            Match match_row = Regex.Match(address, "[0-9]+$");
            string row_address = match_row.Value;
            Match match_col = Regex.Match(address, "^[A-Z]+");
            string col_address = match_col.Value;
            foreach (XElement mergeCell in merge_cells)
            {
                string range = mergeCell.Attribute("ref").Value;
                string[] range_parts = range.Split(new char[] { ':' });
                //Получаем адреса начальной ячейки диапазона
                Match match_merge_start_row = Regex.Match(range_parts[0], "[0-9]+$");
                string merge_start_row_address = match_merge_start_row.Value;
                Match match_merge_start_col = Regex.Match(range_parts[0], "^[A-Z]+");
                string merge_start_col_address = match_merge_start_col.Value;
                //Получаем адреса конечной ячейки диапазона
                Match match_merge_end_row = Regex.Match(range_parts[1], "[0-9]+$");
                string merge_end_row_address = match_merge_end_row.Value;
                Match match_merge_end_col = Regex.Match(range_parts[1], "^[A-Z]+");
                string merge_end_col_address = match_merge_end_col.Value;

                if (Int32.Parse(merge_start_row_address) > Int32.Parse(row_address))
                {
                    merge_start_row_address = (Int32.Parse(merge_start_row_address) + y_increment).ToString();
                }
                if (Int32.Parse(merge_end_row_address) > Int32.Parse(row_address))
                {
                    merge_end_row_address = (Int32.Parse(merge_end_row_address) + y_increment).ToString();
                }
                mergeCell.Attribute("ref").SetValue(merge_start_col_address + merge_start_row_address + ":" + merge_end_col_address + merge_end_row_address);
            }
        }

        private void recalculate_xelement_address(XElement element, int x_increment, int y_increment)
        {
            if (element.Name.LocalName == "row")
            {
                element.SetAttributeValue("r", Int32.Parse(element.Attribute("r").Value) + y_increment);
                List<XElement> c_elements = element.Elements().ToList<XElement>();
                foreach (XElement c_element in c_elements)
                {
                    string cell_address = c_element.Attribute("r").Value;
                    c_element.SetAttributeValue("r", ca_increment(x_increment, y_increment, cell_address));
                }
            }
            else
                if (element.Name.LocalName == "c")
                {
                    string cell_address = element.Attribute("r").Value;
                    element.SetAttributeValue("r", ca_increment(x_increment, y_increment, cell_address));
                }
                else
                {
                    ApplicationException exception = new ApplicationException("Неподдерживаемый тип элемента {0}");
                    exception.Data.Add("{0}", element.Name.LocalName);
                    throw exception;
                }
        }
    }
}
