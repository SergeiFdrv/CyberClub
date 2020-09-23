using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CyberClub
{
    public partial class AdminForm : Form
    {
        public AdminForm()
        {
            InitializeComponent();
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(LoginForm.CS))
            {
                if (!ConnOpen(conn)) return;
                SqlCommand command = conn.CreateCommand();
                command.CommandText = "SELECT username FROM users WHERE userid = @id";
                command.Parameters.Add(new SqlParameter("@id", LoginForm.UserID));
                UserLabel.Text = command.ExecuteScalar().ToString();
            }
            LeftGames.Checked = true;
        }

        private void AdminForm_FormClosed(object sender, FormClosedEventArgs e) =>
            Owner.Show();

        // -------------------- Кнопки левого бокового меню --------------------
        private void LeftGames_CheckedChanged(object sender, EventArgs e)
        {
            if (GamesPanel.Visible = LeftGames.Checked)
            {
                UpdateData(GamesPanel, GamesDGVQuery, new ComboBox[] { GEditID }, "id");
            }
        }

        private void LeftAccounts_CheckedChanged(object sender, EventArgs e)
        {
            if (AccountsPanel.Visible = LeftAccounts.Checked)
            {
                UpdateData(AccountsPanel, AccountsDGVQuery,
                new ComboBox[] { AxEditName }, "username");
            }
        }

        private void LeftMessages_CheckedChanged(object sender, EventArgs e)
        {
            if (MessagesPanel.Visible = LeftMessages.Checked)
            {
                UpdateTable(DGVMessages, MessagesDGVQuery);
                foreach (DataGridViewRow i in DGVMessages.Rows)
                { // Выделить непрочитанные сообщения жирным шрифтом
                    if (!(i.Cells["isread"].Value is null || (bool)i.Cells["isread"].Value))
                    {
                        i.DefaultCellStyle.Font = new Font(DGVMessages.Font, FontStyle.Bold);
                    }
                }
            }
        }

        private void LogOutButton_CheckedChanged(object sender, EventArgs e) => Close();

        // ------------------------- ИГРЫ -------------------------
        // -------------------- Верхние кнопки --------------------
        private void GamesAllTab_Click(object sender, EventArgs e)
        {
            GameAddPanel.Visible = GameEditPanel.Visible = false;
            DGVGames.Visible = true;
            UpdateData(GamesPanel, GamesDGVQuery, new ComboBox[] { GEditID }, "id");
        }

        private void GamesAddTab_Click(object sender, EventArgs e)
        {
            DGVGames.Visible = GameEditPanel.Visible = false;
            GameAddPanel.Visible = true;
            UpdateBox(GAddDev.Items, "devname", "devs", "devid");
            UpdateBox(GAddGenresCLB.Items, "genrename", "genres", "genreid");
        }

        private void GamesEditTab_Click(object sender, EventArgs e)
        {
            DGVGames.Visible = GameAddPanel.Visible = false;
            GameEditPanel.Visible = true;
            UpdateBox(GEditDev.Items, "devname", "devs", "devid");
            UpdateBox(GEditGenresCLB.Items, "genrename", "genres", "genreid");
            UpdateBox(GEditPicID.Items, "picid", "pics");
        }

        // ----------------------- Внутри вкладки "Добавить игру" -----------------------
        private void GAddNewDevBtn_Click(object sender, EventArgs e)
        {
            if (AddGetDevID(GAddDev.Text) < 0) Voice.Say(Resources.Lang.AddError);
            GAddDev.Text = "";
            UpdateBox(GAddDev.Items, "devname", "devs", "devid");
        }

        /// <summary>
        /// Если возможно, по имени находит в базе данных индекс разработчика.
        /// Иначе добавляет нового разработчика с именем name и возвращает индекс.
        /// </summary>
        private static int AddGetDevID(string name)
        {
            if (name.Length == 0) return -1;
            using (SqlConnection conn = new SqlConnection(LoginForm.CS))
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

        private void GAddNewGenreBtn_Click(object sender, EventArgs e)
        { // Добавить в базу новый жанр
            if (GAddNewGenre.Text.Length == 0) return;
            using (SqlConnection conn = new SqlConnection(LoginForm.CS))
            {
                if (!ConnOpen(conn)) return;
                SqlCommand command = conn.CreateCommand();
                command.CommandText = "INSERT INTO genres (genrename) VALUES (@gnr)";
                command.Parameters.Add(new SqlParameter("@gnr", GAddNewGenre.Text));
                try { command.ExecuteNonQuery(); }
                catch (SqlException) { Voice.Say(Resources.Lang.NameAlreadyUsed); }
            }
            UpdateBox(GAddGenresCLB.Items, "genrename", "genres", "genreid");
            GAddNewGenre.Text = "";
        }

        private void GAddPicBrowse_Click(object sender, EventArgs e)
        { // Открыть выбор файла. Выбранное фото добавить в PictureBox
            if (GamePicOFD.ShowDialog() == DialogResult.OK)
            {
                GAddPicBox.Image = Image.FromStream(GamePicOFD.OpenFile());
                GAddPicName.Text = GamePicOFD.FileName.
                    Remove(0, GamePicOFD.FileName.LastIndexOf('\\') + 1);
            }
        }

        private void GAddPicButton_Click(object sender, EventArgs e) =>
            Voice.Say(AddGetPicID(GAddPicName.Text) < 0 ? 
                Resources.Lang.NameNotEntered : Resources.Lang.AddedSuccessfully);

        private int AddGetPicID(string name)
        { // Добавить картинку в БД
            if (name.Length == 0) return -1;
            ImageConverter imgConverter = new ImageConverter();
            byte[] image = (byte[])
                imgConverter.ConvertTo(GAddPicBox.Image, typeof(byte[]));
            using (SqlConnection conn = new SqlConnection(LoginForm.CS))
            {
                if (!ConnOpen(conn)) return -1;
                SqlCommand command = conn.CreateCommand();
                command.CommandText = "INSERT INTO pics (picname, bin) VALUES (@n, @b)";
                command.Parameters.Add(new SqlParameter("@n", GAddPicName.Text));
                command.Parameters.Add(new SqlParameter("@b", image));
                command.ExecuteNonQuery();
                command.CommandText = "SELECT TOP 1 picid FROM pics WHERE bin = @b " +
                    "AND picname = @n";
                return (int)command.ExecuteScalar();
            }
        }

        private void GAddSubmit_Click(object sender, EventArgs e)
        { // Добавить в базу новую игру
            if (GAddName.Text.Length == 0) return;
            using (SqlConnection conn = new SqlConnection(LoginForm.CS))
            {
                if (!ConnOpen(conn)) return;
                // Добавить игру
                SqlCommand command = conn.CreateCommand();
                command.CommandText = "INSERT INTO games (gamename, madeby, " +
                    "gamelink, gamepic, singleplayer, multiplayer)" +
                    "VALUES (@name, @dev, @link, @pic, @sp, @mp)";
                command.Parameters.Add(new SqlParameter("@name", GAddName.Text));
                if (GAddDev.Text.Length == 0)
                    command.Parameters.Add(new SqlParameter("@dev", DBNull.Value));
                else
                {
                    command.Parameters.Add(new SqlParameter("@dev",
                        AddGetDevID(GAddDev.Text)));
                    UpdateBox(GAddDev.Items, "devname", "devs", "devid");
                }
                command.Parameters.Add(new SqlParameter("@link", GAddLink.Text));
                if (GAddPicName.Text.Length == 0)
                    command.Parameters.Add(new SqlParameter("@pic", DBNull.Value));
                else
                    command.Parameters.Add(new SqlParameter("@pic",
                        AddGetPicID(GAddPicName.Text)));
                command.Parameters.Add(new SqlParameter("@sp", GAddSingleCB.Checked));
                command.Parameters.Add(new SqlParameter("@mp", GAddMultiCB.Checked));
                command.ExecuteNonQuery();
                if (GAddGenresCLB.CheckedItems.Count > 0)
                {
                    // Получить индекс игры
                    command.CommandText = "SELECT TOP 1 gameid FROM games " +
                        "WHERE gamename = @name ORDER BY gameid DESC";
                    int id = (int)command.ExecuteScalar();
                    // Присвоить игре жанры
                    string query = "INSERT INTO gamegenre (game, genre) VALUES";
                    command.CommandText = query + GenreValues(GAddGenresCLB, command);
                    command.Parameters.Add(new SqlParameter("@id", id));
                    command.ExecuteNonQuery();
                }
            }
            Voice.Say(Resources.Lang.GameAddedToDB);
            UpdateData(GamesPanel, GamesDGVQuery, new ComboBox[] { GEditID }, "id");
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

        // ---------------------- Внутри вкладки "Изменить игру" ----------------------
        private void DGVGames_CellDoubleClick
            (object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                GamesEditTab_Click(sender, e);
                GEditID.Text = DGVGames.Rows[e.RowIndex].Cells["id"].Value.ToString();
            }
        }

        private void GEditNameCB_TextChanged(object sender, EventArgs e)
        { // Выбор редактируемой записи об игре
            if (!int.TryParse(GEditID.Text, out int id))
            {
                GEditName.Text = "";
                GEditName.Enabled = false;
                GEditSubsN.Text = GEditRatesN.Text = $"{0}";
                GEditRatingN.Text = $"{0.0}";
                return;
            }
            GEditName.Enabled = true;
            using (SqlConnection conn = new SqlConnection(LoginForm.CS))
            {
                if (!ConnOpen(conn)) return;
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
                    GEditName.Text = dataReader["gamename"].ToString();
                    GEditLink.Text = dataReader["gamelink"].ToString();
                    GEditDev.Text = dataReader["devname"].ToString();
                    GEditPicID.Text = dataReader["gamepic"].ToString();
                    GEditSingleCB.Checked = (bool)dataReader["singleplayer"];
                    GEditMultiCB.Checked = (bool)dataReader["multiplayer"];
                    GEditSubsN.Text = dataReader["subs"].ToString();
                    GEditRatesN.Text = dataReader["rates"].ToString();
                    GEditRatingN.Text = dataReader["rating"].ToString();
                    dataReader.Close();
                    command.CommandText = "SELECT genrename FROM gamegenre " +
                        "LEFT JOIN genres ON genre = genreid WHERE game = @gid";
                    dataReader = command.ExecuteReader();
                    for (int i = 0; i < GEditGenresCLB.Items.Count; i++)
                        GEditGenresCLB.SetItemChecked(i, false);
                    while (dataReader.Read())
                        GEditGenresCLB.SetItemChecked(GEditGenresCLB.Items.IndexOf(
                            dataReader["genrename"].ToString()), true);
                }
            }
        }

        private void GEditDev_TextChanged(object sender, EventArgs e)
        { // Выбор редактируемого имени разработчика
            if (GEditDev.Text.Length == 0)
            {
                GEditDevID.Text += '_';
                return;
            }
            string query = "SELECT TOP 1 devid FROM devs WHERE devname = @name";
            using (SqlConnection conn = new SqlConnection(LoginForm.CS))
            {
                if (!ConnOpen(conn)) return;
                SqlCommand command = conn.CreateCommand();
                command.CommandText = query;
                command.Parameters.Add(new SqlParameter("@name", GEditDev.Text));
                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader.Read())
                    GEditDevID.Text = dataReader["devid"].ToString();
            }
        }

        private void GEditDevBtn_Click(object sender, EventArgs e)
        { // Переименовать разработчика
            if (Voice.Ask(Resources.Lang.RenameDeveloperPrompt) == DialogResult.No)
                return;
            if (!int.TryParse(GEditDevID.Text, out int id)) return;
            string query = "UPDATE devs SET devname = @name WHERE devid = @id";
            using (SqlConnection conn = new SqlConnection(LoginForm.CS))
            {
                if (!ConnOpen(conn)) return;
                SqlCommand command = conn.CreateCommand();
                command.CommandText = query;
                command.Parameters.Add(new SqlParameter("@name", GEditDev.Text));
                command.Parameters.Add(new SqlParameter("@id", id));
                try { command.ExecuteNonQuery(); }
                catch (SqlException) { Voice.Say(Resources.Lang.NameAlreadyUsed); }
            }
            UpdateData(GamesPanel, GamesDGVQuery, new ComboBox[] { GEditID }, "id");
            UpdateBox(GEditDev.Items, "devname", "devs", "devid");
        }

        private void GEditGenresCLB_SelectedIndexChanged(object sender, EventArgs e) =>
            GEditGenre.Text = GEditGenresCLB.SelectedItem.ToString();

        private void GEditGenreBtn_Click(object sender, EventArgs e)
        { // Переименовать жанр
            if (GEditGenre.Text.Length == 0 || GEditGenresCLB.SelectedItem is null)
            {
                Voice.Say(Resources.Lang.SelectAGenreNTypeTheName);
                return;
            }
            if (Voice.Ask(Resources.Lang.RenameGenrePrompt) == DialogResult.No)
                return;
            string query = "UPDATE genres SET genrename = @n0 WHERE genrename = @n1";
            using (SqlConnection conn = new SqlConnection(LoginForm.CS)) 
            {
                if (!ConnOpen(conn)) return;
                SqlCommand command = conn.CreateCommand();
                command.CommandText = query;
                command.Parameters.Add(new SqlParameter("@n0", GEditGenre.Text));
                command.Parameters.Add(new SqlParameter("@n1",
                    GEditGenresCLB.SelectedItem.ToString()));
                try { command.ExecuteNonQuery(); }
                catch (SqlException) { Voice.Say(Resources.Lang.NameAlreadyUsed); }
            }
            UpdateBox(GEditGenresCLB.Items, "genrename", "genres", "genreid");
        }

        private void GEditPicName_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(GEditPicID.Text, out int id))
            {
                GEditPicName.Text = "";
                GEditPicBox.Image = GEditPicBox.InitialImage;
                return;
            }
            using (SqlConnection conn = new SqlConnection(LoginForm.CS))
            {
                if (!ConnOpen(conn)) return;
                SqlCommand command = conn.CreateCommand();
                command.CommandText = "SELECT picname, bin FROM pics WHERE picid = @id";
                command.Parameters.Add(new SqlParameter("@id", id));
                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader.Read())
                {
                    GEditPicName.Text = dataReader["picname"].ToString();
                    using (MemoryStream memoryStream =
                        new MemoryStream((byte[])dataReader["bin"]))
                    {
                        GEditPicBox.Image = Image.FromStream(memoryStream);
                    }
                }
            }
        }

        private void GEditPicBtn_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(GEditPicID.Text, out int id)) return;
            using (SqlConnection conn = new SqlConnection(LoginForm.CS))
            {
                if (!ConnOpen(conn)) return;
                SqlCommand command = conn.CreateCommand();
                command.CommandText = "UPDATE pics SET picname = @n WHERE picid = @id";
                command.Parameters.Add(new SqlParameter("@n", GEditPicName.Text));
                command.Parameters.Add(new SqlParameter("@id", id));
                command.ExecuteNonQuery();
            }
            UpdateBox(GEditPicID.Items, "picid", "pics");
        }

        private void GEditSubmit_Click(object sender, EventArgs e)
        { // Обновить запись об игре
            if (Voice.Ask(Resources.Lang.UpdateGamePrompt) == DialogResult.No) return;
            if (!int.TryParse(GEditID.Text, out int id)) return;
            using (SqlConnection conn = new SqlConnection(LoginForm.CS))
            {
                if (!ConnOpen(conn)) return;
                SqlCommand command = conn.CreateCommand();
                command.CommandText = "UPDATE games SET gamename = @name, " +
                    "madeby = @dev, gamelink = @link, gamepic = @pic, " +
                    "singleplayer = @sp, multiplayer = @mp WHERE gameid = @id";
                command.Parameters.Add(new SqlParameter("@name", GEditName.Text));
                if (int.TryParse(GEditDevID.Text, out int dev))
                    command.Parameters.Add(new SqlParameter("@dev", dev));
                else command.Parameters.Add(new SqlParameter("@dev", DBNull.Value));
                command.Parameters.Add(new SqlParameter("@link", GEditLink.Text));
                if (int.TryParse(GEditPicID.Text, out int pic))
                    command.Parameters.Add(new SqlParameter("@pic", pic));
                else command.Parameters.Add(new SqlParameter("@pic", DBNull.Value));
                command.Parameters.Add(new SqlParameter("@sp", GEditSingleCB.Checked));
                command.Parameters.Add(new SqlParameter("@mp", GEditMultiCB.Checked));
                command.Parameters.Add(new SqlParameter("@id", id));
                command.ExecuteNonQuery();
                command.CommandText = "DELETE FROM gamegenre WHERE game = @id";
                command.ExecuteNonQuery();
                if (GEditGenresCLB.CheckedItems.Count > 0)
                {
                    string query = "INSERT INTO gamegenre (game, genre) VALUES";
                    command.CommandText = query + GenreValues(GEditGenresCLB, command);
                    command.ExecuteNonQuery();
                }
            }
            GEditPicID.Text = "";
            UpdateData(GamesPanel, GamesDGVQuery, new ComboBox[] { GEditID }, "id");
        }

        // ----------------------- Удаление информации об играх -----------------------
        private void GEditDelDevBtn_Click(object sender, EventArgs e)
        {
            if (Voice.Ask(Resources.Lang.DeleteDeveloperPrompt) == DialogResult.No) return;
            if (!int.TryParse(GEditDevID.Text, out int id)) return;
            string query = "UPDATE games SET madeby = null WHERE madeby = @id";
            using (SqlConnection conn = new SqlConnection(LoginForm.CS))
            {
                if (!ConnOpen(conn)) return;
                SqlCommand command = conn.CreateCommand();
                command.CommandText = query;
                command.Parameters.Add(new SqlParameter("@id", id));
                command.ExecuteNonQuery();
                command.CommandText = "DELETE FROM devs WHERE devid = @id";
                command.ExecuteNonQuery();
            }
            GEditDev.Text = "";
            UpdateBox(GEditDev.Items, "devname", "devs", "devid");
            UpdateData(GamesPanel, GamesDGVQuery, new ComboBox[] { GEditID }, "id");
        }

        private void GEditDelGenreBtn_Click(object sender, EventArgs e)
        {
            if (GEditGenresCLB.SelectedItem is null)
            {
                Voice.Say(Resources.Lang.SelectAGenreToDelete);
                return;
            }
            if (Voice.Ask(Resources.Lang.DeleteGenrePrompt) == DialogResult.No) return;
            using (SqlConnection conn = new SqlConnection(LoginForm.CS))
            {
                if (!ConnOpen(conn)) return;
                SqlCommand command = conn.CreateCommand();
                command.CommandText = 
                    "SELECT TOP 1 genreid FROM genres WHERE genrename = @name";
                command.Parameters.Add(new SqlParameter
                    ("@name", GEditGenresCLB.SelectedItem.ToString()));
                int id = (int)command.ExecuteScalar();
                command.CommandText = "DELETE FROM gamegenre WHERE genre = @id";
                command.Parameters.Add(new SqlParameter("@id", id));
                command.ExecuteNonQuery();
                command.CommandText = "DELETE FROM genres WHERE genreid = @id";
                command.ExecuteNonQuery();
            }
            UpdateBox(GEditGenresCLB.Items, "genrename", "genres", "genreid");
            UpdateData(GamesPanel, GamesDGVQuery, new ComboBox[] { GEditID }, "id");
        }

        private void GEditDelPic_Click(object sender, EventArgs e)
        {
            if (Voice.Ask(Resources.Lang.DeleteImagePrompt) == DialogResult.No) return;
            if (!int.TryParse(GEditPicID.Text, out int id)) return;
            using (SqlConnection conn = new SqlConnection(LoginForm.CS))
            {
                if (!ConnOpen(conn)) return;
                SqlCommand command = conn.CreateCommand();
                command.CommandText = 
                    "UPDATE games SET gamepic = NULL WHERE gamepic = @id";
                command.Parameters.Add(new SqlParameter("@id", id));
                command.ExecuteNonQuery();
                command.CommandText = "DELETE FROM pics WHERE picid = @id";
                command.ExecuteNonQuery();
            }
            GEditPicID.Text = "";
            UpdateBox(GEditPicID.Items, "picid", "pics");
        }

        private void GEditDelSubmit_Click(object sender, EventArgs e)
        {
            if (Voice.Ask(Resources.Lang.DeleteGamePrompt) == DialogResult.No) return;
            if (!int.TryParse(GEditID.Text, out int id)) return;
            using (SqlConnection conn = new SqlConnection(LoginForm.CS))
            {
                if (!ConnOpen(conn)) return;
                SqlCommand command = conn.CreateCommand();
                command.CommandText = "DELETE FROM gamegenre WHERE game = @id";
                command.Parameters.Add(new SqlParameter("@id", id));
                command.ExecuteNonQuery();
                command.CommandText = "DELETE FROM subscriptions WHERE game = @id";
                command.ExecuteNonQuery();
                command.CommandText = "DELETE FROM games WHERE gameid = @id";
                command.ExecuteNonQuery();
            }
            GEditPicID.Text = "";
            UpdateData(GamesPanel, GamesDGVQuery, new ComboBox[] { GEditID }, "id");
        }

        // ------------------------- УЧЕТНЫЕ ЗАПИСИ -------------------------
        // ------------------------- Верхние кнопки -------------------------
        private void AxAllTab_Click(object sender, EventArgs e)
        {
            AxAddPanel.Visible = AxEditPanel.Visible = false;
            DGVAccounts.Visible = true;
            UpdateData(AccountsPanel, AccountsDGVQuery,
                new ComboBox[] { AxEditName }, "username");
        }

        private void AxAddTab_Click(object sender, EventArgs e)
        {
            DGVAccounts.Visible = AxEditPanel.Visible = false;
            AxAddPanel.Visible = true;
            UpdateBox(AxAddAuth.Items, "authname", "hierarchy", "authid");
        }

        private void AxEditTab_Click(object sender, EventArgs e)
        {
            DGVAccounts.Visible = AxAddPanel.Visible = false;
            AxEditPanel.Visible = true;
            UpdateBox(AxEditAuth.Items, "authname", "hierarchy", "authid");
        }

        // ----------------- Внутри вкладки "Добавить аккаунт" -----------------
        private void AxAddShowPasswd_CheckedChanged(object sender, EventArgs e) =>
            AxAddPasswd.PasswordChar = AxAddPasswd.PasswordChar == '*' ? '\0' : '*';

        private void AxAddSubmit_Click(object sender, EventArgs e)
        { // Добавить в базу новую учетную запись
            if (AxAddName.Text.Length == 0) return;
            using (SqlConnection conn = new SqlConnection(LoginForm.CS))
            {
                if (!ConnOpen(conn)) return;
                // Добавить аккаунт
                SqlCommand command = conn.CreateCommand();
                command.CommandText = "SELECT authid FROM hierarchy WHERE authname = @a";
                command.Parameters.Add(new SqlParameter("@a", AxAddAuth.Text));
                int? authid = (int?)command.ExecuteScalar();
                command.CommandText = "INSERT INTO users (username, email, info, " +
                    "authority, passwd) VALUES (@name, @email, @info, @auth, @passwd)";
                command.Parameters.Add(new SqlParameter("@name", AxAddName.Text));
                command.Parameters.Add(new SqlParameter("@email", AxAddEMail.Text));
                command.Parameters.Add(new SqlParameter("@info", AxAddInfo.Text));
                command.Parameters.Add(new SqlParameter("@auth", authid));
                command.Parameters.Add(new SqlParameter("@passwd", AxAddPasswd.Text));
                try { command.ExecuteNonQuery(); }
                catch (SqlException)
                {
                    Voice.Say(Resources.Lang.AccountFailMissingParamsOrNameAlreadyUsed);
                }
            }
            UpdateData(AccountsPanel, AccountsDGVQuery,
                new ComboBox[] { AxEditName }, "username");
        }

        // ----------------- Внутри вкладки "Редактировать аккаунт" -----------------
        private void DGVAccounts_CellDoubleClick
            (object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                AxEditTab_Click(sender, e);
                AxEditName.Text =
                    DGVAccounts.Rows[e.RowIndex].Cells["username"].Value.ToString();
            }
        }

        private void AxEditName_TextChanged(object sender, EventArgs e)
        { // Выбор редактируемой учетной записи
            if (AxEditName.Text.Length == 0)
            {
                AxEditEMail.Enabled = AxEditInfo.Enabled = false;
                AxEditAuth.Enabled = AxEditPasswd.Enabled = false;
                AxEditID.Text = AxEditSubsN.Text = AxEditRatesN.Text = 
                    AxEditMsgsN.Text = $"{0}";
                AxEditEMail.Text = AxEditInfo.Text = AxEditAuth.Text = AxEditPasswd.Text = "";
                return;
            }
            using (SqlConnection conn = new SqlConnection(LoginForm.CS))
            {
                if (!ConnOpen(conn)) return;
                SqlCommand command = conn.CreateCommand();
                command.CommandText = "SELECT TOP 1 userid, email, info, authname, " +
                    "passwd FROM users INNER JOIN hierarchy ON authority = authid " +
                    "WHERE username = @name GROUP BY userid, email, info, authname, passwd";
                command.Parameters.Add(new SqlParameter("@name", AxEditName.Text));
                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader.Read())
                {
                    AxEditID.Text = dataReader["userid"].ToString();
                    AxEditEMail.Text = dataReader["email"].ToString();
                    AxEditEMail.Enabled = true;
                    AxEditInfo.Text = dataReader["info"].ToString();
                    AxEditInfo.Enabled = true;
                    AxEditAuth.Text = dataReader["authname"].ToString();
                    AxEditAuth.Enabled = int.Parse(AxEditID.Text,
                        CultureInfo.InvariantCulture) != LoginForm.UserID;
                    AxEditPasswd.Text = dataReader["passwd"].ToString();
                    dataReader.Close();
                    AxEditPasswd.Enabled = true;
                    command.CommandText = "SELECT COUNT(game) AS subs " +
                        "FROM subscriptions WHERE who = @me";
                    command.Parameters.Add(
                        new SqlParameter("@me", int.Parse(AxEditID.Text,
                            CultureInfo.InvariantCulture)));
                    AxEditSubsN.Text = command.ExecuteScalar().ToString();
                    command.CommandText = "SELECT COUNT(rate) AS rates " +
                        "FROM subscriptions WHERE who = @me";
                    AxEditRatesN.Text = command.ExecuteScalar().ToString();
                    command.CommandText = "SELECT COUNT(messageid) AS msgs " +
                        "FROM feedback WHERE who = @me";
                    AxEditMsgsN.Text = command.ExecuteScalar().ToString();
                }
            }
            AxEditDelSubmit.Enabled = int.Parse(AxEditID.Text,
                CultureInfo.InvariantCulture) != LoginForm.UserID;
        }
        private void AxEditShowPasswd_CheckedChanged(object sender, EventArgs e) =>
            AxEditPasswd.PasswordChar = AxEditPasswd.PasswordChar == '*' ? '\0' : '*';

        private void AxEditSubmit_Click(object sender, EventArgs e)
        { // Обновить запись пользователя
            if (Voice.Ask(Resources.Lang.UpdateAccountPrompt) == DialogResult.No) return;
            if (!int.TryParse(AxEditID.Text, out int id)) return;
            string query = "UPDATE users SET username = @name, email = @email, " +
                "info = @info, authority = @auth, passwd = @pwd WHERE userid = @id";
            using (SqlConnection conn = new SqlConnection(LoginForm.CS))
            {
                if (!ConnOpen(conn)) return;
                SqlCommand command = conn.CreateCommand();
                command.CommandText = query;
                command.Parameters.Add(new SqlParameter("@name", AxEditName.Text));
                command.Parameters.Add(new SqlParameter("@email", AxEditEMail.Text));
                command.Parameters.Add(new SqlParameter("@info", AxEditInfo.Text));
                command.Parameters.Add(new SqlParameter("@auth", AxEditAuth.SelectedIndex+1));
                command.Parameters.Add(new SqlParameter("@pwd", AxEditPasswd.Text));
                command.Parameters.Add(new SqlParameter("@id", id));
                try { command.ExecuteNonQuery(); }
                catch (SqlException)
                {
                    Voice.Say(Resources.Lang.AccountFailMissingParamsOrNameAlreadyUsed);
                }
            }
            UpdateData(AccountsPanel, AccountsDGVQuery,
                new ComboBox[] { AxEditName }, "username");
        }

        private void AxEditDelSubmit_Click(object sender, EventArgs e)
        { // Удалить аккаунт
            if (Voice.Ask(Resources.Lang.DeleteAccountPrompt) == DialogResult.No) return;
            if (!int.TryParse(AxEditID.Text, out int id)) return;
            using (SqlConnection conn = new SqlConnection(LoginForm.CS))
            {
                if (!ConnOpen(conn)) return;
                SqlCommand command = conn.CreateCommand();
                command.CommandText = "DELETE FROM subscriptions WHERE who = @id";
                command.Parameters.Add(new SqlParameter("@id", id));
                command.ExecuteNonQuery();
                command.CommandText = "UPDATE feedback SET who = NULL WHERE who = @id";
                command.ExecuteNonQuery();
                command.CommandText = "DELETE FROM users WHERE userid = @id";
                command.ExecuteNonQuery();
            }
            UpdateData(AccountsPanel, AccountsDGVQuery,
                new ComboBox[] { AxEditName }, "username");
        }

        // ----------------------- Обратная связь -----------------------
        private void DGVMessages_CellClick(object sender, DataGridViewCellEventArgs e) =>
            DGVMessages.Rows[e.RowIndex].Selected = true;

        private void DGVMessages_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DGVMessages.Visible)
            {
                if (DGVMessages.Rows.Count <= 1) return;
                DGVMessages.Visible = false;
                MessagesReader.Visible = true;
                MsgsFrom.Text =
                    DGVMessages.Rows[e.RowIndex].Cells["sender"].Value.ToString();
                MsgsBriefly.Text =
                    DGVMessages.Rows[e.RowIndex].Cells["briefly"].Value.ToString();
                MsgsTime.Text =
                    DGVMessages.Rows[e.RowIndex].Cells["senddate"].Value.ToString();
                MsgsID.Text =
                    DGVMessages.Rows[e.RowIndex].Cells["id"].Value.ToString();
                MsgsSwitch.Text = Resources.Lang.Back;
                MsgsIsRead.Checked = MsgsIsRead.Visible = true;
                using (SqlConnection conn = new SqlConnection(LoginForm.CS))
                {
                    if (!ConnOpen(conn)) return;
                    SqlCommand command = conn.CreateCommand();
                    command.CommandText = "SELECT TOP 1 indetails FROM feedback " +
                    "WHERE messageid = @id";
                    command.Parameters.Add(new SqlParameter("@id",
                        (int)DGVMessages.Rows[e.RowIndex].Cells["id"].Value));
                    MsgsDetails.Text = command.ExecuteScalar().ToString();
                    command.CommandText = "UPDATE feedback SET isread = @isread " +
                    "WHERE messageid = @id";
                    command.Parameters.Add(new SqlParameter("@isread", true));
                    command.ExecuteNonQuery();
                }
                return;
            }
            MsgsSwitch.Text = Resources.Lang.Open;
            MsgsIsRead.Visible = MessagesReader.Visible = false;
            DGVMessages.Visible = true;
            UpdateTable(DGVMessages, MessagesDGVQuery);
            foreach (DataGridViewRow i in DGVMessages.Rows)
                if (!(i.Cells["isread"].Value is null || (bool)i.Cells["isread"].Value))
                    i.DefaultCellStyle.Font = new Font(DGVMessages.Font, FontStyle.Bold);
        }

        private void MsgsSwitch_Click(object sender, EventArgs e) =>
            DGVMessages_CellDoubleClick(sender, new DataGridViewCellEventArgs
                (DGVMessages.SelectedCells[0].ColumnIndex, 
                DGVMessages.SelectedCells[0].RowIndex));

        private void MsgsIsRead_CheckedChanged(object sender, EventArgs e)
        {
            string query = "UPDATE feedback SET isread = @isread WHERE messageid = @id";
            using (SqlConnection conn = new SqlConnection(LoginForm.CS))
            {
                if (!ConnOpen(conn)) return;
                SqlCommand command = conn.CreateCommand();
                command.CommandText = query;
                command.Parameters.Add(new SqlParameter("@isread", MsgsIsRead.Checked));
                command.Parameters.Add(new SqlParameter("@id", int.Parse(MsgsID.Text,
                    CultureInfo.InvariantCulture)));
                command.ExecuteNonQuery();
            }
        }

        // -------------------------- Обновление данных --------------------------
        private static string GamesDGVQuery => "SELECT gameid AS id, " +
            "gamename AS name, devname AS dev, singleplayer AS sngl, multiplayer AS " +
            "mlt, COUNT(who) AS subs, CONVERT(varchar, ROUND(AVG(CAST(rate AS float))," +
            " 2)) + ' (' + CONVERT(varchar, COUNT(rate)) + ')' AS rating FROM (games " +
            "LEFT JOIN subscriptions ON gameid = game) LEFT JOIN devs ON madeby = " +
            "devid GROUP BY singleplayer, multiplayer, devname, gamename, gameid";
        private static string AccountsDGVQuery => "SELECT userid AS id, " +
            "username, email, info, authname AS auth FROM ((users LEFT JOIN " +
            "hierarchy ON authority = authid) LEFT JOIN subscriptions on " +
            "userid = who) LEFT JOIN feedback ON userid = feedback.who " +
            "GROUP BY userid, username, email, info, authname";
        private static string MessagesDGVQuery => "SELECT messageid AS id, " +
            "username AS sender, briefly, dt AS senddate, isread FROM feedback " +
            "LEFT JOIN users ON who = userid ORDER BY dt DESC";

        /// <summary>
        /// Очищает текстбоксы и чекбоксы, обновляет dataGridView и данные 
        /// в элементах "поле со списком" внутри элемента panel.
        /// </summary>
        private static void UpdateData
            (Panel panel, string DGVQuery, ComboBox[] CB, params string[] fields)
        {
            foreach (Control el in panel.Controls.OfType<Panel>())
            {
                foreach (TextBox i in el.Controls.OfType<TextBox>()) i.Text = "";
                foreach (CheckBox i in el.Controls.OfType<CheckBox>()) i.Checked = false;
            }
            DataGridView DGV = panel.Controls.OfType<DataGridView>().First();
            if (!UpdateTable(DGV, DGVQuery)) return;
            if (CB is null || fields is null) return;
            // Заполнить списки
            int count = (CB.Length > fields.Length ? fields.Length : CB.Length);
            for (int i = 0; i < count; i++)
            {
                CB[i].Items.Clear();
                CB[i].Text = "";
            }
            for (int i = 1; i <= DGV.RowCount; i++)
                for (int j = 0; j < count; j++)
                    CB[j].Items.Add(DGV.Rows[i - 1].Cells[fields[j]].Value);
        }

        /// <summary>
        /// Обновляет данные в dataGridView.
        /// </summary>
        private static bool UpdateTable(DataGridView DGV, string query)
        {
            using (SqlConnection conn = new SqlConnection(LoginForm.CS))
            {
                if (!ConnOpen(conn)) return false;
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
                    using (DataSet dataSet = new DataSet())
                    {
                        dataAdapter.Fill(dataSet);
                        DGV.DataSource = dataSet.Tables[0];
                    }
                    DGV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
                    DGV.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    DGV.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            return true;
        }

        // ----------------------- Заимствования -----------------------
        private static bool UpdateBox(IList items,
            string select, string from, string order = "") =>
            LoginForm.UpdateBox(items, select, from, order);

        private static bool ConnOpen(SqlConnection conn) => LoginForm.ConnOpen(conn);

        private void PrintColors_CheckedChanged(object sender, EventArgs e)
        {
            PrintColors.Dispose();
            BackColor = Color.White;
            ForeColor = Color.Black;
            Opacity = 100;
            foreach (Button i in Controls.OfType<Button>())
            {
                i.FlatAppearance.BorderColor = Color.Black;
            }
            foreach (Panel i in Controls.OfType<Panel>())
            {
                foreach (DataGridView d in i.Controls.OfType<DataGridView>())
                    d.BackgroundColor = Color.White;
                foreach (Button j in i.Controls.OfType<Button>())
                    j.BackColor = Color.FromArgb(224, 224, 224);
                foreach (Panel p in i.Controls.OfType<Panel>())
                {
                    p.BackColor = Color.FromArgb(224, 224, 224);
                    foreach (Control j in p.Controls)
                    {
                        j.ForeColor = Color.Black;
                        j.BackColor = p.BackColor;
                    }
                    foreach (Button j in p.Controls.OfType<Button>())
                        j.FlatAppearance.BorderColor = Color.Black;
                    foreach (CheckBox j in p.Controls.OfType<CheckBox>())
                    {
                        j.FlatAppearance.CheckedBackColor = Color.White;
                    }
                }
            }
        }
    }
}
