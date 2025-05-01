using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Internet_Explorer_11
{
    partial class AboutBox1 : Form
    {
        public AboutBox1()
        {
            InitializeComponent();
            this.Text = "Acerca de iNS Minibrowser";
            this.labelProductName.Text = "Componentes adaptados para deneOS de iNS.";
            this.labelVersion.Text = String.Format("Versión {0}", "AssemblyVersion");
            this.labelCopyright.Text = "Adaptación de código cerrado para un miniOS de código semiabierto";
            this.labelCompanyName.Text = "iNS";
            this.textBoxDescription.Text = "iNS Minibrowser usa la API de Internet Explorer para mostrar páginas web, está hecha con .NET Framework, para deneOS por parte de iNS. La idea de lo que quiere portar a deneOS es Internet Explorer 11. ¿Por qué 11? Simple, porque Windows 10 y 11 usan IE11.";
        }
        private void AboutBox1_Load(object sender, EventArgs e)
        {
        }
    }
}
