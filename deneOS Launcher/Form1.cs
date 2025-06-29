using Microsoft.VisualBasic.Logging;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace deneOS_Launcher
{
    public partial class Form1 : Form
    {
        bool havePro;
        bool proId;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            #region DON'T TOUCH THIS CODE!
            getRgKey();
            if (havePro)
            {
                if (!proId)
                {
                    havePro = false;
                    MessageBox.Show("ANTI-PIRACY ALERT! - PLEASE BUY deneOS PRO ON iNS WEBPAGE!");
                    //Registry.SetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\iNS\\deneOS", "deneOSProEnabled", "n");
                    Directory.Delete(@"C:\DENEOS\sysconf\cfg", true);
                    Environment.Exit(-3278947);
                }
                else
                {
                    loadPro();
                }
            }
            else
            {
                //MessageBox.Show("Are you a developer? Try deneOS Pro! 32,5$ or 28€");
                Size = new Size(401, 185);
                MaximumSize = new Size(401, 185);
            }
            #endregion
            if (!File.Exists("C:\\DENEOS\\core\\deneOS.exe"))
            {
                button1.Text = "INSTALL►";
                button1.Click -= button1_Click!;
                button1.Click += Install_Click!;
            }
        }
        #region DON'T TOUCH THIS CODE!
        void getRgKey()
        {
            bool proEnabled2 = false;
            bool proKeyEnabled = false;
            bool cfg = File.Exists(@"C:\DENEOS\sisconf\config.ini");
            if (cfg)
            {
                var cfgInfo = File.ReadAllLines(@"C:\DENEOS\sysconf\config.ini");
                if (cfgInfo[1].Contains("deneOSProEnabled = true"))
                {
                    proEnabled2 = true;
                }
                if (cfgInfo.Length > 2 && cfgInfo[2].Contains("deneOSProKey = {7843NF-DRF764-VD9786-67W4N3}"))
                {
                    proKeyEnabled = true;
                }
                if (proEnabled2 && proKeyEnabled)
                {

                }
            }
            havePro = proEnabled2;
            proId = proKeyEnabled;
        }
        void loadPro()
        {
            Size = new Size(401, 338);
            MaximumSize = new Size(401, 338);
        }
        #endregion

        async void Install_Click(object sender, EventArgs e)
        {
            var response = MessageBox.Show("Install on Verbose mode?", "deneOS Setup", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            var info = await GetUpdateInfo();
            if (info == null)
            {
                MessageBox.Show("Could not fetch update information.");
                return;
            }

            switch (response)
            {
                case DialogResult.Yes:
                    MessageBox.Show("Installing fonts...", "deneOS Setup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    InstallFonts();
                    MessageBox.Show("Installing deneOS...");
                    await UpdateScreen(true, info.download);
                    MessageBox.Show("Installing languages...");
                    InstallLanguages();
                    MessageBox.Show("Writing Configuration...");
                    WriteConfiguration();
                    MessageBox.Show("Downloading System Applications...");
                    DownloadSystemApps();
                    MessageBox.Show("Installation completed successfully!", "deneOS Setup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;

                case DialogResult.No:
                    InstallFonts();
                    await UpdateScreen(false, info.download);
                    InstallLanguages();
                    WriteConfiguration();
                    DownloadSystemApps();
                    MessageBox.Show("Installation completed successfully!", "deneOS Setup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;

                case DialogResult.Cancel:
                    MessageBox.Show("Installation cancelled.", "deneOS Setup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
            }

            Process.Start(Application.ExecutablePath);
            Application.Exit();

        }

        async Task DownloadAll()
        {
            await btnCheckUpdates_Click(false);
            InstallFonts();
            InstallLanguages();
            WriteConfiguration();
            DownloadSystemApps();

            MessageBox.Show("Installation completed successfully!", "deneOS Setup", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        void InstallFonts()
        {
            WebClient client = new WebClient();
            // Descargar Segoe UI Fluent Icons

            string baseUri = "https://repoficialx.xyz/fonts/";
            string materialIconsUri = baseUri + "Material Icons.ttf";
            string segoeUI_VFUri = baseUri + "SegoeUI-VF.ttf";
            string segoeUIFluentIconsUri = baseUri + "segoe-fluent-icons.ttf";
            string segoe_slbootUri = baseUri + "segoe_slboot.ttf";
            string FluentSystemIcons_RegularUri = baseUri + "FluentSystemIcons-Regular.ttf";

            string fontsPath = @"C:\DENEOS\sysfonts\";
            if (!Directory.Exists(fontsPath))
            {
                Directory.CreateDirectory(fontsPath);
            }
            string materialIconsPath = Path.Combine(fontsPath, "Material Icons.ttf");
            string segoeUI_VFPath = Path.Combine(fontsPath, "SegoeUI-VF.ttf");
            string segoeUIFluentIconsPath = Path.Combine(fontsPath, "segoe-fluent-icons.ttf");
            string segoe_slbootPath = Path.Combine(fontsPath, "segoe_slboot.ttf");
            string FluentSystemIcons_RegularPath = Path.Combine(fontsPath, "FluentSystemIcons-Regular.ttf");

            if (File.Exists(materialIconsPath))
            {
                File.Delete(materialIconsPath);
            }
            if (File.Exists(segoeUI_VFPath))
            {
                File.Delete(segoeUI_VFPath);
            }
            if (File.Exists(segoeUIFluentIconsPath))
            {
                File.Delete(segoeUIFluentIconsPath);
            }
            if (File.Exists(segoe_slbootPath))
            {
                File.Delete(segoe_slbootPath);
            }
            if (File.Exists(FluentSystemIcons_RegularPath))
            {
                File.Delete(FluentSystemIcons_RegularPath);
            }

            client.DownloadFile(segoeUI_VFUri, segoeUI_VFPath);
            Console.WriteLine($"Downloaded Segoe UI VF to {segoeUI_VFPath}");
            client.DownloadFile(materialIconsUri, materialIconsPath);
            Console.WriteLine($"Downloaded Material Icons to {materialIconsPath}");
            client.DownloadFile(segoeUIFluentIconsUri, segoeUIFluentIconsPath);
            Console.WriteLine($"Downloaded Segoe UI Fluent Icons to {segoeUIFluentIconsPath}");
            client.DownloadFile(segoe_slbootUri, segoe_slbootPath);
            Console.WriteLine($"Downloaded Segoe SL Boot to {segoe_slbootPath}");
            client.DownloadFile(FluentSystemIcons_RegularUri, FluentSystemIcons_RegularPath);
            Console.WriteLine($"Downloaded Fluent System Icons Regular to {FluentSystemIcons_RegularPath}");
        }
        private async Task<UpdateInfo> GetUpdateInfo()
        {
            using HttpClient client = new HttpClient();
            try
            {
                string json = await client.GetStringAsync("https://repoficialx.xyz/deneOS/api/versions.json");
                return JsonSerializer.Deserialize<UpdateInfo>(json)!;
            }
            catch
            {
                return null!;
            }
        }

        private class UpdateInfo
        {
            public string latestVersion { get; set; }
            public string download { get; set; }
            public string changelog { get; set; }
        }
        private string currentVersion = "0.0";
        private string latestVersion = "0.0";
        private string changelog = "";
        private string downloadUrl = "";
        private async Task btnCheckUpdates_Click(bool verbose)
        {
            var info = await GetUpdateInfo();
            if (info != null)
            {
                await UpdateScreen(true, info.download);
            }
            //return info;
        }

        async Task UpdateScreen(bool verbose, string downloadUrl)
        {
            Console.WriteLine("Deleting actual system files..." + Environment.NewLine);
            if (verbose) MessageBox.Show("Deleting actual system files...");

            ProcessStartInfo processStartInfo = new ProcessStartInfo
            {
                FileName = "taskkill",
                Arguments = "/f /im",
                UseShellExecute = true,
                Verb = "runas",
                CreateNoWindow = true
            };
            string[] execs = { "deneOS_Home.exe", "deneOS.exe" };
            foreach (var a in execs)
            {
                processStartInfo.Arguments += " " + a;
                Process process = Process.Start(processStartInfo);
                await process!.WaitForExitAsync();
            }

            if (Directory.Exists(@"C:\DENEOS\core"))
            {
                if (!File.Exists(@$"C:\DENEOS\core{currentVersion}_backup.bck"))
                {
                    ZipFile.CreateFromDirectory(@"C:\DENEOS\core\", @$"C:\DENEOS\core{currentVersion}_backup.bck");
                }
                else
                {
                    File.Delete(@$"C:\DENEOS\core{currentVersion}_backup.bck");
                    ZipFile.CreateFromDirectory(@"C:\DENEOS\core\", @$"C:\DENEOS\core{currentVersion}_backup.bck");
                }
                Directory.Delete(@"C:\DENEOS\core\", true);
            }
            if (Directory.Exists(@"C:\DENEOS\lang"))
            {
                Directory.Delete(@"C:\DENEOS\lang\", true);
            }

            Console.WriteLine("Starting download...");
            if (verbose) MessageBox.Show("Starting download...");

            WebClient client1 = new();
            Uri h = new Uri(downloadUrl);
            await client1.DownloadFileTaskAsync(h, @"C:\DENEOS\newcore.zip");

            Console.WriteLine("Decompressing new system files...");
            if (verbose) MessageBox.Show("Decompressing new system files...");

            Directory.CreateDirectory(@"C:\DENEOS\lang\");
            ZipFile.ExtractToDirectory(@"C:\DENEOS\newcore.zip", @"C:\DENEOS\core\");

            Console.WriteLine("Patching...");
            if (verbose) MessageBox.Show("Patching...");

            File.Delete(@"C:\DENEOS\newcore.zip");
        }




        void InstallLanguages()
        {
            string languagesPath = @"C:\DENEOS\lang\";
            if (!Directory.Exists(languagesPath))
            {
                Directory.CreateDirectory(languagesPath);
            }
            string[] languages = new string[]
            {
                "en.json",
                "es.json"
            };
            WebClient client = new();
            foreach (string language in languages)
            {
                string languageUri = $"https://repoficialx.xyz/deneOS/api/{language}";
                string languagePath = Path.Combine(languagesPath, language);
                if (File.Exists(languagePath))
                {
                    File.Delete(languagePath);
                }
                client.DownloadFile(languageUri, languagePath);
                Console.WriteLine($"Downloaded {language} to {languagePath}");
            }
        }

        void WriteConfiguration()
        {
            Directory.CreateDirectory(@"C:\DENEOS\");
            Directory.CreateDirectory(@"C:\DENEOS\sysconf\");
            Directory.CreateDirectory(@"C:\DENEOS\desktop\");

            string configPath = @"C:\DENEOS\sysconf\config.ini";
            if (File.Exists(configPath))
            {
                File.Delete(configPath);
            }

            string langConfigPath = @"C:\DENEOS\sysconf\lang.ini";
            if (!File.Exists(langConfigPath))
            {
                void Lang2LangCode(SelENorES.Language lang, out string lc)
                {
                    lc = "en";
                    switch (lang)
                    {
                        case SelENorES.Language.English:
                            lc = "en";
                            break;
                        case SelENorES.Language.Spanish:
                            lc = "es";
                            break;
                    }
                }
                SelENorES selENorES = new SelENorES();
                selENorES.ShowDialog();
                SelENorES.Language lang_ = SelENorES.SelectedLanguage;
                Lang2LangCode(lang_, out string langCode);

                using (StreamWriter sw = new StreamWriter(langConfigPath))
                {
                    sw.WriteLine("[LangConfig]");
                    sw.WriteLine(langCode);
                }
            }

            // Registro
            string input = InputMessageBox.Show("Wallpaper location: (leave empty for default)", "deneOS Installer");
            string fixedInput = string.IsNullOrEmpty(input) ? @"c:\windows\web\4k\Wallpaper\windows\img0_1920x1200.jpg" : input;
            using (RegistryKey key = Registry.CurrentUser.CreateSubKey(@"Software\deneOS\desktop"))
            {
                if (key != null)
                {
                    // Establecer valor DWORD showIcons = 1
                    key.SetValue("showIcons", 1, RegistryValueKind.DWord);

                    // Establecer valor cadena wallpaper = ruta de la imagen
                    key.SetValue("wallpaper", fixedInput, RegistryValueKind.String);
                }
            }
        }

        void DownloadSystemApps()
        {
            DownloadDeneFiles();
            DownloadDeneNavi();
            DownloadDeneNotes();
        }

        void DownloadDeneFiles()
        {
            const string denefilespath = @"C:\DENEOS\systemApps\deneFiles\";
            const string denefilehost = "https://repoficialx.xyz/deneosversions/systemApps/deneFiles/";
            string[] files =
            {
                "deneFiles.exe",
                "deneFiles.exe.config",
                "deneFiles.pdb"
            };
            if (Directory.Exists(denefilespath))
            {
                Directory.Delete(denefilespath, true);
            }
            else
            {
                Directory.CreateDirectory(denefilespath);
                foreach (var a in files)
                {
                    WebClient client = new WebClient();
                    string fileUri = denefilehost + a;
                    string filePath = Path.Combine(denefilespath, a);
                    client.DownloadFile(fileUri, filePath);
                    Console.WriteLine($"Downloaded {a} to {filePath}");
                }
            }
        }

        void DownloadDeneNavi()
        {
            const string denenavipath = @"C:\DENEOS\systemApps\deneNavi\";
            const string denenavihost = "https://repoficialx.xyz/deneosversions/systemApps/deneNavi/";
            string[] files =
            {
        "Internet Explorer 11.exe",
        "Internet Explorer 11.exe.config",
        "Internet Explorer 11.pdb",
        "Microsoft.Web.WebView2.Core.dll",
        "Microsoft.Web.WebView2.Core.xml",
        "Microsoft.Web.WebView2.WinForms.dll",
        "Microsoft.Web.WebView2.WinForms.xml",
        "Microsoft.Web.WebView2.Wpf.dll",
        "Microsoft.Web.WebView2.Wpf.xml",
        "runtimes/win-arm64/native/WebView2Loader.dll",
        "runtimes/win-x64/native/WebView2Loader.dll",
        "runtimes/win-x86/native/WebView2Loader.dll",
    };

            if (Directory.Exists(denenavipath))
            {
                Directory.Delete(denenavipath, true);
            }

            Directory.CreateDirectory(denenavipath);

            foreach (var a in files)
            {
                WebClient client = new WebClient();
                string fileUri = denenavihost + a;
                string filePath = Path.Combine(denenavipath, a);

                // Crear carpetas intermedias si no existen
                string directory = Path.GetDirectoryName(filePath);
                if (!string.IsNullOrEmpty(directory))
                    Directory.CreateDirectory(directory);

                client.DownloadFile(fileUri, filePath);
                Console.WriteLine($"Downloaded {a} to {filePath}");
            }
        }

        void DownloadDeneNotes()
        {
            const string denenotespath = @"C:\DENEOS\systemApps\deneNotes\";
            const string denenoteshost = "https://repoficialx.xyz/deneosversions/systemApps/deneNotes/";
            Array files = new string[]
            {
                "deneNotes.exe",
                "deneNotes.exe.config",
                "deneNotes.pdb"
            };
            if (!Directory.Exists(denenotespath))
            {
                Directory.CreateDirectory(denenotespath);
                foreach (string b in files)
                {
                    var client = new WebClient();
                    string fileUri = denenoteshost + b;
                    string filePath = Path.Combine(denenotespath, b);
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                    client.DownloadFile(fileUri, filePath);
                    Console.WriteLine($"Downloaded {b} to {filePath}");

                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Process dnh = new Process();
            dnh.StartInfo.FileName = "c:\\DENEOS\\core\\deneOS.exe";
            dnh.StartInfo.Verb = "runas";
            dnh.StartInfo.UseShellExecute = true;
            dnh.Start();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
            {
                System.Media.SystemSounds.Beep.Play();
                System.Media.SoundPlayer sp = new();
                // futuro: archivo .wav de sonido debug
                //sp.Play();

                if (button1.Text != "INSTALL►")
                {
                    button1.Text = "INSTALL►";
                    button1.Click -= button1_Click!;
                    button1.Click += Install_Click!;
                }
                else
                {
                    button1.Text = "RUN►";
                    button1.Click -= Install_Click!;
                    button1.Click += button1_Click!;
                }
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
        }
    }
}
