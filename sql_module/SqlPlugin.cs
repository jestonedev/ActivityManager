using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Text.RegularExpressions;

//Плагин для доступа к данным баз данных
namespace sql_module
{
    //Доступный из-вне интерфейс плагина
    public interface IPlugin
    {
        void sql_set_provider(string name);
        void sql_set_connection_string(string connection_string);
        void sql_open_connection();
        void sql_close_connection();
        void sql_select_table(string query, out DataTable table);
        void sql_select_scalar(string query, out object result);
        void sql_modify_query(string query, out int rows_affected);
        void sql_begin_transaction();
        void sql_commit_transaction();
        void sql_rollback_transaction();
        void sql_get_row(DataTable table, int row_number, out DataRow row);
    }

    public class SqlPlugin: IPlugin
    {
        private bool permanent_connection = false;
        private DbConnection connection;
        private DbTransaction transaction = null;
        private DbProviderFactory factory;

        public SqlPlugin()
        {
            factory = DbProviderFactories.GetFactory(parse_provider_name("ODBC"));
            connection = factory.CreateConnection();
        }

        /// <summary>
        /// Установка строки соединения
        /// </summary>
        /// <param name="connection_string">Строка соединения</param>
        public void sql_set_connection_string(string connection_string)
        {
            connection.ConnectionString = connection_string;
        }

        /// <summary>
        /// Открыть перманентное соединение с базой данных. 
        /// Позволяет оптимизировать производительность в случае большого количества запросов
        /// </summary>
        public void sql_open_connection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
                connection.Open();
            permanent_connection = true;
        }

        /// <summary>
        /// Закрыть перманентное соединение с базой данных
        /// </summary>
        public void sql_close_connection()
        {
            connection.Close();
            permanent_connection = false;
        }

        /// <summary>
        /// Выборка данных из базы данных
        /// </summary>
        /// <param name="query">Запрос к базе данных в формате SQL</param>
        /// <param name="result">Результат запроса</param>
        public void sql_select_table(string query, out DataTable table)
        {
            DbCommand command = factory.CreateCommand();
            command.CommandText = query;
            command.Connection = connection;
            if (transaction != null)
                command.Transaction = transaction;
            if (connection.State == ConnectionState.Closed)
            {
                if (permanent_connection)
                    throw new ApplicationException("Соединение прервано по неизвестным причинам");
                else
                    connection.Open();
            }
            DbDataAdapter adapter = factory.CreateDataAdapter();
            adapter.SelectCommand = command;
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            if (ds.Tables.Count > 0)
                table = ds.Tables[0];
            else
                throw new ApplicationException("Запрос к базе данных не вернул результат");
            if (!permanent_connection)
                connection.Close();
        }

        /// <summary>
        /// Выборка скалярного значения из базы данных
        /// </summary>
        /// <param name="query">Запрос на выполнение</param>
        /// <param name="result">Возврат скалярного значения</param>
        public void sql_select_scalar(string query, out object result)
        {
            DbCommand command = factory.CreateCommand();
            command.CommandText = query;
            command.Connection = connection;
            if (transaction != null)
                command.Transaction = transaction;
            if (connection.State == ConnectionState.Closed)
            {
                if (permanent_connection)
                    throw new ApplicationException("Соединение с базой данных прервано по неизвестным причинам");
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
        /// <param name="rows_affected">Число измененных строк</param>
        public void sql_modify_query(string query, out int rows_affected)
        {
            DbCommand command = factory.CreateCommand();
            command.CommandText = query;
            command.Connection = connection;
            if (transaction != null)
                command.Transaction = transaction;
            if (connection.State == ConnectionState.Closed)
            {
                if (permanent_connection)
                    throw new ApplicationException("Соединение прервано по неизвестным причинам");
                else
                    connection.Open();
            }
            try
            {
                rows_affected = command.ExecuteNonQuery();
            }
            catch (InvalidOperationException e)
            {
                sql_rollback_transaction();
                throw new InvalidOperationException(e.Message);
            }
            if (!permanent_connection)
                connection.Close();
        }

        /// <summary>
        /// Начать выполнение транзакции
        /// </summary>
        public void sql_begin_transaction()
        {
            transaction = connection.BeginTransaction();
        }

        /// <summary>
        /// Подтверждение транзакции
        /// </summary>
        public void sql_commit_transaction()
        {
            transaction.Commit();
            transaction.Dispose();
            transaction = null;
        }

        /// <summary>
        /// Откат транзакции
        /// </summary>
        public void sql_rollback_transaction()
        {
            transaction.Rollback();
            transaction.Dispose();
            transaction = null;
        }

        /// <summary>
        /// Получить строку таблицы по указанному номеру
        /// </summary>
        /// <param name="table"></param>
        /// <param name="row_number"></param>
        /// <param name="row"></param>
        public void sql_get_row(DataTable table, int row_number, out DataRow row)
        {
            if (table.Rows.Count <= row_number)
            {
                ApplicationException exception = new ApplicationException("В таблице {0} отсутствует запрашиваемая строка № {1}");
                exception.Data.Add("{0}",table.TableName);
                exception.Data.Add("{1}",row_number.ToString());
            }
            row = table.Rows[row_number];
        }

        /// <summary>
        /// Установить провайдера баз данных
        /// </summary>
        /// <param name="name">Краткое или полное инвариантное имя провайдера базы данных</param>
        public void sql_set_provider(string name)
        {
            if ((connection != null) && (connection.State != ConnectionState.Closed))
                throw new ApplicationException("Нельзя переназначить провайдера на открытом соединении");
            string connection_string = "";
            if (connection != null)
                connection_string = connection.ConnectionString;
            factory = DbProviderFactories.GetFactory(parse_provider_name(name));
            connection = factory.CreateConnection();
            if (connection_string.Trim() != "")
                connection.ConnectionString = connection_string;
        }

        /// <summary>
        /// Поиск инвариантного имени провайдера по имени, переданному пользователем
        /// </summary>
        /// <param name="name">Имя провайдера, переданное пользователем</param>
        /// <returns>Полное инвариантное имя провайдера</returns>
        private string parse_provider_name(string name)
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
            ApplicationException exception = new ApplicationException("Провайдер {0} не найден");
            exception.Data.Add("{0}", name);
            throw exception;
        }
    }
}
