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
            flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            panel12 = new System.Windows.Forms.Panel();
            pictureBox2 = new System.Windows.Forms.PictureBox();
            label18 = new System.Windows.Forms.Label();
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            label15 = new System.Windows.Forms.Label();
            flowLayoutPanel2.SuspendLayout();
            panel12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // flowLayoutPanel2
            // 
            flowLayoutPanel2.Controls.Add(panel12);
            flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            flowLayoutPanel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            flowLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            flowLayoutPanel2.Size = new System.Drawing.Size(534, 906);
            flowLayoutPanel2.TabIndex = 3;
            flowLayoutPanel2.Paint += flowLayoutPanel2_Paint;
            // 
            // panel12
            // 
            panel12.Controls.Add(pictureBox2);
            panel12.Controls.Add(label18);
            panel12.Location = new System.Drawing.Point(20, 20);
            panel12.Margin = new System.Windows.Forms.Padding(20);
            panel12.Name = "panel12";
            panel12.Size = new System.Drawing.Size(74, 83);
            panel12.TabIndex = 0;
            panel12.Click += panel12_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = global::deneOS.Properties.Resources.denestore;
            pictureBox2.Location = new System.Drawing.Point(3, 3);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new System.Drawing.Size(68, 61);
            pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 0;
            pictureBox2.TabStop = false;
            // 
            // label18
            // 
            label18.Font = new System.Drawing.Font("Segoe UI", 9F);
            label18.Location = new System.Drawing.Point(3, 62);
            label18.Name = "label18";
            label18.Size = new System.Drawing.Size(68, 15);
            label18.TabIndex = 1;
            label18.Text = "Store";
            label18.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(label15, 0, 1);
            tableLayoutPanel1.Controls.Add(flowLayoutPanel2, 0, 0);
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 95F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            tableLayoutPanel1.Size = new System.Drawing.Size(540, 960);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // label15
            // 
            label15.Dock = System.Windows.Forms.DockStyle.Fill;
            label15.Location = new System.Drawing.Point(3, 912);
            label15.Name = "label15";
            label15.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            label15.Size = new System.Drawing.Size(534, 48);
            label15.TabIndex = 4;
            label15.Text = "deneOS v0.2b\r\nTest Mode";
            // 
            // HomeScreen
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            BackColor = System.Drawing.SystemColors.ControlText;
            ClientSize = new System.Drawing.Size(540, 960);
            ControlBox = false;
            Controls.Add(tableLayoutPanel1);
            Font = new System.Drawing.Font("Segoe UI", 12F);
            ForeColor = System.Drawing.SystemColors.Control;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "explorer";
            WindowState = System.Windows.Forms.FormWindowState.Maximized;
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