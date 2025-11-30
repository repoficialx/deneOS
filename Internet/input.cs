using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Internet
{
    public partial class input : Form
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string contraseña { get; private set; } // Propiedad de instancia

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ssid { get; set; }

        public input()
        {
            InitializeComponent();
        }

        private void input_Load(object sender, EventArgs e)
        {
            label1.Text += ssid;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            contraseña = textBox1.Text;  // Guarda la contraseña antes de cerrar
            Close();
        }
    }
}
