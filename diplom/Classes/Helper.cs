using System;
using System.Data.SqlClient;

namespace diplom.Classes
{
    public class Helper
    {
        public static string ConnectionString =
            @"Data Source=WIN-IIVPL9JM0AG\SQLEXPRESS;
              Initial Catalog=Diplom;
              Integrated Security=True";

        public static SqlConnection Connection;

        public static void OpenConnection()
        {
            if (Connection == null)
                Connection = new SqlConnection(ConnectionString);

            if (Connection.State != System.Data.ConnectionState.Open)
                Connection.Open();
        }

        public static void CloseConnection()
        {
            if (Connection != null &&
                Connection.State == System.Data.ConnectionState.Open)
                Connection.Close();
        }
    }
}