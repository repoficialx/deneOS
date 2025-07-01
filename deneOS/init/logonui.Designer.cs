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
    }
}