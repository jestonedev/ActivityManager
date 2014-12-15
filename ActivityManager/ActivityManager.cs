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

namespace ActivityManager
{
	class ActivityManager
	{
		//Глобальные параметры
		private static Dictionary<string, object> parameters = new Dictionary<string,object>();
		
		//Конфигурация языкового пакета
		private static Language language = new Language("ru");
		private delegate string language_delegate(string text);
		private static language_delegate _;

        [STAThread()]
		public static void Main(string[] args)
		{
            try
            {
                //инициируем переводчик по умолчанию
                _ = language.Translate;

                for (int i = 0; i < args.Length; i++)
                {
                    string[] arg = args[i].Split(new char[] { '=' }, 2);
                    if (arg.Length != 2)
                        throw new AMException(_("Некорректный формат входной строки параметров"));
                    parameters.Add(arg[0], arg[1]);
                }

                //проверяем наличие обязательного параметра: config
                if (!parameters.ContainsKey("config"))
                    throw new AMException(_("Не передана ссылка на файл конфигурации"));
                string configFile = parameters["config"].ToString();
                parameters.Remove("config");
                AmLibrary.ActivityManager.Run(configFile, parameters);
            }
            catch (AMException e)
            {
                MessageBox.Show(e.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
		}
	}
}
