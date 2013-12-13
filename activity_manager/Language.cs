using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace activity_manager
{
	//Класс-переводчик
	public class Language
	{
        private Dictionary<string, string> dictionary = new Dictionary<string, string>();

        public Language(string lang)
        {
            string lang_dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "lang");
            string[] files = Directory.GetFiles(lang_dir, "*." + lang);
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            foreach (string file in files)
            {
                StreamReader sr = new StreamReader(file);
                while (!sr.EndOfStream)
                {
                    string text = sr.ReadLine();
                    string[] text_parts = text.Split(new char[] {':'});
                    if (text_parts.Length != 2)
                        throw new ApplicationException(String.Format(this.Translate("Некорректный формат файла \"{0}\" языковой поддержки"),file));
                    dictionary.Add(text_parts[0], (text_parts[1].Trim() == "") ? text_parts[0] : text_parts[1]);
                }
                sr.Close();
            }
            this.dictionary = dictionary;
        }

        public string Translate(string text)
        {
            return (dictionary.ContainsKey(text)) ? dictionary[text] : text;
        }
	}
}
