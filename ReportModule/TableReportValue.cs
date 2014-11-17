using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using ExtendedTypes;

namespace ReportModule
{
    /// <summary>
    /// Табличная переменная отчета
    /// </summary>
	public class TableReportValue : ReportValue
	{
        private ReportTable table;
        /// <summary>
        /// Таблица отчета
        /// </summary>
        public ReportTable Table { get { return table; } }

        /// <summary>
        /// XML-замыкатель
        /// </summary>
		public string XmlContractor { get; set; }

        /// <summary>
        /// Конструктор класса TableReportValue
        /// </summary>
        /// <param name="table">Таблица, которая будет вставлена в отчет</param>
        /// <param name="xmlContractor">XML-замыкатель</param>
		public TableReportValue(ReportTable table, string xmlContractor)
		{
			this.table = table;
			this.XmlContractor = xmlContractor;
		}
	}
}
