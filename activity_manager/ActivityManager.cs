using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Windows.Forms;
using am_classes;

namespace activity_manager
{
	class ActivityManager
	{
		//Глобальные параметры (действуют на все шаги задачи)
		private Dictionary<string, object> global_parameters = new Dictionary<string,object>();
		
		//Шаги задачи
		private List<ActivityStep> activity_steps = new List<ActivityStep>();
		
		//Путь до папки с плагинами и правила включения плагинов
		private string plugins_path = "";
		private List<PluginIncludeRule> plugins_include_rules = new List<PluginIncludeRule>();

		//Плагины
		private List<Plugin> plugins = new List<Plugin>();
		
		//Конфигурация языкового пакета
		private Language language = new Language("ru");
		private delegate string lanuage_delegate(string text);
		private lanuage_delegate _;

		public ActivityManager(string[] args)
		{
            //инициируем переводчик по умолчанию
			_ = language.Translate;
			//обрабатываем входную строку параметров, сохраняем глобальные параметры и ссылку на файл конфигурации
			for (int i = 0; i < args.Length; i++)
			{
				string[] arg = args[i].Split(new char[]{'='}, 2);
				if (arg.Length != 2)
					throw new ApplicationException(_("Некорректный формат входной строки параметров"));
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
				throw new ApplicationException(String.Format(_("Путь до папки {0} не найден"), plugins_path));

			//проверяем наличие обязательного параметра: config
			if (!global_parameters.ContainsKey("config"))
				throw new ApplicationException(_("Не передана ссылка на файл конфигурации"));
			string config_filename = global_parameters["config"].ToString();
			if (!File.Exists(config_filename))
				throw new ApplicationException(String.Format(_("Файл {0} не найден"), config_filename));
		}

		public void LoadConfigFile()
		{
			string fileName = global_parameters["config"].ToString();
			XDocument xdoc = XDocument.Load(fileName, LoadOptions.PreserveWhitespace);
			if (xdoc.Root.Name.LocalName != "activity")
				throw new ApplicationException("[config.xml]" + _("Корневой элемент файла конфигурации неизвестен"));
			//Обрабатываем элементы step
			IEnumerable<XElement> elements = xdoc.Root.Elements("step");
			foreach (XElement element in elements)
			{
				activity_steps.Add(ConvertXElementToActivityStep(element));
			}
			//Обрабатываем элемент plugins
			XElement plugins = xdoc.Root.Element("plugins");
			if (plugins != null)
			{
				XAttribute plugins_path = plugins.Attribute("path");
				if ((plugins_path != null) && (!global_parameters.ContainsKey("plugins_path")))
				{
					this.plugins_path = plugins_path.Value;
					if (!Directory.Exists(this.plugins_path))
						throw new ApplicationException(String.Format(_("Путь до папки {0} не найден"), this.plugins_path));
				}
				IEnumerable<XElement> plugins_include_rules = plugins.Elements();
				foreach (XElement plugin_include_rule in plugins_include_rules)
				{
					switch (plugin_include_rule.Name.LocalName)
					{
						case "include":
						case "exclude": this.plugins_include_rules.Add(new PluginIncludeRule(
							plugin_include_rule.Name.LocalName,
							plugin_include_rule.Value));
							break;
						default:
							throw new ApplicationException("[config.xml]" + _("Неизвестное правило для фильтрации плагинов"));
					}
				}
			}
			//Обрабатываем элемент language
			XElement language = xdoc.Root.Element("language");
			if ((language != null) && (!global_parameters.ContainsKey("lang")))
			{
				this.language = new Language(language.Value);
				_ = this.language.Translate;
			}
		}

		public ActivityStep ConvertXElementToActivityStep(XElement element)
		{
			ActivityStep activity_step = new ActivityStep();
			//Задаем параметр plugin
			XAttribute plugin_name = element.Attribute("plugin");
			if (plugin_name != null)
				activity_step.plugin_name = plugin_name.Value;
			//Задаем параметр action
			XAttribute action_name = element.Attribute("action");
			if (action_name == null)
				throw new ApplicationException("[config.xml]" + _("Не указан обязательный атрибут \"action\" элемента <step>"));
			activity_step.action_name = action_name.Value;
			//Задаем параметр repeat_count
			XAttribute repeat = element.Attribute("repeat");
			if (repeat != null)
			{
				int repeat_count; 
				if (Int32.TryParse(repeat.Value, out repeat_count))
					activity_step.repeat_count = repeat_count;
				else
					throw new ApplicationException(String.Format("[config.xml]" + _("Некорректное числовое значение атрибута \"repeat\" = {0}"), repeat.Value));
			}
			XElement input = element.Element("input");
            if (input != null)
            {
                IEnumerable<XElement> input_parameters = input.Elements("parameter");
                foreach (XElement input_parameter in input_parameters)
                {
                    XAttribute attribute = input_parameter.Attribute("name");
                    if (attribute == null)
                        throw new ApplicationException(String.Format("[config.xml]" + _("Не указан обязательный атрибут \"name\" элемента <parameter>")));
                    string value = input_parameter.Value;
                    activity_step.AddInputParameter(attribute.Value, value);
                }
            }
			XElement output = element.Element("output");
            if (output != null)
            {
                IEnumerable<XElement> output_parameters = output.Elements("parameter");
                foreach (XElement output_parameter in output_parameters)
                {
                    XAttribute attribute = output_parameter.Attribute("name");
                    if (attribute == null)
                        throw new ApplicationException(String.Format("[config.xml]" + _("Не указан обязательный атрибут \"name\" элемента <parameter>")));
                    string value = output_parameter.Value;
                    activity_step.AddOutputParameter(attribute.Value, value);
                }
            }
			return activity_step;
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
			PluginActionInfo current_action = null;
			foreach (Plugin plugin in plugins)
				if (step.plugin_name == plugin.PluginName)
					foreach (PluginActionInfo action in plugin.PluginActions)
						if (action.ActionName == step.action_name)
							current_action = action;
			if (current_action == null)
				throw new ApplicationException(
					String.Format(_("Не удалось найти действие {0} в плагине {1}"), step.action_name, step.plugin_name));
            if ((step.input_parameters.Count + step.output_parameters.Count) != current_action.parameters.Count)
                throw new ApplicationException(String.Format(_("Передано неверное число параметров в действие {0} плагина {1}"), step.action_name, step.plugin_name));
			foreach (ActivityStepParameter action_parameter in step.input_parameters)
			{
				bool finded = false;
				foreach (PluginActionParameter plugin_parameter in current_action.parameters)
				{
					if (action_parameter.name == plugin_parameter.Name)
					{
						//Нашли параметр, проверяем, чтобы он был корректен
						finded = true;
						if (plugin_parameter.Direction != ParameterDirection.Input)
							throw new ApplicationException(
								String.Format(_("Параметр {0} действия {1} плагина {2} предполагает возвращение, а не получение значения"), 
								action_parameter.name, step.action_name, step.plugin_name));
						break;
					}
				}
				if (!finded)
					throw new ApplicationException(String.Format(_("В действии {0} плагина {1} не существует переменной с именем {2}"),step.action_name, step.plugin_name, action_parameter.name));
			}
			foreach (ActivityStepParameter action_parameter in step.output_parameters)
			{
				bool finded = false;
				foreach (PluginActionParameter plugin_parameter in current_action.parameters)
				{
					if (action_parameter.name == plugin_parameter.Name)
					{
						//Нашли параметр, проверяем, чтобы он был корректен
						finded = true;
						if (plugin_parameter.Direction != ParameterDirection.Output)
							throw new ApplicationException(
								String.Format(_("Параметр {0} действия {1} плагина {2} предполагает получение, а не возвращение значения"),
								action_parameter.name, step.action_name, step.plugin_name));
						break;
					}
				}
				if (!finded)
					throw new ApplicationException(String.Format(_("В действии {0} плагина {1} не существует переменной с именем {2}"), step.action_name, step.plugin_name, action_parameter.name));
			}
		}

		public void CheckActivityVariablesValues(ActivityStep step)
		{
			PluginActionInfo current_action = null;
			foreach (Plugin plugin in plugins)
				if (step.plugin_name == plugin.PluginName)
					foreach (PluginActionInfo action in plugin.PluginActions)
						if (action.ActionName == step.action_name)
							current_action = action;
			if (current_action == null)
				throw new ApplicationException(
					String.Format(_("Не удалось найти действие {0} в плагине {1}"), step.action_name, step.plugin_name));
			foreach (ActivityStepParameter action_parameter in step.input_parameters)
			{
				bool finded = false;
				foreach (PluginActionParameter plugin_parameter in current_action.parameters)
				{
					if (action_parameter.name == plugin_parameter.Name)
					{
						//Нашли параметр значения
						finded = true;
						try
						{
							object value = action_parameter.value;
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
						catch(InvalidCastException)
						{
							throw new ApplicationException(String.Format(_("Ошибка преобразования значения \"{0}\" параметра {1} к типу {2}, требуемому действием {3} плагина {4}"),
                                action_parameter.value, action_parameter.name, plugin_parameter.ParameterType.ToString(), step.action_name, step.plugin_name));
						}
					}
				}
				if (!finded)
					throw new ApplicationException(String.Format(_("В действии {0} плагина {1} не существует переменной с именем {2}"), step.action_name, step.plugin_name, action_parameter.name));
			}
		}

		public void CheckAndPrepareActions(ref ActivityStep step)
		{
			//Проверка и сопоставление plugin_name и action
			if (step.plugin_name == null)
			{
				bool finded = false;
				foreach (Plugin plugin in plugins)
				{
					if (plugin.HasAction(step.action_name))
					{
						if (!finded)
						{
							step.plugin_name = plugin.PluginName;
							finded = true;
						}
						else
							throw new ApplicationException(String.Format(
                                _("Неоднозначность определения заданного действия. Действие \"{0}\" определено в плагинах {1} и {2}. Необходимо явное указание плагина в файле конфигурации"),
								step.action_name, step.plugin_name, plugin.PluginName));
					}
				}
				if (!finded)
					throw new ApplicationException(String.Format(_("Не удалось найти действие {0} ни в одном из плагинов"),
						step.action_name));
			}
			else
				foreach (Plugin plugin in plugins)
					if ((plugin.PluginName == step.plugin_name) && (!plugin.HasAction(step.action_name)))
						throw new ApplicationException(String.Format(_("В плагине {0} не определено действие {1}"),
							plugin.PluginName, step.action_name));
		}

		public void LoadPlugins()
		{
			string[] files = Directory.GetFiles(plugins_path,"*.dll", SearchOption.TopDirectoryOnly);
			foreach (string file in files)
			{
				bool include = true;
				FileInfo fi = new FileInfo(file);
				foreach (PluginIncludeRule pir in plugins_include_rules)
				{
					if ((pir.PluginNameMask == "*") || (pir.PluginNameMask == fi.Name))
						include = pir.IncludeRule == "include";
				}
				if (!include)
					continue;
				try
				{
					plugins.Add(new Plugin(file));
				}
				catch (ApplicationException e)
				{
					throw new ApplicationException(_(e.Message));
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
				Plugin current_plugin = null;
				foreach (Plugin plugin in plugins)
					if (step.plugin_name == plugin.PluginName)
						current_plugin = plugin;
				//Получаем список входных параметров
				object[] input_parameters = new object[step.input_parameters.Count];
				for (int i = 0; i < input_parameters.Length; i++)
                {
                    object value = step.input_parameters[i].value;
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
                    for (int i = 0; i < step.repeat_count; i++)
                        current_plugin.ExecuteAction(step.action_name, input_parameters, out output_parameters);
				}
				catch (ApplicationException e)
				{
                    string message = _(e.InnerException.Message);
                    foreach (string key in e.InnerException.Data.Keys)
                        message = message.Replace(key, e.InnerException.Data[key].ToString());
                    throw new ApplicationException(String.Format(_("[Шаг {0}]")+": ", step_num) + message);
				}
				for (int i = 0; i < output_parameters.Length; i++)
				{
                    string param_name = "";
                    if (step.output_parameters[i].value.Trim() != "")
                        param_name = step.output_parameters[i].value;
                    else
                        param_name = step.output_parameters[i].name;
                    if (global_parameters.ContainsKey(param_name))
                        global_parameters[param_name] = output_parameters[i];
                    else
    					global_parameters.Add(param_name, output_parameters[i]); 
				}
			}
		}

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
            catch(Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
		}
	}
}
