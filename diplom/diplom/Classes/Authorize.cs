using System;
using System.Data.SqlClient;

namespace diplom.Classes
{
    public class Authorize
    {
        public static bool CheckUser(string login, string password)
        {
            Helper.OpenConnection();

            string query = @"SELECT Id, Login, RoleId, FullName 
                             FROM Users 
                             WHERE Login=@login AND PasswordHash=@password";

            SqlCommand cmd = new SqlCommand(query, Helper.Connection);

            cmd.Parameters.AddWithValue("@login", login);
            cmd.Parameters.AddWithValue("@password", password);

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                CurrentUser.Id = Convert.ToInt32(reader["Id"]);
                CurrentUser.Login = reader["Login"].ToString();
                CurrentUser.RoleId = Convert.ToInt32(reader["RoleId"]);
                CurrentUser.FullName = reader["FullName"].ToString();

                reader.Close();
                Helper.CloseConnection();

                return true;
            }

            reader.Close();
            Helper.CloseConnection();

            return false;
        }
    }
}