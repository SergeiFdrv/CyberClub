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
            this.Yes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Yes.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.Yes.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.Yes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Yes.Location = new System.Drawing.Point(284, 43);
            this.Yes.Name = "Yes";
            this.Yes.Size = new System.Drawing.Size(128, 38);
            this.Yes.TabIndex = 1;
            this.Yes.Text = "OK";
            this.Yes.UseVisualStyleBackColor = true;
            this.Yes.Click += new System.EventHandler(this.Yes_Click);
            // 
            // ErrorText
            // 
            this.ErrorText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ErrorText.Location = new System.Drawing.Point(12, 9);
            this.ErrorText.Name = "ErrorText";
            this.ErrorText.Size = new System.Drawing.Size(401, 28);
            this.ErrorText.TabIndex = 3;
            this.ErrorText.Text = resources.GetString("ErrorText.Text");
            // 
            // No
            // 
            this.No.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.No.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.No.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.No.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.No.Location = new System.Drawing.Point(150, 43);
            this.No.Name = "No";
            this.No.Size = new System.Drawing.Size(128, 38);
            this.No.TabIndex = 4;
            this.No.Text = "Отмена";
            this.No.UseVisualStyleBackColor = true;
            this.No.Visible = false;
            this.No.Click += new System.EventHandler(this.No_Click);
            // 
            // ErrorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(0)))), ((int)(((byte)(32)))));
            this.ClientSize = new System.Drawing.Size(425, 93);
            this.Controls.Add(this.No);
            this.Controls.Add(this.ErrorText);
            this.Controls.Add(this.Yes);
            this.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ForeColor = System.Drawing.Color.Lime;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Name = "ErrorForm";
            this.Opacity = 0.95D;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Error";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form_MouseDown);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button Yes;
        private System.Windows.Forms.Label ErrorText;
        private System.Windows.Forms.Button No;
    }
}