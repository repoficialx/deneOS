using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace deneOS_Home.init
{
    public partial class register : Form
    {
        public register()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //txt1 usuario txt2 contraseña
            string[] file = 
            { 
                "[deneOS Home]", 
                string.Format
                (
                    "username = {0}", 
                    textBox1.Text
                ), 
                string.Format
                (
                    "password = {0}", 
                    textBox2.Text
                )
            };
            File.WriteAllLines(@"C:\Program Files\iNS\deneOS\HomeEdition\cfg\config.ini", file);
        }
    }
}
