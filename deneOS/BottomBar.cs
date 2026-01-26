using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection.Emit;
using System.Text;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace deneOS
{
    public partial class BottomBar : Form
    {
        public BottomBar()
        {
            InitializeComponent();
            TopMost = true;
            Shown += (_, __) =>
            {
                AppBarManager.Register(this, AppBarManager.AppBarEdge.Bottom, 64);
            };
            FormClosing += (_, __) =>
            {
                AppBarManager.Unregister(this);
            };
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            AppBarManager.SetPosition(this, AppBarManager.AppBarEdge.Bottom, 64);
        }

        private void BottomBar_Load(object sender, EventArgs e)
        {
            Console.WriteLine("[INFO] Cargando tbar...");
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;
            Console.WriteLine("[INFO] Obtenida resolución de pantalla: " + screenWidth + "x" + screenHeight);
            int taskbarHeight = 64;
            Console.WriteLine("[INFO] Configurando barra de tareas...");
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.Manual;
            
            this.Location = new Point(0, screenHeight - taskbarHeight);
            this.Size = new Size(screenWidth, taskbarHeight);
            this.TopMost = true;
            Console.WriteLine("[INFO] Barra inferior configurada en la parte inferior de la pantalla.");
        }

        
    }
}
