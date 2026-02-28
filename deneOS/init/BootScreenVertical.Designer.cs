namespace deneOS.init
{
    partial class BootScreenVertical
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
            timer1 = new System.Windows.Forms.Timer(components);
            logo = new PictureBox();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)logo).BeginInit();
            SuspendLayout();
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Interval = 6000;
            timer1.Tick += timer1_Tick;
            // 
            // logo
            // 
            logo.Dock = DockStyle.Fill;
            logo.Image = Properties.Resources.denelogo;
            logo.Location = new Point(0, 0);
            logo.Name = "logo";
            logo.Size = new Size(540, 757);
            logo.SizeMode = PictureBoxSizeMode.Zoom;
            logo.TabIndex = 0;
            logo.TabStop = false;
            // 
            // label1
            // 
            label1.Dock = DockStyle.Bottom;
            label1.Font = new Font("Segoe Boot Semilight", 52F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(0, 757);
            label1.Name = "label1";
            label1.Size = new Size(540, 203);
            label1.TabIndex = 2;
            label1.Text = "";
            label1.TextAlign = ContentAlignment.TopCenter;
            // 
            // BootScreenVertical
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = SystemColors.ControlText;
            ClientSize = new Size(540, 960);
            ControlBox = false;
            Controls.Add(logo);
            Controls.Add(label1);
            Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = SystemColors.Control;
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "BootScreenVertical";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "deninit";
            WindowState = FormWindowState.Maximized;
            Load += BootScreen_Load;
            ((System.ComponentModel.ISupportInitialize)logo).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox logo;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label1;
    }
}