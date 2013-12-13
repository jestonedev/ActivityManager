using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace conv_module
{
    public class MaleClass : SexClass
    {
        public override string Translate(string text)
        {
            return text;
        }
    }

    public class FemaleClass : SexClass
    {
        Dictionary<string, string> words = new Dictionary<string, string>() {
        {"один","одна"},
        {"два","две"},
        {"нулевой", "нулевая"},
        {"первый", "первая"},
        {"второй", "вторая"},
        {"третий", "третья"},
        {"четвертый", "четвертая"},
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
        {"трехсотый", "трехсотая"},
        {"четырехсотый", "четырехсотая"},
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

    public class NeuterClass : SexClass
    {
        Dictionary<string, string> words = new Dictionary<string, string>() {
        {"один","одно"},
        {"третий", "третье"},
        };

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

    public abstract class SexClass
    {
        public abstract string Translate(string text);
    }
}
