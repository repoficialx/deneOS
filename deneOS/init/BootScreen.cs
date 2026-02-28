using deneOS.Security;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static dosu.UniversalConfiguration;
using static Traductor;
using Timer = System.Windows.Forms.Timer;

namespace deneOS.init
{
    public partial class BootScreen : Form
    {
        private string[] bootAnillas = new string[]
    {
        "\uE052", "\uE053", "\uE054", "\uE055", "\uE056", "\uE057", "\uE058", "\uE059",
        "\uE05A", "\uE05B", "\uE05C", "\uE05D", "\uE05E", "\uE05F", "\uE060", "\uE061",
        "\uE062", "\uE063", "\uE064", "\uE065", "\uE066", "\uE067", "\uE068", "\uE069",
        "\uE06A", "\uE06B", "\uE06C", "\uE06D", "\uE06E", "\uE06F", "\uE070", "\uE071",
        "\uE072", "\uE073", "\uE074", "\uE075", "\uE076", "\uE077", "\uE078", "\uE079",
        "\uE07A", "\uE07B", "\uE07C", "\uE07D", "\uE07E", "\uE07F", "\uE080", "\uE081",
        "\uE082", "\uE083", "\uE084", "\uE085", "\uE086", "\uE087", "\uE088", "\uE089",
        "\uE08A", "\uE08B", "\uE08C", "\uE08D", "\uE08E", "\uE08F", "\uE090", "\uE091",
        "\uE092", "\uE093", "\uE094", "\uE095", "\uE096", "\uE097", "\uE098", "\uE099",
        "\uE09A", "\uE09B", "\uE09C", "\uE09D", "\uE09E", "\uE09F", "\uE0A0", "\uE0A1",
        "\uE0A2", "\uE0A3", "\uE0A4", "\uE0A5", "\uE0A6", "\uE0A7", "\uE0A8", "\uE0A9",
        "\uE0AA", "\uE0AB", "\uE0AC", "\uE0AD", "\uE0AE", "\uE0AF", "\uE0B0", "\uE0B1",
        "\uE0B2", "\uE0B3", "\uE0B4", "\uE0B5", "\uE0B6", "\uE0B7", "\uE0B8", "\uE0B9",
        "\uE0BA", "\uE0BB", "\uE0BC", "\uE0BD", "\uE0BE", "\uE0BF", "\uE0C0", "\uE0C1",
        "\uE0C2", "\uE0C3", "\uE0C4", "\uE0C5", "\uE0C6", "\uE0C7"
    };
        private int bootIndex = 0;
        private System.Windows.Forms.Timer bootTimer;
        private bool skipUserCheck = false;
        public BootScreen()
        {
            // comprobar flags importantes antes del inicio
            if (flagMgmt.EmergencyUI)
            {
                // Mostrar pantalla de emergencia sin inicializar nada más
                EmergencyScreen emergencyScreen = new EmergencyScreen();
                emergencyScreen.ShowDialog();
                Application.Exit();
                return;
            }

            if (flagMgmt.SkipBootAnim)
            {
                // Saltar completamente la pantalla de boot
                InitializeComponent();
                this.Opacity = 0; // Hacerlo invisible
                this.Show();

                DisableExplorer();
                FilenFolderCheck();
                CargarIdioma();

                // Iniciar directamente el login
                this.BeginInvoke(new Action(() =>
                {
                    this.Hide();
                    new logonui().Show();
                }));
                return;
            }

            if (flagMgmt.ForceOOBE)
            {
                skipUserCheck = true;
            }

            // iniciar cosas visuales
            InitializeComponent();

            this.DoubleBuffered = true;
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            UpdateStyles();

            // config. la ventana
            ConfigureWindow();

            // iniciar el timer de la anim.
            InitializeBootAnimation();

            // mostrar el form
            this.Show();
            Application.DoEvents(); // render inicial
        }
        private void ConfigureWindow()
        {
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;

            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);
            this.Size = new Size(screenWidth, screenHeight);
            this.FormBorderStyle = FormBorderStyle.None;
            this.TopMost = true;
            this.BackColor = Color.Black;
            this.WindowState = FormWindowState.Maximized;

            // centrar y redim. logo
            float posX = 684f / 1920f;
            float posY = 261f / 1080f;
            float widthPct = 572f / 1920f;
            float heightPct = 468f / 1080f;

            logo.Location = new Point((int)(posX * this.Width), (int)(posY * this.Height));
            logo.Size = new Size((int)(widthPct * this.Width), (int)(heightPct * this.Height));
        }
        private void InitializeBootAnimation()
        {
            // crear el timer de la anim.
            bootTimer = new Timer();
            bootTimer.Interval = 30;
            bootTimer.Tick += BootTimer_Tick;
            bootTimer.Start();

            Console.WriteLine("[INFO] Animación de boot iniciada");
        }
        void DisableExplorer()
        {
            Process[] processes = Process.GetProcessesByName("explorer");
            foreach (Process process in processes)
            {
                process.Kill(true);
            }

            ConfigureExplorerAutoRestart();
        }
        private void ConfigureExplorerAutoRestart()
        {
            const string subkey = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon";
            const string valueName = "AutoRestartShell";

            // abrir la key (con write access)
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(subkey, true))
            {
                if (key != null)
                {
                    // obtener el value
                    object value = key.GetValue(valueName);
                    int? currentValue = value as int?;

                    // si es 1, que sea 0
                    if (currentValue.HasValue && currentValue.Value == 1)
                    {
                        key.SetValue(valueName, 0, RegistryValueKind.DWord);
                        Console.WriteLine("AutoRestartShell cambiado a 0 para prevenir el reinicio de explorer.exe.");
                        SetAutoRun();
                        RebootSystem();
                    }
                    else if (currentValue.HasValue && currentValue.Value == 0)
                    {
                        // si es 0 está bien
                        Console.WriteLine("AutoRestartShell ya es 0. No se requiere ninguna acción.");
                    }
                    else
                    {
                        // si no existe o es otra cosa, que sea 0
                        key.SetValue(valueName, 0, RegistryValueKind.DWord);
                        Console.WriteLine("AutoRestartShell no existía o tenía un valor inesperado, se ha creado y establecido a 0.");
                        SetAutoRun();
                        RebootSystem();
                    }
                }
                else
                {
                    Console.WriteLine("No se pudo abrir la clave del registro. Asegúrese de que el programa se ejecuta con permisos de administrador.");
                }
            }
        }
        private void SetAutoRun()
        {
            const string runKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(runKey, true))
            {
                if (key != null)
                {
                    key.SetValue("deneOS", Application.ExecutablePath);
                }
            }
        }
        private void RebootSystem()
        {
            Process.Start("shutdown.exe", "/r /t 0");
        }
        void FilenFolderCheck()
        {
            Console.WriteLine("[INFO] Verificando estructura de archivos...");

            try
            {
                // Verificar carpetas
                bool existeCarpetaDN = Directory.Exists("C:\\DENEOS");
                bool existeHomeEdition = Directory.Exists("C:\\DENEOS\\sysconf\\");

                // Verificar archivos
                bool existeLauncherCFG = File.Exists("C:\\DENEOS\\sysconf\\config.ini");
                bool existecfgIdioma = File.Exists("C:\\DENEOS\\sysconf\\lang.ini");

                bool cfgIdiomaContenido = false;
                bool cfgIdiomaNotNull = false;

                if (existecfgIdioma)
                {
                    string contenido = File.ReadAllText("C:\\DENEOS\\sysconf\\lang.ini");
                    cfgIdiomaContenido = !string.IsNullOrWhiteSpace(contenido);
                    cfgIdiomaNotNull = contenido != null;
                }

                bool carpetas = existeCarpetaDN && existeHomeEdition;
                bool archivos = existeLauncherCFG && existecfgIdioma;
                bool nonempty = cfgIdiomaNotNull && cfgIdiomaContenido;

                if (carpetas && archivos && nonempty)
                {
                    Console.WriteLine("[SUCCESS] Todos los archivos presentes");
                }
                else
                {
                    Console.WriteLine("[WARN] Archivos faltantes detectados");
                    Console.WriteLine($"[INFO] Carpeta DENEOS: {existeCarpetaDN}");
                    Console.WriteLine($"[INFO] Carpeta sysconf: {existeHomeEdition}");
                    Console.WriteLine($"[INFO] config.ini: {existeLauncherCFG}");
                    Console.WriteLine($"[INFO] lang.ini: {existecfgIdioma}");

                    // ✅ CREAR estructura automáticamente
                    CreateMissingStructure();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Error verificando archivos: {ex.Message}");
            }
        }
        void CreateMissingStructure()
        {
            Console.WriteLine("[INFO] Creando estructura de archivos faltante...");

            try
            {
                // Crear carpetas
                Directory.CreateDirectory("C:\\DENEOS");
                Directory.CreateDirectory("C:\\DENEOS\\sysconf");
                Directory.CreateDirectory("C:\\DENEOS\\lang");
                Directory.CreateDirectory("C:\\DENEOS\\desktop");

                // Crear lang.ini con inglés por defecto
                if (!File.Exists("C:\\DENEOS\\sysconf\\lang.ini"))
                {
                    File.WriteAllText("C:\\DENEOS\\sysconf\\lang.ini", "en");
                }

                // Crear archivo de idioma inglés básico
                if (!File.Exists("C:\\DENEOS\\lang\\en.json"))
                {
                    string basicEnglish = @"{
  ""txt1"": ""00:00"",
  ""txt2"": ""Date"",
  ""welctodeneosE"": ""Welcome to deneOS!"",
  ""welc"": ""Welcome"",
  ""sisfct"": false,
  ""mo1"": ""January"",
  ""mo2"": ""February"",
  ""mo3"": ""March"",
  ""mo4"": ""April"",
  ""mo5"": ""May"",
  ""mo6"": ""June"",
  ""mo7"": ""July"",
  ""mo8"": ""August"",
  ""mo9"": ""September"",
  ""mo10"": ""October"",
  ""mo11"": ""November"",
  ""mo12"": ""December"",
  ""dowmon"": ""Mon"",
  ""dowtue"": ""Tue"",
  ""dowwed"": ""Wed"",
  ""dowthu"": ""Thu"",
  ""dowfri"": ""Fri"",
  ""dowsat"": ""Sat"",
  ""dowsun"": ""Sun""
}";
                    File.WriteAllText("C:\\DENEOS\\lang\\en.json", basicEnglish);
                }

                Console.WriteLine("[SUCCESS] Estructura creada exitosamente");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Error creando estructura: {ex.Message}");
            }
        }
        void CargarIdioma()
        {
            Console.WriteLine("[INFO] Cargando idioma...");

            try
            {
                if (flagMgmt.ShowUntranslatedStrings)
                {
                    UN_ST = true;
                    Console.WriteLine("[INFO] Modo strings sin traducir activado");
                    return;
                }

                string idioma = "en"; // ✅ Fallback por defecto

                if (flagMgmt.Language != "")
                {
                    idioma = flagMgmt.Language;
                    Console.WriteLine($"[INFO] Idioma desde flag: {idioma}");
                }
                else
                {
                    try
                    {
                        idioma = dosu.UniversalConfiguration.GetLang();
                        Console.WriteLine($"[INFO] Idioma desde configuración: {idioma}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[WARN] No se pudo obtener idioma de configuración: {ex.Message}");
                        Console.WriteLine("[INFO] Usando idioma por defecto: en");
                    }
                }

                Cargar(idioma);
                Console.WriteLine($"[SUCCESS] Idioma cargado: {idioma}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Error cargando idioma: {ex.Message}");
                Console.WriteLine("[INFO] Continuando sin traducciones...");
            }
        }
        private void BootScreen_Load(object sender, EventArgs e)
        {
            DisableExplorer();
            FilenFolderCheck();
            CargarIdioma();

            // ✅ Ejecutar migración de contraseñas si es necesario
            try
            {
                PasswordMigration.MigrateIfNeeded();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Error en migración de contraseñas: {ex.Message}");
                // No bloqueamos el arranque por esto, pero lo registramos
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();

            Console.WriteLine("[INFO] Timer de boot completado, iniciando login...");

            try
            {
                //if (skipUserCheck) throw new Exception("Flag ForceOOBE activado, saltando verificación de usuario");
                if (skipUserCheck)
                {
                    Console.WriteLine("[INFO] ForceOOBE activado, saltando verificación de usuario...");
                    this.BeginInvoke(new Action(() =>
                    {
                        new OOBE.PCWelcomeBG().ShowDialog();
                    }));
                    return;
                }

                this.Hide();

                // obtener user con dosu
                User user = GetUser();

                // check user / password no vacíos
                bool isUserNull = user.Equals(null);
                bool hasValidUser = (!isUserNull) &&
                                    (!string.IsNullOrWhiteSpace(user.Username)) &&
                                    (!string.IsNullOrWhiteSpace(user.Password));

                if (!hasValidUser)
                {
                    Console.WriteLine("[INFO] No hay usuario configurado, iniciando OOBE...");

                    this.BeginInvoke(new Action(() =>
                    {
                        new OOBE.PCWelcomeBG().ShowDialog();
                    }));
                }
                else
                {
                    Console.WriteLine($"[INFO] Usuario encontrado: {user.Username}, mostrando login...");

                    this.BeginInvoke(new Action(() =>
                    {
                        new logonui().Show();
                    }));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Error en timer1_Tick: {ex.Message}");

                // Fallback: Si hay error, ir a OOBE por seguridad
                this.BeginInvoke(new Action(() =>
                {
                    new OOBE.PCWelcomeBG().ShowDialog();
                }));
            }
        }
        private void BootTimer_Tick(object sender, EventArgs e)
        {
            label1.Text = bootAnillas[bootIndex];
            bootIndex = (bootIndex + 1) % bootAnillas.Length;
        }
    }
}
