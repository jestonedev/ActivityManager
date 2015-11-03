using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Windows.Forms;
using AMClasses;
using System.Globalization;
using Newtonsoft.Json;

namespace ActivityManager
{
	class ActivityManager
	{
		//Глобальные параметры
		private static readonly Dictionary<string, object> parameters = new Dictionary<string,object>();
        //Экземпляр ActivityManager
        private static AmLibrary.ActivityManager _am;
		//Конфигурация языкового пакета
		private static readonly Language language = new Language("ru");
		private delegate string LanguageDelegate(string text);
		private static LanguageDelegate _;

        [STAThread()]
		public static void Main(string[] args)
		{
            try
            {
                //инициируем переводчик по умолчанию
                _ = language.Translate;
                for (var i = 0; i < args.Length; i++)
                {
                    var arg = args[i].Split(new[] {'='}, 2);
                    switch (arg.Length)
                    {
                        case 1:
                            parameters.Add(arg[0], null);
                            break;
                        case 2:
                            parameters.Add(arg[0], arg[1]);
                            break;
                        default:
                            throw new AMException(_("Некорректный формат входной строки параметров"));
                    }
                }

                //проверяем наличие обязательного параметра: config
                if (!parameters.ContainsKey("config"))
                    throw new AMException(_("Не передана ссылка на файл конфигурации"));
                var configFile = parameters["config"].ToString();
                parameters.Remove("config");
                _am = new AmLibrary.ActivityManager(configFile, parameters);
                _am.Run();
            }
            catch (Exception e)
            {
                if (parameters.ContainsKey("--nodialog"))
                    Console.WriteLine(e.InnerException != null ? e.InnerException.Message : e.Message);
                else
                    MessageBox.Show(
                        "Данное сообщение является следствием ошибки в работе ядра менеджера отчетов. Обратитесь к разработчику. Подробный текст ошибки: " +
                        (e.InnerException != null ? e.InnerException.Message : e.Message),
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
		}
	}
}
