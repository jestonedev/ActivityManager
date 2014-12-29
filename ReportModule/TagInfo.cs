using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReportModule
{
    internal enum SpecTag { B, I, U, S }
    internal enum SpecTagType { OpenTag, CloseTag }
    /// <summary>
    /// Перечисление стилей оформления
    /// </summary>
    internal enum Style
    {
        /// <summary>
        /// Жирный
        /// </summary>
        Bold,
        /// <summary>
        /// Курсив
        /// </summary>
        Italic,
        /// <summary>
        /// Подчеркивание
        /// </summary>
        Underline,
        /// <summary>
        /// Зачеркнутый текст
        /// </summary>
        Strike,
        /// <summary>
        /// Отсутствует
        /// </summary>
        None
    };

    internal class TagInfo
    {
        public SpecTag tag { get; set; }
        public SpecTagType tag_type { get; set; }
        public TagInfo(SpecTag tag, SpecTagType tag_type)
        {
            this.tag = tag;
            this.tag_type = tag_type;
        }
    }
}
