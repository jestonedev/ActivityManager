﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExtendedTypes;
using System.Data;
using Declensions.Unicode;
using System.Text.RegularExpressions;
using System.Runtime.Serialization;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Globalization;
using System.IO;
using System.Reflection;

namespace ConvertModule
{
    /// <summary>
    /// Доступный из-вне интерфейс плагина
    /// </summary>
    public interface IPlug
    {

        /// <summary>
        /// Функция конвертации целого числа в текст
        /// </summary>
        /// <param name="number">Число</param>
        /// <param name="textCase">Падеж</param>
        /// <param name="sex">Пол</param>
        /// <param name="firstCapital">Ставить первую букву прописной</param>
        /// <param name="isOrdinal">Если true, то порядковое числительно, если false, то количественное</param>
        /// <param name="words">Возвращаемый текст</param>
        void ConvertIntToString(long number, TextCase textCase, Sex sex, bool firstCapital, bool isOrdinal, out string words);

        /// <summary>
        /// Действие конвертации ячейки с числом в строковое представление чисел
        /// </summary>
        /// <param name="inRow">Входная строка</param>
        /// <param name="column">Имя колонки (или перечень колонок через запятую)</param>
        /// <param name="textCase">Падеж</param>
        /// <param name="sex">Пол</param>
        /// <param name="firstCapital">Ставить первую букву прописной или нет</param>
        /// <param name="isOrdinal">Если true, то порядковое числительно, если false, то количественное</param>
        /// <param name="outRow">Выходная строка</param>
        void ConvertIntCellToString(ReportRow inRow, string column, TextCase textCase, Sex sex, bool firstCapital, bool isOrdinal, out ReportRow outRow);

        /// <summary>
        /// Действие конвертации всех значений колонки таблицы в строковое представление чисел
        /// </summary>
        /// <param name="inTable">Входная таблица</param>
        /// <param name="column">Имя колонки (или перечень колонок через запятую)</param>
        /// <param name="textCase">Падеж</param>
        /// <param name="sex">Пол</param>
        /// <param name="firstCapital">Ставить первую букву прописной или нет</param>
        /// <param name="isOrdinal">Если true, то порядковое числительно, если false, то количественное</param>
        /// <param name="outTable">Выходная таблица</param>
        void ConvertIntColToString(ReportTable inTable, string column, TextCase textCase, Sex sex, bool firstCapital, bool isOrdinal, out ReportTable outTable);

        /// <summary>
        /// Форматирование даты и времени в виде текста
        /// </summary>
        /// <param name="dateTime">Дата и время</param>
        /// <param name="format">
        /// Формат даты и времени. Можно задавать стандартный формат даты и времени, либо расширенный вариант:
        /// ddx - день месяца в виде текста
        /// MMx - месяц в виде текста
        /// yyx - год в формате двухзначного числа в виде текста
        /// yyyyx - год в формате четырехзначного числа в виде текста
        /// hhx - время от 1 до 12 в виде текста
        /// HHx - время от 0 до 23 в виде текста
        /// mmx - минуты в виде текста
        /// ssx - секунды в виде текста
        /// Буква "х" во всех расширенных форматах - первая буква падежа n,g,d,a,i,p
        /// </param>
        /// <param name="firstCapital">Ставить первую букву прописной или нет</param>
        /// <param name="words">Возвращаемый текст</param>
        void ConvertDateTimeToString(DateTime dateTime, string format, bool firstCapital, out string words);

        /// <summary>
        /// Действие конвертации ячейки с датой в форматированное представление даты
        /// </summary>
        /// <param name="inRow">Входная строка</param>
        /// <param name="column">Имя колонки (или перечень колонок через запятую)</param>
        /// <param name="format">
        /// Формат даты и времени. Можно задавать стандартный формат даты и времени, либо расширенный вариант:
        /// ddx - день месяца в виде текста
        /// MMx - месяц в виде текста
        /// yyx - год в формате двухзначного числа в виде текста
        /// yyyyx - год в формате четырехзначного числа в виде текста
        /// hhx - время от 1 до 12 в виде текста
        /// HHx - время от 0 до 23 в виде текста
        /// mmx - минуты в виде текста
        /// ssx - секунды в виде текста
        /// Буква "х" во всех расширенных форматан - первая буква падежа n,g,d,a,i,p
        /// </param>
        /// <param name="firstCapital">Ставить первую букву прописной или нет</param>
        /// <param name="outRow">Выходная строка</param>
        void ConvertDateTimeCellToString(ReportRow inRow, string column, string format, bool firstCapital, out ReportRow outRow);

        /// <summary>
        /// Действие конвертации всех значений колонки таблицы в форматированное представление даты
        /// </summary>
        /// <param name="inTable">Входная таблица</param>
        /// <param name="column">Имя колонки (или перечень колонок через запятую)</param>
        /// <param name="format">
        /// Формат даты и времени. Можно задавать стандартный формат даты и времени, либо расширенный вариант:
        /// ddx - день месяца в виде текста
        /// MMx - месяц в виде текста
        /// yyx - год в формате двухзначного числа в виде текста
        /// yyyyx - год в формате четырехзначного числа в виде текста
        /// hhx - время от 1 до 12 в виде текста
        /// HHx - время от 0 до 23 в виде текста
        /// mmx - минуты в виде текста
        /// ssx - секунды в виде текста
        /// Буква "х" во всех расширенных форматан - первая буква падежа n,g,d,a,i,p
        /// </param>
        /// <param name="firstCapital">Ставить первую букву прописной или нет</param>
        /// <param name="outTable">Выходная таблица</param>
        void ConvertDateTimeColToString(ReportTable inTable, string column, string format, bool firstCapital, out ReportTable outTable);

        /// <summary>
        /// Действие конвертации суммы в строку
        /// </summary>
        /// <param name="currency">Сумма</param>
        /// <param name="currencyType">Тип валюты</param>
        /// <param name="format">Формат строки вывода суммы:
        /// ii - рубли (доллары, евро) в виде числа
        /// ff - копейки (центы) в виде числа
        /// iix - рубли (доллары, евро) в виде строки
        /// ffx - копейки (центы) в виде строки
        /// rx - слово "рубль" ("доллар", "евро")
        /// kx - словок "копейка" ("цент")
        /// Буква "х" во всех форматах - первая буква падежа n,g,d,a,i,p
        /// </param>
        /// <param name="thousandSeparator">Разделитель порядков</param>
        /// <param name="firstCapital">Ставить первую букву прописной или нет</param>
        /// <param name="isOrdinal">Если true, то порядковое числительно, если false, то количественное</param>
        /// <param name="words">Возвращаемый текст</param>
        void ConvertCurrencyToString(decimal currency, CurrencyType currencyType, string format, string thousandSeparator, bool firstCapital, bool isOrdinal, out string words);

        /// <summary>
        /// Действие конвертации значения ячейки с суммой в строковое представление суммы
        /// </summary>
        /// <param name="inRow">Входная строка</param>
        /// <param name="column">Имя колонки (или перечень колонок через запятую)</param>
        /// <param name="currencyType">Тип валюты</param>
        /// <param name="format">Формат строки вывода суммы:
        /// ii - рубли (доллары, евро) в виде числа
        /// ff - копейки (центы) в виде числа
        /// iix - рубли (доллары, евро) в виде строки
        /// ffx - копейки (центы) в виде строки
        /// rx - слово "рубль" ("доллар", "евро")
        /// kx - словок "копейка" ("цент")
        /// nn - слово "минус ", если число отрицательное. Если число положительное, то пустая строка. Пробел после слова минус ставится автоматически.
        /// n - знак "-", если число отрицательное. Если число положительное, то знак не ставится. Пробел после знака "-" автоматически НЕ ставится.
        /// Буква "х" во всех форматах - первая буква падежа n,g,d,a,i,p
        /// </param>
        /// <param name="thousandSeparator">Разделитель порядков</param>
        /// <param name="firstCapital">Ставить первую букву прописной или нет</param>
        /// <param name="isOrdinal">Если true, то порядковое числительно, если false, то количественное</param>
        /// <param name="outRow">Выходная строка</param>
        void ConvertCurrencyCellToString(ReportRow inRow, string column, CurrencyType currencyType, string format, string thousandSeparator, bool firstCapital, bool isOrdinal, out ReportRow outRow);

        /// <summary>
        /// Действие конвертации всех значений колонки таблицы в строковое представление суммы
        /// </summary>
        /// <param name="inTable">Входная таблица</param>
        /// <param name="column">Имя колонки (или перечень колонок через запятую)</param>
        /// <param name="currencyType">Тип валюты</param>
        /// <param name="format">Формат строки вывода суммы:
        /// ii - рубли (доллары, евро) в виде числа
        /// ff - копейки (центы) в виде числа
        /// iix - рубли (доллары, евро) в виде строки
        /// ffx - копейки (центы) в виде строки
        /// rx - слово "рубль" ("доллар", "евро")
        /// kx - словок "копейка" ("цент")
        /// nn - слово "минус ", если число отрицательное. Если число положительное, то пустая строка. Пробел после слова минус ставится автоматически.
        /// n - знак "-", если число отрицательное. Если число положительное, то знак не ставится. Пробел после знака "-" автоматически НЕ ставится.
        /// Буква "х" во всех форматах - первая буква падежа n,g,d,a,i,p
        /// </param>
        /// <param name="thousandSeparator">Разделитель порядков</param>
        /// <param name="firstCapital">Ставить первую букву прописной или нет</param>
        /// <param name="isOrdinal">Если true, то порядковое числительно, если false, то количественное</param>
        /// <param name="outTable">Выходная таблица</param>
        void ConvertCurrencyColToString(ReportTable inTable, string column, CurrencyType currencyType, string format, string thousandSeparator, bool firstCapital, bool isOrdinal, out ReportTable outTable);

        /// <summary>
        /// Функция конвертации вещественного числа в текст
        /// </summary>
        /// <param name="number">Число</param>
        /// <param name="textCase">Падеж</param>
        /// <param name="firstCapital">Ставить первую букву прописной</param>
        /// <param name="words">Возвращаемый текст</param>
        void ConvertFloatToString(decimal number, TextCase textCase, bool firstCapital, out string words);

        /// <summary>
        /// Действие конвертации значения ячейки с вещественным числом в троку
        /// </summary>
        /// <param name="inRow">Входная строка</param>
        /// <param name="column">Имя колонки (или перечень колонок через запятую)</param>
        /// <param name="textCase">Падеж</param>
        /// <param name="firstCapital">Ставить первую букву прописной или нет</param>
        /// <param name="outRow">Выходная строка</param>
        void ConvertFloatCellToString(ReportRow inRow, string column, TextCase textCase, bool firstCapital, out ReportRow outRow);

        /// <summary>
        /// Действие конвертации всех значений колонки таблицы в строковое представление вещественного числа
        /// </summary>
        /// <param name="inTable">Входная таблица</param>
        /// <param name="column">Имя колонки (или перечень колонок через запятую)</param>
        /// <param name="textCase">Падеж</param>
        /// <param name="firstCapital">Ставить первую букву прописной или нет</param>
        /// <param name="outTable">Выходная таблица</param>
        void ConvertFloatColToString(ReportTable inTable, string column, TextCase textCase, bool firstCapital, out ReportTable outTable);

        /// <summary>
        /// Действие перевода ФИО в указанный падеж
        /// </summary>
        /// <param name="nameIn">ФИО в именительном падеже</param>
        /// <param name="format">Формат ФИО:
        /// ss - полное представление фамилии
        /// s - первая буква фамилии
        /// nn - полное представление имени
        /// n - первая буква имени
        /// pp - полное представление отчества 
        /// p - первая буква отчества
        /// </param>
        /// <param name="textCase">Падеж</param>
        /// <param name="nameOut">Выходная строка с ФИО</param>
        void ConvertNameToCase(string nameIn, string format, TextCase textCase, out string nameOut);

        /// <summary>
        /// Перевод ячейки с ФИО в указанный падеж
        /// </summary>
        /// <param name="inRow">Входная строка</param>
        /// <param name="column">Имя колонки (или перечень колонок через запятую)</param>
        /// <param name="format">Формат ФИО:
        /// ss - полное представление фамилии
        /// s - первая буква фамилии
        /// nn - полное представление имени
        /// n - первая буква имени
        /// pp - полное представление отчества 
        /// p - первая буква отчества
        /// </param>
        /// <param name="textCase">Падеж</param>
        /// <param name="outRow">Выходная строка</param>
        void ConvertNameCellToCase(ReportRow inRow, string column, string format, TextCase textCase, out ReportRow outRow);

        /// <summary>
        /// Перевод всех значений колонки с ФИО в указанный падеж
        /// </summary>
        /// <param name="inTable">Входная таблица</param>
        /// <param name="column">Имя колонки (или перечень колонок через запятую)</param>
        /// <param name="format">Формат ФИО:
        /// ss - полное представление фамилии
        /// s - первая буква фамилии
        /// nn - полное представление имени
        /// n - первая буква имени
        /// pp - полное представление отчества 
        /// p - первая буква отчества
        /// </param>
        /// <param name="textCase">Падеж</param>
        /// <param name="outTable">Выходная таблица</param>
        void ConvertNameColToCase(ReportTable inTable, string column, string format, TextCase textCase, out ReportTable outTable);

        /// <summary>
        /// Действие перевода должности в указанный падеж 
        /// </summary>
        /// <param name="postIn">Должность в именительном падеже</param>
        /// <param name="textCase">Падеж</param>
        /// <param name="postOut">Выходная строка с должностью</param>
        void ConvertPostToCase(string postIn, TextCase textCase, out string postOut);

        /// <summary>
        /// Действие перевода ячейки с должностью в указанный падеж 
        /// </summary>
        /// <param name="inRow">Входная строка</param>
        /// <param name="column">Имя колонки (или перечень колонок через запятую)</param>
        /// <param name="textCase">Падеж</param>
        /// <param name="outRow">Выходная строка</param>
        void ConvertPostCellToCase(ReportRow inRow, string column, TextCase textCase, out ReportRow outRow);

        /// <summary>
        /// Действие перевода колонки с должностью в указанный падеж 
        /// </summary>
        /// <param name="inTable">Входная таблица</param>
        /// <param name="column">Имя колонки (или перечень колонок через запятую)</param>
        /// <param name="textCase">Падеж</param>
        /// <param name="outTable">Выходная таблица</param>
        void ConvertPostColToCase(ReportTable inTable, string column, TextCase textCase, out ReportTable outTable);

        /// <summary>
        /// Действие перевода названия подразделения или предприятия в указанный падеж 
        /// </summary>
        /// <param name="officeIn">Название подразделения или предприятия в именительном падеже</param>
        /// <param name="textCase">Падеж</param>
        /// <param name="officeOut">Выходная строка с названием подразделения или предприятия</param>
        void ConvertOfficeToCase(string officeIn, TextCase textCase, out string officeOut);

        /// <summary>
        /// Действие перевода ячейки с названием подразделения или предприятия в указанный падеж 
        /// </summary>
        /// <param name="inRow">Входная строка</param>
        /// <param name="column">Имя колонки (или перечень колонок через запятую)</param>
        /// <param name="textCase">Падеж</param>
        /// <param name="outRow">Выходная строка</param>
        void ConvertOfficeCellToCase(ReportRow inRow, string column, TextCase textCase, out ReportRow outRow);

        /// <summary>
        /// Действие перевода колонки с названием подразделения или предприятия в указанный падеж 
        /// </summary>
        /// <param name="inTable">Входная таблица</param>
        /// <param name="column">Имя колонки (или перечень колонок через запятую)</param>
        /// <param name="textCase">Падеж</param>
        /// <param name="outTable">Выходная таблица</param>
        void ConvertOfficeColToCase(ReportTable inTable, string column, TextCase textCase, out ReportTable outTable);

        /// <summary>
        /// Действие объединения всех ячеек строки данных в одну строку
        /// </summary>
        /// <param name="inRow">Входная строка отчета</param>
        /// <param name="separator">Разделитель ячеек</param>
        /// <param name="outValue">Результирующая строка</param>
        void RowConcat(ReportRow inRow, string separator, out string outValue);

        /// <summary>
        /// Действие объединения всех значений колонки в одну строку
        /// </summary>
        /// <param name="inTable">Входная таблица</param>
        /// <param name="column">Имя колонки</param>
        /// <param name="separator">Разделитель объединяемых значений</param>
        /// <param name="outValue">Результирующая строка</param>
        void ColumnConcat(ReportTable inTable, string column, string separator, out string outValue);

        /// <summary>
        /// Действие объединения значений всех ячеек таблицы в одну строку
        /// </summary>
        /// <param name="inTable">Входная таблица</param>
        /// <param name="rowSeparator">Разделитель строк</param>
        /// <param name="cellSeparator">Разделитель ячеек</param>
        /// <param name="outValue">Выходная строка</param>
        void TableConcat(ReportTable inTable, string rowSeparator, string cellSeparator, out string outValue);

        /// <summary>
        /// Получить строку таблицы по указанному номеру
        /// </summary>
        /// <param name="table">Таблица, из которой выбирается строка</param>
        /// <param name="rowNumber">Номер строки начиная с 0</param>
        /// <param name="row">Результирующая строка</param>
        void GetRow(ReportTable table, int rowNumber, out ReportRow row);

        /// <summary>
        /// Получить значение ячейки таблицы по указанному номеру
        /// </summary>
        /// <param name="table">Таблица, из которой выбирается значение ячейки</param>
        /// <param name="rowNumber">Номер строки начиная с 0</param>
        /// <param name="columnName">Имя столбца</param>
        /// <param name="value">Результирующее значение</param>
        void GetCell(ReportTable table, int rowNumber, string columnName, out object value);
    }

    /// <summary>
    /// Плагин, реализующий интерфейс IPlug
    /// </summary>
    public class ConvertPlug : IPlug
    {
        /// <summary>
        /// В конструкторе происходит подгрузка словаря для Padeg.dll
        /// </summary>
        public ConvertPlug()
        {
            string fileName = Path.Combine(new FileInfo(Assembly.GetExecutingAssembly().FullName).DirectoryName, "plugins" + Path.DirectorySeparatorChar + "Except.dic");
            if (File.Exists(fileName))
            {
                Declension.SetExceptionsDictionaryFileName(fileName);
                if (!Declension.UpdateExceptionsDictionary())
                {
                    ConvertException exception = new ConvertException("Не удалось подгрузить словарь {0}");
                    exception.Data.Add("{0}",fileName);
                    throw exception;
                }
            }
        }

        /// <summary>
        /// Функция конвертации целого числа в текст
        /// </summary>
        /// <param name="number">Число</param>
        /// <param name="textCase">Падеж</param>
        /// <param name="sex">Пол</param>
        /// <param name="firstCapital">Ставить первую букву прописной</param>
        /// <param name="isOrdinal">Если true, то порядковое числительно, если false, то количественное</param>
        /// <param name="words">Возвращаемый текст</param>
        public void ConvertIntToString(long number, TextCase textCase, Sex sex, bool firstCapital, bool isOrdinal, out string words)
        {
            Converter conv = new Converter(textCase, sex);
            words = conv.NumberToText(number, isOrdinal);
            if (words.Length == 0)
                return;
            if (firstCapital)
            {
                string first = words[0].ToString().ToUpper(CultureInfo.CurrentCulture);
                words = first + words.Substring(1);
            }
        }

        /// <summary>
        /// Функция конвертации вещественного числа в текст
        /// </summary>
        /// <param name="number">Число</param>
        /// <param name="textCase">Падеж</param>
        /// <param name="firstCapital">Ставить первую букву прописной</param>
        /// <param name="words">Возвращаемый текст</param>
        public void ConvertFloatToString(decimal number, TextCase textCase, bool firstCapital, out string words)
        {
            Converter conv = new Converter(textCase, Sex.Female);
            words = conv.FloatToString(number);
            if (words.Length == 0)
                return;
            if (firstCapital)
            {
                string first = words[0].ToString().ToUpper(CultureInfo.CurrentCulture);
                words = first + words.Substring(1);
            }
        }

        /// <summary>
        /// Форматирование даты и времени в виде текста
        /// </summary>
        /// <param name="dateTime">Дата и время</param>
        /// <param name="format">
        /// Формат даты и времени. Можно задавать стандартный формат даты и времени, либо расширенный вариант:
        /// ddx - день месяца в виде текста
        /// MMx - месяц в виде текста
        /// yyx - год в формате двухзначного числа в виде текста
        /// yyyyx - год в формате четырехзначного числа в виде текста
        /// hhx - время от 1 до 12 в виде текста
        /// HHx - время от 0 до 23 в виде текста
        /// mmx - минуты в виде текста
        /// ssx - секунды в виде текста
        /// Буква "х" во всех расширенных форматах - первая буква падежа n,g,d,a,i,p
        /// </param>
        /// <param name="firstCapital">Ставить первую букву прописной</param>
        /// <param name="words">Возвращаемый текст</param>
        public void ConvertDateTimeToString(DateTime dateTime, string format, bool firstCapital, out string words)
        {
            Converter conv = new Converter(TextCase.Nominative, Sex.Neuter);
            words = conv.DateTimeToText(dateTime, format);
            if (words.Length == 0)
                return;
            if (firstCapital)
            {
                string first = words[0].ToString().ToUpper(CultureInfo.CurrentCulture);
                words = first + words.Substring(1);
            }
        }

        /// <summary>
        /// Действие конвертации суммы в строку
        /// </summary>
        /// <param name="currency">Сумма</param>
        /// <param name="currencyType">Тип валюты</param>
        /// <param name="format">Формат строки вывода суммы:
        /// ii - рубли (доллары, евро) в виде числа
        /// ff - копейки (центы) в виде числа
        /// iix - рубли (доллары, евро) в виде строки
        /// ffx - копейки (центы) в виде строки
        /// rx - слово "рубль" ("доллар", "евро")
        /// kx - словок "копейка" ("цент")
        /// nn - слово "минус ", если число отрицательное. Если число положительное, то пустая строка. Пробел после слова минус ставится автоматически.
        /// n - знак "-", если число отрицательное. Если число положительное, то знак не ставится. Пробел после знака "-" автоматически НЕ ставится.
        /// Буква "х" во всех форматах - первая буква падежа n,g,d,a,i,p
        /// </param>
        /// <param name="thousandSeparator">Разделитель порядков</param>
        /// <param name="firstCapital">Ставить первую букву прописной или нет</param>
        /// <param name="isOrdinal">Если true, то порядковое числительно, если false, то количественное</param>
        /// <param name="words">Возвращаемый текст</param>
        public void ConvertCurrencyToString(decimal currency, CurrencyType currencyType, string format, 
            string thousandSeparator, bool firstCapital, bool isOrdinal, out string words)
        {
            Converter conv = new Converter(TextCase.Nominative, Sex.Neuter);
            words = conv.CurrencyToString(currency, currencyType, format, thousandSeparator, isOrdinal);
            if (words.Length == 0)
                return;
            if (firstCapital)
            {
                string first = words[0].ToString().ToUpper(CultureInfo.CurrentCulture);
                words = first + words.Substring(1);
            }
        }

        /// <summary>
        /// Действие конвертации ячейки с числом в строковое представление чисел
        /// </summary>
        /// <param name="inRow">Входная строка</param>
        /// <param name="column">Имя колонки (или перечень колонок через запятую)</param>
        /// <param name="textCase">Падеж</param>
        /// <param name="sex">Пол</param>
        /// <param name="firstCapital">Ставить первую букву прописной или нет</param>
        /// <param name="isOrdinal">Если true, то порядковое числительно, если false, то количественное</param>
        /// <param name="outRow">Выходная строка</param>
        public void ConvertIntCellToString(ReportRow inRow, string column, TextCase textCase, Sex sex, bool firstCapital, bool isOrdinal, out ReportRow outRow)
        {
            if (inRow == null)
                throw new ConvertException("Не задана входная строка");
            if (column == null)
                throw new ConvertException("Не задано имя колонки");
            ReportRow tmp_row = new ReportRow(inRow.Table);
            string[] columns = column.Split(new char[] { ',' });
            for (int i = 0; i < columns.Count(); i++)
                columns[i] = columns[i].Trim();
            for (int i = 0; i < inRow.Count; i++)
            {
                string words = inRow[i].Value;
                if (columns.Contains(tmp_row.Table.Columns[i]))
                {
                    long value;
                    if (long.TryParse(words.Split(new char[] { '.', ',' })[0], out value))
                        ConvertIntToString(value, textCase, sex, firstCapital, isOrdinal, out words);
                }
                tmp_row.Add(new ReportCell(tmp_row, words));
            }
            outRow = tmp_row;
        }

        /// <summary>
        /// Действие конвертации всех значений колонки таблицы в строковое представление чисел
        /// </summary>
        /// <param name="inTable">Входная таблица</param>
        /// <param name="column">Имя колонки</param>
        /// <param name="textCase">Падеж</param>
        /// <param name="sex">Пол</param>
        /// <param name="firstCapital">Ставить первую букву прописной или нет</param>
        /// <param name="isOrdinal">Если true, то порядковое числительно, если false, то количественное</param>
        /// <param name="outTable">Выходная таблица</param>
        public void ConvertIntColToString(ReportTable inTable, string column, TextCase textCase, Sex sex, bool firstCapital, bool isOrdinal, out ReportTable outTable)
        {
            if (inTable == null)
                throw new ConvertException("Не задана входная таблица");
            ReportTable tmp_table = new ReportTable();
            tmp_table.SetColumns(inTable.Columns);
            foreach (ReportRow row in inTable)
            {
                ReportRow tmp_row = new ReportRow(tmp_table);
                ConvertIntCellToString(row, column, textCase, sex, firstCapital, isOrdinal, out tmp_row);
                tmp_table.Add(tmp_row);
            }
            outTable = tmp_table;
        }

        /// <summary>
        /// Действие конвертации ячейки с датой в форматированное представление даты
        /// </summary>
        /// <param name="inRow">Входная строка</param>
        /// <param name="column">Имя колонки (или перечень колонок через запятую)</param>
        /// <param name="format">
        /// Формат даты и времени. Можно задавать стандартный формат даты и времени, либо расширенный вариант:
        /// ddx - день месяца в виде текста
        /// MMx - месяц в виде текста
        /// yyx - год в формате двухзначного числа в виде текста
        /// yyyyx - год в формате четырехзначного числа в виде текста
        /// hhx - время от 1 до 12 в виде текста
        /// HHx - время от 0 до 23 в виде текста
        /// mmx - минуты в виде текста
        /// ssx - секунды в виде текста
        /// Буква "х" во всех расширенных форматан - первая буква падежа n,g,d,a,i,p
        /// </param>
        /// <param name="firstCapital">Ставить первую букву прописной или нет</param>
        /// <param name="outRow">Выходная строка</param>
        public void ConvertDateTimeCellToString(ReportRow inRow, string column, string format, bool firstCapital, out ReportRow outRow)
        {
            if (inRow == null)
                throw new ConvertException("Не задана входная строка");
            if (column == null)
                throw new ConvertException("Не задано имя колонки");
            ReportRow tmp_row = new ReportRow(inRow.Table);
            string[] columns = column.Split(new char[] { ',' });
            for (int i = 0; i < columns.Count(); i++)
                columns[i] = columns[i].Trim();
            for (int i = 0; i < inRow.Count; i++)
            {
                string words = inRow[i].Value;
                if (columns.Contains(tmp_row.Table.Columns[i]))
                {
                    DateTime value;
                    if (DateTime.TryParse(words, out value))
                        ConvertDateTimeToString(value, format, firstCapital, out words);
                }
                tmp_row.Add(new ReportCell(tmp_row, words));
            }
            outRow = tmp_row;
        }

        /// <summary>
        /// Действие конвертации всех значений колонки таблицы в форматированное представление даты
        /// </summary>
        /// <param name="inTable">Входная таблица</param>
        /// <param name="column">Имя колонки</param>
        /// <param name="format">
        /// Формат даты и времени. Можно задавать стандартный формат даты и времени, либо расширенный вариант:
        /// ddx - день месяца в виде текста
        /// MMx - месяц в виде текста
        /// yyx - год в формате двухзначного числа в виде текста
        /// yyyyx - год в формате четырехзначного числа в виде текста
        /// hhx - время от 1 до 12 в виде текста
        /// HHx - время от 0 до 23 в виде текста
        /// mmx - минуты в виде текста
        /// ssx - секунды в виде текста
        /// Буква "х" во всех расширенных форматан - первая буква падежа n,g,d,a,i,p
        /// </param>
        /// <param name="firstCapital">Ставить первую букву прописной или нет</param>
        /// <param name="outTable">Выходная таблица</param>
        public void ConvertDateTimeColToString(ReportTable inTable, string column, string format, bool firstCapital, out ReportTable outTable)
        {
            if (inTable == null)
                throw new ConvertException("Не задана входная таблица");
            ReportTable tmp_table = new ReportTable();
            tmp_table.SetColumns(inTable.Columns);
            foreach (ReportRow row in inTable)
            {
                ReportRow tmp_row = new ReportRow(tmp_table);
                ConvertDateTimeCellToString(row, column, format, firstCapital, out tmp_row);
                tmp_table.Add(tmp_row);
            }
            outTable = tmp_table;
        }

        /// <summary>
        /// Действие конвертации значения ячейки с суммой в строковое представление суммы
        /// </summary>
        /// <param name="inRow">Входная строка</param>
        /// <param name="column">Имя колонки (или перечень колонок через запятую)</param>
        /// <param name="currencyType">Тип валюты</param>
        /// <param name="format">Формат строки вывода суммы:
        /// ii - рубли (доллары, евро) в виде числа
        /// ff - копейки (центы) в виде числа
        /// iix - рубли (доллары, евро) в виде строки
        /// ffx - копейки (центы) в виде строки
        /// rx - слово "рубль" ("доллар", "евро")
        /// kx - словок "копейка" ("цент")
        /// nn - слово "минус ", если число отрицательное. Если число положительное, то пустая строка. Пробел после слова минус ставится автоматически.
        /// n - знак "-", если число отрицательное. Если число положительное, то знак не ставится. Пробел после знака "-" автоматически НЕ ставится.
        /// Буква "х" во всех форматах - первая буква падежа n,g,d,a,i,p
        /// </param>
        /// <param name="thousandSeparator">Разделитель порядков</param>
        /// <param name="firstCapital">Ставить первую букву прописной или нет</param>
        /// <param name="isOrdinal">Если true, то порядковое числительно, если false, то количественное</param>
        /// <param name="outRow">Выходная строка</param>
        public void ConvertCurrencyCellToString(ReportRow inRow, string column, CurrencyType currencyType, 
            string format, string thousandSeparator, bool firstCapital, bool isOrdinal, out ReportRow outRow)
        {
            if (inRow == null)
                throw new ConvertException("Не задана входная строка");
            if (column == null)
                throw new ConvertException("Не задано имя колонки");
            ReportRow tmp_row = new ReportRow(inRow.Table);
            string[] columns = column.Split(new char[] { ',' });
            for (int i = 0; i < columns.Count(); i++)
                columns[i] = columns[i].Trim();
            for (int i = 0; i < inRow.Count; i++)
            {
                string words = inRow[i].Value;
                if (columns.Contains(tmp_row.Table.Columns[i]))
                {
                    decimal value;
                    if (decimal.TryParse(words, out value))
                        ConvertCurrencyToString(value, currencyType, format, thousandSeparator, firstCapital, isOrdinal,
                            out words);
                }
                tmp_row.Add(new ReportCell(tmp_row, words));
            }
            outRow = tmp_row;
        }

        /// <summary>
        /// Действие конвертации всех значений колонки таблицы в строковое представление суммы
        /// </summary>
        /// <param name="inTable">Входная таблица</param>
        /// <param name="column">Имя колонки</param>
        /// <param name="currencyType">Тип валюты</param>
        /// <param name="format">Формат строки вывода суммы:
        /// ii - рубли (доллары, евро) в виде числа
        /// ff - копейки (центы) в виде числа
        /// iix - рубли (доллары, евро) в виде строки
        /// ffx - копейки (центы) в виде строки
        /// rx - слово "рубль" ("доллар", "евро")
        /// kx - словок "копейка" ("цент")
        /// nn - слово "минус ", если число отрицательное. Если число положительное, то пустая строка. Пробел после слова минус ставится автоматически.
        /// n - знак "-", если число отрицательное. Если число положительное, то знак не ставится. Пробел после знака "-" автоматически НЕ ставится.
        /// Буква "х" во всех форматах - первая буква падежа n,g,d,a,i,p
        /// </param>
        /// <param name="thousandSeparator">Разделитель порядков</param>
        /// <param name="firstCapital">Ставить первую букву прописной или нет</param>
        /// <param name="isOrdinal">Если true, то порядковое числительно, если false, то количественное</param>
        /// <param name="outTable">Выходная таблица</param>
        public void ConvertCurrencyColToString(ReportTable inTable, string column, 
            CurrencyType currencyType, string format, string thousandSeparator, bool firstCapital, bool isOrdinal, out ReportTable outTable)
        {
            if (inTable == null)
                throw new ConvertException("Не задана входная таблица");
            ReportTable tmp_table = new ReportTable();
            tmp_table.SetColumns(inTable.Columns);
            foreach (ReportRow row in inTable)
            {
                ReportRow tmp_row = new ReportRow(tmp_table);
                ConvertCurrencyCellToString(row, column, currencyType, format, thousandSeparator, firstCapital, isOrdinal, out tmp_row);
                tmp_table.Add(tmp_row);
            }
            outTable = tmp_table;
        }

        /// <summary>
        /// Действие конвертации значения ячейки с вещественным числом в троку
        /// </summary>
        /// <param name="inRow">Входная строка</param>
        /// <param name="column">Имя колонки (или перечень колонок через запятую)</param>
        /// <param name="textCase">Падеж</param>
        /// <param name="firstCapital">Ставить первую букву прописной или нет</param>
        /// <param name="outRow">Выходная строка</param>
        public void ConvertFloatCellToString(ReportRow inRow, string column, TextCase textCase, bool firstCapital, out ReportRow outRow)
        {
            if (inRow == null)
                throw new ConvertException("Не задана входная строка");
            if (column == null)
                throw new ConvertException("Не задано имя колонки");
            ReportRow tmp_row = new ReportRow(inRow.Table);
            string[] columns = column.Split(new char[] { ',' });
            for (int i = 0; i < columns.Count(); i++)
                columns[i] = columns[i].Trim();
            for (int i = 0; i < inRow.Count; i++)
            {
                string words = inRow[i].Value;
                if (columns.Contains(tmp_row.Table.Columns[i]))
                {
                    decimal value;
                    if (decimal.TryParse(words, out value))
                        ConvertFloatToString(value, textCase, firstCapital, out words);
                }
                tmp_row.Add(new ReportCell(tmp_row, words));
            }
            outRow = tmp_row;
        }

        /// <summary>
        /// Действие конвертации всех значений колонки таблицы в строковое представление вещественного числа
        /// </summary>
        /// <param name="inTable">Входная таблица</param>
        /// <param name="column">Имя колонки</param>
        /// <param name="textCase">Падеж</param>
        /// <param name="firstCapital">Ставить первую букву прописной или нет</param>
        /// <param name="outTable">Выходная таблица</param>
        public void ConvertFloatColToString(ReportTable inTable, string column, TextCase textCase, bool firstCapital, out ReportTable outTable)
        {
            if (inTable == null)
                throw new ConvertException("Не задана входная таблица");
            ReportTable tmp_table = new ReportTable();
            tmp_table.SetColumns(inTable.Columns);
            foreach (ReportRow row in inTable)
            {
                ReportRow tmp_row = new ReportRow(tmp_table);
                ConvertFloatCellToString(row, column, textCase, firstCapital, out tmp_row);
                tmp_table.Add(tmp_row);
            }
            outTable = tmp_table;
        }

        /// <summary>
        /// Перевод ФИО в указанный падеж
        /// </summary>
        /// <param name="nameIn">ФИО в именительном падеже</param>
        /// <param name="format">Формат ФИО:
        /// ss - полное представление фамилии
        /// s - первая буква фамилии
        /// nn - полное представление имени
        /// n - первая буква имени
        /// pp - полное представление отчества 
        /// p - первая буква отчества
        /// </param>
        /// <param name="textCase">Падеж</param>
        /// <param name="nameOut">Выходная строка с ФИО</param>
        public void ConvertNameToCase(string nameIn, string format, TextCase textCase, out string nameOut)
        {
            if (nameIn == null)
            {
                nameOut = "";
                return;
            }
            //Если входная строка не соответствует шаблону ФИО, то вернуть как есть
            string surname;
            string name;
            string patronymic;
            Declension.GetSNM(nameIn, out surname, out name, out patronymic);
            if (String.IsNullOrEmpty(surname) || String.IsNullOrEmpty(name) || String.IsNullOrEmpty(patronymic))
            {
                nameOut = nameIn;
                return;
            }
            DeclensionCase dec_case;
            switch (textCase)
            {
                case TextCase.Nominative: dec_case = DeclensionCase.Imenit;
                    break;
                case TextCase.Genitive: dec_case = DeclensionCase.Rodit;
                    break;
                case TextCase.Dative: dec_case = DeclensionCase.Datel;
                    break;
                case TextCase.Accusative: dec_case = DeclensionCase.Vinit;
                    break;
                case TextCase.Instrumental: dec_case = DeclensionCase.Tvorit;
                    break;
                case TextCase.Prepositional: dec_case = DeclensionCase.Predl;
                    break;
                default:
                    dec_case = DeclensionCase.Imenit;
                    break;
            }
            Gender gender = Declension.GetGender(nameIn);
            string nameInGender = "";
            if (gender != Gender.NotDefind)
                nameInGender = Declension.GetSNPDeclension(nameIn, gender, dec_case);
            else
                nameInGender = nameIn;
            Declension.GetSNM(nameInGender, out surname, out name, out patronymic);
            //Заменяем шаблонные строки
            if (Regex.IsMatch(format, "ss"))
                format = Regex.Replace(format, "ss", surname);
            if (Regex.IsMatch(format, "s"))
                format = Regex.Replace(format, "s", surname[0].ToString());
            if (Regex.IsMatch(format, "nn"))
                format = Regex.Replace(format, "nn", name);
            if (Regex.IsMatch(format, "n"))
                format = Regex.Replace(format, "n", name[0].ToString());
            if (Regex.IsMatch(format, "pp"))
                format = Regex.Replace(format, "pp", patronymic);
            if (Regex.IsMatch(format, "p"))
                format = Regex.Replace(format, "p", patronymic[0].ToString());
            nameOut = format;
        }

        /// <summary>
        /// Перевод ячейки с ФИО в указанный падеж
        /// </summary>
        /// <param name="inRow">Входная строка</param>
        /// <param name="column">Имя колонки (или перечень колонок через запятую)</param>
        /// <param name="format">Формат ФИО:
        /// ss - полное представление фамилии
        /// s - первая буква фамилии
        /// nn - полное представление имени
        /// n - первая буква имени
        /// pp - полное представление отчества 
        /// p - первая буква отчества
        /// </param>
        /// <param name="textCase">Падеж</param>
        /// <param name="outRow">Выходная строка</param>
        public void ConvertNameCellToCase(ReportRow inRow, string column, string format, TextCase textCase, out ReportRow outRow)
        {
            if (inRow == null)
                throw new ConvertException("Не задана входная строка");
            if (column == null)
                throw new ConvertException("Не задано имя колонки");
            ReportRow tmp_row = new ReportRow(inRow.Table);
            string[] columns = column.Split(new char[] { ',' });
            for (int i = 0; i < columns.Count(); i++)
                columns[i] = columns[i].Trim();
            for (int i = 0; i < inRow.Count; i++)
            {
                string words = inRow[i].Value;
                if (columns.Contains(tmp_row.Table.Columns[i]))
                    ConvertNameToCase(words, format, textCase, out words);
                tmp_row.Add(new ReportCell(tmp_row, words));
            }
            outRow = tmp_row;
        }

        /// <summary>
        /// Перевод всех значений колонки с ФИО в указанный падеж
        /// </summary>
        /// <param name="inTable">Входная таблица</param>
        /// <param name="column">Имя колонки</param>
        /// <param name="format">Формат ФИО:
        /// ss - полное представление фамилии
        /// s - первая буква фамилии
        /// nn - полное представление имени
        /// n - первая буква имени
        /// pp - полное представление отчества 
        /// p - первая буква отчества
        /// </param>
        /// <param name="textCase">Падеж</param>
        /// <param name="outTable">Выходная таблица</param>
        public void ConvertNameColToCase(ReportTable inTable, string column, string format, TextCase textCase, out ReportTable outTable)
        {
            if (inTable == null)
                throw new ConvertException("Не задана входная таблица");
            ReportTable tmp_table = new ReportTable();
            tmp_table.SetColumns(inTable.Columns);
            foreach (ReportRow row in inTable)
            {
                ReportRow tmp_row = new ReportRow(tmp_table);
                ConvertNameCellToCase(row, column, format, textCase, out tmp_row);
                tmp_table.Add(tmp_row);
            }
            outTable = tmp_table;
        }

        /// <summary>
        /// Действие перевода должности в указанный падеж 
        /// </summary>
        /// <param name="postIn">Должность в именительном падеже</param>
        /// <param name="textCase">Падеж</param>
        /// <param name="postOut">Выходная строка с должностью</param>
        public void ConvertPostToCase(string postIn, TextCase textCase, out string postOut)
        {
            if (postIn == null)
            {
                postOut = "";
                return;
            }
            DeclensionCase dec_case;
            switch (textCase)
            {
                case TextCase.Nominative: dec_case = DeclensionCase.Imenit;
                    break;
                case TextCase.Genitive: dec_case = DeclensionCase.Rodit;
                    break;
                case TextCase.Dative: dec_case = DeclensionCase.Datel;
                    break;
                case TextCase.Accusative: dec_case = DeclensionCase.Vinit;
                    break;
                case TextCase.Instrumental: dec_case = DeclensionCase.Tvorit;
                    break;
                case TextCase.Prepositional: dec_case = DeclensionCase.Predl;
                    break;
                default:
                    dec_case = DeclensionCase.Imenit;
                    break;
            }
            postOut = Declension.GetAppointmentDeclension(postIn, dec_case);
        }

        /// <summary>
        /// Действие перевода ячейки с должностью в указанный падеж 
        /// </summary>
        /// <param name="inRow">Входная строка</param>
        /// <param name="column">Имя колонки (или перечень колонок через запятую)</param>
        /// <param name="textCase">Падеж</param>
        /// <param name="outRow">Выходная строка</param>
        public void ConvertPostCellToCase(ReportRow inRow, string column, TextCase textCase, out ReportRow outRow)
        {
            if (inRow == null)
                throw new ConvertException("Не задана входная строка");
            if (column == null)
                throw new ConvertException("Не задано имя колонки");
            ReportRow tmp_row = new ReportRow(inRow.Table);
            string[] columns = column.Split(new char[] { ',' });
            for (int i = 0; i < columns.Count(); i++)
                columns[i] = columns[i].Trim();
            for (int i = 0; i < inRow.Count; i++)
            {
                string words = inRow[i].Value;
                if (columns.Contains(tmp_row.Table.Columns[i]))
                    ConvertPostToCase(words, textCase, out words);
                tmp_row.Add(new ReportCell(tmp_row, words));
            }
            outRow = tmp_row;
        }

        /// <summary>
        /// Действие перевода колонки с должностью в указанный падеж 
        /// </summary>
        /// <param name="inTable">Входная таблица</param>
        /// <param name="column">Имя колонки (или перечень колонок через запятую)</param>
        /// <param name="textCase">Падеж</param>
        /// <param name="outTable">Выходная таблица</param>
        public void ConvertPostColToCase(ReportTable inTable, string column, TextCase textCase, out ReportTable outTable)
        {
            if (inTable == null)
                throw new ConvertException("Не задана входная таблица");
            ReportTable tmp_table = new ReportTable();
            tmp_table.SetColumns(inTable.Columns);
            foreach (ReportRow row in inTable)
            {
                ReportRow tmp_row = new ReportRow(tmp_table);
                ConvertPostCellToCase(row, column, textCase, out tmp_row);
                tmp_table.Add(tmp_row);
            }
            outTable = tmp_table;
        }

        /// <summary>
        /// Действие перевода названия подразделения или предприятия в указанный падеж 
        /// </summary>
        /// <param name="officeIn">Название подразделения или предприятия в именительном падеже</param>
        /// <param name="textCase">Падеж</param>
        /// <param name="officeOut">Выходная строка с названием подразделения или предприятия</param>
        public void ConvertOfficeToCase(string officeIn, TextCase textCase, out string officeOut)
        {
            if (officeIn == null)
            {
                officeOut = "";
                return;
            }
            DeclensionCase dec_case;
            switch (textCase)
            {
                case TextCase.Nominative: dec_case = DeclensionCase.Imenit;
                    break;
                case TextCase.Genitive: dec_case = DeclensionCase.Rodit;
                    break;
                case TextCase.Dative: dec_case = DeclensionCase.Datel;
                    break;
                case TextCase.Accusative: dec_case = DeclensionCase.Vinit;
                    break;
                case TextCase.Instrumental: dec_case = DeclensionCase.Tvorit;
                    break;
                case TextCase.Prepositional: dec_case = DeclensionCase.Predl;
                    break;
                default:
                    dec_case = DeclensionCase.Imenit;
                    break;
            }
            officeOut = Declension.GetOfficeDeclension(officeIn, dec_case);
        }

        /// <summary>
        /// Действие перевода ячейки с названием подразделения или предприятия в указанный падеж 
        /// </summary>
        /// <param name="inRow">Входная строка</param>
        /// <param name="column">Имя колонки (или перечень колонок через запятую)</param>
        /// <param name="textCase">Падеж</param>
        /// <param name="outRow">Выходная строка</param>
        public void ConvertOfficeCellToCase(ReportRow inRow, string column, TextCase textCase, out ReportRow outRow)
        {
            if (inRow == null)
                throw new ConvertException("Не задана входная строка");
            if (column == null)
                throw new ConvertException("Не задано имя колонки");
            ReportRow tmp_row = new ReportRow(inRow.Table);
            string[] columns = column.Split(new char[] { ',' });
            for (int i = 0; i < columns.Count(); i++)
                columns[i] = columns[i].Trim();
            for (int i = 0; i < inRow.Count; i++)
            {
                string words = inRow[i].Value;
                if (columns.Contains(tmp_row.Table.Columns[i]))
                    ConvertOfficeToCase(words, textCase, out words);
                tmp_row.Add(new ReportCell(tmp_row, words));
            }
            outRow = tmp_row;
        }

        /// <summary>
        /// Действие перевода колонки с названием подразделения или предприятия в указанный падеж 
        /// </summary>
        /// <param name="inTable">Входная таблица</param>
        /// <param name="column">Имя колонки (или перечень колонок через запятую)</param>
        /// <param name="textCase">Падеж</param>
        /// <param name="outTable">Выходная таблица</param>
        public void ConvertOfficeColToCase(ReportTable inTable, string column, TextCase textCase, out ReportTable outTable)
        {
            if (inTable == null)
                throw new ConvertException("Не задана входная таблица");
            ReportTable tmp_table = new ReportTable();
            tmp_table.SetColumns(inTable.Columns);
            foreach (ReportRow row in inTable)
            {
                ReportRow tmp_row = new ReportRow(tmp_table);
                ConvertOfficeCellToCase(row, column, textCase, out tmp_row);
                tmp_table.Add(tmp_row);
            }
            outTable = tmp_table;
        }

        /// <summary>
        /// Действие объединения всех ячеек строки данных в одну строку
        /// </summary>
        /// <param name="inRow">Входная строка отчета</param>
        /// <param name="separator">Разделитель ячеек</param>
        /// <param name="outValue">Результирующая строка</param>
        public void RowConcat(ReportRow inRow, string separator, out string outValue)
        {
            if (inRow == null)
                throw new ConvertException("Входная строка не задана");
            string result = "";
            for (int i = 0; i < inRow.Count; i++)
            {
                result += inRow[i].Value;
                if (i < (inRow.Count - 1))
                    result += separator;
            }
            outValue = result;
        }

        /// <summary>
        /// Действие объединения всех значений колонки в одну строку
        /// </summary>
        /// <param name="inTable">Входная таблица</param>
        /// <param name="column">Имя колонки</param>
        /// <param name="separator">Разделитель объединяемых значений</param>
        /// <param name="outValue">Результирующая строка</param>
        public void ColumnConcat(ReportTable inTable, string column, string separator, out string outValue)
        {
            if (inTable == null)
                throw new ConvertException("Не задана входная таблица");
            string result = "";
            for (int i = 0; i < inTable.Count; i++)
            {
                result += inTable[i][column].Value;
                if (i < (inTable.Count - 1))
                    result += separator;
            }
            outValue = result;
        }

        /// <summary>
        /// Действие объединения значений всех ячеек таблицы в одну строку
        /// </summary>
        /// <param name="inTable">Входная таблица</param>
        /// <param name="rowSeparator">Разделитель строк</param>
        /// <param name="cellSeparator">Разделитель ячеек</param>
        /// <param name="outValue">Выходная строка</param>
        public void TableConcat(ReportTable inTable, string rowSeparator, string cellSeparator, out string outValue)
        {
            if (inTable == null)
                throw new ConvertException("Входная таблица не задана");
            string result = "";
            for (int i = 0; i < inTable.Count; i++)
            {
                string concated_row = "";
                RowConcat(inTable[i], cellSeparator, out concated_row);
                result += concated_row;
                if (i < (inTable.Count - 1))
                    result += rowSeparator;
            }
            outValue = result;
        }

        /// <summary>
        /// Получить строку таблицы по указанному номеру
        /// </summary>
        /// <param name="table">Таблица, из которой выбирается строка</param>
        /// <param name="rowNumber">Номер строки начиная с 0</param>
        /// <param name="row">Результирующая строка</param>
        public void GetRow(ReportTable table, int rowNumber, out ReportRow row)
        {
            if (table == null)
                throw new ConvertException("Входная таблица не задана");
            if (table.Count <= rowNumber)
            {
                ConvertException exception = new ConvertException("В таблице отсутствует запрашиваемая строка № {0}");
                exception.Data.Add("{0}", rowNumber.ToString(CultureInfo.CurrentCulture));
                throw exception;
            }
            row = table[rowNumber];
        }

        /// <summary>
        /// Получить значение ячейки таблицы по указанному номеру
        /// </summary>
        /// <param name="table">Таблица, из которой выбирается значение ячейки</param>
        /// <param name="rowNumber">Номер строки начиная с 0</param>
        /// <param name="columnName">Имя столбца</param>
        /// <param name="value">Результирующее значение</param>
        public void GetCell(ReportTable table, int rowNumber, string columnName, out object value)
        {
            if (table == null)
                throw new ConvertException("Входная таблица не задана");
            if (table.Count <= rowNumber)
            {
                ConvertException exception = new ConvertException("В таблице отсутствует запрашиваемая строка № {0}");
                exception.Data.Add("{0}", rowNumber.ToString(CultureInfo.CurrentCulture));
                throw exception;
            }
            if (!table.Columns.Contains(columnName))
            {
                ConvertException exception = new ConvertException("В таблице отсутствует запрашиваемый столбец {0}");
                exception.Data.Add("{0}", columnName);
                throw exception;
            }
            value = table[rowNumber][columnName].Value;
        }
    }

    /// <summary>
    /// Класс исключения модуля ConvertPlugin
    /// </summary>
    [Serializable()]
    public class ConvertException : Exception
    {
        /// <summary>
        /// Конструктор класса исключения ConvertException
        /// </summary>
        public ConvertException() : base() { }

        /// <summary>
        /// Конструктор класса исключения ConvertException
        /// </summary>
        /// <param name="message">Сообщение об ошибке</param>
        public ConvertException(string message) : base(message) { }

        /// <summary>
        /// Конструктор класса исключения ConvertException
        /// </summary>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="innerException">Вложенное исключение</param>
        public ConvertException(string message, Exception innerException) : base(message, innerException) { }

        /// <summary>
        /// Конструктор класса исключения ConvertException
        /// </summary>
        /// <param name="info">Информация сериализации</param>
        /// <param name="context">Контекст потока</param>
        protected ConvertException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
