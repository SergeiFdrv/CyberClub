namespace CyberClub
{
    partial class UserForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserForm));
            this.LogOutButton = new System.Windows.Forms.Button();
            this.LeftSettings = new System.Windows.Forms.Button();
            this.LeftGames = new System.Windows.Forms.Button();
            this.AdminLabel = new System.Windows.Forms.Label();
            this.MsgPanel = new System.Windows.Forms.Panel();
            this.FeedbackLabel = new System.Windows.Forms.Label();
            this.MsgSubmit = new System.Windows.Forms.Button();
            this.MsgDetails = new System.Windows.Forms.RichTextBox();
            this.MsgBriefly = new System.Windows.Forms.RichTextBox();
            this.MsgDetailLabel = new System.Windows.Forms.Label();
            this.MsgBrieflyLabel = new System.Windows.Forms.Label();
            this.UserLabel = new System.Windows.Forms.Label();
            this.FlowLayout = new System.Windows.Forms.FlowLayoutPanel();
            this.GamePagePanel = new System.Windows.Forms.Panel();
            this.GameRating = new System.Windows.Forms.Label();
            this.GameID = new System.Windows.Forms.Label();
            this.GameSubscribe = new System.Windows.Forms.CheckBox();
            this.GameRate = new System.Windows.Forms.CheckBox();
            this.GameRateNUD = new System.Windows.Forms.NumericUpDown();
            this.GamePageClose = new System.Windows.Forms.Button();
            this.GameFilePath = new System.Windows.Forms.Label();
            this.GameModes = new System.Windows.Forms.Label();
            this.GameGenres = new System.Windows.Forms.Label();
            this.GameDevName = new System.Windows.Forms.Label();
            this.GamePicBox = new System.Windows.Forms.PictureBox();
            this.GameName = new System.Windows.Forms.Label();
            this.GameRunSubmit = new System.Windows.Forms.Button();
            this.GamePanel = new System.Windows.Forms.Panel();
            this.GSrchGenres = new System.Windows.Forms.CheckedListBox();
            this.GamesSwitch = new System.Windows.Forms.CheckBox();
            this.GamesList = new System.Windows.Forms.ListView();
            this.GamePics = new System.Windows.Forms.ImageList(this.components);
            this.GSrchMultiCB = new System.Windows.Forms.CheckBox();
            this.GamesLabel = new System.Windows.Forms.Label();
            this.GSrchSingleCB = new System.Windows.Forms.CheckBox();
            this.GameSearch = new System.Windows.Forms.RichTextBox();
            this.GSrchDevLabel = new System.Windows.Forms.Label();
            this.GSrchDev = new System.Windows.Forms.ComboBox();
            this.AccountPanel = new System.Windows.Forms.Panel();
            this.PasswdRepeatLabel = new System.Windows.Forms.Label();
            this.PasswdRepeat = new System.Windows.Forms.TextBox();
            this.Passwd = new System.Windows.Forms.TextBox();
            this.AccSetingsLabel = new System.Windows.Forms.Label();
            this.PasswdShow = new System.Windows.Forms.CheckBox();
            this.PasswdLabel = new System.Windows.Forms.Label();
            this.EMail = new System.Windows.Forms.RichTextBox();
            this.EMailLabel = new System.Windows.Forms.Label();
            this.AccSubmit = new System.Windows.Forms.Button();
            this.UserInfo = new System.Windows.Forms.RichTextBox();
            this.UserName = new System.Windows.Forms.RichTextBox();
            this.UserInfoLabel = new System.Windows.Forms.Label();
            this.UserNameLabel = new System.Windows.Forms.Label();
            this.PrintColors = new System.Windows.Forms.CheckBox();
            this.MsgPanel.SuspendLayout();
            this.FlowLayout.SuspendLayout();
            this.GamePagePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GameRateNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GamePicBox)).BeginInit();
            this.GamePanel.SuspendLayout();
            this.AccountPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // LogOutButton
            // 
            resources.ApplyResources(this.LogOutButton, "LogOutButton");
            this.LogOutButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.LogOutButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.LogOutButton.Name = "LogOutButton";
            this.LogOutButton.UseVisualStyleBackColor = true;
            this.LogOutButton.Click += new System.EventHandler(this.LogOutButton_Click);
            // 
            // LeftSettings
            // 
            this.LeftSettings.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.LeftSettings.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.LeftSettings, "LeftSettings");
            this.LeftSettings.Name = "LeftSettings";
            this.LeftSettings.UseVisualStyleBackColor = true;
            this.LeftSettings.Click += new System.EventHandler(this.LeftSettings_Click);
            // 
            // LeftGames
            // 
            this.LeftGames.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.LeftGames.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.LeftGames, "LeftGames");
            this.LeftGames.Name = "LeftGames";
            this.LeftGames.UseVisualStyleBackColor = true;
            this.LeftGames.Click += new System.EventHandler(this.LeftGames_Click);
            // 
            // AdminLabel
            // 
            resources.ApplyResources(this.AdminLabel, "AdminLabel");
            this.AdminLabel.Name = "AdminLabel";
            // 
            // MsgPanel
            // 
            resources.ApplyResources(this.MsgPanel, "MsgPanel");
            this.MsgPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(32)))), ((int)(((byte)(48)))));
            this.MsgPanel.Controls.Add(this.FeedbackLabel);
            this.MsgPanel.Controls.Add(this.MsgSubmit);
            this.MsgPanel.Controls.Add(this.MsgDetails);
            this.MsgPanel.Controls.Add(this.MsgBriefly);
            this.MsgPanel.Controls.Add(this.MsgDetailLabel);
            this.MsgPanel.Controls.Add(this.MsgBrieflyLabel);
            this.MsgPanel.Name = "MsgPanel";
            // 
            // FeedbackLabel
            // 
            resources.ApplyResources(this.FeedbackLabel, "FeedbackLabel");
            this.FeedbackLabel.Name = "FeedbackLabel";
            // 
            // MsgSubmit
            // 
            resources.ApplyResources(this.MsgSubmit, "MsgSubmit");
            this.MsgSubmit.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.MsgSubmit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.MsgSubmit.Name = "MsgSubmit";
            this.MsgSubmit.UseVisualStyleBackColor = true;
            this.MsgSubmit.Click += new System.EventHandler(this.MsgSubmit_Click);
            // 
            // MsgDetails
            // 
            resources.ApplyResources(this.MsgDetails, "MsgDetails");
            this.MsgDetails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(16)))), ((int)(((byte)(32)))));
            this.MsgDetails.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.MsgDetails.ForeColor = System.Drawing.Color.Lime;
            this.MsgDetails.Name = "MsgDetails";
            // 
            // MsgBriefly
            // 
            resources.ApplyResources(this.MsgBriefly, "MsgBriefly");
            this.MsgBriefly.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(16)))), ((int)(((byte)(32)))));
            this.MsgBriefly.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.MsgBriefly.ForeColor = System.Drawing.Color.Lime;
            this.MsgBriefly.Name = "MsgBriefly";
            // 
            // MsgDetailLabel
            // 
            resources.ApplyResources(this.MsgDetailLabel, "MsgDetailLabel");
            this.MsgDetailLabel.Name = "MsgDetailLabel";
            // 
            // MsgBrieflyLabel
            // 
            resources.ApplyResources(this.MsgBrieflyLabel, "MsgBrieflyLabel");
            this.MsgBrieflyLabel.Name = "MsgBrieflyLabel";
            // 
            // UserLabel
            // 
            resources.ApplyResources(this.UserLabel, "UserLabel");
            this.UserLabel.Name = "UserLabel";
            // 
            // FlowLayout
            // 
            resources.ApplyResources(this.FlowLayout, "FlowLayout");
            this.FlowLayout.Controls.Add(this.GamePagePanel);
            this.FlowLayout.Controls.Add(this.GamePanel);
            this.FlowLayout.Controls.Add(this.AccountPanel);
            this.FlowLayout.Controls.Add(this.MsgPanel);
            this.FlowLayout.Name = "FlowLayout";
            // 
            // GamePagePanel
            // 
            resources.ApplyResources(this.GamePagePanel, "GamePagePanel");
            this.GamePagePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.GamePagePanel.Controls.Add(this.GameRating);
            this.GamePagePanel.Controls.Add(this.GameID);
            this.GamePagePanel.Controls.Add(this.GameSubscribe);
            this.GamePagePanel.Controls.Add(this.GameRate);
            this.GamePagePanel.Controls.Add(this.GameRateNUD);
            this.GamePagePanel.Controls.Add(this.GamePageClose);
            this.GamePagePanel.Controls.Add(this.GameFilePath);
            this.GamePagePanel.Controls.Add(this.GameModes);
            this.GamePagePanel.Controls.Add(this.GameGenres);
            this.GamePagePanel.Controls.Add(this.GameDevName);
            this.GamePagePanel.Controls.Add(this.GamePicBox);
            this.GamePagePanel.Controls.Add(this.GameName);
            this.GamePagePanel.Controls.Add(this.GameRunSubmit);
            this.GamePagePanel.ForeColor = System.Drawing.Color.Black;
            this.GamePagePanel.Name = "GamePagePanel";
            // 
            // GameRating
            // 
            resources.ApplyResources(this.GameRating, "GameRating");
            this.GameRating.ForeColor = System.Drawing.Color.DimGray;
            this.GameRating.Name = "GameRating";
            // 
            // GameID
            // 
            resources.ApplyResources(this.GameID, "GameID");
            this.GameID.ForeColor = System.Drawing.Color.DimGray;
            this.GameID.Name = "GameID";
            // 
            // GameSubscribe
            // 
            resources.ApplyResources(this.GameSubscribe, "GameSubscribe");
            this.GameSubscribe.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.GameSubscribe.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.GameSubscribe.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.GameSubscribe.ForeColor = System.Drawing.Color.Black;
            this.GameSubscribe.Name = "GameSubscribe";
            this.GameSubscribe.UseVisualStyleBackColor = false;
            this.GameSubscribe.Click += new System.EventHandler(this.GameSubscribe_Click);
            // 
            // GameRate
            // 
            resources.ApplyResources(this.GameRate, "GameRate");
            this.GameRate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.GameRate.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.GameRate.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.GameRate.ForeColor = System.Drawing.Color.Black;
            this.GameRate.Name = "GameRate";
            this.GameRate.UseVisualStyleBackColor = false;
            this.GameRate.Click += new System.EventHandler(this.GameRate_Click);
            // 
            // GameRateNUD
            // 
            this.GameRateNUD.BackColor = System.Drawing.Color.White;
            this.GameRateNUD.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.GameRateNUD.ForeColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.GameRateNUD, "GameRateNUD");
            this.GameRateNUD.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.GameRateNUD.Name = "GameRateNUD";
            this.GameRateNUD.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // GamePageClose
            // 
            resources.ApplyResources(this.GamePageClose, "GamePageClose");
            this.GamePageClose.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.GamePageClose.ForeColor = System.Drawing.Color.Black;
            this.GamePageClose.Name = "GamePageClose";
            this.GamePageClose.UseVisualStyleBackColor = true;
            this.GamePageClose.Click += new System.EventHandler(this.GamePageClose_Click);
            // 
            // GameFilePath
            // 
            resources.ApplyResources(this.GameFilePath, "GameFilePath");
            this.GameFilePath.ForeColor = System.Drawing.Color.DimGray;
            this.GameFilePath.Name = "GameFilePath";
            // 
            // GameModes
            // 
            resources.ApplyResources(this.GameModes, "GameModes");
            this.GameModes.ForeColor = System.Drawing.Color.DimGray;
            this.GameModes.Name = "GameModes";
            // 
            // GameGenres
            // 
            resources.ApplyResources(this.GameGenres, "GameGenres");
            this.GameGenres.ForeColor = System.Drawing.Color.DimGray;
            this.GameGenres.Name = "GameGenres";
            // 
            // GameDevName
            // 
            resources.ApplyResources(this.GameDevName, "GameDevName");
            this.GameDevName.ForeColor = System.Drawing.Color.DimGray;
            this.GameDevName.Name = "GameDevName";
            // 
            // GamePicBox
            // 
            this.GamePicBox.Image = global::CyberClub.Properties.Resources.icon;
            this.GamePicBox.InitialImage = global::CyberClub.Properties.Resources.icon;
            resources.ApplyResources(this.GamePicBox, "GamePicBox");
            this.GamePicBox.Name = "GamePicBox";
            this.GamePicBox.TabStop = false;
            // 
            // GameName
            // 
            resources.ApplyResources(this.GameName, "GameName");
            this.GameName.ForeColor = System.Drawing.Color.Black;
            this.GameName.Name = "GameName";
            // 
            // GameRunSubmit
            // 
            this.GameRunSubmit.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.GameRunSubmit, "GameRunSubmit");
            this.GameRunSubmit.ForeColor = System.Drawing.Color.Black;
            this.GameRunSubmit.Name = "GameRunSubmit";
            this.GameRunSubmit.UseVisualStyleBackColor = true;
            this.GameRunSubmit.Click += new System.EventHandler(this.GameRunSubmit_Click);
            // 
            // GamePanel
            // 
            resources.ApplyResources(this.GamePanel, "GamePanel");
            this.GamePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.GamePanel.Controls.Add(this.GSrchGenres);
            this.GamePanel.Controls.Add(this.GamesSwitch);
            this.GamePanel.Controls.Add(this.GamesList);
            this.GamePanel.Controls.Add(this.GSrchMultiCB);
            this.GamePanel.Controls.Add(this.GamesLabel);
            this.GamePanel.Controls.Add(this.GSrchSingleCB);
            this.GamePanel.Controls.Add(this.GameSearch);
            this.GamePanel.Controls.Add(this.GSrchDevLabel);
            this.GamePanel.Controls.Add(this.GSrchDev);
            this.GamePanel.Name = "GamePanel";
            // 
            // GSrchGenres
            // 
            resources.ApplyResources(this.GSrchGenres, "GSrchGenres");
            this.GSrchGenres.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(96)))), ((int)(((byte)(0)))));
            this.GSrchGenres.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.GSrchGenres.ForeColor = System.Drawing.Color.White;
            this.GSrchGenres.FormattingEnabled = true;
            this.GSrchGenres.MultiColumn = true;
            this.GSrchGenres.Name = "GSrchGenres";
            this.GSrchGenres.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.GSrchGenres_ItemCheck);
            // 
            // GamesSwitch
            // 
            resources.ApplyResources(this.GamesSwitch, "GamesSwitch");
            this.GamesSwitch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.GamesSwitch.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(96)))), ((int)(((byte)(0)))));
            this.GamesSwitch.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(96)))), ((int)(((byte)(0)))));
            this.GamesSwitch.Name = "GamesSwitch";
            this.GamesSwitch.UseVisualStyleBackColor = false;
            this.GamesSwitch.CheckedChanged += new System.EventHandler(this.GamesSwitch_CheckedChanged);
            // 
            // GamesList
            // 
            this.GamesList.Activation = System.Windows.Forms.ItemActivation.OneClick;
            resources.ApplyResources(this.GamesList, "GamesList");
            this.GamesList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(96)))), ((int)(((byte)(0)))));
            this.GamesList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.GamesList.ForeColor = System.Drawing.Color.White;
            this.GamesList.HideSelection = false;
            this.GamesList.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            ((System.Windows.Forms.ListViewItem)(resources.GetObject("GamesList.Items")))});
            this.GamesList.LargeImageList = this.GamePics;
            this.GamesList.MultiSelect = false;
            this.GamesList.Name = "GamesList";
            this.GamesList.SmallImageList = this.GamePics;
            this.GamesList.TileSize = new System.Drawing.Size(200, 100);
            this.GamesList.UseCompatibleStateImageBehavior = false;
            this.GamesList.Click += new System.EventHandler(this.GamesList_Click);
            // 
            // GamePics
            // 
            this.GamePics.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("GamePics.ImageStream")));
            this.GamePics.TransparentColor = System.Drawing.Color.Transparent;
            this.GamePics.Images.SetKeyName(0, "gamepad.ico");
            // 
            // GSrchMultiCB
            // 
            resources.ApplyResources(this.GSrchMultiCB, "GSrchMultiCB");
            this.GSrchMultiCB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.GSrchMultiCB.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(96)))), ((int)(((byte)(0)))));
            this.GSrchMultiCB.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(96)))), ((int)(((byte)(0)))));
            this.GSrchMultiCB.Name = "GSrchMultiCB";
            this.GSrchMultiCB.UseVisualStyleBackColor = false;
            this.GSrchMultiCB.CheckedChanged += new System.EventHandler(this.GSrchMultiCB_CheckedChanged);
            // 
            // GamesLabel
            // 
            resources.ApplyResources(this.GamesLabel, "GamesLabel");
            this.GamesLabel.Name = "GamesLabel";
            // 
            // GSrchSingleCB
            // 
            resources.ApplyResources(this.GSrchSingleCB, "GSrchSingleCB");
            this.GSrchSingleCB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.GSrchSingleCB.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(96)))), ((int)(((byte)(0)))));
            this.GSrchSingleCB.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(96)))), ((int)(((byte)(0)))));
            this.GSrchSingleCB.Name = "GSrchSingleCB";
            this.GSrchSingleCB.UseVisualStyleBackColor = false;
            this.GSrchSingleCB.CheckedChanged += new System.EventHandler(this.GSrchSingleCB_CheckedChanged);
            // 
            // GameSearch
            // 
            resources.ApplyResources(this.GameSearch, "GameSearch");
            this.GameSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(96)))), ((int)(((byte)(0)))));
            this.GameSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.GameSearch.ForeColor = System.Drawing.Color.White;
            this.GameSearch.Name = "GameSearch";
            this.GameSearch.TextChanged += new System.EventHandler(this.GameSearch_TextChanged);
            // 
            // GSrchDevLabel
            // 
            resources.ApplyResources(this.GSrchDevLabel, "GSrchDevLabel");
            this.GSrchDevLabel.BackColor = System.Drawing.Color.Transparent;
            this.GSrchDevLabel.Name = "GSrchDevLabel";
            // 
            // GSrchDev
            // 
            resources.ApplyResources(this.GSrchDev, "GSrchDev");
            this.GSrchDev.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(96)))), ((int)(((byte)(0)))));
            this.GSrchDev.ForeColor = System.Drawing.Color.White;
            this.GSrchDev.FormattingEnabled = true;
            this.GSrchDev.Name = "GSrchDev";
            this.GSrchDev.TextChanged += new System.EventHandler(this.GSrchDev_TextChanged);
            // 
            // AccountPanel
            // 
            resources.ApplyResources(this.AccountPanel, "AccountPanel");
            this.AccountPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(64)))), ((int)(((byte)(32)))));
            this.AccountPanel.Controls.Add(this.PasswdRepeatLabel);
            this.AccountPanel.Controls.Add(this.PasswdRepeat);
            this.AccountPanel.Controls.Add(this.Passwd);
            this.AccountPanel.Controls.Add(this.AccSetingsLabel);
            this.AccountPanel.Controls.Add(this.PasswdShow);
            this.AccountPanel.Controls.Add(this.PasswdLabel);
            this.AccountPanel.Controls.Add(this.EMail);
            this.AccountPanel.Controls.Add(this.EMailLabel);
            this.AccountPanel.Controls.Add(this.AccSubmit);
            this.AccountPanel.Controls.Add(this.UserInfo);
            this.AccountPanel.Controls.Add(this.UserName);
            this.AccountPanel.Controls.Add(this.UserInfoLabel);
            this.AccountPanel.Controls.Add(this.UserNameLabel);
            this.AccountPanel.Name = "AccountPanel";
            // 
            // PasswdRepeatLabel
            // 
            resources.ApplyResources(this.PasswdRepeatLabel, "PasswdRepeatLabel");
            this.PasswdRepeatLabel.BackColor = System.Drawing.Color.Transparent;
            this.PasswdRepeatLabel.Name = "PasswdRepeatLabel";
            // 
            // PasswdRepeat
            // 
            this.PasswdRepeat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(48)))), ((int)(((byte)(24)))));
            this.PasswdRepeat.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.PasswdRepeat.ForeColor = System.Drawing.Color.Lime;
            resources.ApplyResources(this.PasswdRepeat, "PasswdRepeat");
            this.PasswdRepeat.Name = "PasswdRepeat";
            // 
            // Passwd
            // 
            this.Passwd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(48)))), ((int)(((byte)(24)))));
            this.Passwd.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Passwd.ForeColor = System.Drawing.Color.Lime;
            resources.ApplyResources(this.Passwd, "Passwd");
            this.Passwd.Name = "Passwd";
            // 
            // AccSetingsLabel
            // 
            resources.ApplyResources(this.AccSetingsLabel, "AccSetingsLabel");
            this.AccSetingsLabel.Name = "AccSetingsLabel";
            // 
            // PasswdShow
            // 
            resources.ApplyResources(this.PasswdShow, "PasswdShow");
            this.PasswdShow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(48)))), ((int)(((byte)(24)))));
            this.PasswdShow.Checked = true;
            this.PasswdShow.CheckState = System.Windows.Forms.CheckState.Checked;
            this.PasswdShow.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(48)))), ((int)(((byte)(24)))));
            this.PasswdShow.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(64)))), ((int)(((byte)(32)))));
            this.PasswdShow.Name = "PasswdShow";
            this.PasswdShow.UseVisualStyleBackColor = false;
            this.PasswdShow.CheckedChanged += new System.EventHandler(this.PasswdShow_CheckedChanged);
            // 
            // PasswdLabel
            // 
            resources.ApplyResources(this.PasswdLabel, "PasswdLabel");
            this.PasswdLabel.BackColor = System.Drawing.Color.Transparent;
            this.PasswdLabel.Name = "PasswdLabel";
            // 
            // EMail
            // 
            resources.ApplyResources(this.EMail, "EMail");
            this.EMail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(48)))), ((int)(((byte)(24)))));
            this.EMail.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.EMail.ForeColor = System.Drawing.Color.Lime;
            this.EMail.Name = "EMail";
            // 
            // EMailLabel
            // 
            resources.ApplyResources(this.EMailLabel, "EMailLabel");
            this.EMailLabel.Name = "EMailLabel";
            // 
            // AccSubmit
            // 
            resources.ApplyResources(this.AccSubmit, "AccSubmit");
            this.AccSubmit.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.AccSubmit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.AccSubmit.Name = "AccSubmit";
            this.AccSubmit.UseVisualStyleBackColor = true;
            this.AccSubmit.Click += new System.EventHandler(this.AccSubmit_Click);
            // 
            // UserInfo
            // 
            resources.ApplyResources(this.UserInfo, "UserInfo");
            this.UserInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(48)))), ((int)(((byte)(24)))));
            this.UserInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.UserInfo.ForeColor = System.Drawing.Color.Lime;
            this.UserInfo.Name = "UserInfo";
            // 
            // UserName
            // 
            resources.ApplyResources(this.UserName, "UserName");
            this.UserName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(48)))), ((int)(((byte)(24)))));
            this.UserName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.UserName.ForeColor = System.Drawing.Color.Lime;
            this.UserName.Name = "UserName";
            // 
            // UserInfoLabel
            // 
            resources.ApplyResources(this.UserInfoLabel, "UserInfoLabel");
            this.UserInfoLabel.Name = "UserInfoLabel";
            // 
            // UserNameLabel
            // 
            resources.ApplyResources(this.UserNameLabel, "UserNameLabel");
            this.UserNameLabel.Name = "UserNameLabel";
            // 
            // PrintColors
            // 
            resources.ApplyResources(this.PrintColors, "PrintColors");
            this.PrintColors.BackColor = System.Drawing.Color.Black;
            this.PrintColors.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(96)))), ((int)(((byte)(0)))));
            this.PrintColors.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.PrintColors.ForeColor = System.Drawing.Color.White;
            this.PrintColors.Name = "PrintColors";
            this.PrintColors.UseVisualStyleBackColor = false;
            this.PrintColors.CheckedChanged += new System.EventHandler(this.PrintColors_CheckedChanged);
            // 
            // UserForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(24)))), ((int)(((byte)(32)))));
            this.Controls.Add(this.PrintColors);
            this.Controls.Add(this.FlowLayout);
            this.Controls.Add(this.UserLabel);
            this.Controls.Add(this.LeftSettings);
            this.Controls.Add(this.LeftGames);
            this.Controls.Add(this.AdminLabel);
            this.Controls.Add(this.LogOutButton);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "UserForm";
            this.Opacity = 0.99D;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.UserForm_FormClosed);
            this.Load += new System.EventHandler(this.UserForm_Load);
            this.MsgPanel.ResumeLayout(false);
            this.MsgPanel.PerformLayout();
            this.FlowLayout.ResumeLayout(false);
            this.FlowLayout.PerformLayout();
            this.GamePagePanel.ResumeLayout(false);
            this.GamePagePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GameRateNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GamePicBox)).EndInit();
            this.GamePanel.ResumeLayout(false);
            this.GamePanel.PerformLayout();
            this.AccountPanel.ResumeLayout(false);
            this.AccountPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button LogOutButton;
        private System.Windows.Forms.Button LeftSettings;
        private System.Windows.Forms.Button LeftGames;
        private System.Windows.Forms.Label AdminLabel;
        private System.Windows.Forms.Panel MsgPanel;
        private System.Windows.Forms.Label UserLabel;
        private System.Windows.Forms.Label MsgBrieflyLabel;
        private System.Windows.Forms.Button MsgSubmit;
        private System.Windows.Forms.RichTextBox MsgDetails;
        private System.Windows.Forms.RichTextBox MsgBriefly;
        private System.Windows.Forms.Label MsgDetailLabel;
        private System.Windows.Forms.FlowLayoutPanel FlowLayout;
        private System.Windows.Forms.Panel AccountPanel;
        private System.Windows.Forms.Button AccSubmit;
        private System.Windows.Forms.RichTextBox UserInfo;
        private System.Windows.Forms.RichTextBox UserName;
        private System.Windows.Forms.Label UserInfoLabel;
        private System.Windows.Forms.Label UserNameLabel;
        private System.Windows.Forms.RichTextBox EMail;
        private System.Windows.Forms.Label EMailLabel;
        private System.Windows.Forms.CheckBox PasswdShow;
        private System.Windows.Forms.Label PasswdLabel;
        private System.Windows.Forms.Label FeedbackLabel;
        private System.Windows.Forms.Label AccSetingsLabel;
        private System.Windows.Forms.Panel GamePanel;
        private System.Windows.Forms.Label GamesLabel;
        private System.Windows.Forms.RichTextBox GameSearch;
        private System.Windows.Forms.ListView GamesList;
        private System.Windows.Forms.ImageList GamePics;
        private System.Windows.Forms.CheckBox GamesSwitch;
        private System.Windows.Forms.CheckedListBox GSrchGenres;
        private System.Windows.Forms.CheckBox GSrchMultiCB;
        private System.Windows.Forms.CheckBox GSrchSingleCB;
        private System.Windows.Forms.Label GSrchDevLabel;
        private System.Windows.Forms.ComboBox GSrchDev;
        private System.Windows.Forms.TextBox Passwd;
        private System.Windows.Forms.Panel GamePagePanel;
        private System.Windows.Forms.Label GameFilePath;
        private System.Windows.Forms.Label GameModes;
        private System.Windows.Forms.Label GameGenres;
        private System.Windows.Forms.Label GameDevName;
        private System.Windows.Forms.PictureBox GamePicBox;
        private System.Windows.Forms.Label GameName;
        private System.Windows.Forms.Button GameRunSubmit;
        private System.Windows.Forms.Button GamePageClose;
        private System.Windows.Forms.CheckBox GameRate;
        private System.Windows.Forms.NumericUpDown GameRateNUD;
        private System.Windows.Forms.CheckBox GameSubscribe;
        private System.Windows.Forms.Label GameID;
        private System.Windows.Forms.Label GameRating;
        private System.Windows.Forms.CheckBox PrintColors;
        private System.Windows.Forms.Label PasswdRepeatLabel;
        private System.Windows.Forms.TextBox PasswdRepeat;
    }
}