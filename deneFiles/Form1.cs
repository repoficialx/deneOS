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
        public Form1()
        {
            InitializeComponent();
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
            webBrowser1.Navigate("c:\\deneos\\");
        }

        private void userToolStripMenuItem_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate("c:\\deneos\\user\\");
        }
    }
}
