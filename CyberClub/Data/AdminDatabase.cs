﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CyberClub.Data
{
    public class AdminDatabase : Database
    {
        #region - common
        public string GetUserName(int id)
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
        public int AddGetDevID(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return -1;
            using (SqlConnection conn = new SqlConnection(CS))
            {
                if (!ConnOpen(conn)) return -1;
                SqlCommand command = conn.CreateCommand();
                command.CommandText =
                    "SELECT count(developerid) AS num FROM developers WHERE developername = @name";
                command.Parameters.Add(new SqlParameter("@name", name));
                if ((int)command.ExecuteScalar() > 0)
                { // Если найден, получить номер
                    command.CommandText =
                        "SELECT TOP 1 developerid FROM developers WHERE developername = @name";
                }
                else
                { // Если не найден, создать и...
                    command.CommandText = "INSERT INTO developers (developername) VALUES (@name)";
                    command.ExecuteNonQuery();
                    // ...получить номер
                    command.CommandText = "SELECT TOP 1 developerid FROM developers " +
                        "WHERE developername = @name ORDER BY developerid DESC";
                }
                return (int)command.ExecuteScalar();
            }
        }

        /// <summary>
        /// Adds the given <paramref name="image"/> to the database
        /// giving it the <paramref name="name"/>
        /// </summary>
        public int AddGetPicID(string name, Image image)
        { // Добавить картинку в БД
            if (string.IsNullOrWhiteSpace(name) || image == null) return -1;
            ImageConverter imgConverter = new ImageConverter();
            byte[] imageBytes = (byte[])
                imgConverter.ConvertTo(image, typeof(byte[]));
            using (SqlConnection conn = new SqlConnection(CS))
            {
                if (!ConnOpen(conn)) return -1;
                SqlCommand command = conn.CreateCommand();
                command.CommandText = "INSERT INTO images (imagename, bin) VALUES (@n, @b)";
                command.Parameters.Add(new SqlParameter("@n", name));
                command.Parameters.Add(new SqlParameter("@b", imageBytes));
                command.ExecuteNonQuery();
                command.CommandText = "SELECT TOP 1 imageid FROM images WHERE bin = @b " +
                    "AND imagename = @n";
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
        public byte AddGenre(string name)
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

        public void AddGame(
            string name, string dev, string path, string imageName, Image image,
            bool singleplayer, bool multiplayer, CheckedListBox genres)
        {
            if (string.IsNullOrWhiteSpace(name)) return;
            using (SqlConnection conn = new SqlConnection(CS))
            {
                if (!ConnOpen(conn)) return;
                // Добавить игру
                SqlCommand command = conn.CreateCommand();
                command.CommandText = "INSERT INTO games (gamename, developer, " +
                    "gameexepath, gameicon, issingleplayer, ismultiplayer)" +
                    "VALUES (@name, @dev, @link, @image, @sp, @mp)";
                command.Parameters.Add(new SqlParameter("@name", name));
                if (string.IsNullOrWhiteSpace(dev))
                    command.Parameters.Add(new SqlParameter("@dev", DBNull.Value));
                else
                    command.Parameters.Add(new SqlParameter("@dev", AddGetDevID(dev)));
                command.Parameters.Add(new SqlParameter("@link", path));
                if (string.IsNullOrWhiteSpace(imageName))
                    command.Parameters.Add(new SqlParameter("@image", DBNull.Value));
                else
                    command.Parameters.Add(new SqlParameter("@image",
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

        public void UpdateGame(int id, string name, int dev, string path, int image,
            bool sp, bool mp, CheckedListBox genres)
        {
            using (SqlConnection conn = new SqlConnection(CS))
            {
                if (!ConnOpen(conn)) return;
                SqlCommand command = conn.CreateCommand();
                command.CommandText = "UPDATE games SET gamename = @name, " +
                    "developer = @dev, gameexepath = @link, gameimage = @image, " +
                    "issingleplayer = @sp, ismultiplayer = @mp WHERE gameid = @id";
                command.Parameters.Add(new SqlParameter("@name", name));
                if (dev > -1)
                    command.Parameters.Add(new SqlParameter("@dev", dev));
                else command.Parameters.Add(new SqlParameter("@dev", DBNull.Value));
                command.Parameters.Add(new SqlParameter("@link", path));
                if (image > -1)
                    command.Parameters.Add(new SqlParameter("@image", image));
                else command.Parameters.Add(new SqlParameter("@image", DBNull.Value));
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

        public Dictionary<string, object> GetGame(int id)
        {
            using (SqlConnection conn = new SqlConnection(CS))
            {
                if (!ConnOpen(conn)) return null;
                SqlCommand command = conn.CreateCommand();
                command.CommandText = "SELECT TOP 1 gamename, gameexepath, gameicon, " +
                    "issingleplayer, ismultiplayer, developername, COUNT(subscriber) AS subs, " +
                    "COUNT(rate) AS rates, CONVERT(varchar, AVG(CAST(rate AS float))) " +
                    "AS rating FROM games LEFT JOIN subscriptions ON gameid = game " +
                    "LEFT JOIN developers ON developer = developerid WHERE gameid = @gid GROUP BY " +
                    "ismultiplayer, issingleplayer, gameicon, gameexepath, " +
                    "developername, gamename, gameid";
                command.Parameters.Add(new SqlParameter("@gid", id));
                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader.Read())
                {
                    return ToDictionary(dataReader);
                }
                else return null;
            }
        }

        public string[] GetGameGenres(int id)
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
                while (dataReader.Read()) res.Add(dataReader["genrename"].ToString());
                return res.ToArray();
            }
        }

        public Dictionary<string, object> GetDeveloper(string name)
        {
            using (SqlConnection conn = new SqlConnection(CS))
            {
                if (!ConnOpen(conn)) return null;
                SqlCommand command = conn.CreateCommand();
                command.CommandText = "SELECT TOP 1 developerid FROM developers WHERE developername = @name";
                command.Parameters.Add(new SqlParameter("@name", name));
                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader.Read())
                {
                    return ToDictionary(dataReader);
                }
                else return null;
            }
        }

        public byte RenameDeveloper(int id, string name)
        {
            using (SqlConnection conn = new SqlConnection(CS))
            {
                if (!ConnOpen(conn)) return 2;
                SqlCommand command = conn.CreateCommand();
                command.CommandText = "UPDATE developers SET developername = @name WHERE developerid = @id";
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

        public byte RenameGenre(string oldName, string newName)
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

        public Dictionary<string, object> GetImage(int id)
        {
            using (SqlConnection conn = new SqlConnection(CS))
            {
                if (!ConnOpen(conn)) return null;
                SqlCommand command = conn.CreateCommand();
                command.CommandText = "SELECT imagename, bin FROM images WHERE imageid = @id";
                command.Parameters.Add(new SqlParameter("@id", id));
                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader.Read())
                {
                    return ToDictionary(dataReader);
                }
                else return null;
            }
        }

        public void RenameImage(int id, string name)
        {
            using (SqlConnection conn = new SqlConnection(CS))
            {
                if (!ConnOpen(conn)) return;
                SqlCommand command = conn.CreateCommand();
                command.CommandText = "UPDATE images SET imagename = @n WHERE imageid = @id";
                command.Parameters.Add(new SqlParameter("@n", name));
                command.Parameters.Add(new SqlParameter("@id", id));
                command.ExecuteNonQuery();
            }
        }

        public void DeleteDeveloper(int id)
        {
            using (SqlConnection conn = new SqlConnection(CS))
            {
                if (!ConnOpen(conn)) return;
                SqlCommand command = conn.CreateCommand();
                command.CommandText = "DELETE FROM developers WHERE developerid = @id";
                command.Parameters.Add(new SqlParameter("@id", id));
                command.ExecuteNonQuery();
            }
        }

        public void DeleteGenre(string name)
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

        public void DeleteImage(int id)
        {
            using (SqlConnection conn = new SqlConnection(CS))
            {
                if (!ConnOpen(conn)) return;
                SqlCommand command = conn.CreateCommand();
                command.Parameters.Add(new SqlParameter("@id", id));
                command.CommandText = "DELETE FROM images WHERE imageid = @id";
                command.ExecuteNonQuery();
            }
        }

        public void DeleteGame(int id)
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
        public byte AddAccount(
            string name, UserLevel level, string email, string about, string password)
        {
            using (SqlConnection conn = new SqlConnection(CS))
            {
                if (!ConnOpen(conn)) return 2;
                // Добавить аккаунт
                SqlCommand command = conn.CreateCommand();
                command.CommandText = "INSERT INTO users (username, email, about, " +
                    "userlevel, userpass) VALUES (@name, @email, @about, @lvl, @userpass)";
                command.Parameters.Add(new SqlParameter("@name", name));
                command.Parameters.Add(new SqlParameter("@email", email));
                command.Parameters.Add(new SqlParameter("@about", about));
                command.Parameters.Add(new SqlParameter("@lvl", level));
                command.Parameters.Add(new SqlParameter("@userpass", password));
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

        public Dictionary<string, object> GetAccountByName(string name)
        {
            using (SqlConnection conn = new SqlConnection(CS))
            {
                if (!ConnOpen(conn)) return null;
                SqlCommand command = conn.CreateCommand();
                command.CommandText = "SELECT TOP 1 userid, email, about, userlevel, userpass " +
                    "FROM users " +
                    "WHERE username = @name GROUP BY userid, email, about, userlevel, userpass";
                command.Parameters.Add(new SqlParameter("@name", name));
                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader.Read())
                {
                    return ToDictionary(dataReader);
                }
                else return null;
            }
        }

        public Dictionary<string, object> GetAccountStats(int id)
        {
            using (SqlConnection conn = new SqlConnection(CS))
            {
                if (!ConnOpen(conn)) return null;
                SqlCommand command = conn.CreateCommand();
                Dictionary<string, object> res = new Dictionary<string, object>(3)
                {
                    { "subs", null }, { "rates", null }, { "messsages", null }
                };
                command.CommandText = "SELECT COUNT(game) AS subs " +
                    "FROM subscriptions WHERE subscriber = @me";
                command.Parameters.Add(new SqlParameter("@me", id));
                res["subs"] = command.ExecuteScalar().ToString();
                command.CommandText = "SELECT COUNT(rate) AS rates " +
                    "FROM subscriptions WHERE subscriber = @me";
                res["rates"] = command.ExecuteScalar().ToString();
                command.CommandText = "SELECT COUNT(messageid) AS msgs " +
                    "FROM textmessages WHERE sender = @me";
                res["messages"] = command.ExecuteScalar().ToString();
                return res;
            }
        }

        public byte UpdateAccount(int id,
            string name, string email, string about, UserLevel level, string password)
        {
            using (SqlConnection conn = new SqlConnection(CS))
            {
                if (!ConnOpen(conn)) return 2;
                SqlCommand command = conn.CreateCommand();
                command.CommandText = "UPDATE users SET username = @name, email = @email, " +
                "about = @about, userlevel = @lvl, userpass = @pwd WHERE userid = @id";
                command.Parameters.Add(new SqlParameter("@name", name));
                command.Parameters.Add(new SqlParameter("@email", email));
                command.Parameters.Add(new SqlParameter("@about", about));
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

        public void DeleteAccount(int id)
        {
            using (SqlConnection conn = new SqlConnection(CS))
            {
                if (!ConnOpen(conn)) return;
                SqlCommand command = conn.CreateCommand();
                command.Parameters.Add(new SqlParameter("@id", id));
                command.CommandText = "UPDATE textmessages SET sender = NULL WHERE sender = @id";
                command.ExecuteNonQuery(); // триггер делает не то
                command.CommandText = "DELETE FROM users WHERE userid = @id";
                command.ExecuteNonQuery();
            }
        }
        #endregion
        #region - messages
        public string GetMessageText(int id)
        {
            using (SqlConnection conn = new SqlConnection(CS))
            {
                if (!ConnOpen(conn)) return null;
                SqlCommand command = conn.CreateCommand();
                command.CommandText =
                    "SELECT TOP 1 longtext FROM textmessages WHERE messageid = @id";
                command.Parameters.Add(new SqlParameter("@id",
                    id));
                string res = command.ExecuteScalar().ToString();
                command.CommandText =
                    "UPDATE textmessages SET isread = @isread WHERE messageid = @id";
                command.Parameters.Add(new SqlParameter("@isread", true));
                command.ExecuteNonQuery();
                return res;
            }
        }

        public void SetMessageIsRead(int id, bool status)
        {
            using (SqlConnection conn = new SqlConnection(CS))
            {
                if (!ConnOpen(conn)) return;
                SqlCommand command = conn.CreateCommand();
                command.CommandText =
                    "UPDATE textmessages SET isread = @isread WHERE messageid = @id";
                command.Parameters.Add(new SqlParameter("@isread", status));
                command.Parameters.Add(new SqlParameter("@id", id));
                command.ExecuteNonQuery();
            }
        }

        public DataTable GetDataTable(string query)
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
