using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace deneFiles
{
    public partial class Form1 : Form
    {
        public enum FolderType
        {
            SOFTWARE,
            DNUSR
        }
        public Form1(bool enableRoot = false, FolderType startupFolder = FolderType.DNUSR)
        {
            InitializeComponent();
            // Si enableRoot es true, se inicia en file:///C:/
            if (enableRoot)
            {
                webBrowser1.Navigate("file:///C:/");
                //añadir un mensaje de advertencia al usuario
                MessageBox.Show("¡Atención! Estás navegando en modo raíz. Ten cuidado con los archivos del sistema.", "Modo Raíz", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //añadir al menuStrip un item que permita ir a la carpeta raíz (C:\)
                ToolStripMenuItem rootMenuItem = new ToolStripMenuItem("Ir a la raíz (C:\\)");
                rootMenuItem.Click += (s, e) => webBrowser1.Navigate("file:///C:/");
                rootMenuItem.Image = Properties.Resources.icons8_program_100;
                this.Size = new Size(this.Size.Width + 100, this.Size.Height);
                menuStrip1.Items.Add(rootMenuItem);
                this.BackColor = Color.FromArgb(255, 240, 230);
            }
            else
            {
                // Si se especifica una carpeta de inicio, se navega a ella
                switch (startupFolder)
                {
                    case FolderType.SOFTWARE:
                        webBrowser1.Navigate("file:///C:/SOFTWARE/");
                        break;
                    case FolderType.DNUSR:
                        webBrowser1.Navigate("file:///C:/DNUSR/");
                        break;
                    default:
                        webBrowser1.Navigate("file:///C:/DNUSR");
                        break;
                }
            }
        }

        private void backToolStripMenuItem_Click(object sender, EventArgs e)
        {
            webBrowser1.GoBack();
        }

        private void forwardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            webBrowser1.GoForward();
        }

        private void reloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            webBrowser1.Refresh();
        }

        private void deneossysToolStripMenuItem_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate("c:\\SOFTWARE\\");
        }

        private void userToolStripMenuItem_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate("c:\\DNUSR\\");
        }

        private void parentDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string currentUrl = webBrowser1.Url.ToString();
            int lastSlash = currentUrl.LastIndexOf('/');

            if (lastSlash > "file:///C:/".Length) // No te salgas del root
            {
                string parentUrl = currentUrl.Substring(0, lastSlash);
                webBrowser1.Navigate(parentUrl);
            }


        }
    }
}
