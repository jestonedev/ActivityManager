using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace extended_types
{
    /// <summary>
    /// Ячейка таблицы отчета
    /// </summary>
    public class ReportCell
    {
        public string Value { get; set; }

        private ReportRow row;
        public ReportRow Row { get { return row; } }

        public ReportCell(ReportRow Row)
        {
            this.row = Row;
        }

        public ReportCell(ReportRow Row, string Value)
        {
            this.Value = Value;
            this.row = Row;
        }
    }

    /// <summary>
    /// Строка таблицы отчета
    /// </summary>
    public class ReportRow : List<ReportCell>
    {
        private ReportTable table;
        public ReportTable Table { get { return table; } }

        public ReportRow(ReportTable Table)
        {
            this.table = Table;
        }

        public ReportCell this[string column_name]
        {
            get
            {
                return this[this.table.Columns.IndexOf(column_name)];
            }
        }
    }

    /// <summary>
    /// Таблица отчета
    /// </summary>
    public class ReportTable : List<ReportRow>
    {
        public List<string> Columns { get; set; }

        public ReportTable()
        {
            Columns = new List<string>();
        }
    }
}
