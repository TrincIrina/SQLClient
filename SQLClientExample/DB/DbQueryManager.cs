using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLClientExample.DB
{
    // DbQueryManager - класс, выполняющий операции с БД (запросы)
    internal class DbQueryManager
    {
        private readonly SqlConnection _connection;
        public SqlDataAdapter adapter { get; set; }

        public DbQueryManager(string text, SqlConnection connection)
        {
            _connection = connection;
            adapter = new SqlDataAdapter(text, connection);
        }
        public DataTable Query()
        {
            //создаем DataTable, который будет выводить данные в виде таблицы
            DataTable dt = new DataTable();
            //передаем в адаптер DataTable, что бы в него записать результат запроса            
            adapter.Fill(dt);
            return dt;
        }

        /// <summary>
        /// тут возвращаемм количество измененных или добавленных в результате запроса записей
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public string Query(string query)
        {
            SqlCommand command = new SqlCommand(query, _connection);
            int i = command.ExecuteNonQuery();
            return $"Ваш запрос выполнил {i} команд";
        }
    }
}
