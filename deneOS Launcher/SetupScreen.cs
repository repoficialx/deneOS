#pragma warning disable CS8604 // Possible null reference argument.
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace deneOS_Launcher
{
    public class Dependency
{
    public string name { get; set; }
    public string download { get; set; }
    public string path { get; set; }
}

public class CoreInfo
{
    public string download { get; set; }
}

public class VersionInfo
{
    public string latestVersion { get; set; }
    public string changelog { get; set; }

    public CoreInfo core { get; set; }

    public List<Dependency> dependencies { get; set; }
}
    public partial class SetupScreen : Form
    {
        public SetupScreen()
        {
            InitializeComponent();
        }

        private async void SetupScreen_Load(object sender, EventArgs e)
        {
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            string time = DateTime.Now.ToString("HH:mm:ss");
            string zone = "UTC"+DateTime.Now.ToString("zzz");
            label1.Text = string.Format("""
                          deneOS Launcher :: Setup
                          
                          LOG # {0} {1} {2}
                          
                          
                          """, date, time, zone);

            label1.Text += "Checking if there are previous installations...\n";
            if (Directory.Exists(@"C:\DENEOS\"))
            {
                label1.Text += "[ERROR] Previous installation detected! Use Control Center to update\n";
                label1.Text += "Press any key to exit setup...\n";
                KeyPreview = true;
                this.KeyDown += (s, ev) => { Application.Exit(); };
                return;
            }
            label1.Text += "Creating folder structure...\n";
            Directory.CreateDirectory(@"C:\DENEOS\");
            Directory.CreateDirectory(@"C:\DENEOS\core\");
            Directory.CreateDirectory(@"C:\DENEOS\lang");
            Directory.CreateDirectory(@"C:\DENEOS\sysconf\");
            Directory.CreateDirectory(@"C:\DENEOS\systemApps\");
            Directory.CreateDirectory(@"C:\DENEOS\sysfonts\");
            Directory.CreateDirectory(@"C:\SOFTWARE\");
            Directory.CreateDirectory(@"C:\DNUSR\");
            Directory.CreateDirectory(@"C:\DNUSR\Documents\");
            Directory.CreateDirectory(@"C:\DNUSR\Downloads\");
            Directory.CreateDirectory(@"C:\DNUSR\Desktop\");

            Log("Downloading languages");
            string espUrl = "https://repoficialx.xyz/deneOS/api/es.json";
            string espPath = @"C:\DENEOS\lang\es.json";
            string engUrl = "https://repoficialx.xyz/deneOS/api/en.json";
            string engPath = @"C:\DENEOS\lang\en.json";
            HttpClient httpClient = new HttpClient();
            var espData = await httpClient.GetStringAsync(espUrl);
            await File.WriteAllTextAsync(espPath, espData);
            var engData = await httpClient.GetStringAsync(engUrl);
            await File.WriteAllTextAsync(engPath, engData);
            Log("Languages downloaded successfully.");

            string latestVersion;
            string versionsjsonurl = "https://repoficialx.xyz/deneOS/api/versions.json";
            string versions_json = await new HttpClient().GetStringAsync(versionsjsonurl);
            var versionInfo = System.Text.Json.JsonSerializer.Deserialize<VersionInfo>(versions_json);
            latestVersion = versionInfo.latestVersion;
            string url = versionInfo.core.download;
            string path = @"C:\DENEOS\core\deneOS.exe";

            Log("Ready to install deneOS " + latestVersion + ".");

            StartDownload();

            await DownloadFileAsync(url, path, percent =>
            {
                UpdateProgress(percent);
            });

Log("Ready to install dependencies.");

progressLineIndex = GetLastLineIndex();

int current = 0;
int total = versionInfo.dependencies.Count;

foreach (var dep in versionInfo.dependencies)
{
    current++;

    string fullPath = Path.Combine(
        @"C:\DENEOS",
        dep.path.Replace("/", "\\"));

    string directory =
        Path.GetDirectoryName(fullPath);

    if (!Directory.Exists(directory))

                    Directory.CreateDirectory(directory);

                await DownloadFileAsync(
        dep.download,
        fullPath,
        percent =>
        {
            UpdateProgress(
                percent,
                dep.name,
                true,
                current,
                total);
        });

    Log($"{dep.name} installed.");
}

            Log("Ready to install fonts.");
            string sysFontsBaseUrl = "https://repoficialx.xyz/fonts/";
            string segoeuivfUrl = sysFontsBaseUrl + "SegoeUI-VF.ttf";
            string segoeuivfPath = @"C:\DENEOS\sysfonts\SegoeUI-VF.ttf";
            string segoeflicUrl = sysFontsBaseUrl + "segoe-fluent-icons.ttf";
            string segoeflicPath = @"C:\DENEOS\sysfonts\segoe-fluent-icons.ttf";
            string segoeslbtUrl = sysFontsBaseUrl + "segoe_slboot.ttf";
            string segoeslbtPath = @"C:\DENEOS\sysfonts\segoe_slboot.ttf";
            string mtrliconsUrl = sysFontsBaseUrl + "Material Icons.ttf";
            string mtrliconsPath = @"C:\DENEOS\sysfonts\Material Icons.ttf";
            string flsysicnsUrl = sysFontsBaseUrl + "FluentSystemIcons-Regular.ttf";
            string flsysicnsPath = @"C:\DENEOS\sysfonts\FluentSystemIcons-Regular.ttf";

            progressLineIndex = GetLastLineIndex();

            await DownloadFileAsync(segoeuivfUrl, segoeuivfPath, percent =>
            {
                UpdateProgress(percent, "SegoeUI-VF.ttf", true, 1, 5);
            });
            await DownloadFileAsync(segoeflicUrl, segoeflicPath, percent =>
            {
                UpdateProgress(percent, "segoe-fluent-icons.ttf", true, 2, 5);
            });
            await DownloadFileAsync(segoeslbtUrl, segoeslbtPath, percent =>
            {
                UpdateProgress(percent, "segoe_slboot.ttf", true, 3, 5);
            });
            await DownloadFileAsync(mtrliconsUrl, mtrliconsPath, percent =>
            {
                UpdateProgress(percent, "Material Icons.ttf", true, 4, 5);
            });
            await DownloadFileAsync(flsysicnsUrl, flsysicnsPath, percent =>
            {
                UpdateProgress(percent, "FluentSystemIcons-Regular.ttf", true, 5, 5);
            });
            Log("Adding deneOS\\core\\ to PATH...");
            AddToPath();

            Log("deneOS setup completed successfully!");

            Log("Restarting in 5...");
            progressLineIndex = GetLastLineIndex();
            for (int seconds = 4; seconds >= 1; seconds--)
            {
                await Task.Delay(1000);
                ChangeLine(progressLineIndex, $"Restarting in {seconds}...");
            }
            await Task.Delay(1000);
            if (Debugger.IsAttached)
            {
                Log("[DEBUG] Shutdown skipped in debug mode.");
                return;
            }
            Process.Start(fileName:"setConfig", arguments:"sdoptions rn");
        }
        int progressLineIndex = -1;
                string[] GetCleanLines()
        {
            var content = label1.Text.Replace("\r\n", "\n").TrimEnd('\n');
            return content.Split('\n');
        }

        int GetLastLineIndex()
        {
            var lines = GetCleanLines();
            return Math.Max(0, lines.Length - 1);
        }

        void StartDownload()
        {
            Log("Downloading deneOS... 0%");
            progressLineIndex = GetLastLineIndex();
        }
        void AddToPath(string newPath = @"C:\DENEOS\core\")
        {
            try
            {
                // Get current system PATH
                string currentPath = Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.Machine);

                if (string.IsNullOrWhiteSpace(currentPath))
                {
                    currentPath = string.Empty;
                }

                // Check if already in PATH (case-insensitive)
                var paths = currentPath.Split(';', StringSplitOptions.RemoveEmptyEntries);
                if (paths.Any(p => string.Equals(p.Trim(), newPath, StringComparison.OrdinalIgnoreCase)))
                {
                    Console.WriteLine("Path already exists in system PATH.");
                    return;
                }

                // Append new path
                string updatedPath = currentPath.TrimEnd(';') + ";" + newPath;

                // Update system PATH (requires admin rights)
                Environment.SetEnvironmentVariable("PATH", updatedPath, EnvironmentVariableTarget.Machine);

                Console.WriteLine("System PATH updated successfully.");
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Error: Administrator privileges are required to modify the system PATH.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
        void ChangeLine(int lineIndex, string newText)
        {
            var lines = GetCleanLines();
            if (lineIndex < 0 || lineIndex >= lines.Length)
                return;
            lines[lineIndex] = newText;
            label1.Text = string.Join("\r\n", lines) + "\r\n";
            label1.Refresh();
        }
        void UpdateProgress(int percent, string product = "deneOS", bool staged=false, int cstage=0, int stages=0)
        {
            var lines = GetCleanLines();

            if (progressLineIndex < 0 || progressLineIndex >= lines.Length)
                return;

            if (staged)
                lines[progressLineIndex] = $"Downloading {product}... Stage {cstage}/{stages} {percent}%";
            else
                lines[progressLineIndex] = $"Downloading {product}... {percent}%";

            label1.Text = string.Join("\r\n", lines) + "\r\n";
            label1.Refresh();
        }


        void Log(string text)
        {
            label1.Text += text + "\r\n";
            label1.Refresh();
            Application.DoEvents();
        }
        private async Task DownloadFileAsync(string url, string path, Action<int> onProgress)
{
    using HttpClient client = new HttpClient();
    // Aumentar timeout para descargas largas
    client.Timeout = TimeSpan.FromMinutes(10);
    
    using HttpResponseMessage response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
    response.EnsureSuccessStatusCode();
    
    // Usar Content-Length si está disponible, sino determinar por lectura
    long total = response.Content.Headers.ContentLength ?? -1;
    
    using Stream stream = await response.Content.ReadAsStreamAsync();
    using FileStream file = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
    
    byte[] buffer = new byte[8192];
    long read = 0L;
    int bytes;
    
    while ((bytes = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
    {
        await file.WriteAsync(buffer, 0, bytes);
        read += bytes;
        
        // Solo calcular porcentaje si Content-Length es válido
        if (total > 0)
        {
            int percent = (int)(read * 100 / total);
            onProgress(percent);
        }
    }
    
    // Notificar 100% cuando se complete
    onProgress(100);

    }
}

}