using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.Media.Protection.PlayReady;
using System.IO;
using System.Net.Http;
using System.Security.Cryptography;

namespace controlcenter
{
    public partial class UpdateScreen : Form
    {
        private string currentVersion = "0.0";
        private string latestVersion = "0.0";
        private string changelog = "";
        private string downloadUrl = "";
        private Boolean forcedUrl = false;
        public UpdateScreen(string? URL = null)
        {
            InitializeComponent();
            if (URL != null)
            {
                forcedUrl = true;
                downloadUrl = URL;
            }
            var x = FileVersionInfo.GetVersionInfo(@"C:\DENEOS\core\deneOS.exe");
            // 0.4b -> 0 . 4 . 1
            int[] preVersion = { x.FileMajorPart, x.FileMinorPart, x.FileBuildPart };
            string tag = preVersion[2] == 0 ? "" : (preVersion[2] == 1 ? "b" : "a");
            currentVersion = $"{preVersion[0]}.{preVersion[1]}{tag}";
            //lblCurrentVersion.Text = (string)T("actualVersion") + ": " + currentVersion;
        }

        private void UpdateScreen_FormClosing(
            object sender,
            FormClosingEventArgs e)
        {
            if (!allowClose)
                e.Cancel = true;
        }

        private async void UpdateScreen_Load(object sender, EventArgs e)
        {
            log.Text = $"LOG # {DateTime.Now:yyyy-MM-dd} # {DateTime.Now:HH:mm}\r\n";

            try
            {
                log.Text += "Getting information about update...\r\n";

                string json = await Http.GetStringAsync(
                    "https://repoficialx.xyz/deneOS/api/versions.json");

                var info = JsonSerializer.Deserialize<VersionInfo>(json)
                    ?? throw new InvalidDataException("Invalid versions.json.");

                if (string.IsNullOrWhiteSpace(info.core.download))
                    throw new InvalidDataException("Core download URL is missing.");

                if (info.dependencies == null)
                    throw new InvalidDataException("Dependencies list is missing.");

                Directory.CreateDirectory(StagePath);
                Directory.CreateDirectory(CorePath);

                var filesToReplace = new List<(string Staged, string Target)>();

                // 1. Download the new core executable directly.
                string stagedCore = Path.Combine(StagePath, "deneOS.exe");

                string coreUrl = forcedUrl ? downloadUrl : info.core.download;

                log.Text += $"Preparing deneOS {info.latestVersion}...\r\n";
                await DownloadFileAsync(coreUrl, stagedCore);

                string currentCore = Path.Combine(CorePath, "deneOS.exe");

                if (!File.Exists(currentCore) ||
                    !await FilesMatchAsync(currentCore, stagedCore))
                {
                    filesToReplace.Add((stagedCore, currentCore));
                    log.Text += "Core update required.\r\n";
                }
                else
                {
                    log.Text += "Core is already up to date.\r\n";
                    File.Delete(stagedCore);
                }

                // 2. Download and compare each server dependency.
                foreach (var dep in info.dependencies)
                {
                    if (string.IsNullOrWhiteSpace(dep.name) ||
                        string.IsNullOrWhiteSpace(dep.download) ||
                        string.IsNullOrWhiteSpace(dep.path))
                    {
                        throw new InvalidDataException(
                            "A dependency contains missing fields.");
                    }

                    string target = Path.GetFullPath(
                        Path.Combine(Root, dep.path.Replace('/', '\\')));

                    // Prevent paths from escaping C:\DENEOS.
                    if (!target.StartsWith(
                        Root + "\\",
                        StringComparison.OrdinalIgnoreCase))
                    {
                        throw new InvalidDataException(
                            $"Invalid dependency path: {dep.path}");
                    }

                    string staged = Path.Combine(
                        StagePath,
                        Guid.NewGuid().ToString("N") + ".exe");

                    log.Text += $"Checking {dep.name}...\r\n";

                    await DownloadFileAsync(dep.download, staged);

                    if (!File.Exists(target) ||
                        !await FilesMatchAsync(target, staged))
                    {
                        filesToReplace.Add((staged, target));
                        log.Text += $"{dep.name}: update required.\r\n";
                    }
                    else
                    {
                        File.Delete(staged);
                        log.Text += $"{dep.name}: already up to date.\r\n";
                    }
                }

                // 3. Stop system processes before replacing their files.
                log.Text += "Preparing to apply updates...\r\n";

                foreach (string processName in new[]
                {
            "deneOS",
            "deneOS.Watchdog"
        })
                {
                    foreach (var process in Process.GetProcessesByName(processName))
                    {
                        try
                        {
                            process.Kill();
                            await process.WaitForExitAsync();
                        }
                        finally
                        {
                            process.Dispose();
                        }
                    }
                }

                // 4. Replace changed files, keeping rollback copies.
                var backups = new List<(string Backup, string Target)>();

                try
                {
                    foreach (var file in filesToReplace)
                    {
                        Directory.CreateDirectory(
                            Path.GetDirectoryName(file.Target)!);

                        if (File.Exists(file.Target))
                        {
                            string backup = file.Target + ".update-backup";

                            File.Copy(file.Target, backup, true);
                            backups.Add((backup, file.Target));
                        }

                        File.Move(file.Staged, file.Target, true);

                        log.Text += $"Updated: {Path.GetFileName(file.Target)}\r\n";
                    }
                }
                catch
                {
                    // Restore previous files if applying the update fails.
                    foreach (var backup in backups.AsEnumerable().Reverse())
                    {
                        try
                        {
                            File.Copy(backup.Backup, backup.Target, true);
                        }
                        catch
                        {
                            // Preserve the backup for manual recovery.
                        }
                    }

                    throw;
                }

                // 5. Clean staging files and restart.
                foreach (var backup in backups)
                {
                    try { File.Delete(backup.Backup); }
                    catch { /* Keep backup if cleanup fails. */ }
                }

                try
                {
                    Directory.Delete(StagePath, true);
                }
                catch
                {
                    // Staging cleanup can be retried later.
                }

                log.Text += "Update completed.\r\n";
                log.Text += $"Installed version: {info.latestVersion}\r\n";
                log.Text += "Restarting in 5...\r\n";

                for (int i = 4; i >= 1; i--)
                {
                    await Task.Delay(1000);
                    log.Text += $"{i}...\r\n";
                }

                await Task.Delay(1000);

                Process.Start(new ProcessStartInfo
                {
                    FileName = Path.Combine(CorePath, "deneOS.exe"),
                    UseShellExecute = true
                });

                // Permit the form to close after the restart has been launched.
                allowClose = true;
                Close();
            }
            catch (Exception ex)
            {
                log.Text += $"UPDATE ERROR: {ex}\r\n";

                MessageBox.Show(
                    (string)T("errCouldntgetUpdateInformation"),
                    (string)T("err"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private static async Task DownloadFileAsync(
            string url,
            string destination)
        {
            using var response = await Http.GetAsync(
                url,
                HttpCompletionOption.ResponseHeadersRead);

            response.EnsureSuccessStatusCode();

            await using var source = await response.Content.ReadAsStreamAsync();

            await using var output = new FileStream(
                destination,
                FileMode.Create,
                FileAccess.Write,
                FileShare.None,
                81920,
                useAsync: true);

            await source.CopyToAsync(output);
        }

        private static async Task<bool> FilesMatchAsync(
            string first,
            string second)
        {
            await using var a = File.OpenRead(first);
            await using var b = File.OpenRead(second);

            byte[] hashA = await SHA256.HashDataAsync(a);
            byte[] hashB = await SHA256.HashDataAsync(b);

            return CryptographicOperations.FixedTimeEquals(hashA, hashB);
        }

        private bool allowClose = false;
        private class VersionInfo
        {
            public string latestVersion { get; set; } = "0.0";
            public string changelog { get; set; } = "";
            public CoreInfo core { get; set; } = new();
            public List<DependencyInfo> dependencies { get; set; } = new();
        }

        private class CoreInfo
        {
            public string download { get; set; } = "";
        }

        private class DependencyInfo
        {
            public string name { get; set; } = "";
            public string download { get; set; } = "";
            public string path { get; set; } = "";
        }

        private static readonly string Root = @"C:\DENEOS";
        private static readonly string CorePath = @"C:\DENEOS\core";
        private static readonly string StagePath = @"C:\DENEOS\update-staging";

        private static readonly HttpClient Http = new()
        {
            Timeout = TimeSpan.FromMinutes(10)
        };
    }
}
