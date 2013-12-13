using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;

namespace report_module
{
    public class SharedXLSXStrings
    {
        public int count { get; set; }
        public int uniqueCount { get; set; }
        public List<XElement> shared_strings { get; set; }

        public SharedXLSXStrings(string file_name)
        {
            XDocument sharedStrings = XDocument.Load(file_name);
            shared_strings = sharedStrings.Root.Elements().ToList<XElement>();
            count = Int32.Parse(sharedStrings.Root.Attribute("count").Value);
            uniqueCount = Int32.Parse(sharedStrings.Root.Attribute("uniqueCount").Value);
        }

        public int Add(string shared_string)
        {
            count++;
            bool is_containt = false;
            XElement ss = XElement.Parse(shared_string, LoadOptions.PreserveWhitespace);
            for (int i = 0; i < shared_strings.Count; i++)
                if (shared_strings[i] == ss)
                {
                    is_containt = true;
                    return i;
                }
            if (!is_containt)
            {
                shared_strings.Add(ss);
                uniqueCount++;
                return shared_strings.Count - 1;
            }
            throw new ApplicationException("Неизвестная ошибка при добавлении строки в sharedStrings.xml");
        }

        public void Save(string file_name)
        {
            XDocument sharedStrings = XDocument.Load(file_name);
            sharedStrings.Root.RemoveNodes();
            sharedStrings.Root.SetAttributeValue("count", count);
            sharedStrings.Root.SetAttributeValue("uniqueCount", uniqueCount);
            foreach (XElement element in shared_strings)
                sharedStrings.Root.Add(element);
            sharedStrings.Save(file_name);
        }
    }

}
