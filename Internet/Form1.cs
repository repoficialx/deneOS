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
            debug("obtenerssid a ser llamado");
            label6.Text = Internet.ObtenerSSID();
            debug("obtenerssid devolvió: " + label6.Text);
            debug("obtenerversionwifi a ser llamado");
            label4.Text = Internet.ObtenerVersionWiFi(comboBox1);
            debug("obtenerversionwifi devolvió: " + label4.Text);
            debug("obtenerestadodatosmoviles a ser llamado");

            string band = Internet.getBand();
            label3.Text = Band2Icon(band);
            debug($"texto asignado a label3: {label3.Text}");
            debug($"getBand estado: {band}");
        }
        void debug(string texto)
        {
            Console.WriteLine("[DEBUG] " + texto);
        }

        string Band2Icon(string band)
        {
            switch (band.Trim().ToLowerInvariant()) // Quita espacios y estandariza mayúsculas
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
            // MÉTODO ANTIGUO (NETSH):
            /*
            Process process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "netsh",
                    Arguments = "mbn show interfaces",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };
            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            if (!output.Contains("Estado de la SIM"))
            {
                label5.Text = "Mobile Data: NOT SUPPORTED";
                label5.ForeColor = Color.Gray;
                label3.Text = "";
                return;
            }



            bool datosEncendidos = output.Contains("Estado de la conexión: Conectado");
            string tipoRed = output.Split('\n')
                                   .FirstOrDefault(line => line.Contains("Tipo de acceso de datos"))?
                                   .Split(':')[1]
                                   .Trim();

            if (!datosEncendidos)
            {
                label5.Text = "Mobile Data: OFF";
                label3.Text = "";
            }
            else
            {
                label5.Text = "Mobile Data: ON";
                switch (tipoRed)
                {
                    case "HSDPA":
                        label3.Text = "h_plus_mobiledata";
                        break;
                    case "4G":
                        label3.Text = "4g_mobiledata";
                        break;
                    case "4G+":
                        label3.Text = "4g_plus_mobiledata";
                        break;






                    case "LTE":
                        label3.Text = "lte_mobiledata";
                        break;
                    case "LTE+":
                        label3.Text = "lte_plus_mobiledata";
                        break;
                    case "5G":
                        label3.Text = "5g";
                        break;
                    default:
                        label3.Text = "Unknown Network Type";
                        break;
                }
            }*/
            // MÉTODO NUEVO (Windows.Networking.Connectivity):
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
                            label3.Text = "h_plus_mobiledata";
                            break;
                        case "4g":
                            label3.Text = "4g_mobiledata";
                            break;
                        case "4gplus":
                            label3.Text = "4g_plus_mobiledata";
                            break;
                        case "lte":
                            label3.Text = "lte_mobiledata";
                            break;
                        case "lteplus":
                            label3.Text = "lte_plus_mobiledata";
                            break;
                        case "5g":
                            label3.Text = "5g";
                            break;
                        default:
                            label3.Text = "Unknown Network Type";
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
                debug("obtenerestadodatosmoviles a ser llamado");
                ObtenerEstadoDatosMoviles();
                debug("obtenerestadodatosmoviles devolvió: " + label5.Text);
            });

        }

        private void button3_Click(object sender, EventArgs e)
        {
            label6.Text = Internet.ObtenerSSID();
            label4.Text = Internet.ObtenerVersionWiFi(comboBox1);
            string band = Internet.getBand();
            label3.Text = Band2Icon(band);
        }
    }
}