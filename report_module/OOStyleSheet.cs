using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace report_module
{
    public enum Style { Bold, Italic, Underline, Strike, None };

    public class OOStyleSheet
    {
        public const string xmlns_fo = "urn:oasis:names:tc:opendocument:xmlns:xsl-fo-compatible:1.0";
        public const string xmlns_style = "urn:oasis:names:tc:opendocument:xmlns:style:1.0";
        public const string xmlns_office = "urn:oasis:names:tc:opendocument:xmlns:office:1.0";
        public const string xmlns_text = "urn:oasis:names:tc:opendocument:xmlns:text:1.0";
        public const string xmlns_draw = "urn:oasis:names:tc:opendocument:xmlns:drawing:1.0";
        private List<XElement> styles = new List<XElement>();
        private int next_style_num;
        private XDocument xdocument;
        
        private Dictionary<Style, List<XAttribute>> styles_attributes = new Dictionary<Style,List<XAttribute>>()
        {
            {Style.Bold, new List<XAttribute>() { 
                new XAttribute(XName.Get("font-weight",xmlns_fo),"bold"),
                new XAttribute(XName.Get("font-weight-asian", xmlns_style),"bold"),
                new XAttribute(XName.Get("font-weight-complex",xmlns_style),"bold")
            } },
            {Style.Italic, new List<XAttribute>() {
                new XAttribute(XName.Get("font-style",xmlns_fo),"italic"),
                new XAttribute(XName.Get("font-style-asian", xmlns_style),"italic"),
                new XAttribute(XName.Get("font-style-complex",xmlns_style),"italic")
            } },
            {Style.Underline, new List<XAttribute>() {
                new XAttribute(XName.Get("text-underline-style",xmlns_style),"solid"),
                new XAttribute(XName.Get("text-underline-width",xmlns_style),"auto"),
                new XAttribute(XName.Get("text-underline-color",xmlns_style),"font-color")
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

        public OOStyleSheet(XDocument xdocument)
        {
            this.xdocument = xdocument;
            styles = ReportHelper.find_xelements(xdocument.Root, "style");
            foreach (XElement style in styles)
            {
                string name = style.Attribute(XName.Get("name", xmlns_style)).Value;
                if (name[0] == 'T')
                {
                    int style_number = Int32.Parse(name.TrimStart(new Char[] {'T'}));
                    if (style_number > next_style_num)
                        next_style_num = style_number;
                }
            }
            next_style_num++;
        }

        public string CopyStyle(string style_name, string new_style_family)
        {
            foreach (XElement style in styles)
                if (style.Attribute(XName.Get("name", xmlns_style)).Value == style_name)
                {
                    XElement new_style = new XElement(style);
                    string new_style_name = get_style_name();
                    if (new_style.Attribute(XName.Get("name", xmlns_style)) != null)
                        new_style.Attribute(XName.Get("name", xmlns_style)).Value = new_style_name;
                    else
                        new_style.Add(new XAttribute(XName.Get("name", xmlns_style), new_style_name));
                    if (new_style.Attribute(XName.Get("family", xmlns_style)) != null)
                        new_style.Attribute(XName.Get("family", xmlns_style)).Value = new_style_family;
                    else
                        new_style.Add(new XAttribute(XName.Get("family", xmlns_style), new_style_family));
                    styles.Add(new_style);
                    xdocument.Root.Element(XName.Get("automatic-styles", xmlns_office)).Add(new_style);
                    return new_style_name;
                }
            return style_name;
        }

        public string CreateStyle(string new_style_family)
        {
            string new_style_name = get_style_name();
            XElement new_style = new XElement(XName.Get("style", xmlns_style),
                new XAttribute(XName.Get("name", xmlns_style),new_style_name),
                new XAttribute(XName.Get("family", xmlns_style),new_style_family),
                new XElement(XName.Get("text-properties", xmlns_style)));
            styles.Add(new_style);
            xdocument.Root.Element(XName.Get("automatic-styles", xmlns_office)).Add(new_style);
            return new_style_name;
        }

        public void ApplyStyle(string style_name, Style style)
        {
            List<XAttribute> attributes = styles_attributes[style];
            foreach (XElement style_element in styles)
                if (style_element.Attribute(XName.Get("name", xmlns_style)).Value == style_name)
                    foreach (XAttribute attribute in attributes)
                    {
                        XElement text_properties = style_element.Element(XName.Get("text-properties", xmlns_style));
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
