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
            label15 = new Label();
            flowLayoutPanel2 = new FlowLayoutPanel();
            panel12 = new Panel();
            label18 = new Label();
            pictureBox2 = new PictureBox();
            panel1 = new Panel();
            flowLayoutPanel2.SuspendLayout();
            panel12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(836, 0);
            label15.Name = "label15";
            label15.RightToLeft = RightToLeft.Yes;
            label15.Size = new Size(244, 56);
            label15.TabIndex = 2;
            label15.Text = "deneOS Concept Testing\r\nNot Final - Concept v0.1-b";
            // 
            // flowLayoutPanel2
            // 
            flowLayoutPanel2.Controls.Add(panel12);
            flowLayoutPanel2.Dock = DockStyle.Fill;
            flowLayoutPanel2.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel2.Font = new Font("Microsoft Sans Serif", 8.25F);
            flowLayoutPanel2.Location = new Point(0, 0);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            flowLayoutPanel2.Size = new Size(1080, 1102);
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
            panel12.Size = new Size(152, 185);
            panel12.TabIndex = 0;
            panel12.Click += panel12_Click;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Font = new Font("Segoe UI", 18F);
            label18.Location = new Point(15, 144);
            label18.Name = "label18";
            label18.Size = new Size(122, 41);
            label18.TabIndex = 1;
            label18.Text = "dnStore";
            label18.TextAlign = ContentAlignment.TopCenter;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources.denestore;
            pictureBox2.Location = new Point(3, 3);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(148, 144);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 0;
            pictureBox2.TabStop = false;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Transparent;
            panel1.Controls.Add(label15);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 1046);
            panel1.Name = "panel1";
            panel1.Size = new Size(1080, 56);
            panel1.TabIndex = 4;
            // 
            // HomeScreen
            // 
            AutoScaleDimensions = new SizeF(120F, 120F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = SystemColors.ControlText;
            ClientSize = new Size(1080, 1102);
            ControlBox = false;
            Controls.Add(panel1);
            Controls.Add(flowLayoutPanel2);
            Font = new Font("Segoe UI", 12F);
            ForeColor = SystemColors.Control;
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "HomeScreen";
            ShowIcon = false;
            StartPosition = FormStartPosition.Manual;
            Text = "explorer";
            FormClosing += desktop_FormClosing;
            Load += desktop_Load;
            flowLayoutPanel2.ResumeLayout(false);
            panel12.ResumeLayout(false);
            panel12.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Panel panel1;
    }
}