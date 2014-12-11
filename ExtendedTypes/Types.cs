using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

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
                result += "\"" + this.table.Columns[i].Replace("\"", "\\\"") + "\":\"" + this[i].Value.Replace("\"", "\\\"") + "\"";
                if (i != this.Count - 1)
                    result += ",";
            }
            return "{" + result + "}";
        }
    }

    /// <summary>
    /// Таблица отчета
    /// </summary>
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
            return result;
        }
    }

}
