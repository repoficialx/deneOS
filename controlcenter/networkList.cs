using ManagedNativeWifi;
using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace Internet
{
    public partial class networkList : Form
    {
        class WifiNetworkInfo
        {
            public string Ssid { get; set; }
            public string WifiVersion { get; set; }
            public int Signal { get; set; } // de 0 a 100

            public override string ToString() => $"{Ssid} - {WifiVersion} ({Signal}%)";
        }

        public class ComboBoxItem
        {
            public string Text { get; set; }
            public string Tag { get; set; }

            public override string ToString()
            {
                return Text; // This is what will be displayed in the ComboBox
            }
        }
        enum WifiSecurityType
        {
            Open,
            WEP,
            WPA1,
            WPA2,
            WPA3,
            Unknown
        }

        WifiSecurityType DetectSecurity(AvailableNetworkInfo bss)
        {
            switch (bss.AuthenticationAlgorithm)
            {
                case AuthenticationAlgorithm.Open:
                    return WifiSecurityType.Open;

                case AuthenticationAlgorithm.Shared:
                    return WifiSecurityType.WEP;

                case AuthenticationAlgorithm.WPA:
                case AuthenticationAlgorithm.WPA_PSK:
                    return WifiSecurityType.WPA1;

                case AuthenticationAlgorithm.RSNA:
                case AuthenticationAlgorithm.RSNA_PSK:
                    return WifiSecurityType.WPA2;

                case AuthenticationAlgorithm.WPA3_ENT:
                case AuthenticationAlgorithm.WPA3_ENT_192:
                case AuthenticationAlgorithm.WPA3_SAE:
                case AuthenticationAlgorithm.OWE:
                    return WifiSecurityType.WPA3;

                default:
                    return WifiSecurityType.Unknown;
            }
        }


        public networkList()
        {
            InitializeComponent();
        }

        private void networkList_Load(object sender, EventArgs e)
        {
            LoadNetworks();
        }

        private void ButtonConnect_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                var item = (ComboBoxItem)comboBox1.SelectedItem;
                string ssid = item.Tag;
                ConectarWiFi(ssid);
            }
        }

        /*private void ConectarWiFi(string ssid)
        {/*
            var iface = NativeWifi.EnumerateInterfaces().FirstOrDefault();
            if (iface == null)
            {
                MessageBox.Show("No se encontró una interfaz Wi-Fi.");
                return;
            }

            foreach (var i_face in NativeWifi.EnumerateInterfaces())
            {
                Console.WriteLine($"{i_face.Id} - {i_face.Description}");
            }

            // Obtener red seleccionada en base al SSID
            var networks = NativeWifi.EnumerateAvailableNetworks(iface.Id);
            var selected = networks.list.FirstOrDefault(n => n.Ssid.ToString() == ssid);

            //MessageBox.Show("WiFi seleccionado: " + ssid);

            if (selected == null)
            {
                MessageBox.Show("No se encontró la red seleccionada.");
                return;
            }

            var bssEntries = NativeWifi.EnumerateBssNetworks(iface.Id);
            var bss = bssEntries.list.FirstOrDefault(b => b.Ssid.ToString() == ssid);

            if (bss == null)
            {
                MessageBox.Show("No se pudo obtener información de la red.");
                return;
            }

            WifiSecurityType security = DetectSecurity(selected);

            string password = "";
            if (security != WifiSecurityType.Open)
            {
                input dialog = new input { ssid = ssid };
                dialog.ShowDialog();
                password = dialog.contraseña;
            }
            ssid = ssid.Trim();
            // Generar perfil adecuado
            string xml = GenerateWifiProfileXml(ssid, password, security);
            Console.WriteLine("=== XML GENERADO ===");
            Console.WriteLine(xml);
            Console.WriteLine("===================");


            //NativeWifi.DeleteProfile(iface.Id, ssid);


            // Guardar perfil
            bool saved = NativeWifi.SetProfile(iface.Id, ProfileType.AllUser, xml, security.ToString(), true);
            if (!saved)
            {
                MessageBox.Show("Error al guardar el perfil Wi-Fi.");
                return;
            }

            // Conectar
            bool ok = NativeWifi.ConnectNetwork(
                iface.Id,
                ssid,
                BssType.Infrastructure
            );

            if (ok)
                MessageBox.Show($"Conectando a {ssid} ({security})...");
            else
                MessageBox.Show($"No se pudo conectar a {ssid}");*

            // 1️⃣ Generar la contraseña y seguridad

            var iface = NativeWifi.EnumerateInterfaces().FirstOrDefault();

            input input1 = new input();
            input1.ssid = ssid;
            input1.ShowDialog();
            string password = input1.contraseña;

            var networks = NativeWifi.EnumerateAvailableNetworks(iface.Id);
            var selected = networks.list.FirstOrDefault(n => n.Ssid.ToString() == ssid);

            WifiSecurityType security = DetectSecurity(selected); // tu método que devuelve WPA2/WPA3/Open

            // 2️⃣ Generar XML del perfil (solo para netsh)
            string xml = GenerateWifiProfileXml(ssid, password, security);
            string path = Path.Combine(Path.GetTempPath(), "perfil.xml");
            File.WriteAllText(path, xml);

            // 3️⃣ Crear el perfil usando netsh (evita los fails de MNW)
            var psi = new ProcessStartInfo("netsh", $"wlan add profile filename=\"{path}\"")
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            Process process = Process.Start(psi);
            process.WaitForExit();
            File.Delete(path);

            MessageBox.Show($"Perfil creado para {ssid}");

            bool connected = NativeWifi.ConnectNetwork(iface.Id, ssid, BssType.Infrastructure);

            if (connected)
                MessageBox.Show($"Conectado a {ssid} correctamente ✅");
            else
                MessageBox.Show($"Error al conectar a {ssid} ❌");
        }*/private void ConectarWiFi(string ssid)
           {
               if (string.IsNullOrWhiteSpace(ssid))
               {
                   MessageBox.Show("SSID inválido.");
                   return;
               }
           
               var iface = NativeWifi.EnumerateInterfaces().FirstOrDefault();
               if (iface == null)
               {
                   MessageBox.Show("No se encontró interfaz Wi-Fi.");
                   return;
               }
           
               var networks = NativeWifi.EnumerateAvailableNetworks(iface.Id);
               var selected = networks.list.FirstOrDefault(n => n.Ssid.ToString() == ssid);
           
               if (selected == null)
               {
                   MessageBox.Show("Red no encontrada.");
                   return;
               }
           
               var security = DetectSecurity(selected);
               string password = "";
           
               if (security != WifiSecurityType.Open)
               {
                   using var dialog = new input { ssid = ssid };
                   dialog.ShowDialog();
           
                   if (string.IsNullOrEmpty(dialog.contraseña))
                   {
                       MessageBox.Show("Contraseña requerida.");
                       return;
                   }
           
                   password = dialog.contraseña;
               }
           
               string xml = GenerateWifiProfileXml(ssid, password, security);
               string path = Path.GetTempFileName();
               File.WriteAllText(path, xml);
           
               var psi = new ProcessStartInfo("netsh", $"wlan add profile filename=\"{path}\"")
               {
                   UseShellExecute = false,
                   CreateNoWindow = true
               };
           
               Process.Start(psi)?.WaitForExit();
               File.Delete(path);
           
               bool connected = NativeWifi.ConnectNetwork(iface.Id, ssid, BssType.Infrastructure);
           
               MessageBox.Show(
                   connected
                   ? $"Conectado a {ssid} ✅"
                   : $"No se pudo conectar a {ssid} ❌"
               );
           }
           




        string GenerateWifiProfileXml(string ssid, string password, WifiSecurityType security)
        {
            switch (security)
            {
                // -------------------------------
                // OPEN NETWORK (sin contraseña)
                // -------------------------------
                case WifiSecurityType.Open:
                    return $@"
<WLANProfile xmlns='http://www.microsoft.com/networking/WLAN/profile/v1'>
    <name>{ssid}</name>
    <SSIDConfig>
        <SSID>
            <name>{ssid}</name>
        </SSID>
    </SSIDConfig>
    <connectionType>ESS</connectionType>
    <connectionMode>manual</connectionMode>
</WLANProfile>";

                // -------------------------------
                // WPA3-Personal (SAE)
                // -------------------------------
                case WifiSecurityType.WPA3:
                    return $@"
<WLANProfile xmlns='http://www.microsoft.com/networking/WLAN/profile/v1'>
    <name>{ssid}</name>
    <SSIDConfig>
        <SSID>
            <name>{ssid}</name>
        </SSID>
    </SSIDConfig>
    <connectionType>ESS</connectionType>
    <connectionMode>manual</connectionMode>
    <MSM>
        <security>
            <authEncryption>
                <authentication>WPA3SAE</authentication>
                <encryption>AES</encryption>
                <useOneX>false</useOneX>
            </authEncryption>
            <sharedKey>
                <keyType>passPhrase</keyType>
                <protected>false</protected>
                <keyMaterial>{password}</keyMaterial>
            </sharedKey>
            <PMF>
                <capable>true</capable>
                <required>true</required>
            </PMF>
        </security>
    </MSM>
</WLANProfile>";

                // -------------------------------
                // WPA2-Personal (PSK)
                // -------------------------------
                case WifiSecurityType.WPA2:
                default:
                    return $@"
<WLANProfile xmlns='http://www.microsoft.com/networking/WLAN/profile/v1'>
    <name>{ssid}</name>
    <SSIDConfig>
        <SSID>
            <name>{ssid}</name>
        </SSID>
    </SSIDConfig>
    <connectionType>ESS</connectionType>
    <connectionMode>manual</connectionMode>
    <MSM>
        <security>
            <authEncryption>
                <authentication>WPA2PSK</authentication>
                <encryption>AES</encryption>
                <useOneX>false</useOneX>
            </authEncryption>
            <sharedKey>
                <keyType>passPhrase</keyType>
                <protected>false</protected>
                <keyMaterial>{password}</keyMaterial>
            </sharedKey>
        </security>
    </MSM>
</WLANProfile>";
            }
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ButtonConnect_Click(sender, e);
        }


        private void LoadNetworks()
        {
            try
            {
                comboBox1.Items.Clear();

                // Enumerar todas las interfaces WiFi
                var interfaces = NativeWifi.EnumerateInterfaces();

                foreach (var iface in interfaces)
                {
                    // Enumerar redes disponibles en cada interfaz
                    var networks = NativeWifi.EnumerateAvailableNetworks(iface.Id);

                    /*foreach (var net in networks.list)
                    {
                        string ssid = net.Ssid.ToString();
                        if (string.IsNullOrWhiteSpace(ssid))
                            continue;

                        var bssEntries = NativeWifi.EnumerateBssNetworks(iface.Id);
                        var bss = bssEntries.list.FirstOrDefault(b => b.Ssid.ToString() == ssid);

                        if (bss == null)
                        {
                            comboBox1.Items.Add(ssid);
                            continue;
                        }

                        var band = bss.Band;
                        var phy = bss.PhyType;

                        string wifiVersion = WifiVersionFromPhyAndBand(phy, band);

                        // Ejemplo: "MiWifi - Wi-Fi 6E (85%)"
                        comboBox1.Items.Add(new ComboBoxItem
                        {
                            Text = $"{ssid} - {wifiVersion} ({net.SignalQuality}%)",
                            Tag = ssid
                        });

                    }*/
                    List<WifiNetworkInfo> redes = new List<WifiNetworkInfo>();

                    foreach (var net in networks.list   ) // lista de redes detectadas
                    {
                        string ssid = net.Ssid.ToString();
                        if (string.IsNullOrWhiteSpace(ssid))
                            continue;

                        var bssEntries = NativeWifi.EnumerateBssNetworks(iface.Id);
                        var bss = bssEntries.list.FirstOrDefault(b => b.Ssid.ToString() == ssid);

                        if (bss == null)
                        {
                            //comboBox1.Items.Add(ssid);
                            continue;
                        }

                        var band = bss.Band;
                        var phy = bss.PhyType;

                        string wifiVersion = WifiVersionFromPhyAndBand(phy, band);

                        redes.Add(new WifiNetworkInfo
                        {
                            Ssid = ssid,
                            WifiVersion = wifiVersion, // Wi-Fi 4/5/6
                            Signal = net.SignalQuality
                        });
                    }

                    redes = redes
                        .GroupBy(r => r.Ssid)
                        .Select(g => g.OrderByDescending(r => r.Signal).First())
                        .ToList();

                    // Ordenar de más señal a menos
                    redes = redes.OrderByDescending(r => r.Signal).ToList();

                    // Añadir al ComboBox
                    comboBox1.Items.Clear();
                    foreach (var r in redes)
                        comboBox1.Items.Add(r);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar redes Wi-Fi: {ex.Message}");
            }
        }

        string WifiVersionFromPhyAndBand(PhyType phy, float band)
        {
            if (phy == PhyType.He) // 802.11ax
            {
                return band switch
                {
                    6.0F => "Wi-Fi 6E",
                    5.0F => "Wi-Fi 6",
                    2.4F => "Wi-Fi 6",
                    _ => "Wi-Fi 6"
                };
            }

            return phy switch
            {
                PhyType.Eht => "Wi-Fi 7",
                PhyType.Vht => "Wi-Fi 5",
                PhyType.Ht => "Wi-Fi 4",
                PhyType.Ofdm => "802.11b",
                PhyType.Erp => "802.11g",
                _ => "Desconocido"
            };
        }


        [StructLayout(LayoutKind.Sequential)]
        internal struct WlanAvailableNetwork
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] Ssid;
            // Otros campos relevantes pueden ser añadidos aquí si es necesario  
        }
    }
}
