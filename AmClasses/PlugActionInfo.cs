using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections.ObjectModel;

namespace AMClasses
{
	public class PlugActionInfo
	{
		public string ActionName { get; set; }
        private Collection<PlugActionParameter> parameters = new Collection<PlugActionParameter>();
        public Collection<PlugActionParameter> Parameters { get { return parameters; } }
		private MethodInfo method_info { get; set; }
        private PlugInfo plugin { get; set; }

		public PlugActionInfo(PlugInfo plugin, MethodInfo mi, ParameterInfo[] pis)
		{
            this.plugin = plugin;
			method_info = mi;
			ActionName = mi.Name;
			foreach (ParameterInfo pi in pis)
			{
				string Name = pi.Name;
				Type ParameterType = pi.ParameterType;
				ParameterDirection pd = ParameterDirection.Input;
				if (pi.IsOut)
					pd = ParameterDirection.Output;
				Parameters.Add(new PlugActionParameter(Name, ParameterType, pd));
			}
		}

		public void Execute(object[] inputParameters, out object[] outputParameters)
		{
			//Выполняем проверку числа параметров
			int input_parameters_count = 0;
			int output_parameters_count = 0;
			foreach (PlugActionParameter parameter in this.Parameters)
			{
				if (parameter.Direction == ParameterDirection.Input)
					input_parameters_count++;
				else
					output_parameters_count++;
			}
			if (inputParameters.Length != input_parameters_count)
                throw new AMException("Число переданных входных параметров неверно");
			object[] exec_parameters = new object[this.Parameters.Count];
			for (int i = 0, j = 0; i < this.Parameters.Count; i++)
			{
				if (this.Parameters[i].Direction == ParameterDirection.Input)
				{
					Type ParameterType = this.Parameters[i].ParameterType;
                    try
                    {
                        if (ParameterType.IsEnum && (inputParameters[j] is string))
                            inputParameters[j] = Enum.Parse(ParameterType, inputParameters[j].ToString(), true);
                        exec_parameters[i] = Convert.ChangeType(inputParameters[j], ParameterType);
                    }
                    catch(Exception e)
                    {
                        throw new AMException(e.Message);
                    }
					j++;
				}
			}	
			method_info.Invoke(plugin.instance, exec_parameters);
			outputParameters = new object[this.Parameters.Count - inputParameters.Length];
			for (int i = 0, j = 0; i < this.Parameters.Count; i++)
			{
				if (this.Parameters[i].Direction == ParameterDirection.Output)
				{
					outputParameters[j] = exec_parameters[i];
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
