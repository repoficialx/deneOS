using Microsoft.Win32;
using System.Diagnostics;
using System.Text.Json;
using dosu;

namespace deneOS_Launcher
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (File.Exists("C:\\DENEOS\\core\\deneOS.exe")) return;
            button1.Text = @"INSTALL►";
            button1.Click -= button1_Click!;
            button1.Click += Install_Click!;
        }

        async void Install_Click(object sender, EventArgs e)
        {
            SetupScreen setup = new SetupScreen();
            setup.Show();
            /*
            var response = MessageBox.Show(@"Install on Verbose mode?", @"deneOS Setup", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            var info = await GetUpdateInfo();
            if (info.Equals(null))
            {
                MessageBox.Show(@"Could not fetch update information.");
                return;
            }

            switch (response)
            {
                case DialogResult.Yes:
                    MessageBox.Show(@"Installing fonts...", @"deneOS Setup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    InstallFonts();
                    MessageBox.Show(@"Installing deneOS...");
                    await UpdateScreen(true, info.download!);
                    MessageBox.Show(@"Installing languages...");
                    InstallLanguages();
                    MessageBox.Show(@"Writing Configuration...");
                    WriteConfiguration();
                    MessageBox.Show(@"Downloading System Applications...");
                    DownloadSystemApps();
                    MessageBox.Show(@"Installation completed successfully!", @"deneOS Setup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;

                case DialogResult.No:
                    InstallFonts();
                    await UpdateScreen(false, info.download!);
                    InstallLanguages();
                    WriteConfiguration();
                    DownloadSystemApps();
                    MessageBox.Show(@"Installation completed successfully!", @"deneOS Setup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;

                case DialogResult.Cancel:
                    MessageBox.Show(@"Installation cancelled.", @"deneOS Setup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
            }

            Process.Start(Application.ExecutablePath);
            Application.Exit();*/
        }
/*
        async Task DownloadAll()
        {
            await btnCheckUpdates_Click(false);
            InstallFonts();
            InstallLanguages();
            WriteConfiguration();
            DownloadSystemApps();

            MessageBox.Show(@"Installation completed successfully!", @"deneOS Setup", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
*/
        void InstallFonts()
        {
            FontInstaller.InstallFonts();
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
            public string? latestVersion { get; set; }
            public string? download { get; set; }
            public string? changelog { get; set; }
        }/*
        private async Task btnCheckUpdates_Click(bool verbose)
        {
            var info = await GetUpdateInfo();
            if (info.Equals(null))
            {
                await UpdateScreen(true, info!.download!);
            }
            //return info;
        }
        */


        private string currentVersion;// = Utils.deneOSVersion.ShortVersion.GetVersion(Utils.deneOSVersion.ShortVersion.Formats.M_mx);
        
        
        async Task UpdateScreen(bool verbose, string downloadUrl)
        {
            if (!Directory.Exists(@"C:\DENEOS\core\"))
                Directory.CreateDirectory(@"C:\DENEOS\core\");

            currentVersion = File.Exists(@"C:\DENEOS\core\deneOS.exe") ? Utils.deneOSVersion.ShortVersion.GetVersion(Utils.deneOSVersion.ShortVersion.Formats.M_mx) : "0.0";
            string corePath = @"C:\DENEOS\core\";
            string exePath = Path.Combine(corePath, "deneOS.exe");
            string backupPath = Path.Combine(corePath, $"deneOS_{currentVersion}.bak");
            string tempPath = Path.Combine(corePath, "deneOS.new");

            if (verbose) MessageBox.Show(@"Closing deneOS...");

            // 1️⃣ Cerrar deneOS si está abierto
            try
            {
                var kill = Process.Start(new ProcessStartInfo
                {
                    FileName = "taskkill",
                    Arguments = "/f /im deneOS.exe",
                    UseShellExecute = true,
                    Verb = "runas",
                    CreateNoWindow = true
                });
                await kill!.WaitForExitAsync();
            }
            catch
            {
                // No estaba abierto → seguimos
            }

            // 2️⃣ Backup del binario actual
            if (File.Exists(exePath))
            {
                File.Copy(exePath, backupPath, overwrite: true);
            }

            if (verbose) MessageBox.Show(@"Downloading update...");

            // 3️⃣ Descargar nuevo binario a archivo temporal usando HttpClient
            using (HttpClient httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(downloadUrl, HttpCompletionOption.ResponseHeadersRead))
                {
                    response.EnsureSuccessStatusCode();
                    using (var fs = new FileStream(tempPath, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        await response.Content.CopyToAsync(fs);
                    }
                }
            }

            // 4️⃣ Reemplazo atómico
            File.Move(tempPath, exePath, overwrite: true);

            if (verbose) MessageBox.Show(@"deneOS updated successfully.");
        }


        void InstallLanguages()
        {
            string languagesPath = @"C:\DENEOS\lang\";
            if (!Directory.Exists(languagesPath))
            {
                Directory.CreateDirectory(languagesPath);
            }
            string[] languages =
            [
                "en.json",
                "es.json"
            ];

            using (HttpClient client = new HttpClient())
            {
                foreach (string language in languages)
                {
                    string languageUri = $"https://repoficialx.xyz/deneOS/api/{language}";
                    string languagePath = Path.Combine(languagesPath, language);
                    if (File.Exists(languagePath))
                    {
                        File.Delete(languagePath);
                    }
                    var data = client.GetByteArrayAsync(languageUri).GetAwaiter().GetResult();
                    File.WriteAllBytes(languagePath, data);
                    Console.WriteLine($@"Downloaded {language} to {languagePath}");
                }
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
            using RegistryKey key = Registry.CurrentUser.CreateSubKey(@"Software\deneOS\desktop");
            if (key.Equals(null)) return;
            // Establecer valor DWORD showIcons = 1
            key.SetValue("showIcons", 1, RegistryValueKind.DWord);

            // Establecer valor cadena wallpaper = ruta de la imagen
            key.SetValue("wallpaper", fixedInput, RegistryValueKind.String);
        }

        void DownloadSystemApps()
        {
            DownloadDeneFiles();
            DownloadDeneNavi();
            DownloadDeneNotes();
            DownloadDPKXT();
            DownloadInternal();
        }

        void DownloadDeneFiles()
        {
            const string denefilespath = @"C:\DENEOS\systemApps\deneFiles\";
            const string denefilehost = "https://repoficialx.xyz/deneosversions/systemApps/deneFiles/";
            string[] files =
            {
                "deneFiles.exe"
            };
            if (Directory.Exists(denefilespath))
            {
                Directory.Delete(denefilespath, true);
            }
            else
            {
                Directory.CreateDirectory(denefilespath);
                using (HttpClient client = new HttpClient())
                {
                    foreach (var a in files)
                    {
                        string fileUri = denefilehost + a;
                        string filePath = Path.Combine(denefilespath, a);
                        var data = client.GetByteArrayAsync(fileUri).GetAwaiter().GetResult();
                        File.WriteAllBytes(filePath, data);
                        Console.WriteLine($@"Downloaded {a} to {filePath}");
                    }
                }
            }
        }

        void DownloadDPKXT()
        {
            const string host = "https://repoficialx.xyz/deneosversions/systemApps/dpkxt/dpkxt.exe";
            const string path = @"C:\DENEOS\core\dpkxt.exe";
            if (File.Exists(path))
                File.Delete(path);

            using (HttpClient client = new HttpClient())
            {
                var data = client.GetByteArrayAsync(host).GetAwaiter().GetResult();
                File.WriteAllBytes(path, data);
                Console.WriteLine($@"Downloaded dpkxt.exe to {path}");
            }
        }

        void DownloadInternal()
        {
            DownloadsetConfig();
            DownloadaboutDialogs();
        }

        void DownloadsetConfig()
        {
            const string host = "https://repoficialx.xyz/deneosversions/internalApps/setConfig/setConfig.exe";
            const string path = @"C:\DENEOS\core\setConfig.exe";

            if (File.Exists(path))
                File.Delete(path);

            using (HttpClient client = new HttpClient())
            {
                var data = client.GetByteArrayAsync(host).GetAwaiter().GetResult();
                File.WriteAllBytes(path, data);
                Console.WriteLine($@"Downloaded setConfig.exe to {path}");
            }
        }

        void DownloadaboutDialogs()
        {
            const string host = "https://repoficialx.xyz/deneosversions/internalApps/aboutDialogs/aboutDialogs.exe";
            const string path = @"C:\DENEOS\core\aboutDialogs.exe";

            if (File.Exists(path))
                File.Delete(path);

            using (HttpClient client = new HttpClient())
            {
                var data = client.GetByteArrayAsync(host).GetAwaiter().GetResult();
                File.WriteAllBytes(path, data);
                Console.WriteLine($@"Downloaded aboutDialogs.exe to {path}");
            }
        }
        void DownloadDeneNavi()
        {
            const string denenavipath = @"C:\DENEOS\systemApps\deneNavi\";
            const string denenavihost = "https://repoficialx.xyz/deneosversions/systemApps/deneNavi/";
            string[] files = ["deneNavi.exe"];

            if (Directory.Exists(denenavipath))
            {
                Directory.Delete(denenavipath, true);
            }

            Directory.CreateDirectory(denenavipath);

            using (HttpClient client = new HttpClient())
            {
                foreach (var a in files)
                {
                    string fileUri = denenavihost + a;
                    string filePath = Path.Combine(denenavipath, a);

                    // Crear carpetas intermedias si no existen
                    string directory = Path.GetDirectoryName(filePath);
                    if (!string.IsNullOrEmpty(directory))
                        Directory.CreateDirectory(directory);

                    var data = client.GetByteArrayAsync(fileUri).GetAwaiter().GetResult();
                    File.WriteAllBytes(filePath, data);
                    Console.WriteLine($@"Downloaded {a} to {filePath}");
                }
            }
        }

        void DownloadDeneNotes()
        {
            const string denenotespath = @"C:\DENEOS\systemApps\deneNotes\";
            const string denenoteshost = "https://repoficialx.xyz/deneosversions/systemApps/deneNotes/";
            string[] files =
            {
                "deneNotes.exe",
                "deneNotes.exe.config",
                "deneNotes.pdb"
            };
            if (!Directory.Exists(denenotespath))
            {
                Directory.CreateDirectory(denenotespath);
                using (HttpClient client = new HttpClient())
                {
                    foreach (string b in files)
                    {
                        string fileUri = denenoteshost + b;
                        string filePath = Path.Combine(denenotespath, b);
                        if (File.Exists(filePath))
                        {
                            File.Delete(filePath);
                        }
                        var data = client.GetByteArrayAsync(fileUri).GetAwaiter().GetResult();
                        File.WriteAllBytes(filePath, data);
                        Console.WriteLine($@"Downloaded {b} to {filePath}");
                    }
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
            if (e.KeyCode != Keys.F10) return;
            System.Media.SystemSounds.Asterisk.Play();
            //System.Media.SoundPlayer sp = new();
            //sp.SoundLocation = "deneos_sound.wav";
            //sp.Play();

            if (button1.Text != @"INSTALL►")
            {
                button1.Text = @"INSTALL►";
                button1.Click -= button1_Click!;
                button1.Click += Install_Click!;
            }
            else
            {
                button1.Text = @"RUN►";
                button1.Click -= Install_Click!;
                button1.Click += button1_Click!;
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
