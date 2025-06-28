namespace deneOS.init
{
    partial class logonui
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
            components = new System.ComponentModel.Container();
            txt1 = new Label();
            txt2 = new Label();
            MinuteUpdate = new System.Windows.Forms.Timer(components);
            button2 = new Button();
            panel1 = new Panel();
            pictureBox1 = new PictureBox();
            txt3 = new Label();
            button6 = new Button();
            txt6 = new Button();
            txt5 = new Label();
            boxpass = new TextBox();
            txt4 = new Label();
            boxusr = new TextBox();
            panel2 = new Panel();
            boxregpassag = new TextBox();
            txt12 = new Label();
            txt9 = new Label();
            button1 = new Button();
            txt13 = new Button();
            txt11 = new Label();
            boxregpass = new TextBox();
            txt10 = new Label();
            boxregusr = new TextBox();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // txt1
            // 
            txt1.AutoSize = true;
            txt1.BackColor = Color.Transparent;
            txt1.Font = new Font("Segoe UI Variable Display", 72F);
            txt1.ForeColor = SystemColors.Control;
            txt1.Location = new Point(61, 694);
            txt1.Name = "txt1";
            txt1.Size = new Size(307, 159);
            txt1.TabIndex = 0;
            txt1.Text = "TXT1";
            // 
            // txt2
            // 
            txt2.AutoSize = true;
            txt2.BackColor = Color.Transparent;
            txt2.Font = new Font("Segoe UI Variable Display", 22F);
            txt2.ForeColor = SystemColors.Control;
            txt2.Location = new Point(79, 913);
            txt2.Name = "txt2";
            txt2.Size = new Size(102, 49);
            txt2.TabIndex = 1;
            txt2.Text = "TXT2";
            // 
            // MinuteUpdate
            // 
            MinuteUpdate.Enabled = true;
            MinuteUpdate.Interval = 1;
            MinuteUpdate.Tick += MinuteUpdate_Tick;
            // 
            // button2
            // 
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("Segoe MDL2 Assets", 24F);
            button2.ForeColor = SystemColors.Control;
            button2.Location = new Point(1820, 980);
            button2.Name = "button2";
            button2.Size = new Size(75, 57);
            button2.TabIndex = 100;
            button2.Text = "";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ControlText;
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(txt3);
            panel1.Controls.Add(button6);
            panel1.Controls.Add(txt6);
            panel1.Controls.Add(txt5);
            panel1.Controls.Add(boxpass);
            panel1.Controls.Add(txt4);
            panel1.Controls.Add(boxusr);
            panel1.Font = new Font("Segoe UI Variable Display", 14F);
            panel1.ForeColor = SystemColors.Control;
            panel1.Location = new Point(523, 226);
            panel1.Name = "panel1";
            panel1.Size = new Size(403, 402);
            panel1.TabIndex = 9;
            panel1.Visible = false;
            panel1.Paint += panel1_Paint;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.DefaultAccountTile;
            pictureBox1.Location = new Point(101, 47);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(171, 155);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 17;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // txt3
            // 
            txt3.AutoSize = true;
            txt3.Font = new Font("Segoe UI Variable Display", 9F);
            txt3.Location = new Point(3, 3);
            txt3.Name = "txt3";
            txt3.Size = new Size(36, 20);
            txt3.TabIndex = 16;
            txt3.Text = "Txt3";
            txt3.Click += txt3_Click;
            // 
            // button6
            // 
            button6.BackColor = SystemColors.ControlText;
            button6.FlatStyle = FlatStyle.Flat;
            button6.Font = new Font("Segoe UI Variable Display", 7F);
            button6.ForeColor = SystemColors.Control;
            button6.Location = new Point(368, 3);
            button6.Name = "button6";
            button6.Size = new Size(33, 23);
            button6.TabIndex = 15;
            button6.Text = "";
            button6.UseVisualStyleBackColor = false;
            button6.Click += button6_Click;
            // 
            // txt6
            // 
            txt6.BackColor = SystemColors.ControlText;
            txt6.ForeColor = SystemColors.Control;
            txt6.Location = new Point(17, 345);
            txt6.Name = "txt6";
            txt6.Size = new Size(366, 45);
            txt6.TabIndex = 14;
            txt6.Text = "Txt6";
            txt6.UseVisualStyleBackColor = false;
            txt6.Click += button5_Click;
            // 
            // txt5
            // 
            txt5.AutoSize = true;
            txt5.ForeColor = SystemColors.Control;
            txt5.Location = new Point(12, 283);
            txt5.Name = "txt5";
            txt5.Size = new Size(57, 32);
            txt5.TabIndex = 13;
            txt5.Text = "Txt5";
            txt5.Click += txt5_Click;
            // 
            // boxpass
            // 
            boxpass.BackColor = SystemColors.WindowText;
            boxpass.ForeColor = SystemColors.Window;
            boxpass.Location = new Point(158, 283);
            boxpass.Name = "boxpass";
            boxpass.Size = new Size(226, 39);
            boxpass.TabIndex = 12;
            boxpass.TextChanged += boxpass_TextChanged;
            // 
            // txt4
            // 
            txt4.AutoSize = true;
            txt4.ForeColor = SystemColors.Control;
            txt4.Location = new Point(12, 216);
            txt4.Name = "txt4";
            txt4.Size = new Size(57, 32);
            txt4.TabIndex = 11;
            txt4.Text = "Txt4";
            txt4.Click += txt4_Click;
            // 
            // boxusr
            // 
            boxusr.BackColor = SystemColors.ControlText;
            boxusr.ForeColor = SystemColors.Window;
            boxusr.Location = new Point(158, 216);
            boxusr.Name = "boxusr";
            boxusr.Size = new Size(226, 39);
            boxusr.TabIndex = 10;
            boxusr.TextChanged += boxusr_TextChanged;
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.ControlText;
            panel2.Controls.Add(boxregpassag);
            panel2.Controls.Add(txt12);
            panel2.Controls.Add(txt9);
            panel2.Controls.Add(button1);
            panel2.Controls.Add(txt13);
            panel2.Controls.Add(txt11);
            panel2.Controls.Add(boxregpass);
            panel2.Controls.Add(txt10);
            panel2.Controls.Add(boxregusr);
            panel2.Font = new Font("Segoe UI Variable Display", 14F);
            panel2.ForeColor = SystemColors.Control;
            panel2.Location = new Point(44, 230);
            panel2.Name = "panel2";
            panel2.Size = new Size(337, 386);
            panel2.TabIndex = 17;
            panel2.Visible = false;
            // 
            // boxregpassag
            // 
            boxregpassag.BackColor = SystemColors.WindowText;
            boxregpassag.ForeColor = SystemColors.Window;
            boxregpassag.Location = new Point(40, 243);
            boxregpassag.Name = "boxregpassag";
            boxregpassag.Size = new Size(260, 39);
            boxregpassag.TabIndex = 14;
            // 
            // txt12
            // 
            txt12.AutoSize = true;
            txt12.ForeColor = SystemColors.Control;
            txt12.Location = new Point(34, 208);
            txt12.Name = "txt12";
            txt12.Size = new Size(66, 32);
            txt12.TabIndex = 17;
            txt12.Text = "Txt12";
            // 
            // txt9
            // 
            txt9.AutoSize = true;
            txt9.Font = new Font("Segoe UI Variable Display", 9F);
            txt9.Location = new Point(3, 3);
            txt9.Name = "txt9";
            txt9.Size = new Size(36, 20);
            txt9.TabIndex = 16;
            txt9.Text = "Txt9";
            // 
            // button1
            // 
            button1.BackColor = SystemColors.ControlText;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI Variable Display", 7F);
            button1.ForeColor = SystemColors.Control;
            button1.Location = new Point(301, 4);
            button1.Name = "button1";
            button1.Size = new Size(33, 23);
            button1.TabIndex = 99;
            button1.Text = "";
            button1.UseVisualStyleBackColor = false;
            // 
            // txt13
            // 
            txt13.BackColor = SystemColors.ControlText;
            txt13.ForeColor = SystemColors.Control;
            txt13.Location = new Point(39, 293);
            txt13.Name = "txt13";
            txt13.Size = new Size(260, 45);
            txt13.TabIndex = 18;
            txt13.Text = "Txt13";
            txt13.UseVisualStyleBackColor = false;
            txt13.Click += txt13_Click;
            // 
            // txt11
            // 
            txt11.AutoSize = true;
            txt11.ForeColor = SystemColors.Control;
            txt11.Location = new Point(32, 134);
            txt11.Name = "txt11";
            txt11.Size = new Size(62, 32);
            txt11.TabIndex = 13;
            txt11.Text = "Txt11";
            // 
            // boxregpass
            // 
            boxregpass.BackColor = SystemColors.WindowText;
            boxregpass.ForeColor = SystemColors.Window;
            boxregpass.Location = new Point(40, 169);
            boxregpass.Name = "boxregpass";
            boxregpass.Size = new Size(260, 39);
            boxregpass.TabIndex = 12;
            // 
            // txt10
            // 
            txt10.AutoSize = true;
            txt10.ForeColor = SystemColors.Control;
            txt10.Location = new Point(32, 58);
            txt10.Name = "txt10";
            txt10.Size = new Size(66, 32);
            txt10.TabIndex = 11;
            txt10.Text = "Txt10";
            // 
            // boxregusr
            // 
            boxregusr.BackColor = SystemColors.ControlText;
            boxregusr.ForeColor = SystemColors.Window;
            boxregusr.Location = new Point(40, 92);
            boxregusr.Name = "boxregusr";
            boxregusr.Size = new Size(260, 39);
            boxregusr.TabIndex = 10;
            // 
            // logonui
            // 
            AutoScaleDimensions = new SizeF(120F, 120F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = SystemColors.ControlText;
            BackgroundImage = Properties.Resources.img100;
            BackgroundImageLayout = ImageLayout.Zoom;
            ClientSize = new Size(1920, 1055);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(button2);
            Controls.Add(txt2);
            Controls.Add(txt1);
            Font = new Font("Segoe UI", 24F);
            FormBorderStyle = FormBorderStyle.None;
            MaximumSize = new Size(1920, 1080);
            MinimumSize = new Size(1918, 1018);
            Name = "logonui";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Wed., 31th December 8888";
            WindowState = FormWindowState.Maximized;
            FormClosing += logonui_FormClosing;
            Load += logonui_Load;
            KeyDown += logonui_KeyDown;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label txt1;
        private System.Windows.Forms.Label txt2;
        private System.Windows.Forms.Timer MinuteUpdate;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button txt6;
        private System.Windows.Forms.Label txt5;
        private System.Windows.Forms.TextBox boxpass;
        private System.Windows.Forms.Label txt4;
        private System.Windows.Forms.TextBox boxusr;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Label txt3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label txt9;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button txt13;
        private System.Windows.Forms.Label txt11;
        private System.Windows.Forms.TextBox boxregpass;
        private System.Windows.Forms.Label txt10;
        private System.Windows.Forms.TextBox boxregusr;
        private System.Windows.Forms.TextBox boxregpassag;
        private System.Windows.Forms.Label txt12;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}