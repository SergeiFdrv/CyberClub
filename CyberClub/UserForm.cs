using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CyberClub
{
    public partial class UserForm : Form
    {
        public UserForm()
        {
            InitializeComponent();
        }

        private void LogOutButton_Click(object sender, EventArgs e) => Close();

        private void UserForm_FormClosed(object sender, FormClosedEventArgs e) =>
            Owner.Show();

        private void UserForm_Load(object sender, EventArgs e)
        {
            GetUserData();
            LoadGameList(GamesList, GamesSwitch);
            UpdateBox(GSrchDev.Items, "devname", "devs", "devid");
            UpdateBox(GSrchGenres.Items, "genrename", "genres", "genreid");
        }

        // ------------------------------ Кнопки слева ------------------------------
        private void LeftGames_Click(object sender, EventArgs e)
        {
            LeftGames.BackColor = Color.FromArgb(64, 128, 0);
            LeftSettings.BackColor = BackColor;
            AccountPanel.Visible = MsgPanel.Visible = false;
            GamePanel.Visible = true;
        }

        private void LeftSettings_Click(object sender, EventArgs e)
        {
            LeftGames.BackColor = BackColor;
            LeftSettings.BackColor = Color.FromArgb(64, 128, 0);
            GamePanel.Visible = false;
            if (GamePagePanel.Visible)
            {
                GamePagePanel.Visible = false;
                GamePanel.Height += GamePagePanel.Height + 6;
            }
            AccountPanel.Visible = MsgPanel.Visible = true;
        }

        // ------------------------------ ИГРЫ ------------------------------
        // ----------------------- Информация об игре -----------------------
        private void GameRunSubmit_Click(object sender, EventArgs e) =>
            RunApp(GameFilePath.Text);

        private void RunApp(string path)
        { // Как из нашего приложения запустить стороннюю программу:
            // Шаг 1: подключить System.Diagnostics (см. выше)
            // Шаг 2:
            if (!(File.Exists(path) || 
                File.Exists(Environment.SystemDirectory + '\\' + path)))
            {
                Voice.Say(Properties.Resources.ExeNotFound);
                return;
            }
            Process.Start(new ProcessStartInfo
            {
                WorkingDirectory =
                path.Contains('\\') ? path.Substring(0, path.LastIndexOf('\\')) : "",
                FileName = path.Contains('\\') ? 
                path.Substring(path.LastIndexOf('\\') + 1) : path
            });
        }

        private void GameSubscribe_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(LoginForm.CS))
            {
                if (!ConnOpen(conn)) return;
                SqlCommand command = conn.CreateCommand();
                command.Parameters.Add(new SqlParameter("@me", LoginForm.UserID));
                command.Parameters.Add(new SqlParameter("@id", int.Parse(GameID.Text)));
                if (GameSubscribe.Checked)
                {
                    GameSubscribe.Text = Properties.Resources.Unsubscribe;
                    GameRunSubmit.Visible = GameRate.Visible = GameRateNUD.Visible = true;
                    command.CommandText = "INSERT INTO subscriptions (who, game) VALUES (@me, @id)";
                    command.ExecuteNonQuery();
                    GameRate.Checked = false;
                    GameRateNUD.Value = 5;
                }
                else if (Voice.Ask(Properties.Resources.UnsubscribePrompt) == DialogResult.No)
                {
                    GameSubscribe.Checked = true;
                    return;
                }
                else
                {
                    GameSubscribe.Text = Properties.Resources.Subscribe;
                    GameRunSubmit.Visible = GameRate.Visible = GameRateNUD.Visible = false;
                    command.CommandText = "DELETE FROM subscriptions WHERE who = @me AND game = @id";
                    command.ExecuteNonQuery();
                }
            }
        }

        private void GameRate_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(LoginForm.CS))
            {
                if (!ConnOpen(conn)) return;
                SqlCommand command = conn.CreateCommand();
                command.CommandText = "UPDATE subscriptions SET rate = @rate " +
                    "WHERE who = @me AND game = @game";
                command.Parameters.Add(
                    new SqlParameter("@game", int.Parse(GameID.Text)));
                command.Parameters.Add(new SqlParameter("@me", LoginForm.UserID));
                if (GameRate.Checked)
                    command.Parameters.Add(new SqlParameter("@rate", GameRateNUD.Value));
                else command.Parameters.Add(new SqlParameter("@rate", DBNull.Value));
                command.ExecuteNonQuery();
            }
        }

        private void GamePageClose_Click(object sender, EventArgs e)
        {
            GamePagePanel.Visible = false;
            GamePanel.Height += GamePagePanel.Height + 6;
        }

        // ----------------------- Поиск по играм -----------------------
        private void GamesSwitch_CheckedChanged(object sender, EventArgs e)
        {
            GamesSwitch.Text = GamesSwitch.Checked ?
                Properties.Resources.SearchGames : Properties.Resources.MyGames;
            LoadGameList(GamesList, GamesSwitch);
        }

        private void GameSearch_TextChanged(object sender, EventArgs e) =>
            LoadGameList(GamesList, GamesSwitch);

        private void GSrchDev_TextChanged(object sender, EventArgs e) =>
            LoadGameList(GamesList, GamesSwitch);

        private void GSrchSingleCB_CheckedChanged(object sender, EventArgs e) =>
            LoadGameList(GamesList, GamesSwitch);

        private void GSrchMultiCB_CheckedChanged(object sender, EventArgs e) =>
            LoadGameList(GamesList, GamesSwitch);

        private void GSrchGenres_ItemCheck(object sender, ItemCheckEventArgs e) =>
            BeginInvoke((MethodInvoker)(() => LoadGameList(GamesList, GamesSwitch)));

        /// <summary>
        /// Обновить элемент ListView с играми. 
        /// Встроен поиск по жанрам, разработчикам, режимам игры и по подпискам
        /// </summary>
        /// <param name="Switch">Если отмечен, искать только подписки</param>
        private void LoadGameList(ListView LV, CheckBox Switch)
        { // Если отмечены жанры или разработчик, включить их в критерии поиска
            bool genres = GSrchGenres.CheckedItems.Count > 0;
            bool dev = GSrchDev.Text.Length > 0;
            using (SqlConnection conn = new SqlConnection(LoginForm.CS))
            {
                if (!ConnOpen(conn)) return;
                string query = "SELECT DISTINCT gameid, gamename, devname, " +
                    "singleplayer, multiplayer, picname, bin FROM games " +
                    "LEFT JOIN pics ON gamepic = picid " + (dev ? " INNER" : " LEFT") + 
                    " JOIN devs ON madeby = devid" + (genres ? " INNER JOIN " +
                    "(gamegenre LEFT JOIN genres ON genre = genreid) ON " +
                    "gameid = game" : "") + (Switch.Checked ? "" :
                    " INNER JOIN subscriptions ON games.gameid = subscriptions.game") +
                    " WHERE gamelink != ''" + (GameSearch.Text.Length == 0 ? 
                    "" : " AND gamename LIKE @name") + (Switch.Checked ? 
                    "" : " AND who = @id") + (GSrchSingleCB.Checked ? 
                    " AND singleplayer = 1" : "") + (GSrchMultiCB.Checked ? 
                    " AND multiplayer = 1" : "") + (dev ? " AND devname LIKE @dev" : "");
                if (genres)
                {
                    query += " AND (";
                    for (int i = 0; i < GSrchGenres.CheckedItems.Count; i++)
                        query += $" genrename = '{GSrchGenres.CheckedItems[i]}' OR";
                    query = query.Substring(0, query.Length - 3) + ')';
                }
                LV.Clear();
                SqlCommand command = conn.CreateCommand();
                command.CommandText = query;
                command.Parameters.Add(
                    new SqlParameter("@name", '%' + GameSearch.Text + '%'));
                command.Parameters.Add(new SqlParameter("@id", LoginForm.UserID));
                command.Parameters.Add(
                    new SqlParameter("@dev", '%' + GSrchDev.Text + '%'));
                SqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    string item = dataReader["gamename"].ToString() + " (";
                    if (dataReader["devname"].ToString().Length > 0)
                    {
                        item += dataReader["devname"];
                        if ((bool)dataReader["singleplayer"]) item += ", singleplayer";
                        if ((bool)dataReader["multiplayer"]) item += ", multiplayer";
                    }
                    LV.Items.Add(new ListViewItem
                    {
                        Text = item + ')',
                        ToolTipText = dataReader["gameid"].ToString()
                    });
                    if (dataReader["bin"] == DBNull.Value)
                    {
                        LV.Items[LV.Items.Count - 1].ImageIndex = 0;
                    }
                    else
                    {
                        GamePics.Images.Add(dataReader["picname"].ToString(), 
                            Image.FromStream(
                                new MemoryStream((byte[])dataReader["bin"])));
                        LV.Items[LV.Items.Count - 1].ImageIndex = 
                            GamePics.Images.Count - 1;
                    }
                }
            }
        }

        private void GamesList_Click(object sender, EventArgs e)
        {
            GameName.Text = GamesList.SelectedItems[0].Text;
            using (SqlConnection conn = new SqlConnection(LoginForm.CS))
            {
                if (!ConnOpen(conn)) return;
                SqlCommand command = conn.CreateCommand();
                command.CommandText = "SELECT gamename, devname, singleplayer, " +
                    "multiplayer, gamelink, bin, CONVERT(varchar, " +
                    "ROUND(AVG(CAST(rate AS float)), 2)) + ' (' + CONVERT(varchar, " +
                    "COUNT(rate)) + ')' AS rating FROM games LEFT JOIN pics ON " +
                    "gamepic = picid LEFT JOIN devs ON madeby = devid LEFT JOIN " +
                    "subscriptions ON gameid = game WHERE gameid = @id GROUP BY " +
                    "gamename, devname, singleplayer, multiplayer, gamelink, bin";
                string id = GamesList.SelectedItems[0].ToolTipText;
                command.Parameters.Add(new SqlParameter("@id", int.Parse(id)));
                GameID.Text = id;
                SqlDataReader dataReader = command.ExecuteReader();
                dataReader.Read();
                GameName.Text = dataReader["gamename"].ToString();
                GameDevName.Text = dataReader["devname"].ToString();
                GameFilePath.Text = dataReader["gamelink"].ToString();
                GameRating.Text = dataReader["rating"].ToString();
                GamePicBox.Image = dataReader["bin"] == DBNull.Value ?
                    GamePics.Images[0] :
                    Image.FromStream(new MemoryStream((byte[])dataReader["bin"]));
                GameModes.Text = "";
                if ((bool)dataReader["singleplayer"])
                {
                    GameModes.Text = Properties.Resources.SinglePlayer;
                    if ((bool)dataReader["multiplayer"])
                        GameModes.Text += ", мультиплеер";
                }
                else if ((bool)dataReader["multiplayer"])
                    GameModes.Text = Properties.Resources.Multiplayer;
                dataReader.Close();
                command.CommandText = "SELECT genrename FROM gamegenre " +
                    "INNER JOIN genres ON genre = genreid WHERE game = @id";
                dataReader = command.ExecuteReader();
                GameGenres.Text = "";
                while (dataReader.Read())
                    GameGenres.Text += dataReader["genrename"].ToString() + ", ";
                dataReader.Close();
                if (GameGenres.Text.Length > 0) GameGenres.Text = 
                    GameGenres.Text.Substring(0, GameGenres.Text.LastIndexOf(','));
                command.CommandText = "SELECT * FROM subscriptions WHERE who = @me " +
                    "AND game = @id";
                command.Parameters.Add(new SqlParameter("@me", LoginForm.UserID));
                dataReader = command.ExecuteReader();
                if (GameSubscribe.Checked = dataReader.Read())
                {
                    GameSubscribe.Text = Properties.Resources.Unsubscribe;
                    GameRunSubmit.Visible = GameRate.Visible = GameRateNUD.Visible = true;
                    GameID.Text = dataReader["game"].ToString();
                    if (GameRate.Checked = dataReader["rate"] != DBNull.Value)
                        GameRateNUD.Value = (int)dataReader["rate"];
                    else GameRateNUD.Value = 5;
                }
                else
                {
                    GameSubscribe.Text = Properties.Resources.Subscribe;
                    GameRunSubmit.Visible = GameRate.Visible = GameRateNUD.Visible = false;
                }
            }
            if (!GamePagePanel.Visible)
            {
                GamePanel.Height -= GamePagePanel.Height + 6;
                GamePagePanel.Visible = true;
            }
        }

        // ------------------------------ Аккаунт ------------------------------
        private void GetUserData()
        {
            using (SqlConnection conn = new SqlConnection(LoginForm.CS))
            {
                if (!ConnOpen(conn)) return;
                SqlCommand command = conn.CreateCommand();
                command.CommandText = "SELECT username, email, info, passwd, authname " +
                    "FROM users INNER JOIN hierarchy ON authority = authid WHERE userid = @id";
                command.Parameters.Add(new SqlParameter("@id", LoginForm.UserID));
                SqlDataReader dataReader = command.ExecuteReader();
                dataReader.Read();
                UserLabel.Text = UserName.Text = dataReader["username"].ToString();
                EMail.Text = dataReader["email"].ToString();
                UserInfo.Text = dataReader["info"].ToString();
                Passwd.Text = dataReader["passwd"].ToString();
            }
        }

        private void AccSubmit_Click(object sender, EventArgs e)
        { // Обновить запись пользователя
            if (Passwd.Text != PasswdRepeat.Text ||
                Voice.Ask(Properties.Resources.UpdateAccountPrompt) == DialogResult.No) return;
            using (SqlConnection conn = new SqlConnection(LoginForm.CS))
            {
                if (!ConnOpen(conn)) return;
                SqlCommand command = conn.CreateCommand();
                command.CommandText = "UPDATE users SET " +
                    (UserName.Text.Length == 0 ? "" : "username = @name, ") +
                    "email = @email, info = @info, passwd = @pwd WHERE userid = @id";
                command.Parameters.Add(new SqlParameter("@name", UserName.Text));
                command.Parameters.Add(new SqlParameter("@email", EMail.Text));
                command.Parameters.Add(new SqlParameter("@info", UserInfo.Text));
                command.Parameters.Add(new SqlParameter("@pwd", Passwd.Text));
                command.Parameters.Add(new SqlParameter("@id", LoginForm.UserID));
                command.ExecuteNonQuery();
            }
            Voice.Say(Properties.Resources.UpdatedSuccessfully);
        }

        private void PasswdShow_CheckedChanged(object sender, EventArgs e) =>
            Passwd.PasswordChar = PasswdRepeat.PasswordChar = 
                Passwd.PasswordChar == '*' ? '\0' : '*';

        // ---------------------------- Обратная связь ----------------------------
        private void MsgSubmit_Click(object sender, EventArgs e)
        {
            if (MsgBriefly.Text.Length == 0)
            {
                Voice.Say(Properties.Resources.FillInNecessaryTextBox);
                return;
            }
            using (SqlConnection conn = new SqlConnection(LoginForm.CS))
            {
                if (!ConnOpen(conn)) return;
                SqlCommand command = conn.CreateCommand();
                command.CommandText = "INSERT INTO feedback (who, briefly, indetails, " +
                    "dt, isread) VALUES (@me, @br, @de, @dt, @rd)";
                command.Parameters.Add(new SqlParameter("@me", LoginForm.UserID));
                command.Parameters.Add(new SqlParameter("@br", MsgBriefly.Text));
                command.Parameters.Add(new SqlParameter("@de", MsgDetails.Text));
                command.Parameters.Add(new SqlParameter("@dt", DateTime.Now));
                command.Parameters.Add(new SqlParameter("@rd", false));
                command.ExecuteNonQuery();
            }
            Voice.Say(Properties.Resources.SentSuccessfully);
        }

        // ----------------------- Заимствования -----------------------
        private bool UpdateBox
            (IList items, string select, string from, string order = "") =>
            LoginForm.UpdateBox(items, select, from, order);

        private bool ConnOpen(SqlConnection conn) => LoginForm.ConnOpen(conn);

        private void PrintColors_CheckedChanged(object sender, EventArgs e)
        {
            PrintColors.Dispose();
            BackColor = Color.White;
            ForeColor = Color.Black;
            Opacity = 100;
            var buttons = Controls.OfType<Button>();
            foreach (Button i in buttons)
            {
                i.FlatAppearance.BorderColor = Color.Black;
            }
            foreach (Panel i in FlowLayout.Controls)
            {
                i.BackColor = Color.FromArgb(224, 224, 224);
                foreach (Control j in i.Controls)
                {
                    j.ForeColor = Color.Black;
                    if (j.BackColor == Color.FromArgb(64, 128, 0) ||
                        j.BackColor == Color.FromArgb(32, 96, 0) ||
                        j.BackColor == Color.FromArgb(12, 48, 24) ||
                        j.BackColor == Color.FromArgb(0, 16, 32))
                        j.BackColor = Color.FromArgb(200, 200, 200);
                }
                foreach (RichTextBox j in i.Controls.OfType<RichTextBox>())
                    j.BackColor = Color.FromArgb(200, 200, 200);
                foreach (CheckBox j in i.Controls.OfType<CheckBox>())
                    j.FlatAppearance.CheckedBackColor = Color.White;
            }
        }
    }
}
