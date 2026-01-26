namespace WARun
{
    public partial class Form1 : Form
    {
        private readonly AppConfig _config;

        public Form1(AppConfig config)
        {
            InitializeComponent();
            _config = config;

            Text = _config.Title;
            try { Icon = new Icon(_config.IconPath); } catch { }

            InitializeWebView(_config.Url);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private async void InitializeWebView(string url)
        {
            await webView21.EnsureCoreWebView2Async();
            if (!url.StartsWith("http"))
                url = "https://" + url;

            webView21.CoreWebView2.Navigate(url);
        }
    }
}
