namespace Internet
{
    partial class networkList
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
            comboBox1 = new ComboBox();
            button1 = new Button();
            panel1 = new Panel();
            button2 = new Button();
            panel2 = new Panel();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // comboBox1
            // 
            comboBox1.Dock = DockStyle.Fill;
            comboBox1.DropDownStyle = ComboBoxStyle.Simple;
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(0, 0);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(313, 202);
            comboBox1.TabIndex = 0;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // button1
            // 
            button1.Dock = DockStyle.Fill;
            button1.Location = new Point(0, 0);
            button1.Name = "button1";
            button1.Size = new Size(276, 27);
            button1.TabIndex = 1;
            button1.Text = "conectar";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(comboBox1);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(313, 202);
            panel1.TabIndex = 4;
            // 
            // button2
            // 
            button2.Dock = DockStyle.Right;
            button2.Font = new Font("Material Icons", 9F);
            button2.Location = new Point(276, 0);
            button2.Name = "button2";
            button2.Size = new Size(37, 27);
            button2.TabIndex = 3;
            button2.Text = "refresh";
            button2.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            panel2.Controls.Add(button1);
            panel2.Controls.Add(button2);
            panel2.Dock = DockStyle.Bottom;
            panel2.Location = new Point(0, 175);
            panel2.Name = "panel2";
            panel2.Size = new Size(313, 27);
            panel2.TabIndex = 2;
            // 
            // networkList
            // 
            AcceptButton = button1;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(313, 202);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "networkList";
            ShowIcon = false;
            ShowInTaskbar = false;
            Text = "Available Connections";
            Load += networkList_Load;
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private ComboBox comboBox1;
        private Button button1;
        private Panel panel1;
        private Button button2;
        private Panel panel2;
    }
}