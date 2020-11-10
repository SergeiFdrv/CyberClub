using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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

        public static int UserID { get; private set; }

        private void LogInButton_Click(object sender, EventArgs e)
        { // Вход в аккаунт
            if (!string.IsNullOrEmpty(UserName.Text))
            {
                string query = "SELECT userid, authority, authname " +
                    "FROM users INNER JOIN hierarchy ON users.authority = " +
                    "hierarchy.authid WHERE username = @name AND passwd = @pwd";
                using (SqlConnection conn = new SqlConnection(AppWide.CS))
                {
                    if (!AppWide.ConnOpen(conn)) return;
                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.Add(new SqlParameter("@name", UserName.Text));
                        command.Parameters.Add(new SqlParameter("@pwd", Password.Text));
                        SqlDataReader dataReader = command.ExecuteReader();
                        if (dataReader.Read())
                        {
                            UserName.Text = Password.Text = "";
                            UserID = (int)dataReader["userid"];
                            string authname = (string)dataReader["authname"];
                            if (authname == "banned")
                            {
                                Voice.Say(Resources.Lang.YouAreBanned);
                                return;
                            }
                            Hide();
                            if (authname == "admin")
                                using (AdminForm af = new AdminForm { Owner = this })
                                    af.ShowDialog();
                            else using (UserForm uf = new UserForm { Owner = this })
                                    uf.ShowDialog();
                        }
                        else Voice.Say(Resources.Lang.LoginPasswordNotFound);
                    }
                }
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
