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
using static Traductor;

namespace deneOS_Home
{
    public partial class tbar : Form
    {
        public tbar()
        {
            InitializeComponent();
            this.TopMost = true;

        }

        private void tbar_Load(object sender, EventArgs e)
        {
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;

            int taskbarHeight = 60; // O el valor que tú elijas (¡como los 60px de Windows!)

            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.Manual;
            this.Size = new Size(screenWidth, taskbarHeight);
            this.Location = new Point(0, screenHeight - taskbarHeight);
            this.TopMost = true;

            Timer tim = new Timer();
            tim.Interval = 100;
            tim.Tick += new EventHandler(timer1_Tick);
            tim.Start();

            Timer utb = new Timer();
            utb.Interval = 5000; // Actualizar cada 5 segundos
            utb.Tick += new EventHandler(utb_Tick);
            utb.Start();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            PowerStatus status = SystemInformation.PowerStatus;
            //OBTENER PORCENTAJE BATERÍA
            label19.Text = DateTime.Now.ToString("HH:mm:ss");
            label20.Text = DateTime.Now.ToString("dd/MM/yyyy");
            float getBattery = status.BatteryLifePercent * 100;
            label21.Text = !flagMgmt.MockBattery ? $"{getBattery}%" : "100%";
            //CAMBIAR ICONO (LABEL22)
            BatteryChargeStatus chargeStatus = status.BatteryChargeStatus;
            PowerLineStatus powerLineStatus = status.PowerLineStatus;

            bool isCharging = chargeStatus.HasFlag(BatteryChargeStatus.Charging);
            bool isSaverOn = dosu.Power.BatteryStatus.IsBatterySaverOn();

            label22.Text = GetBatteryIcon(getBattery, isCharging, isSaverOn);
            gws();
            gvs();

            if (globaldata.isImageLoaded)
            {
                BackColor = ColorTranslator.FromHtml(globaldata.wallpaperPredominantColorHex);
            }
        }
        private void utb_Tick(object sender, EventArgs e)
        {
            RefreshTaskbar();
        }
        void gws()
        {
            int wifiSignal = dosu.Network.WiFiStatus.GetWifiSignalStrength(); // 0 - 100
            string wifiIcon;
            if (wifiSignal < 0)
                wifiIcon = ""; // no wifi
            else if (wifiSignal <= 25)
                wifiIcon = "";
            else if (wifiSignal <= 50)
                wifiIcon = "";
            else if (wifiSignal > 50)
                wifiIcon = "";

            else
                wifiIcon = "";

            // Mostrar en la UI
            label24.Text = wifiIcon;

        }
        void gvs()
        {
            int vol = dosu.Audio.VolumeManager.GetSystemVolume(); // 0 - 100
            string voli;
            if (vol == 0)
                voli = ""; // no volumen
            else if (vol > 0 && vol <= 33)
                voli = "";
            else if (vol > 33 && vol <= 66)
                voli = "";
            else if (vol > 66)
                voli = "";
            else
                voli = ""; // mute

            // Mostrar en la UI
            label23.Text = voli;
            label25.Text = vol.ToString()+"%";
        }
        static string GetBatteryIcon(float percentage, bool charging, bool saverMode)
        {
            if (flagMgmt.MockBattery)
            {
                // Si se está simulando la batería, devolver un icono fijo
                return ""; // Icono de batería al 100% (simulado)
            }
            if (charging)
            {
                if (percentage >= 90) return "";
                if (percentage >= 80) return "";
                if (percentage >= 70) return "";
                if (percentage >= 60) return "";
                if (percentage >= 50) return "";
                if (percentage >= 40) return "";
                if (percentage >= 30) return "";
                if (percentage >= 20) return "";
                if (percentage >= 10) return "";
                if (percentage >= 5) return "";
                return ""; // batería crítica pero cargando
            }
            else if (saverMode)
            {
                if (percentage >= 90) return "";
                if (percentage >= 80) return "";
                if (percentage >= 70) return "";
                if (percentage >= 60) return "";
                if (percentage >= 50) return "";
                if (percentage >= 40) return "";
                if (percentage >= 30) return "";
                if (percentage >= 20) return "";
                if (percentage >= 10) return "";
                if (percentage >= 5) return "";
                return ""; // batería crítica pero ahorrando
            }
            else
            {
                if (percentage >= 90) return "";
                if (percentage >= 80) return "";
                if (percentage >= 70) return "";
                if (percentage >= 60) return "";
                if (percentage >= 50) return "";
                if (percentage >= 40) return "";
                if (percentage >= 30) return "";
                if (percentage >= 20) return "";
                if (percentage >= 10) return "";
                if (percentage >= 5) return "";
                return ""; // batería crítica
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool estaAbierto = Application.OpenForms.OfType<sm>().Any();
            if (estaAbierto)
            {
                sm form2 = Application.OpenForms.OfType<sm>().FirstOrDefault();
                if (form2 != null)
                {
                    form2.Close();
                    return;
                }

                MessageBox.Show($"ERROR 0x6: {T("e0x6")}", "deneOS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                sm sm = new sm();
                sm.Show();
            }
            
        }
        public static List<Form> VentanasAbiertas = new List<Form>();
        void addApps()
        {
            var apps = dosu.UI.WindowTracker.ObtenerVentanas();

            foreach (var app in apps)
            {
                //AppPanel app1 = new AppPanel(app.Icono.ToBitmap(), app.Titulo, "-1");
                Button app1 = new Button();
                app1.BackgroundImage = app.Icono.ToBitmap();
                app1.BackgroundImageLayout = ImageLayout.Zoom;
                app1.FlatStyle = FlatStyle.Popup;

                app1.Click += (s, e) =>
                {
                    // Aquí puedes manejar el evento de clic para abrir la ventana correspondiente
                    if (app.Proceso != null && !app.Proceso.HasExited)
                    {
                        // Si el proceso está activo, lo traemos al frente
                        //FocusEXE(app.Proceso.ProcessName);
                        SetForegroundWindow(app.Proceso.MainWindowHandle);

                        // También puedes hacer algo más aquí, como cambiar el estado del botón o mostrar información adicional
                    }
                    else
                    {
                        MessageBox.Show((string)T("appclosed"), "deneOS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                };
                ToolTip tip = new ToolTip();
                tip.SetToolTip(app1, app.Titulo);
                app1.Size = new Size(40, 60);
                app1.Location = new Point(0, 0);
                flowLayoutPanel1.Controls.Add(app1);
            }


        }
        void FocusEXE(string exeName)
        {
            string processName = exeName;

            Process[] processes = Process.GetProcessesByName(processName);
            if (processes.Length > 0)
            {
                // Get the main window handle of the first matching process
                IntPtr handle = processes[0].MainWindowHandle;

                // Bring the application to the foreground
                if (SetForegroundWindow(handle))
                {
                    Console.WriteLine($"{processName} is now focused.");
                }
                else
                {
                    Console.WriteLine($"Failed to focus {processName}.");
                }
            }
            else
            {
                Console.WriteLine($"No process named {processName} is running.");
            }
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        void RefreshTaskbar()
        {
            flowLayoutPanel1.Controls.Clear(); // Limpiar todos los iconos anteriores
            Button home = new Button();
            #region aplicar config
            home.BackgroundImage = global::deneOS_Home.Properties.Resources.denelogo;
            home.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            home.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            home.Location = new System.Drawing.Point(0, 0);
            home.Name = "button1";
            home.Size = new System.Drawing.Size(72, 60);
            home.TabIndex = 0;
            home.UseVisualStyleBackColor = true;
            home.Click += new System.EventHandler(this.button1_Click);
            #endregion
            flowLayoutPanel1.Controls.Add(home); // Añadir el botón de inicio al panel
            addApps();
        }

        private void tbar_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true; // Evitar que se cierre el formulario
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label23_Click(object sender, EventArgs e)
        {
            volSlider volSlider = new volSlider();
            var trigger = label23;
            var triggerScreenPos = trigger.PointToScreen(Point.Empty);

            // Centro horizontal del label
            int centerX = triggerScreenPos.X + trigger.Width / 2;

            // Tamaño del popup
            int popupWidth = volSlider.Width;
            int popupHeight = volSlider.Height;

            // Posición final del popup
            int popupX = centerX - (popupWidth / 2);
            int popupY = triggerScreenPos.Y - popupHeight - 10; // 10px encima

            volSlider.StartPosition = FormStartPosition.Manual;
            volSlider.Location = new Point(popupX, popupY);
            volSlider.Show();

        }
    }
}
