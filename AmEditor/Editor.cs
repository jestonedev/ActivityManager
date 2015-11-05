using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;
using AMClasses;
using Newtonsoft.Json;

namespace AmEditor
{
    internal partial class Editor : Form
    {
        //флаг отладки
        private bool _debug;
        //сокет-сервер
        private TcpListener _server;
        //сокет-клент
        private TcpClient _client;
        //поток сервера
        private NetworkStream _stream;
        //номер порта сервера
        private int _port;
        //процесс для AM.exe
        private Process _process;
        //Параметры командной строки
        private Dictionary<string, string> _commandLineParams = new Dictionary<string, string>();

        //Сохраненные настройки программы
        private readonly SettingsStorage _settingsStorage;
        
        //Текущий файл конфигурации
        private string _currentFileName = "";
        private DateTime _currentFileOpenDatetime;

        //Шаги задачи
        private readonly List<ActivityStep> _activitySteps = new List<ActivityStep>();

        //Путь до папки с плагинами и правила включения плагинов
        private readonly string _pluginsPath;
        private List<PlugIncludeRule> _pluginsIncludeRules = new List<PlugIncludeRule>();

        //Перечень игнорируемых библиотек
        readonly List<string> _pluginsIgnoreList = new List<string>();

        //Плагины и документация по ним
        private readonly List<PlugInfo> _plugins = new List<PlugInfo>();
        private readonly Dictionary<string, XDocument> _pluginsDoc = new Dictionary<string, XDocument>();

        //Конфигурация языкового пакета
        private Language _configLanguage = new Language("ru");
        private Language _language = new Language("ru");
        private delegate string LanguageDelegate(string text);
        private LanguageDelegate _;

        //В ручную ли изменяется состояние формы?
        private bool _isManualChangeState;

        private bool _canDrag;
        //Состояние формы
        private enum FormState { Display, Edit }
        private FormState _formState;

        private int _rowIndexFromMouseDown;
        private DataGridViewRow _rw;

        private readonly List<Type> _comboBoxValueTypes = new List<Type>() { typeof(short), typeof(int), typeof(long), 
            typeof(ushort), typeof(uint), typeof(ulong), 
            typeof(decimal), typeof(float), typeof(double), typeof(bool) };

        public Editor(string fileName)
        {            
            InitializeComponent();

            //Загружаем настройки, если файл настроек существует либо инициализируем настройки по умолчанию
            _settingsStorage = SettingsStorage.LoadSettings();
            if (_settingsStorage != null)
            {
                _language = new Language(_settingsStorage.InterfaceLanguagePrefix);
            }
            else
                _settingsStorage = new SettingsStorage();
            //инициируем переводчик по умолчанию
            _ = _language.Translate;

            //Задаем путь до папки с плагинами по умолчанию
            var directoryName = new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName;
            if (directoryName == null) throw new ApplicationException("Неизвестный путь до директории сборки");
            _pluginsPath = Path.Combine(directoryName, "plugins");
            if (!Directory.Exists(_pluginsPath))
            {
                MessageBox.Show(string.Format(CultureInfo.CurrentCulture, _("Путь до папки {0} не найден"), _pluginsPath), _("Ошибка"),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                return;
            }
            //Загружаем список файлов в папке plugins, которые заведомо не являются плагинами
            LoadPluginsIgnoreList();
            //Настраиваем состояине формы
            InterfaceLanguageReload();
            _formState = FormState.Display;
            FormClosing += Editor_FormClosing;
            if (!string.IsNullOrEmpty(fileName))
                OpenConfig(fileName);
        }

        private void LoadPlugins()
        {
            _plugins.Clear();
            _pluginsDoc.Clear();
            var files = Directory.GetFiles(_pluginsPath, "*.dll", SearchOption.TopDirectoryOnly);
            foreach (var file in files)
            {
                var include = false;
                var fi = new FileInfo(file);
                if (_pluginsIgnoreList.Contains(fi.Name))
                    continue;
                foreach (var pir in _pluginsIncludeRules)
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
                catch (AMException e)
                {
                    throw new AMException(_(e.Message));
                }
            }
            var filesDoc = Directory.GetFiles(_pluginsPath, "*.xml", SearchOption.TopDirectoryOnly);
            foreach (var file in filesDoc)
            {
                var fi = new FileInfo(file);
                if (_pluginsIgnoreList.Contains(fi.Name))
                    continue;
                try
                {
                    var xdoc = XDocument.Load(file);
                    if (xdoc.Root != null)
                    {
                        var xElement = xdoc.Root.Element("assembly");
                        if (xElement != null)
                        {
                            var element = xElement.Element("name");
                            if (element != null)
                            {
                                var assemblyName = element.Value;
                                _pluginsDoc.Add(assemblyName, xdoc);
                            }
                        }
                    }
                }
                catch
                {
                    //Если произошла ошибка загрузки документации, просто игнорировать
                } 
            }
        }

        private void LoadPluginsIgnoreList()
        {
            var ignoreListFile = Path.Combine(Environment.CurrentDirectory, "AmEditor.ignorelist");
            if (!File.Exists(ignoreListFile))
                return;
            using (var reader = new StreamReader(ignoreListFile))
            {
                while (!reader.EndOfStream)
                {
                    var fileName = reader.ReadLine();
                    if (fileName != null && !string.IsNullOrEmpty(fileName.Trim()))
                        _pluginsIgnoreList.Add(fileName.Trim());
                }
            }
        }

        private void LoadConfigFile(string fileName)
        {
            _pluginsIncludeRules.Clear();
            _activitySteps.Clear();
            var xdoc = XDocument.Load(fileName, LoadOptions.PreserveWhitespace);
            if (xdoc.Root == null) throw new ApplicationException("Некоррекный файл конфигурации");
            if (xdoc.Root.Name.LocalName != "activity")
                throw new AMException("[config.xml]" + _("Корневой элемент файла конфигурации неизвестен"));
            //Обрабатываем элементы step
            var elements = xdoc.Root.Elements("step");
            foreach (var element in elements)
                _activitySteps.Add(ActivityStep.ConvertXElementToActivityStep(element, _language));
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
            if (xlanguage != null)
                _configLanguage = new Language(xlanguage.Value);
        }

        public void PrepareConfig()
        {
            for (var i = 0; i < _activitySteps.Count; i++)
            {
                var step = _activitySteps[i];
                CheckAndPrepareActions(ref step);
            }
        }

        public void CheckAndPrepareActions(ref ActivityStep step)
        {
            //Проверка и сопоставление plugin_name и action
            if (step.PlugName == null)
            {
                var finded = false;
                foreach (var plugin in _plugins)
                {
                    if (plugin.HasAction(step.ActionName, PlugActionHelper.ConvertActivityStepToPlugParameters(step.InputParameters, step.OutputParameters)))
                    {
                        if (!finded)
                        {
                            step.PlugName = plugin.PlugName;
                            finded = true;
                        }
                        else
                            throw new AMException(string.Format(CultureInfo.CurrentCulture,
                                _("Неоднозначность определения заданного действия. Действие \"{0}\" определено в плагинах {1} и {2}. Необходимо явное указание плагина в файле конфигурации"),
                                step.ActionName, step.PlugName, plugin.PlugName));
                    }
                }
                if (!finded)
                    throw new AMException(string.Format(CultureInfo.CurrentCulture, _("Не удалось найти действие {0} ни в одном из плагинов"),
                        step.ActionName));
            }
            else
                foreach (var plugin in _plugins)
                {
                    if ((plugin.PlugName == step.PlugName) &&
                        (!plugin.HasAction(step.ActionName, PlugActionHelper.ConvertActivityStepToPlugParameters(step.InputParameters, step.OutputParameters))))
                        throw new AMException(string.Format(CultureInfo.CurrentCulture, _("В плагине {0} не определено действие {1}"),
                            plugin.PlugName, step.ActionName));
                }
        }

        //Загрузить список этапов выполнения в DataGridView
        private void LoadDataGridViewSteps()
        {
            dataGridViewSteps.Rows.Clear();
            StepLabel.Visible = false;
            var i = 0;
            foreach (var step in _activitySteps)
            {
                i++;
                dataGridViewSteps.Rows.Add(i, step.Label, step +                                                                                                                                                                 (step.RepeatCount > 1 ? " "+string.Format(CultureInfo.CurrentCulture, _language.Translate("[повторений - {0}]"), step.RepeatCount) : ""));
                dataGridViewSteps.Rows[i - 1].ContextMenuStrip = contextMenuStrip1;
                if (!string.IsNullOrEmpty(step.Label))
                {
                    StepLabel.Visible = true;
                    dataGridViewSteps.Rows[i - 1].Cells["StepLabel"].Style.BackColor = Color.FromArgb(174, 210, 79);
                }
                if (!string.IsNullOrEmpty(step.Description))
                {
                    foreach (DataGridViewCell cell in dataGridViewSteps.Rows[i - 1].Cells)
                        cell.ToolTipText = step.Description;
                }
            }
        }

        //Загрузить список плагинов в выпадающий список
        private void LoadPluginsComboBox()
        {
            pluginName_comboBox.Items.Clear();
            foreach (var plugin in _plugins)
                pluginName_comboBox.Items.Add(plugin);
        }

        //Изменить состояние формы
        private void ChangeFormState()
        {
            buttonDel.Enabled = dataGridViewSteps.SelectedRows.Count > 0;
            buttonUp.Enabled = dataGridViewSteps.SelectedRows.Count > 0 && dataGridViewSteps.SelectedRows[0].Index > 0;
            buttonDown.Enabled = dataGridViewSteps.SelectedRows.Count > 0 &&
                dataGridViewSteps.SelectedRows[0].Index < (dataGridViewSteps.Rows.Count - 1);
            Text = _("AM-редактор");
            if (_formState == FormState.Edit)
                Text += @" [*]";
            if (!string.IsNullOrEmpty(_currentFileName))
                Text += string.Format(CultureInfo.CurrentCulture, " [{0}]", _currentFileName);
            pluginName_comboBox.Enabled = (dataGridViewSteps.SelectedRows.Count > 0);
            actionName_comboBox.Enabled = (dataGridViewSteps.SelectedRows.Count > 0);
        }

        //Разрешить перевод формы в состояние display
        private bool SetDisplayFormState()
        {
            if (_formState != FormState.Edit) return true;
            var dr = MessageBox.Show(_("Вы хотите сохранить изменения?"), _("Внимание"),
                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            switch (dr)
            {
                case DialogResult.No:
                    _formState = FormState.Display;
                    return true;
                case DialogResult.Yes:
                    return Save();
            }
            return false;
        }

        //Сохранить файл конфигурации
        private bool SaveFile(string fileName)
        {
            var document = new XDocument(new XDeclaration("1.0", "UTF-8", ""), new XElement("activity"));
            var activityElement = document.Root;
            if (activityElement == null) throw new ApplicationException("Неизвестная ошибка при сохранении файла");
            if (_pluginsIncludeRules.Count > 0)
            {
                var pluginsElement = new XElement("plugins");
                foreach (var pir in _pluginsIncludeRules)
                {
                    var includeElement = new XElement(pir.IncludeRule) {Value = pir.PlugNameMask};
                    pluginsElement.Add(includeElement);
                }
                activityElement.Add(pluginsElement);
            }
            activityElement.Add(new XElement("language", new XText(_configLanguage.Prefix)));
            if (_activitySteps.Count > 0)
            {
                var i = 0;
                foreach (var step in _activitySteps)
                {
                    if (step.PlugName == null || step.ActionName == null)
                    {
                        MessageBox.Show(string.Format(CultureInfo.CurrentCulture, _("Ошибка конфигурации шага выполнения №{0}. Данные не могут быть сохранены"), i),
                            _("Ошибка"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    i++;
                    var stepComment = new XComment(string.Format(CultureInfo.CurrentCulture, "step {0}", i));
                    var stepElement = new XElement("step", new XAttribute("plugin", step.PlugName),
                        new XAttribute("action", step.ActionName));
                    if (step.RepeatCount > 1)
                        stepElement.Add(new XAttribute("repeat", step.RepeatCount));
                    if (step.Label != null)
                        stepElement.Add(new XAttribute("label", step.Label));
                    if (step.Description != null)
                        stepElement.Add(new XAttribute("description", step.Description));
                    if (step.InputParameters.Count > 0)
                    {
                        var inputParameters = new XElement("input");
                        foreach (var asp in step.InputParameters)
                            inputParameters.Add(new XElement("parameter", new XAttribute("name", asp.Name), new XText(asp.Value)));
                        stepElement.Add(inputParameters);
                    }
                    if (step.OutputParameters.Count > 0)
                    {
                        var outputParameters = new XElement("output");
                        foreach (var asp in step.OutputParameters)
                            outputParameters.Add(new XElement("parameter", new XAttribute("name", asp.Name), new XText(asp.Value)));
                        stepElement.Add(outputParameters);
                    }
                    activityElement.Add(stepComment);
                    activityElement.Add(stepElement);
                }
            }
            try
            {
                document.Save(fileName);
                return true;
            }
            catch (IOException e)
            {
                MessageBox.Show(e.Message, _("Ошибка"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void SaveSettings()
        {
            _settingsStorage.OpenFileHistory.Sort();
            //Если в списке истории больше 20 файлов, то оставить только 20 самых новых
            if (_settingsStorage.OpenFileHistory.Count > 20)
                _settingsStorage.OpenFileHistory.RemoveRange(0, _settingsStorage.OpenFileHistory.Count - 20);
            SettingsStorage.SaveSettings(_settingsStorage);
        }

        //Сохранить
        private bool Save()
        {
            if (!File.Exists(_currentFileName)) return SaveAs();
            if (!SaveFile(_currentFileName)) return false;
            _formState = FormState.Display;
            ChangeFormState();
            SaveOpenFileState();
            return true;
        }

        //Сохранить как...
        private bool SaveAs()
        {
            if (saveFileDialog1.ShowDialog() != DialogResult.OK) return false;
            if (!SaveFile(saveFileDialog1.FileName)) return false;
            _currentFileName = saveFileDialog1.FileName;
            _currentFileOpenDatetime = DateTime.Now;
            _formState = FormState.Display;
            ChangeFormState();
            SaveOpenFileState();
            return true;
        }

        //Сохранить состояние параметров тестирования
        private void SaveOpenFileState()
        {
            //Добавляем информацию о только что сохраненном файле в историю открытых файлов
            var isExists = false;
            foreach (var item in _settingsStorage.OpenFileHistory)
                if (item.FileName == _currentFileName)
                {
                    //Если запись существует, то мы просто ее обновляем
                    item.CommandLineParams.Clear();
                    item.OpenDateTime = _currentFileOpenDatetime;
                    foreach (var key in _commandLineParams.Keys)
                        item.CommandLineParams.Add(key, _commandLineParams[key]);
                    isExists = true;
                }
            if (isExists) return;
            //Если запись не существует, то добавляем новую
            var ofhi = new OpenFileHistoryItem
            {
                FileName = _currentFileName,
                OpenDateTime = _currentFileOpenDatetime,
                CommandLineParams = _commandLineParams
            };
            _settingsStorage.OpenFileHistory.Add(ofhi);
        }

        //Открыть
        private void OpenConfig(string fileName)
        {
            _canDrag = false;
            //Если есть несохраненные данные, то сохраняем их
            if (!SetDisplayFormState())
                return;
            //Обновляем состояние внутренних переменных
            _currentFileName = fileName;
            _currentFileOpenDatetime = DateTime.Now;
            _commandLineParams.Clear();
            try
            {
                //Загружаем информацию
                LoadConfigFile(_currentFileName);
                LoadPlugins();
                PrepareConfig();
                LoadPluginsComboBox();
                LoadDataGridViewSteps();

                //Обновляем состояние формы
                ChangeFormState();

                //Если в истории открытых файлов есть данный файл, то обновляем время открытия и загружаем список параметров командной строки
                var isExists = false;
                foreach (var item in _settingsStorage.OpenFileHistory)
                    if (item.FileName == _currentFileName)
                    {
                        item.OpenDateTime = _currentFileOpenDatetime;
                        foreach (var key in item.CommandLineParams.Keys)
                            _commandLineParams.Add(key, item.CommandLineParams[key]);
                        isExists = true;
                    }
                //Добавляем информацию об открытом файле в историю открытых файлов если его не существует
                if (isExists) return;
                var ofhi = new OpenFileHistoryItem
                {
                    FileName = _currentFileName,
                    OpenDateTime = _currentFileOpenDatetime,
                    CommandLineParams = _commandLineParams
                };
                _settingsStorage.OpenFileHistory.Add(ofhi);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, @"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Транслятор языка
        private void InterfaceLanguageReload()
        {
            конфигурацияToolStripMenuItem.Text = _("Конфигурация");
            настройкаToolStripMenuItem1.Text = _("Настройка");
            создатьToolStripMenuItem.Text = _("Создать");
            открытьToolStripMenuItem.Text = _("Открыть");
            сохранитьToolStripMenuItem.Text = _("Сохранить");
            сохранитьКакToolStripMenuItem1.Text = _("Сохранить как");
            копироватьСтрокуЗапускаToolStripMenuItem.Text = _("Копировать строку выполнения");
            выполнитьToolStripMenuItem.Text = _("Выполнить");
            выходToolStripMenuItem3.Text = _("Выход");
            плагиныToolStripMenuItem2.Text = _("Плагины");
            параметрыКоманднойСтрокиToolStripMenuItem.Text = _("Параметры командной строки");
            языкToolStripMenuItem2.Text = _("Язык");
            dataGridViewSteps.Columns["StepName"].HeaderText = _("Шаг");
            dataGridViewSteps.Columns["StepLabel"].HeaderText = _("Метка");
            label1.Text = _("Действие");
            label2.Text = _("Плагин");
            label3.Text = _("Параметры");
            label4.Text = _("Описание");
            повторенийToolStripMenuItem.Text = _("Число повторений");
            меткаToolStripMenuItem.Text = _("Метка");
            примечаниеToolStripMenuItem.Text = _("Примечание");
            dataGridViewParams.Columns["ParamName"].HeaderText = _("Имя");
            dataGridViewParams.Columns["ParamType"].HeaderText = _("Тип");
            dataGridViewParams.Columns["ParamDirection"].HeaderText = _("Направление");
            dataGridViewParams.Columns["ParamValue"].HeaderText = _("Значение");
            if (dataGridViewSteps.SelectedRows.Count > 0)
            {
                var step = dataGridViewSteps.SelectedRows[0].Index;
                LoadDataGridViewSteps();
                dataGridViewSteps.Rows[step].Selected = true;
                dataGridViewSteps.CurrentCell = dataGridViewSteps.Rows[step].Cells[0];
            }
            ShowActionDescription();
            ChangeFormState();
        }

        private void StartDebug()
        {
            if (string.IsNullOrEmpty(_currentFileName))
            {
                открытьToolStripMenuItem_Click(this, new EventArgs());
                if (!File.Exists(_currentFileName))
                    return;
            }
            else
            {
                if (!Save())
                    return;
            }
            _debug = true;
            ProcessReport(_debug);
            debugToolStripMenuItem.Text = @"Остановить отладку";
            dataGridViewSteps.Enabled = false;
            buttonAdd.Enabled = false;
            buttonDel.Enabled = false;
            buttonDown.Enabled = false;
            buttonUp.Enabled = false;
            pluginName_comboBox.Enabled = false;
            actionName_comboBox.Enabled = false;
            dataGridViewParams.Enabled = false;
            создатьToolStripMenuItem.Enabled = false;
            открытьToolStripMenuItem.Enabled = false;
            сохранитьКакToolStripMenuItem1.Enabled = false;
            сохранитьToolStripMenuItem.Enabled = false;
            плагиныToolStripMenuItem2.Enabled = false;
            параметрыКоманднойСтрокиToolStripMenuItem.Enabled = false;
            языкToolStripMenuItem2.Enabled = false;
            выполнитьToolStripMenuItem.Enabled = true;
            dataGridViewSteps.DefaultCellStyle.SelectionBackColor = Color.LightCoral;
            if (dataGridViewSteps.Rows.Count > 0)
                dataGridViewSteps.Rows[0].Selected = true;
            dataGridViewSteps.Enabled = false;
        }

        private void StopDebug()
        {
            StopServer();
            _debug = false;
            debugToolStripMenuItem.Text = @"Начать отладку";
            dataGridViewSteps.Enabled = true;
            buttonAdd.Enabled = true;
            buttonDel.Enabled = true;
            buttonDown.Enabled = true;
            buttonUp.Enabled = true;
            pluginName_comboBox.Enabled = true;
            actionName_comboBox.Enabled = true;
            dataGridViewParams.Enabled = true;
            конфигурацияToolStripMenuItem.Enabled = true;
            настройкаToolStripMenuItem1.Enabled = true;
            создатьToolStripMenuItem.Enabled = true;
            открытьToolStripMenuItem.Enabled = true;
            сохранитьКакToolStripMenuItem1.Enabled = true;
            сохранитьToolStripMenuItem.Enabled = true;
            плагиныToolStripMenuItem2.Enabled = true;
            параметрыКоманднойСтрокиToolStripMenuItem.Enabled = true;
            языкToolStripMenuItem2.Enabled = true;
            //меняем стиль строк шагов редактора на стандартный 
            dataGridViewSteps.DefaultCellStyle.SelectionBackColor = SystemColors.Highlight;
            dataGridViewSteps.Enabled = true;
        }

        void Editor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!SetDisplayFormState())
                e.Cancel = true;
            //Сохраняем состояние программы
            SaveSettings();
        }

        private void плагиныToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            using (var fp = new FormPlugins(_pluginsIncludeRules, _language, _pluginsIgnoreList))
                if (fp.ShowDialog() == DialogResult.OK)
                {
                    _pluginsIncludeRules = fp.PluginsIncludeRules;
                    try
                    {
                        LoadPlugins();
                        LoadPluginsComboBox();
                        _formState = FormState.Edit;
                        ChangeFormState();
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show(err.Message, @"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                OpenConfig(openFileDialog1.FileName);
        }

        private void dataGridViewSteps_SelectionChanged(object sender, EventArgs e)
        {
            ChangeFormState();
            _isManualChangeState = false;
            pluginName_comboBox.SelectedIndex = -1;
            actionName_comboBox.SelectedIndex = -1;
            dataGridViewParams.Rows.Clear();
            richTextBoxDescription.Clear();
            if (dataGridViewSteps.SelectedRows.Count == 0)
                return;
            var step = _activitySteps[dataGridViewSteps.SelectedRows[0].Index];
            var pluginName = step.PlugName;
            var actionName = step.ActionName;
            //Выбираем плагин из списка
            if (pluginName == null)
            {
                _isManualChangeState = true;
                return;
            }
            for (var i = 0; i < pluginName_comboBox.Items.Count; i++)
            {
                if (((PlugInfo)pluginName_comboBox.Items[i]).PlugName == pluginName)
                {
                    pluginName_comboBox.SelectedIndex = i;
                    break;
                }
            }
            if (pluginName_comboBox.SelectedIndex == -1)
            {
                MessageBox.Show(string.Format(CultureInfo.CurrentCulture, _("Неизвестный плагин {0}"), pluginName), _("Ошибка"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                _isManualChangeState = true;
                return;
            }
            //Выбираем действие из списка
            if (actionName == null)
            {
                _isManualChangeState = true;
                return;
            }
            for (var i = 0; i < actionName_comboBox.Items.Count; i++)
            {
                var action = (PlugActionInfo)actionName_comboBox.Items[i];
                if (action.ActionName != step.ActionName)
                    continue;
                if (action.Parameters.Count != (step.InputParameters.Count + step.OutputParameters.Count))
                    continue;
                var actionIsEqual = true;
                foreach (var parameter in action.Parameters)
                {
                    var parameterFounded = false;
                    foreach (var chkParameter in
                        PlugActionHelper.ConvertActivityStepToPlugParameters(step.InputParameters, step.OutputParameters))
                    {
                        if ((chkParameter.Name != parameter.Name) || (chkParameter.Direction != parameter.Direction))
                            continue;
                        parameterFounded = true;
                        break;
                    }
                    if (parameterFounded) continue;
                    actionIsEqual = false;
                    break;
                }
                if (!actionIsEqual) continue;
                actionName_comboBox.SelectedIndex = i;
                break;
            }
            _isManualChangeState = true;
            if (actionName_comboBox.SelectedIndex == -1)
                MessageBox.Show(string.Format(CultureInfo.CurrentCulture, _("Неизвестное действие {0}"), actionName), _("Ошибка"), MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!SetDisplayFormState())
                return;
            _activitySteps.Clear();
            _pluginsIncludeRules.Clear();
            _plugins.Clear();
            _pluginsDoc.Clear();
            _commandLineParams.Clear();
            LoadDataGridViewSteps();
            LoadPluginsComboBox();
            _currentFileName = "";
            _currentFileOpenDatetime = DateTime.Now;
            ChangeFormState();
        }

        private void pluginName_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            actionName_comboBox.Items.Clear();
            if (pluginName_comboBox.SelectedIndex == -1)
                return;
            var plugin = (PlugInfo)pluginName_comboBox.SelectedItem;
            foreach (var pai in plugin.PlugActions)
                actionName_comboBox.Items.Add(pai);
            if (!_isManualChangeState) return;
            _activitySteps[dataGridViewSteps.SelectedRows[0].Index].PlugName = plugin.PlugName;
            _formState = FormState.Edit;
            ChangeFormState();
        }

        private void ShowActionDescription()
        {
            var pai = (PlugActionInfo)actionName_comboBox.SelectedItem;
            var plugin = (PlugInfo)pluginName_comboBox.SelectedItem;
            if (plugin == null || pai == null)
                return;
            var pluginName = plugin.PlugName;
            var releazeClassName = plugin.RealizeClassName();
            var actionName = pai.ActionName;
            var actionParametersList = "";
            foreach (var pap in pai.Parameters)
                actionParametersList += pap.ParameterType.FullName.Replace("&", "@") + ",";
            actionParametersList = actionParametersList.Trim(',');
            if (!_pluginsDoc.ContainsKey(pluginName)) return;
            var pluginDoc = _pluginsDoc[pluginName];
            //Формат атрибута name элемента member в файле документации
            var elementName = "M:" + pluginName + "." + releazeClassName + "." + actionName + "(" + actionParametersList + ")";
            foreach (var element in pluginDoc.Root.Element("members").Elements())
            {
                if (element.Attribute("name").Value == elementName)
                {
                    //Это искомое описание метода, отформатировать и вывести в поле описания
                    var summary = element.Element("summary").Value.Trim();
                    var paramsNames = new Dictionary<string, string>();
                    foreach (var paramElement in element.Elements("param"))
                        paramsNames.Add(paramElement.Attribute("name").Value, paramElement.Value.Trim());
                    var description = _("Описание") + ": " + summary;
                    foreach (var key in paramsNames.Keys)
                    {
                        var paramValue = paramsNames[key];
                        description += Environment.NewLine;
                        description += _("Параметр") + " [" + key + "]: " + paramValue;
                    }
                    richTextBoxDescription.Text = description;
                    richTextBoxDescription.Select(0, (_("Описание") + ": ").Length);
                    richTextBoxDescription.SelectionFont = new Font(richTextBoxDescription.Font, FontStyle.Bold);
                    foreach (var key in paramsNames.Keys)
                    {
                        richTextBoxDescription.Select(
                            richTextBoxDescription.Text.IndexOf(_("Параметр") + " [" + key + "]: ", StringComparison.CurrentCulture),
                            (_("Параметр") + " [" + key + "]: ").Length);
                        richTextBoxDescription.SelectionFont = new Font(richTextBoxDescription.Font, FontStyle.Bold);
                        richTextBoxDescription.Select(
                            richTextBoxDescription.Text.IndexOf("[" + key + "]", StringComparison.CurrentCulture), ("[" + key + "]").Length);
                        richTextBoxDescription.SelectionColor = Color.Blue;
                    }
                }
            }
        }

        private void actionName_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Загружаем параметры действия и их значения (если они заданы)
            dataGridViewParams.Rows.Clear();
            richTextBoxDescription.Clear();
            if (actionName_comboBox.SelectedIndex == -1)
                return;
            var pai = (PlugActionInfo)actionName_comboBox.SelectedItem;
            var i = 0;
            if (_isManualChangeState && dataGridViewSteps.SelectedRows.Count > 0)
            {
                var step = _activitySteps[dataGridViewSteps.SelectedRows[0].Index];
                step.InputParameters.Clear();
                step.OutputParameters.Clear();
                foreach (var pap in pai.Parameters)
                {
                    if (pap.Direction == ParameterDirection.Input)
                        step.InputParameters.Add(new ActivityStepParameter(pap.Name, ""));
                    else
                        step.OutputParameters.Add(new ActivityStepParameter(pap.Name, ""));
                }
            }
            foreach (var pap in pai.Parameters)
            {
                i++;
                if (dataGridViewSteps.SelectedRows.Count == 0)
                {
                    dataGridViewParams.Rows.Add(i, pap.Name, pap.ParameterType, pap.Direction, null);
                    continue;
                }
                //Если выбран шаг, то подгрузить значения для него
                object value = null;
                var step = _activitySteps[dataGridViewSteps.SelectedRows[0].Index];
                foreach (var asp in step.InputParameters)
                {
                    if (pap.Name == asp.Name)
                    {
                        value = asp.Value;
                        break;
                    }
                }
                foreach (var asp in step.OutputParameters)
                {
                    if (pap.Name == asp.Name)
                    {
                        value = asp.Value;
                        break;
                    }
                }
                dataGridViewParams.Rows.Add(i, pap.Name, pap.ParameterType, pap.Direction, value);
            }
            //Если есть документация в plugins_doc по данному действию, то получить ее
            ShowActionDescription();
            //Обновить состояние формы
            if (_isManualChangeState)
            {
                var step = _activitySteps[dataGridViewSteps.SelectedRows[0].Index];
                step.ActionName = pai.ActionName;
                dataGridViewSteps.SelectedRows[0].Cells["StepName"].Value = step +
                    (step.RepeatCount > 1 ? " " + string.Format(CultureInfo.CurrentCulture, _language.Translate("[повторений - {0}]"), step.RepeatCount) : "");
                _formState = FormState.Edit;
                ChangeFormState();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var step = new ActivityStep();
            var rowIndex = 0;
            if (dataGridViewSteps.CurrentCell != null)
                rowIndex = dataGridViewSteps.CurrentCell.RowIndex + 1;
            _activitySteps.Insert(rowIndex, step);
            LoadDataGridViewSteps();
            dataGridViewSteps.Rows[rowIndex].Selected = true;
            dataGridViewSteps.CurrentCell = dataGridViewSteps.Rows[rowIndex].Cells[0];
            _formState = FormState.Edit;
            ChangeFormState();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridViewSteps.SelectedRows.Count == 0)
                return;
            var step = _activitySteps[dataGridViewSteps.SelectedRows[0].Index];
            var index = dataGridViewSteps.SelectedRows[0].Index;
            _activitySteps.Remove(step);
            LoadDataGridViewSteps();
            if (dataGridViewSteps.RowCount > 0)
            {
                dataGridViewSteps.Rows[index > (dataGridViewSteps.Rows.Count - 1) ? (dataGridViewSteps.Rows.Count - 1) : index].Selected = true;
                dataGridViewSteps.CurrentCell = dataGridViewSteps.
                    Rows[index > (dataGridViewSteps.Rows.Count - 1) ? (dataGridViewSteps.Rows.Count - 1) : index].Cells[0];
            }
            _formState = FormState.Edit;
            ChangeFormState();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var index = dataGridViewSteps.SelectedRows[0].Index;
            var step = _activitySteps[index];
            _activitySteps[index] = _activitySteps[index - 1];
            _activitySteps[index - 1] = step;
            LoadDataGridViewSteps();
            dataGridViewSteps.Rows[index - 1].Selected = true;
            dataGridViewSteps.CurrentCell = dataGridViewSteps.Rows[index - 1].Cells[0];
            _formState = FormState.Edit;
            ChangeFormState();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var index = dataGridViewSteps.SelectedRows[0].Index;
            var step = _activitySteps[index];
            _activitySteps[index] = _activitySteps[index + 1];
            _activitySteps[index + 1] = step;
            LoadDataGridViewSteps();
            dataGridViewSteps.Rows[index + 1].Selected = true;
            dataGridViewSteps.CurrentCell = dataGridViewSteps.Rows[index + 1].Cells[0];
            _formState = FormState.Edit;
            ChangeFormState();
        }

        private void dataGridViewSteps_MouseMove(object sender, MouseEventArgs e)
        {
            if (dataGridViewSteps.SelectedRows.Count == 1)
            {
                if ((e.Button != MouseButtons.Left) || !_canDrag) return;
                _rw = dataGridViewSteps.SelectedRows[0];
                _rowIndexFromMouseDown = dataGridViewSteps.SelectedRows[0].Index;
                dataGridViewSteps.DoDragDrop(_rw, DragDropEffects.Move);
            }
        }

        private void dataGridViewSteps_MouseDown(object sender, MouseEventArgs e)
        {
            _canDrag = true;
            if (e.Button != MouseButtons.Right) return;
            var hti = dataGridViewSteps.HitTest(e.X, e.Y);
            if (hti.RowIndex != -1)
                dataGridViewSteps.Rows[hti.RowIndex].Selected = true;
        }

        private void dataGridViewSteps_MouseUp(object sender, MouseEventArgs e)
        {
            _canDrag = false;
        }

        private void dataGridViewSteps_DragDrop(object sender, DragEventArgs e)
        {
            var files = (Array)e.Data.GetData(DataFormats.FileDrop);
            if (files != null)
            {
                var file = files.GetValue(0).ToString();
                if (File.Exists(file))
                {
                    var fi = new FileInfo(file);
                    if (fi.Extension.ToUpper(CultureInfo.CurrentCulture) == ".XML")
                        OpenConfig(file);
                }
                Activate();
            }
            else
            {
                var clientPoint = dataGridViewSteps.PointToClient(new Point(e.X, e.Y));
                var rowIndexOfItemUnderMouseToDrop = dataGridViewSteps.HitTest(clientPoint.X, clientPoint.Y).RowIndex;
                if ((e.Effect != DragDropEffects.Move) || (rowIndexOfItemUnderMouseToDrop == -1) ||
                    (rowIndexOfItemUnderMouseToDrop == _rowIndexFromMouseDown)) return;
                var step = _activitySteps[_rowIndexFromMouseDown];
                _activitySteps.Remove(step);
                _activitySteps.Insert(rowIndexOfItemUnderMouseToDrop, step);
                LoadDataGridViewSteps();
                dataGridViewSteps.Rows[rowIndexOfItemUnderMouseToDrop].Selected = true;
                dataGridViewSteps.CurrentCell = dataGridViewSteps.Rows[rowIndexOfItemUnderMouseToDrop].Cells[0];
                _formState = FormState.Edit;
                ChangeFormState();
            }
        }

        private void dataGridViewSteps_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                if (dataGridViewSteps.SelectedRows.Count > 0)
                    e.Effect = DragDropEffects.Move;
                else
                    e.Effect = DragDropEffects.None;
        }

        //Поиск списка видимых глобальных параметров указанного типа на данном шаге выполнения
        private List<string> GlobalParametersNamesBy(Type type, int stepIndex)
        {
            var globalParameters = new List<string>();
            for (var i = 0; i < stepIndex; i++)
            {
                PlugInfo currentPlugin = null;
                foreach (var plugin in _plugins)
                    if (plugin.PlugName == _activitySteps[i].PlugName)
                        currentPlugin = plugin;
                if (currentPlugin == null)
                    continue;
                PlugActionInfo currentAction = null;
                foreach (var pai in currentPlugin.PlugActions)
                    if (pai.ActionName == _activitySteps[i].ActionName)
                        currentAction = pai;
                if (currentAction == null) continue;
                foreach (var param in currentAction.Parameters)
                    if ((param.Direction == ParameterDirection.Output) && (param.ParameterType.FullName.Replace("&", "") == type.FullName))
                    {
                        foreach (var asp in _activitySteps[i].OutputParameters)
                            if (asp.Name == param.Name)
                            {
                                if (!string.IsNullOrEmpty(asp.Value.Trim()))
                                {
                                    if (!globalParameters.Contains("[" + asp.Value + "]"))
                                        globalParameters.Add("[" + asp.Value + "]");
                                }
                                else
                                {
                                    if (!globalParameters.Contains("[" + asp.Value + "]"))
                                        globalParameters.Add("[" + asp.Name + "]");
                                }
                            }
                    }
            }
            return globalParameters;
        }

        private void SetCurrentActiveStepParameterValue(string value)
        {
            var paramName = (string)dataGridViewParams.SelectedRows[0].Cells["ParamName"].Value;
            for (var i = 0; i < _activitySteps[dataGridViewSteps.SelectedRows[0].Index].InputParameters.Count; i++)
            {
                if (_activitySteps[dataGridViewSteps.SelectedRows[0].Index].InputParameters[i].Name == paramName)
                    _activitySteps[dataGridViewSteps.SelectedRows[0].Index].InputParameters[i].Value = value;
            }
            for (var i = 0; i < _activitySteps[dataGridViewSteps.SelectedRows[0].Index].OutputParameters.Count; i++)
            {
                if (_activitySteps[dataGridViewSteps.SelectedRows[0].Index].OutputParameters[i].Name == paramName)
                    _activitySteps[dataGridViewSteps.SelectedRows[0].Index].OutputParameters[i].Value = value;
            }
            dataGridViewParams.SelectedRows[0].Cells["ParamValue"].Value = value;
        }

        private string GetCurrentActiveStepParameterValue()
        {
            return dataGridViewParams.SelectedRows[0].Cells["ParamValue"].Value.ToString();
        }

        public void ProcessReport(bool debug)
        {
            if (string.IsNullOrEmpty(_currentFileName))
            {
                открытьToolStripMenuItem_Click(this, new EventArgs());
                return;
            }
            var activityManager = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ActivityManager.exe");
            if (!File.Exists(activityManager))
            {
                MessageBox.Show(_("Не удалось найти исполняемый файл ActivityManager.exe"),
                    _("Ошибка"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (debug)
            {
                //пересоздаем сервер с новым портом
                _server = new TcpListener(IPAddress.Parse("127.0.0.1"), GetFreePort());
                //ожидаем клиента в отдельном потоке
                var ts = new Thread(StartServer);
                ts.Start();
            }
            var arguments = "config=\"" + _currentFileName + "\"";
            foreach (var key in _commandLineParams.Keys)
                arguments += " " + key + "=\"" + _commandLineParams[key] + "\"";
            //добавим аргумент отладки
            arguments += " " + "debug" + "=\"" + debug + "\"";
            arguments += " " + "debug_port" + "=\"" + _port + "\"";
            if (debug)
                arguments += " " + "--nodialog";
            _process = new Process();
            var psi = new ProcessStartInfo(activityManager, arguments)
            {
                CreateNoWindow = true,
                UseShellExecute = false
            };
            _process.StartInfo = psi;
            _process.Start();
        }

        private void CommunicationToClient(MessageForDebug message, bool receive = true)
        {
            try
            {                                             
                var array = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
                _stream.Write(array, 0, array.Length);
                if (!receive) return;
                var buffer = new byte[_client.ReceiveBufferSize];
                var bytes = _stream.Read(buffer, 0, _client.ReceiveBufferSize);
                var str = Encoding.UTF8.GetString(buffer, 0, bytes);
                var response = JsonConvert.DeserializeObject<MessageForDebug>(str);
                if (response == null)
                    return;
                if (response.ContainsKey("debug") && response["debug"] == "done")
                {
                    StopDebug();
                    return;
                }
                if (response.ContainsKey("exception"))
                {
                    StopDebug();
                    MessageBox.Show(response["exception"], @"Ошибка", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);  
                    return;
                }
                if (!response.ContainsKey("step")) return;
                int step;
                if (!int.TryParse(response["step"], out step)) return;
                // Меняем стиль строки следующего шага
                if (dataGridViewSteps.Rows.Count > step)
                    dataGridViewSteps.Rows[step].Selected = true;
            }
            catch (ApplicationException e)
            {
                MessageBox.Show(e.InnerException != null ? e.InnerException.Message : e.Message,  @"Ошибка", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                StopDebug();
            }         
        }

        public void StartServer()
        {
            _server.Start();
            _client = _server.AcceptTcpClient();
            _stream = _client.GetStream();  
        }

        public void StopServer()
        {
            if (_server == null) return;
            if (_client != null && _client.Connected)
            {
                CommunicationToClient(new MessageForDebug { { "command", "stop" } }, false);
                _client.Close();
            }
            _server.Stop();
        }

        private static IEnumerable<int> GetOpenPorts()
        {
            var properties = IPGlobalProperties.GetIPGlobalProperties();
            var tcpConnections = properties.GetActiveTcpConnections();
            return tcpConnections.Select(c => c.LocalEndPoint.Port);
        }

        private int GetFreePort()
        {
            var has = true;
            while (has)
            {
                var r = new Random();
                _port = r.Next(16000, 16535);
                has = GetOpenPorts().Any(p => p == _port);
            }
            return _port;            
        }

        private void dataGridViewParams_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridViewParams.SelectedRows.Count == 0)
                return;
            var paramType = (Type)dataGridViewParams.SelectedRows[0].Cells["ParamType"].Value;
            var paramName = (string)dataGridViewParams.SelectedRows[0].Cells["ParamName"].Value;
            var direction =
                (ParameterDirection)dataGridViewParams.SelectedRows[0].Cells["ParamDirection"].Value;
            if (direction == ParameterDirection.Output)
            {
                using (var fsv = new FormStringValue(_language))
                {
                    fsv.Text = _("Задать глобальное имя") + @" [" + paramName + @"]";
                    fsv.Value = GetCurrentActiveStepParameterValue();
                    if (fsv.ShowDialog() == DialogResult.OK)
                    {
                        SetCurrentActiveStepParameterValue(fsv.Value);
                        _formState = FormState.Edit;
                        ChangeFormState();
                    }
                }
                return;
            }
            var values = GlobalParametersNamesBy(paramType, dataGridViewSteps.SelectedRows[0].Index);
            foreach (var key in _commandLineParams.Keys)
                if (!values.Contains("[" + key + "]"))
                    values.Add("[" + key + "]");
            //Если тип данных перечисление
            if (paramType.IsEnum)
            {
                var names = Enum.GetNames(paramType);
                foreach (var name in names)
                    values.Add(name);
                using (var fcbv = new FormComboBoxValue(values, _language))
                {
                    fcbv.Text = _("Задать значение параметра") + @" [" + paramName + @"]";
                    fcbv.value = GetCurrentActiveStepParameterValue();
                    if (fcbv.ShowDialog() != DialogResult.OK) return;
                    SetCurrentActiveStepParameterValue(fcbv.value);
                    _formState = FormState.Edit;
                    ChangeFormState();
                }
                return;
            }
            //Если тип является одним из стандартных типов данных вроде числа, даты, логического типа
            var isComboboxType = false;
            foreach (var type in _comboBoxValueTypes)
                if (paramType == type)
                    isComboboxType = true;
            if (isComboboxType)
            {
                if (paramType == typeof(bool))
                {
                    values.Add("true");
                    values.Add("false");
                }
                using (var fcbv = new FormComboBoxValue(values, _language))
                {
                    fcbv.Text = _("Задать значение параметра") + @" [" + paramName + @"]";
                    fcbv.value = GetCurrentActiveStepParameterValue();
                    if (fcbv.ShowDialog() != DialogResult.OK) return;
                    SetCurrentActiveStepParameterValue(fcbv.value);
                    _formState = FormState.Edit;
                    ChangeFormState();
                }
                return;
            }
            //Если тип является строкой или не известен
            foreach (var type in _comboBoxValueTypes)
                values.AddRange(GlobalParametersNamesBy(type, dataGridViewSteps.SelectedRows[0].Index));
            using (var fsqlv = new FormMultiValue(values, _language))
            {
                fsqlv.Text = _("Задать значение параметра") + @" [" + paramName + @"]";
                fsqlv.Value = GetCurrentActiveStepParameterValue();
                if (fsqlv.ShowDialog() != DialogResult.OK) return;
                SetCurrentActiveStepParameterValue(fsqlv.Value);
                _formState = FormState.Edit;
                ChangeFormState();
            }
        }

        private void параметрыКоманднойСтрокиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var fclp = new FormCommandLineParams(_language))
            {
                fclp.command_line_params = _commandLineParams;
                if (fclp.ShowDialog() == DialogResult.OK)
                {
                    _commandLineParams = fclp.command_line_params;
                    _formState = FormState.Edit;
                    ChangeFormState();
                }
            }
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void сохранитьКакToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SaveAs();
        }

        private void выполнитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_debug == false)
            {
                if (!Save())
                    return;
                ProcessReport(_debug);
            }
            else
            {
                CommunicationToClient(new MessageForDebug {{"command", "run"}}, false);
                StopDebug();
            }
        }

        private void выходToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void языкToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            using (var fl = new FormLanguage(_language))
            {
                var langPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "lang");
                var files = Directory.GetFiles(langPath);
                var languages = new List<string> {"ru"};
                //Язык по умолчанию
                foreach (var file in files)
                {
                    var fi = new FileInfo(file);
                    if (!languages.Contains(fi.Extension.Trim('.')))
                        languages.Add(fi.Extension.Trim('.'));
                }
                fl.languages = languages;
                fl.config_language = _configLanguage.Prefix;
                fl.interface_language = _language.Prefix;
                if (fl.ShowDialog() != DialogResult.OK) return;
                _language = new Language(fl.interface_language);
                _settingsStorage.InterfaceLanguagePrefix = fl.interface_language;
                _ = _language.Translate;
                if (_configLanguage.Prefix != fl.config_language)
                {
                    _configLanguage = new Language(fl.config_language);
                    _formState = FormState.Edit;
                    ChangeFormState();
                }
                InterfaceLanguageReload();
            }
        }

        private void копироватьСтрокуЗапускаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var activityManager = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ActivityManager.exe");
            var arguments = "config=\"" + _currentFileName + "\"";
            foreach (var key in _commandLineParams.Keys)
                arguments += " " + key + "=\"" + _commandLineParams[key] + "\"";
            Clipboard.SetText(activityManager + " " + arguments);
            MessageBox.Show(_("Строка выполнения успешно скопирована"), _("Информация"), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dataGridViewParams_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            dataGridViewParams_DoubleClick(sender, e);
            e.Handled = true;
        }

        private void повторенийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridViewSteps.SelectedRows.Count == 0)
                return;
            var step = dataGridViewSteps.SelectedRows[0].Index;
            using (var fsv = new FormStringValue(_language))
            {
                fsv.Text = _("Задать число повторений шага") + @" №" + (step + 1);
                fsv.Value = _activitySteps[step].RepeatCount.ToString(CultureInfo.CurrentCulture);
                if (fsv.ShowDialog() == DialogResult.OK)
                {
                    int repeatCount;
                    if (int.TryParse(fsv.Value.Trim(), out repeatCount))
                        _activitySteps[step].RepeatCount = repeatCount;
                    else
                    {
                        MessageBox.Show(string.Format(CultureInfo.CurrentCulture, _("Не удалось привести значение '{0}' к числовому типу. Число повторений шага задано по умолчанию равным единице"), fsv.Value),
                            _("Ошибка"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        _activitySteps[step].RepeatCount = 1;
                    }
                    LoadDataGridViewSteps();
                    dataGridViewSteps.Rows[step].Selected = true;
                    dataGridViewSteps.CurrentCell = dataGridViewSteps.Rows[step].Cells[0];
                    _formState = FormState.Edit;
                    ChangeFormState();
                }
            }
        }

        private void меткаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridViewSteps.SelectedRows.Count == 0)
                return;
            var step = dataGridViewSteps.SelectedRows[0].Index;
            using (var fsv = new FormStringValue(_language))
            {
                fsv.Text = _("Задать метку шага") + @" №" + (step + 1);
                fsv.Value = _activitySteps[step].Label;
                if (fsv.ShowDialog() != DialogResult.OK) return;
                _activitySteps[step].Label = string.IsNullOrEmpty(fsv.Value.Trim()) ? null : fsv.Value.Trim();
                LoadDataGridViewSteps();
                dataGridViewSteps.Rows[step].Selected = true;
                dataGridViewSteps.CurrentCell = dataGridViewSteps.Rows[step].Cells[0];
                _formState = FormState.Edit;
                ChangeFormState();
            }
        }

        private void примечаниеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridViewSteps.SelectedRows.Count == 0)
                return;
            var step = dataGridViewSteps.SelectedRows[0].Index;
            using (var fsv = new FormStringValue(_language))
            {
                fsv.Text = _("Задать примечание шага") + @" №" + (step + 1);
                fsv.Value = _activitySteps[step].Description;
                if (fsv.ShowDialog() == DialogResult.OK)
                {
                    _activitySteps[step].Description = string.IsNullOrEmpty(fsv.Value.Trim()) ? null : fsv.Value.Trim();
                    foreach (DataGridViewCell cell in dataGridViewSteps.SelectedRows[0].Cells)
                        cell.ToolTipText = string.IsNullOrEmpty(fsv.Value.Trim()) ? null : fsv.Value.Trim();
                    _formState = FormState.Edit;
                    ChangeFormState();
                }
            }
        }

        private void Editor_DragDrop(object sender, DragEventArgs e)
        {
            var files = (Array)e.Data.GetData(DataFormats.FileDrop);
            if (files == null) return;
            var file = files.GetValue(0).ToString();
            if (File.Exists(file))
            {
                var fi = new FileInfo(file);
                if (fi.Extension.ToUpper(CultureInfo.CurrentCulture) == ".XML")
                    OpenConfig(file);
            }
            Activate();
        }

        private void Editor_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? 
                DragDropEffects.Copy : DragDropEffects.None;
        }

        private void debugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_debug && _process != null && !_process.HasExited)
            {
                StopDebug();
            }
            else
            {
                if (!_debug || _process == null || _process.HasExited)
                    StartDebug();
            }
        }

        private void следующийШагToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!_debug || _process == null || _process.HasExited)
                StartDebug();
            else
            if (_client != null && _client.Connected)
                CommunicationToClient(new MessageForDebug {{"command", "next"}});
        }

        private void Editor_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            StopDebug();
        }
    }
}
