using ManagedNativeWifi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.Networking.Connectivity;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace Internet
{
    internal class Internet
    {
        private static bool IsHumanSsid(string ssid)
        {
            if (string.IsNullOrWhiteSpace(ssid))
                return false;

            string[] keywords =
            {
                "iphone", "android", "redmi", "samsung", "pixel",
                "oneplus", "mi", "galaxy"
            };

            return keywords.Any(k =>
                ssid.ToLower().Contains(k) ||
                ssid.Contains(" de ") ||
                ssid.Contains("'s ")
            );
        }
        private static bool IsMobileGateway()
        {
            try
            {
                foreach (var ni in NetworkInterface.GetAllNetworkInterfaces())
                {
                    if (ni.OperationalStatus != OperationalStatus.Up)
                        continue;

                    var props = ni.GetIPProperties();
                    foreach (var gw in props.GatewayAddresses)
                    {
                        var ip = gw.Address?.ToString();
                        if (ip == null) continue;

                        if (ip.StartsWith("192.168.43.") || // Android
                            ip == "172.20.10.1" ||          // iOS
                            ip.StartsWith("10.42.0."))      // Linux hotspot
                            return true;
                    }
                }
            }
            catch { }

            return false;
        }
        private static bool IsMobileOUI()
        {
            try
            {
                foreach (var ni in NetworkInterface.GetAllNetworkInterfaces())
                {
                    if (ni.OperationalStatus != OperationalStatus.Up)
                        continue;

                    var mac = ni.GetPhysicalAddress()?.ToString();
                    if (string.IsNullOrEmpty(mac) || mac.Length < 6)
                        continue;

                    string oui = mac.Substring(0, 6).ToUpper();

                    string[] mobileOUIs =
                    {
                        "0016CB", // Apple
                        "F4F5E8", // Samsung
                        "7C49EB", // Xiaomi
                        "A4C138", // Google
                    };

                    if (mobileOUIs.Contains(oui))
                        return true;
                }
            }
            catch { }

            return false;
        }
        public static string GetConnectionDescription()
        {
            var profile = NetworkInformation.GetInternetConnectionProfile();
            if (profile == null || profile.GetNetworkConnectivityLevel() != NetworkConnectivityLevel.InternetAccess)
                return "Sin conexión";

            _ = int.TryParse(profile.NetworkAdapter?.IanaInterfaceType.ToString() ?? "-1", out int iana);

            // 6 = Ethernet, 71 = Wi-Fi
            if (iana == 6)
                return "Ethernet";

            if (iana != 71)
                return "Conectado";

            bool metered = profile.GetConnectionCost().NetworkCostType != NetworkCostType.Unrestricted;
            string ssid = profile.WlanConnectionProfileDetails?.GetConnectedSsid() ?? "";

            int score = 0;

            if (metered) score++;

            if (IsHumanSsid(ssid)) score++;

            if (IsMobileGateway()) score++;

            if (IsMobileOUI()) score++;

            return score >= 3
                ? "hotspot"
                : "wifi";
        }

        public static bool isHotspot() => GetConnectionDescription() == "hotspot";

        static string ObtenerBanda(int channel)
        {
            if (channel >= 1 && channel <= 14) return "2.4 GHz";
            else if (channel >= 32 && channel <= 173) return "5 GHz";
            else if (channel >= 1 && channel <= 233) return "6 GHz";
            else return "Desconocida";
        }
        public static string getBand()
        {
            var connectedInterface = NativeWifi.EnumerateInterfaces()
                                                .FirstOrDefault(i => i.State == InterfaceState.Connected);

            if (connectedInterface != null)
            {
                // Info de la red actual
                var network = NativeWifi.GetCurrentConnection(connectedInterface.Id);

                if (!network.Equals(null))
                {
                    var ssid = network.value.Ssid.ToString();
                    var bssEntries = NativeWifi.EnumerateBssNetworks(connectedInterface.Id);
                    var bss = bssEntries.list.FirstOrDefault(b => b.Ssid.ToString() == ssid);

                    if (bss != null)
                    {
                        // Banda (2.4GHz / 5GHz / 6GHz)
                        return cleanString(normalizeSpaces(changeDot2Comma(ObtenerBanda(bss.Channel))));
                    }
                    else
                    {
                        Console.WriteLine("No BSS entry found for the connected SSID.");
                        return "Unknown";
                    }
                }
            } 
            Console.WriteLine("No connected WiFi interface found.");
            return "Unknown";
        }
        private static string changeDot2Comma(string band)
        {
            // cambiar (si hay) el punto por una coma (2.4 GHz -> 2,4 GHz)
            band = band.Replace('.', ',').ToLowerInvariant();
            return band;
        }
        public static string ObtenerSSID()
        {
            // Obtener la interfaz Wi-Fi actualmente conectada
            var connectedInterface = NativeWifi.EnumerateInterfaces()
                                                .FirstOrDefault(i => i.State == InterfaceState.Connected);

            string ssid=null;

            if (connectedInterface != null)
            {
                // Info de la red actual
                var network = NativeWifi.GetCurrentConnection(connectedInterface.Id);

                if (!network.Equals(null))
                {
                    // SSID
                    ssid =  network.value.Ssid.ToString();
                }
            }
            //string ssid = output.Split('\n').FirstOrDefault(line => line.Contains("SSID") && !line.Contains("BSSID"))?.Split(':')[1].Trim();

            return "WiFi: " + (ssid ?? "No conectado");
        }

        public static bool ObtenerEsEthernet()
        {
            foreach (var nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if ((nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet ||
                     nic.NetworkInterfaceType == NetworkInterfaceType.GigabitEthernet ||
                     nic.NetworkInterfaceType == NetworkInterfaceType.FastEthernetFx ||
                     nic.NetworkInterfaceType == NetworkInterfaceType.FastEthernetT) &&
                    nic.OperationalStatus == OperationalStatus.Up)
                {
                    return true;
                }
            }

            return false;
        }

        private static string normalizeSpaces(string band)
        {
            return band.Replace("\u00A0", " ").Trim(); // Reemplaza espacio de ancho no rompible
        }

        private static string cleanString(string band)
        {
            return Regex.Replace(band, @"\s+", " ").Trim(); // Elimina espacios raros
        }

        public static string ObtenerVersionWiFi(ComboBox comboBox1)
        {
            var connectedInterface = NativeWifi.EnumerateInterfaces()
                                                .FirstOrDefault(i => i.State == InterfaceState.Connected);

            string wifiVersion = null;

            if (connectedInterface != null)
            {
                var network = NativeWifi.GetCurrentConnection(connectedInterface.Id);

                if (!network.Equals(null)) {
                    var ssid = network.value.Ssid.ToString();
                    var bssEntries = NativeWifi.EnumerateBssNetworks(connectedInterface.Id);
                    var bss = bssEntries.list.FirstOrDefault(b => b.Ssid.ToString() == ssid);

                    if (bss != null)
                    {
                        wifiVersion = bss.PhyType.ToString();
                        //MessageBox.Show("Versión WiFi detectada: " + wifiVersion);
                        comboBox1.Text = wifiVersion;
                        switch (wifiVersion)
                        {
                            //case "Ofdm":
                                //wifiVersion = "802.11a";
                                //break;
                            case "Ofdm":
                                wifiVersion = "802.11b";
                                break;
                            case "Erp":
                                wifiVersion = "802.11g";
                                break;
                            case "Ht":
                                wifiVersion = "802.11n";
                                break;
                            case "Vht":
                                wifiVersion = "802.11ac";
                                break;
                            case "He":
                                wifiVersion = "802.11ax";
                                break;
                            case "Eht":
                                wifiVersion = "802.11be";
                                break;
                            default:
                                wifiVersion = "Desconocida";
                                break;
                        }
                    }
                    

                } 
            }

            comboBox1.Text = wifiVersion;

            switch (wifiVersion)
            {
                case "802.11be":
                    return "looks_7";
                case "802.11ax":
                    return "looks_6";
                case "802.11ac":
                    return "looks_5";
                case "802.11n":
                    return "looks_4";
                case "802.11g":
                    return "looks_3";
                case "802.11b":
                    return "looks_2";
                case "802.11a":
                    return "looks_1";
                default:
                    return "question_mark";
            }
            
        }
    }
}
