using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Security.Permissions;
using System.Runtime.Serialization;
using System.Windows.Forms;

//Плагин для работы с вводом-выводом
namespace IOModule
{
    /// <summary>
    /// Доступный из-вне интерфейс плагина
    /// </summary>
    public interface IPlug
    {
        /// <summary>
        /// Метод для запуска на исполнение программ и открытия файлов документов
        /// </summary>
        /// <param name="fileName">Путь до программы или документа</param>
        /// <param name="arguments">Аргументы командной строки</param>
        [PermissionSetAttribute(SecurityAction.LinkDemand, Name = "FullTrust")]
        void IOOpenFile(string fileName, string arguments);

        /// <summary>
        /// Вывод отладочного сообщения
        /// </summary>
        /// <param name="message">Текст сообщения</param>
        void IODebugMessage(string message);

        /// <summary>
        /// Условный переход на указанный шаг
        /// </summary>
        /// <param name="condition">Если true - то переходить, если false - пропустить</param>
        /// <param name="step">Шаг, на который необходимо перейти</param>
        void IOIfCondititonToStep(bool condition, int step);

        /// <summary>
        /// Условный переход на конец программы
        /// </summary>
        /// <param name="condition">Если true - то переходить, если false - пропустить</param>
        void IOIfCondititonExit(bool condition);

        /// <summary>
        /// Вывод в консоль
        /// </summary>
        /// <param name="text">Текст, который необходимо выводить в консоль</param>
        void IOConsole(string text);
    }

    /// <summary>
    /// Класс, реализующий интерфейс IPlug
    /// </summary>
    public class IOPlug: IPlug
    {
        /// <summary>
        /// Метод для запуска на исполнение программ и открытия файлов документов
        /// </summary>
        /// <param name="fileName">Путь до программы или документа</param>
        /// <param name="arguments">Аргументы командной строки</param>
        [PermissionSetAttribute(SecurityAction.LinkDemand, Name="FullTrust")]
        public void IOOpenFile(string fileName, string arguments)
        {
            if (!File.Exists(fileName))
            {
                IOException exception = new IOException("Файл {0} не существует");
                exception.Data.Add("{0}", fileName);
                throw exception;
            }
            using (Process proc = new Process())
            {
                proc.StartInfo.FileName = fileName;
                proc.StartInfo.Arguments = arguments;
                proc.StartInfo.UseShellExecute = true;
                proc.Start();
            }
        }

        /// <summary>
        /// Вывод отладочного сообщения
        /// </summary>
        /// <param name="message">Текст сообщения</param>
        public void IODebugMessage(string message)
        {
            MessageBox.Show(message, "Отладка", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Условный переход на указанный шаг
        /// </summary>
        /// <param name="condition">Если true - то переходить, если false - пропустить</param>
        /// <param name="step">Шаг, на который необходимо перейти</param>
        public void IOIfCondititonToStep(bool condition, int step)
        {
            if (condition)
            {
                IfConditionException exception = new IfConditionException();
                exception.Data.Add("step", step);
                throw exception;
            }
        }

        /// <summary>
        /// Условный переход на конец программы
        /// </summary>
        /// <param name="condition">Если true - то переходить, если false - пропустить</param>
        public void IOIfCondititonExit(bool condition)
        {
            if (condition)
            {
                IfConditionException exception = new IfConditionException();
                exception.Data.Add("step", Int32.MaxValue);
                throw exception;
            }
        }

        /// <summary>
        /// Вывод в консоль
        /// </summary>
        /// <param name="text">Текст, который необходимо выводить в консоль</param>
        public void IOConsole(string text)
        {
            Console.WriteLine(text);
        }
    }

    /// <summary>
    /// Класс исключения модуля IOPlug
    /// </summary>
    [Serializable()]
    public class IOException : Exception
    {
        /// <summary>
        /// Конструктор класса исключения IOException
        /// </summary>
        public IOException() : base() { }
        
        /// <summary>
        /// Конструктор класса исключения IOException
        /// </summary>
        /// <param name="message">Сообщение об ошибке</param>
        public IOException(string message) : base(message) { }

        /// <summary>
        /// Конструктор класса исключения IOException
        /// </summary>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="innerException">Вложенное исключение</param>
        public IOException(string message, Exception innerException) : base(message, innerException) { }

        /// <summary>
        /// Конструктор класса исключения IOException
        /// </summary>
        /// <param name="info">Информация сериализации</param>
        /// <param name="context">Контекст потока</param>
        protected IOException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    /// <summary>
    /// Класс исключения - прерывание и смена контекста выполнения
    /// </summary>
    [Serializable()]
    public class IfConditionException : IOException
    {
        /// <summary>
        /// Конструктор класса исключения IOException
        /// </summary>
        public IfConditionException() : base() { }

        /// <summary>
        /// Конструктор класса исключения IOException
        /// </summary>
        /// <param name="message">Сообщение об ошибке</param>
        public IfConditionException(string message) : base(message) { }

        /// <summary>
        /// Конструктор класса исключения IOException
        /// </summary>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="innerException">Вложенное исключение</param>
        public IfConditionException(string message, Exception innerException) : base(message, innerException) { }

        /// <summary>
        /// Конструктор класса исключения IOException
        /// </summary>
        /// <param name="info">Информация сериализации</param>
        /// <param name="context">Контекст потока</param>
        protected IfConditionException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
