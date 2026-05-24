using System.Diagnostics;
using Windows.Networking.Connectivity;
using System.Management;
using System.Threading.Tasks;

namespace Internet
{
    public partial class Form1 : Form
    {
        public Form1(IMobileDataService mobile)
        {
            InitializeComponent();
            this.mobile = mobile;
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

        /*private async void ObtenerEstadoDatosMoviles()
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

        }*/
        #region -- DEV -- TESTS -- SIMULAR DATOS MÓVILES --
        private void ObtenerEstadoDatosMoviles()
        {
            // Obtener estado en hilo actual (puede ser fondo)
            bool supported = mobile.IsSupported;
            bool connected = mobile.IsConnected;
            string statusText;
            Color statusColor = Color.Black;
            string iconText = "";

            if (!supported)
            {
                statusText = "Mobile Data: NOT SUPPORTED";
                statusColor = Color.Gray;
                iconText = "";
            }
            else if (!connected)
            {
                statusText = "Mobile Data: OFF";
                iconText = "";
            }
            else
            {
                statusText = "Mobile Data: ON";
                switch (mobile.NetworkType?.ToLower())
                {
                    case "hsdpa": iconText = "h_plus_mobiledata"; break;
                    case "4g":    iconText = "4g_mobiledata"; break;
                    case "4gplus":iconText = "4g_plus_mobiledata"; break;
                    case "lte":   iconText = "lte_mobiledata"; break;
                    case "lteplus":iconText = "lte_plus_mobiledata"; break;
                    case "5g":    iconText = "5g"; break;
                    default:      iconText = "Unknown Network Type"; break;
                }
            }

            // Actualizar UI de forma segura
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)(() =>
                {
                    label5.Text = statusText;
                    label5.ForeColor = statusColor;
                    label3.Text = iconText;
                }));
            }
            else
            {
                label5.Text = statusText;
                label5.ForeColor = statusColor;
                label3.Text = iconText;
            }
        }

        public interface IMobileDataService
        {
            bool IsSupported { get; }
            bool IsConnected { get; }
            string NetworkType { get; }
        }

        public class RealMobileDataService : IMobileDataService
        {
            public bool IsSupported
            {
                get
                {
                    var profiles = NetworkInformation.GetConnectionProfiles();
                    return profiles.Any(p => p.IsWwanConnectionProfile);
                }
            }

            public bool IsConnected
            {
                get
                {
                    var profile = NetworkInformation.GetConnectionProfiles()
                        .FirstOrDefault(p => p.IsWwanConnectionProfile);

                    if (profile == null) return false;

                    return profile.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess;
                }
            }

            public string NetworkType
            {
                get
                {
                    var profile = NetworkInformation.GetConnectionProfiles()
                        .FirstOrDefault(p => p.IsWwanConnectionProfile);

                    if (profile == null) return "";

                    return profile.WwanConnectionProfileDetails.GetCurrentDataClass().ToString();
                }
            }
        }
        public class FakeMobileDataService : IMobileDataService
        {
            public bool IsSupported { get; set; }
            public bool IsConnected { get; set; }
            public string NetworkType { get; set; }
        }
        private readonly IMobileDataService mobile;

        #endregion
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