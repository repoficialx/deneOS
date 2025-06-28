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
using static Traductor;

namespace deneOS
{
    public partial class EmergencyScreen : Form
    {
        int seconds = 10;
        public EmergencyScreen(string error_code = "")
        {
            InitializeComponent();
            label1.Text = (string)T("dhap");
            label2.Text = (string)T("rsodmsg");
            label3.Text = error_code != "" ? string.Format("{0}: {1}", T("errcode"), error_code) : (string)T("noecprov");
            button1.Text = (string)T("rstnow");
            label4.Text = string.Format("{0} {1} {2}", T("resetin"), 10, T("ss"));
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
                label4.Text = string.Format("{0} {1} {2}", T("resetin"), seconds, T("ss"));
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

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
