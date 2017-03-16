using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
using System.Globalization;

namespace ReportModule
{
    /// <summary>
    /// Редактор отчетов OpenOffice Calc
    /// </summary>
    internal class CalcEditor: OOEditor
    {
        private readonly string _office = "urn:oasis:names:tc:opendocument:xmlns:office:1.0";

        protected override void TryConvertTypes(List<PatternNodeInfoCollection> ppis, string value)
        {
            for (int i = 0; i < ppis.Count; i++)
            {
                if (ppis[i].Items[ppis[i].Items.Count - 1].IsClosingPatternNode)
                {
                    var patternNodeInfo = ppis[i].Items.FirstOrDefault();
                    if (patternNodeInfo == null)
                    {
                        continue;
                    }
                    var patternNode = patternNodeInfo.Node;
                    if (patternNode.NodeType == System.Xml.XmlNodeType.Element &&
                        ((XElement)patternNode).Name.LocalName == "table-cell")
                    {
                        TryConvertNodeType(((XElement)patternNode), value);
                        continue;
                    }
                    var parent = patternNode.Parent;
                    while(parent != null && parent.Name.LocalName != "table-cell")
                    {
                        parent = parent.Parent;
                    }
                    if (parent != null && parent.Name.LocalName == "table-cell")
                    {
                        TryConvertNodeType(parent, value);
                    }
                }
            }
        }

        private void TryConvertNodeType(XElement element, string value)
        {
            decimal decimalValue;
            if (Decimal.TryParse(value, out decimalValue))
            {
                element.SetAttributeValue(XName.Get("value-type", _office), "float");
                element.SetAttributeValue(XName.Get("value", _office), decimalValue);
            }
        }
    }
}
