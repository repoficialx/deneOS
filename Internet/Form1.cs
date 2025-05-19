using System.Diagnostics;
using System.Management;

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
            ObtenerSSID();
            ObtenerVersionWiFi();
            ObtenerEstadoDatosMoviles();
        }

        private void ObtenerSSID()
        {
            Process process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "netsh",
                    Arguments = "wlan show interfaces",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
#pragma warning disable CS8600 // Se va a convertir un literal nulo o un posible valor nulo en un tipo que no acepta valores NULL
            // Extraer el SSID de la salida
            string ssid = output.Split('\n')
                                .FirstOrDefault(line => line.Contains("SSID") && !line.Contains("BSSID"))?
                                .Split(':')[1]
                                .Trim();

            label6.Text = "WiFi: " + (ssid ?? "No conectado");
        }
        private void ObtenerVersionWiFi()
        {
            Process process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "netsh",
                    Arguments = "wlan show interfaces",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            // Extraer la versión WiFi

            string wifiVersion = output.Split('\n')
                                      .FirstOrDefault(line => line.Contains("Tipo de radio"))?
                                      .Split(':')[1]
                                      .Trim();
            switch (wifiVersion)
            {
                case "802.11be":
                    label4.Text = "looks_7";
                    break;
                case "802.11ax":
                    label4.Text = "looks_6";
                    break;
                case "802.11ac":
                    label4.Text = "looks_5";
                    break;
                case "802.11n":
                    label4.Text = "looks_4";
                    break;
                case "802.11g":
                    label4.Text = "looks_3";
                    break;
                case "802.11b":
                    label4.Text = "looks_2";
                    break;
                case "802.11a":
                    label4.Text = "looks_1";
                    break;
                default:
                    label4.Text = "unknown";
                    break;
            }
            comboBox1.Text = wifiVersion;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void ObtenerEstadoDatosMoviles()
        {
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
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}