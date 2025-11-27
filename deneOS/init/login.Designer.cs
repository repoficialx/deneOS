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
            txt3 = new Label();
            boxpass = new TextBox();
            boxusr = new TextBox();
            txt5 = new Label();
            txt4 = new Label();
            pictureBox1 = new PictureBox();
            txt6 = new Button();
            tableLayoutPanel1 = new TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // txt3
            // 
            txt3.AutoSize = true;
            txt3.Font = new Font("Segoe UI Variable Display", 9F);
            txt3.ForeColor = SystemColors.ControlText;
            txt3.Location = new Point(3, 0);
            txt3.Name = "txt3";
            txt3.Padding = new Padding(5, 5, 0, 0);
            txt3.Size = new Size(41, 25);
            txt3.TabIndex = 24;
            txt3.Text = "Txt3";
            // 
            // boxpass
            // 
            boxpass.BackColor = SystemColors.Control;
            boxpass.Font = new Font("Segoe UI Variable Display", 20F);
            boxpass.ForeColor = SystemColors.ControlText;
            boxpass.Location = new Point(594, 307);
            boxpass.Name = "boxpass";
            boxpass.Size = new Size(216, 52);
            boxpass.TabIndex = 20;
            // 
            // boxusr
            // 
            boxusr.BackColor = SystemColors.Control;
            boxusr.Font = new Font("Segoe UI Variable Display", 20F);
            boxusr.ForeColor = SystemColors.ControlText;
            boxusr.Location = new Point(594, 243);
            boxusr.Name = "boxusr";
            boxusr.Size = new Size(216, 52);
            boxusr.TabIndex = 18;
            // 
            // txt5
            // 
            txt5.Dock = DockStyle.Fill;
            txt5.Font = new Font("Segoe UI Variable Display", 24F);
            txt5.ForeColor = SystemColors.Control;
            txt5.Location = new Point(119, 304);
            txt5.Name = "txt5";
            txt5.Size = new Size(227, 72);
            txt5.TabIndex = 21;
            txt5.Text = "Txt5";
            txt5.TextAlign = ContentAlignment.TopRight;
            // 
            // txt4
            // 
            txt4.Dock = DockStyle.Fill;
            txt4.Font = new Font("Segoe UI Variable Display", 24F);
            txt4.ForeColor = SystemColors.Control;
            txt4.Location = new Point(119, 240);
            txt4.Name = "txt4";
            txt4.Size = new Size(227, 64);
            txt4.TabIndex = 19;
            txt4.Text = "Txt4";
            txt4.TextAlign = ContentAlignment.TopRight;
            // 
            // pictureBox1
            // 
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.Image = Properties.Resources.DefaultAccountTile;
            pictureBox1.Location = new Point(352, 58);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(236, 179);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 25;
            pictureBox1.TabStop = false;
            // 
            // txt6
            // 
            txt6.BackColor = SystemColors.Control;
            txt6.Dock = DockStyle.Top;
            txt6.FlatStyle = FlatStyle.Popup;
            txt6.ForeColor = SystemColors.ControlText;
            txt6.Location = new Point(352, 379);
            txt6.Name = "txt6";
            txt6.Size = new Size(236, 45);
            txt6.TabIndex = 22;
            txt6.Text = "Txt6";
            txt6.UseVisualStyleBackColor = false;
            txt6.Click += txt6_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.BackColor = Color.Transparent;
            tableLayoutPanel1.ColumnCount = 6;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.395833F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 24.8665962F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25.8271084F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 29.9893284F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 6.97916651F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Controls.Add(txt3, 0, 0);
            tableLayoutPanel1.Controls.Add(boxpass, 3, 3);
            tableLayoutPanel1.Controls.Add(boxusr, 3, 2);
            tableLayoutPanel1.Controls.Add(txt5, 1, 3);
            tableLayoutPanel1.Controls.Add(txt4, 1, 2);
            tableLayoutPanel1.Controls.Add(pictureBox1, 2, 1);
            tableLayoutPanel1.Controls.Add(txt6, 2, 4);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 6;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10.7407408F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 35.7833672F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 12.37911F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 13.9264994F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 27.272728F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(960, 540);
            tableLayoutPanel1.TabIndex = 27;
            tableLayoutPanel1.Paint += tableLayoutPanel1_Paint;
            // 
            // login
            // 
            AutoScaleDimensions = new SizeF(120F, 120F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = SystemColors.ControlText;
            BackgroundImage = Properties.Resources.gradiente;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(960, 540);
            Controls.Add(tableLayoutPanel1);
            Font = new Font("Segoe UI Variable Display", 14F);
            ForeColor = SystemColors.Control;
            FormBorderStyle = FormBorderStyle.None;
            Name = "login";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "login";
            TopMost = true;
            WindowState = FormWindowState.Maximized;
            Load += login_Load;
            Click += login_Click;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label txt3;
        private TextBox boxpass;
        private TextBox boxusr;
        private Label txt5;
        private Label txt4;
        private PictureBox pictureBox1;
        private Button txt6;
        private TableLayoutPanel tableLayoutPanel1;
    }
}