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
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // txt1
            // 
            txt1.AutoSize = true;
            txt1.BackColor = Color.Transparent;
            txt1.Dock = DockStyle.Fill;
            txt1.Font = new Font("Segoe UI Variable Display", 72F);
            txt1.ForeColor = SystemColors.Control;
            txt1.Location = new Point(3, 378);
            txt1.Name = "txt1";
            txt1.Size = new Size(762, 102);
            txt1.TabIndex = 0;
            txt1.Text = "TXT1";
            txt1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txt2
            // 
            txt2.AutoSize = true;
            txt2.BackColor = Color.Transparent;
            txt2.Dock = DockStyle.Fill;
            txt2.Font = new Font("Segoe UI Variable Display", 38F);
            txt2.ForeColor = SystemColors.Control;
            txt2.Location = new Point(3, 480);
            txt2.Name = "txt2";
            txt2.Size = new Size(762, 60);
            txt2.TabIndex = 1;
            txt2.Text = "TXT2";
            txt2.TextAlign = ContentAlignment.MiddleLeft;
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
            button2.Dock = DockStyle.Fill;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("Segoe MDL2 Assets", 32F, FontStyle.Bold);
            button2.ForeColor = Color.DarkMagenta;
            button2.Location = new Point(886, 483);
            button2.Name = "button2";
            button2.Size = new Size(71, 54);
            button2.TabIndex = 100;
            button2.Text = "";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            button1.Dock = DockStyle.Fill;
            button1.Location = new Point(771, 483);
            button1.Name = "button1";
            button1.Size = new Size(109, 54);
            button1.TabIndex = 101;
            button1.Text = "Exit";
            button1.UseVisualStyleBackColor = true;
            button1.Visible = false;
            button1.Click += button1_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.BackColor = Color.Transparent;
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 8F));
            tableLayoutPanel1.Controls.Add(button2, 2, 2);
            tableLayoutPanel1.Controls.Add(button1, 1, 2);
            tableLayoutPanel1.Controls.Add(txt1, 0, 1);
            tableLayoutPanel1.Controls.Add(txt2, 0, 2);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 70F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 19F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 11F));
            tableLayoutPanel1.Size = new Size(960, 540);
            tableLayoutPanel1.TabIndex = 102;
            // 
            // logonui
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = SystemColors.Control;
            BackgroundImage = Properties.Resources.FY25_Pride2025__BKG_02_Desktop;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(960, 540);
            Controls.Add(tableLayoutPanel1);
            Font = new Font("Segoe UI", 24F);
            FormBorderStyle = FormBorderStyle.None;
            Name = "logonui";
            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Maximized;
            FormClosing += logonui_FormClosing;
            Load += logonui_Load;
            KeyDown += logonui_KeyDown;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label txt1;
        private System.Windows.Forms.Label txt2;
        private System.Windows.Forms.Timer MinuteUpdate;
        private System.Windows.Forms.Button button2;
        private Button button1;
        private TableLayoutPanel tableLayoutPanel1;
    }
}