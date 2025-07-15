using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace deneOS.init
{
    public partial class newusr : Form
    {
        public newusr()
        {
            InitializeComponent();
            txt9.Text = (string)T("txt9");
            txt10.Text = (string)T("txt10");
            txt11.Text = (string)T("txt11");
            txt12.Text = (string)T("txt12");
            txt13.Text = (string)T("txt13");
        }

        private void txt13_Click(object sender, EventArgs e)
        {

            if (boxregpass.Text == boxregpassag.Text)
            {
                string[] file =
                {
                    "[deneOS Home]",
                    string.Format
                    (
                        "username = {0}",
                        boxregusr.Text
                    ),
                    string.Format
                    (
                        "password = {0}",
                        boxregpass.Text
                    )
                };
                File.WriteAllLines(@"C:\DENEOS\sysconf\config.ini", file);
                MessageBox.Show((string)T("usrcrscc"), (string)T("suc"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Cerrar el formulario de registro y mostrar el formulario de inicio de sesión
                Close();
            }
            else
            {
                MessageBox.Show((string)T("pssdntmatch"), (string)T("pssdntmatch"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
