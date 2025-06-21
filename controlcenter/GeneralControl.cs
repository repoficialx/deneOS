using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace controlcenter
{
    public partial class GeneralControl : UserControl
    {
        public GeneralControl()
        {
            InitializeComponent();
            SetupUI();
        }

        private void GeneralControl_Load(object sender, EventArgs e)
        {

        }

        private void SetupUI()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.White;

            // Label Idioma
            Label lblIdioma = new Label();
            lblIdioma.Text = "Idioma del sistema:";
            lblIdioma.Location = new Point(20, 20);
            lblIdioma.Font = new Font("Segoe UI", 10);
            lblIdioma.AutoSize = true;

            // ComboBox idiomas
            ComboBox cbIdioma = new ComboBox();
            cbIdioma.Items.AddRange(new string[] { "Español", "English" });
            cbIdioma.SelectedIndex = 0;
            cbIdioma.Location = new Point(190, 18);
            cbIdioma.Width = 150;
            cbIdioma.SelectionChangeCommitted += (s, e) =>
            {
                MessageBox.Show($"Elegido: {cbIdioma.SelectedItem.ToString()}");
            };

            // Checkbox iniciar con Windows
            CheckBox cbAutoStart = new CheckBox();
            cbAutoStart.Text = "Iniciar deneOS con Windows";
            cbAutoStart.Location = new Point(20, 60);
            cbAutoStart.Font = new Font("Segoe UI", 10);    
            cbAutoStart.AutoSize = true;

            // Checkbox animaciones
            CheckBox cbAnimaciones = new CheckBox();
            cbAnimaciones.Text = "deneOS Shell predeterminada";
            cbAnimaciones.Location = new Point(20, 90);
            cbAnimaciones.Font = new Font("Segoe UI", 10);
            cbAnimaciones.AutoSize = true;
            cbAutoStart.CheckedChanged += (s, e) =>
            {
                //Hacer deneOS_Home.exe shell (en vez de explorer) elevando el programa
                if (cbAutoStart.Checked)
                {
                    MessageBox.Show("deneOS se iniciará con Windows.");
                    // Aquí se podría agregar el código para modificar el registro de Windows
                    // Por ejemplo, agregar una entrada en el registro de inicio
                    // Hacerlo de forma segura y con permisos adecuados
                    // Elevar para obtener permisos de administrador
                    if (!AdminHelper.IsRunningAsAdmin())
                    {
                        AdminHelper.RestartAsAdmin("page:general");
                        MessageBox.Show("ERROR: Se necesitan permisos de administrador para modificar el registro. Por favor, reinicie la aplicación como administrador.", "Permisos necesarios", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return; // O cerrar la función si es en Main()
                    }
                    else
                    {
                        try
                        {
                            // Código para modificar el registro
                            // Aquí se puede usar Microsoft.Win32.Registry para agregar la entrada de inicio
                            // Ejemplo:
                            // using (var key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run"))
                            // {
                            //     key.SetValue("deneOS", @"C:\ruta\a\deneOS_Home.exe");
                            // }
                            MessageBox.Show("Registro modificado correctamente. deneOS se iniciará con Windows.");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error al configurar el inicio automático: {ex.Message}");
                        }
                    }
                    
                }
                else
                {
                    MessageBox.Show("deneOS no se iniciará con Windows.");
                }
            };

            // Agregar controles
            this.Controls.Add(lblIdioma);
            this.Controls.Add(cbIdioma);
            this.Controls.Add(cbAutoStart);
            this.Controls.Add(cbAnimaciones);
        }

        void cbAutoStart(object sender, EventArgs e)
        {

        }
    }
}
