using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Globalization;

namespace AMClasses
{
    //Класс-переводчик
    public class Language
    {
        private Dictionary<string, string> dictionary = new Dictionary<string, string>();
        private string prefix;
        public string Prefix { get { return prefix; } }

        public Language(string lang)
        {
            prefix = lang;
            string lang_dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "lang");
            string[] files = Directory.GetFiles(lang_dir, "*." + lang);
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            foreach (string file in files)
            {
                using (StreamReader sr = new StreamReader(file))
                {
                    while (!sr.EndOfStream)
                    {
                        string text = sr.ReadLine();
                        string[] text_parts = text.Split(new char[] { ':' });
                        if (text_parts.Length != 2)
                            throw new AMException(
                                String.Format(CultureInfo.CurrentCulture, this.Translate("Некорректный формат файла \"{0}\" языковой поддержки"), file));
                        if (!dictionary.ContainsKey(text_parts[0]))
                            dictionary.Add(text_parts[0],
                                String.IsNullOrEmpty(text_parts[1].Trim()) ? text_parts[0] : text_parts[1]);
                    }
                }
            }
            this.dictionary = dictionary;
        }

        public string Translate(string text)
        {
            return (dictionary.ContainsKey(text)) ? dictionary[text] : text;
        }
    }
}
