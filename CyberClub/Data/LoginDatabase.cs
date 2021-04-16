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
        public Dictionary<string, object> GetAccount(string login)
        {
            const string query = "SELECT userid, userlevel " +
                "FROM users WHERE username = @name";
            using (SqlConnection conn = new SqlConnection(CS))
            {
                if (!ConnOpen(conn)) return null;
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@name", login);
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
