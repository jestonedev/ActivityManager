using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ExtendedTypes;
using System.Runtime.Serialization;
using QueryTextDriver;
using DataTypes;

namespace TextDataSource
{
    
	/// <summary>
    /// Доступный из-вне интерфейс плагина
	/// </summary>
    public interface IPlug
    {
        /// <summary>
        /// Выборка данных из текстового файла
        /// </summary>
        /// <param name="query">Запрос к текстовому файлу в формате Sql</param>
        /// <param name="columnSeparator">Разделитель колонок</param>
        /// <param name="rowSeparator">Разделитель строк</param>
        /// <param name="firstRowHeader">Является ли первая строка файла(ов) заголовком</param>
        /// <param name="ignoreDataTypes">Необходимо ли автоматически определять типы данных, или считать все значения строками</param>
        /// <param name="table">Результат запроса</param>
        void TextSelectTable(string query, string columnSeparator, string rowSeparator, bool firstRowHeader, bool ignoreDataTypes, out ReportTable table);

        /// <summary>
        /// Выборка скалярного значения из базы данных
        /// </summary>
        /// <param name="query">Запрос на выполнение</param>
        /// <param name="columnSeparator">Разделитель колонок</param>
        /// <param name="rowSeparator">Разделитель строк</param>
        /// <param name="firstRowHeader">Является ли первая строка файла(ов) заголовком</param>
        /// <param name="ignoreDataTypes">Необходимо ли автоматически определять типы данных, или считать все значения строками</param>
        /// <param name="result">Возврат скалярного значения</param>
        void TextSelectScalar(string query, string columnSeparator, string rowSeparator, bool firstRowHeader, bool ignoreDataTypes, out Object result);

        /// <summary>
        /// Запрос на внесение изменений в текстовом файле
        /// </summary>
        /// <param name="query">Запрос на выполнение</param>
        /// <param name="columnSeparator">Разделитель колонок</param>
        /// <param name="rowSeparator">Разделитель строк</param>
        /// <param name="firstRowHeader">Является ли первая строка файла(ов) заголовком</param>
        /// <param name="ignoreDataTypes">Необходимо ли автоматически определять типы данных, или считать все значения строками</param>
        /// <param name="rowsAffected">Число измененных строк</param>
        void TextModifyQuery(string query, string columnSeparator, string rowSeparator, bool firstRowHeader, bool ignoreDataTypes, out int rowsAffected);
    }

    /// <summary>
    /// Класс, реализующий интерфейс плагина
    /// </summary>
    public class TextDataSourcePlug : IPlug
    {
        /// <summary>
        /// Выборка данных из базы данных
        /// </summary>
        /// <param name="query">Запрос к базе данных в формате Sql</param>
        /// <param name="columnSeparator">Разделитель колонок</param>
        /// <param name="rowSeparator">Разделитель строк. По умолчанию перенос строки</param>
        /// <param name="firstRowHeader">Является ли первая строка файла(ов) источника(ов) заголовком</param>
        /// <param name="ignoreDataTypes">Необходимо ли автоматически определять типы данных, или считать все значения строками</param>
        /// <param name="table">Результат запроса</param>
        public void TextSelectTable(string query, string columnSeparator, string rowSeparator, bool firstRowHeader, bool ignoreDataTypes, out ReportTable table)
        {
            QueryExecutor executor = new QueryExecutor(columnSeparator, rowSeparator, firstRowHeader, ignoreDataTypes);
            TableJoin resultJoin = executor.Execute(query);
            //Конвертируем результат в ReportTable
            ReportTable resultTable = new ReportTable();
            //Инициализируем колонки
            for (int i = 0; i < resultJoin.Columns.Count; i++)
                if (String.IsNullOrEmpty(resultJoin.Columns[i].ColumnAlias))
                    resultTable.Columns.Add(resultJoin.Columns[i].ColumnName.Trim(new char[] {'`'}));
                else
                    resultTable.Columns.Add(resultJoin.Columns[i].ColumnAlias.Trim(new char[] { '`' }));
            //Заполняем данными
            for (int i = 0; i < resultJoin.Rows.Count; i++)
            {
                ReportRow row = new ReportRow(resultTable);
                for (int j = 0; j < resultJoin.Rows[i].Cells.Count; j++)
                {
                    ReportCell cell = new ReportCell(row);
                    cell.Value = resultJoin.Rows[i].Cells[j].Value.AsString().Value();
                    row.Add(cell);
                }
                resultTable.Add(row);
            }
            table = resultTable;
        }

        /// <summary>
        /// Выборка скалярного значения из базы данных
        /// </summary>
        /// <param name="query">Запрос на выполнение</param>
        /// <param name="columnSeparator">Разделитель колонок</param>
        /// <param name="rowSeparator">Разделитель строк</param>
        /// <param name="firstRowHeader">Является ли первая строка файла(ов) заголовком</param>
        /// <param name="ignoreDataTypes">Необходимо ли автоматически определять типы данных, или считать все значения строками</param>
        /// <param name="result">Возврат скалярного значения</param>
        public void TextSelectScalar(string query, string columnSeparator, string rowSeparator, bool firstRowHeader, bool ignoreDataTypes, out Object result)
        {
            QueryExecutor executor = new QueryExecutor(columnSeparator, rowSeparator, firstRowHeader, ignoreDataTypes);
            TableJoin resultJoin = executor.Execute(query);
            if ((resultJoin.Columns.Count != 1) && (resultJoin.Rows.Count != 1))
            {
                TextDataSourceException exception = new TextDataSourceException("Запрос {0} вернул нескалярное значение");
                exception.Data.Add("{0}", query);
                throw exception;
            }
            result = resultJoin.Rows[0].Cells[0].Value.AsString().Value();
        }

        /// <summary>
        /// Запрос на внесение изменений в текстовом файле
        /// </summary>
        /// <param name="query">Запрос на выполнение</param>
        /// <param name="columnSeparator">Разделитель колонок</param>
        /// <param name="rowSeparator">Разделитель строк</param>
        /// <param name="firstRowHeader">Является ли первая строка файла(ов) заголовком</param>
        /// <param name="ignoreDataTypes">Необходимо ли автоматически определять типы данных, или считать все значения строками</param>
        /// <param name="rowsAffected">Число измененных строк</param>
        public void TextModifyQuery(string query, string columnSeparator, string rowSeparator, bool firstRowHeader, bool ignoreDataTypes, out int rowsAffected)
        {
            QueryExecutor executor = new QueryExecutor(columnSeparator, rowSeparator, firstRowHeader, ignoreDataTypes);
            executor.Execute(query);
            rowsAffected = executor.RowsAffected;
        }
    }

    /// <summary>
    /// Класс исключения модуля CSVModule
    /// </summary>
    [Serializable()]
    public class TextDataSourceException : Exception
    {
        /// <summary>
        /// Конструктор класса исключения CSVException
        /// </summary>
        public TextDataSourceException() : base() { }

        /// <summary>
        /// Конструктор класса исключения CSVException
        /// </summary>
        /// <param name="message">Сообщение об ошибке</param>
        public TextDataSourceException(string message) : base(message) { }

        /// <summary>
        /// Конструктор класса исключения CSVException
        /// </summary>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="innerException">Вложенное исключение</param>
        public TextDataSourceException(string message, Exception innerException) : base(message, innerException) { }

        /// <summary>
        /// Конструктор класса исключения CSVException
        /// </summary>
        /// <param name="info">Информация сериализации</param>
        /// <param name="context">Контекст потока</param>
        protected TextDataSourceException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
