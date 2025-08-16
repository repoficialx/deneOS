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
using System.Windows.Forms.VisualStyles;

namespace deneOS.OOBE
{
    public partial class PCWelcome : Form
    {
        // Evento para avisar que terminó
        public event EventHandler PantallaTerminada;
        public PCWelcome()
        {
            InitializeComponent();
        }
        private void btnContinuar_Click(object sender, EventArgs e)
        {
            // Cuando el usuario pulse continuar, avisamos al padre
            PantallaTerminada?.Invoke(this, EventArgs.Empty);
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Open GitHub page for deneOS
            Process.Start(new ProcessStartInfo("https://github.com/repoficialx/deneOS") { UseShellExecute = true });
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Open repoficialx.xyz page for deneOS
             Process.Start(new ProcessStartInfo("https://repoficialx.xyz/deneOS") { UseShellExecute = true });
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Open desktop form
            Process.Start(
                new ProcessStartInfo(
                    "deneOS.exe",
                    "/dangerZone:skipBootAnim " +
                    "/safeMode " +
                    "/dangerZone:disableLockScreen " +
                    "/bypassChecks"
                    )
                { UseShellExecute = true });
            Environment.Exit (0);
        }
    }
}
