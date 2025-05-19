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
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            button1 = new Button();
            comboBox1 = new ComboBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe Fluent Icons", 48F);
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(114, 80);
            label1.TabIndex = 0;
            label1.Text = "";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = SystemColors.Control;
            label2.Font = new Font("Segoe Fluent Icons", 52F, FontStyle.Bold);
            label2.ForeColor = SystemColors.Highlight;
            label2.Location = new Point(12, 145);
            label2.Name = "label2";
            label2.Size = new Size(125, 87);
            label2.TabIndex = 1;
            label2.Text = "";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Material Icons", 20F);
            label3.Location = new Point(12, 9);
            label3.Name = "label3";
            label3.Size = new Size(49, 34);
            label3.TabIndex = 2;
            label3.Text = "5g";
            label3.Click += label3_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Material Icons", 20F);
            label4.ForeColor = SystemColors.Highlight;
            label4.Location = new Point(88, 209);
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
            label5.Location = new Point(194, 31);
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
            label6.Location = new Point(194, 169);
            label6.Name = "label6";
            label6.Size = new Size(378, 46);
            label6.TabIndex = 5;
            label6.Text = "WiFi: {GET_WIFI_NAME}";
            // 
            // button1
            // 
            button1.FlatStyle = FlatStyle.Popup;
            button1.Font = new Font("Segoe UI Light", 12F);
            button1.Location = new Point(62, 244);
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
            comboBox1.Items.AddRange(new object[] { "802.11a", "802.11b", "802.11g", "802.11ac", "802.11ax", "802.11be", "802.11bn" });
            comboBox1.Location = new Point(244, 244);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(179, 36);
            comboBox1.TabIndex = 7;
            comboBox1.Text = "802.11ac";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(120F, 120F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(696, 413);
            Controls.Add(comboBox1);
            Controls.Add(button1);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Internet";
            Load += Form1_Load;
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
    }
}
