using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using extended_types;
using FSLib.Declension;

//Модуль специальных функций программы Мена
namespace mena_module
{
    //Доступный из-вне интерфейс плагина
    public interface IPlugin
    {
        void mena_signers_table_convert(ReportTable in_table, out ReportTable out_table);
        void mena_floor_template(ReportRow in_row, string column_floor_name, out ReportRow out_row);
    }

    public class MenaPlugin: IPlugin
    {
        /// <summary>
        /// Специфичный метод конвертации таблицы подписчиков соглашения
        /// </summary>
        /// <param name="in_table">Входная таблица</param>
        /// <param name="out_table">Выходная таблица</param>
        public void mena_signers_table_convert(ReportTable in_table, out ReportTable out_table)
        {
            ReportTable tmp_table = new ReportTable();
            foreach(string column in in_table.Columns)
                if (column != "text_case" && column != "warrant_fio")
                {
                    tmp_table.Columns.Add(column);
                }
            foreach (ReportRow rr in in_table)
            {
                ReportRow tmp_row = new ReportRow(tmp_table);
                string fio = "";
                string template = rr["person_warrant"].Value;
                if (rr["warrant_fio"].Value.Trim() != "")
                {
                    if (rr["text_case"].Value == "Nominative")
                        fio = Declension1251.GetNominativeDeclension(rr["warrant_fio"].Value);
                    else
                        fio = rr["warrant_fio"].Value;
                    string[] fio_arr = fio.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (fio_arr.Length != 3)
                    {
                        ApplicationException exception = new ApplicationException("Имя участника договора {0} задано не полностью или не в корректном формате");
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
            out_table = tmp_table;
        }

        public void mena_floor_template(ReportRow in_row, string column_floor_name, out ReportRow out_row)
        {
            if (in_row[column_floor_name].Value == "нулевом")
                in_row[column_floor_name].Value = " ";
            else
                in_row[column_floor_name].Value = " расположена на " + in_row[column_floor_name].Value + " этаже, ";
            out_row = in_row;
        }
    }
}
