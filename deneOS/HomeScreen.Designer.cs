namespace deneOS
{
    partial class HomeScreen
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
            flowLayoutPanel2 = new FlowLayoutPanel();
            panel12 = new Panel();
            pictureBox2 = new PictureBox();
            label18 = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            label15 = new Label();
            flowLayoutPanel2.SuspendLayout();
            panel12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // flowLayoutPanel2
            // 
            flowLayoutPanel2.Controls.Add(panel12);
            flowLayoutPanel2.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel2.Font = new Font("Microsoft Sans Serif", 8.25F);
            flowLayoutPanel2.Location = new Point(3, 3);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            flowLayoutPanel2.Size = new Size(534, 906);
            flowLayoutPanel2.TabIndex = 3;
            flowLayoutPanel2.Paint += flowLayoutPanel2_Paint;
            // 
            // panel12
            // 
            panel12.Controls.Add(pictureBox2);
            panel12.Controls.Add(label18);
            panel12.Location = new Point(20, 20);
            panel12.Margin = new Padding(20);
            panel12.Name = "panel12";
            panel12.Size = new Size(74, 83);
            panel12.TabIndex = 0;
            panel12.Click += panel12_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources.denestore;
            pictureBox2.Location = new Point(3, 3);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(68, 61);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 0;
            pictureBox2.TabStop = false;
            // 
            // label18
            // 
            label18.Font = new Font("Segoe UI", 9F);
            label18.Location = new Point(3, 62);
            label18.Name = "label18";
            label18.Size = new Size(68, 15);
            label18.TabIndex = 1;
            label18.Text = "Store";
            label18.TextAlign = ContentAlignment.TopCenter;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(label15, 0, 1);
            tableLayoutPanel1.Controls.Add(flowLayoutPanel2, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 95F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.Size = new Size(540, 960);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // label15
            // 
            label15.Dock = DockStyle.Fill;
            label15.Location = new Point(3, 912);
            label15.Name = "label15";
            label15.RightToLeft = RightToLeft.Yes;
            label15.Size = new Size(534, 48);
            label15.TabIndex = 4;
            label15.Text = "deneOS v0.2b\r\nTest Mode";
            // 
            // HomeScreen
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = SystemColors.ControlText;
            ClientSize = new Size(540, 960);
            ControlBox = false;
            Controls.Add(tableLayoutPanel1);
            Font = new Font("Segoe UI", 12F);
            ForeColor = SystemColors.Control;
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "HomeScreen";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "explorer";
            WindowState = FormWindowState.Maximized;
            FormClosing += desktop_FormClosing;
            Load += desktop_Load;
            flowLayoutPanel2.ResumeLayout(false);
            panel12.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.PictureBox pictureBox2;
        private TableLayoutPanel tableLayoutPanel1;
        private Label label15;
    }
}