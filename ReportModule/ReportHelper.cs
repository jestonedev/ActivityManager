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
        public static List<XElement> FindElementsByTag(XElement xelement, string xml_tag)
        {
            List<XElement> elements = new List<XElement>();
            foreach (XElement element in xelement.Elements())
            {
                List<XElement> child_elements = ReportHelper.FindElementsByTag(element, xml_tag);
                foreach (XElement child_element in child_elements)
                    elements.Add(child_element);
                if ((element.Name.LocalName == xml_tag))
                    elements.Add(element);
            }
            return elements;
        }

        /// <summary>
        /// Метод поиска шаблона в текстовых xml-узлах
        /// </summary>
        /// <param name="textNode">Текстовый узел</param>
        /// <param name="pattern">Шаблон</param>
        /// <param name="ListPNIC">Коллекция шаблонов в узлах</param>
        /// <returns>Обновленная коллекция шаблонов в узлах</returns>
        private static List<PatternNodeInfoCollection> GetTextNodePatternPartInfo(XText textNode, string pattern, List<PatternNodeInfoCollection> ListPNIC)
        {
            PatternNodeInfo pni = new PatternNodeInfo(textNode);
            PatternNodeInfoCollection pnic = null;
            string text = (textNode as XText).Value;
            int currentPatternIndex = 0;
            bool isNewPNIC = true;
            if (ListPNIC.Count > 0)
                currentPatternIndex = ListPNIC[ListPNIC.Count - 1].PatternLength % pattern.Length;
            if (ListPNIC.Count == 0 || ListPNIC[ListPNIC.Count - 1].Items[ListPNIC[ListPNIC.Count - 1].Items.Count - 1].IsClosingPatternNode)
            {
                pnic = new PatternNodeInfoCollection();
                currentPatternIndex = 0;
            }
            else
            {
                pnic = ListPNIC[ListPNIC.Count - 1];
                isNewPNIC = false;
            }
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == pattern[currentPatternIndex])
                {
                    if (currentPatternIndex == 0)
                        pni.IsStartingPatternNode = true;
                    if (pni.StartIndex == -1)
                        pni.StartIndex = text.Length - i;
                    pni.EndIndex = text.Length - i;
                    currentPatternIndex++;
                    if (pattern.Length == currentPatternIndex)
                    {
                        pni.IsClosingPatternNode = true;
                        pnic.Items.Add(pni);
                        if (isNewPNIC)
                            ListPNIC.Add(pnic);
                        pnic = new PatternNodeInfoCollection();
                        isNewPNIC = true;
                        pni = new PatternNodeInfo(textNode);
                        currentPatternIndex = 0;
                    }
                }
                else
                    if (text[i] == pattern[0])
                    {
                        currentPatternIndex = 1;
                        pni = new PatternNodeInfo(textNode);
                        pni.IsStartingPatternNode = true;
                        pni.StartIndex = text.Length - i;
                        pni.EndIndex = text.Length - i;
                        pnic = new PatternNodeInfoCollection();
                        isNewPNIC = true;
                    }
                    else
                    {
                        currentPatternIndex = 0;
                        pni = new PatternNodeInfo(textNode);
                        if (!isNewPNIC)
                        {
                            for (int j = pnic.Items.Count - 1; j >= 0; j--)
                            {
                                if (!pnic.Items[j].IsStartingPatternNode)
                                    pnic.Items.Remove(pnic.Items[j]);
                                else
                                if (pnic.Items[j].IsStartingPatternNode)
                                {
                                    pnic.Items.Remove(pnic.Items[j]);
                                    break;
                                }
                            }
                        }
                        else
                            pnic = new PatternNodeInfoCollection();
                        isNewPNIC = true;
                    }
            }
            if ((pni.StartIndex != -1) && (pni.StartIndex >= pni.EndIndex))
            {
                pnic.Items.Add(pni);
                if (isNewPNIC)
                    ListPNIC.Add(pnic);
            }
            for (int i = ListPNIC.Count - 1; i >= 0; i--)
                if (ListPNIC[i].Items.Count == 0)
                    ListPNIC.Remove(ListPNIC[i]);
            return ListPNIC;
        }

        /// <summary>
        /// Метод поиска частей шаблона в xml-узлах
        /// </summary>
        /// <param name="node">XML-узел, в котором производится поиск</param>
        /// <param name="pattern">Шаблон поиска</param>
        /// <param name="ListPNIC">Коллекция шаблонов в узлах</param>
        /// <returns>Обновленная коллекция шаблонов в узлах</returns>
        public static List<PatternNodeInfoCollection> GetNodePatternPartsInfo(XNode node, string pattern, List<PatternNodeInfoCollection> ListPNIC)
        {
            if (node.NodeType == System.Xml.XmlNodeType.Text)
                ListPNIC = GetTextNodePatternPartInfo(node as XText, pattern, ListPNIC);
            else
                if (node.NodeType == System.Xml.XmlNodeType.Element)
                {
                    // Если элемент имеет дочерние, то входим в дочерний элемент и обрабатываем его
                    foreach (XNode child_node in (node as XElement).Nodes())
                    {
                        if (child_node.NodeType == System.Xml.XmlNodeType.Element)
                            ListPNIC = GetNodePatternPartsInfo(child_node, pattern, ListPNIC);
                        else
                            if (child_node.NodeType == System.Xml.XmlNodeType.Text)
                                ListPNIC = GetTextNodePatternPartInfo(child_node as XText, pattern, ListPNIC);
                    }
                }
            return ListPNIC;
        }

        /// <summary>
        /// Метод ищет совпадения шаблона в элементе и возвращает их количество
        /// </summary>
        /// <param name="element">Элемент</param>
        /// <param name="pattern">Шаблон</param>
        /// <returns>Количество совпадений шаблона в элементе</returns>
        public static int MatchesPattern(XElement element, string pattern)
        {
            int matches = 0;    //Число совпадений шаблона
            List<PatternNodeInfoCollection> ListPNIC = new List<PatternNodeInfoCollection>();
            foreach (XNode node in element.Nodes())
                ListPNIC = ReportHelper.GetNodePatternPartsInfo(node, pattern, ListPNIC);
            foreach (PatternNodeInfoCollection item in ListPNIC)
                if (item.Items[item.Items.Count - 1].IsClosingPatternNode)
                    matches++;
            return matches;
        }

        /// <summary>
        /// Метод заменяет шаблон переданным значением
        /// </summary>
        /// <param name="pnic">Информация о блоке замены</param>
        /// <param name="value">Значение, на которое производится замена</param>
        public static XNode ReplaceNodePatternPart(PatternNodeInfo pnic, string value)
        {
            string text = "";
            string leftTextPart = "";
            string rightTextPart = "";
            if (pnic.Node.NodeType == System.Xml.XmlNodeType.Text)
                text = (pnic.Node as XText).Value;
            else
                throw new ReportException("Тип xml-узла неизвестен");
            if (pnic.Node.Parent as XElement == null)
                throw new ReportException("Тип родительского xml-узла неизвестен");
            leftTextPart = text.Substring(0, text.Length - pnic.StartIndex);
            rightTextPart = text.Substring(text.Length - pnic.EndIndex + 1);
            text = leftTextPart + value + rightTextPart;
            XText node = new XText(text);
            (pnic.Node as XText).ReplaceWith(node);
            return node;
        }

        /// <summary>
        /// Метод выполняет перепривязку ноды после замены
        /// </summary>
        /// <param name="node"></param>
        /// <param name="ppis"></param>
        public static void RebindNodePatternPartsInfo(XNode newNode, XNode oldNode, List<PatternNodeInfoCollection> ppis)
        {
            for (int i = 0; i < ppis.Count; i++)
                for (int j = 0; j < ppis[i].Items.Count; j++)
                    if (ppis[i].Items[j].Node == oldNode)
                        ppis[i].Items[j].Node = newNode;
        }
    }

    /// <summary>
    /// Коллекция классов с информацией о позиции частей паттерна в элементах
    /// </summary>
    internal class PatternNodeInfoCollection
    {
        public List<PatternNodeInfo> Items { get; set; }
        public int PatternLength
        {
            get
            {
                int length = 0;
                foreach (PatternNodeInfo item in Items)
                    length += item.StartIndex - item.EndIndex + 1;
                return length;
            } 
        }

        public PatternNodeInfoCollection()
        {
            Items = new List<PatternNodeInfo>();
        }
    }

    /// <summary>
    /// Класс с информацией о позиции частей паттерна в элементах
    /// </summary>
    internal class PatternNodeInfo
    {
        public XNode Node { get; set; }
        public int StartIndex { get; set; }
        public int EndIndex { get; set; }
        public bool IsStartingPatternNode { get; set; }
        public bool IsClosingPatternNode { get; set; }

        public PatternNodeInfo(XNode node)
        {
            this.Node = node;
            StartIndex = -1;
            EndIndex = -1;
        }
    }

    /// <summary>
    /// Компаратор классов XElement
    /// </summary>
    internal class XComparer : IComparer<XElement>
    {
        public int Compare(XElement x, XElement y)
        {
            return String.Compare(y == null ? "" : y.ToString(), x == null ? "" : x.ToString(), StringComparison.Ordinal);
        }
    }
}
