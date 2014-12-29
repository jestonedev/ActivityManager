using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ReportModule
{
    internal class MSEditor: ReportEditor
    {
        //Описание стилей: тип стиля, тэг, атрибуты тэга
        protected Dictionary<Style, Dictionary<string, Dictionary<string, string>>> styleTags = new Dictionary<Style, Dictionary<string, Dictionary<string, string>>>() {
                { Style.Bold, new  Dictionary<string, Dictionary<string, string>>() { {"b", new Dictionary<string,string>() } }},
                { Style.Italic, new  Dictionary<string, Dictionary<string, string>>() { {"i", new Dictionary<string,string>() } }},
                { Style.Underline, new  Dictionary<string, Dictionary<string, string>>() { {"u", new Dictionary<string,string>() {{"val", "single"}}} }},
                { Style.Strike, new  Dictionary<string, Dictionary<string, string>>() { {"strike", new Dictionary<string,string>() } }}
        };

        /// <summary>
        /// Список стилей, которые необходимо применить к каждому последующему элементу
        /// </summary>
        protected List<Style> styles = new List<Style>();

        protected virtual void prepair_element(XElement xelement)
        {
            if (xelement == null)
                throw new ReportException("Не задана ссылка на элемент документ шаблона");
            List<string> patterns = new List<string>();
            foreach (string spec_tag in Enum.GetNames(typeof(SpecTag)))
            {
                patterns.Add("$" + spec_tag.ToUpper(CultureInfo.CurrentCulture) + "$");
                patterns.Add("$" + spec_tag.ToLower(CultureInfo.CurrentCulture) + "$");
                patterns.Add("$/" + spec_tag.ToUpper(CultureInfo.CurrentCulture) + "$");
                patterns.Add("$/" + spec_tag.ToLower(CultureInfo.CurrentCulture) + "$");
            }
            patterns.Add("$br$");
            patterns.Add("$BR$");
            patterns.Add("$sbr$");
            patterns.Add("$SBR$");
            foreach (string pattern in patterns)
            {
                List<PatternNodeInfoCollection> ppis = new List<PatternNodeInfoCollection>();
                foreach (XNode node in xelement.Nodes())
                {
                    ppis = ReportHelper.GetNodePatternPartsInfo(node, pattern, ppis);
                }
                for (int i = 0; i < ppis.Count; i++)
                {
                    if (ppis[i].Items[ppis[i].Items.Count - 1].IsClosingPatternNode)
                    {
                        XNode newNode = ReportHelper.ReplaceNodePatternPart(ppis[i].Items[0], pattern);
                        ReportHelper.RebindNodePatternPartsInfo(newNode, ppis[i].Items[0].Node, ppis);
                        for (int j = 1; j < ppis[i].Items.Count; j++)
                        {
                            newNode = ReportHelper.ReplaceNodePatternPart(ppis[i].Items[j], "");
                            ReportHelper.RebindNodePatternPartsInfo(newNode, ppis[i].Items[j].Node, ppis);
                        }
                    }
                }
            }
        }

        protected virtual XElement parse_style_tags(XElement xelement, string xmlnsMain)
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
                    string content = textElement.Value;
                    SortedDictionary<int, TagInfo> style_tags = GetStyleTags(content);
                    if (style_tags.Count > 0)
                    {
                        string[] spliters = new string[style_tags.Count * 2];
                        int i = 0;
                        foreach (var spec_tag in style_tags)
                        {
                            if (spec_tag.Value.tag_type == SpecTagType.OpenTag)
                            {
                                spliters[i] = @"$" + spec_tag.Value.tag.ToString().ToLower(CultureInfo.CurrentCulture) + @"$";
                                spliters[i + 1] = @"$" + spec_tag.Value.tag.ToString().ToUpper(CultureInfo.CurrentCulture) + @"$";
                            }
                            else
                            {
                                spliters[i] = @"$/" + spec_tag.Value.tag.ToString().ToLower(CultureInfo.CurrentCulture) + @"$";
                                spliters[i + 1] = @"$/" + spec_tag.Value.tag.ToString().ToUpper(CultureInfo.CurrentCulture) + @"$";
                            }
                            i = i + 2;
                        }
                        string[] values = content.Split(spliters, StringSplitOptions.None);
                        i = 0;
                        foreach (string value in values)
                        {
                            TagInfo spec_tag_info = null;
                            int j = 1;
                            foreach (var spec_tag in style_tags)
                            {
                                if (i == j)
                                {
                                    spec_tag_info = spec_tag.Value;
                                    break;
                                }
                                j++;
                            }
                            if (spec_tag_info != null)
                            {
                                if (spec_tag_info.tag_type == SpecTagType.OpenTag)
                                    styles.Add(ReportHelper.GetStyleBySpecTag(spec_tag_info.tag));
                                else
                                    styles.Remove(ReportHelper.GetStyleBySpecTag(spec_tag_info.tag));
                            }
                            i++;
                            if (String.IsNullOrEmpty(value))
                                continue;
                            XElement new_element = new XElement(child_element);
                            textElement = new_element.Element(XName.Get("t", xmlnsMain));
                            textElement.Value = value;
                            if (value != value.Trim() && textElement.Attribute(XNamespace.Xml + "space") == null)
                                textElement.Add(new XAttribute(XNamespace.Xml + "space", "preserve"));
                            foreach (Style style in styles)
                                foreach (var styleTag in styleTags[style])
                                {
                                    XElement tag = new XElement(XName.Get(styleTag.Key, xmlnsMain));
                                    XElement rPrElement = child_element.Element(XName.Get("rPr", xmlnsMain));
                                    foreach (var attribute in styleTag.Value)
                                        tag.Add(new XAttribute(XName.Get(attribute.Key, xmlnsMain), attribute.Value));
                                    if (rPrElement == null)
                                        new_element.Add(new XElement(XName.Get("rPr", xmlnsMain)));
                                    new_element.Element(XName.Get("rPr", xmlnsMain)).Add(tag);
                                }
                            new_xelement.Add(new_element);
                        }
                    }
                    else
                    {
                        XElement new_element = new XElement(child_element);
                        foreach (Style style in styles)
                            foreach (var styleTag in styleTags[style])
                            {
                                XElement tag = new XElement(XName.Get(styleTag.Key, xmlnsMain));
                                XElement rPrElement = child_element.Element(XName.Get("rPr", xmlnsMain));
                                foreach (var attribute in styleTag.Value)
                                    tag.Add(new XAttribute(XName.Get(attribute.Key, xmlnsMain), attribute.Value));
                                if (rPrElement == null)
                                    new_element.Add(new XElement(XName.Get("rPr", xmlnsMain)));
                                new_element.Element(XName.Get("rPr", xmlnsMain)).Add(tag);
                            }
                        new_xelement.Add(new_element);
                    }
                }
                else
                    new_xelement.Add(child_element);
            xelement.ReplaceWith(new_xelement);
            return new_xelement;
        }
    }
}
