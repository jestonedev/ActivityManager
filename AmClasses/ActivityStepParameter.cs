using System;
using System.Collections.Generic;
using System.Text;

namespace AMClasses
{
	public class ActivityStepParameter
	{
		public string Name { get; set; }
		public string Value { get; set; }
		public ActivityStepParameter(string name, string value)
		{
			this.Name = name;
			this.Value = value;
		}
	}
}
