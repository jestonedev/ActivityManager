using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Globalization;

namespace ConvertModule
{
    /// <summary>
    /// Падеж
    /// </summary>
    public enum TextCase
    {
        /// <summary>
        /// Кто? Что?
        /// </summary>
        Nominative,
        /// <summary>
        /// Кого? Чего?
        /// </summary>
        Genitive,
        /// <summary>
        /// Кому? Чему?
        /// </summary>
        Dative,
        /// <summary>
        /// Кого? Что?
        /// </summary>
        Accusative,
        /// <summary>
        /// Кем? Чем?
        /// </summary>
        Instrumental,
        /// <summary>
        /// О ком? О чём?
        /// </summary>
        Prepositional
    };

    /// <summary>
    /// Пол
    /// </summary>
    public enum Sex
    {
        /// <summary>
        /// Мужской
        /// </summary>
        Male,
        /// <summary>
        /// Женский
        /// </summary>
        Female,
        /// <summary>
        /// Средний
        /// </summary>
        Neuter
    }

    /// <summary>
    /// Тип валюты
    /// </summary>
    public enum CurrencyType
    {
        /// <summary>
        /// Рубли
        /// </summary>
        Ruble,
        /// <summary>
        /// Доллары
        /// </summary>
        Dollar,
        /// <summary>
        /// Евро
        /// </summary>
        Euro
    }

    /// <summary>
    /// Базовый класс конвертации
    /// </summary>
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
                                                    "пятисотый","шестисотый","семисотый","восьмисотый","девятисотый"};
        List<string> ordinal_part = new List<string>() { "тысячный", "миллионный", "миллиардный", "триллионный" };

        List<string> ordinals_part_ones_prefix = new List<string>() { "", "одно", "двух", "трех", "четырех", "пяти", 
            "шести", "семи", "восьми", "девяти", "десяти", "одиннадцати", "двенадцати", "тринадцати", 
            "четырнадцати", "пятнадцати", "шестнадцати", "семнадцати", "восемнадцати", "девятнадцати"};

        List<string> ordinals_part_tens_prefix = new List<string>() { "", "", "двадцати", "тридцати", "сорока", "пятидесяти", 
                                                    "шестидесяти","семидесяти","восьмидесяти","девяносто"};
        List<string> ordinals_part_hundreds_prefix = new List<string>() { "", "сто", "двухсот", "трехсот", "четырехсот", 
                                                    "пятисот","шестисот","семисот","восьмисот","девятисот"};

        List<string> monthes = new List<string>() { "январь", "февраль", "март", "апрель", 
            "май", "июнь", "июль", "август", "сентябрь", "октябрь", "ноябрь", "декабрь" };

        List<string> fractional_postfixs = new List<string>() { "десятая", "сотая", "тысячная", "десятитысячная", "стотысячная", "миллионная", "десятимиллионная", "стомиллионная", "миллиардная" };

        /// <summary>
        /// Конструктор класса конвертации
        /// </summary>
        /// <param name="textCase">Падеж по умолчанию, в который мы планируем конвертировать</param>
        /// <param name="sex">Пол по умолчанию, в который мы планируем конвертировать</param>
        public Converter(TextCase textCase, Sex sex)
        {
            switch (textCase)
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

        private string OrdinalRemainderToText(long remainder, TextCaseClass textCase, SexClass inSex, bool lastPart, int partNum)
        {
            int x = (int)remainder % 100;
            int hundreds_count = (int)(remainder - x) / 100;
            int tens_count = (int)(remainder - (remainder % 10) - hundreds_count * 100) / 10;
            if (tens_count >= 2)
                x = (int)remainder % 10;
            string result = "";
            if (hundreds_count > 0)
            {
                if (!lastPart)
                    result = inSex.Translate(this.hundreds[hundreds_count]);
                else
                {
                    if (partNum == 0)
                    {
                        if ((tens_count == 0) && (x == 0))
                            result = textCase.Translate(inSex.Translate(this.ordinal_hundreds[hundreds_count]));
                        else
                            result = inSex.Translate(this.hundreds[hundreds_count]);
                    }
                    else
                        result = inSex.Translate(this.ordinals_part_hundreds_prefix[hundreds_count]);
                }
            }
            if (tens_count > 1)
            {
                if ((result.Length > 0) && ((!lastPart) || (partNum == 0)))
                    result = result + " ";
                if (!lastPart)
                    result = result + inSex.Translate(this.tens[tens_count]);
                else
                {
                    if (partNum == 0)
                    {
                        if (x == 0)
                            result = result + textCase.Translate(inSex.Translate(this.ordinal_tens[tens_count]));
                        else
                            result = result + inSex.Translate(this.tens[tens_count]);
                    }
                    else
                        result = result + inSex.Translate(this.ordinals_part_tens_prefix[tens_count]);
                }
            }
            if (x > 0)
            {
                if ((result.Length > 0) && ((!lastPart) || (partNum == 0)))
                    result = result + " ";
                if (!lastPart)
                    result = result + inSex.Translate(this.ones[x]);
                else
                {
                    if (partNum == 0)
                        result = result + textCase.Translate(inSex.Translate(this.ordinals_ones[x]));
                    else
                        result = result + inSex.Translate(this.ordinals_part_ones_prefix[x]);
                }
            }
            return result;
        }

        private string CardinalRemainderToText(long remainder, SexClass in_sex)
        {
            int x = (int)remainder % 100;
            int hundreds_count = (int)(remainder - x) / 100;
            int tens_count = (int)(remainder - (remainder % 10) - hundreds_count * 100) / 10;
            if (tens_count >= 2)
                x = (int)remainder % 10;
            string result = "";
            if (hundreds_count > 0)
                result = in_sex.Translate(this.hundreds[hundreds_count]);
            if (tens_count > 1)
            {
                if (result.Length > 0)
                    result = result + " ";
                result = result + in_sex.Translate(this.tens[tens_count]);
            }
            if (x > 0)
            {
                if (result.Length > 0)
                    result = result + " ";
                result = result + in_sex.Translate(this.ones[x]);
            }
            return result;
        }

        private string CardinalToText(long number)
        {
            string result = "";
            bool negative = false;
            if (number < 0)
            {
                number = Math.Abs(number);
                negative = true;
            }
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
            if (negative)
                result = "минус " + result;
            return result;
        }

        private string OrdinalToText(long number)
        {
            string result = "";
            bool negative = false;
            if (number < 0)
            {
                number = Math.Abs(number);
                negative = true;
            }
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
            if (negative)
                result = "минус " + result;
            return result;
        }

        /// <summary>
        /// Метод конвертации числа в текст
        /// </summary>
        /// <param name="number">Число</param>
        /// <param name="isOrdinal">Если true - порядковое числительное, если false - количественное числительное</param>
        /// <returns>Строковое представление числа</returns>
        public string NumberToText(long number, bool isOrdinal)
        {
            if (isOrdinal)
                return OrdinalToText(number);
            else
                return CardinalToText(number);
        }

        private static TextCaseClass LetterToTextCase(string letter)
        {
            switch (letter)
            {
                case "n": return new NominativeCaseClass();
                case "g": return new GenitiveCaseClass();
                case "d": return new DativeCaseClass();
                case "a": return new AccusativeCaseClass();
                case "i": return new InstrumentalCaseClass();
                case "p": return new PrepositionalCaseClass();
                default: throw new ConvertException("Неизвестный падеж");
            }
        }

        private string DayToText(DateTime dateTime, string format)
        {
            Match match_day = Regex.Match(format, "dd[ngdaip]");
            while (match_day.Success)
            {
                int day = dateTime.Day;
                this.textcase = LetterToTextCase(match_day.Value[2].ToString());
                this.sex = new NeuterClass();
                string day_str = NumberToText(day, true);
                format = Regex.Replace(format, match_day.Value, day_str);
                match_day = match_day.NextMatch();
            }
            return format;
        }

        private string MonthToText(DateTime dateTime, string format)
        {
            Match match_month = Regex.Match(format, "MM[ngdaip]");
            while (match_month.Success)
            {
                int month = dateTime.Month;
                this.textcase = LetterToTextCase(match_month.Value[2].ToString());
                format = Regex.Replace(format, match_month.Value,
                    this.textcase.Translate(this.monthes[month - 1]));
                match_month = match_month.NextMatch();
            }
            return format;
        }

        private string YearToText(DateTime dateTime, string format)
        {
            Match match_year = Regex.Match(format, "yyyy[ngdaip]");
            while (match_year.Success)
            {
                int year = dateTime.Year;
                this.sex = new MaleClass();
                this.textcase = LetterToTextCase(match_year.Value[4].ToString());
                format = Regex.Replace(format, match_year.Value, NumberToText(year, true) + " " +
                    this.textcase.Translate("год"));
                match_year = match_year.NextMatch();
            }
            match_year = Regex.Match(format, "yy[ngdaip]");
            while (match_year.Success)
            {
                int year = dateTime.Year % 100;
                this.textcase = LetterToTextCase(match_year.Value[2].ToString());
                this.sex = new MaleClass();
                format = Regex.Replace(format, match_year.Value, NumberToText(year, true) + " " +
                    this.textcase.Translate("год"));
                match_year = match_year.NextMatch();
            }
            return format;
        }

        private string HoursToText(DateTime dateTime, string format)
        {
            Match match_hour = Regex.Match(format, "hh[ngdaip]");
            while (match_hour.Success)
            {
                int hour = dateTime.Hour % 12 == 0 ? 12 : dateTime.Hour % 12;
                string hour_postfix = "";
                if (hour == 1)
                    hour_postfix = "час";
                else
                    if (hour >= 5)
                        hour_postfix = "часов";
                    else
                        hour_postfix = "часа";
                this.textcase = LetterToTextCase(match_hour.Value[2].ToString());
                this.sex = new MaleClass();
                format = Regex.Replace(format, match_hour.Value, NumberToText(hour, false) + " " +
                    this.textcase.Translate(hour_postfix));
                match_hour = match_hour.NextMatch();
            }
            match_hour = Regex.Match(format, "HH[ngdaip]");
            while (match_hour.Success)
            {
                int hour = dateTime.Hour;
                string hour_postfix = "";
                if ((hour == 1) || (hour == 21))
                    hour_postfix = "час";
                else
                    if (((hour >= 5) && (hour <= 20)) || (hour == 0))
                        hour_postfix = "часов";
                    else
                        hour_postfix = "часа";
                this.sex = new MaleClass();
                this.textcase = LetterToTextCase(match_hour.Value[2].ToString());
                format = Regex.Replace(format, match_hour.Value, NumberToText(hour, false) + " " +
                    this.textcase.Translate(hour_postfix));
                match_hour = match_hour.NextMatch();
            }
            return format;
        }

        private string MinutesToText(DateTime dateTime, string format)
        {
            Match match_minute = Regex.Match(format, "mm[ngdaip]");
            while (match_minute.Success)
            {
                int minute = dateTime.Minute;
                string minute_postfix = "";
                if (minute % 10 == 1)
                {
                    if (minute != 11)
                        minute_postfix = "минута";
                    else
                        minute_postfix = "минут";
                }
                else
                    if ((minute % 10 >= 5) || (minute % 10 == 0))
                        minute_postfix = "минут";
                    else
                    {
                        if ((minute >= 12) && (minute <= 14))
                            minute_postfix = "минут";
                        else
                            minute_postfix = "минуты";
                    }
                this.textcase = LetterToTextCase(match_minute.Value[2].ToString());
                this.sex = new FemaleClass();
                format = Regex.Replace(format, match_minute.Value, NumberToText(minute, false) + " " +
                    this.textcase.Translate(minute_postfix));
                match_minute = match_minute.NextMatch();
            }
            return format;
        }

        private string SecondsToText(DateTime dateTime, string format)
        {
            Match match_second = Regex.Match(format, "ss[ngdaip]");
            while (match_second.Success)
            {
                int second = dateTime.Second;
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
                this.textcase = LetterToTextCase(match_second.Value[2].ToString());
                this.sex = new FemaleClass();
                format = Regex.Replace(format, match_second.Value, NumberToText(second, false) + " " +
                    this.textcase.Translate(second_postfix));
                match_second = match_second.NextMatch();
            }
            return format;
        }

        /// <summary>
        /// Метод конвертации даты в текст
        /// </summary>
        /// <param name="dateTime">Дата</param>
        /// <param name="format">
        /// Формат даты и времени. Можно задавать стандартный формат даты и времени, либо расширенный вариант:
        /// ddx - день месяца в виде текста
        /// MMx - месяц в виде текста
        /// yyx - год в формате двухзначного числа в виде текста
        /// yyyyx - год в формате четырехзначного числа в виде текста
        /// hhx - время от 1 до 12 в виде текста
        /// HHx - время от 0 до 23 в виде текста
        /// mmx - минуты в виде текста
        /// ssx - секунды в виде текста
        /// Буква "х" во всех расширенных форматах - первая буква падежа n,g,d,a,i,p
        /// </param>
        /// <returns>Строковое представление даты</returns>
        public string DateTimeToText(DateTime dateTime, string format)
        {
            format = DayToText(dateTime, format);
            format = MonthToText(dateTime, format);
            format = YearToText(dateTime, format);
            format = HoursToText(dateTime, format);
            format = MinutesToText(dateTime, format);
            format = SecondsToText(dateTime, format);
            return dateTime.ToString(format, CultureInfo.CurrentCulture);
        }

        private static string GetIntegralPart(CurrencyType currencyType, bool isOrdinal, long integral)
        {
            switch (currencyType)
            {
                case CurrencyType.Ruble:
                    if (isOrdinal)
                        return "рубль";
                    else
                        if ((integral % 10 == 0) || (integral % 10 >= 5) || ((integral % 100 >= 11) && (integral % 100 <= 14)))
                            return "рублей";
                        else
                            if (integral % 10 == 1)
                                return "рубль";
                            else
                                return "рубля";
                case CurrencyType.Dollar:
                    if (isOrdinal)
                        return "доллар";
                    else
                        if ((integral % 10 == 0) && (integral % 10 >= 5) || ((integral >= 11) && (integral <= 14)))
                            return "долларов";
                        else
                            if (integral % 10 == 1)
                                return "доллар";
                            else
                                return "доллара";
                case CurrencyType.Euro:
                    return "евро";
                default: throw new ConvertException("Неизвестный тип валюты");
            }
        }

        private static string GetFractionalPart(CurrencyType currencyType, bool isOrdinal, long fractional)
        {
            switch (currencyType)
            {
                case CurrencyType.Ruble:
                    if (isOrdinal)
                        return "копейка";
                    else
                        if ((fractional % 10 == 0) || (fractional % 10 >= 5) ||
                            ((fractional >= 11) && (fractional <= 14)))
                            return "копеек";
                        else
                            if (fractional % 10 == 1)
                                return "копейка";
                            else
                                return "копейки";
                case CurrencyType.Dollar:
                    if (isOrdinal)
                        return "цент";
                    else
                        if ((fractional % 10 == 0) && (fractional % 10 >= 5) ||
                            ((fractional >= 11) && (fractional <= 14)))
                            return "центов";
                        else
                            if (fractional % 10 == 1)
                                return "цент";
                            else
                                return "цента";
                case CurrencyType.Euro:
                    if (isOrdinal)
                        return "цент";
                    else
                        if ((fractional % 10 == 0) && (fractional % 10 >= 5) ||
                            ((fractional >= 11) && (fractional <= 14)))
                            return "центов";
                        else
                            if (fractional % 10 == 1)
                                return "цент";
                            else
                                return "цента";
                default: throw new ConvertException("Неизвестный тип валюты");
            }
        }

        private static List<string> GetCurrenctyPostfix(ConvertModule.CurrencyType currencyType, double currency, bool isOrdinal)
        {
            string[] currency_parts = currency.ToString(CultureInfo.CurrentCulture).Split(new char[] { '.', ',' });
            long integral = long.Parse(currency_parts[0], CultureInfo.CurrentCulture);
            long fractional = 0;
            if (currency_parts.Length > 1)
            {
                if (currency_parts[1].Length == 1)
                    fractional = long.Parse(currency_parts[1][0] + "0", CultureInfo.CurrentCulture);
                else
                    fractional = long.Parse(currency_parts[1].Substring(0, 2), CultureInfo.CurrentCulture);
            }
            string integral_postfix = GetIntegralPart(currencyType, isOrdinal, integral);
            string fractional_postfix = GetFractionalPart(currencyType, isOrdinal, fractional);
            return new List<string>() { integral_postfix, fractional_postfix };
        }

        /// <summary>
        /// Метод конвертации суммы в строку
        /// </summary>
        /// <param name="currency">Сумма</param>
        /// <param name="currencyType">Тип валюты</param>
        /// <param name="format">
        /// Формат строки вывода суммы:
        /// ii - рубли (доллары, евро) в виде числа
        /// ff - копейки (центы) в виде числа
        /// iix - рубли (доллары, евро) в виде строки
        /// ffx - копейки (центы) в виде строки
        /// rx - слово "рубль" ("доллар", "евро")
        /// kx - словок "копейка" ("цент")
        /// nn - слово "минус ", если число отрицательное. Если число положительное, то пустая строка. Пробел после слова минус ставится автоматически.
        /// n - знак "-", если число отрицательное. Если число положительное, то знак не ставится. Пробел после знака "-" автоматически НЕ ставится.
        /// Буква "х" во всех форматах - первая буква падежа n,g,d,a,i,p
        /// </param>
        /// <param name="thousandSeparator">Разделитель порядков</param>
        /// <param name="isOrdinal">Если true - порядковое числительное (первый рубль), если false - количественное числительное (один рубль)</param>
        /// <returns>Возвращает строковое представление суммы</returns>
        public string CurrencyToString(double currency, CurrencyType currencyType, string format, string thousandSeparator, bool isOrdinal)
        {
            bool negative = false;
            if (currency < 0)
            {
                currency = Math.Abs(currency);
                negative = true;
            }
            List<string> currency_types = GetCurrenctyPostfix(currencyType, currency, isOrdinal);
            string[] currency_parts = currency.ToString(CultureInfo.CurrentCulture).Split(new char[] { '.', ',' }, 2, StringSplitOptions.RemoveEmptyEntries);
            long integral = long.Parse(currency_parts[0], CultureInfo.CurrentCulture);
            if (currencyType == CurrencyType.Euro)
                this.sex = new NeuterClass();
            else
                this.sex = new MaleClass();
            Match match_integral = Regex.Match(format, "ii[ngdaip]");
            while (match_integral.Success)
            {
                this.textcase = LetterToTextCase(match_integral.Value[2].ToString());
                string integral_part = NumberToText(integral, isOrdinal);
                format = format.Replace(match_integral.Value, integral_part);
                match_integral = match_integral.NextMatch();
            }
            Match match_rur = Regex.Match(format, "r[ngdaip]");
            while (match_rur.Success)
            {
                this.textcase = LetterToTextCase(match_rur.Value[1].ToString());
                format = format.Replace(match_rur.Value, this.textcase.Translate(currency_types[0]));
                match_rur = match_rur.NextMatch();
            }
            long fractional = 0;
            if (currency_parts.Length > 1)
            {
                if (currency_parts[1].Length == 1)
                    fractional = long.Parse(currency_parts[1][0] + "0", CultureInfo.CurrentCulture);
                else
                    fractional = long.Parse(currency_parts[1].Substring(0, 2), CultureInfo.CurrentCulture);
            }
            if (currencyType == CurrencyType.Ruble)
                this.sex = new FemaleClass();
            else
                this.sex = new MaleClass();
            Match match_fractional = Regex.Match(format, "ff[ngdaip]");
            while (match_fractional.Success)
            {
                this.textcase = LetterToTextCase(match_fractional.Value[2].ToString());
                string fractional_part = NumberToText(fractional, isOrdinal);
                format = format.Replace(match_fractional.Value, fractional_part);
                match_fractional = match_fractional.NextMatch();
            }
            Match match_kop = Regex.Match(format, "k[ngdaip]");
            while (match_kop.Success)
            {
                this.textcase = LetterToTextCase(match_kop.Value[1].ToString());
                format = format.Replace(match_kop.Value, this.textcase.Translate(currency_types[1]));
                match_kop = match_kop.NextMatch();
            }
            string integral_num = "";
            while (integral > 0)
            {
                long integral_part = integral % 1000;
                integral = (integral - integral_part) / 1000;
                string integral_part_str = integral_part.ToString(CultureInfo.CurrentCulture);
                if ((integral_part_str.Length == 1) && (integral > 0))
                    integral_part_str = "00" + integral_part_str;
                else
                    if ((integral_part_str.Length == 2) && (integral > 0))
                        integral_part_str = "0" + integral_part_str;
                integral_num = integral_part_str + integral_num;
                if (integral > 0)
                    integral_num = thousandSeparator + integral_num;
            }
            if (String.IsNullOrEmpty(integral_num))
                integral_num = "0";
            format = format.Replace("ii", integral_num);
            string fractional_str = fractional.ToString(CultureInfo.CurrentCulture);
            if (fractional_str.Length == 1)
                fractional_str = "0" + fractional_str;
            format = format.Replace("ff", fractional_str);
            if (negative)
            {
                format = format.Replace("nn", "минус ");
                format = format.Replace("n", "-");
            }
            else
                format = format.Replace("n", "");
            return format;
        }

        /// <summary>
        /// Метод конвертации вещественного числа в строку
        /// </summary>
        /// <param name="number">Вещественное число</param>
        /// <returns>Результат</returns>
        public string FloatToString(float number)
        {
            bool negative = false;
            if (number < 0)
            {
                number = Math.Abs(number);
                negative = true;
            }
            string[] number_parts = number.ToString(CultureInfo.CurrentCulture).Split(new char[] { '.', ',' }, 2, StringSplitOptions.RemoveEmptyEntries);
            long integral = long.Parse(number_parts[0], CultureInfo.CurrentCulture);
            long fractional = 0;
            if (number_parts.Length > 1)
                fractional = long.Parse(number_parts[1], CultureInfo.CurrentCulture);
            string integrel_part = "";
            integrel_part = NumberToText(integral, false);
            string fractional_part = NumberToText(fractional, false);
            string integral_postfix = "";
            string fractional_postfix = "";
            if ((integral % 10 == 1) && (integral != 11))
                integral_postfix = "целая";
            else
                integral_postfix = "целых";
            if ((fractional % 10 == 1) && (fractional != 11))
                fractional_postfix = fractional_postfixs[fractional.ToString(CultureInfo.CurrentCulture).Length - 1];
            else
                fractional_postfix = Regex.Replace(fractional_postfixs[fractional.ToString(CultureInfo.CurrentCulture).Length - 1], @"(ая)$", "ых");
            string full_text = integrel_part;
            if (fractional != 0)
                full_text = full_text + " " + this.textcase.Translate(integral_postfix)
                    + " " + fractional_part + " " + this.textcase.Translate(fractional_postfix);
            if (negative)
                full_text = "минус " + full_text;
            return full_text;
        }
    }
}
