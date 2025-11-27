namespace deneOS
{
    partial class TopBar
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
            panel1 = new Panel();
            flowLayoutPanel1 = new FlowLayoutPanel();
            panel2 = new Panel();
            panel16 = new Panel();
            flowLayoutPanel3 = new FlowLayoutPanel();
            panel17 = new Panel();
            label20 = new Label();
            label19 = new Label();
            label22 = new Label();
            label23 = new Label();
            label24 = new Label();
            panel1.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            panel2.SuspendLayout();
            panel16.SuspendLayout();
            flowLayoutPanel3.SuspendLayout();
            panel17.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(flowLayoutPanel1);
            panel1.Controls.Add(panel16);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1080, 60);
            panel1.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(panel2);
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.Location = new Point(0, 0);
            flowLayoutPanel1.Margin = new Padding(0, 3, 3, 3);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(556, 60);
            flowLayoutPanel1.TabIndex = 2;
            flowLayoutPanel1.Paint += flowLayoutPanel1_Paint;
            // 
            // panel2
            // 
            panel2.Controls.Add(panel17);
            panel2.Location = new Point(3, 3);
            panel2.Name = "panel2";
            panel2.Size = new Size(180, 60);
            panel2.TabIndex = 3;
            // 
            // panel16
            // 
            panel16.Controls.Add(flowLayoutPanel3);
            panel16.Dock = DockStyle.Right;
            panel16.Location = new Point(556, 0);
            panel16.Name = "panel16";
            panel16.Size = new Size(524, 60);
            panel16.TabIndex = 1;
            // 
            // flowLayoutPanel3
            // 
            flowLayoutPanel3.Controls.Add(label22);
            flowLayoutPanel3.Controls.Add(label23);
            flowLayoutPanel3.Controls.Add(label24);
            flowLayoutPanel3.Dock = DockStyle.Fill;
            flowLayoutPanel3.FlowDirection = FlowDirection.RightToLeft;
            flowLayoutPanel3.Location = new Point(0, 0);
            flowLayoutPanel3.Name = "flowLayoutPanel3";
            flowLayoutPanel3.Size = new Size(524, 60);
            flowLayoutPanel3.TabIndex = 0;
            // 
            // panel17
            // 
            panel17.Controls.Add(label20);
            panel17.Controls.Add(label19);
            panel17.Dock = DockStyle.Fill;
            panel17.Location = new Point(0, 0);
            panel17.Name = "panel17";
            panel17.Size = new Size(180, 60);
            panel17.TabIndex = 0;
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Font = new Font("Segoe UI", 6F);
            label20.Location = new Point(123, 45);
            label20.Name = "label20";
            label20.Size = new Size(53, 12);
            label20.TabIndex = 1;
            label20.Text = "00/00/0000";
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Font = new Font("Segoe UI", 30F);
            label19.Location = new Point(0, -7);
            label19.Name = "label19";
            label19.Size = new Size(148, 67);
            label19.TabIndex = 0;
            label19.Text = "00:00";
            // 
            // label22
            // 
            label22.Font = new Font("Segoe Fluent Icons", 16F);
            label22.Location = new Point(479, 0);
            label22.Name = "label22";
            label22.Size = new Size(42, 58);
            label22.TabIndex = 4;
            label22.Text = "";
            label22.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label23
            // 
            label23.Font = new Font("Segoe Fluent Icons", 16F);
            label23.Location = new Point(437, 0);
            label23.Name = "label23";
            label23.Size = new Size(36, 62);
            label23.TabIndex = 5;
            label23.Text = "";
            label23.TextAlign = ContentAlignment.MiddleRight;
            label23.Click += label23_Click;
            // 
            // label24
            // 
            label24.Font = new Font("Segoe Fluent Icons", 16F);
            label24.Location = new Point(392, 0);
            label24.Name = "label24";
            label24.Size = new Size(39, 58);
            label24.TabIndex = 6;
            label24.Text = "";
            label24.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // TopBar
            // 
            AutoScaleDimensions = new SizeF(120F, 120F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = SystemColors.ControlText;
            ClientSize = new Size(1080, 60);
            Controls.Add(panel1);
            Font = new Font("Segoe UI Variable Display", 12F);
            ForeColor = SystemColors.Control;
            FormBorderStyle = FormBorderStyle.None;
            Name = "TopBar";
            StartPosition = FormStartPosition.Manual;
            Text = "tbar";
            FormClosing += tbar_FormClosing;
            Load += tbar_Load;
            panel1.ResumeLayout(false);
            flowLayoutPanel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel16.ResumeLayout(false);
            flowLayoutPanel3.ResumeLayout(false);
            panel17.ResumeLayout(false);
            panel17.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel16;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.Panel panel17;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}