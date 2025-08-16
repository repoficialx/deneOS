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
        public BootScreen()
        {
            #region flags
            if (flagMgmt.ResetPrefs)
            {
                Properties.Settings.Default.Reset();
                Properties.Settings.Default.Save();
            }
            if (flagMgmt.EnableDebug)
            {
                Console.WriteLine("[DEBUG] Debug mode enabled");
            }
            if (flagMgmt.EnableRoot)
            {
                Console.WriteLine("[DEBUG] Root mode enabled");
            }
            if (flagMgmt.DisableLockScreen)
            {
                Console.WriteLine("[DEBUG] Lock screen disabled");
            }
            if (flagMgmt.MockBattery)
            {
                Console.WriteLine("[DEBUG] Mock battery enabled");
            }
            if (flagMgmt.ClassicMode)
            {
                Console.WriteLine("[DEBUG] Classic mode enabled");
            }
            if (flagMgmt.ForceUpdate)
            {
                Console.WriteLine("[DEBUG] Force update enabled");
            }
            if (flagMgmt.SafeMode)
            {
                Console.WriteLine("[DEBUG] Safe mode enabled");
            }
            if (flagMgmt.SafeModeWithNetwork)
            {
                Console.WriteLine("[DEBUG] Safe mode with network enabled");
            }
            if (flagMgmt.RecoverMode)
            {
                Console.WriteLine("[DEBUG] Recover mode enabled");
            }
            if (flagMgmt.LogSession)
            {
                Console.WriteLine("[DEBUG] Session logging enabled");
            }
            if (flagMgmt.BypassChecks)
            {
                Console.WriteLine("[DEBUG] Bypass checks enabled");
            }
            if (flagMgmt.ShowSysInfo)
            {
                bool IsW64() {
                    if (OperatingSystem.IsWindowsVersionAtLeast(10, 0, 22000))
                        return true;
                    else
                        return false;
                }
                Console.WriteLine("[DEBUG] System info display enabled");
                Console.WriteLine(
                    new string[]
                    {
                        $"Is 64-Bit? {Environment.Is64BitProcess}",
                        $"Is Windows x64? {Environment.Is64BitOperatingSystem}",
                        $"Is Windows 11? {IsW64()}",
                        $"OS Version: {Environment.OSVersion.VersionString}",
                        $"OS Platform: {Environment.OSVersion.Platform}",
                        $"Machine Name: {Environment.MachineName}",
                        $"User Domain Name: {Environment.UserDomainName}",
                        $"User Name: {Environment.UserName}",
                        $"Current Directory: {Environment.CurrentDirectory}",
                        $"System Directory: {Environment.SystemDirectory}",
                        $"System Page Size: {Environment.SystemPageSize}",
                        $"Processor Count: {Environment.ProcessorCount}",
                        $"CLR Version: {Environment.Version}",
                        $"Current Culture: {System.Globalization.CultureInfo.CurrentCulture.Name}",
                        $"Current UI Culture: {System.Globalization.CultureInfo.CurrentUICulture.Name}",
                        $"Current Directory: {Environment.CurrentDirectory}",
                        $"Current Process ID: {Process.GetCurrentProcess().Id}",
                        $"Current Process Name: {Process.GetCurrentProcess().ProcessName}",
                        $"Current Process Start Time: {Process.GetCurrentProcess().StartTime}",
                        $"Current Process Total Processor Time: {Process.GetCurrentProcess().TotalProcessorTime}",
                        $"Current Process Working Set: {Process.GetCurrentProcess().WorkingSet64}",
                        $"Current Process Peak Working Set: {Process.GetCurrentProcess().PeakWorkingSet64}",
                        $"Current Process Virtual Memory Size: {Process.GetCurrentProcess().VirtualMemorySize64}",
                        $"Current Process Peak Virtual Memory Size: {Process.GetCurrentProcess().PeakVirtualMemorySize64}",
                        $"Current Process Base Priority: {Process.GetCurrentProcess().BasePriority}",
                        // No hagas esto para el proceso actual
// $"Current Process Exit Code: {Process.GetCurrentProcess().ExitCode}",
// $"Current Process Exit Time: {Process.GetCurrentProcess().ExitTime}",

                        $"Current Process Handle Count: {Process.GetCurrentProcess().HandleCount}",
                        $"Current Process Main Module: {Process.GetCurrentProcess().MainModule.FileName}",
                        $"Current Process Main Window Title: {Process.GetCurrentProcess().MainWindowTitle}",
                        $"Current Process Main Window Handle: {Process.GetCurrentProcess().MainWindowHandle}",
                        $"Current Process Main Window Size: {(Process.GetCurrentProcess().MainWindowHandle == IntPtr.Zero ? "N/A" : Process.GetCurrentProcess().MainWindowHandle.ToString())}",
                        $"Current Process Main Window Style: {(Process.GetCurrentProcess().MainWindowHandle == IntPtr.Zero ? "N/A" : Process.GetCurrentProcess().MainWindowHandle.ToString())}",
                        $"Current Process Main Window Class: {(Process.GetCurrentProcess().MainWindowHandle == IntPtr.Zero ? "N/A" : Process.GetCurrentProcess().MainWindowHandle.ToString())}",
                        $"Current Process Main Window Rectangle: {(Process.GetCurrentProcess().MainWindowHandle == IntPtr.Zero ? "N/A" : Process.GetCurrentProcess().MainWindowHandle.ToString())}",
                        $"Current Process Main Window Location: {(Process.GetCurrentProcess().MainWindowHandle == IntPtr.Zero ? "N/A" : Process.GetCurrentProcess().MainWindowHandle.ToString())}",
                        $"Current Process Main Window Size: {(Process.GetCurrentProcess().MainWindowHandle == IntPtr.Zero ? "N/A" : Process.GetCurrentProcess().MainWindowHandle.ToString())}",
                        $"Current Process Main Window Style: {(Process.GetCurrentProcess().MainWindowHandle == IntPtr.Zero ? "N/A" : Process.GetCurrentProcess().MainWindowHandle.ToString())}",
                        $"Current Process Main Window Class: {(Process.GetCurrentProcess().MainWindowHandle == IntPtr.Zero ? "N/A" : Process.GetCurrentProcess().MainWindowHandle.ToString())}",
                        $"Current Process Main Window Rectangle: {(Process.GetCurrentProcess().MainWindowHandle == IntPtr.Zero ? "N/A" : Process.GetCurrentProcess().MainWindowHandle.ToString())}",
                        $"Current Process Main Window Location: {(Process.GetCurrentProcess().MainWindowHandle == IntPtr.Zero ? "N/A" : Process.GetCurrentProcess().MainWindowHandle.ToString())}",
                    }.Aggregate(
                        (current, next) => current + Environment.NewLine + next)
                    );
            }
            if (flagMgmt.NoShell)
            {
                Console.WriteLine("[DEBUG] No shell mode enabled");
            }
            if (flagMgmt.EmergencyUI)
            {
                Console.WriteLine("[DEBUG] Emergency UI enabled");
                EmergencyScreen emergencyScreen = new EmergencyScreen();
                emergencyScreen.ShowDialog();
                this.BeginInvoke(new Action(() => this.Close()));
            }
            if (flagMgmt.OfflineOnly)
            {
                Console.WriteLine("[DEBUG] Offline only mode enabled");
            }
            if (flagMgmt.Language != "")
            {
                Console.WriteLine($"[DEBUG] Language set to {flagMgmt.Language}");
            }
            if (flagMgmt.LaunchAppId != "")
            {
                Console.WriteLine($"[DEBUG] Launch app ID set to {flagMgmt.LaunchAppId}");
            }
            if (flagMgmt.Locale != "")
            {
                Console.WriteLine($"[DEBUG] Locale set to {flagMgmt.Locale}");
            }
            if (flagMgmt.SelectedTimeFormat != flagMgmt.TimeFormat.SystemDefault)
            {
                Console.WriteLine($"[DEBUG] Time format set to {flagMgmt.SelectedTimeFormat}");
            }
            if (flagMgmt.ShowUntranslatedStrings)
            {
                Console.WriteLine("[DEBUG] Showing untranslated strings");
            }
            if (flagMgmt.SkipBootAnim)
            {
                DisableExplorer();
                FilenFolderCheck();
                CargarIdioma();
                new logonui();
                return;
            }
#endregion
            // Configura el timer
            bootTimer = new Timer();
            bootTimer.Interval = 30; // Puedes ajustar la velocidad aquí
            bootTimer.Tick += BootTimer_Tick;
            bootTimer.Start();
            InitializeComponent();
        }
        void DisableExplorer()
        {
            Process cproc = new Process();
            cproc.StartInfo.FileName = "taskkill";
            cproc.StartInfo.Arguments = "/f /im explorer.exe";
            cproc.StartInfo.CreateNoWindow = true;
            cproc.Start();
            ConfigureExplorerAutoRestart();

        }

        private void ConfigureExplorerAutoRestart()
        {
            const string subkey = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon";
            const string valueName = "AutoRestartShell";

            // Abrir la clave del registro en modo de escritura
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(subkey, true))
            {
                if (key != null)
                {
                    // Leer el valor actual
                    object value = key.GetValue(valueName);
                    int? currentValue = value as int?;

                    // Si el valor es 1, cambiarlo a 0
                    if (currentValue.HasValue && currentValue.Value == 1)
                    {
                        key.SetValue(valueName, 0, RegistryValueKind.DWord);
                        Console.WriteLine("AutoRestartShell cambiado a 0 para prevenir el reinicio de explorer.exe.");
                        SetAutoRun();
                        RebootSystem();
                    }
                    else if (currentValue.HasValue && currentValue.Value == 0)
                    {
                        // El valor ya es 0, no hacer nada
                        Console.WriteLine("AutoRestartShell ya es 0. No se requiere ninguna acción.");
                    }
                    else
                    {
                        // El valor no existe o es de otro tipo, crearlo y establecerlo a 0
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
            //Comprobar que todos los archivos estén
            bool existeCarpetaDN = Directory.Exists("C:\\DENEOS");
            bool existeLauncher = true;
            bool existeLauncherCFG = File.Exists("C:\\DENEOS\\sysconf\\config.ini");
            bool existeHomeEdition = Directory.Exists("C:\\DENEOS\\sysconf\\");
            bool existecfgIdioma = File.Exists("C:\\DENEOS\\sysconf\\lang.ini");
            bool cfgIdiomaContenido = File.ReadAllText("C:\\DENEOS\\sysconf\\lang.ini") != String.Empty;
            bool cfgIdiomaNotNull = File.ReadAllText("C:\\DENEOS\\sysconf\\lang.ini") != null;

            bool carpetas = existeCarpetaDN && existeLauncher && existeHomeEdition;
            bool archivos = existeLauncherCFG && existecfgIdioma;
            bool nonempty = cfgIdiomaNotNull && cfgIdiomaContenido;

            if (carpetas && archivos && nonempty)
            {
                Console.WriteLine("[INFO] No files missing");
                Console.WriteLine($"[INFO] ~D\\ Folder status: {existeCarpetaDN}");
                Console.WriteLine($"[INFO] sysconf\\config.ini status: {existeLauncherCFG}");
                Console.WriteLine($"[INFO] DENEOS\\sysconf folder status: {existeHomeEdition}");
                Console.WriteLine($"[INFO] sysconf\\lang.ini status: {existecfgIdioma}");
                Console.WriteLine($"[INFO] lang.ini has any type of content? {cfgIdiomaContenido}");
                Console.WriteLine($"[INFO] lang.ini is not null (blank)? {cfgIdiomaNotNull}");
                Console.WriteLine($"[INFO] Do all the folders exist? {carpetas}");
                Console.WriteLine($"[INFO] Do all the files exist? {archivos}");
                Console.WriteLine($"[INFO] lang.ini isn't neither blank nor null? {nonempty}");
            }
            else
            {
                Console.WriteLine("[WARN] files missing");
                Console.WriteLine($"[INFO] ~D\\ Folder status: {existeCarpetaDN}");
                Console.WriteLine($"[INFO] sysconf\\config.ini status: {existeLauncherCFG}");
                Console.WriteLine($"[INFO] DENEOS\\sysconf folder status: {existeHomeEdition}");
                Console.WriteLine($"[INFO] sysconf\\lang.ini status: {existecfgIdioma}");
                Console.WriteLine($"[INFO] lang.ini has any type of content? {cfgIdiomaContenido}");
                Console.WriteLine($"[INFO] lang.ini is not null (blank)? {cfgIdiomaNotNull}");
                Console.WriteLine($"[INFO] Do all the folders exist? {carpetas}");
                Console.WriteLine($"[INFO] Do all the files exist? {archivos}");
                Console.WriteLine($"[INFO] lang.ini isn't neither blank nor null? {nonempty}");
            }
            
        }
        void CargarIdioma()
        {
            if (flagMgmt.ShowUntranslatedStrings)
            {
                UN_ST = true;
                return;
            }
            if (flagMgmt.Language != "")
            {
                Cargar(flagMgmt.Language);
                return;
            }
            Cargar(dosu.UniversalConfiguration.GetLang());
        }
        private void BootScreen_Load(object sender, EventArgs e)
        {
            DisableExplorer();
            FilenFolderCheck();
            CargarIdioma();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            this.Hide();
            // Comprobación de Usuario
            User user = GetUser();
            if (user.Password == "" 
                || user.Password == null 
                || user.Username == ""
                || user.Username == null
                || user.Equals(null))
            {
                // Si no tiene contraseña, iniciamos sesión directamente
                Console.WriteLine("[INFO] User has no user, starting logonui...");
                new OOBE.PCWelcomeBG().ShowDialog();
                return;
            }
            // Si tiene contraseña, mostramos la pantalla de inicio de sesión
            Console.WriteLine("[INFO] User has a password, showing logonui...");
            new logonui();
        }

        private void BootTimer_Tick(object sender, EventArgs e)
        {
            label1.Text = bootAnillas[bootIndex];
            bootIndex = (bootIndex + 1) % bootAnillas.Length;
        }
    }
}
