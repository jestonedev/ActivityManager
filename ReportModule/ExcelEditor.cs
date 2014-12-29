using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using ExtendedTypes;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Xml.XPath;

namespace ReportModule
{
    /// <summary>
    /// Редактор отчетов Excel
    /// </summary>
    internal class ExcelEditor : MSEditor
    {

        private Dictionary<string, string> xml_contractors = new Dictionary<string, string>() {
        {"table","worksheet"},
        {"row","row"},
        {"cell","c"}
        };

        //xml-namespaces
        private static string xmlnsMain = "http://schemas.openxmlformats.org/spreadsheetml/2006/main";
        private static string xmlnsRel = "http://schemas.openxmlformats.org/package/2006/relationships";
        private static string xmlnsRelOD = "http://schemas.openxmlformats.org/officeDocument/2006/relationships";
        private static string xmlnsContentTypes = "http://schemas.openxmlformats.org/package/2006/content-types";

        private SharedXLSXStrings shared_strings;
        private XDocument workbook;
        private XDocument workbookRel;
        private XDocument contentTypes;
        private string report_unzip_path;
        private int next_sheet_number = 1;
        private int next_rId = 1;

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
            if (values == null)
                throw new ReportException("Не задана ссылка на список переменных");
            this.report_unzip_path = reportUnzipPath;
            string xl_path = Path.Combine(reportUnzipPath, "xl");
            string _rels_path = Path.Combine(xl_path, "_rels");
            shared_strings = new SharedXLSXStrings(Path.Combine(xl_path, "sharedStrings.xml"));
            string[] sheets = Directory.GetFiles(Path.Combine(xl_path, "worksheets"), "*.xml");
            string contentTypesFile = Path.Combine(reportUnzipPath, "[Content_Types].xml");
            string workbookFile = Path.Combine(xl_path, "workbook.xml");
            string workbookRelFile = Path.Combine(_rels_path, "workbook.xml.rels");
            Console.WriteLine("Загружаем файлы отчета");
            Dictionary<string, XDocument> xdocuments = new Dictionary<string, XDocument>();
            contentTypes = XDocument.Load(contentTypesFile, LoadOptions.PreserveWhitespace);
            workbook = XDocument.Load(workbookFile, LoadOptions.PreserveWhitespace);
            workbookRel = XDocument.Load(workbookRelFile, LoadOptions.PreserveWhitespace);
            xdocuments.Add(contentTypesFile, contentTypes);
            xdocuments.Add(workbookFile, workbook);
            xdocuments.Add(workbookRelFile, workbookRel);
            foreach (string sheet in sheets)
                 xdocuments.Add(sheet, XDocument.Load(sheet, LoadOptions.PreserveWhitespace));
            Console.WriteLine("Заполняем файлы отчета данными");
            foreach (ReportValue report_value in values)
            {
                StringReportValue string_report_value = report_value as StringReportValue;
                TableReportValue table_report_value = report_value as TableReportValue;
                Dictionary<string, XDocument> new_xdocuments = new Dictionary<string, XDocument>();
                Dictionary<string, XDocument> remove_xdocuments = new Dictionary<string, XDocument>();
                foreach (var xdocument in xdocuments)
                {
                    if (string_report_value != null)
                        WriteString(string_report_value, xdocument.Value);
                    else
                        if (table_report_value != null)
                        {
                            Dictionary<string, XDocument> result_new_xdocuments = WriteTable(table_report_value, xdocument);
                            if (result_new_xdocuments.Count > 0)
                            {
                                remove_xdocuments.Add(xdocument.Key, xdocument.Value);
                                new_xdocuments = new_xdocuments.Union(result_new_xdocuments).ToDictionary(s => s.Key, s => s.Value);
                            }
                        }
                }
                foreach (var xdocument in remove_xdocuments)
                    xdocuments.Remove(xdocument.Key);
                xdocuments = xdocuments.Union(new_xdocuments).ToDictionary(s => s.Key, s => s.Value);
            }
            Console.WriteLine("Сохраняем файлы отчета во временную директорию");
            foreach (var xdocument in xdocuments)
                xdocument.Value.Save(xdocument.Key);
            shared_strings.Save(Path.Combine(xl_path, "sharedStrings.xml"));
        }

        /// <summary>
        /// Метод, заменяющий строковый шаблон отчета
        /// </summary>
        /// <param name="reportValue">Строковая переменная отчета</param>
        /// <param name="document">Отчет</param>
        protected override void WriteString(StringReportValue reportValue, XDocument document)
        {
            if (reportValue == null)
                throw new ReportException("Не задана ссылка на строковую переменную отчета");
            if (document == null)
                throw new ReportException("Не задана ссылка на документ шаблона");
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
            WritePatternAttributes(document.Root, reportValue.Pattern, reportValue.Value);
        }

        /// <summary>
        /// Метод, заменяющий табличный шаблон отчета
        /// </summary>
        /// <param name="reportValue">Табличная переменная отчета</param>
        /// <param name="document">Отчет</param>
        protected Dictionary<string, XDocument> WriteTable(TableReportValue reportValue, KeyValuePair<string, XDocument> document)
        {
            if (reportValue == null)
                throw new ReportException("Не задана ссылка на табличную переменную отчета");
            if (document.Value == null)
                throw new ReportException("Не задана ссылка на документ шаблона");
            Dictionary<string, XDocument> new_xdocuments = new Dictionary<string, XDocument>();
            List<int> excel_templates_ss_indexes = new List<int>();
            foreach (string template in reportValue.Table.Columns)
                excel_templates_ss_indexes.Add(ss_index("$" + template + "$", shared_strings));
            List<string> excel_templates = new List<string>();
            foreach (int ss_index in excel_templates_ss_indexes)
                excel_templates.Add("<v>" + ss_index.ToString(CultureInfo.CurrentCulture) + "</v>");
            string reg_match_pattern = ReportHelper.GetTablePatternRegex(excel_templates);
            List<XElement> xml_contractor_elements = ReportHelper.FindElementsByTag(document.Value.Root, reportValue.XmlContractor);
            int x_increment = 0;        //Инкремент адреса столбца
            int y_increment = 0;        //Инкремент адреса строки
            foreach (XElement element in xml_contractor_elements)
            {
                if (reportValue.XmlContractor == "row" || reportValue.XmlContractor == "c")
                    recalculate_xelement_address(element, x_increment, y_increment);
                if (Regex.IsMatch(element.ToString(SaveOptions.DisableFormatting), reg_match_pattern))
                {
                    List<XElement> new_elements = CreateElementsByTemplate(reportValue, element);
                    if (reportValue.XmlContractor == "row" || reportValue.XmlContractor == "c")
                    {
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
                        string address = "";
                        if (element.Name.LocalName == "row")
                            address = element.Elements().First().Attribute("r").Value;
                        else
                            if (element.Name.LocalName == "c")
                                address = element.Attribute("r").Value;
                        recalculate_merge_cells(document.Value, address, y_increment);
                    } else
                    if (reportValue.XmlContractor == "worksheet")
                        new_xdocuments = new_xdocuments.Union(
                            CreateSheetsByElements(document.Key, new_elements, reportValue.Table)).ToDictionary(s => s.Key, s => s.Value);
                }
            }
            return new_xdocuments;
        }

        /// <summary>
        /// Метод создает листы Excel по шаблонному документу и перечню элементов. Метаданные шаблонного листа удаляются из документа.
        /// </summary>
        /// <param name="tempSheetName">Полный путь до файла шаблона</param>
        /// <param name="elements">Перечень элементов worksheet, на базе которых будут созданы листы</param>
        /// <param name="table">Таблица с параметрами. Используется для задания имен листов</param>
        /// <returns>Возвращает коллекцию новых листов (исключая шаблонный)</returns>
        private Dictionary<string, XDocument> CreateSheetsByElements(string tempSheetName, List<XElement> elements, ReportTable table)
        {
            Dictionary<string, XDocument> new_xdocuments = new Dictionary<string, XDocument>();
            string worksheetsDir = Path.Combine(report_unzip_path, "xl" + Path.DirectorySeparatorChar + "worksheets");
            string xlDir = Path.Combine(report_unzip_path, "xl");
            int row_index = 0;
            string docRId = null;
            foreach (XElement new_element in elements)
            {
                //Создаем новый лист (копию текущего) и заменяем в нем шаблоны
                while (File.Exists(Path.Combine(worksheetsDir,
                    "sheet" + next_sheet_number.ToString(CultureInfo.CurrentCulture) + ".xml")))
                    next_sheet_number++;
                string newFileName = Path.Combine(worksheetsDir,
                    "sheet" + next_sheet_number.ToString(CultureInfo.CurrentCulture) + ".xml");
                File.Copy(tempSheetName, newFileName);
                XDocument xsheet = XDocument.Load(newFileName);
                xsheet.Root.ReplaceWith(new_element);
                new_xdocuments.Add(newFileName, xsheet);
                //Вычисляем новое значение rId и rId текущего листа
                string newRId = "rId" + next_rId.ToString(CultureInfo.CurrentCulture);
                while (true)
                {
                    bool founded = false;
                    foreach (XElement relationship in workbookRel.Root.Elements(XName.Get("Relationship", xmlnsRel)))
                    {
                        string currentRId = relationship.Attribute("Id").Value;
                        string currentfileName = Path.Combine(xlDir, relationship.Attribute("Target").Value.Replace("/","\\"));
                        if (currentfileName == tempSheetName)
                            docRId = currentRId;
                        if (currentRId == newRId)
                        {
                            founded = true;
                            next_rId++;
                            newRId = "rId" + next_rId.ToString(CultureInfo.CurrentCulture);
                            break;
                        }
                    }
                    if (!founded)
                        break;
                }
                //добавить в workbook.xml.rels копию записи с новым rId
                workbookRel.Root.Add(new XElement(XName.Get("Relationship", xmlnsRel),
                    new XAttribute("Target", Path.Combine("worksheets", "sheet" + 
                        next_sheet_number.ToString(CultureInfo.CurrentCulture) + ".xml").Replace("\\", "/")),
                    new XAttribute("Type", "http://schemas.openxmlformats.org/officeDocument/2006/relationships/worksheet"),
                    new XAttribute("Id", newRId)));
                //добавить в workbook.xml копию записи с новым r:id
                foreach (XElement sheet in workbook.Root.Element(XName.Get("sheets", xmlnsMain)).Elements(XName.Get("sheet", xmlnsMain)))
                    if (sheet.Attribute(XName.Get("id", xmlnsRelOD)).Value == docRId)
                    {
                        XElement new_sheet = new XElement(sheet);
                        new_sheet.Attribute(XName.Get("id", xmlnsRelOD)).Value = newRId;
                        XAttribute sheet_name_attr = new_sheet.Attribute("name");
                        foreach(string column_name in table.Columns)
                            if (Regex.IsMatch(sheet_name_attr.Value, Regex.Escape("$"+column_name+"$")))
                                sheet_name_attr.Value = Regex.Replace(sheet_name_attr.Value, Regex.Escape("$" + column_name + "$"),
                                    table[row_index][column_name].Value);
                        string sheet_name = sheet_name_attr.Value;
                        bool founded = false;
                        foreach (XElement nsheet in workbook.Root.Element(XName.Get("sheets", xmlnsMain)).Elements(XName.Get("sheet", xmlnsMain)))
                            if (nsheet.Attribute("name").Value == sheet_name)
                            {
                                founded = true;
                                break;
                            }
                        if (founded)
                            new_sheet.Attribute("name").Value = "Лист" + next_sheet_number.ToString(CultureInfo.CurrentCulture);
                        new_sheet.Attribute("sheetId").Value = next_sheet_number.ToString(CultureInfo.CurrentCulture);
                        sheet.AddAfterSelf(new_sheet);
                    }
                //добавить в [Content_Types] копию записи с новым PartName
                contentTypes.Root.Add(new XElement(XName.Get("Override", xmlnsContentTypes),
                    new XAttribute("ContentType", "application/vnd.openxmlformats-officedocument.spreadsheetml.worksheet+xml"),
                    new XAttribute("PartName", "/xl/worksheets/sheet" + next_sheet_number.ToString(CultureInfo.CurrentCulture) + ".xml")));
                row_index++;
            }
            //удаляем шаблон
            File.Delete(tempSheetName);
            foreach (XElement element in contentTypes.Root.Elements(XName.Get("Override", xmlnsContentTypes)))
                if (Path.Combine(report_unzip_path, element.Attribute("PartName").Value.Replace("/", "\\")) == tempSheetName)
                    element.Remove();
            foreach (XElement element in workbookRel.Root.Elements(XName.Get("Relationship", xmlnsRel)))
                if (element.Attribute("Id").Value == docRId)
                    element.Remove();
            foreach (XElement element in workbook.Root.Elements(XName.Get("sheets", xmlnsMain)).Elements(XName.Get("sheet", xmlnsMain)))
                if (element.Attribute(XName.Get("id", xmlnsRelOD)).Value == docRId)
                    element.Remove();
            return new_xdocuments;
        }

        private List<XElement> CreateElementsByTemplate(TableReportValue reportValue, XElement templateElement)
        {
            List<XElement> new_elements = new List<XElement>();
            string templateString = templateElement.ToString(SaveOptions.DisableFormatting);
            int count = 0;
            foreach (ReportRow row in reportValue.Table)
            {
                count++;
                if (count % 500 == 0)
                    Console.WriteLine(String.Format(CultureInfo.CurrentCulture, "Заполнено {0} из {1} строк", count, reportValue.Table.Count));
                string result_row = templateString;
                for (int i = 0; i < reportValue.Table.Columns.Count; i++)
                {
                    result_row = result_row.Replace(
                        "<v>" + ss_index("$" + reportValue.Table.Columns[i] + "$", shared_strings).ToString(CultureInfo.CurrentCulture) + "</v>",
                        "<v>" + shared_strings.Add(
                        ss_element("$" + reportValue.Table.Columns[i] + "$", shared_strings).ToString(SaveOptions.DisableFormatting).
                        Replace("$" + reportValue.Table.Columns[i] + "$", row[i].Value).ToString()) + "</v>");
                }
                XElement new_element = XElement.Parse(result_row, LoadOptions.PreserveWhitespace);
                new_elements.Add(new_element);
            }
            return new_elements;
        }

        /// <summary>
        /// Получить элемент в файле sharedStrings.xml Excel-отчета
        /// </summary>
        /// <param name="shared_string">Искомая строка</param>
        /// <param name="shared_strings_list">Класс-обертка sharedStrings.xml</param>
        /// <returns>Найденый элемент. Если ничего не найдено - вызывается исключение</returns>
        private static XElement ss_element(string shared_string, SharedXLSXStrings shared_strings_list)
        {
            for (int i = 0; i < shared_strings_list.SharedStrings.Count; i++)
            {
                foreach (XElement r in shared_strings_list.SharedStrings[i].Elements(XName.Get("r", xmlnsMain)))
                {
                    if (r.Elements(XName.Get("t", xmlnsMain)).
                        First().Value == shared_string)
                        return shared_strings_list.SharedStrings[i];
                }
                if (shared_strings_list.SharedStrings[i].Elements(XName.Get("t", xmlnsMain)).
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
        private static int ss_index(string shared_string, SharedXLSXStrings shared_strings_list)
        {
            for (int i = 0; i < shared_strings_list.SharedStrings.Count; i++)
            {
                foreach (XElement r in shared_strings_list.SharedStrings[i].Elements(XName.Get("r", xmlnsMain)))
                {
                    if (r.Elements(XName.Get("t", xmlnsMain)).
                        First().Value == shared_string)
                        return i;
                }
                if (shared_strings_list.SharedStrings[i].Elements(XName.Get("t", xmlnsMain)).
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
            row_address = (Int32.Parse(row_address, CultureInfo.CurrentCulture) + y_increment).ToString(CultureInfo.CurrentCulture);
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

                if (Int32.Parse(merge_start_row_address, CultureInfo.CurrentCulture) > Int32.Parse(row_address, CultureInfo.CurrentCulture))
                {
                    merge_start_row_address = (Int32.Parse(merge_start_row_address, CultureInfo.CurrentCulture) + y_increment).ToString(CultureInfo.CurrentCulture);
                }
                if (Int32.Parse(merge_end_row_address, CultureInfo.CurrentCulture) > Int32.Parse(row_address, CultureInfo.CurrentCulture))
                {
                    merge_end_row_address = (Int32.Parse(merge_end_row_address, CultureInfo.CurrentCulture) + y_increment).ToString(CultureInfo.CurrentCulture);
                }
                mergeCell.Attribute("ref").SetValue(merge_start_col_address + merge_start_row_address + ":" + merge_end_col_address + merge_end_row_address);
            }
        }

        private static void recalculate_xelement_address(XElement element, int x_increment, int y_increment)
        {
            if (element.Name.LocalName == "row")
            {
                element.SetAttributeValue("r", Int32.Parse(element.Attribute("r").Value, CultureInfo.CurrentCulture) + y_increment);
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

        protected override void prepair_element(XElement xelement)
        {
            if (xelement == null)
                throw new ReportException("Не задана ссылка на элемент документ шаблона");
            foreach (XElement child in xelement.Elements())
                if (child.Name.LocalName == "t")
                    child.ReplaceWith(new XElement(XName.Get("r", xmlnsMain), new XElement(XName.Get("rPr", xmlnsMain)), child));
            base.prepair_element(xelement);
        }

        private static void parse_br_tags(XElement xelement)
        {
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
                    textElement.Value = textElement.Value.Replace("$br$", "\n\r").Replace("$BR$".ToUpper(CultureInfo.CurrentCulture), "\n\r")
                        .Replace("$sbr$", "\n\r").Replace("$SBR$".ToUpper(CultureInfo.CurrentCulture), "\n\r");
                    new_xelement.Add(child_element);
                }
                else
                    new_xelement.Add(child_element);
            xelement.ReplaceWith(new_xelement);
        }

        /// <summary>
        /// Метод, производящий замену специальных тэгов в отчете
        /// </summary>
        /// <param name="reportUnzipPath">Путь до файлов отчета во временной директории</param>
        public override void SpecialTagEditing(string reportUnzipPath)
        {
            string file = Path.Combine(reportUnzipPath, "xl" + Path.DirectorySeparatorChar + "sharedStrings.xml");
            XDocument xdocument = shared_strings.sharedStringsDocument;
            List<XElement> xelements = ReportHelper.FindElementsByTag(xdocument.Root, "si");
            foreach (XElement xelement in xelements)
            {
                prepair_element(xelement);
                XElement new_xelement = parse_style_tags(xelement, xmlnsMain);
                parse_br_tags(new_xelement);
            }
            xdocument.Save(file, SaveOptions.DisableFormatting);
        }
    
    }
}
