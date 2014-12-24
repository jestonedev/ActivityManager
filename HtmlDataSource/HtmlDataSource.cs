using ExtendedTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using HtmlAgilityPack;
using Newtonsoft.Json;

namespace HtmlDataSource
{
    /// <summary>
    /// Доступный из-вне интерфейс плагина
    /// </summary>
    public interface IPlug
    {
        /// <summary>
        /// Выборка данных из html-файла
        /// </summary>
        /// <param name="html">Локальный путь до html-файла или текст в формате html</param>
        /// <param name="rowXPath">XPath-выражение, указывающее какие узлы html-дерева считать строками таблицы</param>
        /// <param name="cellXPathes">XPath-выражения в виде JSON-объекта, указывающие какие узлы html-дерева считать ячейками, 
        /// где ключ - имя будущего столбца, значение - вычисляемое XPath-выражение относительно XPath-выражения строки
        /// Пример: {"column1":"@attr1","column2":"text()"}
        /// </param>
        /// <param name="table">Результат запроса</param>
        void HtmlSelectTable(string html, string rowXPath, string cellXPathes, out ReportTable table);

        /// <summary>
        /// Выборка данных из html-файла
        /// </summary>
        /// <param name="uri">URI http-запроса</param>
        /// <param name="method">http-метод: get, post, put. propfind</param>
        /// <param name="rowXPath">XPath-выражение, указывающее какие узлы html-дерева считать строками таблицы</param>
        /// <param name="cellXPathes">XPath-выражения в виде JSON-объекта, указывающие какие узлы html-дерева считать ячейками, 
        /// где ключ - имя будущего столбца, значение - вычисляемое XPath-выражение относительно XPath-выражения строки
        /// Пример: {"column1":"@attr1","column2":"text()"}
        /// </param>
        /// <param name="table">Результат запроса</param>
        void HtmlWebSelectTable(string uri, HttpMethod method, string rowXPath, string cellXPathes, out ReportTable table);

        /// <summary>
        /// Выборка скалярного значения из html-файла
        /// </summary>
        /// <param name="xml">Путь до html-файла или текст в формате html</param>
        /// <param name="xpath">XPath-выражение для выборки скалярного значения</param>
        /// <param name="result">Возврат скалярного значения</param>
        void HtmlSelectScalar(string html, string xpath, out object result);

        /// <summary>
        /// Выборка скалярного значения из html-файла
        /// </summary>
        /// <param name="uri">URI http-запроса</param>
        /// <param name="method">http-метод: post, get</param>
        /// <param name="xpath">XPath-выражение для выборки скалярного значения</param>
        /// <param name="result">Возврат скалярного значения</param>
        void HtmlWebSelectScalar(string uri, HttpMethod method, string xpath, out object result);
    }

    /// <summary>
    /// Класс, реализующий интерфейс плагина
    /// </summary>
    public class HtmlDataSourcePlug : IPlug
    {
        /// <summary>
        /// Выборка данных из html-файла
        /// </summary>
        /// <param name="html">Локальный путь до html-файла или текст в формате html</param>
        /// <param name="rowXPath">XPath-выражение, указывающее какие узлы html-дерева считать строками таблицы</param>
        /// <param name="cellXPathes">XPath-выражения в виде JSON-объекта, указывающие какие узлы html-дерева считать ячейками, 
        /// где ключ - имя будущего столбца, значение - вычисляемое XPath-выражение относительно XPath-выражения строки
        /// Пример: {"column1":"@attr1","column2":"text()"}
        /// </param>
        /// <param name="table">Результат запроса</param>
        public void HtmlSelectTable(string html, string rowXPath, string cellXPathes, out ReportTable table)
        {
            Dictionary<string, string> cellXPathesDic = null;
            try
            {
                cellXPathesDic = JsonConvert.DeserializeObject<Dictionary<string, string>>(cellXPathes);
            }
            catch (JsonException)
            {
                throw new HtmlDataSourceException("Параметр cellXPathes является невалидным JSON-объектом");
            }
            HtmlDocument document = new HtmlDocument();
            if (File.Exists(html))
                document.Load(html);
            else
                document.LoadHtml(html);
            XPathNavigator xpathNavigator = document.CreateNavigator();
            XPathNodeIterator rowsIterator = null;
            try
            {
                rowsIterator = (XPathNodeIterator)xpathNavigator.Evaluate(rowXPath);
            }
            catch (XPathException)
            {
                HtmlDataSourceException exception = new HtmlDataSourceException("Передано некорректное XPath-выражение строки \"{0}\"");
                exception.Data.Add("{0}", rowXPath);
                throw exception;
            }
            table = new ReportTable();
            foreach (var cellXPath in cellXPathesDic)
                table.Columns.Add(cellXPath.Key);
            while (rowsIterator.MoveNext())
            {
                ReportRow row = new ReportRow(table);
                foreach (var cellXPath in cellXPathesDic)
                {
                    XPathNodeIterator cellIterator = null;
                    try
                    {
                        cellIterator = (XPathNodeIterator)rowsIterator.Current.Evaluate(cellXPath.Value);
                    }
                    catch (XPathException)
                    {
                        HtmlDataSourceException exception = new HtmlDataSourceException("Передано некорректное XPath-выражение ячейки \"{0}\"");
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
        /// Выборка данных из html-файла
        /// </summary>
        /// <param name="uri">URI http-запроса</param>
        /// <param name="method">http-метод: post, get</param>
        /// <param name="rowXPath">XPath-выражение, указывающее какие узлы html-дерева считать строками таблицы</param>
        /// <param name="cellXPathes">XPath-выражения в виде JSON-объекта, указывающие какие узлы html-дерева считать ячейками, 
        /// где ключ - имя будущего столбца, значение - вычисляемое XPath-выражение относительно XPath-выражения строки
        /// Пример: {"column1":"@attr1","column2":"text()"}
        /// </param>
        /// <param name="table">Результат запроса</param>
        public void HtmlWebSelectTable(string uri, HttpMethod method, string rowXPath, string cellXPathes, out ReportTable table)
        {
            Dictionary<string, string> cellXPathesDic = null;
            try
            {
                cellXPathesDic = JsonConvert.DeserializeObject<Dictionary<string, string>>(cellXPathes);
            }
            catch (JsonException)
            {
                throw new HtmlDataSourceException("Параметр cellXPathes является невалидным JSON-объектом");
            }
            HtmlDocument document = null;
            try
            {
                document = new HtmlWeb().Load(uri, method.ToString());
            } catch (HtmlWebException)
            {
                throw new HtmlDataSourceException("Ошибка Html-запроса");
            }
            XPathNavigator xpathNavigator = document.CreateNavigator();
            XPathNodeIterator rowsIterator = null;
            try
            {
                rowsIterator = (XPathNodeIterator)xpathNavigator.Evaluate(rowXPath);
            }
            catch (XPathException)
            {
                HtmlDataSourceException exception = new HtmlDataSourceException("Передано некорректное XPath-выражение строки \"{0}\"");
                exception.Data.Add("{0}", rowXPath);
                throw exception;
            }
            table = new ReportTable();
            foreach (var cellXPath in cellXPathesDic)
                table.Columns.Add(cellXPath.Key);
            while (rowsIterator.MoveNext())
            {
                ReportRow row = new ReportRow(table);
                foreach (var cellXPath in cellXPathesDic)
                {
                    XPathNodeIterator cellIterator = null;
                    try
                    {
                        cellIterator = (XPathNodeIterator)rowsIterator.Current.Evaluate(cellXPath.Value);
                    }
                    catch (XPathException)
                    {
                        HtmlDataSourceException exception = new HtmlDataSourceException("Передано некорректное XPath-выражение ячейки \"{0}\"");
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
        /// Выборка скалярного значения из html-файла
        /// </summary>
        /// <param name="xml">Путь до html-файла или текст в формате html</param>
        /// <param name="xpath">XPath-выражение для выборки скалярного значения</param>
        /// <param name="result">Возврат скалярного значения</param>
        public void HtmlSelectScalar(string html, string xpath, out object result)
        {
            HtmlDocument document = new HtmlDocument();
            if (File.Exists(html))
                document.Load(html);
            else
                document.LoadHtml(html);
            XPathNavigator xpathNavigator = document.CreateNavigator();
            XPathNodeIterator iterator = null;
            try
            {
                iterator = (XPathNodeIterator)xpathNavigator.Evaluate(xpath);
            }
            catch (XPathException)
            {
                HtmlDataSourceException exception = new HtmlDataSourceException("Передано некорректное XPath-выражение \"{0}\"");
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

        /// <summary>
        /// Выборка скалярного значения из html-файла
        /// </summary>
        /// <param name="uri">URI http-запроса</param>
        /// <param name="method">http-метод: post, get</param>
        /// <param name="xpath">XPath-выражение для выборки скалярного значения</param>
        /// <param name="result">Возврат скалярного значения</param>
        public void HtmlWebSelectScalar(string uri, HttpMethod method, string xpath, out object result)
        {
            HtmlDocument document = null;
            try
            {
                document = new HtmlWeb().Load(uri, method.ToString());
            }
            catch (HtmlWebException)
            {
                throw new HtmlDataSourceException("Ошибка Html-запроса");
            }
            XPathNavigator xpathNavigator = document.CreateNavigator();
            XPathNodeIterator iterator = null;
            try
            {
                iterator = (XPathNodeIterator)xpathNavigator.Evaluate(xpath);
            }
            catch (XPathException)
            {
                HtmlDataSourceException exception = new HtmlDataSourceException("Передано некорректное XPath-выражение \"{0}\"");
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
    /// Класс исключения модуля HtmlModule
    /// </summary>
    [Serializable()]
    public class HtmlDataSourceException : Exception
    {
        /// <summary>
        /// Конструктор класса исключения HtmlDataSourceException
        /// </summary>
        public HtmlDataSourceException() : base() { }

        /// <summary>
        /// Конструктор класса исключения HtmlDataSourceException
        /// </summary>
        /// <param name="message">Сообщение об ошибке</param>
        public HtmlDataSourceException(string message) : base(message) { }

        /// <summary>
        /// Конструктор класса исключения HtmlDataSourceException
        /// </summary>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="innerException">Вложенное исключение</param>
        public HtmlDataSourceException(string message, Exception innerException) : base(message, innerException) { }

        /// <summary>
        /// Конструктор класса исключения HtmlDataSourceException
        /// </summary>
        /// <param name="info">Информация сериализации</param>
        /// <param name="context">Контекст потока</param>
        protected HtmlDataSourceException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
