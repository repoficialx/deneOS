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
    public partial class PCAccount : Form
    {
        public event EventHandler PantallaTerminada;
        public PCAccount()
        {
            InitializeComponent();
            label2.Text = (string)T("OOBEPCAccountTitle");
            label3.Text = (string)T("OOBEPCAccountUsername");
            label4.Text = (string)T("OOBEPCAccountPassword");
            button1.Text = (string)T("OOBEPCAccountFinish");
            label1.Text = (string)T("OOBEPCTitle");
            linkLabel2.Text = (string)T("OOBEPCED");
            linkLabel1.Text = (string)T("OOBEPCHelp");
            linkLabel3.Text = (string)T("OOBEPCContribute");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;
            string configFile = @"C:\DENEOS\sysconf\config.ini";
            try
            {
                // Ensure the directory exists
                string directory = System.IO.Path.GetDirectoryName(configFile);
                if (!System.IO.Directory.Exists(directory))
                {
                    System.IO.Directory.CreateDirectory(directory);
                }
                // Write the username and password to the config file
                using (System.IO.StreamWriter writer = new System.IO.StreamWriter(configFile, false))
                {
                    writer.WriteLine("[Account]");
                    writer.WriteLine($"username = {username}");
                    writer.WriteLine($"password = {password}");
                }
                PantallaTerminada?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating account: {ex.Message}");
            }
        }
    }
}
