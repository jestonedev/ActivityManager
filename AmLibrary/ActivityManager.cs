using AMClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace AmLibrary
{
    public class ActivityManager
    {
        //Глобальные параметры (действуют на все шаги задачи)
        private Dictionary<string, object> global_parameters = new Dictionary<string, object>();

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

        private ActivityManager(string configFile, Dictionary<string, object> parameters)
        {
            //инициируем переводчик по умолчанию
            _ = language.Translate;

            global_parameters.Add("config", configFile);
            foreach (var parameter in parameters)
                global_parameters.Add(parameter.Key, parameter.Value);
            
            //проверяем наличие необязательного конфигурационного параметра: lang
            if (global_parameters.ContainsKey("lang"))
            {
                language = new Language(global_parameters["lang"].ToString());
                _ = language.Translate;
            }

            //инициализируем путь до папки с плагинами
            plugins_path = Path.Combine(new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName, "plugins");

            if (!Directory.Exists(plugins_path))
                throw new AMException(String.Format(CultureInfo.CurrentCulture, _("Путь до папки {0} не найден"), plugins_path));

            string config_filename = global_parameters["config"].ToString();
            if (!File.Exists(config_filename))
                throw new AMException(String.Format(CultureInfo.CurrentCulture, _("Файл {0} не найден"), config_filename));
        }

        private void LoadConfigFile()
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

        private void PrepareConfig()
        {
            for (int i = 0; i < activity_steps.Count; i++)
            {
                ActivityStep step = activity_steps[i];
                CheckAndPrepareActions(ref step);
            }
        }

        private void CheckActivityVariablesValues(ActivityStep step)
        {
            PlugActionInfo current_action = PlugActionHelper.FindPlugAction(plugins, step);
            if (current_action == null)
                throw new AMException(
                    String.Format(CultureInfo.CurrentCulture, _("Не удалось найти действие {0} в плагине {1}"), step.ActionName, step.PlugName));
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
                            break;
                        }
                        catch (Exception)
                        {
                            throw new AMException(
                                String.Format(CultureInfo.CurrentCulture, _("Ошибка преобразования значения \"{0}\" параметра {1} к типу {2}, требуемому действием {3} плагина {4}"),
                                action_parameter.Value, action_parameter.Name, plugin_parameter.ParameterType.ToString(), step.ActionName, step.PlugName));
                        }
                    }
                }
                if (!finded)
                    throw new AMException(
                        String.Format(CultureInfo.CurrentCulture, _("В действии {0} плагина {1} не существует переменной с именем {2}"), step.ActionName, step.PlugName, action_parameter.Name));
            }
        }

        private void CheckAndPrepareActions(ref ActivityStep step)
        {
            //Проверка и сопоставление plugin_name и action
            if (step.PlugName == null)
            {
                bool finded = false;
                foreach (PlugInfo plugin in plugins)
                {
                    if (plugin.HasAction(step.ActionName,
                        PlugActionHelper.ConvertActivityStepToPlugParameters(step.InputParameters, step.OutputParameters)))
                    {
                        if (!finded)
                        {
                            step.PlugName = plugin.PlugName;
                            finded = true;
                        }
                        else
                            throw new AMException(String.Format(CultureInfo.CurrentCulture,
                                _("Неоднозначность определения заданного действия. Действие \"{0}\" определено в плагинах {1} и {2}. Необходимо явное указание плагина в файле конфигурации"),
                                step.ActionName, step.PlugName, plugin.PlugName));
                    }
                }
                if (!finded)
                    throw new AMException(String.Format(CultureInfo.CurrentCulture, _("Не удалось найти действие {0} ни в одном из плагинов"),
                        step.ActionName));
            }
            else
                foreach (PlugInfo plugin in plugins)
                {
                    if ((plugin.PlugName == step.PlugName) && (!plugin.HasAction(step.ActionName,
                        PlugActionHelper.ConvertActivityStepToPlugParameters(step.InputParameters, step.OutputParameters))))
                        throw new AMException(String.Format(CultureInfo.CurrentCulture, _("В плагине {0} не определено действие {1}"),
                            plugin.PlugName, step.ActionName));
                }
        }

        private void LoadPlugins()
        {
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
            string[] files = Directory.GetFiles(plugins_path, "*.dll", SearchOption.TopDirectoryOnly);
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

        Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            AssemblyName an = new AssemblyName(args.Name);
            Environment.SetEnvironmentVariable("PATH", Environment.GetEnvironmentVariable("PATH") + ";" + Path.Combine(Environment.CurrentDirectory, "plugins"));
            return Assembly.LoadFile(Path.Combine(Directory.GetCurrentDirectory(), @"plugins\"+an.Name+".dll"));
        }

        private void Execute()
        {
            int step_num = 0;
            for (int j = 0; j < activity_steps.Count; j++)
            {
                step_num++;
                ActivityStep step = activity_steps[j];
                for (int rc = 0; rc < step.RepeatCount; rc++)
                {
                    //Выполняем проверку соответствия значений параметров их типам
                    CheckActivityVariablesValues(step);
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
                        PlugActionHelper.FindPlugAction(plugins, step)
                            .Execute(input_parameters, out output_parameters);
                    }
                    catch (ApplicationException e)
                    {
                        string message = "";
                        if (e.InnerException != null)
                        {
                            if (e.InnerException.GetType().FullName == "IOModule.IfConditionException")
                            {
                                if (e.InnerException.Data.Contains("step"))
                                {
                                    int stepNum = -1;
                                    if (Int32.TryParse(e.InnerException.Data["step"].ToString().Trim(), out stepNum))
                                    {
                                        //Элменты массива нумеруются с 0, но пользователь вводит их с 1. При этом по окончании цикла идет инкремент, который необходимо учитывать
                                        step_num = stepNum - 1;
                                        j = stepNum - 2;
                                    }
                                }
                                else
                                    if (e.InnerException.Data.Contains("label"))
                                    {
                                        string label = e.InnerException.Data["label"].ToString().Trim();
                                        int stepNum = int.MaxValue;
                                        for (int k = 0; k < activity_steps.Count; k++)
                                            if (activity_steps[k].Label != null && activity_steps[k].Label.Trim() == label)
                                            {
                                                stepNum = k;
                                                break;
                                            }
                                        step_num = stepNum;
                                        j = stepNum - 1;
                                    }
                                    else
                                    {
                                        step_num = Int32.MaxValue;
                                        j = Int32.MaxValue-1;
                                        break;
                                    }
                                continue;
                            }
                            message = _(e.InnerException.Message);
                            foreach (string key in e.InnerException.Data.Keys)
                                message = message.Replace(key, e.InnerException.Data[key].ToString());
                        }
                        else
                            message = e.Message;
                        throw new AMException(String.Format(CultureInfo.CurrentCulture, _("[Шаг {0}]") + ": ", step_num) + message);
                    }
                    for (int k = 0; k < output_parameters.Length; k++)
                    {
                        string param_name = "";
                        if (!String.IsNullOrEmpty(activity_steps[j].OutputParameters[k].Value.Trim()))
                            param_name = activity_steps[j].OutputParameters[k].Value;
                        else
                            param_name = activity_steps[j].OutputParameters[k].Name;
                        if (global_parameters.ContainsKey(param_name))
                            global_parameters[param_name] = output_parameters[k];
                        else
                            global_parameters.Add(param_name, output_parameters[k]);
                    }
                }
            }
        }
        public static void Run(string configFile, Dictionary<string, object> parameters)
        {
            string pluginsDir = Path.Combine(new FileInfo(Assembly.GetExecutingAssembly().FullName).DirectoryName, "plugins");
            Environment.SetEnvironmentVariable("PATH", Environment.GetEnvironmentVariable("PATH") + ";" + pluginsDir);
            ActivityManager manager = new ActivityManager(configFile, parameters);
            manager.LoadConfigFile();
            manager.LoadPlugins();
            manager.PrepareConfig();
            manager.Execute();
        }
    }
}
