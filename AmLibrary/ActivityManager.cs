using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Linq;
using AMClasses;

namespace AmLibrary
{
    public class ActivityManager
    {
        // Отладчик
        private AmDebugger _debugger;

        //Глобальные параметры (действуют на все шаги задачи)
        private readonly Dictionary<string, object> _globalParameters = new Dictionary<string, object>();

        //Шаги задачи
        private readonly List<ActivityStep> _activitySteps = new List<ActivityStep>();

        //Путь до папки с плагинами и правила включения плагинов
        private readonly string _pluginsPath;

        private readonly List<PlugIncludeRule> _pluginsIncludeRules = new List<PlugIncludeRule>();

        //Плагины
        private readonly List<PlugInfo> _plugins = new List<PlugInfo>();

        //Конфигурация языкового пакета
        private Language _language = new Language("ru");
        private delegate string LanguageDelegate(string text);
        private LanguageDelegate _;

        public ActivityManager(string configFile, Dictionary<string, object> parameters)
        {
            //инициируем переводчик по умолчанию
            _ = _language.Translate;

            _globalParameters.Add("config", configFile);
            foreach (var parameter in parameters)
            {
                _globalParameters.Add(parameter.Key, parameter.Value);
                Console.WriteLine(parameter.Key + ':' + parameter.Value);
            }


            //проверяем наличие необязательного конфигурационного параметра: lang
            if (_globalParameters.ContainsKey("lang"))
            {
                _language = new Language(_globalParameters["lang"].ToString());
                _ = _language.Translate;
            }
            if (_globalParameters.ContainsKey("debug"))
            {
                bool debug;
                bool.TryParse(_globalParameters["debug"].ToString(), out debug);
                _debugger = debug ? new AmDebugger() : new AmMokeDebugger();
            }
            else
                _debugger = new AmMokeDebugger();
            //инициализируем путь до папки с плагинами
            var amPath = new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName;
            if (amPath == null)
                throw new AMException(_("Неизвестный путь запуска сборки"));
            _pluginsPath = Path.Combine(amPath, "plugins");

            if (!Directory.Exists(_pluginsPath))
                throw new AMException(string.Format(CultureInfo.CurrentCulture, _("Путь до папки {0} не найден"), _pluginsPath));

            var configFilename = _globalParameters["config"].ToString();
            if (!File.Exists(configFilename))
                throw new AMException(String.Format(CultureInfo.CurrentCulture, _("Файл {0} не найден"), configFilename));
        }

        private void LoadConfigFile()
        {
            var fileName = _globalParameters["config"].ToString();
            var xdoc = XDocument.Load(fileName, LoadOptions.PreserveWhitespace);
            if (xdoc.Root == null)
                throw new AMException("[config.xml]" + _("Отсутствует корневой элемент файла конфигурации"));
            if (xdoc.Root.Name.LocalName != "activity")
                throw new AMException("[config.xml]" + _("Корневой элемент файла конфигурации неизвестен"));
            //Обрабатываем элементы step
            var elements = xdoc.Root.Elements("step");
            foreach (var element in elements)
            {
                _activitySteps.Add(ActivityStep.ConvertXElementToActivityStep(element, _language));
            }
            //Обрабатываем элемент plugins
            var xplugins = xdoc.Root.Element("plugins");
            if (xplugins != null)
            {
                var xpluginsIncludeRules = xplugins.Elements();
                foreach (var xpluginIncludeRule in xpluginsIncludeRules)
                {
                    switch (xpluginIncludeRule.Name.LocalName)
                    {
                        case "include":
                        case "exclude": _pluginsIncludeRules.Add(new PlugIncludeRule(
                            xpluginIncludeRule.Name.LocalName,
                            xpluginIncludeRule.Value));
                            break;
                        default:
                            throw new AMException("[config.xml]" + _("Неизвестное правило для фильтрации плагинов"));
                    }
                }
            }
            //Обрабатываем элемент language
            var xlanguage = xdoc.Root.Element("language");
            if ((xlanguage == null) || (_globalParameters.ContainsKey("lang"))) return;
            _language = new Language(xlanguage.Value);
            _ = _language.Translate;
        }

        private void PrepareConfig()
        {
            for (var i = 0; i < _activitySteps.Count; i++)
            {
                var step = _activitySteps[i];
                CheckAndPrepareActions(ref step);
            }
        }

        private void CheckActivityVariablesValues(ActivityStep step)
        {
            var currentAction = PlugActionHelper.FindPlugAction(_plugins, step);
            if (currentAction == null)
                throw new AMException(
                    string.Format(CultureInfo.CurrentCulture, _("Не удалось найти действие {0} в плагине {1}"), step.ActionName, step.PlugName));
            foreach (var actionParameter in step.InputParameters)
            {
                var finded = false;
                foreach (var pluginParameter in currentAction.Parameters)
                {
                    if (actionParameter.Name != pluginParameter.Name) continue;
                    //Нашли параметр значения
                    finded = true;
                    try
                    {
                        object value = actionParameter.Value;
                        //Заменяем все шаблоны [variable] на значения
                        var match = Regex.Match(value.ToString(), @"\[[\w]*\]");
                        while (match.Success)
                        {
                            var param = match.Value;
                            param = param.Trim('[', ']');
                            if (_globalParameters.ContainsKey(param))
                                value = match.Value.Length != value.ToString().Length ? 
                                    value.ToString().Replace(match.Value, _globalParameters[param].ToString()) : _globalParameters[param];
                            match = match.NextMatch();
                        }
                        break;
                    }
                    catch (Exception)
                    {
                        throw new AMException(
                            string.Format(CultureInfo.CurrentCulture, _("Ошибка преобразования значения \"{0}\" параметра {1} к типу {2}, требуемому действием {3} плагина {4}"),
                                actionParameter.Value, actionParameter.Name, pluginParameter.ParameterType, step.ActionName, step.PlugName));
                    }
                }
                if (!finded)
                    throw new AMException(
                        string.Format(CultureInfo.CurrentCulture, _("В действии {0} плагина {1} не существует переменной с именем {2}"), step.ActionName, step.PlugName, actionParameter.Name));
            }
        }

        private void CheckAndPrepareActions(ref ActivityStep step)
        {
            //Проверка и сопоставление plugin_name и action
            if (step.PlugName == null)
            {
                bool finded = false;
                foreach (PlugInfo plugin in _plugins)
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
                foreach (PlugInfo plugin in _plugins)
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
            string[] files = Directory.GetFiles(_pluginsPath, "*.dll", SearchOption.TopDirectoryOnly);
            foreach (string file in files)
            {
                bool include = false;
                FileInfo fi = new FileInfo(file);
                foreach (PlugIncludeRule pir in _pluginsIncludeRules)
                {
                    if ((pir.PlugNameMask == "*") || (pir.PlugNameMask == fi.Name))
                        include = pir.IncludeRule == "include";
                }
                if (!include)
                    continue;
                try
                {
                    _plugins.Add(new PlugInfo(file));
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
            return Assembly.LoadFile(Path.Combine(Directory.GetCurrentDirectory(), @"plugins\" + an.Name + ".dll"));
        }

        private void Execute()
        {
            var stepNumber = 0;
            var stepByStep = true;
            for (var j = 0; j < _activitySteps.Count; j++)
            {
                //step_num++;
                var step = _activitySteps[j];
                for (var rc = 0; rc < step.RepeatCount; rc++)
                {
                    //Выполняем проверку соответствия значений параметров их типам
                    CheckActivityVariablesValues(step);
                    //Получаем список входных параметров
                    var inputParameters = new object[step.InputParameters.Count];
                    for (var i = 0; i < inputParameters.Length; i++)
                    {
                        object value = step.InputParameters[i].Value;
                        var match = Regex.Match(value.ToString(), @"\[[\w]*\]");
                        while (match.Success)
                        {
                            var param = match.Value;
                            param = param.Trim('[', ']');
                            if (_globalParameters.ContainsKey(param))
                                value = match.Value.Length != value.ToString().Length ? 
                                    value.ToString().Replace(match.Value, _globalParameters[param].ToString()) : 
                                    _globalParameters[param];
                            match = match.NextMatch();
                        }
                        inputParameters[i] = value;
                    }
                    object[] outputParameters;
                    try
                    {
                        if (stepByStep)
                        {
                            var msg = _debugger.RecieveMessage();
                            if (msg.ContainsKey("command"))
                            {
                                switch (msg["command"])
                                {
                                    case "run":
                                        stepByStep = false;
                                        _debugger = new AmMokeDebugger();
                                        break;
                                    case "stop":
                                        return;
                                }
                            }

                        }
                        PlugActionHelper.FindPlugAction(_plugins, step)
                            .Execute(inputParameters, out outputParameters);
                        if ((j+1) < _activitySteps.Count)
                            _debugger.SendMessage(new MessageForDebug { { "step", (j + 1).ToString() } });
                    }
                    catch (ApplicationException e)
                    {
                        string message;
                        if (e.InnerException != null)
                        {
                            if (e.InnerException.GetType().FullName == "IOModule.IfConditionException")
                            {
                                if (e.InnerException.Data.Contains("step"))
                                {
                                    int stepNum;
                                    if (int.TryParse(e.InnerException.Data["step"].ToString().Trim(), out stepNum))
                                    {
                                        //Элменты массива нумеруются с 0, но пользователь вводит их с 1. При этом по окончании цикла идет инкремент, который необходимо учитывать
                                        stepNumber = stepNum - 1;
                                        j = stepNum - 2;
                                    }
                                }
                                else
                                    if (e.InnerException.Data.Contains("label"))
                                    {
                                        var label = e.InnerException.Data["label"].ToString().Trim();
                                        var stepNum = int.MaxValue;
                                        for (var k = 0; k < _activitySteps.Count; k++)
                                            if (_activitySteps[k].Label != null && _activitySteps[k].Label.Trim() == label)
                                            {
                                                stepNum = k;
                                                break;
                                            }
                                        stepNumber = stepNum;
                                        j = stepNum - 1;
                                    }
                                    else
                                    {
                                        stepNumber = int.MaxValue;
                                        j = int.MaxValue - 1;
                                        break;
                                    }
                                _debugger.SendMessage(new MessageForDebug { { "step", (j + 1).ToString() } });    
                                continue;
                            }
                            message = _(e.InnerException.Message);
                            foreach (string key in e.InnerException.Data.Keys)
                                message = message.Replace(key, e.InnerException.Data[key].ToString());
                        }
                        else
                            message = e.Message;
                        var exceptionMsg =
                            string.Format(CultureInfo.CurrentCulture, _("[Шаг {0}]") + ": ", j+1) + message;
                        _debugger.SendMessage(new MessageForDebug { { "exception", exceptionMsg } });
                        if (_globalParameters.ContainsKey("--nodialog") && stepByStep)
                            Console.WriteLine(exceptionMsg);
                        else
                            MessageBox.Show(exceptionMsg, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    for (var k = 0; k < outputParameters.Length; k++)
                    {
                        var paramName = !string.IsNullOrEmpty(_activitySteps[j].OutputParameters[k].Value.Trim()) ? 
                            _activitySteps[j].OutputParameters[k].Value : _activitySteps[j].OutputParameters[k].Name;
                        if (_globalParameters.ContainsKey(paramName))
                            _globalParameters[paramName] = outputParameters[k];
                        else
                            _globalParameters.Add(paramName, outputParameters[k]);
                    }     
                }
            }
        }

        public void Run()
        {
            var amDir = new FileInfo(Assembly.GetExecutingAssembly().FullName).DirectoryName;
            if (amDir == null)
                return;
            var pluginsDir = Path.Combine(amDir, "plugins");
            Environment.SetEnvironmentVariable("PATH", Environment.GetEnvironmentVariable("PATH") + ";" + pluginsDir);
            LoadConfigFile();
            LoadPlugins();
            PrepareConfig();
            var ipAddress = "127.0.0.1";
            if (_globalParameters.ContainsKey("debug_ip_address"))
                ipAddress = _globalParameters["debug_ip_address"].ToString();
            ushort port = 8888;
            if (_globalParameters.ContainsKey("debug_port"))
                ushort.TryParse(_globalParameters["debug_port"].ToString(), out port);           
            _debugger.Start(ipAddress, port);
            Execute();
            _debugger.Stop();  
        }
    }
}
