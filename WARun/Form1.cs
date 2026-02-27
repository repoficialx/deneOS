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

            try{
                if (!string.IsNullOrEmpty(_config.IconPath)) 
                    Icon = new Icon(_config.IconPath);}
            catch
            {
                // ignored
            }

            InitializeWebView(_config.Url);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private async void InitializeWebView(string url)
        {
            await webView21.EnsureCoreWebView2Async();

            if (string.IsNullOrEmpty(url))
                url = "https://www.example.com";
            if (!url.StartsWith("http"))
                url = "https://" + url;

            

            const string watermark = """
                               (() => {
                                   const id = 'warun-watermark';
                                   if (document.getElementById(id)) return;

                                   const create = () => {
                                       if (!document.body) return false;
                                       const div = document.createElement('div');
                                       div.id = id;
                                       div.textContent = 'Powered by WARun';
                                       div.style.cssText = 'position:fixed;bottom:5px;left:5px;font-size:12px;opacity:0.6;z-index:2147483647;pointer-events:none;';
                                       const isDark = window.matchMedia && window.matchMedia('(prefers-color-scheme: dark)').matches;
                                       div.style.color = isDark ? 'rgba(255,255,255,0.6)' : 'rgba(0,0,0,0.6)';
                                   document.body.appendChild(div);
                                       return true;
                                   };

                                   if (!create()) {
                                       const onReady = () => { if (create()) { window.removeEventListener('load', onReady); document.removeEventListener('DOMContentLoaded', onReady); } };
                                       window.addEventListener('load', onReady);
                                       document.addEventListener('DOMContentLoaded', onReady);

                                       const obs = new MutationObserver(() => { if (create()) obs.disconnect(); });
                                       try { obs.observe(document, { childList: true, subtree: true }); } catch (e) { /* ignore */ }
                                   }
                               })();
                               """;

            await webView21.CoreWebView2.AddScriptToExecuteOnDocumentCreatedAsync(watermark);
            
            webView21.CoreWebView2.Navigate(url);
        }
    }
}
