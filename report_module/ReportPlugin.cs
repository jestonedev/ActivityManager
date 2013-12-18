using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using extended_types;

//Правила написания плагинов:
//В плагине должен быть объявлен интерфейс IPlugin
//Только один класс плагина должен реализовывать интерфейс IPlugin
//Из-вне видны только методы, описанные в интерфейсе IPlugin
//Нельзя использовать одинаковые имена методов плагина (в будущем возможно переделаю более элегантно)
//Менеджер создает только одну инстанцию каждого плагина => состояние класса после вызовов его методов будет сохраняться (в будущем возможно будет пересмотрено)

//Плагин для работы с отчетными формами
namespace report_module
{
    public enum XmlClouser { Table, Row, Cell, P }

	//Доступный из-вне интерфейс плагина
	public interface IPlugin
	{
        void report_set_template_file(string file_name);
		void report_set_string_value(string name, string value);
        void report_set_string_values(ReportRow values);
        void report_set_table_value(ReportTable table, XmlClouser xml_clouser);
        void report_generate(out string file_name);
	}

	//Класс, реализующий интерфейс плагина, в каждой сборке может быть только один такой класс
	public class ReportPlugin: IPlugin
	{
        /// <summary>
        /// Файл шаблона отчета
        /// </summary>
		private string template_file;
        
        /// <summary>
        /// Путь до временной директории пользователя
        /// </summary>
        private string temporary_path;

        /// <summary>
        /// Список переменных для подстановки
        /// </summary>
		private List<ReportValue> values = new List<ReportValue>();

        /// <summary>
        /// В конструкторе происходит проверка существования директории report_module во временной папке.
        /// При необходимости директория создается. Плагин контролирует очистку директории от старых файлов.
        /// </summary>
        public ReportPlugin()
        {
            string tmp_path = Path.GetTempPath();
            string report_module_path = Path.Combine(tmp_path, "report_module");
            if (!Directory.Exists(report_module_path))
                Directory.CreateDirectory(report_module_path);
            string[] files = Directory.GetFiles(report_module_path);
            foreach (string file in files)
                if (File.GetCreationTime(file).AddDays(7) < DateTime.Now)
                    File.Delete(file);
            string[] directories = Directory.GetDirectories(report_module_path);
            foreach (string directory in directories)
                Directory.Delete(directory, true);
            this.temporary_path = report_module_path;
        }

        /// <summary>
        /// Установить файл шаблона
        /// </summary>
        /// <param name="file_name">Полный путь до файла шаблона</param>
		public void report_set_template_file(string file_name)
		{
            if (File.Exists(file_name))
                this.template_file = file_name;
            else
            if (File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, file_name)))
                this.template_file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, file_name);
            else
            {
                ApplicationException exception = new ApplicationException("Указанный файл \"{0}\" шаблона отчета не существует");
                exception.Data.Add("{0}", file_name);
                throw exception;
            }
		}

        /// <summary>
        /// Установить строковый параметр замены
        /// </summary>
        /// <param name="pattern">Шаблон замены в формате @"^\$\w+\$$"</param>
        /// <param name="value">Подставляемое значение</param>
        public void report_set_string_value(string name, string value)
		{
            values.Add(new StringReportValue(name, (value == null) ? "" : value));             
		}

        /// <summary>
        /// Групповая установка строковых параметров замены
        /// </summary>
        /// <param name="values">Список подставляемых значений</param>
        public void report_set_string_values(ReportRow values)
        {
            for (int i = 0; i < values.Count; i++)
                this.values.Add(new StringReportValue(values[i].Row.Table.Columns[i], (values[i].Value == null) ? "" : values[i].Value));
        }

        /// <summary>
        /// Установить списочный (табличный) параметр замены
        /// </summary>
        /// <param name="table">Список параметров</param>
        /// <param name="xml_clouser">XML-замыкатель. Поддерживаемые замыкатели по умолчанию table, row, cell</param>
        public void report_set_table_value(ReportTable table, XmlClouser xml_clouser)
		{
			values.Add(new TableReportValue(table, xml_clouser.ToString()));
		}
        
        /// <summary>
        /// Функция генерации отчета
        /// </summary>
        /// <param name="file_name">Путь до выходного файла отчета</param>
        public void report_generate(out string file_name)
        {
            if (!File.Exists(template_file))
            {
                ApplicationException exception = new ApplicationException("Указанный файл \"{0}\" шаблона отчета не существует");
                exception.Data.Add("{0}", template_file);
                throw exception;
            }
            string report_filename = Guid.NewGuid().ToString();
            FileInfo template_file_info = new FileInfo(template_file);
            //Распаковываем файл шаблона во временную директорию
            FastZip zip = new FastZip();
            string report_unzip_path = Path.Combine(temporary_path, report_filename);
            zip.ExtractZip(template_file, report_unzip_path, "");
            //Формируем отчет
            report_editing(report_unzip_path, template_file_info.Extension);
            //Запаковываем файл шаблона и удаляем временную директорию отчета
            string report_full_filename = Path.Combine(temporary_path, report_filename + template_file_info.Extension);
            zip.CreateZip(report_full_filename, report_unzip_path, true, "");
            Directory.Delete(report_unzip_path, true);
            file_name = report_full_filename;
        }

        /// <summary>
        /// Функция определения типа документа и вызова соответсвующего редактора отчета
        /// </summary>
        /// <param name="report_unzip_path">Путь до распакованного документа</param>
        private void report_editing(string report_unzip_path, string extension)
        {
            ReportEditor editor;
            if (File.Exists(Path.Combine(report_unzip_path, "content.xml")) && (extension == ".odt"))
                editor = new WriterEditor();
            else
            if (File.Exists(Path.Combine(report_unzip_path, "content.xml")) && (extension == ".ods"))
                editor = new CalcEditor();
            else
            if (File.Exists(Path.Combine(report_unzip_path, @"word\document.xml")))
                editor = new WordEditor();
            else
            if (Directory.Exists(Path.Combine(report_unzip_path, "xl")))
                editor = new ExcelEditor();
            else
                throw new ApplicationException("Формат файла шаблона некорректный");
            values = editor.xml_clousers_convert(values);
            editor.report_editing(report_unzip_path, values);
            editor.special_tag_editing(report_unzip_path);
        }
    }
}
