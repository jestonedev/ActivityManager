using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Text.RegularExpressions;
using System.Runtime.Serialization;
using ExtendedTypes;
using System.Globalization;

//Плагин для доступа к данным баз данных
namespace SqlDataSource
{
    /// <summary>
    /// Доступный из-вне интерфейс плагина
    /// </summary>
    public interface IPlug
    {
        /// <summary>
        /// Установить провайдера баз данных
        /// </summary>
        /// <param name="name">Краткое или полное инвариантное имя провайдера базы данных</param>
        void SqlSetProvider(string name);

        /// <summary>
        /// Установка строки соединения
        /// </summary>
        /// <param name="connectionString">Строка соединения</param>
        void SqlSetConnectionString(string connectionString);

        /// <summary>
        /// Открыть перманентное соединение с базой данных. 
        /// Позволяет оптимизировать производительность в случае большого количества запросов
        /// </summary>
        void SqlOpenConnection();

        /// <summary>
        /// Закрыть перманентное соединение с базой данных
        /// </summary>
        void SqlCloseConnection();

        /// <summary>
        /// Выборка данных из базы данных
        /// </summary>
        /// <param name="query">Запрос к базе данных в формате Sql</param>
        /// <param name="table">Результат запроса</param>
        void SqlSelectTable(string query, out ReportTable table);

        /// <summary>
        /// Выборка скалярного значения из базы данных
        /// </summary>
        /// <param name="query">Запрос на выполнение</param>
        /// <param name="result">Возврат скалярного значения</param>
        void SqlSelectScalar(string query, out Object result);

        /// <summary>
        /// Запрос на внесение изменений в базу данных
        /// </summary>
        /// <param name="query">Запрос на выполнение</param>
        /// <param name="rowsAffected">Число измененных строк</param>
        void SqlModifyQuery(string query, out int rowsAffected);

        /// <summary>
        /// Начать выполнение транзакции
        /// </summary>
        void SqlBeginTransaction();

        /// <summary>
        /// Подтверждение транзакции
        /// </summary>
        void SqlCommitTransaction();

        /// <summary>
        /// Откат транзакции
        /// </summary>
        void SqlRollbackTransaction();

    }

    /// <summary>
    /// Класс, реализующий интерфейс IPlug
    /// </summary>
    public class SqlDataSourcePlug: IPlug
    {
        private bool permanent_connection = false;
        private DbConnection connection;
        private DbTransaction transaction = null;
        private DbProviderFactory factory;

        /// <summary>
        /// Конструктор
        /// </summary>
        public SqlDataSourcePlug()
        {
            factory = DbProviderFactories.GetFactory(ParseProviderName("ODBC"));
            connection = factory.CreateConnection();
        }

        /// <summary>
        /// Установка строки соединения
        /// </summary>
        /// <param name="connectionString">Строка соединения</param>
        public void SqlSetConnectionString(string connectionString)
        {
            connection.ConnectionString = connectionString;
        }

        /// <summary>
        /// Открыть перманентное соединение с базой данных. 
        /// Позволяет оптимизировать производительность в случае большого количества запросов
        /// </summary>
        public void SqlOpenConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
                connection.Open();
            permanent_connection = true;
        }

        /// <summary>
        /// Закрыть перманентное соединение с базой данных
        /// </summary>
        public void SqlCloseConnection()
        {
            connection.Close();
            permanent_connection = false;
        }

        /// <summary>
        /// Выборка данных из базы данных
        /// </summary>
        /// <param name="query">Запрос к базе данных в формате Sql</param>
        /// <param name="table">Результат запроса</param>
        public void SqlSelectTable(string query, out ReportTable table)
        {
            DbCommand command = factory.CreateCommand();
            command.CommandText = query;
            command.Connection = connection;
            if (transaction != null)
                command.Transaction = transaction;
            if (connection.State == ConnectionState.Closed)
            {
                if (permanent_connection)
                    throw new SqlException("Соединение прервано по неизвестным причинам");
                else
                    connection.Open();
            }
            DbDataAdapter adapter = factory.CreateDataAdapter();
            adapter.SelectCommand = command;
            using (DataSet ds = new DataSet())
            {
                ds.Locale = CultureInfo.CurrentCulture;
                adapter.Fill(ds);
                if (ds.Tables.Count > 0)
                {
                    //Конвертируем таблицу DataTable в ReportTable
                    ReportTable tmp_table = new ReportTable();
                    for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                        tmp_table.Columns.Add(ds.Tables[0].Columns[i].ColumnName);
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        ReportRow row = new ReportRow(tmp_table);
                        for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                            row.Add(new ReportCell(row, ds.Tables[0].Rows[i][j].ToString()));
                        tmp_table.Add(row);
                    }
                    table = tmp_table;
                }
                else
                    throw new SqlException("Запрос к базе данных не вернул результат");
            }
            if (!permanent_connection)
                connection.Close();
        }

        /// <summary>
        /// Выборка скалярного значения из базы данных
        /// </summary>
        /// <param name="query">Запрос на выполнение</param>
        /// <param name="result">Возврат скалярного значения</param>
        public void SqlSelectScalar(string query, out object result)
        {
            DbCommand command = factory.CreateCommand();
            command.CommandText = query;
            command.Connection = connection;
            if (transaction != null)
                command.Transaction = transaction;
            if (connection.State == ConnectionState.Closed)
            {
                if (permanent_connection)
                    throw new SqlException("Соединение с базой данных прервано по неизвестным причинам");
                else
                    connection.Open();
            }
            result = command.ExecuteScalar();
            if (!permanent_connection)
                connection.Close();
        }

        /// <summary>
        /// Запрос на внесение изменений в базу данных
        /// </summary>
        /// <param name="query">Запрос на выполнение</param>
        /// <param name="rowsAffected">Число измененных строк</param>
        public void SqlModifyQuery(string query, out int rowsAffected)
        {
            DbCommand command = factory.CreateCommand();
            command.CommandText = query;
            command.Connection = connection;
            if (transaction != null)
                command.Transaction = transaction;
            if (connection.State == ConnectionState.Closed)
            {
                if (permanent_connection)
                    throw new SqlException("Соединение прервано по неизвестным причинам");
                else
                    connection.Open();
            }
            try
            {
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (InvalidOperationException e)
            {
                SqlRollbackTransaction();
                throw new InvalidOperationException(e.Message);
            }
            if (!permanent_connection)
                connection.Close();
        }

        /// <summary>
        /// Начать выполнение транзакции
        /// </summary>
        public void SqlBeginTransaction()
        {
            transaction = connection.BeginTransaction();
        }

        /// <summary>
        /// Подтверждение транзакции
        /// </summary>
        public void SqlCommitTransaction()
        {
            transaction.Commit();
            transaction.Dispose();
            transaction = null;
        }

        /// <summary>
        /// Откат транзакции
        /// </summary>
        public void SqlRollbackTransaction()
        {
            transaction.Rollback();
            transaction.Dispose();
            transaction = null;
        }

        /// <summary>
        /// Установить провайдера баз данных
        /// </summary>
        /// <param name="name">Краткое или полное инвариантное имя провайдера базы данных</param>
        public void SqlSetProvider(string name)
        {
            if ((connection != null) && (connection.State != ConnectionState.Closed))
                throw new SqlException("Нельзя переназначить провайдера на открытом соединении");
            string connection_string = "";
            if (connection != null)
                connection_string = connection.ConnectionString;
            factory = DbProviderFactories.GetFactory(ParseProviderName(name));
            connection = factory.CreateConnection();
            if (!String.IsNullOrEmpty(connection_string.Trim()))
                connection.ConnectionString = connection_string;
        }

        /// <summary>
        /// Поиск инвариантного имени провайдера по имени, переданному пользователем
        /// </summary>
        /// <param name="name">Имя провайдера, переданное пользователем</param>
        /// <returns>Полное инвариантное имя провайдера</returns>
        private static string ParseProviderName(string name)
        {
            DataTable dt = DbProviderFactories.GetFactoryClasses();
            List<string> providers = new List<string>();
            foreach (DataRow row in dt.Rows)
            {
                providers.Add(row["InvariantName"].ToString());
            }
            foreach (string provider in providers)
            {
                if (Regex.IsMatch(provider, name, RegexOptions.IgnoreCase))
                    return provider;
            }
            SqlException exception = new SqlException("Провайдер {0} не найден");
            exception.Data.Add("{0}", name);
            throw exception;
        }
    }

    /// <summary>
    /// Класс исключения модуля SqlModule
    /// </summary>
    [Serializable()]
    public class SqlException : Exception
    {
        /// <summary>
        /// Конструктор класса исключения SqlException
        /// </summary>
        public SqlException() : base() { }

        /// <summary>
        /// Конструктор класса исключения SqlException
        /// </summary>
        /// <param name="message">Сообщение об ошибке</param>
        public SqlException(string message) : base(message) { }

        /// <summary>
        /// Конструктор класса исключения SqlException
        /// </summary>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="innerException">Вложенное исключение</param>
        public SqlException(string message, Exception innerException) : base(message, innerException) { }

        /// <summary>
        /// Конструктор класса исключения SqlException
        /// </summary>
        /// <param name="info">Информация сериализации</param>
        /// <param name="context">Контекст потока</param>
        protected SqlException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
