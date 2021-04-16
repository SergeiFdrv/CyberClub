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

        private void LogInButton_Click(object sender, EventArgs e)
        { // Вход в аккаунт
            if (!string.IsNullOrEmpty(UserName.Text))
            {
                System.Collections.Generic.Dictionary<string, object> account
                    = DB.GetAccount(UserName.Text);
                if (account is null || account.ContainsKey("userpass") && 
                    account["userpass"] == Password.Text)
                {
                    Voice.Say(Resources.Lang.LoginPasswordNotFound);
                    return;
                }
                UserName.Text = Password.Text = "";
                UserLevel level = (UserLevel)account["userlevel"];
                if (level == UserLevel.Banned)
                {
                    Voice.Say(Resources.Lang.YouAreBanned);
                    return;
                }
                UserID = (int)account["userid"];
                Hide();
                if (level == UserLevel.Admin)
                    using (AdminForm af = new AdminForm { Owner = this })
                        af.ShowDialog();
                else using (UserForm uf = new UserForm { Owner = this })
                        uf.ShowDialog();
            }
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
