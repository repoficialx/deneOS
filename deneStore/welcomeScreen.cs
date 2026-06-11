using System.Net;
using System.Net.NetworkInformation;

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

        private async void welcomeScreen_Load(object sender, EventArgs e)
        {
            var rxhost = "https://repoficialx-cdn.vercel.app/";
            var logoUrl = rxhost+"desktopassets/deneos/deneOS_Logo.png";
            var internetTask = await InternetChecker.HasInternetAccessAsync(testUrl: logoUrl);
            if (internetTask)
            {
                pictureBox2.Load(logoUrl);
            }
            
        }
    }
}

public class InternetChecker
{
    /// <summary>
    /// Checks if the machine has actual internet access by making a small HTTP request.
    /// </summary>
    /// <param name="timeoutMs">Timeout in milliseconds.</param>
    /// <param name="testUrl">URL to test connectivity.</param>
    public static async Task<bool> HasInternetAccessAsync(int timeoutMs = 5000, string testUrl = "http://www.gstatic.com/generate_204")
    {
        try
        {
            // First, check if any network interface is up
            if (!NetworkInterface.GetIsNetworkAvailable())
                return false;

            var request = (HttpWebRequest)WebRequest.Create(testUrl);
            request.Method = "GET";
            request.KeepAlive = false;
            request.Timeout = timeoutMs;

            using (var response = (HttpWebResponse)await request.GetResponseAsync())
            {
                // HTTP 204 means "No Content" — used by Google to test connectivity
                return response.StatusCode == HttpStatusCode.NoContent || response.StatusCode == HttpStatusCode.OK;
            }
        }
        catch
        {
            return false; // Any exception means no internet access
        }
    }
}