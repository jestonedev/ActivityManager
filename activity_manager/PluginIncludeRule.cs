using System;
using System.Collections.Generic;
using System.Text;

namespace activity_manager
{
	class PluginIncludeRule
	{
		public string IncludeRule { get; set; }
		public string PluginNameMask { get; set; }
		public PluginIncludeRule(string IncludeRule, string PluginNameMask)
		{
			this.IncludeRule = IncludeRule;
			this.PluginNameMask = PluginNameMask;
		}
	}
}
