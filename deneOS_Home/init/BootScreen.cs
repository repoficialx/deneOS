using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Traductor;

namespace deneOS_Home.init
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
        private Timer bootTimer;
        public BootScreen()
        {
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
                Console.WriteLine("[DEBUG] System info display enabled");
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
            InitializeComponent();
            // Configura el timer
            bootTimer = new Timer();
            bootTimer.Interval = 30; // Puedes ajustar la velocidad aquí
            bootTimer.Tick += BootTimer_Tick;
            bootTimer.Start();
        }
        void DisableExplorer()
        {
            Process cproc = new Process();
            cproc.StartInfo.FileName = "taskkill";
            cproc.StartInfo.Arguments = "/f /im explorer.exe";
            cproc.StartInfo.CreateNoWindow = true;
            cproc.Start();
            //Process.Start("explorer.exe", "/nogui");
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
                Traductor.UN_ST = true;
                return;
            }
            if (flagMgmt.Language != "")
            {
                Cargar(flagMgmt.Language);
                return;
            }
            //Load language
            bool fileExists = File.Exists(@"C:\DENEOS\sysconf\lang.ini");
            int langLine = 1;
            string lang;
            if (fileExists)
            {
                try
                {
                    lang = File.ReadAllLines(@"C:\DENEOS\sysconf\lang.ini")[langLine];
                }
                catch
                {
                    MessageBox.Show("ERROR 0x4", "DENEOS HOME EDITION", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    lang = "en";
                    return;
                }
            }
            else
            {
                lang = "en";
            }
            Cargar(lang);
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
            new logonui();
            
        }

        private void BootTimer_Tick(object sender, EventArgs e)
        {
            label1.Text = bootAnillas[bootIndex];
            bootIndex = (bootIndex + 1) % bootAnillas.Length;
        }
    }
}
