using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using System.Collections.ObjectModel;
using System.Globalization;

namespace ReportModule
{
    /// <summary>
    /// Класс работы с shared-строками отчета Excel
    /// </summary>
    public class SharedExcelStrings
    {
        /// <summary>
        /// Число shared-строк
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Число уникальных shared-строк
        /// </summary>
        public int UniqueCount { get; set; }


        private Collection<XElement> sharedStrings = new Collection<XElement>();
        /// <summary>
        /// Список shared-строк
        /// </summary>
        public Collection<XElement> SharedStrings { get { return sharedStrings; } }

        /// <summary>
        /// Конструктор класса SharedXLSXStrings
        /// </summary>
        /// <param name="fileName">Путь до файла с shared-строками</param>
        public SharedExcelStrings(string fileName)
        {
            XDocument sharedStrings_document = XDocument.Load(fileName);

            sharedStrings.Clear();
            foreach (XElement element in sharedStrings_document.Root.Elements().ToList<XElement>())
                sharedStrings.Add(element);

            Count = Int32.Parse(sharedStrings_document.Root.Attribute("count").Value, CultureInfo.CurrentCulture);
            UniqueCount = Int32.Parse(sharedStrings_document.Root.Attribute("uniqueCount").Value, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Добавить shared-строку
        /// </summary>
        /// <param name="value">Shared-строка</param>
        /// <returns>возвращает индекс добавленной строки</returns>
        public int Add(string value)
        {
            Count++;
            bool is_containt = false;
            XElement ss = XElement.Parse(value, LoadOptions.PreserveWhitespace);
            for (int i = 0; i < SharedStrings.Count; i++)
                if (SharedStrings[i] == ss)
                {
                    is_containt = true;
                    return i;
                }
            if (!is_containt)
            {
                SharedStrings.Add(ss);
                UniqueCount++;
                return SharedStrings.Count - 1;
            }
            throw new ReportException("Неизвестная ошибка при добавлении строки в sharedStrings.xml");
        }

        /// <summary>
        /// Сохранить список shared-строк в файл
        /// </summary>
        /// <param name="fileName">Имя файла</param>
        public void Save(string fileName)
        {
            XDocument sharedStringsDocument = XDocument.Load(fileName);
            sharedStringsDocument.Root.RemoveNodes();
            sharedStringsDocument.Root.SetAttributeValue("count", Count);
            sharedStringsDocument.Root.SetAttributeValue("uniqueCount", UniqueCount);
            foreach (XElement element in SharedStrings)
                sharedStringsDocument.Root.Add(element);
            sharedStringsDocument.Save(fileName);
        }
    }

}
