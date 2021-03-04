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
using CyberClub.Data;

namespace CyberClub
{
    public partial class AdminForm : Form
    {
        public AdminForm()
        {
            InitializeComponent();
        }

        private AdminDatabase DB { get; } = new AdminDatabase();

        #region Form     // Загрузка и выгрузка формы

        private void AdminForm_Load(object sender, EventArgs e)
        {
            UserLabel.Text = DB.GetUserName(LoginForm.UserID);
            LeftGames.Checked = true;
        }

        private void AdminForm_FormClosed(object sender, FormClosedEventArgs e) =>
            Owner.Show();
        #endregion

        #region LeftMenu // Кнопки левого бокового меню
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

        private void LeftGoAsUser_Click(object sender, EventArgs e)
        {
            if (LeftGoAsUser.Checked)
            {
                Hide();
                using (UserForm uf = new UserForm { Owner = this })
                    uf.ShowDialog();
            }
        }

        private void LogOutButton_CheckedChanged(object sender, EventArgs e) => Close();
        #endregion

        #region GAMES    // ИГРЫ
        #region Top      // Верхние кнопки
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
        #endregion

        #region Add      // Внутри вкладки "Добавить игру"
        private void GAddLinkBtn_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = Properties.Resources.FileFilterEXE;
                openFileDialog.ShowDialog();
                GAddLink.Text = openFileDialog.FileName;
            }
        }

        private void GAddNewDevBtn_Click(object sender, EventArgs e)
        {
            if (DB.AddGetDevID(GAddDev.Text) < 0) Voice.Say(Resources.Lang.AddError);
            GAddDev.Text = "";
            UpdateBox(GAddDev.Items, "devname", "devs", "devid");
        }

        private void GAddNewGenreBtn_Click(object sender, EventArgs e)
        { // Добавить в базу новый жанр
            byte addStatus = DB.AddGenre(GAddNewGenre.Text);
            switch (addStatus)
            {
                case 3:
                    Voice.Say(Resources.Lang.NameNotEntered); return;
                case 2:
                    Voice.Say(Resources.Lang.DBError); return;
                case 1:
                    Voice.Say(Resources.Lang.NameAlreadyUsed); return;
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
            Voice.Say(DB.AddGetPicID(GAddPicName.Text, GAddPicBox.Image) < 0 ? 
                Resources.Lang.NameNotEntered : Resources.Lang.AddedSuccessfully);

        private void GAddSubmit_Click(object sender, EventArgs e)
        { // Добавить в базу новую игру
            DB.AddGame(GAddName.Text, GAddDev.Text, GAddLink.Text,
                GAddPicName.Text, GAddPicBox.Image,
                GAddSingleCB.Checked, GAddMultiCB.Checked, GAddGenresCLB);
            UpdateBox(GAddDev.Items, "devname", "devs", "devid"); // перенести
            Voice.Say(Resources.Lang.GameAddedToDB);
            UpdateData(GamesPanel, GamesDGVQuery, new ComboBox[] { GEditID }, "id");
        }
        #endregion

        #region Edit     // Внутри вкладки "Изменить игру"
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
            var game = DB.GetGame(id);
            if (game != null)
            {
                GEditName.Text = game["GameName"].ToString();
                GEditLink.Text = game["GameExePath"].ToString();
                GEditDev.Text = game["DeveloperName"].ToString();
                GEditPicID.Text = game["GameIcon"].ToString();
                GEditSingleCB.Checked = (bool)game["IsSingleplayer"];
                GEditMultiCB.Checked = (bool)game["IsMultiplayer"];
                GEditSubsN.Text = game["subs"].ToString();
                GEditRatesN.Text = game["rates"].ToString();
                GEditRatingN.Text = game["rating"].ToString();
                for (int i = 0; i < GEditGenresCLB.Items.Count; i++)
                    GEditGenresCLB.SetItemChecked(i, false);
                var genres = DB.GetGameGenres(id);
                if (genres is null) return;
                foreach (string genre in genres)
                    GEditGenresCLB.SetItemChecked(
                        GEditGenresCLB.Items.IndexOf(genre), true);
            }
        }

        private void GEditLinkBtn_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = Properties.Resources.FileFilterEXE;
                openFileDialog.ShowDialog();
                GEditLink.Text = openFileDialog.FileName;
            }
        }

        private void GEditDev_TextChanged(object sender, EventArgs e)
        { // Выбор редактируемого имени разработчика
            if (GEditDev.Text.Length == 0)
            {
                GEditDevID.Text = string.Empty;
                return;
            }
            var dev = DB.GetDeveloper(GEditDev.Text);
            if (dev != null) GEditDevID.Text = dev["DeveloperID"].ToString();
        }

        private void GEditDevBtn_Click(object sender, EventArgs e)
        { // Переименовать разработчика
            if (Voice.Ask(Resources.Lang.RenameDeveloperPrompt) == DialogResult.No)
                return;
            if (!int.TryParse(GEditDevID.Text, out int id)) return;
            byte opRes = DB.RenameDeveloper(id, GEditDev.Text);
            if (opRes == 2)
            {
                Voice.Say(Resources.Lang.DBError);
                return;
            }
            else if (opRes == 1)
            {
                Voice.Say(Resources.Lang.NameAlreadyUsed);
                return;
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
            byte opRes = DB
                .RenameGenre(GEditGenresCLB.SelectedItem.ToString(), GEditGenre.Text);
            if (opRes == 2)
            {
                Voice.Say(Resources.Lang.DBError);
                return;
            }
            else if (opRes == 1)
            {
                Voice.Say(Resources.Lang.NameAlreadyUsed);
                return;
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
            var image = DB.GetImage(id);
            if (image != null)
            {
                GEditPicName.Text = image["picname"].ToString();
                using (MemoryStream memoryStream =
                    new MemoryStream((byte[])image["bin"]))
                {
                    GEditPicBox.Image = Image.FromStream(memoryStream);
                }
            }
        }

        private void GEditPicBtn_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(GEditPicID.Text, out int id)) return;
            DB.RenameImage(id, GEditPicName.Text);
            UpdateBox(GEditPicID.Items, "picid", "pics");
        }

        private void GEditSubmit_Click(object sender, EventArgs e)
        { // Обновить запись об игре
            if (Voice.Ask(Resources.Lang.UpdateGamePrompt) == DialogResult.No) return;
            if (!int.TryParse(GEditID.Text, out int id)) return;
            if (!int.TryParse(GEditDevID.Text, out int dev)) dev = -1;
            if (!int.TryParse(GEditPicID.Text, out int pic)) pic = -1;
            DB.UpdateGame(id, GEditName.Text, dev, GEditLink.Text, pic,
                GEditSingleCB.Checked, GEditMultiCB.Checked, GEditGenresCLB);
            GEditPicID.Text = "";
            UpdateData(GamesPanel, GamesDGVQuery, new ComboBox[] { GEditID }, "id");
        }
        #endregion

        #region Delete   // Удаление информации об играх
        private void GEditDelDevBtn_Click(object sender, EventArgs e)
        {
            if (Voice.Ask(Resources.Lang.DeleteDeveloperPrompt) == DialogResult.No) return;
            if (!int.TryParse(GEditDevID.Text, out int id)) return;
            DB.DeleteDeveloper(id);
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
            if (Voice.Ask(Resources.Lang.DeleteGenrePrompt + ' ' +
                GEditGenresCLB.SelectedItem.ToString()) == DialogResult.No) return;
            DB.DeleteGenre(GEditGenresCLB.SelectedItem.ToString());
            UpdateBox(GEditGenresCLB.Items, "genrename", "genres", "genreid");
            UpdateData(GamesPanel, GamesDGVQuery, new ComboBox[] { GEditID }, "id");
        }

        private void GEditDelPic_Click(object sender, EventArgs e)
        {
            if (Voice.Ask(Resources.Lang.DeleteImagePrompt) == DialogResult.No) return;
            if (!int.TryParse(GEditPicID.Text, out int id)) return;
            DB.DeleteImage(id);
            GEditPicID.Text = "";
            UpdateBox(GEditPicID.Items, "picid", "pics");
        }

        private void GEditDelSubmit_Click(object sender, EventArgs e)
        {
            if (Voice.Ask(Resources.Lang.DeleteGamePrompt) == DialogResult.No) return;
            if (!int.TryParse(GEditID.Text, out int id)) return;
            DB.DeleteGame(id);
            GEditPicID.Text = "";
            UpdateData(GamesPanel, GamesDGVQuery, new ComboBox[] { GEditID }, "id");
        }
        #endregion
        #endregion

        #region ACCOUNTS // УЧЕТНЫЕ ЗАПИСИ
        #region Top // ------------------------- Верхние кнопки -------------------------
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
        }

        private void AxEditTab_Click(object sender, EventArgs e)
        {
            DGVAccounts.Visible = AxAddPanel.Visible = false;
            AxEditPanel.Visible = true;
        }
        #endregion

        #region Add // ----------------- Внутри вкладки "Добавить аккаунт" -----------------
        private void AxAddShowPasswd_CheckedChanged(object sender, EventArgs e) =>
            AxAddPasswd.PasswordChar = AxAddPasswd.PasswordChar == '*' ? '\0' : '*';

        private void AxAddSubmit_Click(object sender, EventArgs e)
        { // Добавить в базу новую учетную запись
            if (string.IsNullOrWhiteSpace(AxAddName.Text)) return;
            byte addResult = DB.AddAccount(AxAddName.Text, (int)AxAddAuth.SelectedItem,
                AxAddEMail.Text, AxAddInfo.Text, AxAddPasswd.Text);
            switch (addResult)
            {
                case 2:
                    Voice.Say(Resources.Lang.DBError); return;
                case 1:
                    Voice.Say(Resources.Lang.AccountFailMissingParamsOrNameAlreadyUsed); return;
            }
            UpdateData(AccountsPanel, AccountsDGVQuery,
                new ComboBox[] { AxEditName }, "username");
        }
        #endregion

        #region Edit // ----------------- Внутри вкладки "Редактировать аккаунт" -----------------
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
            if (string.IsNullOrWhiteSpace(AxEditName.Text))
            {
                AxEditEMail.Enabled = AxEditInfo.Enabled = false;
                AxEditAuth.Enabled = AxEditPasswd.Enabled = false;
                AxEditID.Text = AxEditSubsN.Text = AxEditRatesN.Text = 
                    AxEditMsgsN.Text = $"{0}";
                AxEditEMail.Text = AxEditInfo.Text = AxEditAuth.Text = AxEditPasswd.Text = "";
                return;
            }
            var account = DB.GetAccountByName(AxEditName.Text);
            if (account != null)
            {
                AxEditID.Text = account["userid"].ToString();
                AxEditEMail.Text = account["email"].ToString();
                AxEditEMail.Enabled = true;
                AxEditInfo.Text = account["info"].ToString();
                AxEditInfo.Enabled = true;
                AxEditAuth.Text = ((UserLevel)account["userlevel"]).ToString();
                AxEditAuth.Enabled = int.Parse(AxEditID.Text,
                    CultureInfo.CurrentCulture) != LoginForm.UserID;
                AxEditPasswd.Text = account["passwd"].ToString();
                AxEditPasswd.Enabled = true;
                int id = (int)account["userid"];
                var stats = DB.GetAccountStats(id);
                AxEditSubsN.Text = stats["subs"].ToString();
                AxEditRatesN.Text = stats["rates"].ToString();
                AxEditMsgsN.Text = stats["messages"].ToString();
                AxEditDelSubmit.Enabled = id != LoginForm.UserID;
            }
        }

        private void AxEditShowPasswd_CheckedChanged(object sender, EventArgs e) =>
            AxEditPasswd.PasswordChar = AxEditPasswd.PasswordChar == '*' ? '\0' : '*';

        private void AxEditSubmit_Click(object sender, EventArgs e)
        { // Обновить запись пользователя
            if (Voice.Ask(Resources.Lang.UpdateAccountPrompt) == DialogResult.No) return;
            if (!int.TryParse(AxEditID.Text, out int id)) return;
            byte editResult = DB.UpdateAccount(id, AxEditName.Text, AxEditEMail.Text,
                AxEditInfo.Text, (int)AxEditAuth.SelectedItem, AxEditPasswd.Text);
            switch (editResult)
            {
                case 2:
                    Voice.Say(Resources.Lang.DBError); return;
                case 1:
                    Voice.Say(Resources.Lang.AccountFailMissingParamsOrNameAlreadyUsed); return;
            }
            UpdateData(AccountsPanel, AccountsDGVQuery,
                new ComboBox[] { AxEditName }, "username");
        }

        private void AxEditDelSubmit_Click(object sender, EventArgs e)
        { // Удалить аккаунт
            if (Voice.Ask(Resources.Lang.DeleteAccountPrompt) == DialogResult.No) return;
            if (!int.TryParse(AxEditID.Text, out int id)) return;
            DB.DeleteAccount(id);
            UpdateData(AccountsPanel, AccountsDGVQuery,
                new ComboBox[] { AxEditName }, "username");
        }
        #endregion
        #endregion

        #region Feedback // Обратная связь
        private void DGVMessages_CellClick(object sender, DataGridViewCellEventArgs e) =>
            DGVMessages.Rows[e.RowIndex].Selected = true;

        private void DGVMessages_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DGVMessages.Visible)
            {
                if (DGVMessages.Rows.Count <= 1) return;
                OpenMessage(e);
            }
            else CloseMessage();
        }

        private void OpenMessage(DataGridViewCellEventArgs e)
        {
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
            MsgsDetails.Text = DB
                .GetMessageText((int)DGVMessages.Rows[e.RowIndex].Cells["id"].Value);
        }

        private void CloseMessage()
        {
            MsgsSwitch.Text = Resources.Lang.Open;
            MsgsIsRead.Visible = MessagesReader.Visible = false;
            DGVMessages.Visible = true;
            UpdateTable(DGVMessages, MessagesDGVQuery);
            foreach (DataGridViewRow i in DGVMessages.Rows)
                if (!(i.Cells["isread"].Value is null || (bool)i.Cells["isread"].Value))
                    i.DefaultCellStyle.Font = new Font(DGVMessages.Font, FontStyle.Bold);
        }

        /// <summary>
        /// MsgsSwitch is a button. If you select an iten on the datagrid
        /// clicking it opens the selected item details.
        /// On the details tab it serves as a back button
        /// </summary>
        private void MsgsSwitch_Click(object sender, EventArgs e) =>
            DGVMessages_CellDoubleClick(sender, new DataGridViewCellEventArgs
                (DGVMessages.SelectedCells[0].ColumnIndex, 
                DGVMessages.SelectedCells[0].RowIndex));

        private void MsgsIsRead_CheckedChanged(object sender, EventArgs e)
        {
            DB.SetMessageIsRead(
                int.Parse(MsgsID.Text, CultureInfo.CurrentCulture),
                MsgsIsRead.Checked);
        }
        #endregion

        #region Update   // Обновление данных
        private static string GamesDGVQuery => "SELECT gameid AS id, " +
            "gamename AS name, devname AS dev, singleplayer AS sngl, multiplayer AS " +
            "mlt, COUNT(who) AS subs, CONVERT(varchar, ROUND(AVG(CAST(rate AS float))," +
            " 2)) + ' (' + CONVERT(varchar, COUNT(rate)) + ')' AS rating FROM (games " +
            "LEFT JOIN subscriptions ON gameid = game) LEFT JOIN devs ON madeby = " +
            "devid GROUP BY singleplayer, multiplayer, devname, gamename, gameid";
        private static string AccountsDGVQuery => "SELECT userid AS id, " +
            "username, email, info, userlevel AS level FROM users " +
            "LEFT JOIN subscriptions on userid = who) " +
            "LEFT JOIN feedback ON userid = feedback.who " +
            "GROUP BY userid, username, email, info, userlevel";
        private static string MessagesDGVQuery => "SELECT messageid AS id, " +
            "username AS sender, briefly, dt AS senddate, isread FROM feedback " +
            "LEFT JOIN users ON who = userid ORDER BY dt DESC";

        /// <summary>
        /// Очищает текстбоксы и чекбоксы, обновляет dataGridView и данные 
        /// в элементах "поле со списком" внутри элемента panel.
        /// </summary>
        private void UpdateData
            (Panel panel, string DGVQuery, ComboBox[] CB, params string[] fields)
        {
            ClearFields(panel);
            DataGridView DGV = panel.Controls.OfType<DataGridView>().First();
            if (!UpdateTable(DGV, DGVQuery)) return;
            PopulateLists(DGV, CB, fields);
        }

        /// <summary>
        /// Clear text boxes and checkboxes within the given Panel
        /// </summary>
        private static void ClearFields(Panel panel)
        {
            foreach (Control el in panel.Controls.OfType<Panel>())
            {
                foreach (TextBox i in el.Controls.OfType<TextBox>()) i.Text = "";
                foreach (CheckBox i in el.Controls.OfType<CheckBox>()) i.Checked = false;
            }
        }

        /// <summary>
        /// Обновляет данные в dataGridView.
        /// </summary>
        private bool UpdateTable(DataGridView DGV, string query)
        {
            var source = DB.GetDataTable(query);
            if (source is null) return false;
            DGV.DataSource = source;
            DGV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
            DGV.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            DGV.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            return true;
        }

        /// <summary>
        /// Update items in the comboboxes based on the given gata grid and fields
        /// </summary>
        private static void PopulateLists
            (DataGridView DGV, ComboBox[] comboBoxes, params string[] fields)
        {
            if (comboBoxes is null || fields is null) return;
            int count = comboBoxes.Length > fields.Length ? fields.Length : comboBoxes.Length;
            for (int i = 0; i < count; i++)
            {
                ref ComboBox comboBox = ref comboBoxes[i];
                comboBox.Items.Clear();
                comboBox.Text = "";
            }
            for (int i = 1; i <= DGV.RowCount; i++)
                for (int j = 0; j < count; j++)
                    comboBoxes[j].Items.Add(DGV.Rows[i - 1].Cells[fields[j]].Value);
        }
        #endregion

        // ----------------------- Заимствования -----------------------
        private bool UpdateBox(IList items,
            string select, string from, string order = "") =>
            DB.UpdateBox(items, select, from, order);

        /// <summary>
        /// Change colors to bright to make the form printable.
        /// This was a college study project and they needed the form printed on paper
        /// </summary>
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
