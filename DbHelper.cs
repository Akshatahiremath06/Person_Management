using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Data.SqlClient;

namespace PersonManag.DAL
{
    public class DbHelper
    {
        public static MySqlConnection GetConnection()
        {
            string connStr =
                ConfigurationManager.ConnectionStrings["MySqlConn"].ConnectionString;

            return new MySqlConnection(connStr);
        }
    }
}
