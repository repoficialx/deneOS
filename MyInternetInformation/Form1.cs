using System.Diagnostics;
using System.Runtime.InteropServices;

namespace MyInternetInformation
{
    public partial class Form1 : Form
    {
        // Importaciones para permitir el movimiento del formulario sin bordes
        [DllImport("user32.dll")]
        private static extern void ReleaseCapture();
        [DllImport("user32.dll")]
        private static extern void SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

        public Form1()
        {
            InitializeComponent();
        }

        private void panelBarra_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0xA1, 0x2, 0);
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnMaximizar_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
                this.WindowState = FormWindowState.Normal;
            else
                this.WindowState = FormWindowState.Maximized;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            this.webIdc.CoreWebView2.Navigate($"http://idcrawl.com/u/{txtUser.Text}");
            this.webIdc.Show();
            this.webIdc.Enabled = true;
            this.webIdc.BringToFront();
        }

        private void lblCreditos_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo{FileName = "https://idcrawl.com/",UseShellExecute = true});
        }
    }
}
