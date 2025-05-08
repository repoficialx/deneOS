using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace deneOS_Launcher
{
    public partial class Form1 : Form
    {
        bool havePro;
        bool proId;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            #region DON'T TOUCH THIS CODE!
            getRgKey();
            if (havePro)
            {
                if (!proId)
                {
                    havePro = false;
                    MessageBox.Show("ANTI-PIRACY ALERT! - PLEASE BUY deneOS PRO ON iNS WEBPAGE!");
                    //Registry.SetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\iNS\\deneOS", "deneOSProEnabled", "n");
                    Directory.Delete(@"C:\Program Files\iNS\deneOS\Launcher\cfg", true);
                    Environment.Exit(-3278947);
                }
                else
                {
                    loadPro();
                }
            }
            else
            {
                MessageBox.Show("Are you a developer? Try deneOS Pro! 32,5$ or 28€");
                Size = new Size(401, 185);
                MaximumSize = new Size(401, 185);
            }
            #endregion
        }
        #region DON'T TOUCH THIS CODE!
        void getRgKey()
        {
            bool proEnabled2 = false;
            bool proKeyEnabled = false;
            bool cfg = File.Exists(@"C:\Program Files\iNS\deneOS\Launcher\cfg\config.ini");
            if (cfg)
            {
                var cfgInfo = File.ReadAllLines(@"C:\Program Files\iNS\deneOS\Launcher\cfg\config.ini");
                if (cfgInfo[1].Contains("deneOSProEnabled = true"))
                {
                    proEnabled2 = true;
                }
                if (cfgInfo[2].Contains("deneOSProKey = {7843NF-DRF764-VD9786-67W4N3}"))
                {
                    proKeyEnabled = true;
                }
                if (proEnabled2 && proKeyEnabled)
                {

                }
            }
            havePro = proEnabled2;
            proId = proKeyEnabled;
        }
        void loadPro()
        {
            Size = new Size(401, 338);
            MaximumSize = new Size(401, 338);
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            Process dnh = new Process();
            dnh.StartInfo.FileName = "C:\\Users\\rayel\\source\\repos\\!New\\repos\\deneOS\\deneOS_Home\\bin\\Debug\\DeneOS_Home.exe";
            dnh.StartInfo.Verb = "runas";
            dnh.StartInfo.UseShellExecute = true;
        }
    }
}
