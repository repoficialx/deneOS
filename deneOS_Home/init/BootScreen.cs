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

namespace deneOS_Home.init
{
    public partial class BootScreen : Form
    {
        public BootScreen()
        {
            InitializeComponent();
        }

        private void BootScreen_Load(object sender, EventArgs e)
        {
            Process cproc = new Process();
            cproc.StartInfo.FileName = "taskkill";
            cproc.StartInfo.Arguments = "/f /im explorer.exe";
            cproc.Start();
            //hacer el boot

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            this.Hide();
            new logonui().Show();
            
        }
    }
}
