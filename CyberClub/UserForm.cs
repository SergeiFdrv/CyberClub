using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
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
            LoadGameList();
            AppWide.UpdateBox(GSrchDev.Items, "devname", "devs", "devid");
            AppWide.UpdateBox(GSrchGenres.Items, "genrename", "genres", "genreid");
        }

        // ------------------------------ Кнопки слева ------------------------------
        private void LeftGames_CheckedChanged(object sender, EventArgs e)
        {
            LeftSettings.BackColor = BackColor;
            AccountPanel.Visible = MsgPanel.Visible = false;
            GamePanel.Visible = true;
        }

        private void LeftSettings_CheckedChanged(object sender, EventArgs e)
        {
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

        private static void RunApp(string path)
        { // Как из нашего приложения запустить стороннюю программу:
            // Шаг 1: подключить System.Diagnostics (см. выше)
            // Шаг 2:
            if (!(File.Exists(path) || 
                File.Exists(Environment.SystemDirectory + '\\' + path)))
            {
                Voice.Say(Resources.Lang.ExeNotFound);
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
            if (GameSubscribe.Checked)
            {
                GameSubscribe.Text = Resources.Lang.Unsubscribe;
                GameRunSubmit.Visible = GameRate.Visible = GameRateNUD.Visible = true;
                AppWide.Subscribe(LoginForm.UserID, int.Parse(GameID.Text,
                    CultureInfo.CurrentCulture));
                GameRate.Checked = false;
                GameRateNUD.Value = 5;
            }
            else if (Voice.Ask(Resources.Lang.UnsubscribePrompt) == DialogResult.No)
            {
                GameSubscribe.Checked = true;
                return;
            }
            else
            {
                GameSubscribe.Text = Resources.Lang.Subscribe;
                GameRunSubmit.Visible = GameRate.Visible = GameRateNUD.Visible = false;
                AppWide.Unsubscribe(LoginForm.UserID, int.Parse(GameID.Text,
                    CultureInfo.CurrentCulture));
            }
        }

        private void GameRate_Click(object sender, EventArgs e)
        {
            if (GameRate.Checked) AppWide.ChangeRate(
                int.Parse(GameID.Text, CultureInfo.CurrentCulture),
                LoginForm.UserID, GameRateNUD.Value);
            else AppWide.ChangeRate(
                int.Parse(GameID.Text, CultureInfo.CurrentCulture),
                LoginForm.UserID);
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
                Resources.Lang.SearchGames : Resources.Lang.MyGames;
            LoadGameList();
        }

        private void RefreshGamesEventHandler(object sender, EventArgs e) =>
            LoadGameList();

        private void GSrchGenres_ItemCheck(object sender, ItemCheckEventArgs e) =>
            BeginInvoke((MethodInvoker)(() => LoadGameList()));

        private void LoadGameList()
        {
            GamesList.Clear();
            AppWide.PopulateGameList(GamesList,
                GameSearch.Text,
                GSrchDev.Text,
                GSrchGenres.CheckedItems,
                GamePics,
                GamesSwitch.Checked,
                GSrchSingleCB.Checked,
                GSrchMultiCB.Checked,
                GGenreAndCB.Checked);
        }
        
        private void GamesList_Click(object sender, EventArgs e)
        {
            GameID.Text = GamesList.SelectedItems[0].ToolTipText;
            int id = int.Parse(GamesList.SelectedItems[0].ToolTipText,
                CultureInfo.CurrentCulture);
            var game = AppWide.SelectGame(id);
            GameName.Text = game["gamename"].ToString();
            GameDevName.Text = game["devname"].ToString();
            GameFilePath.Text = game["gamelink"].ToString();
            GameRating.Text = game["rating"].ToString();
            if (game["bin"] == DBNull.Value)
            {
                GamePicBox.Image = GamePics.Images[0];
            }
            else
            {
                MemoryStream memoryStream = new MemoryStream((byte[])game["bin"]);
                GamePicBox.Image = Image.FromStream(memoryStream);
                memoryStream.Dispose();
            }
            GameModes.Text = string.Empty;
            if ((bool)game["singleplayer"])
            {
                GameModes.Text = Resources.Lang.SinglePlayer;
                if ((bool)game["multiplayer"])
                    GameModes.Text += ", " + Resources.Lang.Multiplayer;
            }
            else if ((bool)game["multiplayer"])
                GameModes.Text = Resources.Lang.Multiplayer;
            AppWide.PopulateGenres(id, GameGenres);
            var subscriptions = AppWide.GetSubscription(LoginForm.UserID, id);
            GameSubscribe.Checked =
            GameRunSubmit.Visible =
            GameRate.Visible =
            GameRateNUD.Visible = subscriptions != null;
            if (GameSubscribe.Checked)
            {
                GameSubscribe.Text = Resources.Lang.Unsubscribe;
                GameID.Text = subscriptions["game"].ToString();
                GameRate.Checked = subscriptions["rate"] != DBNull.Value;
                if (GameRate.Checked)
                    GameRateNUD.Value = decimal.Parse(subscriptions["rate"].ToString(),
                        CultureInfo.CurrentCulture);
                else GameRateNUD.Value = 5;
            }
            else
            {
                GameSubscribe.Text = Resources.Lang.Subscribe;
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
            var user = AppWide.GetUserData(LoginForm.UserID);
            UserLabel.Text = UserName.Text = user["username"].ToString();
            EMail.Text = user["email"].ToString();
            UserInfo.Text = user["info"].ToString();
            Passwd.Text = user["passwd"].ToString();
        }

        private void AccSubmit_Click(object sender, EventArgs e)
        { // Обновить запись пользователя
            if (Passwd.Text != PasswdRepeat.Text ||
                Voice.Ask(Resources.Lang.UpdateAccountPrompt) == DialogResult.No) return;
            AppWide.UpdateAccount(
                LoginForm.UserID, UserName.Text, EMail.Text, UserInfo.Text, Passwd.Text);
            Voice.Say(Resources.Lang.UpdatedSuccessfully);
        }

        private void PasswdShow_CheckedChanged(object sender, EventArgs e) =>
            Passwd.PasswordChar = PasswdRepeat.PasswordChar = 
                Passwd.PasswordChar == '*' ? '\0' : '*';

        // ---------------------------- Обратная связь ----------------------------
        private void MsgSubmit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(MsgBriefly.Text))
            {
                Voice.Say(Resources.Lang.FillInNecessaryTextBox);
                return;
            }
            AppWide.SendMessage(LoginForm.UserID, MsgBriefly.Text, MsgDetails.Text);
            Voice.Say(Resources.Lang.SentSuccessfully);
        }

        // ----------------------- Заимствования -----------------------
        /// <summary>
        /// Change colors to bright to make the form printable.
        /// </summary>
        /// <remarks>
        /// This was a college study project and they needed the form printed on paper
        /// </remarks>
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
