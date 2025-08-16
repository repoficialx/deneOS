﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace deneOS.OOBE
{
    public partial class PCPrivacy : Form
    {
        public event EventHandler PantallaTerminada;
        public PCPrivacy()
        {
            InitializeComponent();
            linkLabel3.Text = (string)T("OOBEPCContribute");
            linkLabel1.Text = (string)T("OOBEPCHelp");
            linkLabel2.Text = (string)T("OOBEPCED");
            label1.Text = (string)T("OOBEPCTitle");
            label2.Text = (string)T("OOBEPCPrivacyTitle");
            amaps.Text = (string)T("OOBEPCPrivacyD1");
            ddgo.Text = (string)T("OOBEPCPrivacyD2");
            protonvpn.Text = (string)T("OOBEPCPrivacyD3");
            button1.Text = (string)T("OOBEPCPrivacySUA");
            label3.Text = (string)T("OOBEPCPrivacyDisclaimer");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (amaps.Checked)
            {

            }
            if (ddgo.Checked)
            {
                using (WebClient client = new WebClient())
                {
                    var ddgoz = 
                        client.DownloadData(
"https://dw23.malavida.com/dwn/144e345151963252aa4c891afa06be4e45c05448e06a399f56e13737af08dfe4/DuckDuckGo.zip");
                    // Ensure the directory exists
                    string extractPath = Path.Combine(Environment.CurrentDirectory, "DuckDuckGo");
                    if (!Directory.Exists(extractPath))
                    {
                        Directory.CreateDirectory(extractPath);
                    }
                    // Fix: Convert the byte array to a MemoryStream before passing it to ExtractToDirectory
                    using (var memoryStream = new MemoryStream(ddgoz))
                    {
                        ZipFile.ExtractToDirectory(memoryStream, extractPath);
                    }
                    // Run the installer
                    string installerPath = Path.Combine(extractPath, "DuckDuckGo.appinstaller");
                    if (File.Exists(installerPath))
                    {
                        System.Diagnostics.Process.Start(installerPath);
                    }
                    else
                    {
                        MessageBox.Show("Installer not found in the extracted files.");
                    }
                }
            }
            if (protonvpn.Checked)
            {
                using (WebClient client = new WebClient())
                {
                    var ddgoz = client.DownloadData("https://vpn.protondownload.com/download/ProtonVPN_v4.2.2_x64.exe");
                    // Ensure the directory exists
                    string extractPath = Path.Combine(Environment.CurrentDirectory, "ProtonVPN");
                    if (!Directory.Exists(extractPath))
                    {
                        Directory.CreateDirectory(extractPath);
                    }
                    // Write the downloaded data to a file
                    string filePath = Path.Combine(extractPath, "ProtonVPN_v4.2.2_x64.exe");
                    File.WriteAllBytes(filePath, ddgoz);
                    // Run the installer
                    if (File.Exists(filePath))
                    {
                        System.Diagnostics.Process.Start(filePath);
                    }
                    else
                    {
                        MessageBox.Show("Installer not found in the extracted files.");
                    }
                    // Clean up the directory after installation
                    Directory.Delete(extractPath, true);
                }
            }
            PantallaTerminada?.Invoke(this, EventArgs.Empty);
        }
    }
}
