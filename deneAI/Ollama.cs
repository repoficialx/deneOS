using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace deneAI
{
    public partial class Ollama : Form
    {
        private bool installed = false;
        public Ollama()
        {
            InitializeComponent();
        }

        private async void btnCheckOllama_Click(object sender, EventArgs e)
        {
            if (IsOllamaInstalled())
            {
                lblStatus.Text = "✅ Ollama already installed.";
                installed = true;
                await Task.Delay(2000);
                Close();
                return;
            }

            var result = MessageBox.Show(
                "Ollama isn't installed. ¿Install now? (~2 GB)",
                "Ollama required",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result != DialogResult.Yes) return;

            progressBar1.Visible = true;
            lblStatus.Text = "Downloading Ollama...";

            var progress = new Progress<int>(p =>
            {
                progressBar1.Value = p;
                lblStatus.Text = $"Downloading... {p}%";
            });

            try
            {
                await DownloadAndInstallOllamaAsync(progress);
                lblStatus.Text = "✅ Ollama installed successfully.";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during installation: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                progressBar1.Visible = false;
            }
        }

        private async Task DownloadAndInstallOllamaAsync(IProgress<int> progress)
        {
            string installerUrl = "https://ollama.com/download/OllamaSetup.exe";
            string tempPath = Path.Combine(Path.GetTempPath(), "OllamaSetup.exe");

            using (var client = new HttpClient())
            using (var response = await client.GetAsync(installerUrl, HttpCompletionOption.ResponseHeadersRead))
            {
                var totalBytes = response.Content.Headers.ContentLength ?? -1;
                using var stream = await response.Content.ReadAsStreamAsync();
        
                using (var fileStream = new FileStream(tempPath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    var buffer = new byte[8192];
                    long downloaded = 0;
                    int read;

                    while ((read = await stream.ReadAsync(buffer)) > 0)
                    {
                        await fileStream.WriteAsync(buffer.AsMemory(0, read));
                        downloaded += read;
                        if (totalBytes > 0)
                            progress?.Report((int)(downloaded * 100 / totalBytes));
                    }

                    await fileStream.FlushAsync();
                } // liberado
            }

            // archivo liberado
            var psi = new ProcessStartInfo
            {
                FileName = tempPath,
                Arguments = "/SILENT",
                UseShellExecute = true
            };

            using var proc = Process.Start(psi);
            await proc!.WaitForExitAsync();

            File.Delete(tempPath);
            
            /*
            // download
            using var client = new HttpClient();
            using var response = await client.GetAsync(installerUrl, HttpCompletionOption.ResponseHeadersRead);
            var totalBytes = response.Content.Headers.ContentLength ?? -1;

            using var stream = await response.Content.ReadAsStreamAsync();
            using var fileStream = new FileStream(tempPath, FileMode.Create);

            var buffer = new byte[8192];
            long downloaded = 0;
            int read;

            while ((read = await stream.ReadAsync(buffer)) > 0)
            {
                await fileStream.WriteAsync(buffer.AsMemory(0, read));
                downloaded += read;
                if (totalBytes > 0)
                    progress?.Report((int)(downloaded * 100 / totalBytes));
            }

            // run
            var psi = new ProcessStartInfo
            {
                FileName = tempPath,
                Arguments = "/SILENT",   // flag
                UseShellExecute = true
            };

            using var proc = Process.Start(psi);
            await proc!.WaitForExitAsync();

            // clean
            File.Delete(tempPath);*/
        }

        private bool IsOllamaInstalled()
        {
            try
            {
                using var client = new HttpClient { Timeout = TimeSpan.FromSeconds(2) };
                var response = client.GetAsync("http://localhost:11434/api/tags").Result;
                return response.IsSuccessStatusCode;
            }
            catch { }

            string defaultPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "Programs", "Ollama", "ollama.exe"
            );
            return File.Exists(defaultPath);
        }

        private void Ollama_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!installed)
            {
                e.Cancel = true;
            }
        }
    }
}
