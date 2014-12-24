using ExtendedTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Runtime.Serialization;

namespace JsonDataSource
{
    /// <summary>
    /// Доступный из-вне интерфейс плагина
    /// </summary>
    public interface IPlug
    {
        /// <summary>
        /// Выборка данных из json-файла
        /// </summary>
        /// <param name="json">Путь до json-файла или текст в формате json</param>
        /// <param name="cellsJson">json-объект, указывающие какие свойства json-документа считать ячейками, 
        /// где ключ - имя будущего столбца, значение - свойство или цепочка вложенных свойств, разделенных знаком "точка" 
        /// Пример: {"name":"stuff.name","surname":"stuff.surname","department":"organization.name"}
        /// </param>
        /// <param name="table">Результат запроса</param>
        void JsonSelectTable(string json, string cellsJson, out ReportTable table);

        /// <summary>
        /// Выборка скалярного значения из json-файла
        /// </summary>
        /// <param name="json">Путь до json-файла или текст в формате json</param>
        /// <param name="key">Ключ для выборки свойства из json-документа. Пример для json-документа {"stuff": {"name":"Ivan","surname":"Ivanov"} } : stuff.name</param>
        /// <param name="result">Возврат скалярного значения</param>
        void JsonSelectScalar(string json, string key, out Object result);
    }

    public class JsonDataSourcePlug: IPlug
    {
        /// <summary>
        /// Выборка данных из json-файла
        /// </summary>
        /// <param name="json">Путь до json-файла или текст в формате json</param>
        /// <param name="cellsJson">json-объект, указывающие какие свойства json-документа считать ячейками, 
        /// где ключ - имя будущего столбца, значение - свойство или цепочка вложенных свойств, разделенных знаком "точка" 
        /// Пример: {"name":"stuff.name","surname":"stuff.surname","department":"organization.name"}
        /// </param>
        /// <param name="table">Результат запроса</param>
        public void JsonSelectTable(string json, string cellsJson, out ReportTable table)
        {
            List<JToken> jsonObjs = null;
            Dictionary<string, string> cellJsonDic = null;
            try
            {
                if (File.Exists(json))
                {
                    StreamReader reader = new StreamReader(json);
                    jsonObjs = JsonConvert.DeserializeObject<List<JToken>>("[" + reader.ReadToEnd().Trim(new char[] { '[', ']' }) + "]");
                }
                else
                    jsonObjs = JsonConvert.DeserializeObject<List<JToken>>("[" + json.Trim(new char[] { '[', ']' }) + "]");
                cellJsonDic = JsonConvert.DeserializeObject<Dictionary<string, string>>(cellsJson);
            } 
            catch (IOException)
            {
                JsonDataSourceException exception = new JsonDataSourceException("Ошибка ввода-вывода при доступе к файлу {0}");
                exception.Data.Add("{0}", json);
                throw exception;
            }
            catch (JsonException e)
            {
                JsonDataSourceException exception = new JsonDataSourceException("Ошибка преобразования json-объекта. Подробнее: {0}");
                exception.Data.Add("{0}", e.Message);
                throw exception;
            }
            table = new ReportTable();
            foreach (var cellJson in cellJsonDic)
                table.Columns.Add(cellJson.Key);
            foreach (JToken jsonObj in jsonObjs)
            {
                ReportRow row = new ReportRow(table);
                foreach (var cell in cellJsonDic)
                {
                    string[] properties = cell.Value.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                    if (properties.Count() == 0)
                    {
                        JsonDataSourceException exception = new JsonDataSourceException("Некорректно задано значение \"{0}\" свойства \"{1}\"");
                        exception.Data.Add("{0}", cell.Value);
                        exception.Data.Add("{1}", cellsJson);
                        throw exception;
                    }
                    JToken currObj = jsonObj;
                    foreach (string property in properties)
                    {
                        if (currObj == null)
                            break;
                        currObj = (JToken)currObj.Value<JToken>(property);
                    }
                    row.Add(new ReportCell(row, currObj == null ? "" : currObj.ToString()));
                }
                table.Add(row);
            }
        }

        /// <summary>
        /// Выборка скалярного значения из json-файла
        /// </summary>
        /// <param name="json">Путь до json-файла или текст в формате json</param>
        /// <param name="key">Ключ для выборки свойства из json-документа. Пример для json-документа {"stuff": {"name":"Ivan","surname":"Ivanov"} } : stuff.name</param>
        /// <param name="result">Возврат скалярного значения</param>
        public void JsonSelectScalar(string json, string key, out object result)
        {
            List<JToken> jsonObjs = null;
            try
            {
                if (File.Exists(json))
                {
                    StreamReader reader = new StreamReader(json);
                    jsonObjs = JsonConvert.DeserializeObject<List<JToken>>("[" + reader.ReadToEnd().Trim(new char[] { '[', ']' }) + "]");
                }
                else
                    jsonObjs = JsonConvert.DeserializeObject<List<JToken>>("[" + json.Trim(new char[] { '[', ']' }) + "]");
            }
            catch (IOException)
            {
                JsonDataSourceException exception = new JsonDataSourceException("Ошибка ввода-вывода при доступе к файлу {0}");
                exception.Data.Add("{0}", json);
                throw exception;
            }
            catch (JsonException e)
            {
                JsonDataSourceException exception = new JsonDataSourceException("Ошибка преобразования json-объекта. Подробнее: {0}");
                exception.Data.Add("{0}", e.Message);
                throw exception;
            }
            string[] properties = key.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            if (properties.Count() == 0)
            {
                JsonDataSourceException exception = new JsonDataSourceException("Некорректно задано значение \"{0}\" свойства \"key\"");
                exception.Data.Add("{0}", key);
                throw exception;
            }
            result = "";
            foreach (JToken jsonObj in jsonObjs)
            {
                JToken currObj = jsonObj;
                foreach (string property in properties)
                {
                    if (currObj == null)
                        break;
                    currObj = (JToken)currObj.Value<JToken>(property);
                }
                if (currObj == null)
                    continue;
                result += currObj.ToString();
            }
        }
    }

    /// <summary>
    /// Класс исключения модуля JsonDataSource
    /// </summary>
    [Serializable()]
    public class JsonDataSourceException : Exception
    {
        /// <summary>
        /// Конструктор класса исключения JsonDataSourceException
        /// </summary>
        public JsonDataSourceException() : base() { }

        /// <summary>
        /// Конструктор класса исключения JsonDataSourceException
        /// </summary>
        /// <param name="message">Сообщение об ошибке</param>
        public JsonDataSourceException(string message) : base(message) { }

        /// <summary>
        /// Конструктор класса исключения JsonDataSourceException
        /// </summary>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="innerException">Вложенное исключение</param>
        public JsonDataSourceException(string message, Exception innerException) : base(message, innerException) { }

        /// <summary>
        /// Конструктор класса исключения JsonDataSourceException
        /// </summary>
        /// <param name="info">Информация сериализации</param>
        /// <param name="context">Контекст потока</param>
        protected JsonDataSourceException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
