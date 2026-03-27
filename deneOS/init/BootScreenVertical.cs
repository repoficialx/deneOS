using deneOS.Security;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static dosu.UniversalConfiguration;
using static Traductor;
using Timer = System.Windows.Forms.Timer;

namespace deneOS.init
{
    public partial class BootScreenVertical : BootScreenBase
    {
        protected override Control LogoControl    => pictureBox1;
        protected override Label   SpinnerLabel   => label1;
        protected override Timer   TransitionTimer => timer1;
        protected override void PositionLogo()
        {
            // En vertical el logo ocupa toda la pantalla (Dock = Fill en el Designer)
            // No hace falta reposicionar manualmente.
        }
        private int bootIndex = 0;
        private System.Windows.Forms.Timer bootTimer;
        public BootScreenVertical()
        {
            InitializeComponent();
            InitializeBootFlow();
        }

        private void BootScreen_Load(object sender, EventArgs e) => RunBootChecks();
        private void timer1_Tick(object sender, EventArgs e) => OnTransitionTimerTick(sender, e);
    }
}
