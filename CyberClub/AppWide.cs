using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
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

        public enum UserLevel
        {
            Admin = 0,
            Player = 1,
            Banned = 2
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
                command.CommandText = "SELECT username, email, info, passwd, userlevel " +
                    "FROM users WHERE userid = @id";
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

        #region Admin
        #region - common
        public static string GetUserName(int id)
        {
            using (SqlConnection conn = new SqlConnection(CS))
            {
                if (!ConnOpen(conn)) return string.Empty;
                SqlCommand command = conn.CreateCommand();
                command.CommandText = "SELECT username FROM users WHERE userid = @id";
                command.Parameters.Add(new SqlParameter("@id", id));
                return command.ExecuteScalar().ToString();
            }
        }
        #endregion
        #region - game
        /// <summary>
        /// Если возможно, по имени находит в базе данных индекс разработчика.
        /// Иначе добавляет нового разработчика с именем name и возвращает индекс.
        /// </summary>
        public static int AddGetDevID(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return -1;
            using (SqlConnection conn = new SqlConnection(CS))
            {
                if (!ConnOpen(conn)) return -1;
                SqlCommand command = conn.CreateCommand();
                command.CommandText =
                    "SELECT count(devid) AS num FROM devs WHERE devname = @name";
                command.Parameters.Add(new SqlParameter("@name", name));
                if ((int)command.ExecuteScalar() > 0)
                { // Если найден, получить номер
                    command.CommandText =
                        "SELECT TOP 1 devid FROM devs WHERE devname = @name";
                }
                else
                { // Если не найден, создать и...
                    command.CommandText = "INSERT INTO devs (devname) VALUES (@name)";
                    command.ExecuteNonQuery();
                    // ...получить номер
                    command.CommandText = "SELECT TOP 1 devid FROM devs " +
                        "WHERE devname = @name ORDER BY devid DESC";
                }
                return (int)command.ExecuteScalar();
            }
        }

        /// <summary>
        /// Adds the given <paramref name="image"/> to the database
        /// giving it the <paramref name="name"/>
        /// </summary>
        public static int AddGetPicID(string name, Image image)
        { // Добавить картинку в БД
            if (string.IsNullOrWhiteSpace(name) || image == null) return -1;
            ImageConverter imgConverter = new ImageConverter();
            byte[] imageBytes = (byte[])
                imgConverter.ConvertTo(image, typeof(byte[]));
            using (SqlConnection conn = new SqlConnection(CS))
            {
                if (!ConnOpen(conn)) return -1;
                SqlCommand command = conn.CreateCommand();
                command.CommandText = "INSERT INTO pics (picname, bin) VALUES (@n, @b)";
                command.Parameters.Add(new SqlParameter("@n", name));
                command.Parameters.Add(new SqlParameter("@b", imageBytes));
                command.ExecuteNonQuery();
                command.CommandText = "SELECT TOP 1 picid FROM pics WHERE bin = @b " +
                    "AND picname = @n";
                return (int)command.ExecuteScalar();
            }
        }

        /// <summary>
        /// Adds the genre of the given <paramref name="name"/> to the database<br/>
        /// Returns the operation result
        /// </summary>
        /// <returns>
        /// <c>3</c> if <paramref name="name"/> is empty;<br/>
        /// <c>2</c> if the database connection falis;<br/>
        /// <c>1</c> if the <paramref name="name"/> is already used;<br/>
        /// <c>0</c> if the operation succeeds
        /// </returns>
        public static byte AddGenre(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return 3;
            using (SqlConnection conn = new SqlConnection(CS))
            {
                if (!ConnOpen(conn)) return 2;
                SqlCommand command = conn.CreateCommand();
                command.CommandText = "INSERT INTO genres (genrename) VALUES (@gnr)";
                command.Parameters.Add(new SqlParameter("@gnr", name));
                try { command.ExecuteNonQuery(); }
                catch (SqlException) { return 1; }
                return 0;
            }
        }

        public static void AddGame(
            string name, string dev, string path, string imageName, Image image,
            bool singleplayer, bool multiplayer, CheckedListBox genres)
        {
            if (string.IsNullOrWhiteSpace(name)) return;
            using (SqlConnection conn = new SqlConnection(CS))
            {
                if (!ConnOpen(conn)) return;
                // Добавить игру
                SqlCommand command = conn.CreateCommand();
                command.CommandText = "INSERT INTO games (gamename, madeby, " +
                    "gamelink, gamepic, singleplayer, multiplayer)" +
                    "VALUES (@name, @dev, @link, @pic, @sp, @mp)";
                command.Parameters.Add(new SqlParameter("@name", name));
                if (string.IsNullOrWhiteSpace(dev))
                    command.Parameters.Add(new SqlParameter("@dev", DBNull.Value));
                else
                    command.Parameters.Add(new SqlParameter("@dev", AddGetDevID(dev)));
                command.Parameters.Add(new SqlParameter("@link", path));
                if (string.IsNullOrWhiteSpace(imageName))
                    command.Parameters.Add(new SqlParameter("@pic", DBNull.Value));
                else
                    command.Parameters.Add(new SqlParameter("@pic",
                        AddGetPicID(imageName, image)));
                command.Parameters.Add(new SqlParameter("@sp", singleplayer));
                command.Parameters.Add(new SqlParameter("@mp", multiplayer));
                command.ExecuteNonQuery();
                if (genres?.CheckedItems.Count > 0)
                {
                    // Получить индекс игры
                    command.CommandText = "SELECT TOP 1 gameid FROM games " +
                        "WHERE gamename = @name ORDER BY gameid DESC";
                    int id = (int)command.ExecuteScalar();
                    // Присвоить игре жанры
                    command.CommandText =
                        "INSERT INTO gamegenre (game, genre) VALUES" +
                        GenreValues(genres, command);
                    command.Parameters.Add(new SqlParameter("@id", id));
                    command.ExecuteNonQuery();
                }
            }
        }

        public static void UpdateGame(int id, string name, int dev, string path, int pic,
            bool sp, bool mp, CheckedListBox genres)
        {
            using (SqlConnection conn = new SqlConnection(CS))
            {
                if (!ConnOpen(conn)) return;
                SqlCommand command = conn.CreateCommand();
                command.CommandText = "UPDATE games SET gamename = @name, " +
                    "madeby = @dev, gamelink = @link, gamepic = @pic, " +
                    "singleplayer = @sp, multiplayer = @mp WHERE gameid = @id";
                command.Parameters.Add(new SqlParameter("@name", name));
                if (dev > -1)
                    command.Parameters.Add(new SqlParameter("@dev", dev));
                else command.Parameters.Add(new SqlParameter("@dev", DBNull.Value));
                command.Parameters.Add(new SqlParameter("@link", path));
                if (pic > -1)
                    command.Parameters.Add(new SqlParameter("@pic", pic));
                else command.Parameters.Add(new SqlParameter("@pic", DBNull.Value));
                command.Parameters.Add(new SqlParameter("@sp", sp));
                command.Parameters.Add(new SqlParameter("@mp", mp));
                command.Parameters.Add(new SqlParameter("@id", id));
                command.ExecuteNonQuery();
                command.CommandText = "DELETE FROM gamegenre WHERE game = @id";
                command.ExecuteNonQuery();
                if (genres?.CheckedItems.Count > 0)
                {
                    command.CommandText =
                        "INSERT INTO gamegenre (game, genre) VALUES" +
                        GenreValues(genres, command);
                    command.ExecuteNonQuery();
                }
            }
        }

        private static string GenreValues(CheckedListBox CLB, SqlCommand command)
        {
            string query = "";
            for (int i = 0; i < CLB.CheckedItems.Count; i++)
            {
                command.CommandText = $"SELECT genreid FROM genres WHERE genrename = @g{i}";
                command.Parameters.Add(new SqlParameter($"@g{i}",
                    CLB.CheckedItems[i].ToString()));
                query += $" (@id, @i{i}),";
                command.Parameters.Add(new SqlParameter($"@i{i}",
                    command.ExecuteScalar().ToString()));
            }
            return query.Substring(0, query.Length - 1);
        }

        public static Dictionary<string, object> GetGame(int id)
        {
            using (SqlConnection conn = new SqlConnection(CS))
            {
                if (!ConnOpen(conn)) return null;
                SqlCommand command = conn.CreateCommand();
                command.CommandText = "SELECT TOP 1 gamename, gamelink, gamepic, " +
                    "singleplayer, multiplayer, devname, COUNT(who) AS subs, " +
                    "COUNT(rate) AS rates, CONVERT(varchar, AVG(CAST(rate AS float))) " +
                    "AS rating FROM games LEFT JOIN subscriptions ON gameid = game " +
                    "LEFT JOIN devs ON madeby = devid WHERE gameid = @gid GROUP BY " +
                    "multiplayer, singleplayer, gamepic, gamelink, " +
                    "devname, gamename, gameid";
                command.Parameters.Add(new SqlParameter("@gid", id));
                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader.Read())
                {
                    return dataReader.ToDictionary();
                }
                else return null;
            }
        }

        public static string[] GetGameGenres(int id)
        {
            using (SqlConnection conn = new SqlConnection(CS))
            {
                if (!ConnOpen(conn)) return null;
                SqlCommand command = conn.CreateCommand();
                command.Parameters.Add(new SqlParameter("@gid", id));
                command.CommandText = "SELECT genrename FROM gamegenre " +
                    "LEFT JOIN genres ON genre = genreid WHERE game = @gid";
                SqlDataReader dataReader = command.ExecuteReader();
                List<string> res = new List<string>();
                while (dataReader.Read()) res.Add(dataReader["GenreName"].ToString());
                return res.ToArray();
            }
        }

        public static Dictionary<string, object> GetDeveloper(string name)
        {
            using (SqlConnection conn = new SqlConnection(CS))
            {
                if (!ConnOpen(conn)) return null;
                SqlCommand command = conn.CreateCommand();
                command.CommandText = "SELECT TOP 1 devid FROM devs WHERE devname = @name";
                command.Parameters.Add(new SqlParameter("@name", name));
                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader.Read())
                {
                    return dataReader.ToDictionary();
                }
                else return null;
            }
        }

        public static byte RenameDeveloper(int id, string name)
        {
            using (SqlConnection conn = new SqlConnection(CS))
            {
                if (!ConnOpen(conn)) return 2;
                SqlCommand command = conn.CreateCommand();
                command.CommandText = "UPDATE devs SET devname = @name WHERE devid = @id";
                command.Parameters.Add(new SqlParameter("@name", name));
                command.Parameters.Add(new SqlParameter("@id", id));
                try
                {
                    command.ExecuteNonQuery();
                    return 0;
                }
                catch (SqlException)
                {
                    return 1;
                }
            }
        }

        public static byte RenameGenre(string oldName, string newName)
        {
            using (SqlConnection conn = new SqlConnection(CS))
            {
                if (!ConnOpen(conn)) return 2;
                SqlCommand command = conn.CreateCommand();
                command.CommandText =
                    "UPDATE genres SET genrename = @new WHERE genrename = @old";
                command.Parameters.Add(new SqlParameter("@new", newName));
                command.Parameters.Add(new SqlParameter("@old", oldName));
                try
                {
                    command.ExecuteNonQuery();
                    return 0;
                }
                catch (SqlException)
                {
                    return 1;
                }
            }
        }

        public static Dictionary<string, object> GetImage(int id)
        {
            using (SqlConnection conn = new SqlConnection(CS))
            {
                if (!ConnOpen(conn)) return null;
                SqlCommand command = conn.CreateCommand();
                command.CommandText = "SELECT picname, bin FROM pics WHERE picid = @id";
                command.Parameters.Add(new SqlParameter("@id", id));
                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader.Read())
                {
                    return dataReader.ToDictionary();
                }
                else return null;
            }
        }

        public static void RenameImage(int id, string name)
        {
            using (SqlConnection conn = new SqlConnection(CS))
            {
                if (!ConnOpen(conn)) return;
                SqlCommand command = conn.CreateCommand();
                command.CommandText = "UPDATE pics SET picname = @n WHERE picid = @id";
                command.Parameters.Add(new SqlParameter("@n", name));
                command.Parameters.Add(new SqlParameter("@id", id));
                command.ExecuteNonQuery();
            }
        }

        public static void DeleteDeveloper(int id)
        {
            using (SqlConnection conn = new SqlConnection(CS))
            {
                if (!ConnOpen(conn)) return;
                SqlCommand command = conn.CreateCommand();
                command.CommandText = "DELETE FROM devs WHERE devid = @id";
                command.Parameters.Add(new SqlParameter("@id", id));
                command.ExecuteNonQuery();
            }
        }

        public static void DeleteGenre(string name)
        {
            using (SqlConnection conn = new SqlConnection(CS))
            {
                if (!ConnOpen(conn)) return;
                SqlCommand command = conn.CreateCommand();
                command.CommandText =
                    "SELECT TOP 1 genreid FROM genres WHERE genrename = @name";
                command.Parameters.Add(new SqlParameter("@name", name));
                int id = (int)command.ExecuteScalar();
                command.CommandText = "DELETE FROM genres WHERE genreid = @id";
                command.Parameters.Add(new SqlParameter("@id", id));
                command.ExecuteNonQuery();
            }
        }

        public static void DeleteImage(int id)
        {
            using (SqlConnection conn = new SqlConnection(CS))
            {
                if (!ConnOpen(conn)) return;
                SqlCommand command = conn.CreateCommand();
                command.Parameters.Add(new SqlParameter("@id", id));
                command.CommandText = "DELETE FROM pics WHERE picid = @id";
                command.ExecuteNonQuery();
            }
        }

        public static void DeleteGame(int id)
        {
            using (SqlConnection conn = new SqlConnection(CS))
            {
                if (!ConnOpen(conn)) return;
                SqlCommand command = conn.CreateCommand();
                command.Parameters.Add(new SqlParameter("@id", id));
                command.CommandText = "DELETE FROM games WHERE gameid = @id";
                command.ExecuteNonQuery();
            }
        }
        #endregion
        #region - account
        public static byte AddAccount(
            string name, int level, string email, string info, string password)
        {
            using (SqlConnection conn = new SqlConnection(CS))
            {
                if (!ConnOpen(conn)) return 2;
                // Добавить аккаунт
                SqlCommand command = conn.CreateCommand();
                command.CommandText = "INSERT INTO users (username, email, info, " +
                    "userlevel, passwd) VALUES (@name, @email, @info, @lvl, @passwd)";
                command.Parameters.Add(new SqlParameter("@name", name));
                command.Parameters.Add(new SqlParameter("@email", email));
                command.Parameters.Add(new SqlParameter("@info", info));
                command.Parameters.Add(new SqlParameter("@lvl", level));
                command.Parameters.Add(new SqlParameter("@passwd", password));
                try
                {
                    command.ExecuteNonQuery();
                    return 0;
                }
                catch (SqlException)
                {
                    return 1;
                }
            }
        }

        public static Dictionary<string, object> GetAccountByName(string name)
        {
            using (SqlConnection conn = new SqlConnection(CS))
            {
                if (!ConnOpen(conn)) return null;
                SqlCommand command = conn.CreateCommand();
                command.CommandText = "SELECT TOP 1 userid, email, info, userlevel, passwd " +
                    "FROM users " +
                    "WHERE username = @name GROUP BY userid, email, info, userlevel, passwd";
                command.Parameters.Add(new SqlParameter("@name", name));
                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader.Read())
                {
                    return dataReader.ToDictionary();
                }
                else return null;
            }
        }

        public static Dictionary<string, object> GetAccountStats(int id)
        {
            using (SqlConnection conn = new SqlConnection(CS))
            {
                if (!ConnOpen(conn)) return null;
                SqlCommand command = conn.CreateCommand();
                command.CommandText = "SELECT COUNT(game) AS subs " +
                    "FROM subscriptions WHERE who = @me";
                command.Parameters.Add(new SqlParameter("@me", id));
                Dictionary<string, object> res = new Dictionary<string, object>();
                res.Add("subs", command.ExecuteScalar().ToString());
                command.CommandText = "SELECT COUNT(rate) AS rates " +
                    "FROM subscriptions WHERE who = @me";
                res.Add("rates", command.ExecuteScalar().ToString());
                command.CommandText = "SELECT COUNT(messageid) AS msgs " +
                    "FROM feedback WHERE who = @me";
                res.Add("messages", command.ExecuteScalar().ToString());
                return res;
            }
        }

        public static byte UpdateAccount(int id,
            string name, string email, string info, int level, string password)
        {
            using (SqlConnection conn = new SqlConnection(CS))
            {
                if (!ConnOpen(conn)) return 2;
                SqlCommand command = conn.CreateCommand();
                command.CommandText = "UPDATE users SET username = @name, email = @email, " +
                "info = @info, userlevel = @lvl, passwd = @pwd WHERE userid = @id";
                command.Parameters.Add(new SqlParameter("@name", name));
                command.Parameters.Add(new SqlParameter("@email", email));
                command.Parameters.Add(new SqlParameter("@info", info));
                command.Parameters.Add(new SqlParameter("@lvl", level));
                command.Parameters.Add(new SqlParameter("@pwd", password));
                command.Parameters.Add(new SqlParameter("@id", id));
                try
                {
                    command.ExecuteNonQuery();
                    return 0;
                }
                catch (SqlException)
                {
                    return 1;
                }
            }
        }

        public static void DeleteAccount(int id)
        {
            using (SqlConnection conn = new SqlConnection(CS))
            {
                if (!ConnOpen(conn)) return;
                SqlCommand command = conn.CreateCommand();
                command.Parameters.Add(new SqlParameter("@id", id));
                command.CommandText = "UPDATE feedback SET who = NULL WHERE who = @id";
                command.ExecuteNonQuery(); // триггер делает не то
                command.CommandText = "DELETE FROM users WHERE userid = @id";
                command.ExecuteNonQuery();
            }
        }
        #endregion
        #endregion

        #region Messages
        public static string GetMessageText(int id)
        {
            using (SqlConnection conn = new SqlConnection(CS))
            {
                if (!ConnOpen(conn)) return null;
                SqlCommand command = conn.CreateCommand();
                command.CommandText =
                    "SELECT TOP 1 indetails FROM feedback WHERE messageid = @id";
                command.Parameters.Add(new SqlParameter("@id",
                    id));
                string res = command.ExecuteScalar().ToString();
                command.CommandText =
                    "UPDATE feedback SET isread = @isread WHERE messageid = @id";
                command.Parameters.Add(new SqlParameter("@isread", true));
                command.ExecuteNonQuery();
                return res;
            }
        }

        public static void SetMessageIsRead(int id, bool status)
        {
            using (SqlConnection conn = new SqlConnection(CS))
            {
                if (!ConnOpen(conn)) return;
                SqlCommand command = conn.CreateCommand();
                command.CommandText =
                    "UPDATE feedback SET isread = @isread WHERE messageid = @id";
                command.Parameters.Add(new SqlParameter("@isread", status));
                command.Parameters.Add(new SqlParameter("@id", id));
                command.ExecuteNonQuery();
            }
        }

        public static DataTable GetDataTable(string query)
        {
            using (SqlConnection conn = new SqlConnection(CS))
            {
                if (!ConnOpen(conn)) return null;
                using (SqlCommand command = conn.CreateCommand())
                {
                    command.CommandText = query;
                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
                    using (DataSet dataSet = new DataSet())
                    {
                        dataAdapter.Fill(dataSet);
                        return dataSet.Tables[0];
                    }
                }
            }
        }
        #endregion
    }
}
