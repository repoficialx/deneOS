namespace Internet
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            button1 = new Button();
            comboBox1 = new ComboBox();
            label7 = new Label();
            groupBox1 = new GroupBox();
            groupBox2 = new GroupBox();
            button2 = new Button();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe Fluent Icons", 48F);
            label1.Location = new Point(6, 23);
            label1.Name = "label1";
            label1.Size = new Size(114, 80);
            label1.TabIndex = 0;
            label1.Text = "";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Segoe Fluent Icons", 52F, FontStyle.Bold);
            label2.ForeColor = SystemColors.Highlight;
            label2.Location = new Point(24, 53);
            label2.Name = "label2";
            label2.Size = new Size(125, 87);
            label2.TabIndex = 1;
            label2.Text = "";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Material Icons", 20F);
            label3.ForeColor = SystemColors.Highlight;
            label3.Location = new Point(6, 23);
            label3.Name = "label3";
            label3.Size = new Size(49, 34);
            label3.TabIndex = 2;
            label3.Text = "filter_6";
            label3.Click += label3_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Material Icons", 20F);
            label4.ForeColor = SystemColors.Highlight;
            label4.Location = new Point(142, 106);
            label4.Name = "label4";
            label4.Size = new Size(49, 34);
            label4.TabIndex = 3;
            label4.Text = "looks_6";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI Variable Display Semib", 20F);
            label5.ForeColor = Color.Gray;
            label5.Location = new Point(126, 40);
            label5.Name = "label5";
            label5.Size = new Size(319, 46);
            label5.TabIndex = 4;
            label5.Text = "Mobile Data: {MDS}";
            label5.Click += label5_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI Variable Display Semib", 20F);
            label6.Location = new Point(164, 53);
            label6.Name = "label6";
            label6.Size = new Size(378, 46);
            label6.TabIndex = 5;
            label6.Text = "WiFi: {GET_WIFI_NAME}";
            // 
            // button1
            // 
            button1.FlatStyle = FlatStyle.Popup;
            button1.Font = new Font("Segoe UI Light", 12F);
            button1.Location = new Point(121, 305);
            button1.Name = "button1";
            button1.Size = new Size(151, 34);
            button1.TabIndex = 6;
            button1.Text = "add network";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // comboBox1
            // 
            comboBox1.FlatStyle = FlatStyle.Popup;
            comboBox1.Font = new Font("Segoe UI Light", 12F);
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "802.11n", "802.11ac", "802.11ax", "802.11be" });
            comboBox1.Location = new Point(354, 303);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(179, 36);
            comboBox1.TabIndex = 7;
            comboBox1.Text = "802.11ac";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 4.5F);
            label7.Location = new Point(-1, 402);
            label7.Name = "label7";
            label7.Size = new Size(566, 11);
            label7.TabIndex = 8;
            label7.Text = "Internet es una app desarrollada entera por Ray (@repoficialx); no hubieron contribudores de open source. App diseñada para deneOS. Detección vía netsh";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(670, 117);
            groupBox1.TabIndex = 9;
            groupBox1.TabStop = false;
            groupBox1.Text = "Mobile Data";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(button2);
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(label6);
            groupBox2.Location = new Point(12, 135);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(670, 164);
            groupBox2.TabIndex = 10;
            groupBox2.TabStop = false;
            groupBox2.Text = "Wireless Local Area Network (WLAN)";
            // 
            // button2
            // 
            button2.BackColor = SystemColors.Control;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("Material Icons", 10F);
            button2.Location = new Point(262, 0);
            button2.Name = "button2";
            button2.Size = new Size(31, 26);
            button2.TabIndex = 6;
            button2.Text = "info";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(120F, 120F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(694, 413);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(label7);
            Controls.Add(comboBox1);
            Controls.Add(button1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Internet";
            Load += Form1_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Button button1;
        private ComboBox comboBox1;
        private Label label7;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Button button2;
    }
}
