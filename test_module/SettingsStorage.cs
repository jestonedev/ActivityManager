using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using am_classes;
using System.IO;

namespace am_editor
{
    /// <summary>
    /// Класс хранилище для настроек программы
    /// </summary>
    [Serializable()]
    public class SettingsStorage
    {
        public string interface_language_prefix { get; set; }
        public List<OpenFileHistoryItem> OpenFileHistory { get; set; }  //В истории открытых файлов храним последние 20, остальные отбрасываем

        public SettingsStorage()
        {
            //Задаем значения по умолчанию
            interface_language_prefix = "ru";
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
                FileStream fs = new FileStream(
                    Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "am_editor.dat"), FileMode.Open);
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
    public class OpenFileHistoryItem: IComparable<OpenFileHistoryItem>
    {
        public string FileName { get; set; }
        public DateTime OpenDateTime { get; set; }
        public Dictionary<string, string> command_line_params = new Dictionary<string, string>();

        #region IComparable<OpenFileHistoryItem> Members

        public int CompareTo(OpenFileHistoryItem other)
        {
            return this.OpenDateTime.CompareTo(other.OpenDateTime); //Сравниваем объекты по дате открытия, сортируем соответственно по ней же
        }

        #endregion
    }
}
