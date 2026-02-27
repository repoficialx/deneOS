using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
        public TextBox txtWallpaperPath;
        public CustomControl()
        {
            InitializeComponent();
            SetupUI();
            SetDefaultStatus();
            loaded = true;
        }

        private bool loaded = false;
        private CheckBox cbShowVolume;
        private CheckBox cbShowIcons;
        private void GeneralControl_Load(object sender, EventArgs e)
        {

        }

        void SetDefaultStatus()
        {
            if (showingVolume())
            {
                cbShowVolume.Checked = true;
            }

            if (showingIcons())
            {
                cbShowIcons.Checked = true;
            }

            txtWallpaperPath.Text = getWpPath();
        }
        static bool showingVolume()
        {
            // HKCU\SOFTWARE\deneOS\taskbar\showVolumePercentage (DWORD) 0 o 1
            string path = @"SOFTWARE\deneOS\taskbar";
            string key = "showVolumePercentage";
            using (var registryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(path))
            {
                if (registryKey != null)
                {
                    object value = registryKey.GetValue(key, 0);
                    if (value != null && value is int intValue)
                    {
                        return intValue == 1;
                    }
                }
            }

            return false;
        }

        static string getWpPath()
        {
            // HKCU\SOFTWARE\deneOS\desktop\wallpaper (String) "C:\path\to\wallpaper.jpg"
            string path = @"SOFTWARE\deneOS\desktop";
            string key = "wallpaper";
            using (var registryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(path))
            {
                if (registryKey != null)
                {
                    object value = registryKey.GetValue(key, "");
                    if (value != null && value is string strValue)
                    {
                        return strValue;
                    }
                }
            }

            return "< NONE >";
        }
        static bool showingIcons()
        {
            // HKCU\SOFTWARE\deneOS\desktop\showIcons (DWORD) 0 o 1
            string path = @"SOFTWARE\deneOS\desktop";
            string key = "showIcons";
            using (var registryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(path))
            {
                if (registryKey != null)
                {
                    object value = registryKey.GetValue(key, 0);
                    if (value != null && value is int intValue)
                    {
                        return intValue == 1;
                    }
                }
            }

            return false;
        }
        private void SetupUI()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.White;

            // Label Idioma
            Label lblIdioma = new Label();
            lblIdioma.Text = (string)T("customisation");
            lblIdioma.Location = new Point(20, 20);
            lblIdioma.Font = new Font("Segoe UI Variable Display", 24);
            lblIdioma.AutoSize = true;

            // Checkbox mostrar iconos
            cbShowIcons = new CheckBox();
            cbShowIcons.Text = (string)T("showIcons");
            cbShowIcons.Location = new Point(20, 90);
            cbShowIcons.Font = new Font("Segoe UI", 10);
            cbShowIcons.AutoSize = true;
            cbShowIcons.CheckedChanged += (s, e) =>
            {
                if (!loaded) return; // Evitar que se ejecute al iniciar el control
                if (cbShowIcons.Checked)
                {
                    MessageBox.Show((string)T("icllbsd"));
                    /*if (!AdminHelper.IsRunningAsAdmin())
                    {
                        AdminHelper.RestartAsAdmin("page:custom");
                        MessageBox.Show($"{T("err")}: {T("errnotpermission")}", $"{T("permsneeded")}", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    **/
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
                    //}
                    
                }
                else
                {
                    MessageBox.Show((string)T("noticllbsd"));
                }
            };

            // TXTBX
            txtWallpaperPath = new();
            txtWallpaperPath.Location = new Point(20, 120);
            txtWallpaperPath.Font = new Font("Segoe UI", 10);
            txtWallpaperPath.Size = new Size(200, 20);
            txtWallpaperPath.AutoSize = true;

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

            // Checkbox mostrar volumen en taskbar
            cbShowVolume = new CheckBox();
            cbShowVolume.Text = (string)T("showvolumetaskbar");
            cbShowVolume.Location = new Point(20, 200);
            cbShowVolume.Font = new Font("Segoe UI", 10);
            cbShowVolume.AutoSize = true;
            cbShowVolume.CheckedChanged += (s, e) =>
            {
                if (!loaded) return; // Evitar que se ejecute al iniciar el control
                if (cbShowVolume.Checked)
                {
                    MessageBox.Show((string)T("svtbds"));
                    /*if (!AdminHelper.IsRunningAsAdmin())
                    {
                        AdminHelper.RestartAsAdmin("page:custom");
                        MessageBox.Show($"{T("err")}: {T("errnotpermission")}", $"{T("permsneeded")}", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {*/
                        try
                        {
                            using (var key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(@"Software\deneOS\taskbar"))
                            {
                                key.SetValue("showVolumePercentage", 1);
                            }
                            MessageBox.Show($"{T("regeditedsuccess")}. {T("svtbds")}");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"{T("errconfiguringsvtb")}: {ex.Message}");
                        }
                    //}
                }
                else
                {
                    MessageBox.Show((string)T("notsvtbds"));
                }
            };

            // Agregar controles
            this.Controls.Add(lblIdioma);
            this.Controls.Add(cbShowIcons);
            this.Controls.Add(apply);
            this.Controls.Add(examinar);
            this.Controls.Add(txtWallpaperPath);
            this.Controls.Add(cbShowVolume);
        }

        private void Examinar_Click(object? sender, EventArgs e)
        {/*
            var x = new FolderBrowserDialog();
            x.ShowDialog();
            txtWallpaperPath.Text = x.SelectedPath;*/
            //MessageBox.Show("(debug) Filtro: " + (string)T("filter.img"));
            //Process.GetCurrentProcess().Kill();
            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = (string)T("filter.img");
                ofd.Title = (string)T("choosewp");

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtWallpaperPath.Text = ofd.FileName;
                }
            }
        }

        private void Apply_Click(object? sender, EventArgs e)
        {
            if (!File.Exists(txtWallpaperPath.Text))
            {
                MessageBox.Show("Selecciona una imagen válida primero.");
                return;
            }


            DialogResult result = MessageBox.Show((string)T("changewp"), (string)T("changewpt"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                using (var key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(@"Software\deneOS\desktop"))
                {
                    key.SetValue("wallpaper", txtWallpaperPath.Text);
                }
            }
        }
    }
}
