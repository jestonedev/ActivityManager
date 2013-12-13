using System;
using System.Collections.Generic;
using System.Text;

namespace am_classes
{
	public class ActivityStep
	{
		public string plugin_name { get; set; }
		public string action_name { get; set; }
		public int repeat_count { get; set; }

		public List<ActivityStepParameter> input_parameters { get; set; }
		public List<ActivityStepParameter> output_parameters { get; set; }

		public ActivityStep()
		{
			input_parameters = new List<ActivityStepParameter>();
			output_parameters = new List<ActivityStepParameter>();
			repeat_count = 1;
		}

		public void AddInputParameter(string name, string value)
		{
			input_parameters.Add(new ActivityStepParameter(name, value));
		}
		public void AddOutputParameter(string name, string value)
		{
			output_parameters.Add(new ActivityStepParameter(name, value));
		}
	}
}
