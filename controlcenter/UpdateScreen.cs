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
            // 0.2b -> 0 . 2 . 1
            int[] preVersion = { x.FileMajorPart, x.FileMinorPart, x.FileBuildPart };
            string tag = preVersion[2] == 0 ? "" : (preVersion[2] == 1 ? "b" : "a");
            currentVersion = $"{preVersion[0]}.{preVersion[1]}{tag}";
            //lblCurrentVersion.Text = (string)T("actualVersion") + ": " + currentVersion;
        }

        private void UpdateScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            return;
        }

        private async void UpdateScreen_Load(object sender, EventArgs e)
        {
             /* Ready to restart.
             * Restarting in 5... 4... 3... 2... 1...
             * Restarting now-
             */

            log.Text = $"LOG # {DateTime.Now.ToString("yyyy-MM-dd")} # {DateTime.Now.ToString("HH:mm")}";
            log.Text += "Getting information about update..." + Environment.NewLine;

            using HttpClient client = new HttpClient();
            try
            {
                string json = await client.GetStringAsync("https://repoficialx.xyz/deneOS/api/versions.json");
                var info = JsonSerializer.Deserialize<UpdateInfo>(json);

                if (info != null)
                {
                    latestVersion = info.latestVersion;
                    changelog = info.changelog;
                    downloadUrl = forcedUrl ? downloadUrl : info.download;
                    log.Text += "Deleting actual system files..." + Environment.NewLine;
                    Process.Start("taskkill", "/f /im deneOS.exe");
                    ZipFile.CreateFromDirectory(@"C:\DENEOS\core\", @$"C:\DENEOS\core{currentVersion}_backup.bck");
                    Directory.Delete(@"C:\DENEOS\lang\", true);
                    Directory.Delete(@"C:\DENEOS\core\", true);
                    log.Text += "Starting download..." + Environment.NewLine;
                    log.Text += "Copying compressed new system files..." + Environment.NewLine;
                    WebClient client1 = new();
                    client1.DownloadFile(downloadUrl, @"C:\DENEOS\newcore.zip");
                    log.Text += "Decompressing new system files..." + Environment.NewLine;
                    Directory.CreateDirectory(@"C:\DENEOS\lang\");
                    ZipFile.ExtractToDirectory(@"C:\DENEOS\newcore.zip", @"C:\DENEOS\core\");
                    log.Text += "Patching..." + Environment.NewLine;
                    File.Move(@"C:\DENEOS\core\es.json", @"C:\DENEOS\lang\es.json");
                    File.Move(@"C:\DENEOS\core\en.json", @"C:\DENEOS\lang\en.json");
                    File.Delete(@"C:\DENEOS\newcore.zip");
                    log.Text += "Ready to restart" + Environment.NewLine;
                    log.Text += "Restarting in 5... ";
                    await Task.Delay(1000);
                    log.Text += "4... ";
                    await Task.Delay(1000);
                    log.Text += "3... ";
                    await Task.Delay(1000);
                    log.Text += "2... ";
                    await Task.Delay(1000);
                    log.Text += "1... "  + Environment.NewLine;
                    await Task.Delay(1000);
                    log.Text += "Restarting.";
                    await Task.Delay(1000);
                    Process.Start(@"C:\DENEOS\core\deneOS.exe");
                }
            }
            catch
            {
                MessageBox.Show((string)T("errCouldntgetUpdateInformation"), (string)T("err"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private class UpdateInfo
        {
            public string latestVersion { get; set; }
            public string download { get; set; }
            public string changelog { get; set; }
        }
    }
}
