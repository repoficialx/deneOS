using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace deneOS.OOBE
{
    public partial class PCRegion : Form
    {
        // Evento para avisar que terminó
        public event EventHandler PantallaTerminada;
        public PCRegion()
        {
            InitializeComponent();
            linkLabel3.Text = (string)T("OOBEPCContribute");
            linkLabel1.Text = (string)T("OOBEPCHelp");
            linkLabel2.Text = (string)T("OOBEPCED");
            label1.Text = (string)T("OOBEPCTitle");
            label2.Text = (string)T("OOBEPCRegionTitle");
            button2.Text = (string)T("OOBEPCRegionUS");
            button3.Text = (string)T("OOBEPCRegionES");
            button1.Text = (string)T("OOBEPCRegionAD");
            button8.Text = (string)T("OOBEPCRegionEN");
            button5.Text = (string)T("OOBEPCRegionDE");
            button4.Text = (string)T("OOBEPCRegionFR");
            button6.Text = (string)T("OOBEPCRegionSystem");
            this.DoubleBuffered = true;
        }

        private void btnContinuar_Click(object sender, EventArgs e)
        {
            // Cuando el usuario pulse continuar, avisamos al padre
            PantallaTerminada?.Invoke(this, EventArgs.Empty);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // US
            btnContinuar_Click(sender: sender, e: e);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // ES
            btnContinuar_Click(sender: sender, e: e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // AD
            btnContinuar_Click(sender: sender, e: e);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            // EN
            btnContinuar_Click(sender: sender, e: e);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // GE
            btnContinuar_Click(sender: sender, e: e);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // FR
            btnContinuar_Click(sender: sender, e: e);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // Sys
            btnContinuar_Click(sender: sender, e: e);
        }
    }
}
