using CyberClub.Data;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CyberClub
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private LoginDatabase DB { get; } = new LoginDatabase();

        public static int UserID { get; private set; }

        private async void LogInButton_Click(object sender, EventArgs e)
        { // Вход в аккаунт
            if (!string.IsNullOrEmpty(UserName.Text))
            {
                var task = LoginAsync(DB, UserName.Text, Password.Text);
                using (var ef = new LoadingForm())
                {
                    ef.Show();
                    var res = await task;
                    if (res != null)
                    {
                        ef.Close();
                        UserID = res.Item1;
                        Hide();
                        if (res.Item2 == UserLevel.Admin)
                            using (AdminForm af = new AdminForm { Owner = this })
                                af.ShowDialog();
                        else using (UserForm uf = new UserForm { Owner = this })
                                uf.ShowDialog();
                    }
                }
            }
        }

        private async Task<Tuple<int, UserLevel>> LoginAsync(LoginDatabase db, string userName, string passwd) =>
            await Task.Run(() => Login(db, userName, passwd));

        private static Tuple<int, UserLevel> Login(LoginDatabase db, string userName, string passwd)
        {
            System.Collections.Generic.Dictionary<string, object> account;
            try
            {
                account = db.GetAccount(userName);
            }
            catch
            {
                Voice.Say(Resources.Lang.DBError);
                return null;
            }
            if (account is null || account.ContainsKey("userpass") &&
                account["userpass"].ToString() == passwd)
            {
                Voice.SayAsync(Resources.Lang.LoginPasswordNotFound);
                return null;
            }
            userName = passwd = "";
            UserLevel level = (UserLevel)account["userlevel"];
            if (level == UserLevel.Banned)
            {
                Voice.SayAsync(Resources.Lang.YouAreBanned);
                return null;
            }
            var userID = (int)account["userid"];
            return Tuple.Create(userID, level);
        }

        private void InteractiveElements_KeyDown(object sender, KeyEventArgs e)
        { // Переключение между интерактивными элементами, включая текстбоксы, стрелками
            base.OnKeyDown(e);
            if (e.KeyCode == Keys.Up)
                GetNextControl((Control)sender, false).Select();
            else if (e.KeyCode == Keys.Down)
                GetNextControl((Control)sender, true).Select();
        }
    }
}
