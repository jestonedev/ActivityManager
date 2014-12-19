using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using System.Collections.ObjectModel;
using System.Globalization;

namespace AMClasses
{
	public class ActivityStep
	{
		public string PlugName { get; set; }
		public string ActionName { get; set; }
		public int RepeatCount { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }

        private Collection<ActivityStepParameter> inputParameters = new Collection<ActivityStepParameter>();
        private Collection<ActivityStepParameter> outputParameters = new Collection<ActivityStepParameter>();

        public Collection<ActivityStepParameter> InputParameters { get { return inputParameters; } }
        public Collection<ActivityStepParameter> OutputParameters { get { return outputParameters; } }

		public ActivityStep()
		{
			RepeatCount = 1;
		}

		public void AddInputParameter(string name, string value)
		{
			InputParameters.Add(new ActivityStepParameter(name, value));
		}
		public void AddOutputParameter(string name, string value)
		{
			OutputParameters.Add(new ActivityStepParameter(name, value));
		}

        public static ActivityStep ConvertXElementToActivityStep(XElement element, Language lang)
        {
            if (lang == null)
                throw new AMException("Не задана ссылка на язык перевода");
            if (element == null)
                throw new AMException("Не задана ссылка на xml-элемент");
            ActivityStep activity_step = new ActivityStep();
            //Задаем параметр plugin
            XAttribute plugin_name = element.Attribute("plugin");
            if (plugin_name != null)
                activity_step.PlugName = plugin_name.Value;
            //Задаем параметр action
            XAttribute action_name = element.Attribute("action");
            if (action_name == null)
                throw new AMException("[config.xml]" + lang.Translate("Не указан обязательный атрибут \"action\" элемента <step>"));
            activity_step.ActionName = action_name.Value;
            //Задаем параметр repeat_count
            XAttribute repeat = element.Attribute("repeat");
            if (repeat != null)
            {
                int repeat_count;
                if (Int32.TryParse(repeat.Value, out repeat_count))
                    activity_step.RepeatCount = repeat_count;
                else
                    throw new AMException(String.Format(CultureInfo.CurrentCulture,"[config.xml]" + 
                        lang.Translate("Некорректное числовое значение атрибута \"repeat\" = {0}"), repeat.Value));
            }
            //Задаем значение атритубат Label
            XAttribute label = element.Attribute("label");
            if (label != null)
                activity_step.Label = label.Value;
            //Задаем значение атритубат Description
            XAttribute description = element.Attribute("description");
            if (description != null)
                activity_step.Description = description.Value;
            XElement input = element.Element("input");
            if (input != null)
            {
                IEnumerable<XElement> input_parameters = input.Elements("parameter");
                foreach (XElement input_parameter in input_parameters)
                {
                    XAttribute attribute = input_parameter.Attribute("name");
                    if (attribute == null)
                        throw new AMException(
                            String.Format(CultureInfo.CurrentCulture, 
                            "[config.xml]" + lang.Translate("Не указан обязательный атрибут \"name\" элемента <parameter>")));
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
                        throw new AMException(String.Format(CultureInfo.CurrentCulture,"[config.xml]" + lang.Translate("Не указан обязательный атрибут \"name\" элемента <parameter>")));
                    string value = output_parameter.Value;
                    activity_step.AddOutputParameter(attribute.Value, value);
                }
            }
            return activity_step;
        }

        public override string ToString()
        {
            return this.ActionName == null ? "" : this.ActionName;
        }
	}
}
