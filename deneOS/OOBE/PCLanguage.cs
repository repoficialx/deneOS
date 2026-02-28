using dosu;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace deneOS.OOBE
{
    public partial class PCLanguage : Form
    {
        public event EventHandler PantallaTerminada;
        public PCLanguage()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }
        private void btnContinuar_Click(object sender, EventArgs e)
        {
            // Cuando el usuario pulse continuar, avisamos al padre

            PantallaTerminada?.Invoke(this, EventArgs.Empty);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Inglés
            UniversalConfiguration.SetLang("en");
            btnContinuar_Click(sender, e);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Español
            UniversalConfiguration.SetLang("es");
            btnContinuar_Click(sender, e);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // Sistema
            UniversalConfiguration.SetLang(CultureInfo.CurrentUICulture.TwoLetterISOLanguageName.ToLower().Trim());
            btnContinuar_Click(sender, e);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Francés
            UniversalConfiguration.SetLang("fr");
            btnContinuar_Click(sender, e);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Alemán
            UniversalConfiguration.SetLang("de");
            btnContinuar_Click(sender, e);
        }
    }
}
