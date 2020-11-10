using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberClub
{
    public static class AppWide
    {
        public static string CS => Properties.Settings.Default.CyberClubConnectionString;

        /// <summary>
        /// Update list control items
        /// </summary>
        /// <param name="items">List control Items property</param>
        /// <param name="select">Column name</param>
        /// <param name="from">Table name</param>
        /// <returns>
        /// Returns <c>false</c> if <paramref name="items"/>, <paramref name="select"/>
        /// or <paramref name="from"/> is empty or the database connection fails.
        /// Otherwise returns <c>true</c>
        /// </returns>
        public static bool UpdateBox(IList items,
            string select, string from, string order = "")
        {
            if (string.IsNullOrEmpty(select) || string.IsNullOrEmpty(from) || items == null)
                return false;
            if (string.IsNullOrEmpty(order)) order = select;
            using (SqlConnection conn = new SqlConnection(CS))
            {
                if (!ConnOpen(conn)) return false;
                if (select.Contains(',')) select = select.Substring(0, select.IndexOf(','));
                if (select.Contains(' ')) select = select.Substring(0, select.IndexOf(' '));
                if (from.Contains(',')) from = from.Substring(0, from.IndexOf(','));
                if (from.Contains(' ')) from = from.Substring(0, from.IndexOf(' '));
                if (order.Contains(',')) order = order.Substring(0, order.IndexOf(','));
                if (order.Contains(' ')) order = order.Substring(0, order.IndexOf(' '));
                using (SqlCommand command = new
                    SqlCommand($"SELECT {select} FROM {from} ORDER BY {order}", conn))
                {
                    SqlDataReader dataReader = command.ExecuteReader();
                    items.Clear();
                    while (dataReader.Read())
                    {
                        items.Add(dataReader[select]);
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Tries to open the given SQLConnection. Catches SqlException.
        /// </summary>
        /// <returns>Returns <c>true</c> if it manages to open</returns>
        public static bool ConnOpen(SqlConnection conn)
        {
            try
            {
                if (conn == null)
                {
                    Voice.Say(Resources.Lang.Error);
                    return false;
                }
                conn.Open();
                return true;
            }
            catch (SqlException)
            {
                Voice.Say(Resources.Lang.DBError);
                return false;
            }
        }
    }
}
