using System;
using System.Collections.Generic;
using System.Text;

namespace am_classes
{
	public enum ParameterDirection {Input, Output};

	public class PluginActionParameter
	{
		public string Name { get; set; }
		public Type ParameterType { get; set; }
		public ParameterDirection Direction { get; set; }
		public PluginActionParameter(string Name, Type ParameterType, ParameterDirection Direction)
		{
			this.Name = Name;
			this.ParameterType = ParameterType;
			this.Direction = Direction;
		}
	}
}
