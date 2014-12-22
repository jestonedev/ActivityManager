using ExtendedTypes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace XmlDataSource
{
    /// <summary>
    /// Доступный из-вне интерфейс плагина
	/// </summary>
    public interface IPlug
    {
        /// <summary>
        /// Выборка данных из xml-файла
        /// </summary>
        /// <param name="xml">Путь до xml-файла или текст в формате xml</param>
        /// <param name="rowXPath">XPath-выражение, указывающее какие узлы xml-дерева считать строками таблицы</param>
        /// <param name="cellXPathes">XPath-выражения в виде JSON-объекта, указывающие какие узлы xml-дерева считать ячейками, 
        /// где ключ - имя будущего столбца, значение - вычисляемое XPath-выражение относительно XPath-выражения строки
        /// Пример: {"column1":"@attr1","column2":"text()"}
        /// </param>
        /// <param name="table">Результат запроса</param>
        void XmlSelectTable(string xml, string rowXPath, string cellXPathes, out ReportTable table);

        /// <summary>
        /// Выборка скалярного значения из xml-файла
        /// </summary>
        /// <param name="xml">Путь до xml-файла или текст в формате xml</param>
        /// <param name="xpath">XPath-выражение для выборки скалярного значения</param>
        /// <param name="result">Возврат скалярного значения</param>
        void XmlSelectScalar(string xml, string xpath, out Object result);
    }

    /// <summary>
    /// Класс, реализующий интерфейс плагина
    /// </summary>
    public class XmlDataSourcePlug: IPlug
    {
        /// <summary>
        /// Выборка данных из xml-файла
        /// </summary>
        /// <param name="xml">Путь до xml-файла или текст в формате xml</param>
        /// <param name="rowXPath">XPath-выражение, указывающее какие узлы xml-дерева считать строками таблицы</param>
        /// <param name="cellXPathes">XPath-выражения в виде JSON-объекта, указывающие какие узлы xml-дерева считать ячейками, 
        /// где ключ - имя будущего столбца, значение - вычисляемое XPath-выражение относительно XPath-выражения строки
        /// Пример: {"column1":"@attr1","column2":"text()"}
        /// </param>
        /// <param name="table">Результат запроса</param>
        public void XmlSelectTable(string xml, string rowXPath, string cellXPathes, out ReportTable table)
        {
            Dictionary<string, string> cellXPathesDic = null;
            try
            {
                cellXPathesDic = JsonConvert.DeserializeObject<Dictionary<string, string>>(cellXPathes);
            } catch (JsonException)
            {
                throw new XmlDataSourceException("Параметр cellXPathes является невалидным JSON-объектом");
            }
            XDocument document = null;
            try
            {
                if (File.Exists(xml))
                    document = XDocument.Load(xml);
                else
                    document = XDocument.Parse(xml);
            } catch (XmlException)
            {
                throw new XmlDataSourceException("Параметр xml указывает на невалидный xml-документ");
            }
            XPathDocument xpathDoc = new XPathDocument(document.CreateReader());
            XPathNavigator xpathNavigator = xpathDoc.CreateNavigator();
            XPathNodeIterator rowsIterator = null;
            try
            {
                rowsIterator = (XPathNodeIterator)xpathNavigator.Evaluate(rowXPath);
            }
            catch (XPathException)
            {
                XmlDataSourceException exception = new XmlDataSourceException("Передано некорректное XPath-выражение строки \"{0}\"");
                exception.Data.Add("{0}", rowXPath);
                throw exception;
            }
            table = new ReportTable();
            foreach (var cellXPath in cellXPathesDic)
                table.Columns.Add(cellXPath.Key);
            while (rowsIterator.MoveNext())
            {
                ReportRow row = new ReportRow(table);
                foreach(var cellXPath in cellXPathesDic)
                {
                    XPathNodeIterator cellIterator = null;
                    try
                    {
                        cellIterator = (XPathNodeIterator)rowsIterator.Current.Evaluate(cellXPath.Value);
                    }
                    catch (XPathException)
                    {
                        XmlDataSourceException exception = new XmlDataSourceException("Передано некорректное XPath-выражение ячейки \"{0}\"");
                        exception.Data.Add("{0}", cellXPath.Value);
                        throw exception;
                    }
                    string result = "";
                    while (cellIterator.MoveNext())
                    {
                        if (cellIterator.Current.NodeType == XPathNodeType.Element)
                            result += cellIterator.Current.InnerXml;
                        else
                            result += cellIterator.Current.Value;
                    }
                    ReportCell cell = new ReportCell(row, result);
                    row.Add(cell);
                }
                table.Add(row);
            }
        }

        /// <summary>
        /// Выборка скалярного значения из xml-файла
        /// </summary>
        /// <param name="xml">Путь до xml-файла или текст в формате xml</param>
        /// <param name="xpath">XPath-выражение для выборки скалярного значения</param>
        /// <param name="result">Возврат скалярного значения</param>
        public void XmlSelectScalar(string xml, string xpath, out object result)
        {
            XDocument document = null;
            try
            {
                if (File.Exists(xml))
                    document = XDocument.Load(xml);
                else
                    document = XDocument.Parse(xml);
            }
            catch (XmlException)
            {
                throw new XmlDataSourceException("Параметр xml указывает на невалидный xml-документ");
            }
            XPathDocument xpathDoc = new XPathDocument(document.CreateReader());
            XPathNavigator xpathNavigator = xpathDoc.CreateNavigator();
            XPathNodeIterator iterator = null;
            try
            {
                iterator = (XPathNodeIterator)xpathNavigator.Evaluate(xpath);
            } catch (XPathException)
            {
                XmlDataSourceException exception = new XmlDataSourceException("Передано некорректное XPath-выражение \"{0}\"");
                exception.Data.Add("{0}", xpath);
                throw exception;
            }
            result = "";
            while (iterator.MoveNext())
            {
                if (iterator.Current.NodeType == XPathNodeType.Element)
                    result += iterator.Current.InnerXml;
                else
                    result += iterator.Current.Value;
            }
        }
    }

    /// <summary>
    /// Класс исключения модуля CSVModule
    /// </summary>
    [Serializable()]
    public class XmlDataSourceException : Exception
    {
        /// <summary>
        /// Конструктор класса исключения CSVException
        /// </summary>
        public XmlDataSourceException() : base() { }

        /// <summary>
        /// Конструктор класса исключения CSVException
        /// </summary>
        /// <param name="message">Сообщение об ошибке</param>
        public XmlDataSourceException(string message) : base(message) { }

        /// <summary>
        /// Конструктор класса исключения CSVException
        /// </summary>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="innerException">Вложенное исключение</param>
        public XmlDataSourceException(string message, Exception innerException) : base(message, innerException) { }

        /// <summary>
        /// Конструктор класса исключения CSVException
        /// </summary>
        /// <param name="info">Информация сериализации</param>
        /// <param name="context">Контекст потока</param>
        protected XmlDataSourceException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
