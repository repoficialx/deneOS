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
    public partial class CustomControl : UserControl
    {
        public static TextBox btnEliminarUsuario;
        public CustomControl()
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
            lblIdioma.Text = (string)T("customisation");
            lblIdioma.Location = new Point(20, 20);
            lblIdioma.Font = new Font("Segoe UI", 10);
            lblIdioma.AutoSize = true;

            // Checkbox mostrar iconos
            CheckBox cbShowIcons = new CheckBox();
            cbShowIcons.Text = (string)T("showIcons");
            cbShowIcons.Location = new Point(20, 90);
            cbShowIcons.Font = new Font("Segoe UI", 10);
            cbShowIcons.AutoSize = true;
            cbShowIcons.CheckedChanged += (s, e) =>
            {
                if (cbShowIcons.Checked)
                {
                    MessageBox.Show((string)T("icllbsd"));
                    if (!AdminHelper.IsRunningAsAdmin())
                    {
                        AdminHelper.RestartAsAdmin("page:custom");
                        MessageBox.Show($"{T("err")}: {T("errnotpermission")}", $"{T("permsneeded")}", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        try
                        {
                            using (var key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(@"Software\deneOS\desktop"))
                            {
                                key.SetValue("showIcons", 1);
                            }
                            MessageBox.Show($"{T("regeditedsuccess")}. {T("icllbsd")}");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"{T("errconfiguringicons")}: {ex.Message}");
                        }
                    }
                    
                }
                else
                {
                    MessageBox.Show((string)T("noticllbsd"));
                }
            };

            // TXTBX
            btnEliminarUsuario = new();
            btnEliminarUsuario.Location = new Point(20, 120);
            btnEliminarUsuario.Font = new Font("Segoe UI", 10);
            btnEliminarUsuario.Size = new Size(200, 20);
            btnEliminarUsuario.AutoSize = true;

            Button apply = new();
            apply.Text = (string)T("apply");
            apply.Location = new Point(20, 160);
            apply.Font = new Font("Segoe UI", 10);
            apply.AutoSize = true;
            apply.Click += Apply_Click;

            Button examinar = new();
            examinar.Text = (string)T("examinar");
            examinar.Location = new Point(240, 120);
            examinar.Font = new Font("Segoe UI", 10);
            examinar.AutoSize = true;
            examinar.Click += Examinar_Click;
           
            // Agregar controles
            this.Controls.Add(lblIdioma);
            this.Controls.Add(cbShowIcons);
            this.Controls.Add(apply);
            this.Controls.Add(examinar);
            this.Controls.Add(btnEliminarUsuario);
        }

        private void Examinar_Click(object? sender, EventArgs e)
        {
            var x = new FolderBrowserDialog();
            x.ShowDialog();
            btnEliminarUsuario.Text = x.SelectedPath;
        }

        private void Apply_Click(object? sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show((string)T("changewp"), (string)T("changewpt"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                using (var key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(@"Software\deneOS\desktop"))
                {
                    key.SetValue("wallpaper", btnEliminarUsuario.Text);
                }
            }
        }
    }
}
