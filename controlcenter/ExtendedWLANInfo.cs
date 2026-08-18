using ManagedNativeWifi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            // Obtener la interfaz Wi-Fi actualmente conectada
            var connectedInterface = NativeWifi.EnumerateInterfaces()
                                                .FirstOrDefault(i => i.State == InterfaceState.Connected);

            if (connectedInterface != null)
            {
                // Info de la red actual
                var network = NativeWifi.GetCurrentConnection(connectedInterface.Id);

                if (!network.Equals(null))
                {
                    // SSID
                    var ssid = network.value.Ssid.ToString();
                    this.ssid.Text = ssid;

                    // Velocidad Tx/Rx aproximada (Mbps)
                    dspeed.Text = $"{network.value.TxRate / 1000} Mbps";
                    uspeed.Text = $"{network.value.RxRate / 1000} Mbps";

                    // Calidad de señal
                    strength.Text = $"{network.value.SignalQuality}%";

                    var bssEntries = NativeWifi.EnumerateBssNetworks(connectedInterface.Id);
                    var bss = bssEntries.list.FirstOrDefault(b => b.Ssid.ToString() == ssid);

                    if (bss != null)
                    {
                        // Banda (2.4GHz / 5GHz / 6GHz)
                        band.Text = ObtenerBanda(bss.Channel);
                        // RSSI en dBm
                        rssi.Text = $"{bss.Rssi} dBm";
                    }
                    else
                    {
                        rssi.Text = "N/A";
                        band.Text = "N/A";
                    }
                }
                else
                {
                    MessageBox.Show("No se pudo obtener información de la red actual.");
                    Close();
                }
            }
            else
            {
                MessageBox.Show("No estás conectado a ninguna red Wi-Fi.");
                Close();
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
