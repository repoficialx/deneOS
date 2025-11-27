namespace deneOS.OOBE
{
    partial class PCPrivacy
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
            label1 = new Label();
            panel1 = new Panel();
            label3 = new Label();
            button1 = new Button();
            protonvpn = new CheckBox();
            ddgo = new CheckBox();
            amaps = new CheckBox();
            linkLabel3 = new LinkLabel();
            linkLabel2 = new LinkLabel();
            linkLabel1 = new LinkLabel();
            label2 = new Label();
            pictureBox1 = new PictureBox();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(273, 9);
            label1.Name = "label1";
            label1.Size = new Size(148, 20);
            label1.TabIndex = 7;
            label1.Text = "Welcome to deneOS.";
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ControlLight;
            panel1.Controls.Add(label3);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(protonvpn);
            panel1.Controls.Add(ddgo);
            panel1.Controls.Add(amaps);
            panel1.Controls.Add(linkLabel3);
            panel1.Controls.Add(linkLabel2);
            panel1.Controls.Add(linkLabel1);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(pictureBox1);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 35);
            panel1.Name = "panel1";
            panel1.Size = new Size(707, 368);
            panel1.TabIndex = 6;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 6F);
            label3.Location = new Point(12, 335);
            label3.Name = "label3";
            label3.Size = new Size(368, 24);
            label3.TabIndex = 12;
            label3.Text = "deneOS does not collect any kind of user data.\r\nIf any of your data is collected is because you use other apps or modified versions.";
            // 
            // button1
            // 
            button1.Location = new Point(305, 303);
            button1.Name = "button1";
            button1.Size = new Size(130, 29);
            button1.TabIndex = 11;
            button1.Text = "Set up account";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // protonvpn
            // 
            protonvpn.AutoSize = true;
            protonvpn.Font = new Font("Segoe UI", 14F);
            protonvpn.Location = new Point(22, 207);
            protonvpn.Name = "protonvpn";
            protonvpn.Size = new Size(275, 36);
            protonvpn.TabIndex = 10;
            protonvpn.Text = "Download Proton VPN";
            protonvpn.UseVisualStyleBackColor = true;
            // 
            // ddgo
            // 
            ddgo.AutoSize = true;
            ddgo.Font = new Font("Segoe UI", 14F);
            ddgo.Location = new Point(22, 160);
            ddgo.Name = "ddgo";
            ddgo.Size = new Size(289, 36);
            ddgo.TabIndex = 9;
            ddgo.Text = "Download DuckDuckGo";
            ddgo.UseVisualStyleBackColor = true;
            // 
            // amaps
            // 
            amaps.AutoSize = true;
            amaps.Font = new Font("Segoe UI", 14F);
            amaps.Location = new Point(22, 113);
            amaps.Name = "amaps";
            amaps.Size = new Size(372, 36);
            amaps.TabIndex = 8;
            amaps.Text = "Download PWAEXE Apple Maps";
            amaps.UseVisualStyleBackColor = true;
            // 
            // linkLabel3
            // 
            linkLabel3.AutoSize = true;
            linkLabel3.Location = new Point(422, 339);
            linkLabel3.Name = "linkLabel3";
            linkLabel3.Size = new Size(79, 20);
            linkLabel3.TabIndex = 7;
            linkLabel3.TabStop = true;
            linkLabel3.Text = "Contribute";
            // 
            // linkLabel2
            // 
            linkLabel2.AutoSize = true;
            linkLabel2.Location = new Point(554, 339);
            linkLabel2.Name = "linkLabel2";
            linkLabel2.Size = new Size(141, 20);
            linkLabel2.TabIndex = 6;
            linkLabel2.TabStop = true;
            linkLabel2.Text = "Emergency Desktop";
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Location = new Point(507, 339);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(41, 20);
            linkLabel1.TabIndex = 5;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "Help";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Variable Display Semib", 18F);
            label2.Location = new Point(150, 44);
            label2.Name = "label2";
            label2.Size = new Size(364, 40);
            label2.TabIndex = 1;
            label2.Text = "Your privacy, our priority.";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.icons8_privacy_policy_100;
            pictureBox1.Location = new Point(22, 18);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(95, 89);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // PCPrivacy
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(707, 403);
            Controls.Add(label1);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "PCPrivacy";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "PCPrivacy";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Panel panel1;
        private LinkLabel linkLabel3;
        private LinkLabel linkLabel2;
        private LinkLabel linkLabel1;
        private Label label2;
        private PictureBox pictureBox1;
        private Button button1;
        private CheckBox protonvpn;
        private CheckBox ddgo;
        private CheckBox amaps;
        private Label label3;
    }
}