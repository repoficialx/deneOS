namespace controlcenter
{
    partial class formAjustes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formAjustes));
            panel1 = new Panel();
            btnUpd = new Button();
            btnAcerca = new Button();
            btnAvanzado = new Button();
            btnSoftware = new Button();
            btnGeneral = new Button();
            btnPantalla = new Button();
            btnInicio = new Button();
            panel2 = new Panel();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(btnUpd);
            panel1.Controls.Add(btnAcerca);
            panel1.Controls.Add(btnAvanzado);
            panel1.Controls.Add(btnSoftware);
            panel1.Controls.Add(btnGeneral);
            panel1.Controls.Add(btnPantalla);
            panel1.Controls.Add(btnInicio);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(150, 450);
            panel1.TabIndex = 0;
            // 
            // btnUpd
            // 
            btnUpd.Dock = DockStyle.Top;
            btnUpd.Image = Properties.Resources.icons8_windows_update_100;
            btnUpd.ImageAlign = ContentAlignment.MiddleLeft;
            btnUpd.Location = new Point(0, 270);
            btnUpd.Name = "btnUpd";
            btnUpd.Size = new Size(150, 45);
            btnUpd.TabIndex = 7;
            btnUpd.Text = "deneUpdate";
            btnUpd.TextAlign = ContentAlignment.MiddleRight;
            btnUpd.UseVisualStyleBackColor = true;
            btnUpd.Click += btnUpd_Click;
            // 
            // btnAcerca
            // 
            btnAcerca.Dock = DockStyle.Top;
            btnAcerca.Image = Properties.Resources.icons8_about_me_100;
            btnAcerca.ImageAlign = ContentAlignment.MiddleLeft;
            btnAcerca.Location = new Point(0, 225);
            btnAcerca.Name = "btnAcerca";
            btnAcerca.Size = new Size(150, 45);
            btnAcerca.TabIndex = 6;
            btnAcerca.Text = "Acerca de...";
            btnAcerca.TextAlign = ContentAlignment.MiddleRight;
            btnAcerca.UseVisualStyleBackColor = true;
            btnAcerca.Click += btnAcerca_Click;
            // 
            // btnAvanzado
            // 
            btnAvanzado.Dock = DockStyle.Top;
            btnAvanzado.Image = Properties.Resources.icons8_ethernet_settings_100;
            btnAvanzado.ImageAlign = ContentAlignment.MiddleLeft;
            btnAvanzado.Location = new Point(0, 180);
            btnAvanzado.Name = "btnAvanzado";
            btnAvanzado.Size = new Size(150, 45);
            btnAvanzado.TabIndex = 5;
            btnAvanzado.Text = "Avanzado";
            btnAvanzado.TextAlign = ContentAlignment.MiddleRight;
            btnAvanzado.UseVisualStyleBackColor = true;
            btnAvanzado.Click += btnAvanzado_Click;
            // 
            // btnSoftware
            // 
            btnSoftware.Dock = DockStyle.Top;
            btnSoftware.Image = Properties.Resources.icons8_software_100;
            btnSoftware.ImageAlign = ContentAlignment.MiddleLeft;
            btnSoftware.Location = new Point(0, 135);
            btnSoftware.Name = "btnSoftware";
            btnSoftware.Size = new Size(150, 45);
            btnSoftware.TabIndex = 4;
            btnSoftware.Text = "Software";
            btnSoftware.TextAlign = ContentAlignment.MiddleRight;
            btnSoftware.UseVisualStyleBackColor = true;
            btnSoftware.Click += btnSoftware_Click;
            // 
            // btnGeneral
            // 
            btnGeneral.Dock = DockStyle.Top;
            btnGeneral.Image = Properties.Resources.icons8_laptop_settings_100;
            btnGeneral.ImageAlign = ContentAlignment.MiddleLeft;
            btnGeneral.Location = new Point(0, 90);
            btnGeneral.Name = "btnGeneral";
            btnGeneral.Size = new Size(150, 45);
            btnGeneral.TabIndex = 3;
            btnGeneral.Text = "General";
            btnGeneral.TextAlign = ContentAlignment.MiddleRight;
            btnGeneral.UseVisualStyleBackColor = true;
            btnGeneral.Click += btnGeneral_Click;
            // 
            // btnPantalla
            // 
            btnPantalla.Dock = DockStyle.Top;
            btnPantalla.Image = Properties.Resources.icons8_monitor_100;
            btnPantalla.ImageAlign = ContentAlignment.MiddleLeft;
            btnPantalla.Location = new Point(0, 45);
            btnPantalla.Name = "btnPantalla";
            btnPantalla.Size = new Size(150, 45);
            btnPantalla.TabIndex = 1;
            btnPantalla.Text = "Pantalla";
            btnPantalla.TextAlign = ContentAlignment.MiddleRight;
            btnPantalla.UseVisualStyleBackColor = true;
            btnPantalla.Click += btnPantalla_Click;
            // 
            // btnInicio
            // 
            btnInicio.Dock = DockStyle.Top;
            btnInicio.Image = Properties.Resources.icons8_home_page_100;
            btnInicio.ImageAlign = ContentAlignment.MiddleLeft;
            btnInicio.Location = new Point(0, 0);
            btnInicio.Name = "btnInicio";
            btnInicio.Size = new Size(150, 45);
            btnInicio.TabIndex = 0;
            btnInicio.Text = "Inicio";
            btnInicio.TextAlign = ContentAlignment.MiddleRight;
            btnInicio.UseVisualStyleBackColor = true;
            btnInicio.Click += btnInicio_Click;
            // 
            // panel2
            // 
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(150, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(650, 450);
            panel2.TabIndex = 1;
            // 
            // formAjustes
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "formAjustes";
            Text = "Centro de control";
            Load += formAjustes_Load;
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Button btnInicio;
        private Button btnAcerca;
        private Button btnAvanzado;
        private Button btnSoftware;
        private Button btnGeneral;
        private Button btnPantalla;
        private Panel panel2;
        private Button btnUpd;
    }
}
