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
    public partial class BootScreen : BootScreenBase
    {
        protected override Control LogoControl  => logo;
        protected override Label   SpinnerLabel => label1;
        protected override Timer   TransitionTimer => timer1;
        protected override void PositionLogo()
        {
            // Proporciones respecto a 1920×1080 (diseño original)
            float posX      = 684f / 1920f;
            float posY      = 261f / 1080f;
            float widthPct  = 572f / 1920f;
            float heightPct = 468f / 1080f;
 
            logo.Location = new Point(
                (int)(posX * this.Width),
                (int)(posY * this.Height));
            logo.Size = new Size(
                (int)(widthPct  * this.Width),
                (int)(heightPct * this.Height));
        }
        private int bootIndex = 0;
        private System.Windows.Forms.Timer bootTimer;
        public BootScreen()
        {
            InitializeComponent();
            InitializeBootFlow();
        }

        private void BootScreen_Load(object sender, EventArgs e) => RunBootChecks();
        private void timer1_Tick(object sender, EventArgs e) => OnTransitionTimerTick(sender, e);
    }
}
