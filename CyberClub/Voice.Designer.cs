namespace CyberClub
{
    partial class Voice
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Voice));
            this.Yes = new System.Windows.Forms.Button();
            this.ErrorText = new System.Windows.Forms.Label();
            this.No = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Yes
            // 
            resources.ApplyResources(this.Yes, "Yes");
            this.Yes.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.Yes.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.Yes.Name = "Yes";
            this.Yes.UseVisualStyleBackColor = true;
            this.Yes.Click += new System.EventHandler(this.Yes_Click);
            // 
            // ErrorText
            // 
            resources.ApplyResources(this.ErrorText, "ErrorText");
            this.ErrorText.Name = "ErrorText";
            // 
            // No
            // 
            resources.ApplyResources(this.No, "No");
            this.No.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.No.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.No.Name = "No";
            this.No.UseVisualStyleBackColor = true;
            this.No.Click += new System.EventHandler(this.No_Click);
            // 
            // Voice
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(0)))), ((int)(((byte)(32)))));
            this.Controls.Add(this.No);
            this.Controls.Add(this.ErrorText);
            this.Controls.Add(this.Yes);
            this.ForeColor = System.Drawing.Color.Lime;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Voice";
            this.Opacity = 0.95D;
            this.ShowInTaskbar = false;
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form_MouseDown);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button Yes;
        private System.Windows.Forms.Label ErrorText;
        private System.Windows.Forms.Button No;
    }
}