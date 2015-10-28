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
		private static Dictionary<string, object> parameters = new Dictionary<string,object>();
        //Экземпляр ActivityManager
        private static AmLibrary.ActivityManager AM;
		//Конфигурация языкового пакета
		private static Language language = new Language("ru");
		private delegate string language_delegate(string text);
		private static language_delegate _;

        [STAThread()]
		public static void Main(string[] args)
		{
            try
            {   //Для тестирования AM
                //args = new string[2];             
                //args[0] = @"config=\\nas\media$\ActivityManager\templates\registry\tenancy\test.xml";
                //args[1] = @"debug=true";

                //инициируем переводчик по умолчанию
                _ = language.Translate;               
                for (int i = 0; i < args.Length; i++)
                {
                    string[] arg = args[i].Split(new char[] { '=' }, 2);
                    if (arg.Length == 1)
                        parameters.Add(arg[0], null);
                    else
                    if (arg.Length == 2)
                        parameters.Add(arg[0], arg[1]);
                    else
                        throw new AMException(_("Некорректный формат входной строки параметров"));
                }

                //проверяем наличие обязательного параметра: config
                if (!parameters.ContainsKey("config"))
                    throw new AMException(_("Не передана ссылка на файл конфигурации"));
                string configFile = parameters["config"].ToString();
                parameters.Remove("config");
                AM = new AmLibrary.ActivityManager(configFile, parameters);
                AM.Run();                                
            }
            catch (AMException e)
            {
                if (parameters.ContainsKey("--nodialog"))
                    Console.WriteLine(e.Message);
                else if(AM != null && AM.Client != null)
                {
                    AM.CommunicationToServer(new MessageForDebug { Exception = e.Message }, false);
                    AM.ClientDispose();
                }
                else
                    MessageBox.Show(e.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception e)
            {
                if (parameters.ContainsKey("--nodialog"))
                    Console.WriteLine(e.Message);
                else if (AM != null && AM.Client != null)
                {
                    AM.CommunicationToServer(new MessageForDebug { Exception = e.Message }, false);
                    AM.ClientDispose();
                }
                else
                    MessageBox.Show("Данное сообщение является следствием ошибки в работе ядра менеджера отчетов. Обратитесь к разработчику. Подробный текст ошибки: "+e.Message, 
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
		}
	}
}
