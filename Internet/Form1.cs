using System.Diagnostics;
using Windows.Networking.Connectivity;
using System.Management;
using System.Threading.Tasks;

namespace Internet
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var isEthernet = Internet.ObtenerEsEthernet();
            if (isEthernet)
            {
                if (Internet.isHotspot())
                {
                    label6.Text = @"Mobile ";
                    debug("Internet.isHotspot() = true");
                    label6.Text += Internet.ObtenerSSID();
                    debug("Internet.ObtenerSSID() = " + label6.Text);
                    label4.Text = Internet.ObtenerVersionWiFi(comboBox1);
                    debug("Internet.ObtenerVersionWiFi() = " + label4.Text);
                    string band_ = Internet.getBand();
                    label3.Text = Band2Icon(band_);
                    debug($"Band2Icon(Internet.getBand()) = {label3.Text}");
                    debug($"Internet.getBand() = {band_}");
                    label2.Text = @"";
                    return;
                }
                label6.Text = @"Ethernet";
                debug("Internet.ObtenerEsEthernet() = " + isEthernet);
                label4.Text = "";
                label3.Text = "";
                button2.Visible = false;
                button3.Visible = false;
                label2.Text = @"";
                return;
            }

            if (Internet.isHotspot())
            {
                label6.Text = @"Mobile ";
                debug("Internet.isHotspot() = true");
                label6.Text += Internet.ObtenerSSID();
                debug("Internet.ObtenerSSID() = " + label6.Text);
                label4.Text = Internet.ObtenerVersionWiFi(comboBox1);
                debug("Internet.ObtenerVersionWiFi() = " + label4.Text);
                string band_ = Internet.getBand();
                label3.Text = Band2Icon(band_);
                debug($"Band2Icon(Internet.getBand()) = {label3.Text}");
                debug($"Internet.getBand() = {band_}");
                label2.Text = @"";
                return;
            }

            label6.Text = Internet.ObtenerSSID();
            debug("Internet.ObtenerSSID() = " + label6.Text);
            label4.Text = Internet.ObtenerVersionWiFi(comboBox1);
            debug("Internet.ObtenerVersionWiFi() = " + label4.Text);
            string band = Internet.getBand();
            label3.Text = Band2Icon(band);
            debug($"Band2Icon(Internet.getBand()) = {label3.Text}");
            debug($"Internet.getBand() = {band}");
        }
        void debug(string texto)
        {
#if DEBUG
            Console.WriteLine("[DEBUG] " + texto);
#endif
        }

        string Band2Icon(string band)
        {
            switch (band.Trim().ToLower())
            {
                case "2,4 ghz":
                    return "filter_4";
                case "5 ghz":
                    return "5g";
                case "6 ghz":
                    return "filter_6";
                default:
                    return band;
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private async void ObtenerEstadoDatosMoviles()
        {
            var profiles = NetworkInformation.GetConnectionProfiles();
            var cellular = profiles.FirstOrDefault(p => p.IsWwanConnectionProfile);
            if (cellular == null)
            {
                Thread.Sleep(500);

                this.Invoke(() =>
                {
                    label5.Text = "Mobile Data: NOT SUPPORTED";
                    label5.ForeColor = Color.Gray;
                    label3.Text = "";

                });
                return;
            }
            bool datosEncendidos = cellular.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess;
            string tipoRed = cellular.WwanConnectionProfileDetails.GetCurrentDataClass().ToString();
            if (!datosEncendidos)
            {
                Thread.Sleep(500);

                this.Invoke(() =>
                {
                    label5.Text = "Mobile Data: OFF";
                    label3.Text = "";
                });
            }
            else
            {
                Thread.Sleep(500);

                this.Invoke(() =>
                {
                    label5.Text = "Mobile Data: ON";
                    switch (tipoRed.ToLower())
                    {
                        case "hsdpa":
                            label3.Text = @"h_plus_mobiledata";
                            break;
                        case "4g":
                            label3.Text = @"4g_mobiledata";
                            break;
                        case "4gplus":
                            label3.Text = @"4g_plus_mobiledata";
                            break;
                        case "lte":
                            label3.Text = @"lte_mobiledata";
                            break;
                        case "lteplus":
                            label3.Text = @"lte_plus_mobiledata";
                            break;
                        case "5g":
                            label3.Text = @"5g";
                            break;
                        default:
                            label3.Text = @"Unknown Network Type";
                            break;
                    }
                });
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            new networkList().ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new ExtendedWLANInfo().ShowDialog();
        }

        private async void Form1_Shown(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                ObtenerEstadoDatosMoviles();
                debug("ObtenerEstadoDatosMoviles() = " + label5.Text);
            });

        }

        private void button3_Click(object sender, EventArgs e)
        {
            var isEthernet = Internet.ObtenerEsEthernet();
            if (isEthernet)
            {
                label6.Text = @"Ethernet";
                debug("Internet.ObtenerEsEthernet() = " + label6.Text);
                label4.Text = "";
                label3.Text = "";
                button2.Visible = false;
                button3.Visible = false;
                label2.Text = @"";
                return;
            }

            label6.Text = Internet.ObtenerSSID();
            label4.Text = Internet.ObtenerVersionWiFi(comboBox1);
            string band = Internet.getBand();
            label3.Text = Band2Icon(band);
        }
    }
}