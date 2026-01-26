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
using dosu;

namespace aboutDialogs
{
    public partial class about : Form
    {
        public about(string product = "deneOS")
        {
            InitializeComponent();
            this.Text = @$"About {product}";
        }

        private void about_Load(object sender, EventArgs e)
        {
            label2.Text = dosu.UniversalConfiguration.GetUser().Username;
            var version = dosu.Utils.deneOSVersion.ShortVersion.GetVersion(Utils.deneOSVersion.ShortVersion.Formats.vM_mx);


            string[] lineas = label1.Text.Split(new[] { "\r\n" }, StringSplitOptions.None);

            for (int i = 0; i < lineas.Length; i++)
            {
                if (lineas[i].Trim().StartsWith("Version v"))
                {
                    lineas[i] = $"Version {version}";
                    break;
                }
            }

            label1.Text = string.Join("\r\n", lineas);

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }
    }
}
