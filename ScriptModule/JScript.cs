using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jint;
using System.IO;

//Модуль запуска скриптов
namespace JSModule
{
    /// <summary>
    /// Доступный из-вне интерфейс плагина
    /// </summary>
    public interface IPlug
    {
        /// <summary>
        /// Выполнить JavaScript
        /// </summary>
        /// <param name="script">Скрипт на языке JavaScript</param>
        /// <param name="result">Возвращаемое значение</param>
        void JSRun(string script, out object result);
    }

    /// <summary>
    /// Класс, реализующий интерфейс IPlug
    /// </summary>
    public class JSPlug: IPlug
    {
        /// <summary>
        /// Выполнить JavaScript
        /// </summary>
        /// <param name="script">Скрипт на языке JavaScript</param>
        /// <param name="result">Возвращаемое значение</param>
        public void JSRun(string script, out object result)
        {
            string jsExtensionFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "plugins" + Path.DirectorySeparatorChar + "JSModule_extension.js");
            JintEngine jingEngine = new JintEngine(Options.Strict | Options.Ecmascript5);
            if (File.Exists(jsExtensionFile))
            {
                using (StreamReader sr = new StreamReader(jsExtensionFile))
                {
                    jingEngine.Run(sr.ReadToEnd());
                }
            }   
            result = jingEngine.Run(script);
        }
    }
}
