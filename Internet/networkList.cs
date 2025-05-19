using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Internet
{
    public partial class networkList : Form
    {
        public networkList()
        {
            InitializeComponent();
        }

        private void networkList_Load(object sender, EventArgs e)
        {
            ObtenerRedesWiFi();
        }
        private void ObtenerRedesWiFi()
        {
            Process process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "netsh",
                    Arguments = "wlan show networks",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            var redes = output.Split('\n')
                              .Where(line => line.Contains("SSID"))
                              .Select(line => line.Split(':')[1].Trim())
                              .ToArray();

            comboBox1.Items.AddRange(redes);
        }
        private void ButtonConnect_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                ConectarWiFi(comboBox1.SelectedItem.ToString()!);
            }
        }
        private void ConectarWiFi(string ssid)
        {
            input input1 = new input();
            input1.ssid = ssid;
            input1.ShowDialog();
            CrearPerfil(ssid, input1.contraseña);
            Process process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "netsh",
                    Arguments = $"wlan connect name=\"{ssid}\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process.Start();
            process.WaitForExit();
            MessageBox.Show($"Intentando conectar a {ssid}");
        }

        void CrearPerfil(string ssid, string contraseña)
        {
            string password = contraseña;

            string xmlProfile = $@"
<WLANProfile xmlns='http://www.microsoft.com/networking/WLAN/profile/v1'>
    <name>{ssid}</name>
    <SSIDConfig>
        <SSID>
            <name>{ssid}</name>
        </SSID>
    </SSIDConfig>
    <connectionType>ESS</connectionType>
    <authentication>WPA2PSK</authentication>
    <encryption>AES</encryption>
    <MSM>
        <security>
            <sharedKey>
                <keyType>passPhrase</keyType>
                <protected>false</protected>
                <keyMaterial>{password}</keyMaterial>
            </sharedKey>
        </security>
    </MSM>
</WLANProfile>";

            System.IO.File.WriteAllText("C:\\perfil.xml", xmlProfile);
            Process.Start("netsh", "wlan add profile filename=\"C:\\perfil.xml\"");
            Process.Start("netsh", "wlan connect name=\"" + ssid + "\"");
            File.Delete("C:\\perfil.xml");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ButtonConnect_Click(sender, e);
        }
    }
}
