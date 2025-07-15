namespace deneOS.init
{
    partial class login
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
            pictureBox1 = new PictureBox();
            txt3 = new Label();
            button6 = new Button();
            txt6 = new Button();
            txt5 = new Label();
            boxpass = new TextBox();
            txt4 = new Label();
            boxusr = new TextBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.DefaultAccountTile;
            pictureBox1.Location = new Point(100, 52);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(171, 155);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 25;
            pictureBox1.TabStop = false;
            // 
            // txt3
            // 
            txt3.AutoSize = true;
            txt3.Font = new Font("Segoe UI Variable Display", 9F);
            txt3.Location = new Point(2, 8);
            txt3.Name = "txt3";
            txt3.Size = new Size(36, 20);
            txt3.TabIndex = 24;
            txt3.Text = "Txt3";
            // 
            // button6
            // 
            button6.BackColor = SystemColors.ControlText;
            button6.FlatStyle = FlatStyle.Flat;
            button6.Font = new Font("Segoe UI Variable Display", 7F);
            button6.ForeColor = SystemColors.Control;
            button6.Location = new Point(367, 8);
            button6.Name = "button6";
            button6.Size = new Size(33, 23);
            button6.TabIndex = 23;
            button6.Text = "";
            button6.UseVisualStyleBackColor = false;
            button6.Click += button6_Click;
            // 
            // txt6
            // 
            txt6.BackColor = SystemColors.ControlText;
            txt6.ForeColor = SystemColors.Control;
            txt6.Location = new Point(16, 350);
            txt6.Name = "txt6";
            txt6.Size = new Size(366, 45);
            txt6.TabIndex = 22;
            txt6.Text = "Txt6";
            txt6.UseVisualStyleBackColor = false;
            txt6.Click += txt6_Click;
            // 
            // txt5
            // 
            txt5.AutoSize = true;
            txt5.ForeColor = SystemColors.Control;
            txt5.Location = new Point(11, 288);
            txt5.Name = "txt5";
            txt5.Size = new Size(57, 32);
            txt5.TabIndex = 21;
            txt5.Text = "Txt5";
            // 
            // boxpass
            // 
            boxpass.BackColor = SystemColors.WindowText;
            boxpass.ForeColor = SystemColors.Window;
            boxpass.Location = new Point(157, 288);
            boxpass.Name = "boxpass";
            boxpass.Size = new Size(226, 39);
            boxpass.TabIndex = 20;
            // 
            // txt4
            // 
            txt4.AutoSize = true;
            txt4.ForeColor = SystemColors.Control;
            txt4.Location = new Point(11, 221);
            txt4.Name = "txt4";
            txt4.Size = new Size(57, 32);
            txt4.TabIndex = 19;
            txt4.Text = "Txt4";
            // 
            // boxusr
            // 
            boxusr.BackColor = SystemColors.ControlText;
            boxusr.ForeColor = SystemColors.Window;
            boxusr.Location = new Point(157, 221);
            boxusr.Name = "boxusr";
            boxusr.Size = new Size(226, 39);
            boxusr.TabIndex = 18;
            // 
            // login
            // 
            AutoScaleDimensions = new SizeF(120F, 120F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = SystemColors.ControlText;
            ClientSize = new Size(403, 402);
            Controls.Add(pictureBox1);
            Controls.Add(txt3);
            Controls.Add(button6);
            Controls.Add(txt6);
            Controls.Add(txt5);
            Controls.Add(boxpass);
            Controls.Add(txt4);
            Controls.Add(boxusr);
            Font = new Font("Segoe UI Variable Display", 14F);
            ForeColor = SystemColors.Control;
            FormBorderStyle = FormBorderStyle.None;
            Name = "login";
            Opacity = 0.6D;
            Text = "login";
            Load += login_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Label txt3;
        private Button button6;
        private Button txt6;
        private Label txt5;
        private TextBox boxpass;
        private Label txt4;
        private TextBox boxusr;
    }
}