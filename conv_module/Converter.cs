using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace conv_module
{
    public enum TextCase { 
        Nominative/*Кто? Что?*/, 
        Genitive/*Кого? Чего?*/, 
        Dative/*Кому? Чему?*/, 
        Accusative/*Кого? Что?*/, 
        Instrumental/*Кем? Чем?*/, 
        Prepositional/*О ком? О чём?*/ 
    };

    public enum Sex { 
        Male,
        Female,
        Neuter
    }

    public enum CurrencyType
    {
        Ruble,
        Dollar,
        Euro
    }

    public class Converter
    {
        private TextCaseClass textcase;
        private SexClass sex;

        List<string> part1 = new List<string>() { "тысяча", "миллион", "миллиард", "триллион" };
        List<string> part2to4 = new List<string>() { "тысячи", "миллиона", "миллиарда", "триллиона" };
        List<string> part5to9 = new List<string>() { "тысяч", "миллионов", "миллиардов", "триллионов" };
        
        List<string> hundreds = new List<string>() { "", "сто", "двести", "триста", "четыреста", 
                                                    "пятьсот","шестьсот","семьсот","восемьсот","девятьсот"};
        List<string> tens = new List<string>() { "", "", "двадцать", "тридцать", "сорок", "пятьдесят", 
                                                    "шестьдесят","семьдесят","восемьдесят","девяносто"};
        List<string> ones = new List<string>() { "", "один", "два", "три", "четыре", "пять", "шесть", "семь", 
            "восемь", "девять", "десять", "одиннадцать", "двенадцать", "тринадцать", "четырнадцать", 
            "пятнадцать", "шестнадцать", "семнадцать", "восемнадцать", "девятнадцать"};

        List<string> ordinals_ones = new List<string>() { "", "первый", "второй", "третий", "четвертый", "пятый", 
            "шестой", "седьмой", "восьмой", "девятый", "десятый", "одиннадцатый", "двенадцатый", "тринадцатый", 
            "четырнадцатый", "пятнадцатый", "шестнадцатый", "семнадцатый", "восемнадцатый", "девятнадцатый"
        };
        List<string> ordinal_tens = new List<string>() { "", "", "двадцатый", "тридцатый", "сороковой", 
            "пятидесятый", "шестидесятый","семидесятый","восьмидесятый","девяностый"};

        List<string> ordinal_hundreds = new List<string>() { "", "сотый", "двухсотый", "трехсотый", "четырехсотый", 
                                                    "пятисотый","шестисотый","семисотый","восемисотый","девятисотый"};
        List<string> ordinal_part = new List<string>() { "тысячный", "миллионный", "миллиардный", "триллионный" };

        List<string> ordinals_part_ones_prefix = new List<string>() { "", "одно", "двух", "трех", "четырех", "пяти", 
            "шести", "семи", "восьми", "девяти", "десяти", "одиннадцати", "двенадцати", "тринадцати", 
            "четырнадцати", "пятнадцати", "шестнадцати", "семнадцати", "восемнадцати", "девятнадцати"};

        List<string> ordinals_part_tens_prefix = new List<string>() { "", "", "двадцати", "тридцати", "сорока", "пятидесяти", 
                                                    "шестидесяти","семидесяти","восемидесяти","девяносто"};
        List<string> ordinals_part_hundreds_prefix = new List<string>() { "", "сто", "двухсот", "трехсот", "четырехсот", 
                                                    "пятисот","шестисот","семисот","восьмисот","девятисот"};

        List<string> monthes = new List<string>() { "январь", "февраль", "март", "апрель", 
            "май", "июнь", "июль", "август", "сентябрь", "октябрь", "ноябрь", "декабрь" };

        List<string> fractional_postfixs = new List<string>() { "десятая", "сотая", "тысячная", "десятитысячная", "стотысячная", "миллионная", "десятимиллионная", "стомиллионная", "миллиардная" };
        
        public Converter(TextCase textcase, Sex sex)
        {
            switch (textcase)
            {
                case TextCase.Nominative: this.textcase = new NominativeCaseClass();
                    break;
                case TextCase.Genitive: this.textcase = new GenitiveCaseClass();
                    break;
                case TextCase.Dative: this.textcase = new DativeCaseClass();
                    break;
                case TextCase.Accusative: this.textcase = new AccusativeCaseClass();
                    break;
                case TextCase.Instrumental: this.textcase = new InstrumentalCaseClass();
                    break;
                case TextCase.Prepositional: this.textcase = new PrepositionalCaseClass();
                    break;
            }
            switch (sex)
            {
                case Sex.Male: this.sex = new MaleClass();
                    break;
                case Sex.Female: this.sex = new FemaleClass();
                    break;
                case Sex.Neuter: this.sex = new NeuterClass();
                    break;
            }
        }

        private string OrdinalRemainderToText(long remainder, TextCaseClass textcase, SexClass sex, bool last_part, int part_num)
        {
            int x = (int)remainder % 100;
            int hundreds = (int)(remainder - x) / 100;
            int tens = (int)(remainder - (remainder % 10) - hundreds * 100) / 10;
            if (tens >= 2)
                x = (int)remainder % 10;
            string result = "";
            if (hundreds > 0)
            {
                if (!last_part)
                    result = sex.Translate(this.hundreds[hundreds]);
                else
                {
                    if (part_num == 0)
                    {
                        if ((tens == 0) && (x == 0))
                            result = textcase.Translate(sex.Translate(this.ordinal_hundreds[hundreds]));
                        else
                            result = sex.Translate(this.hundreds[hundreds]);
                    }
                    else
                        result = sex.Translate(this.ordinals_part_hundreds_prefix[hundreds]);
                }
            }
            if (tens > 1)
            {
                if ((result.Length > 0) && ((!last_part) || (part_num == 0)))
                    result = result + " ";
                if (!last_part)
                    result = result + sex.Translate(this.tens[tens]);
                else
                {
                    if (part_num == 0)
                    {
                        if (x == 0)
                            result = result + textcase.Translate(sex.Translate(this.ordinal_tens[tens]));
                        else
                            result = result + sex.Translate(this.tens[tens]);
                    }
                    else
                        result = result + sex.Translate(this.ordinals_part_tens_prefix[tens]);
                }
            }
            if (x > 0)
            {
                if ((result.Length > 0) && ((!last_part) || (part_num == 0)))
                    result = result + " ";
                if (!last_part)
                    result = result + sex.Translate(this.ones[x]);
                else
                {
                    if (part_num == 0)
                        result = result + textcase.Translate(sex.Translate(this.ordinals_ones[x]));
                    else
                        result = result + sex.Translate(this.ordinals_part_ones_prefix[x]);
                }
            }
            return result;
        }

        private string CardinalRemainderToText(long remainder, SexClass sex)
        {
            int x = (int)remainder % 100;
            int hundreds = (int)(remainder - x) / 100;
            int tens = (int)(remainder - (remainder % 10) - hundreds*100) / 10;
            if (tens >= 2)
                x = (int)remainder % 10;
            string result = "";
            if (hundreds > 0)
                result = sex.Translate(this.hundreds[hundreds]);
            if (tens > 1)
            {
                if (result.Length > 0)
                    result = result + " ";
                result = result + sex.Translate(this.tens[tens]);
            }
            if (x > 0)
            {
                if (result.Length > 0)
                    result = result + " ";
                result = result + sex.Translate(this.ones[x]);
            }
            return result;
        }

        private string CardinalToText(long number)
        {
            string result = "";
            if (number > Math.Pow(10, 15))
                return "";
            int i = 0;
            while (number != 0)
            {
                long remainder = number % 1000;
                number = (number - remainder) / 1000;
                SexClass sex_part;
                if (i == 0)
                    sex_part = this.sex;
                else
                if (i == 1)
                    sex_part = new FemaleClass();
                else
                    sex_part = new MaleClass();
                string remainder_str = CardinalRemainderToText(remainder, sex_part);
                long prefix;
                if (remainder % 100 < 20)
                    prefix = remainder % 100;
                else
                    prefix = remainder % 10;
                if ((i > 0) && (remainder_str.Length > 0))
                {
                    remainder_str += " ";
                    if (prefix == 1)
                        remainder_str += part1[i - 1];
                    else
                        if ((prefix >= 2) && (prefix <= 4))
                            remainder_str += part2to4[i - 1];
                        else
                            remainder_str += part5to9[i - 1];
                }
                if ((result.Length > 0) && (remainder_str.Length > 0))
                    result = " " + result;
                result = remainder_str + result;
                i++;
            }
            if (result.Length == 0)
                result = "ноль";
            string[] parts = result.Split(new char[] { ' ' });
            result = "";
            for (i = 0; i < parts.Length; i++)
            {
                if (result.Length > 0)
                    result += " ";
                result = result + textcase.Translate(parts[i]);
            }
            return result;
        }

        private string OrdinalToText(long number)
        {
            string result = "";
            if (number > Math.Pow(10, 15))
                return "";
            int i = 0;
            while (number != 0)
            {
                long remainder = number % 1000;
                number = (number - remainder) / 1000;
                SexClass sex_part;
                if (i == 0)
                    sex_part = this.sex;
                else
                    if (i == 1)
                        sex_part = new FemaleClass();
                    else
                        sex_part = new MaleClass();
                string remainder_str = OrdinalRemainderToText(remainder, textcase, sex_part, result.Length == 0, i);
                long prefix;
                if (remainder % 100 < 20)
                    prefix = remainder % 100;
                else
                    prefix = remainder % 10;
                if ((i > 0) && (remainder_str.Length > 0))
                {
                    if (result.Length == 0)
                    {
                        remainder_str += textcase.Translate(sex.Translate(ordinal_part[i - 1]));
                    }
                    else
                    {
                        remainder_str += " ";
                        if (prefix == 1)
                            remainder_str += part1[i - 1];
                        else
                            if ((prefix >= 2) && (prefix <= 4))
                                remainder_str += part2to4[i - 1];
                            else
                                remainder_str += part5to9[i - 1];
                    }
                }
                if ((result.Length > 0) && (remainder_str.Length > 0))
                    result = " " + result;
                result = remainder_str + result;
                i++;
            }
            if (result.Length == 0)
                result = textcase.Translate(sex.Translate("нулевой"));
            return result;
        }

        public string NumberToText(long number, bool is_ordinal)
        {

            if (is_ordinal)
                return OrdinalToText(number);
            else
                return CardinalToText(number);
        }

        private TextCaseClass letter_to_textcase(string letter)
        {
            switch (letter)
            {
                case "n": return new NominativeCaseClass();
                case "g": return new GenitiveCaseClass();
                case "d": return new DativeCaseClass();
                case "a": return new AccusativeCaseClass();
                case "i": return new InstrumentalCaseClass();
                case "p": return new PrepositionalCaseClass();
                default: throw new ApplicationException("Неизвестный падеж");
            }
        }

        public string DateTimeToText(DateTime datetime, string format)
        {
            Match match_day = Regex.Match(format, "dd[ngdaip]");
            while (match_day.Success)
            {
                int day = datetime.Day;
                this.textcase = letter_to_textcase(match_day.Value[2].ToString());
                this.sex = new NeuterClass();
                string day_str = NumberToText(day, true);
                format = Regex.Replace(format, match_day.Value, day_str);
                match_day = match_day.NextMatch();
            }
            Match match_month = Regex.Match(format, "MM[ngdaip]");
            while (match_month.Success)
            {
                int month = datetime.Month;
                this.textcase = letter_to_textcase(match_month.Value[2].ToString());
                format = Regex.Replace(format, match_month.Value, 
                    this.textcase.Translate(this.monthes[month - 1]));
                match_month = match_month.NextMatch();
            }
            Match match_year = Regex.Match(format, "yyyy[ngdaip]");
            while (match_year.Success)
            {
                int year = datetime.Year;
                this.sex = new MaleClass();
                this.textcase = letter_to_textcase(match_year.Value[4].ToString());
                format = Regex.Replace(format, match_year.Value, NumberToText(year, true) + " " +
                    this.textcase.Translate("год"));
                match_year = match_year.NextMatch();
            }
            match_year = Regex.Match(format, "yy[ngdaip]");
            while (match_year.Success)
            {
                int year = datetime.Year % 100;
                this.textcase = letter_to_textcase(match_year.Value[2].ToString());
                this.sex = new MaleClass();
                format = Regex.Replace(format, match_year.Value, NumberToText(year, true)+" "+
                    this.textcase.Translate("год"));
                match_year = match_year.NextMatch();
            }
            Match match_hour = Regex.Match(format, "hh[ngdaip]");
            while (match_hour.Success)
            {
                int hour = datetime.Hour % 12 == 0 ? 12 : datetime.Hour % 12;
                string hour_postfix = "";
                if (hour == 1)
                    hour_postfix = "час";
                else
                if (hour >= 5)
                    hour_postfix = "часов";
                else
                    hour_postfix = "часа";
                this.textcase = letter_to_textcase(match_hour.Value[2].ToString());
                this.sex = new MaleClass();
                format = Regex.Replace(format, match_hour.Value, NumberToText(hour, false)+" "+
                    this.textcase.Translate(hour_postfix));
                match_hour = match_hour.NextMatch();
            }
            match_hour = Regex.Match(format, "HH[ngdaip]");
            while (match_hour.Success)
            {
                int hour = datetime.Hour;
                string hour_postfix = "";
                if ((hour == 1) || (hour == 21))
                    hour_postfix = "час";
                else
                if (((hour >= 5) && (hour <= 20)) || (hour == 0))
                    hour_postfix = "часов";
                else
                    hour_postfix = "часа";
                this.sex = new MaleClass();
                this.textcase = letter_to_textcase(match_hour.Value[2].ToString());
                format = Regex.Replace(format, match_hour.Value, NumberToText(hour, false) + " " +
                    this.textcase.Translate(hour_postfix));
                match_hour = match_hour.NextMatch();
            }
            Match match_minute = Regex.Match(format, "mm[ngdaip]");
            while (match_minute.Success)
            {
                int minute = datetime.Minute;
                string minute_postfix = "";
                if (minute % 10 == 1)
                {
                    if (minute != 11)
                        minute_postfix = "минута";
                    else
                        minute_postfix = "минут";
                } else
                if ((minute % 10 >= 5) || (minute % 10 == 0))
                    minute_postfix = "минут";
                else
                {
                    if ((minute >= 12) && (minute <= 14))
                        minute_postfix = "минут";
                    else
                        minute_postfix = "минуты";
                }
                this.textcase = letter_to_textcase(match_minute.Value[2].ToString());
                this.sex = new FemaleClass();
                format = Regex.Replace(format, match_minute.Value, NumberToText(minute, false) + " " +
                    this.textcase.Translate(minute_postfix));
                match_minute = match_minute.NextMatch();
            }
            Match match_second = Regex.Match(format, "ss[ngdaip]");
            while (match_second.Success)
            {
                int second = datetime.Second;
                string second_postfix = "";
                if (second % 10 == 1)
                {
                    if (second != 11)
                        second_postfix = "секунда";
                    else
                        second_postfix = "секунд";
                }
                else
                    if ((second % 10 >= 5) || (second % 10 == 0))
                        second_postfix = "секунд";
                    else
                    {
                        if ((second >= 12) && (second <= 14))
                            second_postfix = "секунд";
                        else
                            second_postfix = "секунды";
                    }
                this.textcase = letter_to_textcase(match_second.Value[2].ToString());
                this.sex = new FemaleClass();
                format = Regex.Replace(format, match_second.Value, NumberToText(second, false) + " " +
                    this.textcase.Translate(second_postfix));
                match_second = match_second.NextMatch();
            }
            return datetime.ToString(format);
        }

        private List<string> GetCurrenctyPostfix(CurrencyType currency_type, double currency, bool is_ordinal)
        {
            string[] currency_parts = currency.ToString().Split(new char[] { '.', ',' });
            long integral = long.Parse(currency_parts[0]);
            long fractional = 0;
            if (currency_parts.Length > 1)
                fractional = long.Parse(currency_parts[1].Substring(0,2));
            string integral_postfix = "";
            string fractional_postfix = "";
            switch (currency_type)
            {
                case CurrencyType.Ruble:
                    if (is_ordinal)
                        integral_postfix = "рубль";
                    else
                    if ((integral % 10 == 0) || (integral % 10 >= 5) || ((integral % 100 >= 11) && (integral % 100 <= 14)))
                        integral_postfix = "рублей";
                    else
                    if (integral % 10 == 1)
                        integral_postfix = "рубль";
                    else
                        integral_postfix = "рубля";
                    if (is_ordinal)
                        fractional_postfix = "копейка";
                    else
                    if ((fractional % 10 == 0) || (fractional % 10 >= 5) || 
                        ((fractional >= 11) && (fractional <= 14)))
                        fractional_postfix = "копеек";
                    else
                        if (fractional % 10 == 1)
                            fractional_postfix = "копейка";
                        else
                            fractional_postfix = "копейки";
                    return new List<string>() { integral_postfix, fractional_postfix };
                case CurrencyType.Dollar:
                    if (is_ordinal)
                        integral_postfix = "доллар";
                    else
                    if ((integral % 10 == 0) && (integral % 10 >= 5) || ((integral >= 11) && (integral <= 14)))
                        integral_postfix = "долларов";
                    else
                    if (integral % 10 == 1)
                        integral_postfix = "доллар";
                    else
                        integral_postfix = "доллара";
                    if (is_ordinal)
                        fractional_postfix = "цент";
                    else
                    if ((fractional % 10 == 0) && (fractional % 10 >= 5) ||
                        ((fractional >= 11) && (fractional <= 14)))
                        fractional_postfix = "центов";
                    else
                        if (fractional % 10 == 1)
                            fractional_postfix = "цент";
                        else
                            fractional_postfix = "цента";
                    return new List<string>() { integral_postfix, fractional_postfix };
                case CurrencyType.Euro:
                    integral_postfix = "евро";
                    if (is_ordinal)
                        fractional_postfix = "цент";
                    else
                    if ((fractional % 10 == 0) && (fractional % 10 >= 5) ||
                        ((fractional >= 11) && (fractional <= 14)))
                        fractional_postfix = "центов";
                    else
                        if (fractional % 10 == 1)
                            fractional_postfix = "цент";
                        else
                            fractional_postfix = "цента";
                    return new List<string>() { integral_postfix, fractional_postfix };
                default: throw new ApplicationException("Неизвестный тип валюты");
            }
        }

        public string CurrencyToString(double currency, CurrencyType currency_type, string format, string thousand_separator, bool is_ordinal)
        {
            List<string> currency_types = GetCurrenctyPostfix(currency_type, currency, is_ordinal);
            string[] currency_parts = currency.ToString().Split(new char[] { '.', ',' }, 2);
            long integral = long.Parse(currency_parts[0]);
            if (currency_type == CurrencyType.Euro)
                this.sex = new NeuterClass();
            else
                this.sex = new MaleClass();
            Match match_integral = Regex.Match(format, "ii[ngdaip]");
            while (match_integral.Success)
            {
                this.textcase = letter_to_textcase(match_integral.Value[2].ToString());
                string integral_part = NumberToText(integral, is_ordinal);
                format = format.Replace(match_integral.Value, integral_part);
                match_integral = match_integral.NextMatch();
            }
            Match match_rur = Regex.Match(format, "r[ngdaip]");
            while (match_rur.Success)
            {
                this.textcase = letter_to_textcase(match_rur.Value[1].ToString());
                format = format.Replace(match_rur.Value, this.textcase.Translate(currency_types[0]));
                match_rur = match_rur.NextMatch();
            }
            long fractional = 0;
            if (currency_parts.Length > 1)
                fractional = long.Parse(currency_parts[1].Substring(0, 2));
            if (currency_type == CurrencyType.Ruble)
                this.sex = new FemaleClass();
            else
                this.sex = new MaleClass();
            Match match_fractional = Regex.Match(format, "ff[ngdaip]");
            while (match_fractional.Success)
            {
                this.textcase = letter_to_textcase(match_fractional.Value[2].ToString());
                string fractional_part = NumberToText(fractional, is_ordinal);
                format = format.Replace(match_integral.Value, fractional_part);
                match_fractional = match_fractional.NextMatch();
            }
            Match match_kop = Regex.Match(format, "k[ngdaip]");
            while (match_kop.Success)
            {
                this.textcase = letter_to_textcase(match_kop.Value[1].ToString());
                format = format.Replace(match_kop.Value, this.textcase.Translate(currency_types[1]));
                match_kop = match_kop.NextMatch();
            }
            string integral_num = "";
            while (integral > 0)
            {
                long integral_part = integral % 1000;
                integral = (integral - integral_part) / 1000;
                string integral_part_str = integral_part.ToString();
                if ((integral_part_str.Length == 1) && (integral > 0))
                    integral_part_str = "00" + integral_part_str;
                else
                if ((integral_part_str.Length == 2) && (integral > 0))
                    integral_part_str = "0" + integral_part_str;
                integral_num = integral_part_str + integral_num;
                if (integral > 0)
                    integral_num = thousand_separator + integral_num;
            }
            if (integral_num == "")
                integral_num = "0";
            format = format.Replace("ii", integral_num);
            string fractional_str = fractional.ToString();
            if (fractional_str.Length == 1)
                fractional_str = "0" + fractional_str;
            format = format.Replace("ff", fractional_str);
            return format;
        }

        public string FloatToString(float number)
        {
            string[] number_parts = number.ToString().Split(new char[] { '.', ',' }, 2);
            long integral = long.Parse(number_parts[0]);
            long fractional = 0;
            if (number_parts.Length > 1)
                fractional = long.Parse(number_parts[1]);
            string integrel_part = "";
            integrel_part = NumberToText(integral, false);
            string fractional_part = NumberToText(fractional, false);
            string integral_postfix = "";
            string fractional_postfix = "";
            if ((integral % 10 == 1) && (integral != 11))
                integral_postfix = "целая";
            else
                integral_postfix ="целых";
            if ((fractional % 10 == 1) && (fractional != 11))
                fractional_postfix = fractional_postfixs[fractional.ToString().Length-1];
            else
                fractional_postfix = Regex.Replace(fractional_postfixs[fractional.ToString().Length-1],@"(ая)$", "ых");
            string full_text = integrel_part;
            if (fractional != 0)
                full_text = full_text + " " + this.textcase.Translate(integral_postfix) 
                    + " " + fractional_part + " " + this.textcase.Translate(fractional_postfix);
            return full_text;
        }
    }
}
