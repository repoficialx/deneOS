namespace deneOS.OOBE
{
    partial class Intro
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
            button1 = new Button();
            label1 = new Label();
            label2 = new Label();
            pictureBox1 = new PictureBox();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.BackColor = Color.Salmon;
            button1.Font = new Font("Agency FB", 21F);
            button1.ForeColor = SystemColors.ButtonHighlight;
            button1.Location = new Point(60, 406);
            button1.Name = "button1";
            button1.Size = new Size(139, 46);
            button1.TabIndex = 0;
            button1.Text = "COMENZAR";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Agency FB", 14F, FontStyle.Underline);
            label1.Location = new Point(199, 422);
            label1.Name = "label1";
            label1.Size = new Size(53, 28);
            label1.TabIndex = 1;
            label1.Text = "   DEV";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Agency FB", 14F, FontStyle.Underline);
            label2.Location = new Point(4, 423);
            label2.Name = "label2";
            label2.Size = new Size(57, 28);
            label2.TabIndex = 2;
            label2.Text = "SOS   ";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.denePhone1_Logo;
            pictureBox1.Location = new Point(10, 41);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(248, 57);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Segoe UI Variable Static Displa", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(12, 9);
            label3.Name = "label3";
            label3.Size = new Size(51, 24);
            label3.TabIndex = 4;
            label3.Text = "17:30";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Segoe Fluent Icons", 16F);
            label4.Location = new Point(220, 9);
            label4.Name = "label4";
            label4.Size = new Size(39, 27);
            label4.TabIndex = 5;
            label4.Text = "";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.Transparent;
            label5.Font = new Font("Segoe UI Light", 14F);
            label5.Location = new Point(177, 5);
            label5.Name = "label5";
            label5.Size = new Size(47, 32);
            label5.TabIndex = 6;
            label5.Text = "LTE";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = Color.Transparent;
            label6.Font = new Font("Segoe Fluent Icons", 16F);
            label6.Location = new Point(144, 9);
            label6.Name = "label6";
            label6.Size = new Size(39, 27);
            label6.TabIndex = 7;
            label6.Text = "";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.BackColor = Color.Transparent;
            label7.Font = new Font("Segoe UI Variable Static Displa", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.ForeColor = SystemColors.ControlLightLight;
            label7.Location = new Point(1, 106);
            label7.Name = "label7";
            label7.Size = new Size(273, 44);
            label7.TabIndex = 8;
            label7.Text = "Bienvenido a tu denePhone 1.\r\nEl ecosistema RLX comienza aquí.";
            label7.TextAlign = ContentAlignment.TopCenter;
            // 
            // Intro
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.gradiente8;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(270, 480);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(pictureBox1);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(button1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Intro";
            Text = "Intro";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Label label1;
        private Label label2;
        private PictureBox pictureBox1;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
    }
}