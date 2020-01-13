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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("Sample", 0);
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
            this.LogOutButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LogOutButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.LogOutButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.LogOutButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LogOutButton.Location = new System.Drawing.Point(16, 635);
            this.LogOutButton.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.LogOutButton.Name = "LogOutButton";
            this.LogOutButton.Size = new System.Drawing.Size(200, 40);
            this.LogOutButton.TabIndex = 5;
            this.LogOutButton.Text = "Выйти";
            this.LogOutButton.UseVisualStyleBackColor = true;
            this.LogOutButton.Click += new System.EventHandler(this.LogOutButton_Click);
            // 
            // LeftSettings
            // 
            this.LeftSettings.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.LeftSettings.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.LeftSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LeftSettings.Location = new System.Drawing.Point(16, 90);
            this.LeftSettings.Name = "LeftSettings";
            this.LeftSettings.Size = new System.Drawing.Size(200, 40);
            this.LeftSettings.TabIndex = 2;
            this.LeftSettings.Text = "Настройки";
            this.LeftSettings.UseVisualStyleBackColor = true;
            this.LeftSettings.Click += new System.EventHandler(this.LeftSettings_Click);
            // 
            // LeftGames
            // 
            this.LeftGames.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.LeftGames.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.LeftGames.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LeftGames.Location = new System.Drawing.Point(16, 44);
            this.LeftGames.Name = "LeftGames";
            this.LeftGames.Size = new System.Drawing.Size(200, 40);
            this.LeftGames.TabIndex = 1;
            this.LeftGames.Text = "Игры";
            this.LeftGames.UseVisualStyleBackColor = true;
            this.LeftGames.Click += new System.EventHandler(this.LeftGames_Click);
            // 
            // AdminLabel
            // 
            this.AdminLabel.AutoSize = true;
            this.AdminLabel.Location = new System.Drawing.Point(12, 9);
            this.AdminLabel.Name = "AdminLabel";
            this.AdminLabel.Size = new System.Drawing.Size(147, 22);
            this.AdminLabel.TabIndex = 0;
            this.AdminLabel.Text = "Главное меню";
            // 
            // MsgPanel
            // 
            this.MsgPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MsgPanel.AutoSize = true;
            this.MsgPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(32)))), ((int)(((byte)(48)))));
            this.MsgPanel.Controls.Add(this.FeedbackLabel);
            this.MsgPanel.Controls.Add(this.MsgSubmit);
            this.MsgPanel.Controls.Add(this.MsgDetails);
            this.MsgPanel.Controls.Add(this.MsgBriefly);
            this.MsgPanel.Controls.Add(this.MsgDetailLabel);
            this.MsgPanel.Controls.Add(this.MsgBrieflyLabel);
            this.MsgPanel.Location = new System.Drawing.Point(3, 1087);
            this.MsgPanel.Name = "MsgPanel";
            this.MsgPanel.Padding = new System.Windows.Forms.Padding(10);
            this.MsgPanel.Size = new System.Drawing.Size(1018, 270);
            this.MsgPanel.TabIndex = 7;
            this.MsgPanel.Visible = false;
            // 
            // FeedbackLabel
            // 
            this.FeedbackLabel.AutoSize = true;
            this.FeedbackLabel.Location = new System.Drawing.Point(51, 10);
            this.FeedbackLabel.Name = "FeedbackLabel";
            this.FeedbackLabel.Size = new System.Drawing.Size(156, 22);
            this.FeedbackLabel.TabIndex = 23;
            this.FeedbackLabel.Text = "Обратная связь";
            // 
            // MsgSubmit
            // 
            this.MsgSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.MsgSubmit.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.MsgSubmit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.MsgSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MsgSubmit.Location = new System.Drawing.Point(816, 216);
            this.MsgSubmit.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.MsgSubmit.Name = "MsgSubmit";
            this.MsgSubmit.Size = new System.Drawing.Size(138, 39);
            this.MsgSubmit.TabIndex = 22;
            this.MsgSubmit.Text = "Отправить";
            this.MsgSubmit.UseVisualStyleBackColor = true;
            this.MsgSubmit.Click += new System.EventHandler(this.MsgSubmit_Click);
            // 
            // MsgDetails
            // 
            this.MsgDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MsgDetails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(16)))), ((int)(((byte)(32)))));
            this.MsgDetails.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.MsgDetails.ForeColor = System.Drawing.Color.Lime;
            this.MsgDetails.Location = new System.Drawing.Point(177, 78);
            this.MsgDetails.MaxLength = 255;
            this.MsgDetails.Name = "MsgDetails";
            this.MsgDetails.Size = new System.Drawing.Size(777, 127);
            this.MsgDetails.TabIndex = 21;
            this.MsgDetails.Text = "";
            // 
            // MsgBriefly
            // 
            this.MsgBriefly.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MsgBriefly.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(16)))), ((int)(((byte)(32)))));
            this.MsgBriefly.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.MsgBriefly.ForeColor = System.Drawing.Color.Lime;
            this.MsgBriefly.Location = new System.Drawing.Point(177, 44);
            this.MsgBriefly.MaxLength = 50;
            this.MsgBriefly.Multiline = false;
            this.MsgBriefly.Name = "MsgBriefly";
            this.MsgBriefly.Size = new System.Drawing.Size(777, 28);
            this.MsgBriefly.TabIndex = 20;
            this.MsgBriefly.Text = "";
            // 
            // MsgDetailLabel
            // 
            this.MsgDetailLabel.AutoSize = true;
            this.MsgDetailLabel.Location = new System.Drawing.Point(51, 78);
            this.MsgDetailLabel.Name = "MsgDetailLabel";
            this.MsgDetailLabel.Size = new System.Drawing.Size(120, 22);
            this.MsgDetailLabel.TabIndex = 19;
            this.MsgDetailLabel.Text = "Подробнее";
            // 
            // MsgBrieflyLabel
            // 
            this.MsgBrieflyLabel.AutoSize = true;
            this.MsgBrieflyLabel.Location = new System.Drawing.Point(51, 45);
            this.MsgBrieflyLabel.Name = "MsgBrieflyLabel";
            this.MsgBrieflyLabel.Size = new System.Drawing.Size(96, 22);
            this.MsgBrieflyLabel.TabIndex = 18;
            this.MsgBrieflyLabel.Text = "Коротко*";
            // 
            // UserLabel
            // 
            this.UserLabel.AutoSize = true;
            this.UserLabel.Location = new System.Drawing.Point(237, 9);
            this.UserLabel.Name = "UserLabel";
            this.UserLabel.Size = new System.Drawing.Size(134, 22);
            this.UserLabel.TabIndex = 6;
            this.UserLabel.Text = "Пользователь";
            // 
            // FlowLayout
            // 
            this.FlowLayout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FlowLayout.AutoScroll = true;
            this.FlowLayout.Controls.Add(this.GamePagePanel);
            this.FlowLayout.Controls.Add(this.GamePanel);
            this.FlowLayout.Controls.Add(this.AccountPanel);
            this.FlowLayout.Controls.Add(this.MsgPanel);
            this.FlowLayout.Location = new System.Drawing.Point(238, 41);
            this.FlowLayout.Margin = new System.Windows.Forms.Padding(0);
            this.FlowLayout.Name = "FlowLayout";
            this.FlowLayout.Size = new System.Drawing.Size(1114, 638);
            this.FlowLayout.TabIndex = 23;
            // 
            // GamePagePanel
            // 
            this.GamePagePanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.GamePagePanel.Location = new System.Drawing.Point(3, 3);
            this.GamePagePanel.Name = "GamePagePanel";
            this.GamePagePanel.Padding = new System.Windows.Forms.Padding(10);
            this.GamePagePanel.Size = new System.Drawing.Size(1018, 156);
            this.GamePagePanel.TabIndex = 163;
            this.GamePagePanel.Visible = false;
            // 
            // GameRating
            // 
            this.GameRating.AutoSize = true;
            this.GameRating.ForeColor = System.Drawing.Color.DimGray;
            this.GameRating.Location = new System.Drawing.Point(645, 110);
            this.GameRating.Name = "GameRating";
            this.GameRating.Size = new System.Drawing.Size(82, 22);
            this.GameRating.TabIndex = 173;
            this.GameRating.Text = "Рейтинг";
            // 
            // GameID
            // 
            this.GameID.AutoSize = true;
            this.GameID.ForeColor = System.Drawing.Color.DimGray;
            this.GameID.Location = new System.Drawing.Point(13, 121);
            this.GameID.Name = "GameID";
            this.GameID.Size = new System.Drawing.Size(29, 22);
            this.GameID.TabIndex = 172;
            this.GameID.Text = "ID";
            this.GameID.Visible = false;
            // 
            // GameSubscribe
            // 
            this.GameSubscribe.Appearance = System.Windows.Forms.Appearance.Button;
            this.GameSubscribe.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.GameSubscribe.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.GameSubscribe.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.GameSubscribe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GameSubscribe.ForeColor = System.Drawing.Color.Black;
            this.GameSubscribe.Location = new System.Drawing.Point(158, 102);
            this.GameSubscribe.Name = "GameSubscribe";
            this.GameSubscribe.Size = new System.Drawing.Size(158, 39);
            this.GameSubscribe.TabIndex = 171;
            this.GameSubscribe.Text = "Подписаться";
            this.GameSubscribe.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.GameSubscribe.UseVisualStyleBackColor = false;
            this.GameSubscribe.Click += new System.EventHandler(this.GameSubscribe_Click);
            // 
            // GameRate
            // 
            this.GameRate.Appearance = System.Windows.Forms.Appearance.Button;
            this.GameRate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.GameRate.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.GameRate.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.GameRate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GameRate.ForeColor = System.Drawing.Color.Black;
            this.GameRate.Location = new System.Drawing.Point(472, 102);
            this.GameRate.Name = "GameRate";
            this.GameRate.Size = new System.Drawing.Size(113, 39);
            this.GameRate.TabIndex = 170;
            this.GameRate.Text = "Оценка";
            this.GameRate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.GameRate.UseVisualStyleBackColor = false;
            this.GameRate.Visible = false;
            this.GameRate.Click += new System.EventHandler(this.GameRate_Click);
            // 
            // GameRateNUD
            // 
            this.GameRateNUD.BackColor = System.Drawing.Color.White;
            this.GameRateNUD.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.GameRateNUD.ForeColor = System.Drawing.Color.Black;
            this.GameRateNUD.Location = new System.Drawing.Point(591, 111);
            this.GameRateNUD.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.GameRateNUD.Name = "GameRateNUD";
            this.GameRateNUD.Size = new System.Drawing.Size(48, 27);
            this.GameRateNUD.TabIndex = 169;
            this.GameRateNUD.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.GameRateNUD.Visible = false;
            // 
            // GamePageClose
            // 
            this.GamePageClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.GamePageClose.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.GamePageClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GamePageClose.ForeColor = System.Drawing.Color.Black;
            this.GamePageClose.Location = new System.Drawing.Point(901, 102);
            this.GamePageClose.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.GamePageClose.Name = "GamePageClose";
            this.GamePageClose.Size = new System.Drawing.Size(101, 39);
            this.GamePageClose.TabIndex = 167;
            this.GamePageClose.Text = "Закрыть";
            this.GamePageClose.UseVisualStyleBackColor = true;
            this.GamePageClose.Click += new System.EventHandler(this.GamePageClose_Click);
            // 
            // GameFilePath
            // 
            this.GameFilePath.AutoSize = true;
            this.GameFilePath.ForeColor = System.Drawing.Color.DimGray;
            this.GameFilePath.Location = new System.Drawing.Point(733, 110);
            this.GameFilePath.Name = "GameFilePath";
            this.GameFilePath.Size = new System.Drawing.Size(114, 22);
            this.GameFilePath.TabIndex = 166;
            this.GameFilePath.Text = "Путь к игре";
            this.GameFilePath.Visible = false;
            // 
            // GameModes
            // 
            this.GameModes.AutoSize = true;
            this.GameModes.ForeColor = System.Drawing.Color.DimGray;
            this.GameModes.Location = new System.Drawing.Point(154, 57);
            this.GameModes.Name = "GameModes";
            this.GameModes.Size = new System.Drawing.Size(253, 22);
            this.GameModes.TabIndex = 165;
            this.GameModes.Text = "Одиночный, мультиплеер";
            // 
            // GameGenres
            // 
            this.GameGenres.AutoSize = true;
            this.GameGenres.ForeColor = System.Drawing.Color.DimGray;
            this.GameGenres.Location = new System.Drawing.Point(154, 79);
            this.GameGenres.Name = "GameGenres";
            this.GameGenres.Size = new System.Drawing.Size(78, 22);
            this.GameGenres.TabIndex = 164;
            this.GameGenres.Text = "Жанры";
            // 
            // GameDevName
            // 
            this.GameDevName.AutoSize = true;
            this.GameDevName.ForeColor = System.Drawing.Color.DimGray;
            this.GameDevName.Location = new System.Drawing.Point(154, 35);
            this.GameDevName.Name = "GameDevName";
            this.GameDevName.Size = new System.Drawing.Size(131, 22);
            this.GameDevName.TabIndex = 163;
            this.GameDevName.Text = "Разработчик";
            // 
            // GamePicBox
            // 
            this.GamePicBox.Image = global::CyberClub.Properties.Resources.icon;
            this.GamePicBox.InitialImage = global::CyberClub.Properties.Resources.icon;
            this.GamePicBox.Location = new System.Drawing.Point(13, 13);
            this.GamePicBox.Name = "GamePicBox";
            this.GamePicBox.Size = new System.Drawing.Size(130, 130);
            this.GamePicBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.GamePicBox.TabIndex = 162;
            this.GamePicBox.TabStop = false;
            // 
            // GameName
            // 
            this.GameName.AutoSize = true;
            this.GameName.ForeColor = System.Drawing.Color.Black;
            this.GameName.Location = new System.Drawing.Point(154, 13);
            this.GameName.Name = "GameName";
            this.GameName.Size = new System.Drawing.Size(57, 22);
            this.GameName.TabIndex = 161;
            this.GameName.Text = "Игра";
            // 
            // GameRunSubmit
            // 
            this.GameRunSubmit.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.GameRunSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GameRunSubmit.ForeColor = System.Drawing.Color.Black;
            this.GameRunSubmit.Location = new System.Drawing.Point(325, 102);
            this.GameRunSubmit.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.GameRunSubmit.Name = "GameRunSubmit";
            this.GameRunSubmit.Size = new System.Drawing.Size(138, 39);
            this.GameRunSubmit.TabIndex = 22;
            this.GameRunSubmit.Text = "Запуск";
            this.GameRunSubmit.UseVisualStyleBackColor = true;
            this.GameRunSubmit.Visible = false;
            this.GameRunSubmit.Click += new System.EventHandler(this.GameRunSubmit_Click);
            // 
            // GamePanel
            // 
            this.GamePanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.GamePanel.Location = new System.Drawing.Point(3, 165);
            this.GamePanel.Name = "GamePanel";
            this.GamePanel.Padding = new System.Windows.Forms.Padding(10);
            this.GamePanel.Size = new System.Drawing.Size(1018, 613);
            this.GamePanel.TabIndex = 162;
            // 
            // GSrchGenres
            // 
            this.GSrchGenres.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GSrchGenres.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(96)))), ((int)(((byte)(0)))));
            this.GSrchGenres.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.GSrchGenres.ColumnWidth = 200;
            this.GSrchGenres.ForeColor = System.Drawing.Color.White;
            this.GSrchGenres.FormattingEnabled = true;
            this.GSrchGenres.Location = new System.Drawing.Point(55, 517);
            this.GSrchGenres.MultiColumn = true;
            this.GSrchGenres.Name = "GSrchGenres";
            this.GSrchGenres.Size = new System.Drawing.Size(899, 78);
            this.GSrchGenres.TabIndex = 115;
            this.GSrchGenres.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.GSrchGenres_ItemCheck);
            // 
            // GamesSwitch
            // 
            this.GamesSwitch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.GamesSwitch.Appearance = System.Windows.Forms.Appearance.Button;
            this.GamesSwitch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.GamesSwitch.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(96)))), ((int)(((byte)(0)))));
            this.GamesSwitch.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(96)))), ((int)(((byte)(0)))));
            this.GamesSwitch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GamesSwitch.Location = new System.Drawing.Point(55, 435);
            this.GamesSwitch.Name = "GamesSwitch";
            this.GamesSwitch.Size = new System.Drawing.Size(214, 39);
            this.GamesSwitch.TabIndex = 163;
            this.GamesSwitch.Text = "Мои игры";
            this.GamesSwitch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.GamesSwitch.UseVisualStyleBackColor = false;
            this.GamesSwitch.CheckedChanged += new System.EventHandler(this.GamesSwitch_CheckedChanged);
            // 
            // GamesList
            // 
            this.GamesList.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.GamesList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GamesList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(96)))), ((int)(((byte)(0)))));
            this.GamesList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.GamesList.ForeColor = System.Drawing.Color.White;
            this.GamesList.HideSelection = false;
            this.GamesList.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.GamesList.LargeImageList = this.GamePics;
            this.GamesList.Location = new System.Drawing.Point(55, 42);
            this.GamesList.MultiSelect = false;
            this.GamesList.Name = "GamesList";
            this.GamesList.Size = new System.Drawing.Size(899, 387);
            this.GamesList.SmallImageList = this.GamePics;
            this.GamesList.TabIndex = 162;
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
            this.GSrchMultiCB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.GSrchMultiCB.Appearance = System.Windows.Forms.Appearance.Button;
            this.GSrchMultiCB.AutoSize = true;
            this.GSrchMultiCB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.GSrchMultiCB.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(96)))), ((int)(((byte)(0)))));
            this.GSrchMultiCB.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(96)))), ((int)(((byte)(0)))));
            this.GSrchMultiCB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GSrchMultiCB.Location = new System.Drawing.Point(810, 480);
            this.GSrchMultiCB.Name = "GSrchMultiCB";
            this.GSrchMultiCB.Size = new System.Drawing.Size(144, 32);
            this.GSrchMultiCB.TabIndex = 113;
            this.GSrchMultiCB.Text = "Мультиплеер";
            this.GSrchMultiCB.UseVisualStyleBackColor = false;
            this.GSrchMultiCB.CheckedChanged += new System.EventHandler(this.GSrchMultiCB_CheckedChanged);
            // 
            // GamesLabel
            // 
            this.GamesLabel.AutoSize = true;
            this.GamesLabel.Location = new System.Drawing.Point(51, 10);
            this.GamesLabel.Name = "GamesLabel";
            this.GamesLabel.Size = new System.Drawing.Size(57, 22);
            this.GamesLabel.TabIndex = 161;
            this.GamesLabel.Text = "Игры";
            // 
            // GSrchSingleCB
            // 
            this.GSrchSingleCB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.GSrchSingleCB.Appearance = System.Windows.Forms.Appearance.Button;
            this.GSrchSingleCB.AutoSize = true;
            this.GSrchSingleCB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.GSrchSingleCB.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(96)))), ((int)(((byte)(0)))));
            this.GSrchSingleCB.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(96)))), ((int)(((byte)(0)))));
            this.GSrchSingleCB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GSrchSingleCB.Location = new System.Drawing.Point(678, 480);
            this.GSrchSingleCB.Name = "GSrchSingleCB";
            this.GSrchSingleCB.Size = new System.Drawing.Size(129, 32);
            this.GSrchSingleCB.TabIndex = 112;
            this.GSrchSingleCB.Text = "Одиночный";
            this.GSrchSingleCB.UseVisualStyleBackColor = false;
            this.GSrchSingleCB.CheckedChanged += new System.EventHandler(this.GSrchSingleCB_CheckedChanged);
            // 
            // GameSearch
            // 
            this.GameSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GameSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(96)))), ((int)(((byte)(0)))));
            this.GameSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.GameSearch.ForeColor = System.Drawing.Color.White;
            this.GameSearch.Location = new System.Drawing.Point(275, 435);
            this.GameSearch.MaxLength = 50;
            this.GameSearch.Multiline = false;
            this.GameSearch.Name = "GameSearch";
            this.GameSearch.Size = new System.Drawing.Size(679, 39);
            this.GameSearch.TabIndex = 160;
            this.GameSearch.Text = "";
            this.GameSearch.ZoomFactor = 1.3F;
            this.GameSearch.TextChanged += new System.EventHandler(this.GameSearch_TextChanged);
            // 
            // GSrchDevLabel
            // 
            this.GSrchDevLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.GSrchDevLabel.AutoSize = true;
            this.GSrchDevLabel.BackColor = System.Drawing.Color.Transparent;
            this.GSrchDevLabel.Location = new System.Drawing.Point(51, 484);
            this.GSrchDevLabel.Name = "GSrchDevLabel";
            this.GSrchDevLabel.Size = new System.Drawing.Size(131, 22);
            this.GSrchDevLabel.TabIndex = 107;
            this.GSrchDevLabel.Text = "Разработчик";
            // 
            // GSrchDev
            // 
            this.GSrchDev.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GSrchDev.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(96)))), ((int)(((byte)(0)))));
            this.GSrchDev.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GSrchDev.ForeColor = System.Drawing.Color.White;
            this.GSrchDev.FormattingEnabled = true;
            this.GSrchDev.Location = new System.Drawing.Point(188, 481);
            this.GSrchDev.MaxLength = 50;
            this.GSrchDev.Name = "GSrchDev";
            this.GSrchDev.Size = new System.Drawing.Size(484, 30);
            this.GSrchDev.TabIndex = 9;
            this.GSrchDev.TextChanged += new System.EventHandler(this.GSrchDev_TextChanged);
            // 
            // AccountPanel
            // 
            this.AccountPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.AccountPanel.Location = new System.Drawing.Point(3, 784);
            this.AccountPanel.Name = "AccountPanel";
            this.AccountPanel.Padding = new System.Windows.Forms.Padding(10);
            this.AccountPanel.Size = new System.Drawing.Size(1018, 297);
            this.AccountPanel.TabIndex = 23;
            this.AccountPanel.Visible = false;
            // 
            // PasswdRepeatLabel
            // 
            this.PasswdRepeatLabel.AutoSize = true;
            this.PasswdRepeatLabel.BackColor = System.Drawing.Color.Transparent;
            this.PasswdRepeatLabel.Location = new System.Drawing.Point(462, 243);
            this.PasswdRepeatLabel.Name = "PasswdRepeatLabel";
            this.PasswdRepeatLabel.Size = new System.Drawing.Size(104, 22);
            this.PasswdRepeatLabel.TabIndex = 164;
            this.PasswdRepeatLabel.Text = "Повторить";
            // 
            // PasswdRepeat
            // 
            this.PasswdRepeat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(48)))), ((int)(((byte)(24)))));
            this.PasswdRepeat.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.PasswdRepeat.ForeColor = System.Drawing.Color.Lime;
            this.PasswdRepeat.Location = new System.Drawing.Point(572, 243);
            this.PasswdRepeat.Name = "PasswdRepeat";
            this.PasswdRepeat.PasswordChar = '*';
            this.PasswdRepeat.Size = new System.Drawing.Size(235, 24);
            this.PasswdRepeat.TabIndex = 163;
            // 
            // Passwd
            // 
            this.Passwd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(48)))), ((int)(((byte)(24)))));
            this.Passwd.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Passwd.ForeColor = System.Drawing.Color.Lime;
            this.Passwd.Location = new System.Drawing.Point(177, 243);
            this.Passwd.Name = "Passwd";
            this.Passwd.PasswordChar = '*';
            this.Passwd.Size = new System.Drawing.Size(235, 24);
            this.Passwd.TabIndex = 162;
            // 
            // AccSetingsLabel
            // 
            this.AccSetingsLabel.AutoSize = true;
            this.AccSetingsLabel.Location = new System.Drawing.Point(51, 10);
            this.AccSetingsLabel.Name = "AccSetingsLabel";
            this.AccSetingsLabel.Size = new System.Drawing.Size(207, 22);
            this.AccSetingsLabel.TabIndex = 161;
            this.AccSetingsLabel.Text = "Настройки аккаунта";
            // 
            // PasswdShow
            // 
            this.PasswdShow.Appearance = System.Windows.Forms.Appearance.Button;
            this.PasswdShow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(48)))), ((int)(((byte)(24)))));
            this.PasswdShow.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.PasswdShow.Checked = true;
            this.PasswdShow.CheckState = System.Windows.Forms.CheckState.Checked;
            this.PasswdShow.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(48)))), ((int)(((byte)(24)))));
            this.PasswdShow.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(64)))), ((int)(((byte)(32)))));
            this.PasswdShow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PasswdShow.Location = new System.Drawing.Point(418, 243);
            this.PasswdShow.Name = "PasswdShow";
            this.PasswdShow.Size = new System.Drawing.Size(28, 24);
            this.PasswdShow.TabIndex = 159;
            this.PasswdShow.Text = "*";
            this.PasswdShow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.PasswdShow.UseVisualStyleBackColor = false;
            this.PasswdShow.CheckedChanged += new System.EventHandler(this.PasswdShow_CheckedChanged);
            // 
            // PasswdLabel
            // 
            this.PasswdLabel.AutoSize = true;
            this.PasswdLabel.BackColor = System.Drawing.Color.Transparent;
            this.PasswdLabel.Location = new System.Drawing.Point(51, 243);
            this.PasswdLabel.Name = "PasswdLabel";
            this.PasswdLabel.Size = new System.Drawing.Size(80, 22);
            this.PasswdLabel.TabIndex = 157;
            this.PasswdLabel.Text = "Пароль";
            // 
            // EMail
            // 
            this.EMail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.EMail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(48)))), ((int)(((byte)(24)))));
            this.EMail.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.EMail.ForeColor = System.Drawing.Color.Lime;
            this.EMail.Location = new System.Drawing.Point(177, 76);
            this.EMail.MaxLength = 50;
            this.EMail.Multiline = false;
            this.EMail.Name = "EMail";
            this.EMail.Size = new System.Drawing.Size(777, 28);
            this.EMail.TabIndex = 24;
            this.EMail.Text = "";
            // 
            // EMailLabel
            // 
            this.EMailLabel.AutoSize = true;
            this.EMailLabel.Location = new System.Drawing.Point(51, 77);
            this.EMailLabel.Name = "EMailLabel";
            this.EMailLabel.Size = new System.Drawing.Size(97, 22);
            this.EMailLabel.TabIndex = 23;
            this.EMailLabel.Text = "Эл. почта";
            // 
            // AccSubmit
            // 
            this.AccSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AccSubmit.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.AccSubmit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.AccSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AccSubmit.Location = new System.Drawing.Point(816, 243);
            this.AccSubmit.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.AccSubmit.Name = "AccSubmit";
            this.AccSubmit.Size = new System.Drawing.Size(138, 39);
            this.AccSubmit.TabIndex = 22;
            this.AccSubmit.Text = "Применить";
            this.AccSubmit.UseVisualStyleBackColor = true;
            this.AccSubmit.Click += new System.EventHandler(this.AccSubmit_Click);
            // 
            // UserInfo
            // 
            this.UserInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UserInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(48)))), ((int)(((byte)(24)))));
            this.UserInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.UserInfo.ForeColor = System.Drawing.Color.Lime;
            this.UserInfo.Location = new System.Drawing.Point(177, 110);
            this.UserInfo.MaxLength = 255;
            this.UserInfo.Name = "UserInfo";
            this.UserInfo.Size = new System.Drawing.Size(777, 127);
            this.UserInfo.TabIndex = 21;
            this.UserInfo.Text = "";
            // 
            // UserName
            // 
            this.UserName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UserName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(48)))), ((int)(((byte)(24)))));
            this.UserName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.UserName.ForeColor = System.Drawing.Color.Lime;
            this.UserName.Location = new System.Drawing.Point(177, 42);
            this.UserName.MaxLength = 50;
            this.UserName.Multiline = false;
            this.UserName.Name = "UserName";
            this.UserName.Size = new System.Drawing.Size(777, 28);
            this.UserName.TabIndex = 20;
            this.UserName.Text = "";
            // 
            // UserInfoLabel
            // 
            this.UserInfoLabel.AutoSize = true;
            this.UserInfoLabel.Location = new System.Drawing.Point(51, 110);
            this.UserInfoLabel.Name = "UserInfoLabel";
            this.UserInfoLabel.Size = new System.Drawing.Size(81, 22);
            this.UserInfoLabel.TabIndex = 19;
            this.UserInfoLabel.Text = "О себе";
            // 
            // UserNameLabel
            // 
            this.UserNameLabel.AutoSize = true;
            this.UserNameLabel.Location = new System.Drawing.Point(51, 43);
            this.UserNameLabel.Name = "UserNameLabel";
            this.UserNameLabel.Size = new System.Drawing.Size(58, 22);
            this.UserNameLabel.TabIndex = 18;
            this.UserNameLabel.Text = "Имя*";
            // 
            // PrintColors
            // 
            this.PrintColors.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PrintColors.Appearance = System.Windows.Forms.Appearance.Button;
            this.PrintColors.BackColor = System.Drawing.Color.Black;
            this.PrintColors.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(96)))), ((int)(((byte)(0)))));
            this.PrintColors.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.PrintColors.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PrintColors.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.PrintColors.ForeColor = System.Drawing.Color.White;
            this.PrintColors.Location = new System.Drawing.Point(1262, 7);
            this.PrintColors.Name = "PrintColors";
            this.PrintColors.Size = new System.Drawing.Size(90, 30);
            this.PrintColors.TabIndex = 164;
            this.PrintColors.Text = "Побелеть";
            this.PrintColors.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.PrintColors.UseVisualStyleBackColor = false;
            this.PrintColors.CheckedChanged += new System.EventHandler(this.PrintColors_CheckedChanged);
            // 
            // UserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(24)))), ((int)(((byte)(32)))));
            this.ClientSize = new System.Drawing.Size(1364, 689);
            this.Controls.Add(this.PrintColors);
            this.Controls.Add(this.FlowLayout);
            this.Controls.Add(this.UserLabel);
            this.Controls.Add(this.LeftSettings);
            this.Controls.Add(this.LeftGames);
            this.Controls.Add(this.AdminLabel);
            this.Controls.Add(this.LogOutButton);
            this.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Name = "UserForm";
            this.Opacity = 0.99D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CyberClub";
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