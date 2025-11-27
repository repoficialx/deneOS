using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Internet
{
    internal class Internet
    {
        public static string getBand()
        {


            Process process = new Process();
            process.StartInfo.FileName = "netsh";
            process.StartInfo.Arguments = "wlan show interfaces";
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.StandardOutputEncoding = Encoding.UTF8;


            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            // Buscar la línea de la banda
            Match match = Regex.Match(output, @"Banda\s*:\s*(.+)");
            if (match.Success)
            {
                string banda = match.Groups[1].Value.Trim();
                Console.WriteLine($"Banda detectada: {banda}");
                return cleanString(normalizeSpaces(changeDot2Comma(banda)));
            }
            else
            {
                Console.WriteLine("No se pudo determinar la banda.");
                return "Unknown";
            }
        }
        private static string changeDot2Comma(string band)
        {
            // cambiar (si hay) el punto por una coma (2.4 GHz -> 2,4 GHz)
            band = band.Replace('.', ',').ToLowerInvariant();
            return band;
        }
        public static string ObtenerSSID()
        {
            Process process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "netsh",
                    Arguments = "wlan show interfaces",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    StandardOutputEncoding = Encoding.UTF8
                }
            };

            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
#pragma warning disable CS8600 // Se va a convertir un literal nulo o un posible valor nulo en un tipo que no acepta valores NULL
            // Extraer el SSID de la salida
            
            string ssid = output.Split('\n').FirstOrDefault(line => line.Contains("SSID") && !line.Contains("BSSID"))?.Split(':')[1].Trim();

            return "WiFi: " + (ssid ?? "No conectado");
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
