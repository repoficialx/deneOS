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
    public partial class PCFinish : Form
    {
        // Evento que se dispara cuando la pantalla ha terminado
        public event EventHandler PantallaTerminada;
        public PCFinish()
        {
            InitializeComponent();
            label1.Text = (string)T("OOBEPCTitle");
            label2.Text = (string)T("OOBEPCFinishTitle");
            button1.Text = (string)T("OOBEPCFinishReboot");
            linkLabel3.Text = (string)T("OOBEPCContribute");
            linkLabel1.Text = (string)T("OOBEPCHelp");
            linkLabel2.Text = (string)T("OOBEPCED");
            this.DoubleBuffered = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PantallaTerminada?.Invoke(this, EventArgs.Empty);
        }
    }
}
