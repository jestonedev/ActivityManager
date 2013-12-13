using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

//Плагин для работы с вводом-выводом
namespace io_module
{
    //Доступный из-вне интерфейс плагина
    public interface IPlugin
    {
        void io_open_file(string file_name, string arguments);
    }

    public class IOPlugin: IPlugin
    {
        /// <summary>
        /// Метод для запуска на исполнение программ и открытия файлов документов
        /// </summary>
        /// <param name="file_name">Путь до программы или документа</param>
        /// <param name="arguments">Аргументы командной строки</param>
        public void io_open_file(string file_name, string arguments)
        {
            if (!File.Exists(file_name))
            {
                ApplicationException exception = new ApplicationException("Файл {0} не существует");
                exception.Data.Add("{0}", file_name);
                throw exception;
            }
            Process proc = new Process();
            proc.StartInfo.FileName = file_name;
            proc.StartInfo.Arguments = arguments;
            proc.StartInfo.UseShellExecute = true;
            proc.Start();
        }
    }
}
