namespace deneOS
{
    partial class BottomBar
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
            label2 = new Label();
            button1 = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 62F);
            label1.Location = new Point(12, 11);
            label1.Name = "label1";
            label1.Size = new Size(172, 139);
            label1.TabIndex = 0;
            label1.Text = "<-";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 62F);
            label2.Location = new Point(605, 11);
            label2.Name = "label2";
            label2.Size = new Size(183, 139);
            label2.TabIndex = 1;
            label2.Text = "[][]";
            // 
            // button1
            // 
            button1.BackgroundImage = Properties.Resources.denelogo;
            button1.BackgroundImageLayout = ImageLayout.Zoom;
            button1.FlatStyle = FlatStyle.Popup;
            button1.Location = new Point(321, 11);
            button1.Name = "button1";
            button1.Size = new Size(141, 136);
            button1.TabIndex = 2;
            button1.UseVisualStyleBackColor = true;
            // 
            // BottomBar
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LightCyan;
            ClientSize = new Size(800, 159);
            Controls.Add(button1);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "BottomBar";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Button button1;
    }
}