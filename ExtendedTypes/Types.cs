using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.ComponentModel;

namespace ExtendedTypes
{
    /// <summary>
    /// Ячейка таблицы отчета
    /// </summary>
    public class ReportCell
    {
        public string Value { get; set; }

        private ReportRow row;
        public ReportRow Row { get { return row; } }

        public ReportCell(ReportRow row)
        {
            this.row = row;
        }

        public ReportCell(ReportRow row, string value)
        {
            this.Value = value;
            this.row = row;
        }
    }

    /// <summary>
    /// Строка таблицы отчета
    /// </summary>
    [TypeConverter(typeof(ReportRowTypeConverter))]
    public class ReportRow : Collection<ReportCell>
    {
        private ReportTable table;
        public ReportTable Table { get { return table; } }

        public ReportRow(ReportTable table)
        {
            this.table = table;
        }

        public ReportCell this[string columnName]
        {
            get
            {
                return this[this.table.Columns.IndexOf(columnName)];
            }
        }

        public override string ToString()
        {
            string result = "";
            for (int i = 0; i < this.Count; i++)
            {
                result += "\"" + this.table.Columns[i].Replace("\\", "\\\\").Replace("\"", "\\\"") + "\":\"" + this[i].Value.Replace("\\", "\\\\").Replace("\"", "\\\"") + "\"";
                if (i != this.Count - 1)
                    result += ",";
            }
            return "{" + result + "}";
        }

        public static implicit operator ReportRow(string json)
        {
            Dictionary<string, string> row = null;
            try
            {
                row = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            } catch (JsonException)
            {
                return null;
            }
            ReportTable table = new ReportTable();
            ReportRow reportRow = new ReportRow(table);
            foreach (var value in row)
            {
                table.Columns.Add(value.Key);
                reportRow.Add(new ReportCell(reportRow, value.Value));
            }
            table.Add(reportRow);
            return reportRow;
        }
    }

    /// <summary>
    /// Таблица отчета
    /// </summary>
    [TypeConverter(typeof(ReportTableTypeConverter))]
    public class ReportTable : Collection<ReportRow>
    {
        private Collection<string> columns = new Collection<string>();
        public Collection<string> Columns { get { return columns; } }

        public void SetColumns(Collection<string> columnsNames)
        {
            this.columns = columnsNames;
        }

        public ReportTable()
        {
        }

        public override string ToString()
        {
            string result = "";
            for (int i = 0; i < this.Count; i++)
            {
                result += this[i].ToString();
                if (i != this.Count - 1)
                    result += "," + Environment.NewLine;
            }
            return "["+result+"]";
        }

        public static implicit operator ReportTable(string json)
        {
            List<Dictionary<string, string>> rows = null;
            try
            { 
                rows = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(json);
            } catch (JsonException)
            {
                return null;
            }
            ReportTable table = new ReportTable();
            bool is_first_row = true;
            foreach (var row in rows)
            {
                ReportRow reportRow = new ReportRow(table);
                foreach (var value in row)
                {
                    if (is_first_row)
                        table.Columns.Add(value.Key);
                    reportRow.Add(new ReportCell(reportRow, value.Value));
                }
                table.Add(reportRow);
                if (is_first_row)
                    is_first_row = false;
            }
            return table;
        }
    }
}
