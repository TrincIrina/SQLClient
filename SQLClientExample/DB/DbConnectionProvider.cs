using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLClientExample.DB
{
    // DbConnectionProvider - класс, обеспечивающий создание подключения к БД
    // согласно переданым параметрам
    internal static class DbConnectionProvider
    {
        // OpenConnection - открыть соединение к БД по переданным параметрам
        public static SqlConnection OpenConnection(string serverName, string dbName, int timeout=5)
        {
            string connectionString = $"Data Source={serverName}; Initial Catalog={dbName};" +
                $"Integrated Security=SSPI; Timeout={timeout}";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }
    }
}
