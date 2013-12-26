using System;
using System.Collections.Generic;
using System.Text;

namespace AMClasses
{
	public enum ParameterDirection {Input, Output};

	public class PlugActionParameter
	{
		public string Name { get; set; }
		public Type ParameterType { get; set; }
		public ParameterDirection Direction { get; set; }
		public PlugActionParameter(string name, Type parameterType, ParameterDirection direction)
		{
			this.Name = name;
			this.ParameterType = parameterType;
			this.Direction = direction;
		}
	}
}
