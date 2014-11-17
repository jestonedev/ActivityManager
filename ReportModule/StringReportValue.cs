using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Text.RegularExpressions;

namespace ReportModule
{
    /// <summary>
    /// Строковая переменная отчета
    /// </summary>
	public class StringReportValue: ReportValue
	{
        /// <summary>
        /// Шаблон в отчете, который будет заменяться на значение переменной
        /// </summary>
		public string Pattern { get; set; }
        
        /// <summary>
        /// Значение переменной
        /// </summary>
		public string Value { get; set; }
		
        /// <summary>
        /// Конструктор класса StringReportValue
        /// </summary>
        /// <param name="pattern">Шаблон замены. Автоматически обрамляется знаками "$"</param>
        /// <param name="value">Значение переменной</param>
		public StringReportValue(string pattern, string value)
		{
			this.Pattern = "$"+pattern+"$";
			this.Value = value;
		}
	}
}
