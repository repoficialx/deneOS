using System;
using System.Diagnostics;
using Color = System.Drawing.Color;


namespace deneOS
{
    public partial class HomeScreen : DesktopBase
    {
        protected override FlowLayoutPanel IconsPanel => flowLayoutPanel2;
 
        protected override void SetTransparentPanels()
        {
            tableLayoutPanel1.BackColor = Color.Transparent;
        }
        
        public HomeScreen()
        {
            InitializeComponent();
        }

        private void desktop_Load(object sender, EventArgs e) => LoadDesktop();
        private void panel12_Click(object sender, EventArgs e) => Process.Start("dnstore");
        private void flowLayoutPanel2_Paint(object sender, PaintEventArgs e) { }
    }
}
