using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

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
            if (!File.Exists("C:\\DENEOS\\core\\deneOS_Home.exe"))
            {
                button1.Text = "INSTALL►";
                button1.Click -= button1_Click;
                button1.Click += Install_Click;
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

        void Install_Click(object sender, EventArgs e)
        {
            Process dnh = new Process();
            var response = MessageBox.Show("Install on Verbose mode?", "deneOS Setup", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            switch (response)
            {
                case DialogResult.Yes:
                    MessageBox.Show("Installing fonts...", "deneOS Setup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    InstallFonts();
                    MessageBox.Show("Installing libraries...");
                    InstallLibraries();
                    MessageBox.Show("Installing languages...");
                    InstallLanguages();
                    MessageBox.Show("Installing executable...");
                    InstallExecutable();
                    MessageBox.Show("Writing Configuration...");
                    WriteConfiguration();
                    MessageBox.Show("Downloading System Applications...");
                    DownloadSystemApps();
                    MessageBox.Show("Installation completed successfully!", "deneOS Setup", MessageBoxButtons.OK, MessageBoxIcon.Information);;

                    break;
                case DialogResult.Cancel:
                    MessageBox.Show("Installation cancelled.", "deneOS Setup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return; 
                case DialogResult.No:
                    DownloadAll();

                    break;
            }
            Process.Start(Application.ExecutablePath);
            Application.Exit();
        }

        void DownloadAll()
        {
            InstallExecutable();
            InstallFonts();
            InstallLibraries();
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

        void InstallLibraries()
        {
            string librariesPath = @"C:\DENEOS\core\";
            if (!Directory.Exists(librariesPath))
            {
                Directory.CreateDirectory(librariesPath);
            }
            string[] libraries = new string[]
            {
                "ColorThief.Desktop.v46.dll",
                "ColorThief.Desktop.v46.pdb",
                "ColorThief.Desktop.v46.xml",
                "deneOS_Home.pdb",
                "deneOS_Home.exe.config",
                "Microsoft.Bcl.AsyncInterfaces.dll",
                "Microsoft.Win32.Registry.dll",
                "Microsoft.Win32.Registry.xml",
                "NAudio.dll",
                "NAudio.Asio.dll",
                "NAudio.Asio.xml",
                "NAudio.Core.dll",
                "NAudio.Core.xml",
                "NAudio.Midi.dll",
                "NAudio.Midi.xml",
                "NAudio.Wasapi.dll",
                "NAudio.Wasapi.xml",
                "NAudio.WinForms.dll",
                "NAudio.WinForms.xml",
                "NAudio.WinMM.dll",
                "NAudio.WinMM.xml",
                "NAudio.xml",
                "System.Buffers.dll",
                "System.Memory.dll",
                "System.Runtime.CompilerServices.Unsafe.dll",
                "System.Numerics.Vectors.dll",
                "System.Threading.Tasks.Extensions.dll",
                "System.ValueTuple.dll",
                "System.Text.Json.dll",
                "System.Text.Encodings.Web.dll",
                "System.Security.Principal.Windows.dll",
                "System.Security.Principal.Windows.xml",
                "System.Security.AccessControl.dll",
                "System.Security.AccessControl.xml",
            };
            WebClient client = new WebClient();
            foreach (string library in libraries)
            {
                string libraryUri = $"https://repoficialx.xyz/deneosversions/v09a/{library}";
                string libraryPath = Path.Combine(librariesPath, library);
                if (File.Exists(libraryPath))
                {
                    File.Delete(libraryPath);
                }
                client.DownloadFile(libraryUri, libraryPath);
                Console.WriteLine($"Downloaded {library} to {libraryPath}");
            }
        }

        void InstallExecutable()
        {
            if (!Directory.Exists("C:\\DENEOS\\core")) Directory.CreateDirectory("C:\\DENEOS\\core");
            string executablePath = @"C:\DENEOS\core\deneOS_Home.exe";
            if (File.Exists(executablePath))
            {
                File.Delete(executablePath);
            }
            WebClient client = new WebClient();
            string executableUri = 
                "https://repoficialx.xyz/deneosversions/v09a/deneOS_Home.exe";
            client.DownloadFile(executableUri, executablePath);
            Console.WriteLine($"Downloaded deneOS_Home.exe to {executablePath}");
        }

        void InstallLanguages()
        {
            string languagesPath = @"C:\DENEOS\core\lang\";
            if (!Directory.Exists(languagesPath))
            {
                Directory.CreateDirectory(languagesPath);
            }
            string[] languages = new string[]
            {
                "en.json",
                "es.json"
            };
            WebClient client = new WebClient();
            foreach (string language in languages)
            {
                string languageUri = $"https://repoficialx.xyz/deneosversions/v09a/lang/{language}";
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
            dnh.StartInfo.FileName = "c:\\DENEOS\\core\\deneOS_Home.exe";
            dnh.StartInfo.Verb = "runas";
            dnh.StartInfo.UseShellExecute = true;
            dnh.Start();
        }
    }
}
