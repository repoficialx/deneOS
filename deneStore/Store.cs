
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace deneStore
{
    public partial class Store : Form
    {
        private const string StoreBaseUrl =
            "https://repoficialx.xyz/deneOS/api/storeApps/";

        private const string SoftwareRoot = @"C:\SOFTWARE";

        private readonly HttpClient _http = new();
        private readonly DamlParser _parser = new();
        private readonly AppCatalog _catalog;
        private readonly ToolTip _toolTip = new();

        private readonly HashSet<string> _loadedTabs =
            new(StringComparer.OrdinalIgnoreCase);

        private TabControl tabControl = null!;

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public Store()
        {
            InitializeComponent();

            _catalog = new AppCatalog(_http, StoreBaseUrl);

            tabControl = new TabControl
            {
                Dock = DockStyle.Fill
            };

            Controls.Add(tabControl);
            tabControl.BringToFront();

            BuildTabs();

            tabControl.SelectedIndexChanged +=
                async (_, _) => await LoadSelectedTabAsync();

            Shown += async (_, _) => await LoadSelectedTabAsync();

            FormClosed += (_, _) =>
            {
                _http.Dispose();
                _toolTip.Dispose();
            };
        }

        // =========================================================
        // TABS
        // =========================================================

        public static string FixString(string str)
        {
            if (string.IsNullOrEmpty(str))
                return str ?? string.Empty;

            return char.ToUpperInvariant(str[0]) + str[1..];
        }

        private void BuildTabs()
        {
            string[] categories =
            {
                "home",
                "utils",
                "social",
                "adapted",
                "management",
                "development"
            };

            foreach (string category in categories)
            {
                var tab = new TabPage(FixString(category))
                {
                    Tag = category,
                    Font = new Font(
                        "Segoe UI Variable Display",
                        12,
                        GraphicsUnit.Point)
                };

                tabControl.TabPages.Add(tab);
            }
        }

        private async Task LoadSelectedTabAsync()
        {
            if (tabControl.SelectedTab is not TabPage tab ||
                tab.Tag is not string category)
            {
                return;
            }

            // Evita descargar y renderizar una pestaña más de una vez.
            if (!_loadedTabs.Add(category))
                return;

            try
            {
                if (category.Equals(
                    "home",
                    StringComparison.OrdinalIgnoreCase))
                {
                    await LoadHomePageAsync(tab);
                }
                else
                {
                    await LoadAppsAsync(tab, category);
                }
            }
            catch (Exception ex)
            {
                _loadedTabs.Remove(category);

                tab.Controls.Clear();

                tab.Controls.Add(new Label
                {
                    AutoSize = true,
                    Dock = DockStyle.Top,
                    Padding = new Padding(16),
                    Text = $"No se pudo cargar {category}.\n{ex.Message}",
                    ForeColor = Color.Firebrick
                });
            }
        }

        // =========================================================
        // HOME / DAML
        // =========================================================

        private async Task LoadHomePageAsync(TabPage tab)
        {
            tab.Controls.Clear();

            var scrollHost = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true
            };

            var root = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Padding = new Padding(18),
                Margin = Padding.Empty
            };

            scrollHost.Controls.Add(root);
            tab.Controls.Add(scrollHost);

            Uri damlUri = new(
                new Uri(StoreBaseUrl),
                "home.daml");

            string daml = await _http.GetStringAsync(damlUri);

            var nodes = _parser.Parse(daml);

            foreach (DamlElement node in nodes)
            {
                await RenderAsync(node, root, _catalog);
            }
        }

        private async Task RenderAsync(
            DamlElement node,
            Control parent,
            AppCatalog catalog)
        {
            switch (node.Name)
            {
                // -------------------------------------------------
                // LABEL
                // -------------------------------------------------

                case "Label":
                    {
                        string type = GetAttribute(node, "type", "body");

                        Font font = type switch
                        {
                            "title" => new Font(
                                "Segoe UI Variable Display",
                                19,
                                FontStyle.Bold),

                            "subtitle" => new Font(
                                "Segoe UI Variable Display",
                                14,
                                FontStyle.Bold),

                            _ => new Font(
                                "Segoe UI Variable Text",
                                10,
                                FontStyle.Regular)
                        };

                        var label = new Label
                        {
                            AutoSize = true,
                            Text = node.Text,
                            Font = font,
                            Margin = new Padding(4, 8, 4, 8),
                            ForeColor = SystemColors.ControlText
                        };

                        parent.Controls.Add(label);
                        break;
                    }

                // -------------------------------------------------
                // APP
                // -------------------------------------------------

                case "App":
                    {
                        string reference = GetRequiredAttribute(node, "ref");

                        string show = GetAttribute(node, "show", "card")
                            .ToLowerInvariant();

                        if (show != "icon" &&
                            show != "card" &&
                            show != "full")
                        {
                            throw new InvalidDataException(
                                $"Valor show no permitido: {show}");
                        }

                        StoreApp storeApp = await catalog.GetAsync(reference);

                        App app = ConvertApp(storeApp);

                        Control appControl = CreateAppControl(
                            app,
                            show,
                            parent);

                        parent.Controls.Add(appControl);
                        break;
                    }

                // -------------------------------------------------
                // APP LIST
                // -------------------------------------------------

                case "AppList":
                    {
                        string size = GetAttribute(
                            node,
                            "size",
                            "medium").ToLowerInvariant();

                        string[] validSizes =
                        {
                        "xxtiny",
                        "tiny",
                        "small",
                        "medium",
                        "large"
                    };

                        if (!validSizes.Contains(size))
                        {
                            throw new InvalidDataException(
                                $"Tamaño AppList no permitido: {size}");
                        }

                        var appList = new FlowLayoutPanel
                        {
                            FlowDirection = FlowDirection.LeftToRight,
                            WrapContents = size != "xxtiny",
                            AutoSize = true,
                            AutoSizeMode = AutoSizeMode.GrowAndShrink,
                            Dock = DockStyle.Top,
                            Width = GetContentWidth(parent),
                            Margin = new Padding(0, 4, 0, 12),
                            Padding = new Padding(2)
                        };

                        parent.Controls.Add(appList);

                        foreach (DamlElement child in node.Children)
                        {
                            await RenderAsync(child, appList, catalog);
                        }

                        break;
                    }

                // -------------------------------------------------
                // L COLUMN
                // -------------------------------------------------

                case "LColumn":
                    {
                        var column = new FlowLayoutPanel
                        {
                            FlowDirection = FlowDirection.TopDown,
                            WrapContents = false,
                            AutoSize = true,
                            AutoSizeMode = AutoSizeMode.GrowAndShrink,
                            Dock = DockStyle.Top,
                            Width = GetContentWidth(parent),
                            Margin = new Padding(4),
                            Padding = new Padding(4)
                        };

                        parent.Controls.Add(column);

                        foreach (DamlElement child in node.Children)
                        {
                            await RenderAsync(child, column, catalog);
                        }

                        break;
                    }

                // -------------------------------------------------
                // BANNER
                // -------------------------------------------------

                case "Banner":
                    {
                        int childCount = node.Children.Count;

                        var banner = new TableLayoutPanel
                        {
                            AutoSize = false,
                            Width = GetContentWidth(parent),
                            Height = 220,
                            ColumnCount = Math.Max(childCount, 1),
                            RowCount = 1,
                            Dock = DockStyle.Top,
                            Margin = new Padding(4, 4, 4, 18),
                            Padding = new Padding(4)
                        };

                        banner.ColumnStyles.Clear();

                        if (childCount == 2)
                        {
                            banner.ColumnStyles.Add(
                                new ColumnStyle(SizeType.Percent, 58F));

                            banner.ColumnStyles.Add(
                                new ColumnStyle(SizeType.Percent, 42F));
                        }
                        else
                        {
                            for (int i = 0; i < Math.Max(childCount, 1); i++)
                            {
                                banner.ColumnStyles.Add(
                                    new ColumnStyle(
                                        SizeType.Percent,
                                        100F / Math.Max(childCount, 1)));
                            }
                        }

                        banner.RowStyles.Add(
                            new RowStyle(SizeType.Percent, 100F));

                        parent.Controls.Add(banner);

                        for (int i = 0; i < childCount; i++)
                        {
                            var slot = new Panel
                            {
                                Dock = DockStyle.Fill,
                                Margin = new Padding(4)
                            };

                            banner.Controls.Add(slot, i, 0);

                            await RenderAsync(
                                node.Children[i],
                                slot,
                                catalog);
                        }

                        break;
                    }

                // -------------------------------------------------
                // IMAGE
                // -------------------------------------------------

                case "Image":
                    {
                        string src = GetRequiredAttribute(node, "src");

                        var picture = new PictureBox
                        {
                            SizeMode = PictureBoxSizeMode.Zoom,
                            Dock = DockStyle.Fill,
                            MinimumSize = new Size(120, 90),
                            Margin = new Padding(4),
                            AccessibleDescription = src
                        };

                        parent.Controls.Add(picture);

                        // La descarga no bloquea el renderizado del DAML.
                        _ = LoadPictureBoxImageAsync(picture, src);

                        break;
                    }

                default:
                    throw new InvalidDataException(
                        $"Etiqueta DAML no permitida: {node.Name}");
            }
        }

        // =========================================================
        // DAML APP RENDERING
        // =========================================================

        private Control CreateAppControl(
            App app,
            string show,
            Control parent)
        {
            return show switch
            {
                "icon" => CreateIconApp(app),
                "card" => CreateAppPanel(app),
                "full" => CreateFeaturedApp(app, parent),
                _ => throw new InvalidDataException(
                    $"Formato App no permitido: {show}")
            };
        }

        private Button CreateIconApp(App app)
        {
            var button = new Button
            {
                Width = 68,
                Height = 68,
                Margin = new Padding(4),
                Text = string.Empty,
                AccessibleName = app.name,
                AccessibleDescription = app.description,
                ImageAlign = ContentAlignment.MiddleCenter,
                FlatStyle = FlatStyle.Standard,
                Tag = app
            };

            _toolTip.SetToolTip(
                button,
                $"{app.name}\n{app.description}");

            button.Click += async (_, _) =>
            {
                await HandleAppActionAsync(app, button);
            };

            if (!string.IsNullOrWhiteSpace(app.imageUrl))
            {
                _ = LoadButtonImageAsync(button, app.imageUrl);
            }

            return button;
        }

        private Panel CreateAppPanel(App app)
        {
            var panel = new Panel
            {
                Width = 230,
                Height = 265,
                Margin = new Padding(8),
                BorderStyle = BorderStyle.FixedSingle
            };

            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 4,
                Padding = new Padding(5)
            };

            layout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 105));

            layout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 36));

            layout.RowStyles.Add(
                new RowStyle(SizeType.Percent, 100F));

            layout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 38));

            var picture = new PictureBox
            {
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.Zoom,
                Margin = new Padding(4)
            };

            var nameLabel = new Label
            {
                Text = app.name,
                Dock = DockStyle.Fill,
                Font = new Font(
                    "Segoe UI Variable Display",
                    12,
                    FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                AutoEllipsis = true
            };

            var descriptionLabel = new Label
            {
                Text = app.description,
                Dock = DockStyle.Fill,
                Font = new Font(
                    "Segoe UI Variable Text",
                    8),
                TextAlign = ContentAlignment.TopLeft,
                AutoEllipsis = true,
                Padding = new Padding(3)
            };

            var actionButton = new Button
            {
                Dock = DockStyle.Fill,
                Font = new Font(
                    "Segoe UI Variable Text",
                    9),
                Margin = new Padding(2)
            };

            RefreshActionButton(app, actionButton);

            actionButton.Click += async (_, _) =>
            {
                await HandleAppActionAsync(app, actionButton);
            };

            layout.Controls.Add(picture, 0, 0);
            layout.Controls.Add(nameLabel, 0, 1);
            layout.Controls.Add(descriptionLabel, 0, 2);
            layout.Controls.Add(actionButton, 0, 3);

            panel.Controls.Add(layout);

            if (!string.IsNullOrWhiteSpace(app.imageUrl))
            {
                _ = LoadPictureBoxImageAsync(picture, app.imageUrl);
            }

            return panel;
        }

        private Control CreateFeaturedApp(
            App app,
            Control parent)
        {
            int width = GetContentWidth(parent);

            var panel = new Panel
            {
                Width = width,
                Height = 155,
                Margin = new Padding(4, 8, 4, 16),
                BorderStyle = BorderStyle.FixedSingle
            };

            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 3,
                RowCount = 1,
                Padding = new Padding(8)
            };

            layout.ColumnStyles.Add(
                new ColumnStyle(SizeType.Absolute, 115));

            layout.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 100F));

            layout.ColumnStyles.Add(
                new ColumnStyle(SizeType.Absolute, 120));

            var picture = new PictureBox
            {
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.Zoom,
                Margin = new Padding(5)
            };

            var details = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true,
                Padding = new Padding(6)
            };

            details.Controls.Add(new Label
            {
                AutoSize = true,
                MaximumSize = new Size(
                    Math.Max(150, width - 300),
                    0),
                Text = app.name,
                Font = new Font(
                    "Segoe UI Variable Display",
                    15,
                    FontStyle.Bold)
            });

            details.Controls.Add(new Label
            {
                AutoSize = true,
                MaximumSize = new Size(
                    Math.Max(150, width - 300),
                    0),
                Text = app.description,
                Font = new Font(
                    "Segoe UI Variable Text",
                    9)
            });

            var actionButton = new Button
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(5),
                Font = new Font(
                    "Segoe UI Variable Text",
                    9)
            };

            RefreshActionButton(app, actionButton);

            actionButton.Click += async (_, _) =>
            {
                await HandleAppActionAsync(app, actionButton);
            };

            layout.Controls.Add(picture, 0, 0);
            layout.Controls.Add(details, 1, 0);
            layout.Controls.Add(actionButton, 2, 0);

            panel.Controls.Add(layout);

            if (!string.IsNullOrWhiteSpace(app.imageUrl))
            {
                _ = LoadPictureBoxImageAsync(picture, app.imageUrl);
            }

            return panel;
        }

        // =========================================================
        // CATEGORY JSON
        // =========================================================

        private async Task LoadAppsAsync(
            TabPage tab,
            string category)
        {
            App[] apps = await FetchAppsAsync(category);

            tab.Controls.Clear();

            var table = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                ColumnCount = 4,
                RowCount = Math.Max(
                    1,
                    (apps.Length + 3) / 4),
                Padding = new Padding(8)
            };

            for (int i = 0; i < 4; i++)
            {
                table.ColumnStyles.Add(
                    new ColumnStyle(SizeType.Percent, 25F));
            }

            for (int i = 0; i < table.RowCount; i++)
            {
                table.RowStyles.Add(
                    new RowStyle(SizeType.Absolute, 280F));
            }

            tab.Controls.Add(table);

            if (apps.Length == 0)
            {
                table.Controls.Add(new Label
                {
                    Text = "No hay aplicaciones disponibles.",
                    AutoSize = true,
                    Padding = new Padding(12)
                }, 0, 0);

                return;
            }

            for (int i = 0; i < apps.Length; i++)
            {
                var panel = CreateAppPanel(apps[i]);

                panel.Dock = DockStyle.Fill;

                table.Controls.Add(
                    panel,
                    i % 4,
                    i / 4);
            }
        }

        private async Task<App[]> FetchAppsAsync(string category)
        {
            Uri url = new(
                new Uri(StoreBaseUrl),
                $"{Uri.EscapeDataString(category)}.json");

            using var response = await _http.GetAsync(url);

            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<App[]>(
                json,
                JsonOptions) ?? Array.Empty<App>();
        }

        // =========================================================
        // INSTALL / RUN
        // =========================================================

        private async Task HandleAppActionAsync(
            App app,
            Button button)
        {
            if (!button.Enabled)
                return;

            button.Enabled = false;

            try
            {
                string exePath = GetInstalledExePath(app);

                if (File.Exists(exePath))
                {
                    RunApp(exePath);
                }
                else
                {
                    await InstallApp(app);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"No se pudo ejecutar la acción para {app.name}.\n\n{ex.Message}",
                    "deneStore",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                if (!button.IsDisposed)
                {
                    RefreshActionButton(app, button);
                    button.Enabled = true;
                }
            }
        }

        private static void RefreshActionButton(
            App app,
            Button button)
        {
            string exePath = GetInstalledExePath(app);

            button.Text = File.Exists(exePath)
                ? "Run ▶"
                : "Install ▶";
        }

        private void RunApp(string exePath)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = exePath,
                    WorkingDirectory =
                        Path.GetDirectoryName(exePath)
                        ?? SoftwareRoot,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error al ejecutar la aplicación:\n{ex.Message}",
                    "deneStore",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        public async Task InstallApp(App app)
        {
            Uri downloadUri = ResolveUrl(app.downloadUrl);

            bool isDpk = downloadUri.AbsolutePath.EndsWith(
                ".dpk",
                StringComparison.OrdinalIgnoreCase);

            string destination;

            if (isDpk)
            {
                destination = Path.Combine(
                    Path.GetTempPath(),
                    Path.GetFileName(downloadUri.LocalPath));
            }
            else
            {
                string appDir = GetAppDirectory(app.code);

                Directory.CreateDirectory(appDir);

                destination = Path.Combine(
                    appDir,
                    Path.GetFileName(downloadUri.LocalPath));
            }

            if (string.IsNullOrWhiteSpace(
                Path.GetFileName(destination)))
            {
                throw new InvalidDataException(
                    "La URL no contiene un nombre de archivo válido.");
            }

            using var progressForm = new ProgressForm();

            progressForm.StartPosition =
                FormStartPosition.CenterParent;

            try
            {
                progressForm.Show(this);

                using var response = await _http.GetAsync(
                    downloadUri,
                    HttpCompletionOption.ResponseHeadersRead);

                response.EnsureSuccessStatusCode();

                long totalBytes =
                    response.Content.Headers.ContentLength ?? -1L;

                progressForm.ProgressBar.Style = totalBytes > 0
                    ? ProgressBarStyle.Continuous
                    : ProgressBarStyle.Marquee;

                using var stream =
                    await response.Content.ReadAsStreamAsync();

                using var fileStream = new FileStream(
                    destination,
                    FileMode.Create,
                    FileAccess.Write,
                    FileShare.None,
                    8192,
                    useAsync: true);

                byte[] buffer = new byte[8192];
                long totalRead = 0;
                int read;

                while ((read = await stream.ReadAsync(
                    buffer, 0, buffer.Length)) > 0)
                {
                    await fileStream.WriteAsync(
                        buffer, 0, read);

                    totalRead += read;

                    if (totalBytes > 0 &&
                        !progressForm.IsDisposed)
                    {
                        int progress = (int)(
                            totalRead * 100L / totalBytes);

                        progressForm.ProgressBar.Value =
                            Math.Clamp(progress, 0, 100);
                    }
                }
            }
            finally
            {
                if (!progressForm.IsDisposed)
                {
                    progressForm.Close();
                }
            }

            if (isDpk)
            {
                string extractor = @"C:\DENEOS\core\dpkxt.exe";

                if (!File.Exists(extractor))
                {
                    throw new FileNotFoundException(
                        "No se encuentra el extractor de paquetes.",
                        extractor);
                }

                using var process = new Process();

                process.StartInfo = new ProcessStartInfo
                {
                    FileName = extractor,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                process.StartInfo.ArgumentList.Add(destination);

                process.Start();

                await process.WaitForExitAsync();

                if (process.ExitCode != 0)
                {
                    throw new InvalidOperationException(
                        $"DPKXT terminó con el código {process.ExitCode}.");
                }

                if (Debugger.IsAttached)
                {
                    MessageBox.Show(
                        $"Paquete procesado correctamente.\n\n" +
                        $"Archivo: {destination}\n" +
                        $"Código DPKXT: {process.ExitCode}",
                        "deneStore");
                }
            }
            else
            {
                MessageBox.Show(
                    $"{app.name} descargado en " +
                    $"{AdaptPath(Path.GetDirectoryName(destination)!)}",
                    "deneStore",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private static string GetInstalledExePath(App app)
        {
            string appDir = GetAppDirectory(app.code);

            string expectedPath = Path.Combine(
                appDir,
                $"{app.code}.exe");

            if (File.Exists(expectedPath))
                return expectedPath;

            // Permite detectar ejecutables cuyo nombre no coincide
            // con el código de la aplicación.
            Uri downloadUri = ResolveUrl(app.downloadUrl);

            string downloadedName = Path.GetFileName(
                downloadUri.LocalPath);

            if (downloadedName.EndsWith(
                ".exe",
                StringComparison.OrdinalIgnoreCase))
            {
                return Path.Combine(appDir, downloadedName);
            }

            return expectedPath;
        }

        private static string GetAppDirectory(string code)
        {
            if (string.IsNullOrWhiteSpace(code) ||
                !code.All(c =>
                    char.IsLetterOrDigit(c) ||
                    c == '_' ||
                    c == '-'))
            {
                throw new InvalidDataException(
                    $"Código de aplicación no válido: {code}");
            }

            return Path.Combine(SoftwareRoot, code);
        }

        public static string AdaptPath(string path)
        {
            string fullPath = Path.GetFullPath(path);
            string root = Path.GetFullPath(SoftwareRoot);

            if (fullPath.Equals(
                root,
                StringComparison.OrdinalIgnoreCase))
            {
                return @"~S\";
            }

            if (fullPath.StartsWith(
                root + Path.DirectorySeparatorChar,
                StringComparison.OrdinalIgnoreCase))
            {
                string relative = Path.GetRelativePath(
                    root,
                    fullPath);

                return @"~S\" + relative.Replace('/', '\\');
            }

            return fullPath;
        }

        // =========================================================
        // URLS / IMAGES
        // =========================================================

        private static Uri ResolveUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new InvalidDataException(
                    "La URL está vacía.");
            }

            Uri uri;

            if (!Uri.TryCreate(
                url,
                UriKind.Absolute,
                out uri!))
            {
                uri = new Uri(new Uri(StoreBaseUrl), url);
            }

            if (uri.Scheme != Uri.UriSchemeHttp &&
                uri.Scheme != Uri.UriSchemeHttps)
            {
                throw new InvalidDataException(
                    "Solo se admiten URLs HTTP y HTTPS.");
            }

            return uri;
        }

        private static string EnsureAbsoluteUrl(string url)
        {
            return ResolveUrl(url).AbsoluteUri;
        }

        private async Task LoadPictureBoxImageAsync(
            PictureBox picture,
            string source)
        {
            try
            {
                byte[] bytes = await _http.GetByteArrayAsync(
                    ResolveUrl(source));

                using var memory = new MemoryStream(bytes);
                using var original = Image.FromStream(memory);

                var bitmap = new Bitmap(original);

                if (picture.IsDisposed)
                {
                    bitmap.Dispose();
                    return;
                }

                Image? previous = picture.Image;
                picture.Image = bitmap;
                previous?.Dispose();
            }
            catch
            {
                // Una imagen fallida no debe impedir abrir la tienda.
            }
        }

        private async Task LoadButtonImageAsync(
            Button button,
            string source)
        {
            try
            {
                byte[] bytes = await _http.GetByteArrayAsync(
                    ResolveUrl(source));

                using var memory = new MemoryStream(bytes);
                using var original = Image.FromStream(memory);

                var bitmap = new Bitmap(original);

                bitmap = new Bitmap(
                    bitmap,
                    new Size(38, 38));

                if (button.IsDisposed)
                {
                    bitmap.Dispose();
                    return;
                }

                Image? previous = button.Image;
                button.Image = bitmap;
                previous?.Dispose();
            }
            catch
            {
                // Se conserva el botón si la imagen no está disponible.
            }
        }

        // =========================================================
        // HELPERS
        // =========================================================

        private static int GetContentWidth(
            Control parent,
            int fallback = 900)
        {
            int width = parent.ClientSize.Width;

            if (width <= 100)
                width = fallback;

            return Math.Max(320, width - 8);
        }

        private static string GetRequiredAttribute(
            DamlElement node,
            string attribute)
        {
            if (!node.Attributes.TryGetValue(
                    attribute,
                    out string? value) ||
                string.IsNullOrWhiteSpace(value))
            {
                throw new InvalidDataException(
                    $"Falta el atributo '{attribute}' " +
                    $"en el componente {node.Name}.");
            }

            return value;
        }

        private static string GetAttribute(
            DamlElement node,
            string attribute,
            string defaultValue)
        {
            return node.Attributes.TryGetValue(
                    attribute,
                    out string? value)
                ? value
                : defaultValue;
        }

        private static App ConvertApp(StoreApp app)
        {
            return new App
            {
                name = app.Name,
                description = app.Description,
                imageUrl = app.ImageUrl,
                downloadUrl = app.DownloadUrl,
                code = app.Code
            };
        }

        public class App
        {
            public string name { get; set; } = "";
            public string description { get; set; } = "";
            public string imageUrl { get; set; } = "";
            public string downloadUrl { get; set; } = "";
            public string code { get; set; } = "";
        }
    }
}
