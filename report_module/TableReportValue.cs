using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using extended_types;

namespace report_module
{
	public class TableReportValue : ReportValue
	{
		public ReportTable table { get; set; }
		public string xml_clouser { get; set; }

		public TableReportValue(ReportTable table, string xml_clouser)
		{
			this.table = table;
			this.xml_clouser = xml_clouser;
		}
	}
}
