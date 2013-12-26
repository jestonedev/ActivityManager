using System;
using System.Collections.Generic;
using System.Text;

namespace AMClasses
{
	public class PlugIncludeRule
	{
		public string IncludeRule { get; set; }
		public string PlugNameMask { get; set; }
		public PlugIncludeRule(string includeRule, string plugNameMask)
		{
			this.IncludeRule = includeRule;
			this.PlugNameMask = plugNameMask;
		}
	}
}
