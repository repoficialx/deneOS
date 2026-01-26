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
            btnCustom = new Button();
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
            panel1.Controls.Add(btnCustom);
            panel1.Controls.Add(btnUpd);
            panel1.Controls.Add(btnAcerca);
            panel1.Controls.Add(btnAvanzado);
            panel1.Controls.Add(btnSoftware);
            panel1.Controls.Add(btnGeneral);
            panel1.Controls.Add(btnPantalla);
            panel1.Controls.Add(btnInicio);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(3, 2, 3, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(131, 338);
            panel1.TabIndex = 0;
            // 
            // btnCustom
            // 
            btnCustom.Dock = DockStyle.Top;
            btnCustom.Image = Properties.Resources.icons8_windows_10_personalization_100;
            btnCustom.ImageAlign = ContentAlignment.MiddleLeft;
            btnCustom.Location = new Point(0, 204);
            btnCustom.Margin = new Padding(3, 2, 3, 2);
            btnCustom.Name = "btnCustom";
            btnCustom.Size = new Size(131, 34);
            btnCustom.TabIndex = 7;
            btnCustom.Text = "Personalización";
            btnCustom.TextAlign = ContentAlignment.MiddleRight;
            btnCustom.UseVisualStyleBackColor = true;
            btnCustom.Click += btnCustom_Click;
            // 
            // btnUpd
            // 
            btnUpd.Dock = DockStyle.Top;
            btnUpd.Image = Properties.Resources.icons8_windows_update_100;
            btnUpd.ImageAlign = ContentAlignment.MiddleLeft;
            btnUpd.Location = new Point(0, 170);
            btnUpd.Margin = new Padding(3, 2, 3, 2);
            btnUpd.Name = "btnUpd";
            btnUpd.Size = new Size(131, 34);
            btnUpd.TabIndex = 6;
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
            btnAcerca.Location = new Point(0, 238);
            btnAcerca.Margin = new Padding(3, 2, 3, 2);
            btnAcerca.Name = "btnAcerca";
            btnAcerca.Size = new Size(131, 34);
            btnAcerca.TabIndex = 8;
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
            btnAvanzado.Location = new Point(0, 136);
            btnAvanzado.Margin = new Padding(3, 2, 3, 2);
            btnAvanzado.Name = "btnAvanzado";
            btnAvanzado.Size = new Size(131, 34);
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
            btnSoftware.Location = new Point(0, 102);
            btnSoftware.Margin = new Padding(3, 2, 3, 2);
            btnSoftware.Name = "btnSoftware";
            btnSoftware.Size = new Size(131, 34);
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
            btnGeneral.Location = new Point(0, 68);
            btnGeneral.Margin = new Padding(3, 2, 3, 2);
            btnGeneral.Name = "btnGeneral";
            btnGeneral.Size = new Size(131, 34);
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
            btnPantalla.Location = new Point(0, 34);
            btnPantalla.Margin = new Padding(3, 2, 3, 2);
            btnPantalla.Name = "btnPantalla";
            btnPantalla.Size = new Size(131, 34);
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
            btnInicio.Margin = new Padding(3, 2, 3, 2);
            btnInicio.Name = "btnInicio";
            btnInicio.Size = new Size(131, 34);
            btnInicio.TabIndex = 0;
            btnInicio.Text = "Inicio";
            btnInicio.TextAlign = ContentAlignment.MiddleRight;
            btnInicio.UseVisualStyleBackColor = true;
            btnInicio.Click += btnInicio_Click;
            // 
            // panel2
            // 
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(131, 0);
            panel2.Margin = new Padding(3, 2, 3, 2);
            panel2.Name = "panel2";
            panel2.Size = new Size(569, 338);
            panel2.TabIndex = 1;
            // 
            // formAjustes
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(700, 338);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 2, 3, 2);
            Name = "formAjustes";
            Text = "Centro de control";
            Load += formAjustes_Load;
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Button btnInicio;
        private Button btnAvanzado;
        private Button btnSoftware;
        private Button btnGeneral;
        private Button btnPantalla;
        private Panel panel2;
        private Button btnUpd;
        private Button btnCustom;
        private Button btnAcerca;
    }
}
