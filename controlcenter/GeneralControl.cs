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

            // Label usuario actual
            Label currentUsuario = new Label();
            string usuario = File.Exists(@"C:\DENEOS\sysconf\config.ini") ? File.ReadAllLines(@"C:\DENEOS\sysconf\config.ini")[1].Substring(File.ReadAllLines(@"C:\DENEOS\sysconf\config.ini")[1].TrimEnd().LastIndexOf(' ') + 1) : "N/A";
            currentUsuario.Text = $"{(string)T("currentuser")}: {usuario}";
            currentUsuario.Location = new Point(20, 150);
            currentUsuario.Font = new Font("Segoe UI", 10);
            currentUsuario.AutoSize = true;

            // Checkbox shell
            CheckBox cbShell = new CheckBox();
            cbShell.Text = (string)T("dnOSdefshell");
            cbShell.Location = new Point(20, 90);
            cbShell.Font = new Font("Segoe UI", 10);
            cbShell.AutoSize = true;
            cbShell.CheckedChanged += (s, e) =>
            {
                //HKLM\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon
                if (cbShell.Checked)
                {
                    MessageBox.Show((string)T("dnOSwillbeshell"));
                    if (!AdminHelper.IsRunningAsAdmin())
                    {
                        AdminHelper.RestartAsAdmin("page:general");
                        MessageBox.Show($"{T("err")}: {T("errnotpermission")}", $"{T("permsneeded")}", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    } else
                    {
                        try {
                            using (var key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", true))
                            {
                                key.SetValue("Shell", @"C:\DENEOS\core\deneOS.Watchdog.exe", Microsoft.Win32.RegistryValueKind.String);
                            }
                            cbAutoStart.Checked = false;
                            cbAutoStart.Enabled = false;
                            MessageBox.Show($"{T("regeditedsuccess")}. {T("dnOSwillbeshell")}");
                        } catch (Exception ex)
                        {
                            MessageBox.Show($"{T("errconfiguringshell")}: {ex.Message}");
                        }
                    }
                } else {
                    MessageBox.Show((string)T("dnOSwillnotbeshell"));
                    if (!AdminHelper.IsRunningAsAdmin())
                    {
                        AdminHelper.RestartAsAdmin("page:general");
                        MessageBox.Show($"{T("err")}: {T("errnotpermission")}", $"{T("permsneeded")}", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        try
                        {
                            using (var key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", true))
                            {
                                key.SetValue("Shell", @"explorer.exe", Microsoft.Win32.RegistryValueKind.String);
                            }
                            MessageBox.Show($"{T("regeditedsuccess")}. {T("dnOSwillnotbeshell")}");
                            cbAutoStart.Enabled = true;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"{T("errconfiguringshell")}: {ex.Message}");
                        }
                    }
                }
            };

            cbAutoStart.CheckedChanged += (s, e) =>
            {
                if (cbAutoStart.Checked)
                {
                    MessageBox.Show((string)T("dnOSwillstartwithWindows"));
                    if (!AdminHelper.IsRunningAsAdmin())
                    {
                        AdminHelper.RestartAsAdmin("page:general");
                        MessageBox.Show($"{T("err")}: {T("errnotpermission")}", $"{T("permsneeded")}", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        try
                        {
                            using (var key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run"))
                            {
                                key.SetValue("deneOS", @"C:\DENEOS\core\deneOS.exe");
                            }
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
                    if (!AdminHelper.IsRunningAsAdmin())
                    {
                        AdminHelper.RestartAsAdmin("page:general");
                        MessageBox.Show($"{T("err")}: {T("errnotpermission")}", $"{T("permsneeded")}", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        try
                        {
                            using (var key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run"))
                            {
                                key.DeleteValue("deneOS", false);
                            }
                            MessageBox.Show($"{T("regeditedsuccess")}. {T("notinitdeneOSwithWindows")}");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"{T("errconfiguringautostart")}: {ex.Message}");
                        }
                    }
                }
            };

            // Botón eliminar usuario
            Button btnEliminarUsuario = new Button();
            btnEliminarUsuario.Text = (string)T("deleteuser");
            btnEliminarUsuario.Location = new Point(20, 120);
            btnEliminarUsuario.Font = new Font("Segoe UI", 10);
            btnEliminarUsuario.AutoSize = true;
            btnEliminarUsuario.Click += (s, e) =>
            {
                // Aquí se puede agregar la lógica para eliminar el usuario
                // Por ejemplo, mostrar un mensaje de confirmación
                DialogResult result = MessageBox.Show((string)T("confirmdeleteuser"), (string)T("deleteuser"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    File.Delete(@"C:\DENEOS\sysconf\config.ini");
                    A();
                    MessageBox.Show((string)T("userdeleted"));
                    
                }


            };
           
            // Agregar controles
            this.Controls.Add(lblIdioma);
            this.Controls.Add(cbIdioma);
            this.Controls.Add(cbAutoStart);
            this.Controls.Add(cbShell);
            this.Controls.Add(btnEliminarUsuario);
            this.Controls.Add(currentUsuario);

            void A()
            {
                currentUsuario.Text = $"{(string)T("currentuser")}: N/A";

            }
        }

        void cbAutoStart(object sender, EventArgs e)
        {

        }
    }
}
