#pragma warning disable CS8622
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Traductor;
using Timer = System.Windows.Forms.Timer;

namespace deneOS
{
    public partial class tbar : Form
    {
        private KeyboardHook _keyboardHook = new KeyboardHook();
        public tbar()
        {
            Console.WriteLine("[INFO] Iniciando barra de tareas...");
            InitializeComponent();
            Console.WriteLine("[INFO] InitializeComponent() completado.");
            this.TopMost = true;
            Console.WriteLine("[INFO] tbar inicializado correctamente.");
        }

        private void tbar_Load(object sender, EventArgs e)
        {
            Console.WriteLine("[INFO] Cargando tbar...");
            _keyboardHook.HookKeyboard();
            Console.WriteLine("[INFO] Teclado enganchado.");
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;
            Console.WriteLine("[INFO] Obtenida resolución de pantalla: " + screenWidth + "x" + screenHeight);
            //uint dpi = GetDpiForWindow();
            int taskbarHeight = 48; // O el valor que tú elijas (¡como los 48px de Windows!)
            Console.WriteLine("[INFO] Configurando barra de tareas...");
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.Manual;
            this.Size = new Size(screenWidth, taskbarHeight);
            this.Location = new Point(0, screenHeight - taskbarHeight);
            this.TopMost = true;
            Console.WriteLine("[INFO] Barra de tareas configurada en la parte inferior de la pantalla.");
            Console.WriteLine("[INFO] Inicializando Timers");
            Timer tim = new Timer();
            tim.Interval = 100;

            tim.Tick += new EventHandler(timer1_Tick);
            tim.Start();

            Timer utb = new Timer();
            utb.Interval = 5000; // Actualizar cada 5 segundos
            utb.Tick += new EventHandler(utb_Tick);
            utb.Start();
            Console.WriteLine("[INFO] Timers iniciados. Configurando el aganche del teclado.");
            // Suscribirse al evento de la tecla de Windows
            _keyboardHook.WindowsKeyPressed += (s, e) =>
            {
                // Aquí pones la lógica para mostrar/ocultar el menú de inicio (sm)
                bool estaAbierto = Application.OpenForms.OfType<sm>().Any();
                if (estaAbierto)
                {
                    Application.OpenForms.OfType<sm>().FirstOrDefault()?.Close();
                }
                else
                {
                    new sm().Show();
                }
            };
            Console.WriteLine("[INFO] Tecla de Windows enganchada correctamente.");
        }
        private async void timer1_Tick(object sender, EventArgs e)
        {
            Console.WriteLine("[INFO] Actualizando información de la barra de tareas...");
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
            await gws();
            gvs();

            if (globaldata.isImageLoaded)
            {
                BackColor = ColorTranslator.FromHtml(globaldata.wallpaperPredominantColorHex);
            }
            Console.WriteLine("[INFO] Información de la barra de tareas actualizada.");
        }
        private void utb_Tick(object sender, EventArgs e)
        {
            Console.WriteLine("[INFO] Actualizando iconos de la barra de tareas...");
            RefreshTaskbar();
            Console.WriteLine("[INFO] Iconos de la barra de tareas actualizados.");
        }
        async Task gws()
        {
            Console.WriteLine("[INFO] Obteniendo estado de la conexión WiFi...");
            // Si no se puede pingear, no hay conexión a Internet.
            Ping ping = new();
            string wifiIcon;
            IPAddress google = new([142, 250, 184, 14]);
            var reply = ping.Send(google);
            if (!(reply.Status == IPStatus.Success))
            {
                wifiIcon = "";
                label24.Text = wifiIcon;
                Console.WriteLine("[INFO] No hay conexión a Internet.");
                return; // no hay conexión a Internet
            }
            Console.WriteLine("[INFO] Conexión a Internet detectada. Obteniendo intensidad de la señal WiFi...");
            var wifiSignal = await dosu.Network.WiFiStatus.GetWifiSignalStrengthAsync(); // 0 - 100

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
            Console.WriteLine("[INFO] Intensidad de la señal WiFi obtenida: " + wifiSignal + "%");
        }
        void gvs()
        {
            Console.WriteLine("[INFO] Obteniendo volumen del sistema...");
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
            label25.Text = vol.ToString() + "%";
            Console.WriteLine("[INFO] Volumen del sistema obtenido: " + vol + "%");
        }
        static string GetBatteryIcon(float percentage, bool charging, bool saverMode)
        {
            Console.WriteLine("[INFO] Obteniendo icono de batería...");
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
                Console.WriteLine("[INFO] Icono de batería obtenido: batería crítica pero cargando.");
                return ""; // batería crítica pero cargando
            }
            else if (saverMode)
            {
                Console.WriteLine("[INFO] Estado de batería obtenido: batería al " + percentage + "% pero ahorrando.");
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
                Console.WriteLine("[INFO] Icono de batería obtenido: batería crítica y ahorrando.");
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
                Console.WriteLine("[WARN] Batería crítica <5%: " + percentage + '%');
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
                app1.Size = new Size(50, 48);
                app1.Location = new Point(0, 0);
                flowLayoutPanel1.Controls.Add(app1);
            }


        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        void RefreshTaskbar()
        {
            flowLayoutPanel1.Controls.Clear(); // Limpiar todos los iconos anteriores
            Button home = new Button();
            #region aplicar config
            home.BackgroundImage = global::deneOS.Properties.Resources.denelogo;
            home.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            home.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            home.Location = new System.Drawing.Point(0, 0);
            home.Name = "button1";
            home.Size = new System.Drawing.Size(72, 48);
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

        private void tbar_FormClosed(object sender, FormClosedEventArgs e)
        {
            _keyboardHook.UnhookKeyboard();
        }
        private void ApagarSistema()
        {
            // 1. Desenganchar el hook del teclado
            _keyboardHook.UnhookKeyboard();

            // 2. Cerrar la aplicación
            Application.Exit();
        }

        private void flowLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
