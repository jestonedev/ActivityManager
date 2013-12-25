using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.IO;

namespace am_classes
{
    public class Plugin
    {
        public string PluginPath { get; set; }
        public string PluginName { get; set; }
        public List<PluginActionInfo> PluginActions { get; set; }
        private Assembly assembly { get; set; }
        private Type realize_class { get; set; }
        object instance = null;

        public Plugin(string AssemblyPath)
        {
            this.PluginPath = AssemblyPath;
            assembly = Assembly.LoadFile(AssemblyPath, AppDomain.CurrentDomain.Evidence);
            this.PluginName = assembly.GetName().Name;
            Type IPlugin = assembly.GetType(this.PluginName + ".IPlugin");
            if ((IPlugin == null) || (!IPlugin.IsInterface))
                throw new ApplicationException("В сборке не задана ссылка на интерфейс плагина IPlugin");
            Type[] types = assembly.GetTypes();
            foreach (Type type in types)
            {
                Type realize_class = type.GetInterface(this.PluginName + ".IPlugin");
                if (realize_class != null)
                {
                    this.realize_class = type;
                    break;
                }
            }
            if (this.realize_class == null)
                throw new ApplicationException("В сборке не задан класс, реализующий интерфейс плагина IPlugin");
            MethodInfo[] methods = this.realize_class.GetMethods();
            PluginActions = new List<PluginActionInfo>();
            MethodInfo[] IPluginMethods = IPlugin.GetMethods();
            foreach (MethodInfo method in methods)
            {
                foreach (MethodInfo IPluginMethod in IPluginMethods)
                {
                    if (IPluginMethod.Name == method.Name)
                    {
                        PluginActionInfo pai = new PluginActionInfo(method, method.GetParameters());
                        PluginActions.Add(pai);
                        break;
                    }
                }
            }
        }

        public static bool IsPlugin(string AssemblyPath)
        {
            try
            {
                Assembly assembly = Assembly.ReflectionOnlyLoadFrom(AssemblyPath);
                Type IPlugin = assembly.GetType(assembly.GetName().Name + ".IPlugin");
                bool is_plugin = false;
                if ((IPlugin != null) && (IPlugin.IsInterface))
                    is_plugin = true;
                return is_plugin;
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

        public void ExecuteAction(string ActionName, object[] input_parameters, out object[] output_parameters)
        {
            foreach (PluginActionInfo action in PluginActions)
            {
                if (action.ActionName == ActionName)
                {
                    if (instance == null)
                        instance = assembly.CreateInstance(realize_class.FullName);
                    action.Execute(instance, input_parameters, out output_parameters);
                    return;
                }
            }
            throw new ApplicationException("Попытка запуск несуществующей задачи");
        }

        public bool HasAction(string ActionName)
        {
            foreach (PluginActionInfo action in PluginActions)
                if (action.ActionName == ActionName)
                    return true;
            return false;
        }

        public override string ToString()
        {
            return PluginName;
        }
    }
}