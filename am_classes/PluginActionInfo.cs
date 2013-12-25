using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace am_classes
{
	public class PluginActionInfo
	{
		public string ActionName { get; set; }
		public List<PluginActionParameter> parameters { get; set; }
		private MethodInfo method_info { get; set; }
		public PluginActionInfo(MethodInfo mi, ParameterInfo[] pis)
		{
			method_info = mi;
			ActionName = mi.Name;
			parameters = new List<PluginActionParameter>();
			foreach (ParameterInfo pi in pis)
			{
				string Name = pi.Name;
				Type ParameterType = pi.ParameterType;
				ParameterDirection pd = ParameterDirection.Input;
				if (pi.IsOut)
					pd = ParameterDirection.Output;
				parameters.Add(new PluginActionParameter(Name, ParameterType, pd));
			}
		}

		public void Execute(object instance, object[] input_parameters, out object[] output_parameters)
		{
			//Выполняем проверку числа параметров
			int input_parameters_count = 0;
			int output_parameters_count = 0;
			foreach (PluginActionParameter parameter in this.parameters)
			{
				if (parameter.Direction == ParameterDirection.Input)
					input_parameters_count++;
				else
					output_parameters_count++;
			}
			if (input_parameters.Length != input_parameters_count)
				throw new ApplicationException("Число переданных входных параметров неверно");
			object[] parameters = new object[this.parameters.Count];
			for (int i = 0, j = 0; i < this.parameters.Count; i++)
			{
				if (this.parameters[i].Direction == ParameterDirection.Input)
				{
					Type ParameterType = this.parameters[i].ParameterType;
                    if (ParameterType.IsEnum && (input_parameters[j] is string))
                        input_parameters[j] = Enum.Parse(ParameterType, input_parameters[j].ToString(),true);
					parameters[i] = Convert.ChangeType(input_parameters[j], ParameterType);
					j++;
				}
			}	
			method_info.Invoke(instance, parameters);
			output_parameters = new object[this.parameters.Count - input_parameters.Length];
			for (int i = 0, j = 0; i < this.parameters.Count; i++)
			{
				if (this.parameters[i].Direction == ParameterDirection.Output)
				{
					output_parameters[j] = parameters[i];
					j++;
				}
			}
		}

        public override string ToString()
        {
            return this.ActionName;
        }
	}
}
