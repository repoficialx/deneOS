using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EugenPechanec.NativeWifi;

namespace Internet
{
    public partial class ExtendedWLANInfo : Form
    {
        public ExtendedWLANInfo()
        {
            InitializeComponent();
        }

        private void ExtendedWLANInfo_Load(object sender, EventArgs e)
        {
            UpdateInternetInfo();
        }

        void UpdateInternetInfo()
        {
            // Initialize the 'client' variable before using it.  
            EugenPechanec.NativeWifi.Wlan.WlanClient client = EugenPechanec.NativeWifi.Wlan.WlanClient.CreateClient();
            foreach (var @interface in client.Interfaces)
            {
                var network = @interface;

                if (network != null)
                {
                    var ssid = (network.CurrentConnection.AssociationAttributes.Ssid.Ssid).TrimEnd('\0');
                    this.ssid.Text = ssid;
                    band.Text = ObtenerBanda(network.Channel);
                    dspeed.Text = $"{network.CurrentConnection.AssociationAttributes.TxRate / 1000} Mbps";
                    uspeed.Text = $"{network.CurrentConnection.AssociationAttributes.RxRate / 1000} Mbps";
                    int signalQuality = (int)network.CurrentConnection.AssociationAttributes.LanSignalQuality;
                    strength.Text = $"{signalQuality}%";
                    rssi.Text = $"{network.Rssi} dBm";
                }
                else
                {
                    MessageBox.Show("No estás conectado a ninguna red Wi-Fi.");
                }
            }
        }

        static string ObtenerBanda(int channel)
        {
            if (channel >= 1 && channel <= 14) return "2.4 GHz";
            else if (channel >= 32 && channel <= 173) return "5 GHz";
            else if (channel >= 1 && channel <= 233) return "6 GHz";
            else return "Desconocida";
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UpdateInternetInfo();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //UpdateInternetInfo();
        }
    }
}
