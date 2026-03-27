using deneOS.Security;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using static dosu.UniversalConfiguration;
using static Traductor;
using Timer = System.Windows.Forms.Timer;

namespace deneOS.init
{
    /// <summary>
    /// Clase base compartida para BootScreen (horizontal) y BootScreenVertical.
    /// Contiene toda la lógica de arranque; las subclases solo definen
    /// el control de logo y su posicionamiento proporcional.
    /// </summary>
    public abstract partial class BootScreenBase : Form
    {
        // ── Animación de boot ────────────────────────────────────────────
        private static readonly string[] BootAnillas =
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

        private int _bootIndex = 0;
        private Timer _bootTimer;

        // ── Propiedades abstractas que cada subclase define ──────────────

        /// <summary>Control que muestra el logo (PictureBox o similar).</summary>
        protected abstract Control LogoControl { get; }

        /// <summary>Control que muestra la animación de spinning.</summary>
        protected abstract Label SpinnerLabel { get; }

        /// <summary>Timer del Designer que dispara el paso a logonui/OOBE.</summary>
        protected abstract Timer TransitionTimer { get; }

        /// <summary>
        /// Posiciona y dimensiona LogoControl en pantalla.
        /// Cada subclase implementa su propia disposición.
        /// </summary>
        protected abstract void PositionLogo();

        // ── Constructor compartido ───────────────────────────────────────

        protected void InitializeBootFlow()
        {
            if (flagMgmt.EmergencyUI)
            {
                using var emergency = new EmergencyScreen();
                emergency.ShowDialog();
                Application.Exit();
                return;
            }

            if (flagMgmt.SkipBootAnim)
            {
                this.Opacity = 0;
                this.Show();
                RunBootChecks();
                this.BeginInvoke(new Action(() =>
                {
                    this.Hide();
                    new logonui().Show();
                }));
                return;
            }

            // Modo normal: doble buffer + ventana a pantalla completa
            this.DoubleBuffered = true;
            SetStyle(ControlStyles.UserPaint |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer, true);
            UpdateStyles();

            ConfigureWindow();
            StartBootAnimation();

            this.Show();
            Application.DoEvents();
        }

        // ── Ventana ──────────────────────────────────────────────────────

        private void ConfigureWindow()
        {
            var bounds = Screen.PrimaryScreen.Bounds;
            this.StartPosition = FormStartPosition.Manual;
            this.Location      = Point.Empty;
            this.Size          = bounds.Size;
            this.FormBorderStyle = FormBorderStyle.None;
            this.TopMost       = true;
            this.BackColor     = Color.Black;
            this.WindowState   = FormWindowState.Maximized;

            PositionLogo();
        }

        // ── Animación spinner ────────────────────────────────────────────

        private void StartBootAnimation()
        {
            _bootTimer = new Timer { Interval = 30 };
            _bootTimer.Tick += (_, __) =>
            {
                SpinnerLabel.Text = BootAnillas[_bootIndex];
                _bootIndex = (_bootIndex + 1) % BootAnillas.Length;
            };
            _bootTimer.Start();
            Console.WriteLine("[INFO] Boot animation started");
        }

        // ── Checks de arranque ───────────────────────────────────────────

        /// <summary>
        /// Ejecuta todos los pasos de verificación del arranque.
        /// Llamado tanto en modo normal (desde Load) como en SkipBootAnim.
        /// </summary>
        protected void RunBootChecks()
        {
            DisableExplorer();
            FileAndFolderCheck();
            LoadLanguage();

            try   { PasswordMigration.MigrateIfNeeded(); }
            catch (Exception ex)
            { Console.WriteLine($"[ERROR] Password migration: {ex.Message}"); }
        }

        private void DisableExplorer()
        {
            foreach (var p in Process.GetProcessesByName("explorer"))
                p.Kill(true);

            ConfigureExplorerAutoRestart();
        }

        private void ConfigureExplorerAutoRestart()
        {
            const string subkey    = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon";
            const string valueName = "AutoRestartShell";

            using var key = Registry.LocalMachine.OpenSubKey(subkey, writable: true);
            if (key == null)
            {
                Console.WriteLine("[WARN] No se pudo abrir la clave de registro (¿sin permisos de admin?)");
                return;
            }

            int current = Convert.ToInt32(key.GetValue(valueName, 1));
            if (current == 0)
            {
                Console.WriteLine("[INFO] AutoRestartShell ya es 0.");
                return;
            }

            key.SetValue(valueName, 0, RegistryValueKind.DWord);
            Console.WriteLine("[INFO] AutoRestartShell → 0. Reiniciando...");
            SetAutoRun();
            Process.Start("shutdown.exe", "/r /t 0");
        }

        private void SetAutoRun()
        {
            const string runKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
            using var key = Registry.CurrentUser.OpenSubKey(runKey, writable: true);
            key?.SetValue("deneOS", Application.ExecutablePath);
        }

        private void FileAndFolderCheck()
        {
            Console.WriteLine("[INFO] Verificando estructura de archivos...");
            try
            {
                bool hasFolders = Directory.Exists(@"C:\DENEOS") &&
                                  Directory.Exists(@"C:\DENEOS\sysconf");
                bool hasConfig  = File.Exists(@"C:\DENEOS\sysconf\config.ini");
                bool hasLang    = File.Exists(@"C:\DENEOS\sysconf\lang.ini") &&
                                  !string.IsNullOrWhiteSpace(
                                      File.ReadAllText(@"C:\DENEOS\sysconf\lang.ini"));

                if (hasFolders && hasConfig && hasLang)
                {
                    Console.WriteLine("[SUCCESS] Folder structure OK");
                    return;
                }

                // BootScreenVertical crea la estructura; BootScreen (horizontal) avisa y sale.
                OnMissingStructure(hasFolders, hasConfig, hasLang);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] FileAndFolderCheck: {ex.Message}");
            }
        }

        /// <summary>
        /// Comportamiento cuando faltan archivos críticos.
        /// BootScreen muestra error y cierra; BootScreenVertical los crea.
        /// </summary>
        protected virtual void OnMissingStructure(bool hasFolders, bool hasConfig, bool hasLang)
        {
            Console.WriteLine("[CRITICAL] Archivos críticos no encontrados.");
            MessageBox.Show(
                "Critical files are missing. Please fix the installation or contact support.",
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Process.Start(new ProcessStartInfo("explorer.exe") { UseShellExecute = true });
            Application.Exit();
        }

        private void LoadLanguage()
        {
            Console.WriteLine("[INFO] Cargando idioma...");
            try
            {
                if (flagMgmt.ShowUntranslatedStrings) { UN_ST = true; return; }

                string lang = flagMgmt.Language != ""
                    ? flagMgmt.Language
                    : TryGetSavedLanguage();

                Cargar(lang);
                Console.WriteLine($"[SUCCESS] Idioma cargado: {lang}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] LoadLanguage: {ex.Message}");
            }
        }

        private static string TryGetSavedLanguage()
        {
            try   { return dosu.UniversalConfiguration.GetLang(); }
            catch { return "en"; }
        }

        // ── Transición a logonui / OOBE ──────────────────────────────────

        /// <summary>
        /// Debe conectarse al evento Tick del TransitionTimer del Designer.
        /// </summary>
        protected void OnTransitionTimerTick(object sender, EventArgs e)
        {
            TransitionTimer.Stop();
            Console.WriteLine("[INFO] Boot timer completado, iniciando login...");

            try
            {
                this.Hide();
                var user = GetUser();
                bool validUser = !string.IsNullOrWhiteSpace(user.Username) &&
                                 !string.IsNullOrWhiteSpace(user.Password);

                this.BeginInvoke(new Action(() =>
                {
                    if (validUser)
                    {
                        Console.WriteLine($"[INFO] Usuario: {user.Username} → logonui");
                        new logonui().Show();
                    }
                    else
                    {
                        Console.WriteLine("[INFO] Sin usuario → OOBE");
                        new OOBE.PCWelcomeBG().ShowDialog();
                    }
                }));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] OnTransitionTimerTick: {ex.Message}");
                this.BeginInvoke(new Action(() => new OOBE.PCWelcomeBG().ShowDialog()));
            }
        }
    }
}