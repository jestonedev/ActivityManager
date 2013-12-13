using System;
using System.Collections.Generic;
using System.Text;

namespace am_classes
{
	public class ActivityStepParameter
	{
		public string name { get; set; }
		public string value { get; set; }
		public ActivityStepParameter(string name, string value)
		{
			this.name = name;
			this.value = value;
		}
	}
}
