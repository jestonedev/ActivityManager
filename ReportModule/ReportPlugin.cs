using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using ExtendedTypes;
using System.Runtime.Serialization;
using System.Collections.ObjectModel;

//Плагин для работы с отчетными формами
namespace ReportModule
{
    /// <summary>
    /// Перечисление XML-замыкателей. Определяет правила формирования таблиц отчетов.
    /// </summary>
    public enum XmlContractor { 
        /// <summary>
        /// Повторять таблицу
        /// </summary>
        Table, 
        /// <summary>
        /// Повторять строку
        /// </summary>
        Row, 
        /// <summary>
        /// Повторять ячейку строки
        /// </summary>
        Cell, 
        /// <summary>
        /// Повторять абзац
        /// </summary>
        Paragraph
    }

	/// <summary>
    /// Доступный из-вне интерфейс плагина
	/// </summary>
	public interface IPlug
	{
        /// <summary>
        /// Установить файл шаблона
        /// </summary>
        /// <param name="fileName">Полный путь до файла шаблона</param>
        void ReportSetTemplateFile(string fileName);

        /// <summary>
        /// Установить строковый параметр замены
        /// </summary>
        /// <param name="name">Шаблон замены"</param>
        /// <param name="value">Подставляемое значение</param>
		void ReportSetStringValue(string name, string value);

        /// <summary>
        /// Групповая установка строковых параметров замены
        /// </summary>
        /// <param name="values">Список подставляемых значений</param>
        void ReportSetStringValues(ReportRow values);

        /// <summary>
        /// Установить списочный (табличный) параметр замены
        /// </summary>
        /// <param name="table">Список параметров</param>
        /// <param name="xmlContractor">XML-замыкатель. Поддерживаемые замыкатели по умолчанию table, row, cell, p</param>
        void ReportSetTableValue(ReportTable table, XmlContractor xmlContractor);

        /// <summary>
        /// Функция генерации отчета
        /// </summary>
        /// <param name="fileName">Путь до выходного файла отчета</param>
        void ReportGenerate(out string fileName);
	}

	/// <summary>
    /// Класс, реализующий интерфейс плагина, в каждой сборке может быть только один такой класс
	/// </summary>
	public class ReportPlug: IPlug
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
        private Collection<ReportValue> Values = new Collection<ReportValue>();

        /// <summary>
        /// В конструкторе происходит проверка существования директории report_module во временной папке.
        /// При необходимости директория создается. Плагин контролирует очистку директории от старых файлов.
        /// </summary>
        public ReportPlug()
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
                if (Directory.GetCreationTime(directory).AddDays(7) < DateTime.Now)
                    Directory.Delete(directory, true);
            this.temporary_path = report_module_path;
        }

        /// <summary>
        /// Установить файл шаблона
        /// </summary>
        /// <param name="fileName">Полный путь до файла шаблона</param>
		public void ReportSetTemplateFile(string fileName)
		{
            if (File.Exists(fileName))
                this.template_file = fileName;
            else
                if (fileName != null && File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName)))
                    this.template_file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            else
            {
                ReportException exception = new ReportException("Указанный файл \"{0}\" шаблона отчета не существует");
                exception.Data.Add("{0}", fileName);
                throw exception;
            }
		}

        /// <summary>
        /// Установить строковый параметр замены
        /// </summary>
        /// <param name="name">Шаблон замены"</param>
        /// <param name="value">Подставляемое значение</param>
        public void ReportSetStringValue(string name, string value)
		{
            Values.Add(new StringReportValue(name, (value == null) ? "" : value));             
		}

        /// <summary>
        /// Групповая установка строковых параметров замены
        /// </summary>
        /// <param name="values">Список подставляемых значений</param>
        public void ReportSetStringValues(ReportRow values)
        {
            if (values == null)
                throw new ReportException("Не задана ссылка на список подставляемых значений шаблона");
            for (int i = 0; i < values.Count; i++)
                this.Values.Add(new StringReportValue(values[i].Row.Table.Columns[i], (values[i].Value == null) ? "" : values[i].Value));
        }

        /// <summary>
        /// Установить списочный (табличный) параметр замены
        /// </summary>
        /// <param name="table">Список параметров</param>
        /// <param name="xmlContractor">XML-замыкатель. Поддерживаемые замыкатели по умолчанию table, row, cell, p</param>
        public void ReportSetTableValue(ReportTable table, XmlContractor xmlContractor)
		{
            if (table == null)
                throw new ReportException("Не передана ссылка ана объект класса ReportTable");
            Values.Add(new TableReportValue(table, xmlContractor.ToString()));
		}
        
        /// <summary>
        /// Функция генерации отчета
        /// </summary>
        /// <param name="fileName">Путь до выходного файла отчета</param>
        public void ReportGenerate(out string fileName)
        {
            if (!File.Exists(template_file))
            {
                ReportException exception = new ReportException("Указанный файл \"{0}\" шаблона отчета не существует");
                exception.Data.Add("{0}", template_file);
                throw exception;
            }
            string report_filename = Guid.NewGuid().ToString();
            FileInfo template_file_info = new FileInfo(template_file);
            //Распаковываем файл шаблона во временную директорию
            FastZip zip = new FastZip();
            string report_unzip_path = Path.Combine(temporary_path, report_filename);
            Console.WriteLine("Распаковываем файл шаблона во временную директорию");
            zip.ExtractZip(template_file, report_unzip_path, "");
            //Формируем отчет
            ReportEditing(report_unzip_path, template_file_info.Extension);
            //Запаковываем файл шаблона и удаляем временную директорию отчета
            Console.WriteLine("Запаковываем файл отчета и удаляем временную директорию шаблона");
            string report_full_filename = Path.Combine(temporary_path, report_filename + template_file_info.Extension);
            zip.CreateZip(report_full_filename, report_unzip_path, true, "");
            Directory.Delete(report_unzip_path, true);
            fileName = report_full_filename;
        }

        /// <summary>
        /// Функция определения типа документа и вызова соответсвующего редактора отчета
        /// </summary>
        /// <param name="report_unzip_path">Путь до распакованного документа</param>
        /// <param name="extension">Расширение файла</param>
        private void ReportEditing(string report_unzip_path, string extension)
        {
            ReportEditor editor;
            if (File.Exists(Path.Combine(report_unzip_path, "content.xml")) && (extension == ".odt" || extension == ".ott"))
                editor = new WriterEditor();
            else
            if (File.Exists(Path.Combine(report_unzip_path, "content.xml")) && (extension == ".ods" || extension == ".ots"))
                editor = new CalcEditor();
            else
                if (File.Exists(Path.Combine(report_unzip_path, @"word" + Path.DirectorySeparatorChar + "document.xml")) &&
                    (extension == ".docx" || extension == ".docm" || extension == ".dotx" || extension == ".dotm"))
                editor = new WordEditor();
            else
            if (Directory.Exists(Path.Combine(report_unzip_path, "xl")) &&
                    (extension == ".xlsx" || extension == ".xlsm" || extension == ".xltx" || extension == ".xltm"))
                editor = new ExcelEditor();
            else
                throw new ReportException("Формат файла шаблона некорректный");
            Values = editor.XmlContractorsConvert(Values);
            editor.ReportEditing(report_unzip_path, Values);
            editor.SpecialTagEditing(report_unzip_path);
        }
    }

    /// <summary>
    /// Класс исключения модуля ReportPlug
    /// </summary>
    [Serializable()]
    public class ReportException : Exception
    {
        /// <summary>
        /// Конструктор класса исключения ReportException
        /// </summary>
        public ReportException() : base() { }

        /// <summary>
        /// Конструктор класса исключения ReportException
        /// </summary>
        /// <param name="message">Сообщение об ошибке</param>
        public ReportException(string message) : base(message) { }

        /// <summary>
        /// Конструктор класса исключения ReportException
        /// </summary>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="innerException">Вложенное исключение</param>
        public ReportException(string message, Exception innerException) : base(message, innerException) { }

        /// <summary>
        /// Конструктор класса исключения ReportException
        /// </summary>
        /// <param name="info">Информация сериализации</param>
        /// <param name="context">Контекст потока</param>
        protected ReportException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
