namespace com.jwsoft.common.data
{
    using System;
    using System.Configuration;
    using System.Data.SqlClient;

    public class Conn
    {
        public static SqlConnection aptSqlConn()
        {
            SqlConnection connection = new SqlConnection();
            string str = ConfigurationSettings.AppSettings["Sql"];
            connection.ConnectionString = str;
            return connection;
        }

        public SqlConnection SqlConnectionSystem()
        {
            SqlConnection connection = new SqlConnection();
            string connectionString = ConfigurationManager.ConnectionStrings["Sql"].ConnectionString;
            connection.ConnectionString = connectionString;
            connection.Open();
            return connection;
        }
    }
}

