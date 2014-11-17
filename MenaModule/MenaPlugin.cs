using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExtendedTypes;
using FSLib.Declension;
using System.Runtime.Serialization;

//Модуль специальных функций программы Мена
namespace MenaModule
{
    /// <summary>
    /// Доступный из-вне интерфейс плагина
    /// </summary>
    public interface IPlug
    {
        /// <summary>
        /// Специфичный метод конвертации таблицы подписчиков соглашения
        /// </summary>
        /// <param name="inTable">Входная таблица</param>
        /// <param name="outTable">Выходная таблица</param>
        void MenaSignersTableConvert(ReportTable inTable, out ReportTable outTable);

        /// <summary>
        /// Специфический метод конвертации этажа. Исключает фразы вроде "нулевом этаже".
        /// </summary>
        /// <param name="inRow">Строка параметров отчета, в которой производится замена</param>
        /// <param name="columnFloorName">Имя колонки с строковым наименованием этажа</param>
        /// <param name="outRow">Выходная строка параметров отчета</param>
        void MenaFloorTemplate(ReportRow inRow, string columnFloorName, out ReportRow outRow);
    }

    /// <summary>
    /// Класс, реализующий интерфейс плагина
    /// </summary>
    public class MenaPlug: IPlug
    {
        /// <summary>
        /// Специфичный метод конвертации таблицы подписчиков соглашения
        /// </summary>
        /// <param name="inTable">Входная таблица</param>
        /// <param name="outTable">Выходная таблица</param>
        public void MenaSignersTableConvert(ReportTable inTable, out ReportTable outTable)
        {
            if (inTable == null)
                throw new MenaException("Входная таблица не задана");
            ReportTable tmp_table = new ReportTable();
            foreach(string column in inTable.Columns)
                if (column != "text_case" && column != "warrant_fio")
                {
                    tmp_table.Columns.Add(column);
                }
            foreach (ReportRow rr in inTable)
            {
                ReportRow tmp_row = new ReportRow(tmp_table);
                string fio = "";
                string template = rr["person_warrant"].Value;
                if (!String.IsNullOrEmpty(rr["warrant_fio"].Value.Trim()))
                {
                    if (rr["text_case"].Value == "Nominative")
                        fio = Declension1251.GetNominativeDeclension(rr["warrant_fio"].Value);
                    else
                        fio = rr["warrant_fio"].Value;
                    string[] fio_arr = fio.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (fio_arr.Length != 3)
                    {
                        MenaException exception = new MenaException("Имя участника договора {0} задано не полностью или не в корректном формате");
                        exception.Data.Add("{0}", fio);
                        throw exception;
                    }
                    template = template.Replace("%fio%", fio_arr[0] + " " + fio_arr[1][0] + "." + fio_arr[2][0] + ".");
                }
                tmp_row.Add(new ReportCell(tmp_row, rr["person_fio"].Value));
                tmp_row.Add(new ReportCell(tmp_row, template));
                tmp_row.Add(new ReportCell(tmp_row, rr["person_fio_short"].Value));
                tmp_table.Add(tmp_row);
            }
            outTable = tmp_table;
        }

        /// <summary>
        /// Специфический метод конвертации этажа. Исключает фразы вроде "нулевом этаже".
        /// </summary>
        /// <param name="inRow">Строка параметров отчета, в которой производится замена</param>
        /// <param name="columnFloorName">Имя колонки с строковым наименованием этажа</param>
        /// <param name="outRow">Выходная строка параметров отчета</param>
        public void MenaFloorTemplate(ReportRow inRow, string columnFloorName, out ReportRow outRow)
        {
            if (inRow == null)
                throw new MenaException("Входная строка не задана");
            if (inRow[columnFloorName].Value == "нулевом")
                inRow[columnFloorName].Value = " ";
            else
                inRow[columnFloorName].Value = " расположена на " + inRow[columnFloorName].Value + " этаже, ";
            outRow = inRow;
        }
    }

    /// <summary>
    /// Класс исключения модуля MenaPlugin
    /// </summary>
    [Serializable()]
    public class MenaException : Exception
    {
        /// <summary>
        /// Конструктор класса исключения MenaException
        /// </summary>
        public MenaException() : base() { }

        /// <summary>
        /// Конструктор класса исключения MenaException
        /// </summary>
        /// <param name="message">Сообщение об ошибке</param>
        public MenaException(string message) : base(message) { }

        /// <summary>
        /// Конструктор класса исключения MenaException
        /// </summary>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="innerException">Вложенное исключение</param>
        public MenaException(string message, Exception innerException) : base(message, innerException) { }

        /// <summary>
        /// Конструктор класса исключения MenaException
        /// </summary>
        /// <param name="info">Информация сериализации</param>
        /// <param name="context">Контекст потока</param>
        protected MenaException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
