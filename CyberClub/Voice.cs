using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CyberClub
{
    /// <summary>
    /// Наш "дизайнерский" MessageBox. Haute couture
    /// </summary>
    public partial class Voice : Form
    {
        public Voice()
        {
            InitializeComponent();
        }

        public static DialogResult Say(
            string text, MessageBoxButtons buttons = MessageBoxButtons.OK)
        {
            using (Voice ef = new Voice())
            {
                if (buttons == MessageBoxButtons.YesNo)
                {
                    ef.Yes.Text = "Да";
                    ef.No.Text = "Нет";
                    ef.No.Visible = true;
                }
                ef.ErrorText.Text = text;
                // Делаем размер окна зависимым от длины текста
                ef.ErrorText.Height = ef.ErrorText.Font.Height + 
                    ef.ErrorText.Font.Height *
                    (ef.ErrorText.PreferredSize.Width / ef.ErrorText.Width);
                ef.Height += ef.ErrorText.Height - ef.ErrorText.Font.Height;
                return ef.ShowDialog(); // Вернуть значение DialogResult
            }
        }

        public static DialogResult Ask(string text) => 
            Say(text, MessageBoxButtons.YesNo);

        private void Form_MouseDown(object sender, MouseEventArgs e)
        { // Как перетаскивать окно за клиентскую часть:
            base.Capture = false;
            Message m = Message.Create(base.Handle, 0xa1, new IntPtr(2), IntPtr.Zero);
            this.WndProc(ref m);
        }

        private void Yes_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void No_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
            this.Close();
        }
    }
}
