using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;

namespace deneStore
{
    public partial class Store : Form
    {
        public static string FixString(string str)
        {
            string newString = string.Empty;   
            int index = 0;
            foreach (char c in str.ToCharArray())
            {
                if (index == 0)
                {
                    newString = c.ToString().ToUpper();
                }
                else
                {
                    newString += c.ToString();
                }
                index++;
            }
            return newString;
        }

        private readonly HttpClient _http = new();
        private readonly DamlParser _parser = new();

        private const string StoreBaseUrl = "https://repoficialx.xyz/deneOS/api/storeApps/";

        TabControl tabControl;
        public Store()
        {
            InitializeComponent();

            tabControl = new TabControl { Dock = DockStyle.Fill };
            this.Controls.Add(tabControl);

            // Pestañas
            string[] categories = { "home", "utils", "social", "adapted", "management", "development" };
            foreach (var category in categories)
            {
                var tab = new TabPage(FixString(category));
                tab.Font = new Font("Segoe UI Variable Display", 12, GraphicsUnit.Point);
                tabControl.TabPages.Add(tab);
                LoadApps(tab, category);
                HomePage_Load();
            }
        }

        private async void HomePage_Load()
        {
            var damlUrl = "https://repoficialx.xyz/deneOS/api/storeApps/home.daml";
            var daml = await _http.GetStringAsync(damlUrl);

            var nodes = _parser.Parse(daml);
            var catalog = new AppCatalog(
                _http,
                "https://repoficialx.xyz/deneOS/api/storeApps/");

            foreach (var node in nodes)
                await RenderAsync(node, tabControl.TabPages[0], catalog);
        }

        private async Task RenderAsync(
    DamlElement node, Control parent, AppCatalog catalog)
        {
            switch (node.Name)
            {
                case "Label":
                    var label = new Label
                    {
                        AutoSize = true,
                        Text = node.Text,
                        Font = node.Attributes.TryGetValue("type", out var type) &&
                               type == "title"
                            ? new Font("Segoe UI", 18, FontStyle.Bold)
                            : new Font("Segoe UI", 10)
                    };
                    parent.Controls.Add(label);
                    break;

                case "App":
                    var app = await catalog.GetAsync(node.Attributes["ref"]);
                    parent.Controls.Add(new Button
                    {
                        AutoSize = true,
                        Text = app.Name,
                        Tag = app.DownloadUrl
                    });
                    break;

                case "AppList":
                case "LColumn":
                case "Banner":
                    var container = new FlowLayoutPanel
                    {
                        AutoSize = true,
                        FlowDirection = FlowDirection.TopDown,
                        WrapContents = false
                    };

                    parent.Controls.Add(container);

                    foreach (var child in node.Children)
                        await RenderAsync(child, container, catalog);
                    break;

                case "Image":
                    var image = new PictureBox
                    {
                        SizeMode = PictureBoxSizeMode.Zoom,
                        Width = 300,
                        Height = 180
                    };
                    image.Load(EnsureAbsoluteUrl(node.Attributes["src"]));
                    parent.Controls.Add(image);
                    break;

                default:
                    throw new InvalidDataException($"Etiqueta DAML no permitida: {node.Name}");
            }
        }

        private async void LoadApps(TabPage tab, string category)
        {
            if (category == "home")
                return;

            var table = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 4,
                RowCount = 4,
                AutoScroll = true
            };

            for (int i = 0; i < 4; i++)
            {
                table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
                table.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            }

            tab.Controls.Add(table);

            try
            {
                var apps = await FetchAppsAsync(category);

                for (int i = 0; i < apps.Length && i < 16; i++)
                {
                    var panel = CreateAppPanel(apps[i]);
                    table.Controls.Add(panel, i % 4, i / 4);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error cargando apps: {ex.Message}");
            }
        }

        private async Task<App[]> FetchAppsAsync(string category)
        {
            using var client = new HttpClient();
            string url = $"https://repoficialx.xyz/deneOS/api/storeApps/{category}.json";
            var json = await client.GetStringAsync(url);
            return JsonSerializer.Deserialize<App[]>(json);
        }

        private Panel CreateAppPanel(App app)
        {
            var panel = new Panel { Dock = DockStyle.Fill, Margin = new Padding(5), BorderStyle = BorderStyle.FixedSingle };

            var picture = new PictureBox
            {
                ImageLocation = EnsureAbsoluteUrl(app.imageUrl),
                SizeMode = PictureBoxSizeMode.Zoom,
                Dock = DockStyle.Top,
                Height = 100
            };

            var nameLabel = new Label
            {
                Text = app.name,
                Dock = DockStyle.Top,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            };

            var descLabel = new Label
            {
                Text = app.description,
                Dock = DockStyle.Top,
                Font = new Font("Segoe UI", 8),
                TextAlign = ContentAlignment.MiddleCenter,
                AutoEllipsis = true,
                Height = 40
            };

            string appDir = $@"C:\SOFTWARE\{app.code}";
            string exePath = Path.Combine(appDir, $"{app.code}.exe"); // suponemos que el ejecutable se llama igual que el código

            var actionButton = new Button
            {
                Font = new Font("Segoe UI Variable Text", 10, FontStyle.Regular),
                Dock = DockStyle.Bottom,
                Height = 30
            };

            if (File.Exists(exePath))
            {
                actionButton.Text = "Run►";
                actionButton.Click += (s, e) => RunApp(exePath);
            }
            else
            {
                actionButton.Text = "Install►";
                actionButton.Click += async (s, e) => await InstallApp(app);
            }

            panel.Controls.Add(actionButton);
            panel.Controls.Add(descLabel);
            panel.Controls.Add(nameLabel);
            panel.Controls.Add(picture);

            return panel;
        }
        private void RunApp(string exePath)
        {
            try
            {
                System.Diagnostics.Process.Start(exePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al ejecutar la aplicación: {ex.Message}");
            }
        }
        public static string AdaptPath(string path)
        {
            int eI = path.IndexOf(@"E\") + 1;
            string fP = path[eI..];
            string p = "~S\\" + fP;
            return p;
        }

        private static string EnsureAbsoluteUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return url;

            // If already an absolute URI (http(s), file, data, etc.) keep it
            if (Uri.IsWellFormedUriString(url, UriKind.Absolute))
                return url;

            // Treat it as a relative filename and resolve against the store base URL
            return StoreBaseUrl + url.TrimStart('/');
        }
        public async Task InstallApp(App app)
        {
            bool esPaqueteDpk = app.downloadUrl.EndsWith(".dpk", StringComparison.OrdinalIgnoreCase);

            string destino;

            if (esPaqueteDpk)
            {
                // Guardar en carpeta temporal
                string tempDir = Path.GetTempPath();
                destino = Path.Combine(tempDir, Path.GetFileName(app.downloadUrl));
            }
            else
            {
                // Guardar en C:\SOFTWARE\{code}
                string appDir = $@"C:\SOFTWARE\{app.code}";
                Directory.CreateDirectory(appDir);
                destino = Path.Combine(appDir, Path.GetFileName(app.downloadUrl));
            }

            using var client = new HttpClient();

            var progressForm = new ProgressForm();
            progressForm.Show();

            using var response = await client.GetAsync(EnsureAbsoluteUrl(app.downloadUrl), HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();

            var totalBytes = response.Content.Headers.ContentLength ?? -1L;
            var buffer = new byte[8192];
            long totalRead = 0;

            using var stream = await response.Content.ReadAsStreamAsync();
            using var fileStream = new FileStream(destino, FileMode.Create, FileAccess.Write, FileShare.None);

            int read;
            while ((read = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
            {
                await fileStream.WriteAsync(buffer, 0, read);
                totalRead += read;

                if (totalBytes > 0)
                {
                    int progress = (int)((totalRead * 100) / totalBytes);
                    progressForm.ProgressBar.Value = Math.Min(progress, 100);
                }

                Application.DoEvents();
            }

            progressForm.Close();

            if (esPaqueteDpk)
            {
                string extractor = @"C:\DENEOS\core\dpkxt.exe";

                var proc = new Process();
                proc.StartInfo.FileName = extractor;
                proc.StartInfo.Arguments = $"\"{destino}\"";
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.CreateNoWindow = true;

                proc.Start();
                proc.WaitForExit(); // Esperar a que termine

                if (Debugger.IsAttached)
                {
                    MessageBox.Show(
                        $"Package processed successfully.\n\n" +
                        $"File: {destino}\n" +
                        $"DPKXT Ended with code: {proc.ExitCode}"
                    );
                }
            }
            else
            {
                MessageBox.Show($"{app.name} instalado en {AdaptPath(Path.GetDirectoryName(destino))}");
            }
        }
        public class App
        {
            public string name { get; set; }
            public string description { get; set; }
            public string imageUrl { get; set; }
            public string downloadUrl { get; set; }
            public string code { get; set; }
        }
    }
}
