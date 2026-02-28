namespace DPKBundler
{
    partial class BundleSEA
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
            textBox1 = new TextBox();
            label1 = new Label();
            button1 = new Button();
            openFileDialog1 = new OpenFileDialog();
            button2 = new Button();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            maskedTextBox1 = new MaskedTextBox();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            button3 = new Button();
            button4 = new Button();
            saveFileDialog1 = new SaveFileDialog();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Location = new Point(12, 27);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(262, 23);
            textBox1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(202, 15);
            label1.TabIndex = 1;
            label1.Text = "Select an executable file (.exe or .wpi)";
            // 
            // button1
            // 
            button1.Location = new Point(280, 27);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 2;
            button1.Text = "Explore...";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // openFileDialog1
            // 
            openFileDialog1.DefaultExt = "exe";
            openFileDialog1.FileName = "openFileDialog1";
            openFileDialog1.Filter = "Windows Executables|*.exe|Windoze Executables|*.wze|deneOS Old Executables|*.dna|deneOS Executables|*.wpi";
            openFileDialog1.FileOk += openFileDialog1_FileOk;
            // 
            // button2
            // 
            button2.Location = new Point(12, 56);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 3;
            button2.Text = "OK";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 95);
            label2.Name = "label2";
            label2.Size = new Size(141, 15);
            label2.TabIndex = 4;
            label2.Text = "Fill metadata information";
            label2.Click += label2_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(27, 111);
            label3.Name = "label3";
            label3.Size = new Size(39, 15);
            label3.TabIndex = 5;
            label3.Text = "Name";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(21, 132);
            label4.Name = "label4";
            label4.Size = new Size(45, 15);
            label4.TabIndex = 6;
            label4.Text = "Version";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(22, 154);
            label5.Name = "label5";
            label5.Size = new Size(44, 15);
            label5.TabIndex = 7;
            label5.Text = "Author";
            // 
            // maskedTextBox1
            // 
            maskedTextBox1.Enabled = false;
            maskedTextBox1.Font = new Font("Segoe UI", 7F);
            maskedTextBox1.Location = new Point(72, 131);
            maskedTextBox1.Mask = "0.0.0";
            maskedTextBox1.Name = "maskedTextBox1";
            maskedTextBox1.Size = new Size(100, 20);
            maskedTextBox1.TabIndex = 9;
            // 
            // textBox2
            // 
            textBox2.Enabled = false;
            textBox2.Font = new Font("Segoe UI", 7F);
            textBox2.Location = new Point(72, 110);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(100, 20);
            textBox2.TabIndex = 8;
            // 
            // textBox3
            // 
            textBox3.Enabled = false;
            textBox3.Font = new Font("Segoe UI", 7F);
            textBox3.Location = new Point(72, 153);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(100, 20);
            textBox3.TabIndex = 10;
            // 
            // button3
            // 
            button3.Enabled = false;
            button3.Location = new Point(12, 179);
            button3.Name = "button3";
            button3.Size = new Size(75, 23);
            button3.TabIndex = 11;
            button3.Text = "OK";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.Enabled = false;
            button4.Location = new Point(12, 208);
            button4.Name = "button4";
            button4.Size = new Size(339, 29);
            button4.TabIndex = 12;
            button4.Text = "Create DPK file";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // saveFileDialog1
            // 
            saveFileDialog1.DefaultExt = "dpk";
            saveFileDialog1.FileName = "Package.dpk";
            saveFileDialog1.Filter = "deneOS Package|*.dpk";
            saveFileDialog1.FileOk += saveFileDialog1_FileOk;
            // 
            // BundleSEA
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(363, 249);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(textBox3);
            Controls.Add(textBox2);
            Controls.Add(maskedTextBox1);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label1);
            Controls.Add(textBox1);
            Name = "BundleSEA";
            Text = "Bundle single-executable app";
            Load += BundleSEA_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private Label label1;
        private Button button1;
        private OpenFileDialog openFileDialog1;
        private Button button2;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private MaskedTextBox maskedTextBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        private Button button3;
        private Button button4;
        private SaveFileDialog saveFileDialog1;
    }
}