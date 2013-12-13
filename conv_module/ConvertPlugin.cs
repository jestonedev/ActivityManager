using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using extended_types;
using System.Data;
using FSLib.Declension;
using System.Text.RegularExpressions;

namespace conv_module
{
    //Доступный из-вне интерфейс плагина
    public interface IPlugin
    {
        void convert_dt_to_rt(DataTable dt, out ReportTable rt);
        void convert_dr_to_rr(DataRow dr, out ReportRow rr);
        
        void convert_int_to_string(long number, TextCase text_case, Sex sex, bool first_capital, bool is_ordinal, out string words);
        void convert_int_cell_to_string(ReportRow in_row, string column, TextCase text_case, Sex sex, bool first_capital, bool is_ordinal, out ReportRow out_row);
        void convert_int_col_to_string(ReportTable in_table, string column, TextCase text_case, Sex sex, bool first_capital, bool is_ordinal, out ReportTable out_table);
 
        void convert_datetime_to_string(DateTime datetime, string format, bool first_capital, out string words);
        void convert_datetime_cell_to_string(ReportRow in_row, string column, string format, bool first_capital, out ReportRow out_row);
        void convert_datetime_col_to_string(ReportTable in_table, string column, string format, bool first_capital, out ReportTable out_table);
        
        void convert_currency_to_string(double currency, CurrencyType currency_type, string format, string thousand_separator, bool first_capital, bool is_ordinal, out string words);
        void convert_currency_cell_to_string(ReportRow in_row, string column, CurrencyType currency_type, string format, string thousand_separator, bool first_capital, bool is_ordinal, out ReportRow out_row);
        void convert_currency_col_to_string(ReportTable in_table, string column, CurrencyType currency_type, string format, string thousand_separator, bool first_capital, bool is_ordinal, out ReportTable out_table);

        void convert_float_to_string(float number, TextCase text_case, bool first_capital, out string words);
        void convert_float_cell_to_string(ReportRow in_row, string column, TextCase text_case, bool first_capital, out ReportRow out_row);
        void convert_float_col_to_string(ReportTable in_table, string column, TextCase text_case, bool first_capital, out ReportTable out_table);

        void convert_snp_padeg(string snp_in, string format, TextCase text_case, out string snp_out);
        void convert_snp_cell_to_padeg(ReportRow in_row, string column, string format, TextCase text_case, out ReportRow out_row);
        void convert_snp_col_to_padeg(ReportTable in_table, string column, string format, TextCase text_case, out ReportTable out_table);

        void convert_cell_concat(ReportRow in_row, string separator, out string out_value);
        void convert_col_concat(ReportTable in_table, string column, string separator, out string out_value);
        void convert_full_concat(ReportTable in_table, string row_separator, string cell_separator, out string out_value);
    }

    public class ConvertPlugin: IPlugin
    {
        /// <summary>
        /// Функция конвертации из типа таблицы базы данных в тип таблицы отчета
        /// </summary>
        /// <param name="dt">Таблица базы данных</param>
        /// <param name="rt">Таблица отчета</param>
        public void convert_dt_to_rt(DataTable dt, out ReportTable rt)
        {
            ReportTable tmp_table = new ReportTable();
            for (int i = 0; i < dt.Columns.Count; i++)
                tmp_table.Columns.Add(dt.Columns[i].ColumnName);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ReportRow row = new ReportRow(tmp_table);
                for (int j = 0; j < dt.Columns.Count; j++)
                    row.Add(new ReportCell(row, dt.Rows[i][j].ToString()));
                tmp_table.Add(row);
            }
            rt = tmp_table;
        }

        /// <summary>
        /// Функция конвертации из типа строки базы данных в тип строки отчета
        /// </summary>
        /// <param name="dr">Строка базы данных</param>
        /// <param name="rr">Строка отчета</param>
        public void convert_dr_to_rr(DataRow dr, out ReportRow rr)
        {
            ReportTable table = new ReportTable();
            for (int i = 0; i < dr.Table.Columns.Count; i++)
                table.Columns.Add(dr.Table.Columns[i].ColumnName);
            ReportRow tmp_rr = new ReportRow(table);
            for (int i = 0; i < dr.ItemArray.Length; i++)
                tmp_rr.Add(new ReportCell(tmp_rr, dr.ItemArray[i].ToString()));
            rr = tmp_rr;
        }

        /// <summary>
        /// Функция конвертации целого числа в текст
        /// </summary>
        /// <param name="number">Число</param>
        /// <param name="textcase">Падеж</param>
        /// <param name="sex">Пол</param>
        /// <param name="first_capital">Ставить первую букву прописной</param>
        /// <param name="is_ordinal">Если true, то порядковое числительно, если false, то количественное</param>
        /// <param name="words">Возвращаемый текст</param>
        public void convert_int_to_string(long number, TextCase text_case, Sex sex, bool first_capital, bool is_ordinal, out string words)
        {
            Converter conv = new Converter(text_case, sex);
            words = conv.NumberToText(number, is_ordinal);
            if (words.Length == 0)
                return;
            if (first_capital)
            {
                string first = words[0].ToString().ToUpper();
                words = first + words.Substring(1);     
            }
        }

        /// <summary>
        /// Функция конвертации вещественного числа в текст
        /// </summary>
        /// <param name="number">Число</param>
        /// <param name="text_case">Падеж</param>
        /// <param name="first_capital">Ставить первую букву прописной</param>
        /// <param name="words">Возвращаемый текст</param>
        public void convert_float_to_string(float number, TextCase text_case, bool first_capital, out string words)
        {
            Converter conv = new Converter(text_case, Sex.Female);
            words = conv.FloatToString(number);
            if (words.Length == 0)
                return;
            if (first_capital)
            {
                string first = words[0].ToString().ToUpper();
                words = first + words.Substring(1);
            }
        }

        /// <summary>
        /// Форматирование даты и времени в виде текста
        /// </summary>
        /// <param name="datetime">Дата и время</param>
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
        /// <param name="textcase">Падеж русского языка</param>
        /// <param name="words">Возвращаемый текст</param>
        public void convert_datetime_to_string(DateTime datetime, string format, bool first_capital, out string words)
        {
            Converter conv = new Converter(TextCase.Nominative, Sex.Neuter);
            words = conv.DateTimeToText(datetime, format);
            if (words.Length == 0)
                return;
            if (first_capital)
            {
                string first = words[0].ToString().ToUpper();
                words = first + words.Substring(1);
            }
        }

        /// <summary>
        /// Метод конвертации суммы в строку
        /// </summary>
        /// <param name="currency">Сумма</param>
        /// <param name="currency_type">Тип валюты</param>
        /// <param name="format">Формат строки вывода суммы:
        /// ii - рубли (доллары, евро) в виде числа
        /// ff - копейки (центы) в виде числа
        /// iix - рубли (доллары, евро) в виде строки
        /// ffx - копейки (центы) в виде строки
        /// rx - слово "рубль" ("доллар", "евро")
        /// kx - словок "копейка" ("цент")
        /// Буква "х" во всех форматах - первая буква падежа n,g,d,a,i,p
        /// </param>
        /// <param name="first_capital">Ставить первую букву прописной или нет</param>
        /// <param name="is_ordinal">Если true, то порядковое числительно, если false, то количественное</param>
        /// <param name="words">Возвращаемый текст</param>
        public void convert_currency_to_string(double currency, CurrencyType currency_type, string format, string thousand_separator, bool first_capital, bool is_ordinal, out string words)
        {
            Converter conv = new Converter(TextCase.Nominative, Sex.Neuter);
            words = conv.CurrencyToString(currency, currency_type, format, thousand_separator, is_ordinal);
            if (words.Length == 0)
                return;
            if (first_capital)
            {
                string first = words[0].ToString().ToUpper();
                words = first + words.Substring(1);
            }
        }

        /// <summary>
        /// Метод конвертации ячейки с числом в строковое представление чисел
        /// </summary>
        /// <param name="in_row">Входная строка</param>
        /// <param name="column">Имя колонки</param>
        /// <param name="text_case">Падеж</param>
        /// <param name="sex">Пол</param>
        /// <param name="first_capital">Ставить первую букву прописной или нет</param>
        /// <param name="is_ordinal">Если true, то порядковое числительно, если false, то количественное</param>
        /// <param name="out_row">Выходная строка</param>
        public void convert_int_cell_to_string(ReportRow in_row, string column, TextCase text_case, Sex sex, bool first_capital, bool is_ordinal, out ReportRow out_row)
        {
            ReportRow tmp_row = new ReportRow(in_row.Table);
            for (int i = 0; i < in_row.Count; i++)
            {
                string words = in_row[i].Value;
                if (tmp_row.Table.Columns[i] == column)
                    convert_int_to_string(long.Parse(words.Split(new char[] {'.',','})[0]), text_case, sex, first_capital, is_ordinal, out words);
                tmp_row.Add(new ReportCell(tmp_row, words));
            }
            out_row = tmp_row;
        }

        /// <summary>
        /// Метод конвертации всех значений колонки таблицы в строковое представление чисел
        /// </summary>
        /// <param name="in_table">Входная таблица</param>
        /// <param name="column">Имя колонки</param>
        /// <param name="text_case">Падеж</param>
        /// <param name="sex">Пол</param>
        /// <param name="first_capital">Ставить первую букву прописной или нет</param>
        /// <param name="is_ordinal">Если true, то порядковое числительно, если false, то количественное</param>
        /// <param name="out_table">Выходная таблица</param>
        public void convert_int_col_to_string(ReportTable in_table, string column, TextCase text_case, Sex sex, bool first_capital, bool is_ordinal, out ReportTable out_table)
        {
            ReportTable tmp_table = new ReportTable();
            tmp_table.Columns = in_table.Columns;
            foreach (ReportRow row in in_table)
            {
                ReportRow tmp_row = new ReportRow(tmp_table);
                convert_int_cell_to_string(row, column, text_case, sex, first_capital, is_ordinal, out tmp_row);
                tmp_table.Add(tmp_row);
            }
            out_table = tmp_table;
        }

        /// <summary>
        /// Метод конвертации ячейки с датой в форматированное представление даты
        /// </summary>
        /// <param name="in_row">Входная строка</param>
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
        /// <param name="first_capital">Ставить первую букву прописной или нет</param>
        /// <param name="out_row">Выходная строка</param>
        public void convert_datetime_cell_to_string(ReportRow in_row, string column, string format, bool first_capital, out ReportRow out_row)
        {
            ReportRow tmp_row = new ReportRow(in_row.Table);
            for (int i = 0; i < in_row.Count; i++)
            {
                string words = in_row[i].Value;
                if (tmp_row.Table.Columns[i] == column)
                    convert_datetime_to_string(DateTime.Parse(words), format, first_capital, out words);
                tmp_row.Add(new ReportCell(tmp_row, words));
            }
            out_row = tmp_row;
        }

        /// <summary>
        /// Метод конвертации всех значений колонки таблицы в форматированное представление даты
        /// </summary>
        /// <param name="in_table">Входная таблица</param>
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
        /// <param name="first_capital">Ставить первую букву прописной или нет</param>
        /// <param name="out_table">Выходная таблица</param>
        public void convert_datetime_col_to_string(ReportTable in_table, string column, string format, bool first_capital, out ReportTable out_table)
        {
            ReportTable tmp_table = new ReportTable();
            tmp_table.Columns = in_table.Columns;
            foreach (ReportRow row in in_table)
            {
                ReportRow tmp_row = new ReportRow(tmp_table);
                convert_datetime_cell_to_string(row, column, format, first_capital, out tmp_row);
                tmp_table.Add(tmp_row);
            }
            out_table = tmp_table;
        }

        /// <summary>
        /// Метод конвертации значения ячейки с суммой в строковое представление суммы
        /// </summary>
        /// <param name="in_row">Входная строка</param>
        /// <param name="column">Имя колонки</param>
        /// <param name="currency_type">Тип валюты: рубли, доллары, евро</param>
        /// <param name="text_case">Падеж</param>
        /// <param name="first_capital">Ставить первую букву прописной или нет</param>
        /// <param name="is_ordinal">Если true, то порядковое числительно, если false, то количественное</param>
        /// <param name="out_row">Выходная строка</param>
        public void convert_currency_cell_to_string(ReportRow in_row, string column, CurrencyType currency_type, string format, string thousand_separator, bool first_capital, bool is_ordinal, out ReportRow out_row)
        {
            ReportRow tmp_row = new ReportRow(in_row.Table);
            for (int i = 0; i < in_row.Count; i++)
            {
                string words = in_row[i].Value;
                if (tmp_row.Table.Columns[i] == column)
                    convert_currency_to_string(double.Parse(words), currency_type, format, thousand_separator, first_capital, is_ordinal, 
                        out words);
                tmp_row.Add(new ReportCell(tmp_row, words));
            }
            out_row = tmp_row;
        }

        /// <summary>
        /// Метод конвертации всех значений колонки таблицы в строковое представление суммы
        /// </summary>
        /// <param name="in_table">Входная таблица</param>
        /// <param name="column">Имя колонки</param>
        /// <param name="currency_type">Тип валюты: рубли, доллары, евро</param>
        /// <param name="text_case">Падеж</param>
        /// <param name="first_capital">Ставить первую букву прописной или нет</param>
        /// <param name="is_ordinal">Если true, то порядковое числительно, если false, то количественное</param>
        /// <param name="out_table">Выходная таблица</param>
        public void convert_currency_col_to_string(ReportTable in_table, string column, CurrencyType currency_type, string format, string thousand_separator, bool first_capital, bool is_ordinal, out ReportTable out_table)
        {
            ReportTable tmp_table = new ReportTable();
            tmp_table.Columns = in_table.Columns;
            foreach (ReportRow row in in_table)
            {
                ReportRow tmp_row = new ReportRow(tmp_table);
                convert_currency_cell_to_string(row, column, currency_type, format, thousand_separator, first_capital, is_ordinal, out tmp_row);
                tmp_table.Add(tmp_row);
            }
            out_table = tmp_table;
        }

        /// <summary>
        /// Метод конвертации значения ячейки с вещественным числом в троку
        /// </summary>
        /// <param name="in_row">Входная строка</param>
        /// <param name="column">Имя колонки</param>
        /// <param name="text_case">Падеж</param>
        /// <param name="first_capital">Ставить первую букву прописной или нет</param>
        /// <param name="out_row">Выходная строка</param>
        public void convert_float_cell_to_string(ReportRow in_row, string column, TextCase text_case, bool first_capital, out ReportRow out_row)
        {
            ReportRow tmp_row = new ReportRow(in_row.Table);
            for (int i = 0; i < in_row.Count; i++)
            {
                string words = in_row[i].Value;
                if (tmp_row.Table.Columns[i] == column)
                    convert_float_to_string(float.Parse(words), text_case, first_capital, out words);
                tmp_row.Add(new ReportCell(tmp_row, words));
            }
            out_row = tmp_row;
        }

        /// <summary>
        /// Метод конвертации всех значений колонки таблицы в строковое представление вещественного числа
        /// </summary>
        /// <param name="in_table">Входная таблица</param>
        /// <param name="column">Имя колонки</param>
        /// <param name="text_case">Падеж</param>
        /// <param name="first_capital">Ставить первую букву прописной или нет</param>
        /// <param name="out_table">Выходная таблица</param>
        public void convert_float_col_to_string(ReportTable in_table, string column, TextCase text_case, bool first_capital, out ReportTable out_table)
        {
            ReportTable tmp_table = new ReportTable();
            tmp_table.Columns = in_table.Columns;
            foreach (ReportRow row in in_table)
            {
                ReportRow tmp_row = new ReportRow(tmp_table);
                convert_float_cell_to_string(row, column, text_case, first_capital, out tmp_row);
                tmp_table.Add(tmp_row);
            }
            out_table = tmp_table;
        }

        /// <summary>
        /// Перевод ФИО в указанный падеж
        /// </summary>
        /// <param name="snp_in">ФИО в именительном падеже</param>
        /// <param name="format">Формат ФИО:
        /// ss - полное представление фамилии
        /// s - первая буква фамилии
        /// nn - полное представление имени
        /// n - первая буква имени
        /// pp - полное представление отчества 
        /// p - первая буква отчества
        /// </param>
        /// <param name="text_case">Падеж</param>
        /// <param name="snp_out">Выходная строка с ФИО</param>
        public void convert_snp_padeg(string snp_in, string format, TextCase text_case, out string snp_out)
        {
            DeclensionCase dec_case;
            switch (text_case)
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
            Gender gender = Declension1251.GetGender(snp_in);
            string snp_tmp = "";
            if (gender != Gender.NotDefind)
                snp_tmp = Declension1251.GetSNPDeclension(snp_in, Declension1251.GetGender(snp_in), dec_case);
            else
                snp_tmp = snp_in;
            string[] snp_array = snp_tmp.Split(new char[] { ' ' });
            if (snp_array.Length < 3)
            {
                ApplicationException exception = new ApplicationException("Некорректно заданы ФИО {0}");
                exception.Data.Add("{0}", snp_in);
                throw exception;
            }
            if (Regex.IsMatch(format, "ss"))
                format = Regex.Replace(format, "ss", snp_array[0]);
            if (Regex.IsMatch(format, "s"))
                format = Regex.Replace(format, "s", snp_array[0][0].ToString());
            if (Regex.IsMatch(format, "nn"))
                format = Regex.Replace(format, "nn", snp_array[1]);
            if (Regex.IsMatch(format, "n"))
                format = Regex.Replace(format, "n", snp_array[1][0].ToString());
            if (Regex.IsMatch(format, "pp"))
                format = Regex.Replace(format, "pp", snp_array[2]);
            if (Regex.IsMatch(format, "p"))
                format = Regex.Replace(format, "p", snp_array[2][0].ToString());
            snp_out = format;
        }

        /// <summary>
        /// Перевод ячейки с ФИО в указанный падеж
        /// </summary>
        /// <param name="in_row">Входная строка</param>
        /// <param name="column">Имя колонки</param>
        /// <param name="format">Формат ФИО:
        /// ss - полное представление фамилии
        /// s - первая буква фамилии
        /// nn - полное представление имени
        /// n - первая буква имени
        /// pp - полное представление отчества 
        /// p - первая буква отчества
        /// </param>
        /// <param name="text_case">Падеж</param>
        /// <param name="out_row">Выходная строка</param>
        public void convert_snp_cell_to_padeg(ReportRow in_row, string column, string format, TextCase text_case, out ReportRow out_row)
        {
            ReportRow tmp_row = new ReportRow(in_row.Table);
            for (int i = 0; i < in_row.Count; i++)
            {
                string words = in_row[i].Value;
                if (tmp_row.Table.Columns[i] == column)
                    convert_snp_padeg(words, format, text_case, out words);
                tmp_row.Add(new ReportCell(tmp_row, words));
            }
            out_row = tmp_row;
        }

        /// <summary>
        /// Перевод всех значений колонки с ФИО в указанный падеж
        /// </summary>
        /// <param name="in_table">Входная таблица</param>
        /// <param name="column">Имя колонки</param>
        /// <param name="format">Формат ФИО:
        /// ss - полное представление фамилии
        /// s - первая буква фамилии
        /// nn - полное представление имени
        /// n - первая буква имени
        /// pp - полное представление отчества 
        /// p - первая буква отчества
        /// </param>
        /// <param name="text_case">Падеж</param>
        /// <param name="out_table">Выходная таблица</param>
        public void convert_snp_col_to_padeg(ReportTable in_table, string column, string format, TextCase text_case, out ReportTable out_table)
        {
            ReportTable tmp_table = new ReportTable();
            tmp_table.Columns = in_table.Columns;
            foreach (ReportRow row in in_table)
            {
                ReportRow tmp_row = new ReportRow(tmp_table);
                convert_snp_cell_to_padeg(row, column, format, text_case, out tmp_row);
                tmp_table.Add(tmp_row);
            }
            out_table = tmp_table;
        }

        /// <summary>
        /// Метод объединения всех ячеек строки данных в одну строку
        /// </summary>
        /// <param name="in_row">Входная строка отчета</param>
        /// <param name="separator">Разделитель ячеек</param>
        /// <param name="out_value">Результирующая строка</param>
        public void convert_cell_concat(ReportRow in_row, string separator, out string out_value)
        {
            string result = "";
            for (int i = 0; i < in_row.Count; i++)
            {
                result += in_row[i].Value;
                if (i < (in_row.Count - 1))
                    result += separator;
            }
            out_value = result;
        }

        /// <summary>
        /// Метод объединения всех значений колонки в одну строку
        /// </summary>
        /// <param name="in_table">Входная таблица</param>
        /// <param name="column">Имя колонки</param>
        /// <param name="separator">Разделитель объединяемых значений</param>
        /// <param name="out_value">Результирующая строка</param>
        public void convert_col_concat(ReportTable in_table, string column, string separator, out string out_value)
        {
            string result = "";
            for (int i = 0; i < in_table.Count; i++)
            {
                result += in_table[i][column].Value;
                if (i < (in_table.Count - 1))
                    result += separator;
            }
            out_value = result;
        }

        /// <summary>
        /// Метод объединения значений всех ячеек таблицы в одну строку
        /// </summary>
        /// <param name="in_table">Входная таблица</param>
        /// <param name="row_separator">Разделитель строк</param>
        /// <param name="cell_separator">Разделитель ячеек</param>
        /// <param name="out_value">Выходная строка</param>
        public void convert_full_concat(ReportTable in_table, string row_separator, string cell_separator, out string out_value)
        {
            string result = "";
            for (int i = 0; i < in_table.Count; i++)
            {
                string concated_row = "";
                convert_cell_concat(in_table[i], cell_separator, out concated_row);
                result += concated_row;
                if (i < (in_table.Count - 1))
                    result += row_separator;
            }
            out_value = result;
        }
    }
}
