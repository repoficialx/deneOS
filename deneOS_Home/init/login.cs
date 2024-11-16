using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace deneOS_Home.init
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void login_Load(object sender, EventArgs e)
        {

        }

        public static bool ReturnBool(bool truefalse)
        {
            return truefalse;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string usr;
            string pss;
            var cfgInfo = File.Exists(@"C:\Program Files\iNS\deneOS\HomeEdition\cfg\config.ini") ? File.ReadAllLines(@"C:\Program Files\iNS\deneOS\HomeEdition\cfg\config.ini") : new string[] { "", "", "" };
            if (cfgInfo[2].ToLower().Contains("password = "))
            {
                pss = cfgInfo[2].Substring(11);
            }
            if (cfgInfo[1].ToLower().Contains("username = "))
            {
                usr = cfgInfo[1].Substring(11);
            }
            else
            {
                button1.Show();
                MessageBox.Show("No password set! Please enable password on settings.", "Insecure!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }
    }
}
