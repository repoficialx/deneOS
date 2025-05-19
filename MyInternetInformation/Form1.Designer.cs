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
            this.panelBarra = new System.Windows.Forms.Panel();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.btnMinimizar = new System.Windows.Forms.Button();
            this.btnMaximizar = new System.Windows.Forms.Button();
            this.lblIcono = new System.Windows.Forms.Label();
            this.lblNombre = new System.Windows.Forms.Label();
            this.webIdc = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.lblUser = new System.Windows.Forms.Label();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.lblCreditos = new System.Windows.Forms.Label();

            // Panel Barra
            this.panelBarra.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelBarra.Height = 30;
            this.panelBarra.BackColor = Color.DarkGray;
            this.panelBarra.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelBarra_MouseDown);

            // Botón Minimizar
            this.btnMinimizar.Text = "";
            this.btnMinimizar.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnMinimizar.Font = new Font("Segoe Fluent Icons", 12);
            this.btnMinimizar.Click += new System.EventHandler(this.btnMinimizar_Click);
            this.btnMinimizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            // Botón Maximizar/Restaurar
            this.btnMaximizar.Text = "";
            this.btnMaximizar.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnMaximizar.Font = new Font("Segoe Fluent Icons", 12);
            this.btnMaximizar.Click += new System.EventHandler(this.btnMaximizar_Click);
            this.btnMaximizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            // Botón Cerrar
            this.btnCerrar.Text = "";
            this.btnCerrar.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCerrar.Font = new Font("Segoe Fluent Icons", 12);
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            // Label para el icono (Fuente Segoe Fluent Icons)
            this.lblIcono.Text = ""; // Código del icono
            this.lblIcono.Font = new Font("Segoe Fluent Icons", 16);
            this.lblIcono.AutoSize = true;
            this.lblIcono.ForeColor = Color.White;
            this.lblIcono.Location = new Point(0, 2); // Posición en la barra
            this.lblIcono.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelBarra_MouseDown);

            // Label para el nombre
            this.lblNombre.Text = "My Internet Information";
            this.lblNombre.Font = new Font("Segoe UI Variable Display Semibold", 12, FontStyle.Bold);
            this.lblNombre.AutoSize = true;
            this.lblNombre.ForeColor = Color.White;
            this.lblNombre.Location = new Point(35, 3); // Justo al lado del icono
            this.lblNombre.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelBarra_MouseDown);

            // Web IDC
            this.webIdc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webIdc.Location = new System.Drawing.Point(0, 30);
            this.webIdc.Source = new System.Uri($"https://idcrawl.com/u/");
            this.webIdc.Size = new System.Drawing.Size(600, 370); // Tamaño del WebView2
            this.webIdc.BackColor = Color.White; // Color de fondo del WebView2
            this.webIdc.Visible = false;


            // Label del textBox del usuario
            this.lblUser.Text = "Username";
            this.lblUser.AutoSize = true;
            this.lblUser.Location = new Point(10, 50); // Posición del label
            this.lblUser.Font = new Font("Segoe UI Variable Display Semibold", 12, FontStyle.Bold);
            this.lblUser.ForeColor = Color.Black;
            this.lblUser.BackColor = Color.Transparent;
            this.lblUser.Visible = true;

            // TextBox del usuario
            this.txtUser.Location = new Point(115, 50); // Posición del textBox
            this.txtUser.Size = new Size(200, 30); // Tamaño del textBox
            this.txtUser.Font = new Font("Segoe UI Variable Display Semibold", 12, FontStyle.Bold);
            this.txtUser.ForeColor = Color.Black;
            this.txtUser.BackColor = Color.White;
            this.txtUser.Visible = true;
            this.txtUser.Text = ""; // Texto por defecto

            // Buscar usuario
            this.btnBuscar.Text = "scan";
            this.btnBuscar.Location = new Point(330, 50); // Posición del botón
            this.btnBuscar.Size = new Size(100, 30); // Tamaño del botón
            this.btnBuscar.Font = new Font("Segoe UI Variable Display Semibold", 10, FontStyle.Bold);
            this.btnBuscar.ForeColor = Color.Black;
            this.btnBuscar.BackColor = Color.LightGray;
            this.btnBuscar.Visible = true;
            this.btnBuscar.Click += (s, e) =>
            {
                // Cambiar la URL del WebView2 al hacer clic en el botón
                this.webIdc.Source = new System.Uri($"https://idcrawl.com/u/{txtUser.Text}");
                this.webIdc.Visible = true; // Hacer visible el WebView2
            };
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            // Label de créditos
            this.lblCreditos.Text = "powered by idcrawl.com";
            this.lblCreditos.AutoSize = true;
            this.lblCreditos.Location = new Point(500, 30); // Posición del label
            this.lblCreditos.Font = new Font("Segoe UI Variable Display Semibold", 5, FontStyle.Bold);
            this.lblCreditos.ForeColor = Color.Black;
            this.lblCreditos.BackColor = Color.Transparent;
            this.lblCreditos.Visible = true;
            this.lblCreditos.Click += (s, e) =>
            {
                // Abrir la URL al hacer clic en el label
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo("https://idcrawl.com") { UseShellExecute = true });
            };

            // Agregar controles
            this.panelBarra.Controls.Add(this.lblIcono);
            this.panelBarra.Controls.Add(this.lblNombre);
            this.panelBarra.Controls.Add(this.btnMinimizar);
            this.panelBarra.Controls.Add(this.btnMaximizar);
            this.panelBarra.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.panelBarra);
            this.Controls.Add(this.webIdc);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.lblUser);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.lblCreditos);

            this.ClientSize = new System.Drawing.Size(600, 400);
            this.Text = "MII";
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None; // Quitar el borde del formulario
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen; // Centrar el formulario en la pantalla
            this.BackColor = SystemColors.Control; // Color de fondo del formulario

        }


        #endregion
    }
}
