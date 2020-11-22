using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CyberClub
{
    public static class AppWide
    {
        #region Common
        public static string CS => Properties.Settings.Default.CyberClubConnectionString;

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

        private static Dictionary<string, object> ToDictionary(this SqlDataReader dr)
        {
            return Enumerable.Range(0, dr.FieldCount).ToDictionary(
                i => dr.GetName(i),
                i => dr.GetValue(i));
        }

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
        #endregion

        #region Login
        public static Dictionary<string, object> GetAccount(string login, string password)
        {
            string query = "SELECT userid, authority, authname " +
                "FROM users INNER JOIN hierarchy ON users.authority = " +
                "hierarchy.authid WHERE username = @name AND passwd = @pwd";
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
                        return dataReader.ToDictionary();
                    }
                    else return null;
                }
            }
        }
        #endregion

        #region User
        #region - subscriptions
        public static void Subscribe(int user, int game)
        {
            using (SqlConnection conn = new SqlConnection(CS))
            {
                if (!ConnOpen(conn)) return;
                SqlCommand command = conn.CreateCommand();
                command.Parameters.AddWithValue("@me", user);
                command.Parameters.AddWithValue("@id", game);
                command.CommandText =
                    "INSERT INTO subscriptions (who, game) VALUES (@me, @id)";
                command.ExecuteNonQuery();
            }
        }

        public static void Unsubscribe(int user, int game)
        {
            using (SqlConnection conn = new SqlConnection(CS))
            {
                if (!ConnOpen(conn)) return;
                SqlCommand command = conn.CreateCommand();
                command.Parameters.AddWithValue("@me", user);
                command.Parameters.AddWithValue("@id", game);
                command.CommandText =
                    "DELETE FROM subscriptions WHERE who = @me AND game = @id";
                command.ExecuteNonQuery();
            }
        }

        public static void ChangeRate(int game, int user, decimal? rate = null)
        {
            using (SqlConnection conn = new SqlConnection(CS))
            {
                if (!ConnOpen(conn)) return;
                SqlCommand command = conn.CreateCommand();
                command.CommandText = "UPDATE subscriptions SET rate = @rate " +
                    "WHERE who = @me AND game = @game";
                command.Parameters.AddWithValue("@game", game);
                command.Parameters.AddWithValue("@me", user);
                if (rate.HasValue)
                {
                    command.Parameters.AddWithValue("@rate", rate.Value);
                }
                else command.Parameters.AddWithValue("@rate", DBNull.Value);
                command.ExecuteNonQuery();
            }
        }
        #endregion
        #region - game search
        /// <summary>
        /// Обновить элемент ListView с играми. 
        /// Встроен поиск по жанрам, разработчикам, режимам игры и по подпискам
        /// </summary>
        public static void PopulateGameList(
            ListView games, string name, string dev, IList genres, ImageList pics,
            bool onlyMyGames, bool singleplayer, bool multiplayer, bool allGenres)
        {
            string query = CreateGameSearchQuery(name, dev, genres,
                onlyMyGames, singleplayer, multiplayer, allGenres);
            ProcessGameSearchQuery(games, name, dev, pics, query);
        }

        private static string CreateGameSearchQuery(
            string name, string dev, IList genres,
            bool onlyMyGames, bool singleplayer, bool multiplayer, bool allGenres)
        {
            bool nameEntered = !string.IsNullOrWhiteSpace(name);
            bool devSelected = !string.IsNullOrWhiteSpace(dev);
            bool genresSelected = genres?.Count > 0;

            string query = "SELECT DISTINCT gameid, gamename, devname, " +
                "singleplayer, multiplayer, picname, bin FROM games " +
                "LEFT JOIN pics ON gamepic = picid";
            string where = " WHERE gamelink != ''";
            if (devSelected)
            {
                query += " INNER";
                where += " AND devname LIKE @dev";
            }
            else query += " LEFT";
            query += " JOIN devs ON madeby = devid";
            if (genresSelected) query += " INNER JOIN (gamegenre LEFT JOIN genres" +
                    " ON genre = genreid) ON gameid = game";
            if (!onlyMyGames)
            {
                query +=
                    " INNER JOIN subscriptions ON games.gameid = subscriptions.game";
                where += " AND who = @id";
            }
            if (nameEntered)
                where += " AND gamename LIKE @name";
            if (singleplayer)
                where += " AND singleplayer = 1";
            if (multiplayer)
                where += " AND multiplayer = 1";
            query += where;
            if (genresSelected)
            {
                query += " AND (";
                string op = allGenres ? "AND" : "OR";
                for (int i = 0; i < genres.Count; i++)
                {
                    query += $" genrename = '{genres[i]}' " + op;
                }
                return query.Substring(0, query.Length - 3) + ')';
            }
            return query;
        }

        /// <summary>
        /// Обновить элемент ListView с играми. 
        /// Встроен поиск по жанрам, разработчикам, режимам игры и по подпискам
        /// </summary>
        private static void ProcessGameSearchQuery(
            this ListView games, string name, string dev, ImageList pics, string query)
        { // Если отмечены жанры или разработчик, включить их в критерии поиска
            using (SqlConnection conn = new SqlConnection(CS))
            {
                if (!ConnOpen(conn) || games is null) return;
                string item;
                SqlCommand command = conn.CreateCommand();
                command.CommandText = query;
                command.Parameters.AddWithValue("@name", '%' + name + '%');
                command.Parameters.AddWithValue("@id", LoginForm.UserID);
                command.Parameters.AddWithValue("@dev", '%' + dev + '%');
                SqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    item = dataReader["gamename"].ToString() + " (";
                    if (dataReader["devname"].ToString().Length > 0)
                    {
                        item += dataReader["devname"];
                        if ((bool)dataReader["singleplayer"]) item += ", singleplayer";
                        if ((bool)dataReader["multiplayer"]) item += ", multiplayer";
                    }
                    games.Items.Add(new ListViewItem
                    {
                        Text = item + ')',
                        ToolTipText = dataReader["gameid"].ToString()
                    });
                    if (pics is null) return;
                    if (dataReader["bin"] == DBNull.Value)
                    {
                        games.Items[games.Items.Count - 1].ImageIndex = 0;
                    }
                    else using (MemoryStream memoryStream =
                            new MemoryStream((byte[])dataReader["bin"]))
                        {
                            pics.Images.Add(dataReader["picname"].ToString(),
                                Image.FromStream(memoryStream));
                            games.Items[games.Items.Count - 1].ImageIndex =
                                pics.Images.Count - 1;
                        }
                }
            }
        }
        #endregion
        #region - selected game
        public static Dictionary<string, object> SelectGame(int id)
        {
            using (SqlConnection conn = new SqlConnection(CS))
            {
                if (!ConnOpen(conn)) return null;
                SqlCommand command = conn.CreateCommand();
                command.CommandText = "SELECT gamename, devname, singleplayer, " +
                    "multiplayer, gamelink, bin, CONVERT(varchar, " +
                    "ROUND(AVG(CAST(rate AS float)), 2)) + ' (' + CONVERT(varchar, " +
                    "COUNT(rate)) + ')' AS rating FROM games LEFT JOIN pics ON " +
                    "gamepic = picid LEFT JOIN devs ON madeby = devid LEFT JOIN " +
                    "subscriptions ON gameid = game WHERE gameid = @id GROUP BY " +
                    "gamename, devname, singleplayer, multiplayer, gamelink, bin";
                command.Parameters.AddWithValue("@id", id);
                SqlDataReader dataReader = command.ExecuteReader();
                dataReader.Read();
                return dataReader.ToDictionary();
            }
        }

        public static void PopulateGenres(int id, Label label)
        {
            using (SqlConnection conn = new SqlConnection(CS))
            {
                if (!ConnOpen(conn) || label is null) return;
                SqlCommand command = conn.CreateCommand();
                command.CommandText = "SELECT genrename FROM gamegenre " +
                "INNER JOIN genres ON genre = genreid WHERE game = @id";
                command.Parameters.AddWithValue("@id", id);
                SqlDataReader dataReader = command.ExecuteReader();
                label.Text = "";
                while (dataReader.Read())
                    label.Text += dataReader["genrename"].ToString() + ", ";
                if (label.Text.Length > 0) label.Text =
                    label.Text.Substring(0, label.Text.LastIndexOf(','));
            }
        }

        public static Dictionary<string, object> GetSubscription(int user, int game)
        {
            using (SqlConnection conn = new SqlConnection(CS))
            {
                if (!ConnOpen(conn)) return null;
                SqlCommand command = conn.CreateCommand();
                command.CommandText =
                    "SELECT * FROM subscriptions WHERE who = @me AND game = @id";
                command.Parameters.AddWithValue("@id", game);
                command.Parameters.AddWithValue("@me", user);
                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader.Read())
                {
                    return dataReader.ToDictionary();
                }
                else return null;
            }
        }
        #endregion
        #region - settings
        public static Dictionary<string, object> GetUserData(int id)
        {
            using (SqlConnection conn = new SqlConnection(CS))
            {
                if (!ConnOpen(conn)) return null;
                SqlCommand command = conn.CreateCommand();
                command.CommandText = "SELECT username, email, info, passwd, authname " +
                    "FROM users INNER JOIN hierarchy ON authority = authid " +
                    "WHERE userid = @id";
                command.Parameters.Add(new SqlParameter("@id", id));
                SqlDataReader dataReader = command.ExecuteReader();
                dataReader.Read();
                return dataReader.ToDictionary();
            }
        }

        public static void UpdateAccount(
            int id, string name, string email, string about, string password)
        {
            using (SqlConnection conn = new SqlConnection(CS))
            {
                if (!ConnOpen(conn)) return;
                SqlCommand command = conn.CreateCommand();
                command.CommandText = "UPDATE users SET " +
                    (string.IsNullOrWhiteSpace(name) ?
                    "" : "username = @name, ") +
                    "email = @email, info = @info, passwd = @pwd WHERE userid = @id";
                command.Parameters.Add(new SqlParameter("@id", id));
                command.Parameters.Add(new SqlParameter("@name", name));
                command.Parameters.Add(new SqlParameter("@email", email));
                command.Parameters.Add(new SqlParameter("@info", about));
                command.Parameters.Add(new SqlParameter("@pwd", password));
                command.ExecuteNonQuery();
            }
        }

        public static void SendMessage(int from, string topic, string text)
        {
            using (SqlConnection conn = new SqlConnection(CS))
            {
                if (!ConnOpen(conn)) return;
                SqlCommand command = conn.CreateCommand();
                command.CommandText = "INSERT INTO feedback (who, briefly, indetails, " +
                    "dt, isread) VALUES (@me, @br, @de, @dt, @rd)";
                command.Parameters.Add(new SqlParameter("@me", from));
                command.Parameters.Add(new SqlParameter("@br", topic));
                command.Parameters.Add(new SqlParameter("@de", text));
                command.Parameters.Add(new SqlParameter("@dt", DateTime.Now));
                command.Parameters.Add(new SqlParameter("@rd", false));
                command.ExecuteNonQuery();
            }
        }
        #endregion
        #endregion
    }
}
