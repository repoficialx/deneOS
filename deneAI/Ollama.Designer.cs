namespace deneAI
{
    partial class Ollama
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
            lblStatus = new Label();
            progressBar1 = new ProgressBar();
            button1 = new Button();
            SuspendLayout();
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(12, 57);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(38, 15);
            lblStatus.TabIndex = 0;
            lblStatus.Text = "";
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(12, 75);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(156, 23);
            progressBar1.TabIndex = 1;
            progressBar1.Visible = false;
            // 
            // button1
            // 
            button1.Location = new Point(12, 12);
            button1.Name = "button1";
            button1.Size = new Size(156, 42);
            button1.TabIndex = 2;
            button1.Text = "Check";
            button1.UseVisualStyleBackColor = true;
            button1.Click += btnCheckOllama_Click;
            // 
            // Ollama
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(183, 117);
            Controls.Add(button1);
            Controls.Add(progressBar1);
            Controls.Add(lblStatus);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Ollama";
            Text = "deneAI: Core";
            FormClosing += Ollama_FormClosing;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblStatus;
        private ProgressBar progressBar1;
        private Button button1;
    }
}