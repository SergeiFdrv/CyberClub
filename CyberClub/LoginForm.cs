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

        public static string CS => Properties.Settings.Default.CyberClubConnectionString;

        public static int UserID { get; private set; }

        private void LogInButton_Click(object sender, EventArgs e)
        { // Вход в аккаунт
            if (!string.IsNullOrEmpty(UserName.Text))
            {
                string query = "SELECT userid, authority, authname " +
                    "FROM users INNER JOIN hierarchy ON users.authority = " +
                    "hierarchy.authid WHERE username = @name AND passwd = @pwd";
                using (SqlConnection conn = new SqlConnection(CS))
                {
                    if (!ConnOpen(conn)) return;
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

        /// <summary>
        /// Обновляет данные в "списочных" элементах.
        /// </summary>
        public static bool UpdateBox(IList items,
            string select, string from, string order = "")
        {
            if (string.IsNullOrEmpty(select) || string.IsNullOrEmpty(from) || items == null)
                return false;
            if (string.IsNullOrEmpty(order)) order = select;
            using (SqlConnection conn = new SqlConnection(CS))
            {
                if (!ConnOpen(conn)) return false;
                if (select.Contains(',') || select.Contains(' '))
                    select = select.Substring(0, select.IndexOf(','))
                        .Substring(0, select.IndexOf(' '));
                if (from.Contains(',') || from.Contains(' '))
                    from = from.Substring(0, from.IndexOf(','))
                        .Substring(0, from.IndexOf(' '));
                if (order.Contains(',') || order.Contains(' '))
                    order = order.Substring(0, order.IndexOf(','))
                        .Substring(0, order.IndexOf(' '));
                using (SqlCommand command = new
                    SqlCommand($"SELECT {select} FROM {from} ORDER BY {order}", conn))
                {
                    SqlDataReader dataReader = command.ExecuteReader();
                    items.Clear();
                    while (dataReader.Read())
                    {
                        items.Add(dataReader[select]);
                    }
                }
            }
            return true;
        }

        /// <summary>По возможности выполняет conn.Open(), т.е. открывает 
        /// подключение к базе данных. Обрабатывает SqlException.</summary>
        public static bool ConnOpen(SqlConnection conn)
        {
            try
            {
                if (conn == null)
                {
                    Voice.Say(Resources.Lang.Error);
                    return false;
                }
                conn.Open();
                return true;
            }
            catch (SqlException)
            {
                Voice.Say(Resources.Lang.DBError);
                return false;
            }
        }
    }
}
