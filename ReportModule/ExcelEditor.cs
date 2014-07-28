using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using ExtendedTypes;
using System.Collections.ObjectModel;

namespace ReportModule
{
    /// <summary>
    /// Редактор отчетов Excel
    /// </summary>
    public class ExcelEditor: ReportEditor
    {
        private SharedExcelStrings shared_strings;

        private Dictionary<string, string> xml_contractors = new Dictionary<string, string>() {
        {"row","row"},
        {"cell","c"}
        };

        /// <summary>
        /// Метод конвертации xml-замыкателя
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
            string xl_path = Path.Combine(reportUnzipPath, "xl");
            shared_strings = new SharedExcelStrings(Path.Combine(xl_path, "sharedStrings.xml"));
            string[] sheets = Directory.GetFiles(Path.Combine(xl_path, "worksheets"), "*.xml");
            List<XDocument> xdoc_sheets = new List<XDocument>();
            foreach (string sheet in sheets)
                xdoc_sheets.Add(XDocument.Load(sheet));
            foreach (ReportValue report_value in values)
            {
                StringReportValue string_report_value = report_value as StringReportValue;
                TableReportValue table_report_value = report_value as TableReportValue;
                foreach (XDocument sheet in xdoc_sheets)
                if (string_report_value != null)    
                    WriteString(string_report_value, sheet);
                else
                if (table_report_value != null)
                    WriteTable(table_report_value, sheet);
            }
            for (int i = 0; i < sheets.Length; i++)
                xdoc_sheets[i].Save(sheets[i]);
            shared_strings.Save(Path.Combine(xl_path, "sharedStrings.xml"));
        }

        /// <summary>
        /// Метод, производящий замену специальных тэгов в отчете
        /// </summary>
        /// <param name="reportUnzipPath">Путь до файлов отчета во временной директории</param>
        public override void SpecialTagEditing(string reportUnzipPath)
        {
            base.SpecialTagEditing(reportUnzipPath);
        }

        /// <summary>
        /// Метод, заменяющий строковый шаблон отчета
        /// </summary>
        /// <param name="reportValue">Строковая переменная отчета</param>
        /// <param name="document">Отчет</param>
        protected override void WriteString(StringReportValue reportValue, XDocument document)
        {
            foreach (XElement element in shared_strings.SharedStrings)
            {
                XElement t_element = element.Elements().First();
                string t_element_str = t_element.ToString(SaveOptions.DisableFormatting);
                if (Regex.IsMatch(t_element_str, Regex.Escape(reportValue.Pattern)))
                {
                    t_element_str = t_element_str.Replace(reportValue.Pattern, reportValue.Value);
                    t_element.ReplaceWith(XElement.Parse(t_element_str, LoadOptions.PreserveWhitespace));
                }
            }
        }

        /// <summary>
        /// Метод, заменяющий табличный шаблон отчета
        /// </summary>
        /// <param name="reportValue">Табличная переменная отчета</param>
        /// <param name="document">Отчет</param>
        protected override void WriteTable(TableReportValue reportValue, XDocument document)
        {
            List<int> excel_templates_ss_indexes = new List<int>();
            foreach (string template in reportValue.Table.Columns)
                excel_templates_ss_indexes.Add(ss_index("$" + template + "$", shared_strings));
            List<string> excel_templates = new List<string>();
            foreach (int ss_index in excel_templates_ss_indexes)
                excel_templates.Add("<v>" + ss_index.ToString() + "</v>");
            string reg_match_pattern = ReportHelper.get_table_pattern_regex(excel_templates);

            List<XElement> xml_contractor_elements = ReportHelper.FindElementsByTag(document.Root, reportValue.XmlContractor);
            int x_increment = 0;        //Инкремент адреса столбца
            int y_increment = 0;        //Инкремент адреса строки

            foreach (XElement element in xml_contractor_elements)
            {
                recalculate_xelement_address(element, x_increment, y_increment);
                string element_value = element.ToString(SaveOptions.DisableFormatting);
                if (Regex.IsMatch(element_value, reg_match_pattern))
                {
                    List<XElement> new_elements = new List<XElement>();
                    foreach (ReportRow row in reportValue.Table)
                    {
                        string result_row = element_value;
                        for (int i = 0; i < reportValue.Table.Columns.Count; i++)
                        {
                            result_row = result_row.Replace(
                                "<v>" + ss_index("$" + reportValue.Table.Columns[i] + "$", shared_strings).ToString() + "</v>",
                                "<v>" + shared_strings.Add(
                                ss_element("$" + reportValue.Table.Columns[i] + "$", shared_strings).ToString(SaveOptions.DisableFormatting).
                                Replace("$" + reportValue.Table.Columns[i] + "$", row[i].Value).ToString()) + "</v>");
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
                    recalculate_merge_cells(document, address, y_increment);
                }
            }
        }

        /// <summary>
        /// Получить элемент в файле sharedStrings.xml Excel-отчета
        /// </summary>
        /// <param name="shared_string">Искомая строка</param>
        /// <param name="shared_strings_list">Класс-обертка sharedStrings.xml</param>
        /// <returns>Найденый элемент. Если ничего не найдено - вызывается исключение</returns>
        private static XElement ss_element(string shared_string, SharedExcelStrings shared_strings_list)
        {
            for (int i = 0; i < shared_strings_list.SharedStrings.Count; i++)
            {
                foreach (XElement r in shared_strings_list.SharedStrings[i].Elements(XName.Get("r", "http://schemas.openxmlformats.org/spreadsheetml/2006/main")))
                {
                    if (r.Elements(XName.Get("t", "http://schemas.openxmlformats.org/spreadsheetml/2006/main")).
                        First().Value == shared_string)
                        return shared_strings_list.SharedStrings[i];
                }
                if (shared_strings_list.SharedStrings[i].Elements(XName.Get("t", "http://schemas.openxmlformats.org/spreadsheetml/2006/main")).
                        First().Value == shared_string)
                    return shared_strings_list.SharedStrings[i];
            }
            ReportException exception = new ReportException("Подставляемая строка \"{0}\" не найдена в файле шаблона");
            exception.Data.Add("{0}", shared_string);
            throw exception;
        }

        /// <summary>
        /// Получить индекс строки в файле sharedStrings.xml Excel-отчета
        /// </summary>
        /// <param name="shared_string">Искомая строка</param>
        /// <param name="shared_strings_list">Класс-обертка sharedStrings.xml</param>
        /// <returns>Индекс найденного элемента. Если ничего не найдено - вызывается исключение</returns>
        private static int ss_index(string shared_string, SharedExcelStrings shared_strings_list)
        {
            for (int i = 0; i < shared_strings_list.SharedStrings.Count; i++)
            {
                foreach (XElement r in shared_strings_list.SharedStrings[i].Elements(XName.Get("r", "http://schemas.openxmlformats.org/spreadsheetml/2006/main")))
                {
                    if (r.Elements(XName.Get("t", "http://schemas.openxmlformats.org/spreadsheetml/2006/main")).
                        First().Value == shared_string)
                        return i;
                }
                if (shared_strings_list.SharedStrings[i].Elements(XName.Get("t", "http://schemas.openxmlformats.org/spreadsheetml/2006/main")).
                        First().Value == shared_string)
                    return i;
            }
            ReportException exception = new ReportException("Подставляемая строка \"{0}\" не найдена в файле шаблона");
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
        private static string ca_increment(int x_increment, int y_increment, string address)
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
        /// <param name="y_increment">Смещение по строкам</param>
        private static void recalculate_merge_cells(XDocument sheet, string address, int y_increment)
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

        private static void recalculate_xelement_address(XElement element, int x_increment, int y_increment)
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
                    ReportException exception = new ReportException("Неподдерживаемый тип элемента {0}");
                    exception.Data.Add("{0}", element.Name.LocalName);
                    throw exception;
                }
        }
    }
}
