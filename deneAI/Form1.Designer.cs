namespace deneAI
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
            btnSend = new Button();
            panel1 = new Panel();
            label1 = new Label();
            comboBox1 = new ComboBox();
            btnClear = new Button();
            txtPrompt = new TextBox();
            rtbChat = new RichTextBox();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // btnSend
            // 
            btnSend.Dock = DockStyle.Right;
            btnSend.Location = new Point(392, 0);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(147, 28);
            btnSend.TabIndex = 1;
            btnSend.Text = "Send";
            btnSend.UseVisualStyleBackColor = true;
            btnSend.Click += btnSend_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(label1);
            panel1.Controls.Add(comboBox1);
            panel1.Controls.Add(btnClear);
            panel1.Controls.Add(btnSend);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 422);
            panel1.Name = "panel1";
            panel1.Size = new Size(539, 28);
            panel1.TabIndex = 2;
            // 
            // label1
            // 
            label1.Dock = DockStyle.Right;
            label1.Location = new Point(218, 0);
            label1.Name = "label1";
            label1.Size = new Size(53, 28);
            label1.TabIndex = 4;
            label1.Text = "Model:";
            label1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // comboBox1
            // 
            comboBox1.Dock = DockStyle.Right;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "llama2:7b", "llama3:8b", "llama3.1:8b", "llama3.2:1b", "llama3.2:3b", "qwen3.5:0.8b", "qwen3.5:2b", "lfm2.5-thinking:1.2b" });
            comboBox1.Location = new Point(271, 0);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(121, 23);
            comboBox1.TabIndex = 3;
            // 
            // btnClear
            // 
            btnClear.Dock = DockStyle.Left;
            btnClear.Location = new Point(0, 0);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(140, 28);
            btnClear.TabIndex = 2;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // txtPrompt
            // 
            txtPrompt.Dock = DockStyle.Bottom;
            txtPrompt.Location = new Point(0, 342);
            txtPrompt.Multiline = true;
            txtPrompt.Name = "txtPrompt";
            txtPrompt.Size = new Size(539, 80);
            txtPrompt.TabIndex = 3;
            txtPrompt.KeyDown += txtPrompt_KeyDown;
            // 
            // rtbChat
            // 
            rtbChat.Dock = DockStyle.Fill;
            rtbChat.Location = new Point(0, 0);
            rtbChat.Name = "rtbChat";
            rtbChat.ReadOnly = true;
            rtbChat.Size = new Size(539, 342);
            rtbChat.TabIndex = 4;
            rtbChat.Text = "";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(539, 450);
            Controls.Add(rtbChat);
            Controls.Add(txtPrompt);
            Controls.Add(panel1);
            Name = "Form1";
            Text = "deneAI";
            FormClosed += Form1_FormClosed;
            Click += Form1_Load;
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnSend;
        private Panel panel1;
        private TextBox txtPrompt;
        private RichTextBox rtbChat;
        private Button btnClear;
        private Label label1;
        private ComboBox comboBox1;
    }
}
