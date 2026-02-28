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
            Console.WriteLine("[INFO] Initializing taskbar");
            InitializeComponent();
            Console.WriteLine("[INFO] InitializeComponent() completed.");
            this.TopMost = true;
            Console.WriteLine("[INFO] tbar intialized successfully.");
        }
        public static bool ObtenerEsEthernet()
        {
            foreach (var nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if ((nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet ||
                     nic.NetworkInterfaceType == NetworkInterfaceType.GigabitEthernet ||
                     nic.NetworkInterfaceType == NetworkInterfaceType.FastEthernetFx ||
                     nic.NetworkInterfaceType == NetworkInterfaceType.FastEthernetT) &&
                    nic.OperationalStatus == OperationalStatus.Up)
                {
                    return true;
                }
            }

            return false;
        }
        private void tbar_Load(object sender, EventArgs e)
        {
            Console.WriteLine("[INFO] Loading taskbar...");
            _keyboardHook.HookKeyboard();
            Console.WriteLine("[INFO] Keyboard hooked.");
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;
            Console.WriteLine("[INFO] Screen resolution: " + screenWidth + "x" + screenHeight);
            //uint dpi = GetDpiForWindow();
            int taskbarHeight = 48; // O el valor que tú elijas (¡como los 48px de Windows!)
            Console.WriteLine("[INFO] Setting up taskbar...");
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.Manual;
            this.Size = new Size(screenWidth, taskbarHeight);
            this.Location = new Point(0, screenHeight - taskbarHeight);
            this.TopMost = true;
            Console.WriteLine("[INFO] Taskbar docked on bottom.");
            Console.WriteLine("[INFO] Initializing timers...");

            Timer clockTimer = new() { Interval = 1000 };
            clockTimer.Tick += (s, e) => UpdateClock();
            clockTimer.Start();

            Timer systemTimer = new() { Interval = 3000 };
            systemTimer.Tick += (s, e) => UpdateSystemStatus();
            systemTimer.Start();

            Timer networkTimer = new() { Interval = 5000 };
            networkTimer.Tick += async (s, e) => await gws();
            networkTimer.Start();

            if (globaldata.isImageLoaded)
            {
                BackColor = ColorTranslator.FromHtml(globaldata.wallpaperPredominantColorHex);
            }

            /*
            Timer tim = new Timer();
            tim.Interval = 100;

            tim.Tick += new EventHandler(timer1_Tick);
            tim.Start();*/

            Timer utb = new Timer();
            utb.Interval = 5000; // Actualizar cada 5 segundos
            utb.Tick += new EventHandler(utb_Tick);
            utb.Start();
            
            Console.WriteLine("[INFO] Timers started. Setting up Windows key hook.");
            // Suscribirse al evento de la tecla de Windows
            // temporalmente no disponible porque saltaba excepción, así que la parte en donde se hookea pues está comentada
            _keyboardHook.WindowsKeyPressed += (s, e) =>
            {
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
            Console.WriteLine("[INFO] Windows key hooked successfully.");
        }
        void UpdateClock()
        {
            label19.Text = DateTime.Now.ToString("HH:mm:ss");
            label20.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        async void UpdateSystemStatus()
        {
            UpdateBattery();
            gvs();
        }
        void UpdateBattery()
        {
            PowerStatus status = SystemInformation.PowerStatus;
            float getBattery = status.BatteryLifePercent * 100;
            label21.Text = !flagMgmt.MockBattery ? $"{getBattery}%" : "100%";
            BatteryChargeStatus chargeStatus = status.BatteryChargeStatus;
            PowerLineStatus powerLineStatus = status.PowerLineStatus;
            bool isCharging = chargeStatus.HasFlag(BatteryChargeStatus.Charging);
            bool isSaverOn = dosu.Power.BatteryStatus.IsBatterySaverOn();
            label22.Text = GetBatteryIcon(getBattery, isCharging, isSaverOn);
        }
        private async void timer1_Tick(object sender, EventArgs e)
        {
            Console.WriteLine("[INFO] Updating taskbar info");
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
            Console.WriteLine("[INFO] Taskbar info updated");
        }
        private void utb_Tick(object sender, EventArgs e)
        {
            Console.WriteLine("[INFO] Updating taskbar icons...");
            RefreshTaskbar();
            Console.WriteLine("[INFO] Taskbar icons updated");
        }
        async Task gws()
        {

            Console.WriteLine("[INFO] Getting Internet status...");
            if (ObtenerEsEthernet())
            {
                // Si está conectado por Ethernet, mostrar icono de cable
                string ethernetIcon = "";
                label24.Text = ethernetIcon;
                Console.WriteLine("[INFO] Ethernet connection detected");
                return;
            }
            // Si no se puede pingear, no hay conexión a Internet.
            Ping ping = new();
            string wifiIcon;
            IPAddress google = new([142, 250, 184, 14]);
            IPStatus response;
            try
            {
                var reply = await ping.SendPingAsync(google);
                response = reply.Status;
            }
            catch (Exception)
            {
                response = IPStatus.NoResources;
            }

            
            if (!(response == IPStatus.Success))
            {
                wifiIcon = "\ue783";
                label24.Text = wifiIcon;
                Console.WriteLine("[INFO] No internet connected.");
                return; // no hay conexión a Internet
            }
            Console.WriteLine("[INFO] Internet connection detected. Getting signal strength...");
            var wifiSignal = await dosu.Network.WiFiStatus.GetWifiSignalStrengthAsync(); // 0 - 100

            if (wifiSignal < 0)
                wifiIcon = ""; // no wifi
            else if (wifiSignal <= 25)
                wifiIcon = "";
            else if (wifiSignal <= 50)
                wifiIcon = "";
            else if (wifiSignal > 50)
                wifiIcon = "";

            else
                wifiIcon = "";

            // Mostrar en UI
            label24.Text = wifiIcon;
            Console.WriteLine("[INFO] WiFi signal strength: " + wifiSignal + "%");
        }
        void gvs()
        {
            Console.WriteLine("[INFO] Getting volume status...");
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

            // Mostrar en UI
            label23.Text = voli;
            label25.Text = vol.ToString() + "%";
            if (showingVolume()) label25.Visible = true;
            else label25.Visible = false;
            Console.WriteLine("[INFO] Volume status: " + vol + "%");
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

        static string GetBatteryIcon(float percentage, bool charging, bool saverMode)
        {
            Console.WriteLine("[INFO] Getting battery status...");
            if (flagMgmt.MockBattery)
            {
                Console.WriteLine("[WARN] Battery mocking [!]. Real battery %: " + percentage);
                return ""; // Icono de batería al 100% (simulado)
            }
            if (charging)
            {
                Console.WriteLine("[INFO] Battery status: " + percentage + "% (charging).");
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
                Console.WriteLine("[INFO] Battery status: "+percentage+"% - critical (charging).");
                return ""; // batería crítica pero cargando
            }
            else if (saverMode)
            {
                Console.WriteLine("[INFO] Battery status: " + percentage + "% (saving).");
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
                Console.WriteLine("[INFO] Battery status: "+percentage+"% - critical (saving).");
                return ""; // batería crítica pero ahorrando
            }
            else
            {
                Console.WriteLine("[INFO] Battery status: " + percentage + "% (discharging).");
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
                Console.WriteLine("[WARN] Battery status: " + percentage + "% - critical.");
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
                    if (app.Proceso != null && !app.Proceso.HasExited)
                    {
                        //FocusEXE(app.Proceso.ProcessName);
                        SetForegroundWindow(app.Proceso.MainWindowHandle);
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
        private Dictionary<IntPtr, Button> _appButtons = new();

        void RefreshTaskbar()
        {
            flowLayoutPanel1.Controls.Clear(); // Limpiar iconos de antes
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
            flowLayoutPanel1.Controls.Add(home); // Añadir start al panel
            //addApps();
            var currentApps = dosu.UI.WindowTracker.ObtenerVentanas();

            var activeHandles = currentApps
                .Where(a => a.Proceso != null && !a.Proceso.HasExited)
                .Select(a => a.Proceso.MainWindowHandle)
                .ToHashSet();

            // quitar apps cerradas
            foreach (var handle in _appButtons.Keys.ToList())
            {
                if (!activeHandles.Contains(handle))
                {
                    flowLayoutPanel1.Controls.Remove(_appButtons[handle]);
                    _appButtons[handle].Dispose();
                    _appButtons.Remove(handle);
                }
            }

            // añadi nuevas apps
            foreach (var app in currentApps)
            {
                if (app.Proceso == null || app.Proceso.HasExited)
                    continue;

                var handle = app.Proceso.MainWindowHandle;

                if (!_appButtons.ContainsKey(handle))
                {
                    var btn = CreateAppButton(app);
                    _appButtons[handle] = btn;
                    flowLayoutPanel1.Controls.Add(btn);
                }
            }
        }

        private Button CreateAppButton(dynamic app)
        {
            Button app1 = new Button();
            app1.BackgroundImage = app.Icono.ToBitmap();
            app1.BackgroundImageLayout = ImageLayout.Zoom;
            app1.FlatStyle = FlatStyle.Flat;
            app1.FlatAppearance.BorderSize = 0;
            app1.Size = new Size(50, 48);
            app1.BackColor = Color.Transparent;
            
            
            app1.Paint += (s, e) =>
            {
                if (Form.ActiveForm?.Handle == app.Proceso.MainWindowHandle)
                {
                    using var brush = new SolidBrush(Color.White);
                    e.Graphics.FillRectangle(brush, 10, app1.Height - 4, app1.Width - 20, 3);
                }
            };

            app1.MouseEnter += (s, e) =>
            {
                app1.BackColor = Color.FromArgb(40, Color.White);
            };

            app1.MouseLeave += (s, e) =>
            {
                app1.BackColor = Color.Transparent;
            };

            app1.Click += (s, e) =>
            {
                if (app.Proceso != null && !app.Proceso.HasExited)
                    SetForegroundWindow(app.Proceso.MainWindowHandle);
            };

            return app1;
        }


        private void tbar_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true; // Evitar cierre
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
