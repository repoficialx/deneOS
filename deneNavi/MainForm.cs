using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;

namespace deneNavi
{
    public partial class MainForm : Form
    {
        private List<BrowserTab> tabs = new List<BrowserTab>();
        private BrowserTab activeTab;
        private const string DOWNLOADS_PATH = @"C:\DNUSR\Downloads";
        private const string HOME_URL = "dnnav:home";

        public MainForm()
        {
            InitializeComponent();
            InitializeBrowser();
        }

        private async void InitializeBrowser()
        {
            // Asegurar que existe la carpeta de descargas
            Directory.CreateDirectory(DOWNLOADS_PATH);

            // Configurar eventos
            newTabButton = new Button
            {
                Text = "\uE710", // Icono de Añadir (+)
                Size = new Size(35, 30),
                Location = new Point(5, 7),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe Fluent Icons", 14F)
            };
            newTabButton.Click += (s, e) => CreateNewTab(HOME_URL);
            tabPanel.Controls.Add(newTabButton);

            backButton.Click += BackButton_Click;
            forwardButton.Click += ForwardButton_Click;
            reloadButton.Click += ReloadButton_Click;
            homeButton.Click += HomeButton_Click;
            goButton.Click += GoButton_Click;
            menuButton.Click += MenuButton_Click;
            urlTextBox.KeyDown += UrlTextBox_KeyDown;

            // Crear la primera pestaña
            CreateNewTab(HOME_URL);
        }

        private async void CreateNewTab(string url)
        {
            var tab = new BrowserTab();
            tab.WebView = new WebView2
            {
                Dock = DockStyle.Fill
            };

            // Inicializar WebView2
            await tab.WebView.EnsureCoreWebView2Async(null);

            // Configurar descargas y eventos
            tab.WebView.CoreWebView2.DownloadStarting += CoreWebView2_DownloadStarting;
            tab.WebView.CoreWebView2.NavigationStarting += (s, e) => CoreWebView2_NavigationStarting(s, e, tab);
            tab.WebView.CoreWebView2.NavigationCompleted += (s, e) => CoreWebView2_NavigationCompleted(s, e, tab);
            tab.WebView.CoreWebView2.DocumentTitleChanged += (s, e) => CoreWebView2_DocumentTitleChanged(s, e, tab);
            tab.WebView.CoreWebView2.SourceChanged += (s, e) => WebView_SourceChanged(s, e, tab);

            // Crear botón de pestaña
            tab.TabButton = new Button
            {
                Text = "Nueva pestaña",
                Width = 180,
                Height = 30,
                FlatStyle = FlatStyle.Flat,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(8, 0, 25, 0),
                Font = new Font("Segoe UI", 9F),
                Tag = tab
            };
            tab.TabButton.Click += TabButton_Click;

            // Crear botón de cerrar
            tab.CloseButton = new Button
            {
                Text = "\uE711", // Icono de cerrar (X)
                Width = 24,
                Height = 24,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe Fluent Icons", 9F),
                Tag = tab,
                BackColor = Color.Transparent,
                ForeColor = Color.FromArgb(100, 100, 100),
                Cursor = Cursors.Hand
            };
            tab.CloseButton.FlatAppearance.BorderSize = 0;
            tab.CloseButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(232, 17, 35);
            tab.CloseButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(200, 15, 30);
            tab.CloseButton.MouseEnter += (s, ev) => {
                tab.CloseButton.ForeColor = Color.White;
            };
            tab.CloseButton.MouseLeave += (s, ev) => {
                tab.CloseButton.ForeColor = Color.FromArgb(100, 100, 100);
            };
            tab.CloseButton.Click += CloseButton_Click;

            // Posicionar pestañas
            int xPos = 45;
            foreach (var t in tabs)
            {
                t.TabButton.Location = new Point(xPos, 5);
                xPos += t.TabButton.Width + 5;
            }
            tab.TabButton.Location = new Point(xPos, 5);
            tab.CloseButton.Location = new Point(tab.TabButton.Right - 28, tab.TabButton.Top + 3);

            tabPanel.Controls.Add(tab.TabButton);
            tabPanel.Controls.Add(tab.CloseButton);
            tab.CloseButton.BringToFront(); // Asegurar que está al frente
            tabs.Add(tab);

            SwitchToTab(tab);
            NavigateToUrl(url);
        }

        private void TabButton_Click(object sender, EventArgs e)
        {
            var button = sender as Button;
            var tab = button.Tag as BrowserTab;
            SwitchToTab(tab);
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            var button = sender as Button;
            var tab = button.Tag as BrowserTab;
            CloseTab(tab);
        }

        private void CloseTab(BrowserTab tab)
        {
            if (tabs.Count == 1)
            {
                Application.Exit();
                return;
            }

            tabs.Remove(tab);
            tabPanel.Controls.Remove(tab.TabButton);
            tabPanel.Controls.Remove(tab.CloseButton);
            contentPanel.Controls.Remove(tab.WebView);

            tab.WebView?.Dispose();

            if (activeTab == tab)
            {
                SwitchToTab(tabs.Last());
            }

            // Reposicionar pestañas
            int xPos = 45;
            foreach (var t in tabs)
            {
                t.TabButton.Location = new Point(xPos, 5);
                t.CloseButton.Location = new Point(t.TabButton.Right - 28, t.TabButton.Top + 3);
                xPos += t.TabButton.Width + 5;
            }
        }

        private void SwitchToTab(BrowserTab tab)
        {
            if (activeTab != null)
            {
                activeTab.WebView.Visible = false;
                activeTab.TabButton.BackColor = Color.FromArgb(230, 230, 230);
            }

            activeTab = tab;
            activeTab.WebView.Visible = true;
            activeTab.TabButton.BackColor = Color.White;

            if (!contentPanel.Controls.Contains(activeTab.WebView))
            {
                contentPanel.Controls.Add(activeTab.WebView);
            }

            UpdateNavigationButtons();

            // Actualizar la barra de URL con la URL actual de la pestaña
            if (!string.IsNullOrEmpty(activeTab.CurrentUrl))
            {
                urlTextBox.Text = activeTab.CurrentUrl;
            }
            else
            {
                urlTextBox.Text = "";
            }

            // Actualizar el título de la ventana principal
            UpdateWindowTitle();
        }

        private void UpdateWindowTitle()
        {
            if (activeTab != null && activeTab.WebView?.CoreWebView2 != null)
            {
                string title = activeTab.WebView.CoreWebView2.DocumentTitle;
                if (string.IsNullOrEmpty(title) || title == "about:blank")
                {
                    this.Text = "deneNavi";
                }
                else
                {
                    this.Text = $"{title} - deneNavi";
                }
            }
            else
            {
                this.Text = "deneNavi";
            }
        }

        private void NavigateToUrl(string url)
        {
            if (activeTab == null) return;

            // Procesar URLs especiales de deneNavi
            if (url.StartsWith("dnnav:", StringComparison.OrdinalIgnoreCase))
            {
                HandleSpecialUrl(url);
                return;
            }

            // Agregar protocolo si no existe
            if (!url.StartsWith("http://") && !url.StartsWith("https://"))
            {
                // Si parece una URL, agregar https
                if (url.Contains(".") && !url.Contains(" "))
                {
                    url = "https://" + url;
                }
                else
                {
                    // Buscar en Google
                    url = "https://www.google.com/search?q=" + Uri.EscapeDataString(url);
                }
            }

            activeTab.CurrentUrl = url;
            activeTab.WebView.CoreWebView2.Navigate(url);
        }

        private void HandleSpecialUrl(string url)
        {
            string page = url.ToLower().Replace("dnnav:", "");
            string html = "";

            switch (page)
            {
                case "home":
                    html = GenerateHomePage();
                    break;
                case "about":
                    html = GenerateAboutPage();
                    break;
                case "passwords":
                    html = GeneratePasswordsPage();
                    break;
                case "downloads":
                    html = GenerateDownloadsPage();
                    break;
                case "history":
                    html = GenerateHistoryPage();
                    break;
                default:
                    html = Generate404Page();
                    break;
            }

            activeTab.CurrentUrl = url;
            urlTextBox.Text = url;
            activeTab.WebView.NavigateToString(html);
        }

        private string GenerateHomePage()
        {
            return @"
<!DOCTYPE html>
<html>
<head>
    <title>deneNavi - Inicio</title>
    <style>
        body { 
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; 
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            color: white;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
            margin: 0;
        }
        .container {
            text-align: center;
            background: rgba(255,255,255,0.1);
            padding: 50px;
            border-radius: 20px;
            backdrop-filter: blur(10px);
        }
        h1 { font-size: 4em; margin: 0; text-shadow: 2px 2px 4px rgba(0,0,0,0.3); }
        p { font-size: 1.2em; margin: 20px 0; }
        .links { margin-top: 30px; }
        .links a {
            color: white;
            text-decoration: none;
            margin: 0 15px;
            padding: 10px 20px;
            background: rgba(255,255,255,0.2);
            border-radius: 5px;
            display: inline-block;
            margin-top: 10px;
        }
        .links a:hover { background: rgba(255,255,255,0.3); }
    </style>
</head>
<body>
    <div class='container'>
        <h1>deneNavi</h1>
        <p>Navegador oficial de deneOS</p>
        <div class='links'>
            <a href='dnnav:about'>Acerca de</a>
            <a href='dnnav:downloads'>Descargas</a>
            <a href='dnnav:history'>Historial</a>
            <a href='dnnav:passwords'>Contraseñas</a>
        </div>
    </div>
</body>
</html>";
        }

        private string GenerateAboutPage()
        {
            return @"
<!DOCTYPE html>
<html>
<head>
    <title>Acerca de deneNavi</title>
    <style>
        body { 
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; 
            background: #f5f5f5;
            margin: 0;
            padding: 20px;
        }
        .container {
            max-width: 800px;
            margin: 0 auto;
            background: white;
            padding: 40px;
            border-radius: 10px;
            box-shadow: 0 2px 10px rgba(0,0,0,0.1);
        }
        h1 { color: #667eea; }
        .version { color: #666; font-size: 0.9em; }
        .info { margin: 20px 0; line-height: 1.6; }
    </style>
</head>
<body>
    <div class='container'>
        <h1>deneNavi</h1>
        <p class='version'>Versión 1.0.0 - Navegador oficial de deneOS</p>
        <div class='info'>
            <h2>Características</h2>
            <ul>
                <li>Navegación por pestañas</li>
                <li>Motor WebView2 de Microsoft</li>
                <li>URLs especiales del sistema (dnnav:)</li>
                <li>Gestión de descargas integrada</li>
                <li>Interfaz moderna estilo Chrome</li>
            </ul>
            <h2>URLs especiales</h2>
            <ul>
                <li><strong>dnnav:home</strong> - Página de inicio</li>
                <li><strong>dnnav:about</strong> - Información del navegador</li>
                <li><strong>dnnav:downloads</strong> - Gestor de descargas</li>
                <li><strong>dnnav:history</strong> - Historial de navegación</li>
                <li><strong>dnnav:passwords</strong> - Gestor de contraseñas</li>
            </ul>
            <p style='margin-top: 30px; color: #888;'>
                © 2024 deneOS Project. Desarrollado con C# y .NET 10
            </p>
        </div>
    </div>
</body>
</html>";
        }

        private string GeneratePasswordsPage()
        {
            return GenerateSimplePage("Gestor de Contraseñas",
                "El gestor de contraseñas estará disponible en futuras versiones de deneNavi.");
        }

        private string GenerateDownloadsPage()
        {
            var files = Directory.Exists(DOWNLOADS_PATH)
                ? Directory.GetFiles(DOWNLOADS_PATH)
                : new string[0];

            string fileList = "";
            foreach (var file in files.OrderByDescending(f => File.GetCreationTime(f)))
            {
                var info = new FileInfo(file);
                fileList += $"<li><strong>{info.Name}</strong> - {FormatBytes(info.Length)} - {info.CreationTime}</li>";
            }

            if (string.IsNullOrEmpty(fileList))
                fileList = "<li>No hay descargas</li>";

            return $@"
<!DOCTYPE html>
<html>
<head>
    <title>Descargas</title>
    <style>
        body {{ 
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; 
            background: #f5f5f5;
            margin: 0;
            padding: 20px;
        }}
        .container {{
            max-width: 900px;
            margin: 0 auto;
            background: white;
            padding: 40px;
            border-radius: 10px;
        }}
        h1 {{ color: #667eea; }}
        ul {{ list-style: none; padding: 0; }}
        li {{ 
            padding: 15px; 
            border-bottom: 1px solid #eee; 
            margin: 10px 0;
        }}
        .path {{ color: #888; font-size: 0.9em; margin-top: 10px; }}
    </style>
</head>
<body>
    <div class='container'>
        <h1>Descargas</h1>
        <p class='path'>Ubicación: {DOWNLOADS_PATH}</p>
        <ul>{fileList}</ul>
    </div>
</body>
</html>";
        }

        private string GenerateHistoryPage()
        {
            var historyEntries = GetBrowserHistory();

            string historyList = "";
            if (historyEntries.Count == 0)
            {
                historyList = "<div class='empty'>No hay historial de navegación</div>";
            }
            else
            {
                foreach (var entry in historyEntries)
                {
                    string favicon = "🌐";
                    string dateStr = entry.LastVisit.ToString("dd/MM/yyyy HH:mm");

                    historyList += $@"
                    <div class='history-item'>
                        <div class='favicon'>{favicon}</div>
                        <div class='info'>
                            <a href='{entry.Url}' class='title'>{System.Security.SecurityElement.Escape(entry.Title)}</a>
                            <div class='url'>{System.Security.SecurityElement.Escape(entry.Url)}</div>
                            <div class='date'>{dateStr} • {entry.VisitCount} visita{(entry.VisitCount > 1 ? "s" : "")}</div>
                        </div>
                    </div>";
                }
            }

            return $@"
<!DOCTYPE html>
<html>
<head>
    <title>Historial de Navegación</title>
    <style>
        body {{ 
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; 
            background: #f5f5f5;
            margin: 0;
            padding: 20px;
        }}
        .container {{
            max-width: 900px;
            margin: 0 auto;
            background: white;
            padding: 40px;
            border-radius: 10px;
            box-shadow: 0 2px 10px rgba(0,0,0,0.1);
        }}
        h1 {{ 
            color: #667eea; 
            margin-top: 0;
            border-bottom: 2px solid #667eea;
            padding-bottom: 15px;
        }}
        .history-item {{
            display: flex;
            align-items: flex-start;
            padding: 15px;
            border-bottom: 1px solid #eee;
            transition: background 0.2s;
        }}
        .history-item:hover {{
            background: #f9f9f9;
        }}
        .favicon {{
            font-size: 24px;
            margin-right: 15px;
            flex-shrink: 0;
        }}
        .info {{
            flex-grow: 1;
            min-width: 0;
        }}
        .title {{
            color: #1a0dab;
            text-decoration: none;
            font-size: 16px;
            font-weight: 500;
            display: block;
            margin-bottom: 4px;
        }}
        .title:hover {{
            text-decoration: underline;
        }}
        .url {{
            color: #006621;
            font-size: 13px;
            margin-bottom: 4px;
            overflow: hidden;
            text-overflow: ellipsis;
            white-space: nowrap;
        }}
        .date {{
            color: #888;
            font-size: 12px;
        }}
        .empty {{
            text-align: center;
            color: #888;
            padding: 40px;
            font-size: 16px;
        }}
        .controls {{
            margin-bottom: 20px;
            display: flex;
            gap: 10px;
        }}
        .btn {{
            padding: 10px 20px;
            background: #667eea;
            color: white;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            text-decoration: none;
            display: inline-block;
        }}
        .btn:hover {{
            background: #5568d3;
        }}
        .btn-danger {{
            background: #e74c3c;
        }}
        .btn-danger:hover {{
            background: #c0392b;
        }}
    </style>
</head>
<body>
    <div class='container'>
        <h1>Historial de Navegación</h1>
        <div class='controls'>
            <button class='btn' onclick='location.reload()'>🔄 Actualizar</button>
        </div>
        {historyList}
    </div>
</body>
</html>";
        }

        private List<HistoryEntry> GetBrowserHistory()
        {
            var entries = new List<HistoryEntry>();

            try
            {
                // Ruta a la base de datos de historial de WebView2
                string historyDbPath = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "deneNavi.exe.WebView2",
                    "EBWebView",
                    "Default",
                    "History"
                );

                if (!File.Exists(historyDbPath))
                {
                    return entries;
                }

                // Crear una copia temporal para evitar conflictos de bloqueo
                string tempDbPath = Path.Combine(Path.GetTempPath(), $"history_temp_{Guid.NewGuid()}.db");
                File.Copy(historyDbPath, tempDbPath, true);

                try
                {
                    using (var connection = new SqliteConnection($"Data Source={tempDbPath};Mode=ReadOnly"))
                    {
                        connection.Open();

                        string query = @"
                            SELECT 
                                u.url,
                                u.title,
                                u.visit_count,
                                v.visit_time
                            FROM urls u
                            LEFT JOIN visits v ON u.id = v.url
                            WHERE u.hidden = 0
                            ORDER BY v.visit_time DESC
                            LIMIT 100";

                        using (var command = new SqliteCommand(query, connection))
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string url = reader.IsDBNull(0) ? "" : reader.GetString(0);
                                string title = reader.IsDBNull(1) ? url : reader.GetString(1);
                                int visitCount = reader.IsDBNull(2) ? 0 : reader.GetInt32(2);
                                long visitTime = reader.IsDBNull(3) ? 0 : reader.GetInt64(3);

                                // Convertir tiempo de Chrome (microsegundos desde 1601) a DateTime
                                DateTime lastVisit = DateTime.MinValue;
                                if (visitTime > 0)
                                {
                                    try
                                    {
                                        // Chrome usa tiempo en microsegundos desde el 1 de enero de 1601
                                        DateTime epoch = new DateTime(1601, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                                        lastVisit = epoch.AddTicks(visitTime * 10).ToLocalTime();
                                    }
                                    catch
                                    {
                                        lastVisit = DateTime.Now;
                                    }
                                }

                                if (!string.IsNullOrEmpty(url) && !url.StartsWith("dnnav:"))
                                {
                                    entries.Add(new HistoryEntry
                                    {
                                        Url = url,
                                        Title = string.IsNullOrEmpty(title) ? url : title,
                                        VisitCount = visitCount,
                                        LastVisit = lastVisit
                                    });
                                }
                            }
                        }
                    }
                }
                finally
                {
                    // Limpiar archivo temporal
                    try { File.Delete(tempDbPath); } catch { }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al leer historial: {ex.Message}");
            }

            return entries;
        }

        private string Generate404Page()
        {
            return GenerateSimplePage("Página no encontrada",
                "La URL especial que intentas acceder no existe en deneNavi.");
        }

        private string GenerateSimplePage(string title, string message)
        {
            return $@"
<!DOCTYPE html>
<html>
<head>
    <title>{title}</title>
    <style>
        body {{ 
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; 
            background: #f5f5f5;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
            margin: 0;
        }}
        .container {{
            text-align: center;
            background: white;
            padding: 50px;
            border-radius: 10px;
            box-shadow: 0 2px 10px rgba(0,0,0,0.1);
        }}
        h1 {{ color: #667eea; }}
    </style>
</head>
<body>
    <div class='container'>
        <h1>{title}</h1>
        <p>{message}</p>
    </div>
</body>
</html>";
        }

        private string FormatBytes(long bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB" };
            double len = bytes;
            int order = 0;
            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len = len / 1024;
            }
            return $"{len:0.##} {sizes[order]}";
        }

        private void CoreWebView2_DownloadStarting(object sender, CoreWebView2DownloadStartingEventArgs e)
        {
            e.ResultFilePath = Path.Combine(DOWNLOADS_PATH, Path.GetFileName(e.ResultFilePath));
            statusLabel.Text = $"Descargando: {Path.GetFileName(e.ResultFilePath)}";
            progressBar.Visible = true;

            e.DownloadOperation.BytesReceivedChanged += (s, ev) =>
            {
                var op = s as CoreWebView2DownloadOperation;
                if (op.TotalBytesToReceive.HasValue && op.TotalBytesToReceive > 0)
                {
                    int progress = (int)((double)op.BytesReceived / op.TotalBytesToReceive.Value * 100);
                    this.Invoke((MethodInvoker)delegate {
                        progressBar.Value = progress;
                    });
                }
            };

            e.DownloadOperation.StateChanged += (s, ev) =>
            {
                var op = s as CoreWebView2DownloadOperation;
                if (op.State == CoreWebView2DownloadState.Completed)
                {
                    this.Invoke((MethodInvoker)delegate {
                        statusLabel.Text = "Descarga completada";
                        progressBar.Visible = false;
                        progressBar.Value = 0;
                    });
                }
            };
        }

        private void CoreWebView2_NavigationStarting(object sender, CoreWebView2NavigationStartingEventArgs e, BrowserTab tab)
        {
            if (activeTab == tab)
            {
                statusLabel.Text = "Cargando...";
                progressBar.Visible = true;
            }
        }

        private void CoreWebView2_NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e, BrowserTab tab)
        {
            if (activeTab == tab)
            {
                statusLabel.Text = "Listo";
                progressBar.Visible = false;

                // Solo actualizar la URL si no es una URL especial dnnav:
                if (!tab.CurrentUrl.StartsWith("dnnav:", StringComparison.OrdinalIgnoreCase))
                {
                    tab.CurrentUrl = tab.WebView.Source.ToString();
                    urlTextBox.Text = tab.CurrentUrl;
                }

                UpdateNavigationButtons();
            }
        }

        private void CoreWebView2_DocumentTitleChanged(object sender, object e, BrowserTab tab)
        {
            string title = tab.WebView.CoreWebView2.DocumentTitle;

            // Truncar el título si es muy largo para la pestaña
            string tabTitle = string.IsNullOrEmpty(title) ? "Nueva pestaña" : title;
            if (tabTitle.Length > 25)
            {
                tabTitle = tabTitle.Substring(0, 22) + "...";
            }

            tab.TabButton.Text = tabTitle;

            // Actualizar el título de la ventana principal si esta es la pestaña activa
            if (activeTab == tab)
            {
                UpdateWindowTitle();
            }
        }

        private void UpdateNavigationButtons()
        {
            if (activeTab?.WebView?.CoreWebView2 != null)
            {
                backButton.Enabled = activeTab.WebView.CoreWebView2.CanGoBack;
                forwardButton.Enabled = activeTab.WebView.CoreWebView2.CanGoForward;
            }
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            if (activeTab?.WebView?.CoreWebView2?.CanGoBack == true)
                activeTab.WebView.CoreWebView2.GoBack();
        }

        private void ForwardButton_Click(object sender, EventArgs e)
        {
            if (activeTab?.WebView?.CoreWebView2?.CanGoForward == true)
                activeTab.WebView.CoreWebView2.GoForward();
        }

        private void ReloadButton_Click(object sender, EventArgs e)
        {
            activeTab?.WebView?.Reload();
        }

        private void HomeButton_Click(object sender, EventArgs e)
        {
            NavigateToUrl(HOME_URL);
        }

        private void GoButton_Click(object sender, EventArgs e)
        {
            NavigateToUrl(urlTextBox.Text);
        }

        private void UrlTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                NavigateToUrl(urlTextBox.Text);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void MenuButton_Click(object sender, EventArgs e)
        {
            var menu = new ContextMenuStrip();
            menu.Items.Add("Nueva pestaña", null, (s, ev) => CreateNewTab(HOME_URL));
            menu.Items.Add(new ToolStripSeparator());
            menu.Items.Add("Historial", null, (s, ev) => NavigateToUrl("dnnav:history"));
            menu.Items.Add("Descargas", null, (s, ev) => NavigateToUrl("dnnav:downloads"));
            menu.Items.Add("Contraseñas", null, (s, ev) => NavigateToUrl("dnnav:passwords"));
            menu.Items.Add(new ToolStripSeparator());
            menu.Items.Add("Acerca de deneNavi", null, (s, ev) => NavigateToUrl("dnnav:about"));
            menu.Items.Add(new ToolStripSeparator());
            menu.Items.Add("Salir", null, (s, ev) => Application.Exit());

            menu.Show(menuButton, new Point(0, menuButton.Height));
        }

        private void WebView_SourceChanged(object sender, CoreWebView2SourceChangedEventArgs e, BrowserTab tab)
        {
            // Mantener la URL personalizada si es una URL especial
            if (!tab.CurrentUrl.StartsWith("dnnav:", StringComparison.OrdinalIgnoreCase))
            {
                tab.CurrentUrl = tab.WebView.Source.ToString();
                if (activeTab == tab)
                {
                    this.Invoke((MethodInvoker)delegate {
                        urlTextBox.Text = tab.CurrentUrl;
                    });
                }
            }
        }
    }

    public class BrowserTab
    {
        public WebView2 WebView { get; set; }
        public Button TabButton { get; set; }
        public Button CloseButton { get; set; }
        public string CurrentUrl { get; set; }
    }

    public class HistoryEntry
    {
        public string Url { get; set; }
        public string Title { get; set; }
        public int VisitCount { get; set; }
        public DateTime LastVisit { get; set; }
    }
}