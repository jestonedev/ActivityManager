using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Text.RegularExpressions;

namespace ReportModule
{
    /// <summary>
    /// Класс со вспомогательными фунциями
    /// </summary>
    class ReportHelper
    {
        private ReportHelper() { }

        /// <summary>
        /// Получить регулярное выражение по списку параметров, входящих в паттерн таблицы
        /// </summary>
        /// <param name="templates">Список параметров, входящих в паттерн таблицы</param>
        /// <returns>Регулярное выражение</returns>
        public static string get_table_pattern_regex(List<string> templates)
        {
            if (templates.Count == 0)
                return "";
            string reg_match_pattern = "^";
            foreach (string template in templates)
                reg_match_pattern += @"[\s\S]*" + Regex.Escape(template);
            return reg_match_pattern + @"[\s\S]*$";
        }

        /// <summary>
        /// Поиск xml-элементов с тэгом xml_tag в элементе xelement с рекурсивным заходом
        /// </summary>
        /// <param name="xelement">Корневой элемент</param>
        /// <param name="xml_tag">XML-тэг искомого элемента</param>
        /// <returns>Список найденных элементов</returns>
        public static List<XElement> find_xelements(XElement xelement, string xml_tag)
        {
            List<XElement> elements = new List<XElement>();
            foreach (XElement element in xelement.Elements())
            {
                List<XElement> child_elements = ReportHelper.find_xelements(element, xml_tag);
                if ((element.Name.LocalName == xml_tag))
                    elements.Add(element);
                foreach (XElement child_element in child_elements)
                    elements.Add(child_element);
            }
            elements.Sort(new XComparer()); 
            return elements;
        }
    }

    /// <summary>
    /// Компаратор классов XElement
    /// </summary>
    internal class XComparer : IComparer<XElement>
    {
        public int Compare(XElement x, XElement y)
        {
            return y.ToString().CompareTo(x.ToString());
        }
    }
}
