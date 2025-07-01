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

namespace deneNotes
{
    public partial class about : Form
    {
        public about()
        {
            InitializeComponent();
        }

        private void about_Load(object sender, EventArgs e)
        {
            label2.Text = Environment.UserName;
            var info = FileVersionInfo.GetVersionInfo(@"C:\DENEOS\core\deneos.exe");
            string major = info.FileMajorPart.ToString();
            string minor = info.FileMinorPart.ToString();
            string bld = info.FileBuildPart.ToString();

            string channel = bld == "1" ? "b" : bld == "2" ? "a" : "";
            string nuevaVersion = $"v{major}.{minor}{channel}";


            string[] lineas = label1.Text.Split(new[] { "\r\n" }, StringSplitOptions.None);

            for (int i = 0; i < lineas.Length; i++)
            {
                if (lineas[i].Trim().StartsWith("Versión v"))
                {
                    lineas[i] = $"Versión {nuevaVersion}";
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
