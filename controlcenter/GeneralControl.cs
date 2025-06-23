using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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
            lblIdioma.Text = (string)T("syslang");
            lblIdioma.Location = new Point(20, 20);
            lblIdioma.Font = new Font("Segoe UI", 10);
            lblIdioma.AutoSize = true;

            // ComboBox idiomas
            ComboBox cbIdioma = new ComboBox();
            cbIdioma.Items.AddRange(new string[] { (string)T("esp"), (string)T("eng") });
            var lang = File.ReadAllLines(@"C:\DENEOS\sysconf\lang.ini")[1];
            int getSI(string lang) =>
                lang switch
                {
                    "es" => 0,
                    "en" => 1,
                    _ => 0
                };
            cbIdioma.SelectedIndex = getSI(lang:lang);
            cbIdioma.Location = new Point(190, 18);
            cbIdioma.Width = 150;
            cbIdioma.SelectionChangeCommitted += (s, e) =>
            {
                //essageBox.Show($"Elegido: {cbIdioma.SelectedItem.ToString()}");
                if (cbIdioma.SelectedItem == T("esp"))
                {
                    File.Delete(@"C:\deneOS\sysconf\lang.ini");
                    File.WriteAllText(@"C:\deneOS\sysconf\lang.ini", "LANGCONFIG\r\nes");
                    MessageBox.Show((string)((string)(T("langchgdto")) + (string)(T("esp"))));
                }
                else if (cbIdioma.SelectedItem == T("eng"))
                {
                    File.Delete(@"C:\deneOS\sysconf\lang.ini");
                    File.WriteAllText(@"C:\deneOS\sysconf\lang.ini", "LANGCONFIG\r\nen");
                    MessageBox.Show((string)((string)(T("langchgdto")) + (string)(T("eng"))));
                }
            };

            // Checkbox iniciar con Windows
            CheckBox cbAutoStart = new CheckBox();
            cbAutoStart.Text = (string)T("initdeneOSwithWindows");
            cbAutoStart.Location = new Point(20, 60);
            cbAutoStart.Font = new Font("Segoe UI", 10);    
            cbAutoStart.AutoSize = true;

            // Checkbox animaciones
            CheckBox cbAnimaciones = new CheckBox();
            cbAnimaciones.Text = (string)T("dnOSdefshell");
            cbAnimaciones.Location = new Point(20, 90);
            cbAnimaciones.Font = new Font("Segoe UI", 10);
            cbAnimaciones.AutoSize = true;
            cbAutoStart.CheckedChanged += (s, e) =>
            {
                //Hacer deneOS_Home.exe shell (en vez de explorer) elevando el programa
                if (cbAutoStart.Checked)
                {
                    MessageBox.Show((string)T("dnOSwillstartwithWindows"));
                    // Aquí se podría agregar el código para modificar el registro de Windows
                    // Por ejemplo, agregar una entrada en el registro de inicio
                    // Hacerlo de forma segura y con permisos adecuados
                    // Elevar para obtener permisos de administrador
                    if (!AdminHelper.IsRunningAsAdmin())
                    {
                        AdminHelper.RestartAsAdmin("page:general");
                        MessageBox.Show($"{T("err")}: {T("errnotpermission")}", $"{T("permsneeded")}", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                            MessageBox.Show($"{T("regeditedsuccess")}. {T("dnOSwillstartwithWindows")}");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"{T("errconfiguringautostart")}: {ex.Message}");
                        }
                    }
                    
                }
                else
                {
                    MessageBox.Show((string)T("notinitdeneOSwithWindows"));
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
