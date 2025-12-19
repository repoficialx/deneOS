namespace deneOS.init
{
    partial class logonuiVertical
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
            txt1 = new Label();
            txt2 = new Label();
            MinuteUpdate = new System.Windows.Forms.Timer(components);
            button2 = new Button();
            SuspendLayout();
            // 
            // txt1
            // 
            txt1.AutoSize = true;
            txt1.BackColor = Color.Transparent;
            txt1.Font = new Font("Segoe UI Variable Display Light", 90F);
            txt1.ForeColor = SystemColors.Control;
            txt1.Location = new Point(132, 95);
            txt1.Name = "txt1";
            txt1.Size = new Size(293, 159);
            txt1.TabIndex = 0;
            txt1.Text = "TXT1";
            txt1.Click += txt1_Click;
            // 
            // txt2
            // 
            txt2.AutoSize = true;
            txt2.BackColor = Color.Transparent;
            txt2.Font = new Font("Segoe UI Variable Display", 32F);
            txt2.ForeColor = SystemColors.Control;
            txt2.Location = new Point(213, 821);
            txt2.Name = "txt2";
            txt2.Size = new Size(118, 57);
            txt2.TabIndex = 1;
            txt2.Text = "TXT2";
            // 
            // MinuteUpdate
            // 
            MinuteUpdate.Enabled = true;
            MinuteUpdate.Interval = 1;
            MinuteUpdate.Tick += MinuteUpdate_Tick;
            // 
            // button2
            // 
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("Segoe MDL2 Assets", 24F);
            button2.ForeColor = SystemColors.Control;
            button2.Location = new Point(993, 12);
            button2.Name = "button2";
            button2.Size = new Size(75, 57);
            button2.TabIndex = 100;
            button2.Text = "";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // logonuiVertical
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = SystemColors.ControlText;
            BackgroundImage = Properties.Resources.gradiente;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(540, 960);
            Controls.Add(button2);
            Controls.Add(txt2);
            Controls.Add(txt1);
            Font = new Font("Segoe UI", 24F);
            FormBorderStyle = FormBorderStyle.None;
            Name = "logonuiVertical";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "deneOS";
            WindowState = FormWindowState.Maximized;
            FormClosing += logonui_FormClosing;
            Load += logonui_Load;
            KeyDown += logonui_KeyDown;
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label txt1;
        private System.Windows.Forms.Label txt2;
        private System.Windows.Forms.Timer MinuteUpdate;
        private System.Windows.Forms.Button button2;
    }
}