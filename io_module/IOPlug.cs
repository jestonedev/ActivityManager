using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Security.Permissions;
using System.Runtime.Serialization;

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
            Process proc = new Process();
            proc.StartInfo.FileName = fileName;
            proc.StartInfo.Arguments = arguments;
            proc.StartInfo.UseShellExecute = true;
            proc.Start();
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
}
