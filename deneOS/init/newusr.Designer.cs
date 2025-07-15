namespace deneOS.init
{
    partial class newusr
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
            boxregpassag = new TextBox();
            txt12 = new Label();
            txt9 = new Label();
            button1 = new Button();
            txt13 = new Button();
            txt11 = new Label();
            boxregpass = new TextBox();
            txt10 = new Label();
            boxregusr = new TextBox();
            SuspendLayout();
            // 
            // boxregpassag
            // 
            boxregpassag.BackColor = SystemColors.WindowText;
            boxregpassag.ForeColor = SystemColors.Window;
            boxregpassag.Location = new Point(40, 241);
            boxregpassag.Name = "boxregpassag";
            boxregpassag.Size = new Size(260, 39);
            boxregpassag.TabIndex = 104;
            // 
            // txt12
            // 
            txt12.AutoSize = true;
            txt12.ForeColor = SystemColors.Control;
            txt12.Location = new Point(34, 206);
            txt12.Name = "txt12";
            txt12.Size = new Size(66, 32);
            txt12.TabIndex = 106;
            txt12.Text = "Txt12";
            // 
            // txt9
            // 
            txt9.AutoSize = true;
            txt9.Font = new Font("Segoe UI Variable Display", 9F);
            txt9.Location = new Point(3, 1);
            txt9.Name = "txt9";
            txt9.Size = new Size(36, 20);
            txt9.TabIndex = 105;
            txt9.Text = "Txt9";
            // 
            // button1
            // 
            button1.BackColor = SystemColors.ControlText;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI Variable Display", 7F);
            button1.ForeColor = SystemColors.Control;
            button1.Location = new Point(301, 2);
            button1.Name = "button1";
            button1.Size = new Size(33, 23);
            button1.TabIndex = 108;
            button1.Text = "";
            button1.UseVisualStyleBackColor = false;
            // 
            // txt13
            // 
            txt13.BackColor = SystemColors.ControlText;
            txt13.ForeColor = SystemColors.Control;
            txt13.Location = new Point(39, 291);
            txt13.Name = "txt13";
            txt13.Size = new Size(260, 45);
            txt13.TabIndex = 107;
            txt13.Text = "Txt13";
            txt13.UseVisualStyleBackColor = false;
            txt13.Click += txt13_Click;
            // 
            // txt11
            // 
            txt11.AutoSize = true;
            txt11.ForeColor = SystemColors.Control;
            txt11.Location = new Point(32, 132);
            txt11.Name = "txt11";
            txt11.Size = new Size(62, 32);
            txt11.TabIndex = 103;
            txt11.Text = "Txt11";
            // 
            // boxregpass
            // 
            boxregpass.BackColor = SystemColors.WindowText;
            boxregpass.ForeColor = SystemColors.Window;
            boxregpass.Location = new Point(40, 167);
            boxregpass.Name = "boxregpass";
            boxregpass.Size = new Size(260, 39);
            boxregpass.TabIndex = 102;
            // 
            // txt10
            // 
            txt10.AutoSize = true;
            txt10.ForeColor = SystemColors.Control;
            txt10.Location = new Point(32, 56);
            txt10.Name = "txt10";
            txt10.Size = new Size(66, 32);
            txt10.TabIndex = 101;
            txt10.Text = "Txt10";
            // 
            // boxregusr
            // 
            boxregusr.BackColor = SystemColors.ControlText;
            boxregusr.ForeColor = SystemColors.Window;
            boxregusr.Location = new Point(40, 90);
            boxregusr.Name = "boxregusr";
            boxregusr.Size = new Size(260, 39);
            boxregusr.TabIndex = 100;
            // 
            // newusr
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = SystemColors.ControlText;
            ClientSize = new Size(337, 386);
            Controls.Add(boxregpassag);
            Controls.Add(txt12);
            Controls.Add(txt9);
            Controls.Add(button1);
            Controls.Add(txt13);
            Controls.Add(txt11);
            Controls.Add(boxregpass);
            Controls.Add(txt10);
            Controls.Add(boxregusr);
            Font = new Font("Segoe UI Variable Display", 14F);
            ForeColor = SystemColors.Control;
            FormBorderStyle = FormBorderStyle.None;
            Name = "newusr";
            Opacity = 0.6D;
            Text = "newusr";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox boxregpassag;
        private Label txt12;
        private Label txt9;
        private Button button1;
        private Button txt13;
        private Label txt11;
        private TextBox boxregpass;
        private Label txt10;
        private TextBox boxregusr;
    }
}