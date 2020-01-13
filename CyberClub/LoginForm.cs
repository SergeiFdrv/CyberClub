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

        public static string cs = @"Data Source=DESKTOP-LFCR3E8\SQLEXPRESS;" +
            "Initial Catalog=CyberClub;Integrated Security=True";

        public static int userid;

        private void LogInButton_Click(object sender, EventArgs e)
        { // Вход в аккаунт
            if (UserName.Text != "")
            {
                string query = "SELECT userid, authority, authname " +
                    "FROM users INNER JOIN hierarchy ON users.authority = " +
                    "hierarchy.authid WHERE username = @name AND passwd = @pwd";
                using (SqlConnection conn = new SqlConnection(cs))
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
                            userid = (int)dataReader["userid"];
                            switch ((string)dataReader["authname"])
                            {
                                case "admin": // админ
                                    AdminForm af = new AdminForm();
                                    af.Owner = this;
                                    af.Show();
                                    break;
                                case "banned":
                                    Voice.Say("Похоже, Ваш аккаунт " +
                                        "заблокирован. Администратор знает больше.");
                                    return;
                                case "player":
                                default: 
                                    UserForm uf = new UserForm();
                                    uf.Owner = this;
                                    uf.Show();
                                    break;
                            }
                            Hide();
                        }
                        else
                        {
                            Voice.Say("Сочетание \"логин-пароль\" " +
                                "не найдено. Для регистрации учетной записи " +
                                "обратитесь к администратору.");
                        }
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
            using (SqlConnection conn = new SqlConnection(LoginForm.cs))
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
                if (order == "") order = select;
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
                conn.Open();
                return true;
            }
            catch (SqlException)
            {
                Voice.Say("Ошибка базы данных.");
                return false;
            }
        }
    }
}
