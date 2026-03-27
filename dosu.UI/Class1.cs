using System.Diagnostics;
using System.Runtime.InteropServices;

namespace dosu.UI;

public class CustomMessageBox : Form
{
    public CustomMessageBox(string message, string title, Icon customIcon)
    {
        this.Text = title;
        this.Size = new Size(400, 200);

        PictureBox iconBox = new PictureBox
        {
            Image = customIcon.ToBitmap(),
            SizeMode = PictureBoxSizeMode.StretchImage,
            Location = new Point(20, 40),
            Size = new Size(50, 50)
        };

        Label messageLabel = new Label
        {
            Text = message,
            Location = new Point(80, 50),
            AutoSize = true
        };

        Button okButton = new Button
        {
            Text = "OK",
            Location = new Point(150, 120),
            DialogResult = DialogResult.OK
        };

        this.Controls.Add(iconBox);
        this.Controls.Add(messageLabel);
        this.Controls.Add(okButton);
        this.AcceptButton = okButton;
    }
    public static DialogResult Show(string message, string title, Icon customIcon)
    {
        using var box = new CustomMessageBox(message, title, customIcon);
        return box.ShowDialog();
    }
}

    public static class SystemSizes
    {
        public static int GetTaskbarHeight()
        {
            // Obtiene el objeto Screen que representa la pantalla principal
            Screen primaryScreen = Screen.PrimaryScreen;

            // Altura total de la pantalla en píxeles
            int screenHeight = primaryScreen!.Bounds.Height;

            // Altura del área de trabajo (excluyendo la barra de tareas)
            int workingAreaHeight = primaryScreen.WorkingArea.Height;

            // La diferencia es la altura de la barra de tareas
            int taskbarHeight = screenHeight - workingAreaHeight;

            return taskbarHeight;
        }
    }
    public static class Scaling
    {
        // ✅ IMPORTANTE: Resolución en la que DISEÑASTE los formularios
        // Si diseñaste en 125% DPI, estos valores están "inflados"
        //private const int DesignWidth = 707;   // Ancho del PCWelcome en designer
        //private const int DesignHeight = 403;  // Alto del PCWelcome en designer

        // Factor de escala base (1.0 = 100% DPI, 1.25 = 125% DPI)
        private const float DesignDpiScale = 1.00f; // ✅ Ajusta según tu DPI de diseño

        /// <summary>
        /// Obtiene el factor de escala actual del sistema
        /// </summary>
        private static float GetSystemDpiScale()
        {
            using Graphics g = Graphics.FromHwnd(IntPtr.Zero);
            return g.DpiX / 96f; // 96 DPI = 100%, 120 DPI = 125%, etc.
        }

        public static float GetScaling(Form form)
        {
            var g = form.CreateGraphics();
            var dpiX = g.DpiX; // DPI horizontal
            g.Dispose();

            // Escalado respecto al estándar 96 DPI
            var scalingPercent = dpiX / 96 * 100;
            return scalingPercent;
        }

        /// <summary>
        /// Escala un tamaño desde el diseño base al DPI actual
        /// </summary>
        private static Size ScaleSize(Size originalSize)
        {
            var currentDpi = GetSystemDpiScale();
            var scaleFactor = currentDpi / DesignDpiScale;

            return new Size(
                (int)(originalSize.Width * scaleFactor),
                (int)(originalSize.Height * scaleFactor)
            );
        }

        /// <summary>
        /// Escala un punto desde el diseño base al DPI actual
        /// </summary>
        static Point ScalePoint(Point originalLocation)
        {
            float currentDpi = GetSystemDpiScale();
            float scaleFactor = currentDpi / DesignDpiScale;

            return new Point(
                (int)(originalLocation.X * scaleFactor),
                (int)(originalLocation.Y * scaleFactor)
            );
        }

        /// <summary>
        /// Guarda las posiciones/tamaños originales antes de escalar
        /// </summary>
        private static Dictionary<Control, (Point Location, Size Size)> originalSizes
            = new Dictionary<Control, (Point, Size)>();

        /// <summary>
        /// Escala controles recursivamente manteniendo proporciones
        /// </summary>
        public static void ScaleControlsRecursive(Control.ControlCollection controls)
        {
            foreach (Control ctrl in controls)
            {
                // Guardar tamaño original si no existe
                if (!originalSizes.ContainsKey(ctrl))
                {
                    originalSizes[ctrl] = (ctrl.Location, ctrl.Size);
                }

                var original = originalSizes[ctrl];

                // Escalar desde el tamaño ORIGINAL, no el actual
                ctrl.Location = ScalePoint(original.Location);
                ctrl.Size = ScaleSize(original.Size);

                // Escalar fuente también
                if (!ctrl.Font.Equals(null))
                {
                    var currentDpi = GetSystemDpiScale();
                    var scaleFactor = currentDpi / DesignDpiScale;
                    var newSize = ctrl.Font.Size * scaleFactor;

                    ctrl.Font = new Font(
                        ctrl.Font.FontFamily,
                        newSize,
                        ctrl.Font.Style
                    );
                }

                // Si tiene hijos, escalar recursivamente
                if (ctrl.HasChildren)
                {
                    ScaleControlsRecursive(ctrl.Controls);
                }
            }
        }

        /// <summary>
        /// Escala un formulario completo
        /// </summary>
        public static void ScaleForm(Form form)
        {
            // Guardar tamaño original del formulario
            if (!originalSizes.ContainsKey(form))
            {
                originalSizes[form] = (form.Location, form.Size);
            }

            var original = originalSizes[form];

            // Escalar el formulario desde su tamaño original
            form.Size = ScaleSize(original.Size);

            // Escalar controles dentro
            ScaleControlsRecursive(form.Controls);
        }

        /// <summary>
        /// Limpia la caché de tamaños originales (llamar al cerrar formularios)
        /// </summary>
        public static void ClearCache()
        {
            originalSizes.Clear();
        }
    }
    public class AppVentana
    {
        public required string Titulo { get; set; }
        public required Icon Icono { get; set; }
        public required Process Proceso { get; set; }
    }
    public static class Fonts
    {
        public enum AvailableFonts
        {
            FluentSystemIcons,
            MaterialIcons,
            SegoeMDL2,
            SegoeSLBoot,
            SegoeFluentIcons
        }
        private static string GetFontName(AvailableFonts font)
        {
            return font switch
            {
                AvailableFonts.FluentSystemIcons => "FluentSystemIcons-Regular",
                AvailableFonts.MaterialIcons => "Material Icons",
                AvailableFonts.SegoeMDL2 => "Segoe MDL2 Assets",
                AvailableFonts.SegoeSLBoot => "Segoe Boot Semilight",
                AvailableFonts.SegoeFluentIcons => "Segoe Fluent Icons",
                _ => "Segoe UI Symbol"
            };
        }
        public static void SetGlyph(AvailableFonts font, string glyph, Control control, int size)
        {
            control.Font = new Font(GetFontName(font), size);
            int code = int.Parse(glyph, System.Globalization.NumberStyles.HexNumber);
            char unicode = (char)code;
            control.Text = unicode.ToString();
        }
        public static char GetGlyph(AvailableFonts font, string glyph)
        {
            int code = int.Parse(glyph, System.Globalization.NumberStyles.HexNumber);
            char unicode = (char)code;
            return unicode;
        }
        public static string GetGlyphAsString(AvailableFonts font, string glyph)
        {
            return GetGlyph(font, glyph).ToString();
        }
        public static List<AvailableFonts> GetFonts()
        {
            List<AvailableFonts> list = [AvailableFonts.FluentSystemIcons, AvailableFonts.MaterialIcons, AvailableFonts.SegoeMDL2,
                AvailableFonts.SegoeSLBoot, AvailableFonts.SegoeFluentIcons];
            return list;
        }
        public static Array GetFontsAsArray()
        {
            string[] fonts = ["FluentSystemIcons", "MaterialIcons", "SegoeMDL2", "SegoeSLBoot", "SegoeFluentIcons"];
            return fonts;
        }
        public static List<string> GetFontsAsStringList()
        {
            List<string> list = new();
            foreach (var item in GetFonts())
            {
                list.Add(GetFontName(item));
            }
            return list;
        }
    }
    public static class DisplayFonts
    {
        public enum Fonts
        {
            AgencyFB,
            AgencyFBBold,
            ProductSansBold,
            ProductSansBoldItalic,
            ProductSansItalic,
            ProductSans,
            SegoeUIVD,
            SegoeUIVDLight,
            SegoeUIVDSemibold,
            SegoeUIVDSemilight,
            SegoeUIVS,
            SegoeUIVSLight,
            SegoeUIVSSemibold,
            SegoeUIVSSemilight,
            SegoeUIVT,
            SegoeUIVTLight,
            SegoeUIVTSemibold,
            SegoeUIVTSemilight
        }
        public static string GetFontName(Fonts font)
        {
            return font switch
            {
                Fonts.AgencyFB => "Agency FB",
                Fonts.AgencyFBBold => "Agency FB",
                Fonts.ProductSansBold => "Product Sans",
                Fonts.ProductSansBoldItalic => "Product Sans",
                Fonts.ProductSansItalic => "Product Sans",
                Fonts.ProductSans => "Product Sans",
                Fonts.SegoeUIVD => "Segoe UI Variable Display",
                Fonts.SegoeUIVDLight => "Segoe UI Variable Display Light",
                Fonts.SegoeUIVDSemibold => "Segoe UI Variable Display Semib",
                Fonts.SegoeUIVDSemilight => "Segoe UI Variable Display Semil",
                Fonts.SegoeUIVS => "Segoe UI Variable Small",
                Fonts.SegoeUIVSLight => "Segoe UI Variable Small Light",
                Fonts.SegoeUIVSSemibold => "Segoe UI Variable Small Semibol",
                Fonts.SegoeUIVSSemilight => "Segoe UI Variable Small Semilig",
                Fonts.SegoeUIVT => "Segoe UI Variable Text",
                Fonts.SegoeUIVTLight => "Segoe UI Variable Text Light",
                Fonts.SegoeUIVTSemibold => "Segoe UI Variable Text Semibold",
                Fonts.SegoeUIVTSemilight => "Segoe UI Variable Text Semiligh",
                _ => "Segoe UI Variable Text"
            };
        }
        public static void SetFont(Fonts font, Control control)
        {
            control.Font = new Font(GetFontName(font), control.Font.Size);
        }
        public static void SetFont(Fonts font, Control control, float size)
        {
            control.Font = new Font(GetFontName(font), size);
        }
    }

    public static class WindowTracker
    {
        [DllImport("user32.dll")]
        private static extern bool IsWindowVisible(IntPtr hWnd);

        public static List<AppVentana> ObtenerVentanas()
        {
            List<AppVentana> lista = new List<AppVentana>();

            foreach (Process proceso in Process.GetProcesses())
            {
                IntPtr hWnd = proceso.MainWindowHandle;

                if (hWnd != IntPtr.Zero && IsWindowVisible(hWnd))
                {
                    try
                    {
                        // Este try/catch protege de procesos que no dejan acceder a info
                        string titulo = proceso.MainWindowTitle;
                        if (!string.IsNullOrWhiteSpace(titulo))
                        {
                            // Extra: descartar procesos suspendidos por bajo uso
                            if (proceso.Responding == false)
                                continue;

                            // A veces .Responding es true, pero aún así está en "Eco"
                            // puedes filtrar por título "Configuración" como workaround
                            if (titulo == "Configuración")
                                continue;

                            string exeName = Path.GetFileNameWithoutExtension(proceso.MainModule.FileName);
                            string[] procesosInternos =
                            {
                                "deneOS",
                                "explorerdna",
                                "tbar",
                                "Shell de experiencia de Windows",
                                "sm",
                                "volSlider",
                                "Host de experiencia del shell de Windows"
                            };
                            if (procesosInternos.Contains<string>(exeName))
                                continue;


                            Icon icono = null;

                            try
                            {
                                icono = Icon.ExtractAssociatedIcon(proceso.MainModule.FileName);
                            }
                            catch
                            {
                                icono = SystemIcons.Application;
                            }

                            lista.Add(new AppVentana
                            {
                                Titulo = titulo,
                                Icono = icono,
                                Proceso = proceso
                            });
                        }
                    }
                    catch
                    {
                        /* procesos protegidos o de sistema */
                    }
                }
            }

            return lista;
        }

    }