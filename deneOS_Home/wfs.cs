using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using static Traductor;
using System.Text.RegularExpressions;   

class wfs
{
    public static int GetWifiSignalStrength()
    {

        ProcessStartInfo psi = new ProcessStartInfo
        {
            FileName = "netsh",
            Arguments = "wlan show interfaces",
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        string output;
        using (Process process = Process.Start(psi))
        {
            output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
        }
        Console.Write(output);
        // Buscar "Señal : 96%" usando regex)
        Match match = Regex.Match(output, @"(\d+)%");
        if (match.Success)
        {
            int signalStrength = int.Parse(match.Groups[1].Value);
            Console.WriteLine($"{T("wifistrength")} {signalStrength}%");
            //MessageBox.Show($"Intensidad de señal WiFi: {signalStrength}%");
            return signalStrength;
        }
        else
        {
            Console.WriteLine(T("nowifisignal"));
            return -1;
        }

        
    }
}