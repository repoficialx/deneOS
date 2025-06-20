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

namespace deneOS_Home
{
    public partial class EmergencyScreen : Form
    {
        int seconds = 10;
        public EmergencyScreen(string error_code = "")
        {
            InitializeComponent();
            label3.Text = error_code != "" ? string.Format("Error code: {0}", error_code) : "No error code provided";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ForceReboot();
        }
        void ForceReboot()
        {
            //Process.Start("shutdown", "/r /f /t 0");
            //Para motivos de prueba, sìmplemente arrancamos explorer y cerramos deneOS
            Process.Start("explorer.exe");
            Application.Exit();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (seconds > 0)
            {
                seconds -= 1;
                label4.Text = string.Format("Restarting in {0} seconds", seconds);
            }
            else
            {
                timer1.Stop();
                ForceReboot();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
