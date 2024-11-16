using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Internet_Explorer_11
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void webBrowser1_FileDownload(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate(toolStripTextBox1.Text);
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void openHTMLPageToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void obrinsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate("bing.com");
        }

        private void homeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate("view-source:https://ntp.msn.com/edge/ntp?locale=es&title=Nueva%20pesta%C3%B1a&dsp=0&sp=Google&PC=U531&adppc=EDGEESS");
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutBox1().ShowDialog();
        }
    }
}
