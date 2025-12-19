using System.Net;

namespace deneStore
{
    public partial class welcomeScreen : Form
    {
        public welcomeScreen()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                string path = Path.Combine("C:\\DENEOS", "core", "setConfig.exe");

                if (!File.Exists(path))
                {
                    using (WebClient client = new())
                    {
                        string url = "https://repoficialx.xyz/deneosinternal/setconfig/setConfig.exe";
                        await client.DownloadFileTaskAsync(new Uri(url), path);
                    }
                }

                System.Diagnostics.Process.Start(path, "swc=true");
            }

            Close();
        }

    }
}
