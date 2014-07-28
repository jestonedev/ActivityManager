using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ReportModule
{
    /// <summary>
    /// Перечисление стилей оформления
    /// </summary>
    public enum Style { 
        /// <summary>
        /// Жирный
        /// </summary>
        Bold, 
        /// <summary>
        /// Курсив
        /// </summary>
        Italic, 
        /// <summary>
        /// Подчеркивание
        /// </summary>
        Underline, 
        /// <summary>
        /// Зачеркнутый текст
        /// </summary>
        Strike, 
        /// <summary>
        /// Отсутствует
        /// </summary>
        None };

    /// <summary>
    /// Класс управления стилями OpenOffice
    /// </summary>
    public class OOStyleSheet
    {
        /// <summary>
        /// Пространство имен fo
        /// </summary>
        public const string XmlnsFO = "urn:oasis:names:tc:opendocument:xmlns:xsl-fo-compatible:1.0";

        /// <summary>
        /// Пространство имен style
        /// </summary>
        public const string XmlnsStyle = "urn:oasis:names:tc:opendocument:xmlns:style:1.0";

        /// <summary>
        /// Пространство имен office
        /// </summary>
        public const string XmlnsOffice = "urn:oasis:names:tc:opendocument:xmlns:office:1.0";

        /// <summary>
        /// Пространство имен text
        /// </summary>
        public const string XmlnsText = "urn:oasis:names:tc:opendocument:xmlns:text:1.0";

        private List<XElement> styles = new List<XElement>();
        private int next_style_num;
        private XDocument document;
        
        private Dictionary<Style, List<XAttribute>> styles_attributes = new Dictionary<Style,List<XAttribute>>()
        {
            {Style.Bold, new List<XAttribute>() { 
                new XAttribute(XName.Get("font-weight",XmlnsFO),"bold"),
                new XAttribute(XName.Get("font-weight-asian", XmlnsStyle),"bold"),
                new XAttribute(XName.Get("font-weight-complex",XmlnsStyle),"bold")
            } },
            {Style.Italic, new List<XAttribute>() {
                new XAttribute(XName.Get("font-style",XmlnsFO),"italic"),
                new XAttribute(XName.Get("font-style-asian", XmlnsStyle),"italic"),
                new XAttribute(XName.Get("font-style-complex",XmlnsStyle),"italic")
            } },
            {Style.Underline, new List<XAttribute>() {
                new XAttribute(XName.Get("text-underline-style",XmlnsStyle),"solid"),
                new XAttribute(XName.Get("text-underline-width",XmlnsStyle),"auto"),
                new XAttribute(XName.Get("text-underline-color",XmlnsStyle),"font-color")
            } },
            {Style.Strike, new List<XAttribute>() {
            } }
        };

        private string get_style_name()
        {
            string style_name = "T"+next_style_num.ToString();
            next_style_num++;
            return style_name;
        }

        /// <summary>
        /// Конструктор класса OOStyleSheet
        /// </summary>
        /// <param name="document">Документ OpenOffice, из которого загружаются стили</param>
        public OOStyleSheet(XDocument document)
        {
            this.document = document;
            styles = ReportHelper.FindElementsByTag(document.Root, "style");
            foreach (XElement style in styles)
            {
                string name = style.Attribute(XName.Get("name", XmlnsStyle)).Value;
                if (name[0] == 'T')
                {
                    int style_number = Int32.Parse(name.TrimStart(new Char[] {'T'}));
                    if (style_number > next_style_num)
                        next_style_num = style_number;
                }
            }
            next_style_num++;
        }

        /// <summary>
        /// Метод копирует стиль
        /// </summary>
        /// <param name="styleName">Имя стиля, который копируем</param>
        /// <param name="newStyleFamily">Семейство нового стиля</param>
        /// <returns>Возвращает имя нового стиля</returns>
        public string CopyStyle(string styleName, string newStyleFamily)
        {
            foreach (XElement style in styles)
                if (style.Attribute(XName.Get("name", XmlnsStyle)).Value == styleName)
                {
                    XElement new_style = new XElement(style);
                    string new_style_name = get_style_name();
                    if (new_style.Attribute(XName.Get("name", XmlnsStyle)) != null)
                        new_style.Attribute(XName.Get("name", XmlnsStyle)).Value = new_style_name;
                    else
                        new_style.Add(new XAttribute(XName.Get("name", XmlnsStyle), new_style_name));
                    if (new_style.Attribute(XName.Get("family", XmlnsStyle)) != null)
                        new_style.Attribute(XName.Get("family", XmlnsStyle)).Value = newStyleFamily;
                    else
                        new_style.Add(new XAttribute(XName.Get("family", XmlnsStyle), newStyleFamily));
                    styles.Add(new_style);
                    document.Root.Element(XName.Get("automatic-styles", XmlnsOffice)).Add(new_style);
                    return new_style_name;
                }
            return styleName;
        }

        /// <summary>
        /// Метод создает новый стиль
        /// </summary>
        /// <param name="newStyleFamily">Семейство нового стиля</param>
        /// <returns>Возвращает имя нового стиля</returns>
        public string CreateStyle(string newStyleFamily)
        {
            string new_style_name = get_style_name();
            XElement new_style = new XElement(XName.Get("style", XmlnsStyle),
                new XAttribute(XName.Get("name", XmlnsStyle),new_style_name),
                new XAttribute(XName.Get("family", XmlnsStyle),newStyleFamily),
                new XElement(XName.Get("text-properties", XmlnsStyle)));
            styles.Add(new_style);
            document.Root.Element(XName.Get("automatic-styles", XmlnsOffice)).Add(new_style);
            return new_style_name;
        }

        /// <summary>
        /// Применить стилевое дополнение к указанному стилю
        /// </summary>
        /// <param name="styleName">Имя стиля</param>
        /// <param name="style">Стилевое дополнение (жирный, курсив, подчеркивание, зачеркивание)</param>
        public void ApplyStyle(string styleName, Style style)
        {
            List<XAttribute> attributes = styles_attributes[style];
            foreach (XElement style_element in styles)
                if (style_element.Attribute(XName.Get("name", XmlnsStyle)).Value == styleName)
                    foreach (XAttribute attribute in attributes)
                    {
                        XElement text_properties = style_element.Element(XName.Get("text-properties", XmlnsStyle));
                        if (text_properties.Attribute(attribute.Name) != null)
                        {
                            text_properties.Attribute(attribute.Name).Value = attribute.Value;
                        }
                        else
                            text_properties.Add(new XAttribute(attribute));
                    }
        }
    }
}
