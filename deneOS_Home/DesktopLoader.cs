using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Reflection;

public class DesktopLoader
{
    public static void LoadShortcuts(string desktopPath, Control container)
    {
        string[] shortcuts = Directory.GetFiles(desktopPath, "*.lnk");

        foreach (var shortcut in shortcuts)
        {
            string name = Path.GetFileNameWithoutExtension(shortcut);
            Icon icon = ShellIcon.GetIconFromLink(shortcut);

            // Panel contenedor
            Panel panel = new Panel
            {
                Size = new Size(131, 169),
                Margin = new Padding(20),
                BackColor = Color.Transparent
            };
            panel.Click += (sender, e) =>
            {
                try
                {
                    System.Diagnostics.Process.Start(shortcut);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al abrir el acceso directo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            };
            // Imagen (icono)
            PictureBox pic = new PictureBox
            {
                Size = new Size(125, 125),
                SizeMode = PictureBoxSizeMode.Zoom,
                Image = icon.ToBitmap(),
                Location = new Point(3, 3) // pequeño margen interno
            };

            pic.Click += (sender, e) =>
            {
                try
                {
                    System.Diagnostics.Process.Start(shortcut);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al abrir el acceso directo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            };

            // Etiqueta (nombre)
            Label label = new Label
            {
                Text = name,
                Size = new Size(131, 28),
                Location = new Point(0, 133),
                TextAlign = ContentAlignment.TopCenter
            };

            label.Click += (sender, e) =>
            {
                try
                {
                    System.Diagnostics.Process.Start(shortcut);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al abrir el acceso directo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            };

            panel.Controls.Add(pic);
            panel.Controls.Add(label);
            container.Controls.Add(panel);
        }
    }
}
