using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Text.RegularExpressions;

namespace report_module
{
	public class StringReportValue: ReportValue
	{
		public string pattern { get; set; }
		public string value { get; set; }
		
		public StringReportValue(string pattern, string value)
		{
			this.pattern = "$"+pattern+"$";
			this.value = value;
		}
	}
}
