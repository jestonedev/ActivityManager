using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace report_module
{
    public class WordEditor: ReportEditor
    {
        private Dictionary<string, string> xml_clousers = new Dictionary<string, string>() {
        {"table","tbl"},
        {"row","tr"},
        {"cell","tc"},
        {"p","p"}
        };

        public override List<ReportValue> xml_clousers_convert(List<ReportValue> values)
        {
            return clousers_convert(xml_clousers, values);
        }

        public override void report_editing(string report_path, List<ReportValue> values)
        {
            report_editing_content_file(Path.Combine(report_path, 
                Path.DirectorySeparatorChar+"word"+Path.DirectorySeparatorChar+"document.xml"), values);
        }

        public override void special_tag_editing(string report_unzip_path)
        {
            base.special_tag_editing(report_unzip_path);
        }
    }
}
