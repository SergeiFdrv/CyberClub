using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberClub.Data
{
    public class LoginDatabase : Database
    {
        public Dictionary<string, object> GetAccount(string login, string password)
        {
            const string query = "SELECT userid, userlevel " +
                "FROM users WHERE username = @name AND passwd = @pwd";
            using (SqlConnection conn = new SqlConnection(CS))
            {
                if (!ConnOpen(conn)) return null;
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@name", login);
                    command.Parameters.AddWithValue("@pwd", password);
                    SqlDataReader dataReader = command.ExecuteReader();
                    if (dataReader.Read())
                    {
                        return ToDictionary(dataReader);
                    }
                    else return null;
                }
            }
        }
    }
}
