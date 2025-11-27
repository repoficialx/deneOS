namespace WARun
{
    public partial class Form1 : Form
    {
        public Form1(string Title, string IconPath, string Url)
        {
            InitializeComponent();
            Text = Title;
            Icon = new Icon(IconPath);
            webView21.CoreWebView2.Navigate(Url);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
