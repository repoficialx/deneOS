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


namespace deneOS_Home
{
    public partial class desktop : Form
    {
        public desktop()
        {
            InitializeComponent();
        }

        private void desktop_Load(object sender, EventArgs e)
        {
            const string regPath = @"Software\deneOS\desktop";
            const string showIconsKey = "showIcons";
            const string wallpaperKey = "wallpaper";

            using (RegistryKey key = Registry.CurrentUser.CreateSubKey(regPath))
            {
                // --- Mostrar / ocultar iconos ---
                int showIcons = Convert.ToInt32(key.GetValue(showIconsKey, 1)); // valor por defecto: 1
                flowLayoutPanel2.Visible = (showIcons == 1);

                // --- Establecer fondo de pantalla ---
                string wallpaperPath = key.GetValue(wallpaperKey)?.ToString();
                if (!string.IsNullOrEmpty(wallpaperPath) && File.Exists(wallpaperPath))
                {
                    try
                    {
                        this.BackgroundImage = Image.FromFile(wallpaperPath);
                        this.BackgroundImageLayout = ImageLayout.Stretch;

                        flowLayoutPanel2.BackColor = Color.Transparent;
                        panel1.BackColor = Color.Transparent;

                        var colorThief = new ColorThief();
                        Bitmap bmp = new Bitmap(wallpaperPath);
                        var dominantColor = colorThief.GetColor(bmp);

                        Color color = Color.FromArgb(dominantColor.Color.R, dominantColor.Color.G, dominantColor.Color.B);

                        string hex = $"#{color.R:X2}{color.G:X2}{color.B:X2}";
                        globaldata.isImageLoaded = true; // Indica que se ha cargado una imagen de fondo
                        globaldata.wallpaperPredominantColorHex = hex;
                    }
                    catch
                    {
                        // Si hay error, simplemente dejar fondo negro (por defecto)
                        //MessageBox.Show("(Error) Error aplicando fondo");
                        this.BackgroundImage = null;
                        this.BackColor = Color.Black;
                    }
                }
                else
                {
                    // No hay fondo, se deja fondo negro
                    //MessageBox.Show("(DEBUG) Fondo no especificado");
                    this.BackgroundImage = null;
                    this.BackColor = Color.Black;
                }
            }
            

        }

        private void panel12_Click(object sender, EventArgs e)
        {
            Process.Start("dnstore");
        }
        private void flowLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void desktop_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true; // Evita que la ventana se cierre
        }
    }
}
