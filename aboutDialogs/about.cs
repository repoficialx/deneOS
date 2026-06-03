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
using DPKCore.Models;

namespace aboutDialogs
{
    public partial class about : Form
    {
        public about(string product = "deneOS")
        {
            InitializeComponent();
            this.Text = @$"About {product}";
        }
        public Manifest aboutManifest()
        {
            return new Manifest
            {
                Name = "deneOS:About Dialogs",
                Version = dosu.Utils.deneOSVersion.ShortVersion.GetVersion(Utils.deneOSVersion.ShortVersion.Formats.M_m)+".0",
                Author = "repoficialx",
                EntryPoint = "aboutDialogs.exe",
                // TODO: añadir permisos
                Permissions = [ "TODO: añadirlos" ],
            };
        }
        private void about_Load(object sender, EventArgs e)
        {
            label2.Text = dosu.UniversalConfiguration.GetUser(aboutManifest()).Username;
            var version = dosu.Utils.deneOSVersion.ShortVersion.GetVersion(Utils.deneOSVersion.ShortVersion.Formats.vM_mx);


            string[] lineas = label1.Text.Split(new[] { "\r\n" }, StringSplitOptions.None);

            for (int i = 0; i < lineas.Length; i++)
            {
                if (lineas[i].Trim().StartsWith("Version v"))
                {
                    lineas[i] = $"Version {version}";
                    break;
                }
                else if (lineas[i].Trim().StartsWith("© "))
                {
                    lineas[i] = $"© {DateTime.Now.Year} repoficialx";
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
