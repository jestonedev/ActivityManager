using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using AMClasses;
using System.IO;

namespace AmEditor
{
    /// <summary>
    /// Класс хранилище для настроек программы
    /// </summary>
    [Serializable()]
    internal class SettingsStorage
    {
        public string InterfaceLanguagePrefix { get; set; }
        public List<OpenFileHistoryItem> OpenFileHistory { get; set; }  //В истории открытых файлов храним последние 20, остальные отбрасываем

        public SettingsStorage()
        {
            //Задаем значения по умолчанию
            InterfaceLanguagePrefix = "ru";
            OpenFileHistory = new List<OpenFileHistoryItem>();
        }

        public static bool SaveSettings(SettingsStorage settingsStorage)
        {
            BinaryFormatter bf = new BinaryFormatter();
            try
            {
                FileStream fs = new FileStream(
                    Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "am_editor.dat"), FileMode.Create);
                try
                {
                    bf.Serialize(fs, settingsStorage);
                }
                finally
                {
                    fs.Close();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static SettingsStorage LoadSettings()
        {
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                if (!File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "am_editor.dat")))
                    return null;
                string file_name = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                FileStream fs = new FileStream(
                    Path.Combine(file_name, "am_editor.dat"), FileMode.Open);
                SettingsStorage ss = null;
                try
                {
                    ss = (SettingsStorage)bf.Deserialize(fs);
                }
                finally
                {
                    fs.Close();
                }
                return ss;
            }
            catch
            {
                return null;
            }
        }
    }

    [Serializable()]
    internal class OpenFileHistoryItem: IComparable<OpenFileHistoryItem>
    {
        public string FileName { get; set; }
        public DateTime OpenDateTime { get; set; }
        public Dictionary<string, string> CommandLineParams { get; set; }

        public OpenFileHistoryItem()
        {
            this.CommandLineParams = new Dictionary<string, string>();
        }

        #region IComparable<OpenFileHistoryItem> Members

        public int CompareTo(OpenFileHistoryItem other)
        {
            if (other == null)
                throw new ArgumentNullException("other","Не задана ссылка на параметр");
            return this.OpenDateTime.CompareTo(other.OpenDateTime); //Сравниваем объекты по дате открытия, сортируем соответственно по ней же
        }

        #endregion
    }
}
