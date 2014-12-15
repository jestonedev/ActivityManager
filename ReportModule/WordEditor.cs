using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections.ObjectModel;

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
            string[] files = Directory.GetFiles(Path.Combine(reportUnzipPath, "word"), "header*.xml");
            foreach (string file in files)
                ReportEditingContentFile(file, values);
        }

        /// <summary>
        /// Метод, производящий замену специальных тэгов в отчете
        /// </summary>
        /// <param name="reportUnzipPath">Путь до файлов отчета во временной директории</param>
        public override void SpecialTagEditing(string reportUnzipPath)
        {
            base.SpecialTagEditing(reportUnzipPath);
        }
    }
}
