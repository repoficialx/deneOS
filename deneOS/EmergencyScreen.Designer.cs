namespace deneOS
{
    partial class EmergencyScreen
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
            label1 = new Label();
            pictureBox1 = new PictureBox();
            label2 = new Label();
            button1 = new Button();
            label3 = new Label();
            label4 = new Label();
            timer1 = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Variable Display Semib", 32F, FontStyle.Bold);
            label1.ForeColor = SystemColors.ControlLightLight;
            label1.Location = new Point(144, 31);
            label1.Name = "label1";
            label1.Size = new Size(471, 57);
            label1.TabIndex = 0;
            label1.Text = "deneOS had a problem";
            label1.Click += label1_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.LightCoral;
            pictureBox1.Image = Properties.Resources.icons8_high_priority_100;
            pictureBox1.Location = new Point(27, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(118, 105);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Variable Display", 20F);
            label2.ForeColor = SystemColors.ControlLightLight;
            label2.Location = new Point(19, 133);
            label2.Name = "label2";
            label2.Size = new Size(590, 108);
            label2.TabIndex = 2;
            label2.Text = "We're sorry but your PC had a critical error and \r\ndeneOS stopped working. After 10 seconds, your\r\nPC will restart.";
            label2.Click += label2_Click;
            // 
            // button1
            // 
            button1.BackColor = Color.Indigo;
            button1.Font = new Font("Segoe UI Variable Display Semib", 30F);
            button1.ForeColor = SystemColors.ControlLightLight;
            button1.Location = new Point(27, 315);
            button1.Name = "button1";
            button1.Size = new Size(330, 95);
            button1.TabIndex = 3;
            button1.Text = "Restart now";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Variable Display Semib", 16F);
            label3.ForeColor = SystemColors.ControlDark;
            label3.Location = new Point(27, 275);
            label3.Name = "label3";
            label3.Size = new Size(409, 30);
            label3.TabIndex = 5;
            label3.Text = "Error code: CRITICAL_PROCESS_KILLED";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Variable Display Semib", 20F);
            label4.ForeColor = SystemColors.ControlLightLight;
            label4.Location = new Point(376, 347);
            label4.Name = "label4";
            label4.Size = new Size(304, 36);
            label4.TabIndex = 6;
            label4.Text = "Restarting in 10 seconds";
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;
            // 
            // EmergencyScreen
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.IndianRed;
            ClientSize = new Size(800, 450);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(button1);
            Controls.Add(label2);
            Controls.Add(pictureBox1);
            Controls.Add(label1);
            Cursor = Cursors.No;
            Font = new Font("Segoe UI Variable Display Semib", 40F);
            FormBorderStyle = FormBorderStyle.None;
            Name = "EmergencyScreen";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "CRITICAL ERROR";
            TopMost = true;
            WindowState = FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Timer timer1;
    }
}