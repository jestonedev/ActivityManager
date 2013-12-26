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

namespace ActivityManager
{
	class ActivityManager
	{
		//Глобальные параметры (действуют на все шаги задачи)
		private Dictionary<string, object> global_parameters = new Dictionary<string,object>();
		
		//Шаги задачи
		private List<ActivityStep> activity_steps = new List<ActivityStep>();
		
		//Путь до папки с плагинами и правила включения плагинов
		private string plugins_path = "";
		private List<PlugIncludeRule> plugins_include_rules = new List<PlugIncludeRule>();

		//Плагины
		private List<PlugInfo> plugins = new List<PlugInfo>();
		
		//Конфигурация языкового пакета
		private Language language = new Language("ru");
		private delegate string language_delegate(string text);
		private language_delegate _;

		public ActivityManager(string[] args)
		{
            //инициируем переводчик по умолчанию
			_ = language.Translate;
			//обрабатываем входную строку параметров, сохраняем глобальные параметры и ссылку на файл конфигурации
			for (int i = 0; i < args.Length; i++)
			{
				string[] arg = args[i].Split(new char[]{'='}, 2);
				if (arg.Length != 2)
                    throw new AMException(_("Некорректный формат входной строки параметров"));
				global_parameters.Add(arg[0].ToLower(),arg[1]);
			}

			//проверяем наличие необязательного конфигурационного параметра: lang
			if (global_parameters.ContainsKey("lang"))
			{
				language = new Language(global_parameters["lang"].ToString());
				_ = language.Translate;
			}

			//проверяем наличие необязательного конфигурационного параметра: plugins_path
			if (global_parameters.ContainsKey("plugins_path"))
			{
				plugins_path = global_parameters["plugins_path"].ToString();
			} else
				//инициализируем путь до папки с плагинами по умолчанию
				plugins_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "plugins");
			if (!Directory.Exists(plugins_path))
                throw new AMException(String.Format(_("Путь до папки {0} не найден"), plugins_path));

			//проверяем наличие обязательного параметра: config
			if (!global_parameters.ContainsKey("config"))
                throw new AMException(_("Не передана ссылка на файл конфигурации"));
			string config_filename = global_parameters["config"].ToString();
			if (!File.Exists(config_filename))
                throw new AMException(String.Format(_("Файл {0} не найден"), config_filename));
		}

		public void LoadConfigFile()
		{
			string fileName = global_parameters["config"].ToString();
			XDocument xdoc = XDocument.Load(fileName, LoadOptions.PreserveWhitespace);
			if (xdoc.Root.Name.LocalName != "activity")
                throw new AMException("[config.xml]" + _("Корневой элемент файла конфигурации неизвестен"));
			//Обрабатываем элементы step
			IEnumerable<XElement> elements = xdoc.Root.Elements("step");
			foreach (XElement element in elements)
			{
				activity_steps.Add(ActivityStep.ConvertXElementToActivityStep(element, this.language));
			}
			//Обрабатываем элемент plugins
			XElement xplugins = xdoc.Root.Element("plugins");
			if (xplugins != null)
			{
				XAttribute xplugins_path = xplugins.Attribute("path");
				if ((xplugins_path != null) && (!global_parameters.ContainsKey("plugins_path")))
				{
					this.plugins_path = xplugins_path.Value;
					if (!Directory.Exists(this.plugins_path))
                        throw new AMException(String.Format(_("Путь до папки {0} не найден"), this.plugins_path));
				}
				IEnumerable<XElement> xplugins_include_rules = xplugins.Elements();
				foreach (XElement xplugin_include_rule in xplugins_include_rules)
				{
					switch (xplugin_include_rule.Name.LocalName)
					{
						case "include":
						case "exclude": this.plugins_include_rules.Add(new PlugIncludeRule(
							xplugin_include_rule.Name.LocalName,
							xplugin_include_rule.Value));
							break;
						default:
                            throw new AMException("[config.xml]" + _("Неизвестное правило для фильтрации плагинов"));
					}
				}
			}
			//Обрабатываем элемент language
			XElement xlanguage = xdoc.Root.Element("language");
			if ((xlanguage != null) && (!global_parameters.ContainsKey("lang")))
			{
				this.language = new Language(xlanguage.Value);
				_ = this.language.Translate;
			}
		}

		public void PrepareConfig()
		{
			for (int i = 0; i < activity_steps.Count; i++ )
			{
				ActivityStep step = activity_steps[i];
				CheckAndPrepareActions(ref step);
				CheckActivityVariablesSignature(step);
			}
		}

		public void CheckActivityVariablesSignature(ActivityStep step)
		{
			PlugActionInfo current_action = null;
			foreach (PlugInfo plugin in plugins)
				if (step.PlugName == plugin.PlugName)
					foreach (PlugActionInfo action in plugin.PlugActions)
						if (action.ActionName == step.ActionName)
							current_action = action;
			if (current_action == null)
                throw new AMException(
					String.Format(_("Не удалось найти действие {0} в плагине {1}"), step.ActionName, step.PlugName));
            if ((step.InputParameters.Count + step.OutputParameters.Count) != current_action.Parameters.Count)
                throw new AMException(String.Format(_("Передано неверное число параметров в действие {0} плагина {1}"), step.ActionName, step.PlugName));
			foreach (ActivityStepParameter action_parameter in step.InputParameters)
			{
				bool finded = false;
				foreach (PlugActionParameter plugin_parameter in current_action.Parameters)
				{
					if (action_parameter.Name == plugin_parameter.Name)
					{
						//Нашли параметр, проверяем, чтобы он был корректен
						finded = true;
						if (plugin_parameter.Direction != ParameterDirection.Input)
                            throw new AMException(
								String.Format(_("Параметр {0} действия {1} плагина {2} предполагает возвращение, а не получение значения"), 
								action_parameter.Name, step.ActionName, step.PlugName));
						break;
					}
				}
				if (!finded)
                    throw new AMException(String.Format(_("В действии {0} плагина {1} не существует переменной с именем {2}"), step.ActionName, step.PlugName, action_parameter.Name));
			}
			foreach (ActivityStepParameter action_parameter in step.OutputParameters)
			{
				bool finded = false;
				foreach (PlugActionParameter plugin_parameter in current_action.Parameters)
				{
					if (action_parameter.Name == plugin_parameter.Name)
					{
						//Нашли параметр, проверяем, чтобы он был корректен
						finded = true;
						if (plugin_parameter.Direction != ParameterDirection.Output)
                            throw new AMException(
								String.Format(_("Параметр {0} действия {1} плагина {2} предполагает получение, а не возвращение значения"),
								action_parameter.Name, step.ActionName, step.PlugName));
						break;
					}
				}
				if (!finded)
                    throw new AMException(String.Format(_("В действии {0} плагина {1} не существует переменной с именем {2}"), step.ActionName, step.PlugName, action_parameter.Name));
			}
		}

		public void CheckActivityVariablesValues(ActivityStep step)
		{
			PlugActionInfo current_action = null;
			foreach (PlugInfo plugin in plugins)
				if (step.PlugName == plugin.PlugName)
					foreach (PlugActionInfo action in plugin.PlugActions)
						if (action.ActionName == step.ActionName)
							current_action = action;
			if (current_action == null)
                throw new AMException(
					String.Format(_("Не удалось найти действие {0} в плагине {1}"), step.ActionName, step.PlugName));
			foreach (ActivityStepParameter action_parameter in step.InputParameters)
			{
				bool finded = false;
				foreach (PlugActionParameter plugin_parameter in current_action.Parameters)
				{
					if (action_parameter.Name == plugin_parameter.Name)
					{
						//Нашли параметр значения
						finded = true;
						try
						{
							object value = action_parameter.Value;
							//Заменяем все шаблоны [variable] на значения
							Match match = Regex.Match(value.ToString(), @"\[[\w]*\]");
							while (match.Success)
							{
								string param = match.Value;
								param = param.Trim(new char[] { '[', ']' });
                                if (global_parameters.ContainsKey(param))
                                    if (match.Value.Length != value.ToString().Length)
                                        value = value.ToString().Replace(match.Value, global_parameters[param].ToString());
                                    else
                                        value = global_parameters[param];
								match = match.NextMatch();
                            }
                            //Если ожидается значение перечисления, а передается строка из config-файла, то конвертируем
                            if (plugin_parameter.ParameterType.IsEnum && (value is string))
                                value = Enum.Parse(plugin_parameter.ParameterType, value.ToString(), true);
							//Пробуем конвертировать тип данных
							Convert.ChangeType(value, plugin_parameter.ParameterType);
							break;
						}
						catch(Exception)
						{
                            throw new AMException(String.Format(_("Ошибка преобразования значения \"{0}\" параметра {1} к типу {2}, требуемому действием {3} плагина {4}"),
                                action_parameter.Value, action_parameter.Name, plugin_parameter.ParameterType.ToString(), step.ActionName, step.PlugName));
						}
					}
				}
				if (!finded)
                    throw new AMException(String.Format(_("В действии {0} плагина {1} не существует переменной с именем {2}"), step.ActionName, step.PlugName, action_parameter.Name));
			}
		}

		public void CheckAndPrepareActions(ref ActivityStep step)
		{
			//Проверка и сопоставление plugin_name и action
			if (step.PlugName == null)
			{
				bool finded = false;
				foreach (PlugInfo plugin in plugins)
				{
					if (plugin.HasAction(step.ActionName))
					{
						if (!finded)
						{
							step.PlugName = plugin.PlugName;
							finded = true;
						}
						else
                            throw new AMException(String.Format(
                                _("Неоднозначность определения заданного действия. Действие \"{0}\" определено в плагинах {1} и {2}. Необходимо явное указание плагина в файле конфигурации"),
								step.ActionName, step.PlugName, plugin.PlugName));
					}
				}
				if (!finded)
                    throw new AMException(String.Format(_("Не удалось найти действие {0} ни в одном из плагинов"),
						step.ActionName));
			}
			else
				foreach (PlugInfo plugin in plugins)
					if ((plugin.PlugName == step.PlugName) && (!plugin.HasAction(step.ActionName)))
                        throw new AMException(String.Format(_("В плагине {0} не определено действие {1}"),
							plugin.PlugName, step.ActionName));
		}

		public void LoadPlugins()
		{
			string[] files = Directory.GetFiles(plugins_path,"*.dll", SearchOption.TopDirectoryOnly);
			foreach (string file in files)
			{
				bool include = false;
				FileInfo fi = new FileInfo(file);
				foreach (PlugIncludeRule pir in plugins_include_rules)
				{
					if ((pir.PlugNameMask == "*") || (pir.PlugNameMask == fi.Name))
						include = pir.IncludeRule == "include";
				}
				if (!include)
					continue;
				try
				{
					plugins.Add(new PlugInfo(file));
				}
				catch (ApplicationException e)
				{
                    throw new AMException(_(e.Message));
				}
			}
		}

		public void Execute()
		{
            int step_num = 0;
			foreach (ActivityStep step in activity_steps)
			{
                step_num++;
				CheckActivityVariablesValues(step);
				//Получаем ссылку на плагин
				PlugInfo current_plugin = null;
				foreach (PlugInfo plugin in plugins)
					if (step.PlugName == plugin.PlugName)
						current_plugin = plugin;
				//Получаем список входных параметров
				object[] input_parameters = new object[step.InputParameters.Count];
				for (int i = 0; i < input_parameters.Length; i++)
                {
                    object value = step.InputParameters[i].Value;
                    Match match = Regex.Match(value.ToString(), @"\[[\w]*\]");
                    while (match.Success)
                    {
                        string param = match.Value;
                        param = param.Trim(new char[] { '[', ']' });
                        if (global_parameters.ContainsKey(param)) 
                            if (match.Value.Length != value.ToString().Length)
                                value = value.ToString().Replace(match.Value, global_parameters[param].ToString());
                            else
                                value = global_parameters[param];
                        match = match.NextMatch();
                    }
					input_parameters[i] = value;
				}
				object[] output_parameters = null;
				try
				{
                    for (int i = 0; i < step.RepeatCount; i++)
                        current_plugin.ExecuteAction(step.ActionName, input_parameters, out output_parameters);
				}
				catch (ApplicationException e)
				{
                    string message = _(e.InnerException.Message);
                    foreach (string key in e.InnerException.Data.Keys)
                        message = message.Replace(key, e.InnerException.Data[key].ToString());
                    throw new AMException(String.Format(_("[Шаг {0}]") + ": ", step_num) + message);
				}
				for (int i = 0; i < output_parameters.Length; i++)
				{
                    string param_name = "";
                    if (!String.IsNullOrEmpty(step.OutputParameters[i].Value.Trim()))
                        param_name = step.OutputParameters[i].Value;
                    else
                        param_name = step.OutputParameters[i].Name;
                    if (global_parameters.ContainsKey(param_name))
                        global_parameters[param_name] = output_parameters[i];
                    else
    					global_parameters.Add(param_name, output_parameters[i]); 
				}
			}
		}

        [STAThread()]
		public static void Main(string[] args)
		{
            try
            {
                ActivityManager manager = new ActivityManager(args);
                manager.LoadConfigFile();
                manager.LoadPlugins();
                manager.PrepareConfig();
                manager.Execute();
            }
            catch (AMException e)
            {
                MessageBox.Show(e.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
		}
	}
}
