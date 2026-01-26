namespace deneStore
{
    partial class welcomeScreen
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(welcomeScreen));
            label1 = new Label();
            pictureBox1 = new PictureBox();
            label2 = new Label();
            pictureBox2 = new PictureBox();
            label3 = new Label();
            button1 = new Button();
            checkBox1 = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(49, 9);
            label1.Name = "label1";
            label1.Size = new Size(309, 189);
            label1.TabIndex = 0;
            label1.Text = "Welcome to deneStore!\r\n\r\nThese apps are designed specially \r\nfor deneOS.\r\n\r\nThese apps run faster, smoother, safer and \r\nare more integrated into deneOS.\r\n\r\nAlways install from deneStore.";
            label1.TextAlign = ContentAlignment.TopCenter;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.official;
            pictureBox1.Location = new Point(12, 255);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(128, 128);
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(146, 286);
            label2.Name = "label2";
            label2.Size = new Size(182, 63);
            label2.TabIndex = 2;
            label2.Text = "deneStore\r\nApps made for deneOS.\r\nSafer. Smoother. Lighter.";
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources.denelogo;
            pictureBox2.Location = new Point(12, 389);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(128, 128);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 3;
            pictureBox2.TabStop = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(146, 417);
            label3.Name = "label3";
            label3.Size = new Size(185, 63);
            label3.TabIndex = 4;
            label3.Text = "deneOS.\r\ndeneOS is not just a skin.\r\ndeneOS is a super-shell.";
            // 
            // button1
            // 
            button1.Location = new Point(199, 523);
            button1.Name = "button1";
            button1.Size = new Size(199, 38);
            button1.TabIndex = 5;
            button1.Text = "OK";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(12, 530);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(148, 25);
            checkBox1.TabIndex = 6;
            checkBox1.Text = "Don't remind me";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // welcomeScreen
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(410, 573);
            Controls.Add(checkBox1);
            Controls.Add(button1);
            Controls.Add(label3);
            Controls.Add(pictureBox2);
            Controls.Add(label2);
            Controls.Add(pictureBox1);
            Controls.Add(label1);
            Font = new Font("Segoe UI Variable Display", 12F);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "welcomeScreen";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "deneStore";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private PictureBox pictureBox1;
        private Label label2;
        private PictureBox pictureBox2;
        private Label label3;
        private Button button1;
        private CheckBox checkBox1;
    }
}
