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
            button1 = new Button();
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
            button2.BackColor = Color.Transparent;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("Segoe MDL2 Assets", 24F);
            button2.ForeColor = Color.Cyan;
            button2.Location = new Point(1820, 980);
            button2.Name = "button2";
            button2.Size = new Size(75, 57);
            button2.TabIndex = 100;
            button2.Text = "";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            button1.Location = new Point(1778, 766);
            button1.Name = "button1";
            button1.Size = new Size(130, 67);
            button1.TabIndex = 101;
            button1.Text = "Exit";
            button1.UseVisualStyleBackColor = true;
            button1.Visible = false;
            button1.Click += button1_Click;
            // 
            // logonui
            // 
            AutoScaleDimensions = new SizeF(120F, 120F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = SystemColors.Control;
            BackgroundImage = Properties.Resources.FY25_Pride2025__BKG_02_Desktop;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1920, 845);
            Controls.Add(button1);
            Controls.Add(button2);
            Controls.Add(txt2);
            Controls.Add(txt1);
            Font = new Font("Segoe UI", 24F);
            FormBorderStyle = FormBorderStyle.None;
            MaximumSize = new Size(1920, 1080);
            MinimumSize = new Size(1364, 718);
            Name = "logonui";
            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Maximized;
            FormClosing += logonui_FormClosing;
            Load += logonui_Load;
            KeyDown += logonui_KeyDown;
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label txt1;
        private System.Windows.Forms.Label txt2;
        private System.Windows.Forms.Timer MinuteUpdate;
        private System.Windows.Forms.Button button2;
        private Button button1;
    }
}