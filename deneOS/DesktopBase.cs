using ColorThiefDotNet;
using Microsoft.Win32;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Color = System.Drawing.Color;

namespace deneOS
{
    /// <summary>
    /// Clase base compartida para <see cref="desktop"/> (horizontal)
    /// y <see cref="HomeScreen"/> (vertical).
    /// Gestiona wallpaper, extracción del color dominante e iconos de escritorio.
    /// </summary>
    public abstract class DesktopBase : Form
    {
        private const string RegPath       = @"Software\deneOS\desktop";
        private const string ShowIconsKey  = "showIcons";
        private const string WallpaperKey  = "wallpaper";

        // ── Propiedades que cada subclase expone ─────────────────────────

        /// <summary>Panel donde se cargan los accesos directos.</summary>
        protected abstract FlowLayoutPanel IconsPanel { get; }

        // ── Lógica compartida de carga ───────────────────────────────────

        /// <summary>
        /// Llama a este método desde el evento Load de cada subclase.
        /// </summary>
        protected void LoadDesktop()
        {
            using var key = Registry.CurrentUser.CreateSubKey(RegPath);

            // Iconos visibles / ocultos
            IconsPanel.Visible = Convert.ToInt32(key.GetValue(ShowIconsKey, 1)) == 1;

            // Wallpaper
            string wallpaperPath = key.GetValue(WallpaperKey)?.ToString();
            ApplyWallpaper(wallpaperPath);

            // Accesos directos
            DesktopLoader.LoadShortcuts(@"C:\DENEOS\desktop\", IconsPanel);
        }

        private void ApplyWallpaper(string path)
        {
            if (!string.IsNullOrEmpty(path) && File.Exists(path))
            {
                try
                {
                    this.BackgroundImage       = Image.FromFile(path);
                    this.BackgroundImageLayout = ImageLayout.Stretch;
                    IconsPanel.BackColor       = Color.Transparent;
                    SetTransparentPanels();

                    // Color dominante para la barra de tareas
                    var colorThief    = new ColorThief();
                    var dominant      = colorThief.GetColor(new Bitmap(path));
                    var color         = Color.FromArgb(dominant.Color.R, dominant.Color.G, dominant.Color.B);

                    globaldata.isImageLoaded               = true;
                    globaldata.wallpaperPredominantColorHex = $"#{color.R:X2}{color.G:X2}{color.B:X2}";
                }
                catch
                {
                    SetBlackBackground();
                }
            }
            else
            {
                SetBlackBackground();
            }
        }

        /// <summary>
        /// Hace transparentes los paneles adicionales que cada subclase necesite.
        /// Opcional: sobreescribir si hay más paneles.
        /// </summary>
        protected virtual void SetTransparentPanels() { }

        private void SetBlackBackground()
        {
            this.BackgroundImage = null;
            this.BackColor       = Color.Black;
        }

        // ── Prevenir cierre accidental ───────────────────────────────────
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            e.Cancel = true;
            base.OnFormClosing(e);
        }
    }
}