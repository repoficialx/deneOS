using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace deneOS_Home
{
    public partial class desktop : Form
    {
        public desktop()
        {
            InitializeComponent();
        }

        private void desktop_Load(object sender, EventArgs e) {
        
        }
        private void panel12_Click(object sender, EventArgs e)
        {
            //INICIAR DENESTORE
        }
        private void flowLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void desktop_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true; // Evita que la ventana se cierre
        }
    }
}
