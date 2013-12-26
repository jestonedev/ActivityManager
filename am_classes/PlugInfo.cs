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
        object instance = null;

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
                        PlugActionInfo pai = new PlugActionInfo(method, method.GetParameters());
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

        public void ExecuteAction(string actionName, object[] inputParameters, out object[] outputParameters)
        {
            foreach (PlugActionInfo action in PlugActions)
            {
                if (action.ActionName == actionName)
                {
                    if (instance == null)
                        instance = assembly.CreateInstance(realize_class.FullName);
                    action.Execute(instance, inputParameters, out outputParameters);
                    return;
                }
            }
            throw new AMException("Попытка запуск несуществующей задачи");
        }

        public bool HasAction(string actionName)
        {
            foreach (PlugActionInfo action in PlugActions)
                if (action.ActionName == actionName)
                    return true;
            return false;
        }

        public override string ToString()
        {
            return PlugName;
        }
    }
}