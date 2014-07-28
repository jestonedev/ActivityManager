using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ConvertModule
{
    /// <summary>
    /// Мужской пол
    /// </summary>
    public class MaleClass : SexClass
    {
        /// <summary>
        /// Метод-заглушка: переводит из мужского пола в мужской, по факту возвращает строку как она есть
        /// </summary>
        /// <param name="text">Текст, который необходимо перевести</param>
        /// <returns>Результат перевода</returns>
        public override string Translate(string text)
        {
            return text;
        }
    }

    /// <summary>
    /// Женский пол
    /// </summary>
    public class FemaleClass : SexClass
    {
        private Dictionary<string, string> words = new Dictionary<string, string>() {
        {"один","одна"},
        {"два","две"},
        {"нулевой", "нулевая"},
        {"первый", "первая"},
        {"второй", "вторая"},
        {"третий", "третья"},
        {"четвёртый", "четвёртая"},
        {"пятый", "пятая"},
        {"шестой", "шестая"},
        {"седьмой", "седьмая"},
        {"восьмой", "восьмая"},
        {"девятый", "девятая"},
        {"десятый", "десятая"},
        {"одиннадцатый", "одиннадцатая"},
        {"двенадцатый", "двенадцатая"},
        {"тринадцатый", "тринадцатая"},
        {"четырнадцатый", "четырнадцатая"},
        {"пятнадцатый", "пятнадцатая"},
        {"шестнадцатый", "шестнадцатая"},
        {"семнадцатый", "семнадцатая"},
        {"восемнадцатый", "восемнадцатая"},
        {"девятнадцатый", "девятнадцатая"},
        {"двадцатый", "двадцатая"},
        {"тридцатый", "тридцатая"},
        {"сороковой", "сороковая"},
        {"пятидесятый", "пятидесятая"},
        {"шестидесятый", "шестидесятая"},
        {"семидесятый", "семидесятая"},
        {"восьмидесятый", "восьмидесятая"},
        {"девяностый", "девяностая"},
        {"сотый", "сотая"},
        {"двухсотый", "двухсотая"},
        {"трёхсотый", "трёхсотая"},
        {"четырёхсотый", "четырёхсотая"},
        {"пятисотый", "пятисотая"},
        {"шестисотый", "шестисотая"},
        {"семисотый", "семисотая"},
        {"восьмисотый", "восьмисотая"},
        {"девятисотый", "девятисотая"},
        {"тысячный", "тысячная"},
        {"миллионный", "миллионная"},
        {"миллиардный", "миллиардная"},
        {"триллионный", "триллионная"}
        };

        /// <summary>
        /// Метод-переводчик в женский пол из мужского
        /// </summary>
        /// <param name="text">Текст, который необходимо перевести</param>
        /// <returns>Результат перевода</returns>
        public override string Translate(string text)
        {
            if (words.ContainsKey(text))
                return words[text];
            else
                if (Regex.IsMatch(text, @"(ой|ый)$"))
                    return Regex.Replace(text, @"(ой|ый)$", "ая");
                else
                    return text;
        }
    }

    /// <summary>
    /// Средний пол
    /// </summary>
    public class NeuterClass : SexClass
    {
        private Dictionary<string, string> words = new Dictionary<string, string>() {
        {"один","одно"},
        {"третий", "третье"},
        };

        /// <summary>
        /// Метод-переводчик в средний пол из мужского
        /// </summary>
        /// <param name="text">Текст, который необходимо перевести</param>
        /// <returns>Результат перевода</returns>
        public override string Translate(string text)
        {
            if (words.ContainsKey(text))
                return words[text];
            else
                if (Regex.IsMatch(text, @"(ой|ый)$"))
                    return Regex.Replace(text, @"(ой|ый)$", "ое");
                else
                    return text;
        }
    }

    /// <summary>
    /// Класс пола
    /// </summary>
    public abstract class SexClass
    {
        /// <summary>
        /// Абстрактный метод-переводчик в нужный пол из мужского
        /// </summary>
        /// <param name="text">Текст, который необходимо перевести</param>
        /// <returns>Результат перевода</returns>
        public abstract string Translate(string text);
    }
}
