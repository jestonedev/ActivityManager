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
using System.Text.RegularExpressions;

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
        #region html_spec_simbols
        private Dictionary<string, string> html_spec_simbols = new Dictionary<string,string>()
        {
            {"&nbsp;"," "},  //неразрывный пробел
            {"&ensp;"," "},  //узкий пробел (еn-шириной в букву n)
            {"&emsp;"," "},  //широкий пробел (em-шириной в букву m)
            {"&ndash;","–"}, //узкое тире (en-тире)
            {"&mdash;","—"}, //широкое тире (em-тире)
            {"&shy;","\u00AD"},   //мягкий перенос
            {"&copy;","©"},  //копирайт
            {"&reg;","®"},   //знак зарегистрированной торговой марки
            {"&trade;","™"}, //знак торговой марки
            {"&ordm;","º"},  //копье Марса
            {"&ordf;","ª"},  //зеркало Венеры
            {"&permil;","‰"},//промилле
            {"&brvbar;","¦"},//вертикальный пунктир
            {"&sect;","§"},  //параграф
            {"&deg;","°"},   //градус
            {"&micro;","µ"}, //знак "микро"
            {"&para;","¶"},  //знак абзаца
            {"&hellip;","…"},//многоточие
            {"&oline;","‾"}, //надчеркивание
            {"&#8254;","‾"}, //надчеркивание
            {"&acute;","´"}, //знак ударения
            {"&times;","×"}, //умножить
            {"&divide;","÷"},//разделить
            {"&lt;","<"},    //меньше
            {"&gt;",">"},    //больше
            {"&plusmn;","±"},//плюс/минус
            {"&sup1;","¹"},  //степень 1
            {"&sup2;","²"},  //степень 2
            {"&sup3;","³"},  //степень 3
            {"&not;","¬"},   //отрицание
            {"&frac14;","¼"},//одна четвертая
            {"&frac12;","½"},//одна вторая
            {"&frac34;","¾"},//три четверти
            {"&frasl;","⁄"}, //дробная черта
            {"&minus;","−"}, //минус
            {"&le;","≤"},    //меньше или равно
            {"&ge;","≥"},    //больше или равно
            {"&asymp;","≈"}, //приблизительно (почти) равно
            {"&ne;","≠"},    //не равно
            {"&equiv;","≡"}, //тождественно
            {"&radic;","√"}, //квадратный корень (радикал)
            {"&infin;","∞"}, //бесконечность
            {"&sum;","∑"},   //знак суммирования
            {"&prod;","∏"},  //знак произведения
            {"&part;","∂"},  //частичный дифференциал
            {"&int;","∫"},   //интеграл
            {"&forall;","∀"},//для всех
            {"&exist;","∃"}, //существует
            {"&empty;","∅"}, //пустое множество
            {"&Oslash;","Ø"},//диаметр
            {"&isin;","∈"},  //принадлежит
            {"&notin;","∉"}, //не принадлежит
            {"&ni;","∋"},    //содержит
            {"&sub;","⊂"},   //является подмножеством
            {"&sup;","⊃"},   //является надмножеством
            {"&nsub;","⊄"},  //не является подмножеством
            {"&sube;","⊆"},  //является подмножеством либо равно
            {"&supe;","⊇"},  //является надмножеством либо равно
            {"&oplus;","⊕"}, //плюс в кружке
            {"&otimes;","⊗"},//знак умножения в кружке
            {"&perp;","⊥"},  //перпендикулярно
            {"&ang;","∠"},   //угол
            {"&and;","∧"},   //логическое И
            {"&or;","∨"},    //логическое ИЛИ
            {"&cap;","∩"},   //пересечение
            {"&cup;","∪"},   //объединение
            {"&euro;","€"},   //Евро
            {"&cent;","¢"},   //Цент
            {"&pound;","£"},  //Фунт
            {"&current;","¤"},//Знак валюты
            {"&yen;","¥"},    //Знак йены и юаня
            {"&fnof;","ƒ"},   //Знак флорина
            {"&bull;","•"},   //простой маркер
            {"&middot;","·"}, //средняя точка
            {"&spades;","♠"}, //пики
            {"&clubs;","♣"},  //трефы
            {"&hearts;","♥"}, //червы
            {"&diams;","♦"},  //бубны
            {"&loz;","◊"},    //ромб
            {"&quot;","\""},  //двойная кавычка
            {"&amp;","&"},    //амперсанд
            {"&laquo;","«"},  //левая типографская кавычка (кавычка-елочка)
            {"&raquo;","»"},  //правая типографская кавычка (кавычка-елочка)
            {"&prime;","′"},  //штрих (минуты, футы)
            {"&Prime;","″"},  //двойной штрих (секунды, дюймы)
            {"&lsquo;","‘"},  //левая верхняя одиночная кавычка
            {"&rsquo;","’"},  //правая  верхняя одиночная кавычка
            {"&sbquo;","‚"},  //правая нижняя одиночная кавычка
            {"&ldquo;","“"},  //кавычка-лапка левая
            {"&rdquo;","”"},  //кавычка-лапка правая верхняя
            {"&bdquo;","„"},  //кавычка-лапка правая нижняя
            {"&larr;","←"},   //стрелка влево
            {"&uarr;","↑"},   //стрелка вверх
            {"&rarr;","→"},   //стрелка вправо
            {"&darr;","↓"},   //стрелка вниз
            {"&harr;","↔"},   //стрелка влево и вправо
            {"&crarr;","↵"},  //возврат каретки
            {"&lArr;","⇐"},  //двойная стрелка влево
            {"&uArr;","⇑"},   //двойная стрелка вверх
            {"&rArr;","⇒"},  //двойная стрелка вправо
            {"&dArr;","⇓"},  //двойная стрелка вниз
            {"&hArr;","⇔"},  //двойная стрелка влево и вправо
            {"&alpha;","α"},  
            {"&beta;","β"},  
            {"&gamma;","γ"},  
            {"&delta;","δ"},  
            {"&epsilon;","ε"},  
            {"&zeta;","ζ"},  
            {"&eta;","η"},  
            {"&theta;","θ"}, 
            {"&iota;","ι"}, 
            {"&kappa;","κ"}, 
            {"&lambda;","λ"}, 
            {"&mu;","μ"}, 
            {"&nu;","ν"}, 
            {"&xi;","ξ"}, 
            {"&omicron;","ο"}, 
            {"&pi;","π"}, 
            {"&rho;","ρ"}, 
            {"&sigma;","σ"}, 
            {"&sigmaf;","ς"}, 
            {"&tau;","τ"}, 
            {"&upsilon;","υ"}, 
            {"&phi;","φ"}, 
            {"&chi;","χ"}, 
            {"&psi;","ψ"}, 
            {"&omega;","ω"}, 
            {"&Alpha;","Α"},  
            {"&Beta;","Β"},  
            {"&Gamma;","Γ"},  
            {"&Delta;","Δ"},  
            {"&Epsilon;","Ε"},  
            {"&Zeta;","Ζ"},  
            {"&Eta;","Η"},  
            {"&Theta;","Θ"}, 
            {"&Iota;","Ι"}, 
            {"&Kappa;","Κ"}, 
            {"&Lambda;","Λ"}, 
            {"&Mu;","Μ"}, 
            {"&Nu;","Ν"}, 
            {"&Xi;","Ξ"}, 
            {"&Omicron;","Ο"}, 
            {"&Pi;","Π"}, 
            {"&Rho;","Ρ"}, 
            {"&Sigma;","Σ"}, 
            {"&Tau;","Τ"}, 
            {"&Upsilon;","Υ"}, 
            {"&Phi;","Φ"}, 
            {"&Chi;","Χ"}, 
            {"&Psi;","Ψ"}, 
            {"&Omega;","Ω"}
        };
        #endregion html_spec_simbols

        private string ReplaceSpecSymbols(string value)
        {
            foreach (var html_spec_simbol in html_spec_simbols)
                value.Replace(html_spec_simbol.Key, html_spec_simbol.Value);
            MatchCollection matches = Regex.Matches(value, "&#([xX]){0,1}([0-9a-fA-F]{1,8});", RegexOptions.IgnoreCase);
            foreach (Match match in matches)
            {
                char c;
                if (match.Groups.Count == 3)
                    c = ((char)int.Parse(match.Groups[match.Groups.Count - 1].Value, System.Globalization.NumberStyles.HexNumber));
                else
                    c = ((char)int.Parse(match.Groups[match.Groups.Count - 1].Value));
                value = value.Replace(match.Value, c.ToString());
            }
            return value;
        }

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
                            result += cellIterator.Current.InnerXml.Replace("&amp;", "&");
                        else
                            result += cellIterator.Current.Value;
                    }
                    result = ReplaceSpecSymbols(result);
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
                        string tmpStr = "";
                        if (cellIterator.Current.NodeType == XPathNodeType.Element)
                            tmpStr = cellIterator.Current.InnerXml.Replace("&amp;", "&");
                        else
                            tmpStr = cellIterator.Current.Value;
                        result += tmpStr;
                    }
                    result = ReplaceSpecSymbols(result);
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
                    result += iterator.Current.InnerXml.Replace("&amp;", "&");
                else
                    result += iterator.Current.Value;
            }
            result = ReplaceSpecSymbols(result.ToString());
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
                    result += iterator.Current.InnerXml.Replace("&amp;","&");
                else
                    result += iterator.Current.Value;
            }
            result = ReplaceSpecSymbols(result.ToString());
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
