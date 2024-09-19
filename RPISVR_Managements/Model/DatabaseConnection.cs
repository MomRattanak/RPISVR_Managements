using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using System.Diagnostics;

namespace RPISVR_Managements.Model
{
    public class DatabaseConnection
    {  
        private string _connectionString;

        public DatabaseConnection()
        {
            _connectionString = "Server=127.0.0.1;Port=3306;Database=rpisvr_system;User ID=root;Password=;";

        }

        // Method to open a connection to the database
        public MySqlConnection OpenConnection()
        {
            MySqlConnection connection = new MySqlConnection(_connectionString);

            try
            {
                connection.Open();
                Debug.WriteLine("Connection Opened and Connected.");
            }
            catch(MySqlException ex)
            {
                Debug.WriteLine("Connection Error: " + ex.Message);
            }
            return connection;
        }
        // Method to close the connection
        public void CloseConnection(MySqlConnection connection)
        {
            if(connection.State ==ConnectionState.Open)
            {
                connection.Close();
                Debug.WriteLine("Connection Closed.");
            }
        }

    }
}
