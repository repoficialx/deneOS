using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using ColorThiefDotNet;
using Color = System.Drawing.Color;


namespace deneOS
{
    public partial class desktop : DesktopBase
    {
        protected override FlowLayoutPanel IconsPanel => flowLayoutPanel2;
        
        protected override void SetTransparentPanels()
        {
            tableLayoutPanel1.BackColor = Color.Transparent;
        }
        
        public desktop()
        {
            InitializeComponent();
        }

        private void desktop_Load(object sender, EventArgs e) => LoadDesktop();

        private void panel12_Click(object sender, EventArgs e) => Process.Start("dnstore");
    }
}
