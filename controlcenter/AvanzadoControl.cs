using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
#pragma warning disable CS8622
#pragma warning disable CS8618
namespace controlcenter
{
    public partial class AvanzadoControl : UserControl
    {
        public AvanzadoControl()
        {
            InitializeComponent();
            SetupUI();
            //LoadFlags();
        }
        private Button btnRootRestart;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0044:Agregar modificador de solo lectura", Justification = "<pendiente>")]
        private ListBox lstFlags;
        private Button btnOpenSystemFolder;
        private Button btnControlPanel;
        private CheckedListBox checkedListBoxFlags;
        private Button btnLaunchWithFlags;
        private Label lblFlagsDescription;

        private void SetupUI()
        {
            this.Dock = DockStyle.Fill;

            // Botón de reinicio con root
            btnRootRestart = new Button()
            {
                Text = (string)T("restartinroot"),
                Location = new System.Drawing.Point(20, 0),
                AutoSize = true,
            };
            btnRootRestart.Click += BtnRootRestart_Click;
            this.Controls.Add(btnRootRestart);
            /*
            // Label + ListBox para flags activas
            Label lblFlags = new Label()
            {
                Text = "Flags activas:",
                Location = new System.Drawing.Point(20, 65)
            };
            this.Controls.Add(lblFlags);*/

            /*lstFlags = new ListBox()
            {
                Location = new System.Drawing.Point(20, 85),
                Size = new System.Drawing.Size(250, 100)
            };
            this.Controls.Add(lstFlags);*/

            // CheckedListBox
            checkedListBoxFlags = new CheckedListBox();
            checkedListBoxFlags.Location = new Point(20, 40);
            checkedListBoxFlags.Size = new Size(300, 180);
            checkedListBoxFlags.CheckOnClick = true;
            checkedListBoxFlags.SelectedIndexChanged += (s, e) =>
            {
                string selected = checkedListBoxFlags.SelectedItem?.ToString() ?? "";
                lblFlagsDescription.Text = selected switch
                {
                    "/dangerZone:enableRoot" => (string)T("dzrootdesc"),
                    "/safeMode" => (string)T("sfmodedesc"),
                    "/language:es" => (string)T("langesdesc"),
                    "/bypassChecks" => (string)T("bypasschecksDesc"),
                    _ => (string)T("descNotAvailable")
                };
            };


            // Botón
            btnLaunchWithFlags = new Button();
            btnLaunchWithFlags.Text = (string)T("bootwithselectedflags");
            btnLaunchWithFlags.Location = new Point(20, 230);
            btnLaunchWithFlags.Size = new Size(300, 30);
            btnLaunchWithFlags.Click += btnLaunchWithFlags_Click;

            // Label para descripción
            lblFlagsDescription = new Label();
            lblFlagsDescription.Text =(string)T("selectaflagtoseedesc");
            lblFlagsDescription.Location = new Point(20, 270);
            lblFlagsDescription.Size = new Size(300, 60);
            lblFlagsDescription.AutoSize = false;

            // Añadir al panel o form
            this.Controls.Add(checkedListBoxFlags);
            this.Controls.Add(btnLaunchWithFlags);
            this.Controls.Add(lblFlagsDescription);


            // Botón para abrir carpeta de sistema
            btnOpenSystemFolder = new Button()
            {
                Text = (string)T("opensysfolder"),
                Location = new System.Drawing.Point(20, 340),
                AutoSize = true
            };
            btnOpenSystemFolder.Click += BtnOpenSystemFolder_Click;
            this.Controls.Add(btnOpenSystemFolder);

            // Botón para panel de control deneOS
            btnControlPanel = new Button()
            {
                Text = (string)T("deneOScontrolpanel"),
                Location = new System.Drawing.Point(20, 370),
                AutoSize = true,
                Enabled = false // activar cuando esté implementado
            };
            btnControlPanel.Click += BtnControlPanel_Click;
            this.Controls.Add(btnControlPanel);
        }
        private void btnLaunchWithFlags_Click(object sender, EventArgs e)
        {
            var selectedFlags = checkedListBoxFlags.CheckedItems
                .Cast<string>()
                .ToArray();

            if (selectedFlags.Length == 0)
            {
                MessageBox.Show((string)T("errselectoneflag"), (string)T("warn"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string combinedArgs = string.Join(" ", selectedFlags);

            try
            {
                Process.Start(new ProcessStartInfo
                {
                    //FileName = @"C:\DENEOS\core\deneOS_Home.exe", // para pruebas usamos el ultimo desde bin\debug
                    FileName = @"C:\Users\rayel\source\repos\!New\repos\deneOS\deneOS_Home\bin\x64\Debug\deneOS_Home.exe",
                    Arguments = combinedArgs,
                    UseShellExecute = true,
                    Verb = "runas"
                });

                Process.GetCurrentProcess().Kill();
            }
            catch (Exception ex)
            {
                MessageBox.Show((string)T("errorbootingwithflags") + ":\n" + ex.Message, (string)T("err"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /*private void LoadFlags()
        {
            string[] activeFlags = flagMgmt.GetActiveFlags(); // asumiendo que tienes este método
            foreach (var flag in activeFlags)
            {
                lstFlags.Items.Add(flag);
            }
        }*/

        private void BtnOpenSystemFolder_Click(object sender, EventArgs e)
        {
            string systemPath = @"C:\DENEOS\systemApps\";
            if (Directory.Exists(systemPath))
                Process.Start("explorer.exe", systemPath);
            else
                MessageBox.Show((string)T("errsysfoldernotfound"));
        }

        private void BtnControlPanel_Click(object sender, EventArgs e)
        {
            MessageBox.Show((string)T("errdeneoscontrolpanelnotavailable"));
        }

        private void BtnRootRestart_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                (string)T("deneoswillrestartwithroot"),
                (string)T("confirmrestartroot"),
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    // Ruta al ejecutable principal
                    string deneOSExe = @"C:\DENEOS\core\deneOS_Home.exe";
                    Process.GetProcessesByName("deneOS_Home").ToList().ForEach(p => p.Kill()); // Cerrar cualquier instancia de deneOS_Home

                    // Lanzar deneOS con la flag de root
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = deneOSExe,
                        Arguments = "/dangerZone:enableRoot",
                        UseShellExecute = true,
                        Verb = "runas" // Ejecutar como admin
                    });

                    // Cerrar el proceso actual (Centro de control)
                    Process.GetCurrentProcess().Kill();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{T("errrestartingroot")}: {ex.Message}", (string)T("err"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void AvanzadoControl_Load(object sender, EventArgs e)
        {
            void AdvancedPanel_Load()
            {
                string[] flags = new string[]
                {
                    "/dangerZone:enableRoot",
                    "/dangerZone:debug",
                    "/dangerZone:disableLockScreen",
                    "/dangerZone:skipBootAnim",
                    "/dangerZone:mockBattery",
                    "/dangerZone:classicMode",
                    "/dangerZone:forceUpdate",
                    "/testing:untranslatedStrings",
                    "/safeMode",
                    "/safeMode:network",
                    "/language:es",
                    "/language:en",
                    "/resetPrefs",
                    "/recover",
                    "/log",
                    "/bypassChecks",
                    "/sysinfo",
                    "/resetToBIOS"
                };

                checkedListBoxFlags.Items.AddRange(flags);
            }
            AdvancedPanel_Load(); 
        }
    }
}