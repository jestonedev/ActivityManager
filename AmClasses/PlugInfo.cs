using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.IO;
using System.Collections.ObjectModel;

namespace AMClasses
{
    public class PlugInfo
    {
        public string PlugPath { get; set; }
        public string PlugName { get; set; }
        private Collection<PlugActionInfo> plugActions = new Collection<PlugActionInfo>();
        public Collection<PlugActionInfo> PlugActions { get { return plugActions; } }
        private Assembly assembly { get; set; }
        private Type realize_class { get; set; }
        private object _instance;
        internal object instance
        {
            get
            {
                if (_instance == null)
                    _instance = assembly.CreateInstance(realize_class.FullName);
                return _instance;
            }
            set { _instance = value; }
        }

        public PlugInfo(string assemblyPath)
        {
            this.PlugPath = assemblyPath;
            assembly = Assembly.LoadFile(assemblyPath, AppDomain.CurrentDomain.Evidence);
            this.PlugName = assembly.GetName().Name;
            Type IPlug = assembly.GetType(this.PlugName + ".IPlug");
            if ((IPlug == null) || (!IPlug.IsInterface))
                throw new AMException("В сборке не задана ссылка на интерфейс плагина IPlug");
            Type[] types = assembly.GetTypes();
            foreach (Type type in types)
            {
                Type realize_class = type.GetInterface(this.PlugName + ".IPlug");
                if (realize_class != null)
                {
                    this.realize_class = type;
                    break;
                }
            }
            if (this.realize_class == null)
                throw new AMException("В сборке не задан класс, реализующий интерфейс плагина IPlug");
            MethodInfo[] methods = this.realize_class.GetMethods();
            MethodInfo[] IPlugMethods = IPlug.GetMethods();
            foreach (MethodInfo method in methods)
            {
                foreach (MethodInfo IPlugMethod in IPlugMethods)
                {
                    if (IPlugMethod.Name == method.Name)
                    {
                        PlugActionInfo pai = new PlugActionInfo(this, method, method.GetParameters());
                        PlugActions.Add(pai);
                        break;
                    }
                }
            }
        }

        public static bool IsPlug(string assemblyPath)
        {
            try
            {
                Assembly assembly = Assembly.ReflectionOnlyLoadFrom(assemblyPath);
                Type IPlug = assembly.GetType(assembly.GetName().Name + ".IPlug");
                bool is_plug = false;
                if ((IPlug != null) && (IPlug.IsInterface))
                    is_plug = true;
                return is_plug;
            }
            catch (BadImageFormatException)
            {
                return false;
            }
        }

        public string RealizeClassName()
        {
            return this.realize_class.Name;
        }

        public bool HasAction(string actionName, Collection<PlugActionParameter> parameters)
        {
            foreach (PlugActionInfo action in PlugActions)
                if (action.ActionName == actionName)
                {
                    if (action.Parameters.Count != parameters.Count)
                        continue;
                    //Если имена методов и число параметров совпало, то проверяем входные параметры на соответствие имен и направлений
                    bool action_is_equal = true;
                    foreach (PlugActionParameter parameter in action.Parameters)
                    {
                        bool parameter_founded = false;
                        foreach (PlugActionParameter chk_parameter in parameters)
                        {
                            if ((chk_parameter.Name == parameter.Name) && (chk_parameter.Direction == parameter.Direction))
                            {
                                parameter_founded = true;
                                break;
                            }
                        }
                        if (!parameter_founded)
                        {
                            action_is_equal = false;
                            break;
                        }
                    }
                    if (action_is_equal)
                        return true;
                }
            return false;
        }

        public override string ToString()
        {
            return PlugName;
        }
    }
}