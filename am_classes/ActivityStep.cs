using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

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

        public static ActivityStep ConvertXElementToActivityStep(XElement element, Language lang)
        {
            ActivityStep activity_step = new ActivityStep();
            //Задаем параметр plugin
            XAttribute plugin_name = element.Attribute("plugin");
            if (plugin_name != null)
                activity_step.plugin_name = plugin_name.Value;
            //Задаем параметр action
            XAttribute action_name = element.Attribute("action");
            if (action_name == null)
                throw new ApplicationException("[config.xml]" + lang.Translate("Не указан обязательный атрибут \"action\" элемента <step>"));
            activity_step.action_name = action_name.Value;
            //Задаем параметр repeat_count
            XAttribute repeat = element.Attribute("repeat");
            if (repeat != null)
            {
                int repeat_count;
                if (Int32.TryParse(repeat.Value, out repeat_count))
                    activity_step.repeat_count = repeat_count;
                else
                    throw new ApplicationException(String.Format("[config.xml]" + lang.Translate("Некорректное числовое значение атрибута \"repeat\" = {0}"), repeat.Value));
            }
            XElement input = element.Element("input");
            if (input != null)
            {
                IEnumerable<XElement> input_parameters = input.Elements("parameter");
                foreach (XElement input_parameter in input_parameters)
                {
                    XAttribute attribute = input_parameter.Attribute("name");
                    if (attribute == null)
                        throw new ApplicationException(String.Format("[config.xml]" + lang.Translate("Не указан обязательный атрибут \"name\" элемента <parameter>")));
                    string value = input_parameter.Value;
                    activity_step.AddInputParameter(attribute.Value, value);
                }
            }
            XElement output = element.Element("output");
            if (output != null)
            {
                IEnumerable<XElement> output_parameters = output.Elements("parameter");
                foreach (XElement output_parameter in output_parameters)
                {
                    XAttribute attribute = output_parameter.Attribute("name");
                    if (attribute == null)
                        throw new ApplicationException(String.Format("[config.xml]" + lang.Translate("Не указан обязательный атрибут \"name\" элемента <parameter>")));
                    string value = output_parameter.Value;
                    activity_step.AddOutputParameter(attribute.Value, value);
                }
            }
            return activity_step;
        }

        public override string ToString()
        {
            return this.action_name == null ? "" : this.action_name;
        }
	}
}
