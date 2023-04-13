using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;

namespace chat31
{
    static class DataBaseConnection
    {
        static MySqlConnection connection = null;
        public static MySqlConnection GetConnection()
        {
            if (connection == null)
            {
                connection = new MySqlConnection("Server=localhost;User ID=pk31;Password=123456;Database=pk31chat");
                connection.Open();
            } else
            {
                connection.Close();
                connection = new MySqlConnection("Server=localhost;User ID=pk31;Password=123456;Database=pk31chat");
                connection.Open();
            }
            //надо сделать проверку подключения
            return connection;
        }

        public static void CloseConnection()
        {
            connection.Close();
        }
    }
}
