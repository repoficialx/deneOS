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
using Microsoft.Win32;

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

            // GroupBox para la sección del menú inicio (8x filas de textbox (path)-botón (examinar)-textbox (categoría)-botón (aplicar))
            GroupBox gbStartMenu = new GroupBox();
            gbStartMenu.Text = (string)T("startmenu");
            gbStartMenu.Location = new Point(20, 240);
            gbStartMenu.Size = new Size(475 , 300);

            #region Contenido del GroupBox
            #region Fila0
            TextBox txtPath0 = new TextBox();
            txtPath0.Location = new Point(10, 30);
            txtPath0.Size = new Size(200, 20);
            txtPath0.AutoSize = true;
            txtPath0.Text = StartMenuReg.GetRegData().Item1?[1][0] ?? "<Not set>";
            Button btnExplore0 = new Button();
            btnExplore0.Text = (string)T("examinar");
            btnExplore0.Location = new Point(220, 28);
            btnExplore0.Size = new Size(75, 23);
            btnExplore0.Click += (s, e) =>
            {
                using (var ofd = new OpenFileDialog())
                {
                    ofd.Filter = (string)T("filter.app");
                    ofd.Title = (string)T("chooseapp");
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        txtPath0.Text = ofd.FileName;
                    }
                }
            };
            TextBox txtCategory0 = new TextBox();
            txtCategory0.Location = new Point(300, 30);
            txtCategory0.Size = new Size(80, 20);
            txtCategory0.Text = StartMenuReg.GetRegData().Item1?[0][0] ?? "<Not set>";
            txtCategory0.AutoSize = true;
            Button btnApply0 = new Button();
            btnApply0.Text = (string)T("apply");
            btnApply0.Location = new Point(390, 28);
            btnApply0.Size = new Size(75, 23);
            btnApply0.Click += (s, e) =>
            {
                string[] categories = StartMenuReg.GetRegData().Item1?[0] ?? new string[8];
                string[] paths = StartMenuReg.GetRegData().Item1?[1] ?? new string[8];
                categories[0] = txtCategory0.Text;
                paths[0] = txtPath0.Text;
                StartMenuReg.SetCategoriesAndPaths(categories, paths);
                MessageBox.Show((string)T("startmenuupdated"));
            };
            #endregion

            #region Fila1
            var txtPath1 = new TextBox
            {
                Location = new Point(10, 50),
                Size = new Size(200, 20),
                AutoSize = true,
                Text = StartMenuReg.GetRegData().Item1?[1][1] ?? "<Not set>"
            };
            var btnExplore1 = new Button
            {
                Text = (string)T("examinar"),
                Location = new Point(220, 48),
                Size = new Size(75, 23)
            };
            btnExplore1.Click += (s, e) =>
            {
                using var ofd = new OpenFileDialog();
                ofd.Filter = (string)T("filter.app");
                ofd.Title = (string)T("chooseapp");
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtPath1.Text = ofd.FileName;
                }
            };
            var txtCategory1 = new TextBox
            {
                Location = new Point(300, 50),
                Size = new Size(80, 20),
                Text = StartMenuReg.GetRegData().Item1?[0][1] ?? "<Not set>",
                AutoSize = true
            };
            var btnApply1 = new Button
            {
                Text = (string)T("apply"),
                Location = new Point(390, 48),
                Size = new Size(75, 23)
            };
            btnApply1.Click += (s, e) =>
            {
                var categories = StartMenuReg.GetRegData().Item1?[0] ?? new string[8];
                var paths = StartMenuReg.GetRegData().Item1?[1] ?? new string[8];
                categories[1] = txtCategory1.Text;
                paths[1] = txtPath1.Text;
                StartMenuReg.SetCategoriesAndPaths(categories, paths);
                MessageBox.Show((string)T("startmenuupdated"));
            };
            #endregion

            #region Fila2
            var txtPath2 = new TextBox
            {
                Location = new Point(10, 75),
                Size = new Size(200, 20),
                AutoSize = true,
                Text = StartMenuReg.GetRegData().Item1?[1][2] ?? "<Not set>"
            };
            var btnExplore2 = new Button
            {
                Text = (string)T("examinar"),
                Location = new Point(220, 73),
                Size = new Size(75, 23)
            };
            btnExplore2.Click += (s, e) =>
            {
                using var ofd = new OpenFileDialog();
                ofd.Filter = (string)T("filter.app");
                ofd.Title = (string)T("chooseapp");
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtPath2.Text = ofd.FileName;
                }
            };
            var txtCategory2 = new TextBox
            {
                Location = new Point(300, 75),
                Size = new Size(80, 20),
                Text = StartMenuReg.GetRegData().Item1?[0][2] ?? "<Not set>",
                AutoSize = true
            };
            var btnApply2 = new Button
            {
                Text = (string)T("apply"),
                Location = new Point(390, 73),
                Size = new Size(75, 23)
            };
            btnApply2.Click += (s, e) =>
            {
                var categories = StartMenuReg.GetRegData().Item1?[0] ?? new string[8];
                var paths = StartMenuReg.GetRegData().Item1?[1] ?? new string[8];
                categories[2] = txtCategory2.Text;
                paths[2] = txtPath2.Text;
                StartMenuReg.SetCategoriesAndPaths(categories, paths);
                MessageBox.Show((string)T("startmenuupdated"));
            };
            #endregion

            #region Fila3
            var txtPath3 = new TextBox
            {
                Location = new Point(10, 100),
                Size = new Size(200, 20),
                AutoSize = true,
                Text = StartMenuReg.GetRegData().Item1?[1][3] ?? "<Not set>"
            };
            var btnExplore3 = new Button
            {
                Text = (string)T("examinar"),
                Location = new Point(220, 100),
                Size = new Size(75, 23)
            };
            btnExplore3.Click += (s, e) =>
            {
                using var ofd = new OpenFileDialog();
                ofd.Filter = (string)T("filter.app");
                ofd.Title = (string)T("chooseapp");
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtPath3.Text = ofd.FileName;
                }
            };
            var txtCategory3 = new TextBox
            {
                Location = new Point(300, 100),
                Size = new Size(80, 20),
                Text = StartMenuReg.GetRegData().Item1?[0][3] ?? "<Not set>",
                AutoSize = true
            };
            var btnApply3 = new Button
            {
                Text = (string)T("apply"),
                Location = new Point(390, 100),
                Size = new Size(75, 23)
            };
            btnApply3.Click += (s, e) =>
            {
                var categories = StartMenuReg.GetRegData().Item1?[0] ?? new string[8];
                var paths = StartMenuReg.GetRegData().Item1?[1] ?? new string[8];
                categories[3] = txtCategory3.Text;
                paths[3] = txtPath3.Text;
                StartMenuReg.SetCategoriesAndPaths(categories, paths);
                MessageBox.Show((string)T("startmenuupdated"));
            };
            #endregion

            #region Fila4
            var txtPath4 = new TextBox
            {
                Location = new Point(10, 125),
                Size = new Size(200, 20),
                AutoSize = true,
                Text = StartMenuReg.GetRegData().Item1?[1][4] ?? "<Not set>"
            };
            var btnExplore4 = new Button
            {
                Text = (string)T("examinar"),
                Location = new Point(220, 125),
                Size = new Size(75, 23)
            };
            btnExplore4.Click += (s, e) =>
            {
                using var ofd = new OpenFileDialog();
                ofd.Filter = (string)T("filter.app");
                ofd.Title = (string)T("chooseapp");
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtPath4.Text = ofd.FileName;
                }
            };
            var txtCategory4 = new TextBox
            {
                Location = new Point(300, 125),
                Size = new Size(80, 20),
                Text = StartMenuReg.GetRegData().Item1?[0][4] ?? "<Not set>",
                AutoSize = true
            };
            var btnApply4 = new Button
            {
                Text = (string)T("apply"),
                Location = new Point(390, 125),
                Size = new Size(75, 23)
            };
            btnApply4.Click += (s, e) =>
            {
                var categories = StartMenuReg.GetRegData().Item1?[0] ?? new string[8];
                var paths = StartMenuReg.GetRegData().Item1?[1] ?? new string[8];
                categories[4] = txtCategory4.Text;
                paths[4] = txtPath4.Text;
                StartMenuReg.SetCategoriesAndPaths(categories, paths);
                MessageBox.Show((string)T("startmenuupdated"));
            };
            #endregion

            #region Fila5
            var txtPath5 = new TextBox
            {
                Location = new Point(10, 150),
                Size = new Size(200, 20),
                AutoSize = true,
                Text = StartMenuReg.GetRegData().Item1?[1][5] ?? "<Not set>"
            };
            var btnExplore5 = new Button
            {
                Text = (string)T("examinar"),
                Location = new Point(220, 150),
                Size = new Size(75, 23)
            };
            btnExplore5.Click += (s, e) =>
            {
                using var ofd = new OpenFileDialog();
                ofd.Filter = (string)T("filter.app");
                ofd.Title = (string)T("chooseapp");
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtPath5.Text = ofd.FileName;
                }
            };
            var txtCategory5 = new TextBox
            {
                Location = new Point(300, 150),
                Size = new Size(80, 20),
                Text = StartMenuReg.GetRegData().Item1?[0][5] ?? "<Not set>",
                AutoSize = true
            };
            var btnApply5 = new Button
            {
                Text = (string)T("apply"),
                Location = new Point(390, 150),
                Size = new Size(75, 23)
            };
            btnApply5.Click += (s, e) =>
            {
                var categories = StartMenuReg.GetRegData().Item1?[0] ?? new string[8];
                var paths = StartMenuReg.GetRegData().Item1?[1] ?? new string[8];
                categories[5] = txtCategory5.Text;
                paths[5] = txtPath5.Text;
                StartMenuReg.SetCategoriesAndPaths(categories, paths);
                MessageBox.Show((string)T("startmenuupdated"));
            };
            #endregion

            #region Fila6
            var txtPath6 = new TextBox
            {
                Location = new Point(10, 175),
                Size = new Size(200, 20),
                AutoSize = true,
                Text = StartMenuReg.GetRegData().Item1?[1][6] ?? "<Not set>"
            };
            var btnExplore6 = new Button
            {
                Text = (string)T("examinar"),
                Location = new Point(220, 175),
                Size = new Size(75, 23)
            };
            btnExplore6.Click += (s, e) =>
            {
                using var ofd = new OpenFileDialog();
                ofd.Filter = (string)T("filter.app");
                ofd.Title = (string)T("chooseapp");
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtPath6.Text = ofd.FileName;
                }
            };
            var txtCategory6 = new TextBox
            {
                Location = new Point(300, 175),
                Size = new Size(80, 20),
                Text = StartMenuReg.GetRegData().Item1?[0][6] ?? "<Not set>",
                AutoSize = true
            };
            var btnApply6 = new Button
            {
                Text = (string)T("apply"),
                Location = new Point(390, 175),
                Size = new Size(75, 23)
            };
            btnApply6.Click += (s, e) =>
            {
                var categories = StartMenuReg.GetRegData().Item1?[0] ?? new string[8];
                var paths = StartMenuReg.GetRegData().Item1?[1] ?? new string[8];
                categories[6] = txtCategory6.Text;
                paths[6] = txtPath6.Text;
                StartMenuReg.SetCategoriesAndPaths(categories, paths);
                MessageBox.Show((string)T("startmenuupdated"));
            };
            #endregion

            #region Fila7
            var txtPath7 = new TextBox
            {
                Location = new Point(10, 200),
                Size = new Size(200, 20),
                AutoSize = true,
                Text = StartMenuReg.GetRegData().Item1?[1][7] ?? "<Not set>"
            };
            var btnExplore7 = new Button
            {
                Text = (string)T("examinar"),
                Location = new Point(220, 200),
                Size = new Size(75, 23)
            };
            btnExplore7.Click += (s, e) =>
            {
                using var ofd = new OpenFileDialog();
                ofd.Filter = (string)T("filter.app");
                ofd.Title = (string)T("chooseapp");
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtPath7.Text = ofd.FileName;
                }
            };
            var txtCategory7 = new TextBox
            {
                Location = new Point(300, 200),
                Size = new Size(80, 20),
                Text = StartMenuReg.GetRegData().Item1?[0][7] ?? "<Not set>",
                AutoSize = true
            };
            var btnApply7 = new Button
            {
                Text = (string)T("apply"),
                Location = new Point(390, 200),
                Size = new Size(75, 23)
            };
            btnApply7.Click += (s, e) =>
            {
                var categories = StartMenuReg.GetRegData().Item1?[0] ?? new string[8];
                var paths = StartMenuReg.GetRegData().Item1?[1] ?? new string[8];
                categories[7] = txtCategory7.Text;
                paths[7] = txtPath7.Text;
                StartMenuReg.SetCategoriesAndPaths(categories, paths);
                MessageBox.Show((string)T("startmenuupdated"));
            };
            #endregion

            #region Fila8
            var txtPath8 = new TextBox
            {
                Location = new Point(10, 225),
                Size = new Size(200, 20),
                AutoSize = true,
                Text = StartMenuReg.GetRegData().Item1?[1][7] ?? "<Not set>"
            };
            var btnExplore8 = new Button
            {
                Text = (string)T("examinar"),
                Location = new Point(220, 225),
                Size = new Size(75, 23)
            };
            btnExplore8.Click += (s, e) =>
            {
                using var ofd = new OpenFileDialog();
                ofd.Filter = (string)T("filter.app");
                ofd.Title = (string)T("chooseapp");
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtPath8.Text = ofd.FileName;
                }
            };
            var txtCategory8 = new TextBox
            {
                Location = new Point(300, 225),
                Size = new Size(80, 20),
                Text = StartMenuReg.GetRegData().Item1?[0][7] ?? "<Not set>",
                AutoSize = true
            };
            var btnApply8 = new Button
            {
                Text = (string)T("apply"),
                Location = new Point(390, 225),
                Size = new Size(75, 23)
            };
            btnApply8.Click += (s, e) =>
            {
                var categories = StartMenuReg.GetRegData().Item1?[0] ?? new string[8];
                var paths = StartMenuReg.GetRegData().Item1?[1] ?? new string[8];
                categories[7] = txtCategory8.Text;
                paths[7] = txtPath8.Text;
                StartMenuReg.SetCategoriesAndPaths(categories, paths);
                MessageBox.Show((string)T("startmenuupdated"));
            };
            #endregion

            gbStartMenu.Controls.AddRange(
            [
                txtPath0, btnExplore0, txtCategory0, btnApply0,
                txtPath1, btnExplore1, txtCategory1, btnApply1,
                txtPath2, btnExplore2, txtCategory2, btnApply2,
                txtPath3, btnExplore3, txtCategory3, btnApply3,
                txtPath4, btnExplore4, txtCategory4, btnApply4,
                txtPath5, btnExplore5, txtCategory5, btnApply5,
                txtPath6, btnExplore6, txtCategory6, btnApply6,
                txtPath7, btnExplore7, txtCategory7, btnApply7,
                txtPath8, btnExplore8, txtCategory8, btnApply8
            ]);

            #endregion

            // Agregar controles
            this.Controls.Add(lblIdioma);
            this.Controls.Add(cbShowIcons);
            this.Controls.Add(apply);
            this.Controls.Add(examinar);
            this.Controls.Add(txtWallpaperPath);
            this.Controls.Add(cbShowVolume);
            this.Controls.Add(gbStartMenu);
        }
        void LoadSMSettings()
        {
            var rawData = StartMenuReg.GetRegData();
            var arraysData = rawData.Item1;
            var showR2WButton = rawData.Item2;

            string[] categories = arraysData[0];
            string[] paths = arraysData[1];
        }
        public static class StartMenuReg
        {
            const string SubKeyPath = @"Software\deneOS\startmenu";
            public static (string[][]?, int?) GetRegData()
            {
                using (RegistryKey? key = Registry.CurrentUser.OpenSubKey(SubKeyPath, writable: false))
                {
                    if (key is not null)
                    {
                        // Leer REG_MULTI_SZ -> string[]
                        string[] categories = key.GetValue("EntriesCategories") as string[] ?? [];
                        string[] paths = key.GetValue("EntriesPaths") as string[] ?? [];

                        // Leer REG_DWORD -> int (o bool si lo prefieres)
                        int rawShowButton = Convert.ToInt32(key.GetValue("showR2WButton", 0));
                        bool showR2WButton = rawShowButton != 0;

                        Console.WriteLine($"Categorías: {string.Join(", ", categories)}");
                        Console.WriteLine($"Rutas: {string.Join(", ", paths)}");
                        Console.WriteLine($"showR2WButton: {rawShowButton} ({showR2WButton})");
                        return (new string[][] { categories, paths }, showR2WButton ? 1 : 0);
                    }
                    else
                    {
                        Console.WriteLine("La clave no existe todavía.");
                        return (null, null); // Devolver arrays vacíos si la clave no existe
                    }
                }
            }
            public static void SetCategoriesAndPaths(string[] categories, string[] paths)
            {
                using (RegistryKey? key = Registry.CurrentUser.CreateSubKey(SubKeyPath))
                {
                    if (key is not null)
                    {
                        key.SetValue("EntriesCategories", categories, RegistryValueKind.MultiString);
                        key.SetValue("EntriesPaths", paths, RegistryValueKind.MultiString);
                    }
                }
            }
            public static void SetShowR2WButton(bool show)
            {
                using (RegistryKey? key = Registry.CurrentUser.CreateSubKey(SubKeyPath))
                {
                    if (key is not null)
                    {
                        key.SetValue("showR2WButton", show ? 1 : 0, RegistryValueKind.DWord);
                    }
                }
            }

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
