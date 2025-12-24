namespace WARun
{
    public partial class Form1 : Form
    {
        public Form1(string Title, string IconPath, string Url)
        {
            InitializeComponent();
            Text = Title;
            try
            {
                Icon = new Icon(IconPath);
            }
            catch
            {
                Console.WriteLine("No se pudo cargar el icono.");
            }

            InitializeWebView(Url);

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
