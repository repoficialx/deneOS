namespace MyInternetInformation
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
        private System.Windows.Forms.Panel panelBarra;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Button btnMinimizar;
        private System.Windows.Forms.Button btnMaximizar;
        private System.Windows.Forms.Label lblIcono;
        private System.Windows.Forms.Label lblNombre;
        private Microsoft.Web.WebView2.WinForms.WebView2 webIdc;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Label lblCreditos;

        private void InitializeComponent()
        {
            panelBarra = new Panel();
            lblIcono = new Label();
            lblNombre = new Label();
            btnMinimizar = new Button();
            btnMaximizar = new Button();
            btnCerrar = new Button();
            webIdc = new Microsoft.Web.WebView2.WinForms.WebView2();
            txtUser = new TextBox();
            lblUser = new Label();
            btnBuscar = new Button();
            lblCreditos = new Label();
            panelBarra.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)webIdc).BeginInit();
            SuspendLayout();
            // 
            // panelBarra
            // 
            panelBarra.BackColor = Color.DarkGray;
            panelBarra.Controls.Add(lblIcono);
            panelBarra.Controls.Add(lblNombre);
            panelBarra.Controls.Add(btnMinimizar);
            panelBarra.Controls.Add(btnMaximizar);
            panelBarra.Controls.Add(btnCerrar);
            panelBarra.Dock = DockStyle.Top;
            panelBarra.Location = new Point(0, 0);
            panelBarra.Name = "panelBarra";
            panelBarra.Size = new Size(600, 30);
            panelBarra.TabIndex = 0;
            panelBarra.MouseDown += panelBarra_MouseDown;
            // 
            // lblIcono
            // 
            lblIcono.AutoSize = true;
            lblIcono.Font = new Font("Segoe Fluent Icons", 16F);
            lblIcono.ForeColor = Color.White;
            lblIcono.Location = new Point(0, 2);
            lblIcono.Name = "lblIcono";
            lblIcono.Size = new Size(39, 27);
            lblIcono.TabIndex = 0;
            lblIcono.Text = "";
            lblIcono.MouseDown += panelBarra_MouseDown;
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Font = new Font("Segoe UI Variable Display Semib", 12F, FontStyle.Bold);
            lblNombre.ForeColor = Color.White;
            lblNombre.Location = new Point(35, 3);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(233, 27);
            lblNombre.TabIndex = 1;
            lblNombre.Text = "My Internet Information";
            lblNombre.MouseDown += panelBarra_MouseDown;
            // 
            // btnMinimizar
            // 
            btnMinimizar.Dock = DockStyle.Right;
            btnMinimizar.FlatStyle = FlatStyle.Flat;
            btnMinimizar.Font = new Font("Segoe Fluent Icons", 12F);
            btnMinimizar.Location = new Point(375, 0);
            btnMinimizar.Name = "btnMinimizar";
            btnMinimizar.Size = new Size(75, 30);
            btnMinimizar.TabIndex = 2;
            btnMinimizar.Text = "";
            btnMinimizar.Click += btnMinimizar_Click;
            // 
            // btnMaximizar
            // 
            btnMaximizar.Dock = DockStyle.Right;
            btnMaximizar.FlatStyle = FlatStyle.Flat;
            btnMaximizar.Font = new Font("Segoe Fluent Icons", 12F);
            btnMaximizar.Location = new Point(450, 0);
            btnMaximizar.Name = "btnMaximizar";
            btnMaximizar.Size = new Size(75, 30);
            btnMaximizar.TabIndex = 3;
            btnMaximizar.Text = "";
            btnMaximizar.Click += btnMaximizar_Click;
            // 
            // btnCerrar
            // 
            btnCerrar.Dock = DockStyle.Right;
            btnCerrar.FlatStyle = FlatStyle.Flat;
            btnCerrar.Font = new Font("Segoe Fluent Icons", 12F);
            btnCerrar.Location = new Point(525, 0);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Size = new Size(75, 30);
            btnCerrar.TabIndex = 4;
            btnCerrar.Text = "";
            btnCerrar.Click += btnCerrar_Click;
            // 
            // webIdc
            // 
            webIdc.AllowExternalDrop = true;
            webIdc.BackColor = Color.White;
            webIdc.CreationProperties = null;
            webIdc.DefaultBackgroundColor = Color.White;
            webIdc.Dock = DockStyle.Fill;
            webIdc.Enabled = false;
            webIdc.Location = new Point(0, 0);
            webIdc.Name = "webIdc";
            webIdc.Size = new Size(600, 400);
            webIdc.Source = new Uri("https://idcrawl.com/u/", UriKind.Absolute);
            webIdc.TabIndex = 1;
            webIdc.Visible = false;
            webIdc.ZoomFactor = 1D;
            // 
            // txtUser
            // 
            txtUser.BackColor = Color.White;
            txtUser.Font = new Font("Segoe UI Variable Display Semib", 12F, FontStyle.Bold);
            txtUser.ForeColor = Color.Black;
            txtUser.Location = new Point(115, 50);
            txtUser.Name = "txtUser";
            txtUser.Size = new Size(200, 34);
            txtUser.TabIndex = 2;
            // 
            // lblUser
            // 
            lblUser.AutoSize = true;
            lblUser.BackColor = Color.Transparent;
            lblUser.Font = new Font("Segoe UI Variable Display Semib", 12F, FontStyle.Bold);
            lblUser.ForeColor = Color.Black;
            lblUser.Location = new Point(10, 50);
            lblUser.Name = "lblUser";
            lblUser.Size = new Size(104, 27);
            lblUser.TabIndex = 3;
            lblUser.Text = "Username";
            // 
            // btnBuscar
            // 
            btnBuscar.BackColor = Color.LightGray;
            btnBuscar.FlatStyle = FlatStyle.Flat;
            btnBuscar.Font = new Font("Segoe UI Variable Display Semib", 10F, FontStyle.Bold);
            btnBuscar.ForeColor = Color.Black;
            btnBuscar.Location = new Point(330, 50);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(100, 30);
            btnBuscar.TabIndex = 4;
            btnBuscar.Text = "scan";
            btnBuscar.UseVisualStyleBackColor = false;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // lblCreditos
            // 
            lblCreditos.AutoSize = true;
            lblCreditos.BackColor = Color.Transparent;
            lblCreditos.Font = new Font("Segoe UI Variable Display Semib", 5F, FontStyle.Bold);
            lblCreditos.ForeColor = Color.Black;
            lblCreditos.Location = new Point(497, 33);
            lblCreditos.Name = "lblCreditos";
            lblCreditos.Size = new Size(103, 12);
            lblCreditos.TabIndex = 5;
            lblCreditos.Text = "powered by idcrawl.com";
            lblCreditos.Click += lblCreditos_Click;
            // 
            // Form1
            // 
            BackColor = SystemColors.Control;
            ClientSize = new Size(600, 400);
            Controls.Add(panelBarra);
            Controls.Add(webIdc);
            Controls.Add(txtUser);
            Controls.Add(lblUser);
            Controls.Add(btnBuscar);
            Controls.Add(lblCreditos);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MII";
            panelBarra.ResumeLayout(false);
            panelBarra.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)webIdc).EndInit();
            ResumeLayout(false);
            PerformLayout();


            #endregion
        }
    }
}
