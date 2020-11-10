using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CyberClub
{
    /// <summary>
    /// Our flexible version of MessageBox. Contains all we need and nothing else
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
                    ef.Yes.Text = Resources.Lang.Yes;
                    ef.No.Text = Resources.Lang.No;
                    ef.No.Visible = true;
                }
                ef.ErrorText.Text = text;
                AdjustFormHeight(ef);
                return ef.ShowDialog();
            }
        }

        /// <summary>
        /// Make window height depend on the text length
        /// </summary>
        private static void AdjustFormHeight(Voice form)
        {
            form.ErrorText.Height = form.ErrorText.Font.Height +
                form.ErrorText.Font.Height *
                form.ErrorText.PreferredSize.Width / form.ErrorText.Width;
            form.Height += form.ErrorText.Height - form.ErrorText.Font.Height;
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
