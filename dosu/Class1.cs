using NAudio.CoreAudioApi;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Runtime.InteropServices;
//using Math = System.Math;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using Sys = System;

namespace dosu;

public static class Utils
{/*
    private enum msgIcons {
        Error = 0x01, // Error
        Question = 0x02, // Question
        Warning = 0x03, // Warning
        Information = 0x04, // Information
        Stop = 0x05, // Stop
        Exclamation = 0x06, // Exclamation
        Hand = 0x07, // Hand
        Asterisk = 0x08, // Asterisk
        Application = 0x09, // Application
        User = 0x0A, // User
        Custom = 0x0B, // Custom
        None = 0x00, // No icon
        Success = 0x0C // Success
    }*/          
    public static int SysMsg(string msg, IntPtr level = 0x01, IntPtr Icon = 0xA0, string title = "", string pathToCustomIcon = "")
    {
        
        MessageBoxIcon icon;
        switch (Icon)
        {
            case 0x01:
                icon = MessageBoxIcon.Error;
                break;
            case 0x02:
                icon = MessageBoxIcon.Question;
                break;
            case 0x03:
                icon = MessageBoxIcon.Warning;
                break;
            case 0x04:
                icon = MessageBoxIcon.Information;
                break;
            case 0x05:
                icon = MessageBoxIcon.Stop;
                break;
            case 0x06:
                icon = MessageBoxIcon.Exclamation;
                break;
            case 0x07:
                icon = MessageBoxIcon.Hand;
                break;
            case 0x08:
                icon = MessageBoxIcon.Asterisk;
                break;
            case 0x09:
                switch (level)
                {
                    case 0x01:
                        icon = MessageBoxIcon.Information;
                        break;
                    case 0x02:
                        icon = MessageBoxIcon.Warning;
                        break;
                    case 0x03:
                        icon = MessageBoxIcon.Error;
                        break;
                    default:
                        icon = MessageBoxIcon.None;
                        break;
                }
                break;
            case 0x0A:
                //icon = MessageBoxIcon.User;
                throw new NotImplementedException();
            case 0x0B:
                var icon_ = new Icon(pathToCustomIcon);
                var _ = CustomMessageBox.Show(msg, title, icon_);
                return (int)_;
            case 0x0C:
                icon = MessageBoxIcon.None; // Success does not have a specific icon in MessageBox
                break;
            default:
                icon = MessageBoxIcon.None;
                break;
        }
        var __ = MessageBox.Show(msg, title, MessageBoxButtons.OK, icon);
        return (int)__;
    }
    public static class deneOSVersion
    {
        public class ShortVersion
        {
            public enum Formats
            {
                /// <summary>
                /// vMajor.Minor.Tag (ex: v1.0.r, v0.2.b, v0.7.a)
                /// </summary>
                vM_m_x,
                /// <summary>
                /// vMajor.Minor (ex: v1.0, v0.2, v0.7)
                /// </summary>
                vM_m,
                /// <summary>
                /// vMajor (ex: v1, v0)
                /// </summary>
                vM,
                /// <summary>
                /// vMinor (ex: v0, v2, v7)
                /// </summary>
                vm,
                /// <summary>
                /// Major.Minor.Tag (ex: 1.0.r, 0.2.b, 0.7.a)
                /// </summary>
                M_m_x,
                /// <summary>
                /// Major.Minor (ex: 1.0, 0.2, 0.7)
                /// </summary>
                M_m,
                /// <summary>
                /// Tag (ex: r, b, a)
                /// </summary>
                x,
                /// <summary>
                /// Major (ex: 1, 0)
                /// </summary>
                M,
                /// <summary>
                /// Minor (ex: 0, 2, 7)
                /// </summary>
                m,
                /// <summary>
                /// vMajor.MinorTag (ex: v1.0b, v0.2, v0.7a) || Note: No tag if Release
                /// </summary>
                vM_mx,
                /// <summary>
                /// Major.MinorTag (ex: 1.0b, 0.2, 0.7a) || Note: No tag if Release
                /// </summary>
                M_mx
            }

            public static string GetVersion(Formats format)
            {
                switch (format)
                {
                    case Formats.vM_m_x:
                    default:
                        return $"v{majorVersion}.{minorVersion}.{versionShortTagA}";
                    case Formats.vM_m:
                        return $"v{majorVersion}.{minorVersion}";
                    case Formats.vM:
                        return $"v{majorVersion}";
                    case Formats.vm:
                        return $"v{minorVersion}";
                    case Formats.M_m_x:
                        return $"{majorVersion}.{minorVersion}.{versionShortTagA}";
                    case Formats.M_m:
                        return $"{majorVersion}.{minorVersion}";
                    case Formats.x:
                        return $"{versionShortTagA}";
                    case Formats.M:
                        return $"{majorVersion}";
                    case Formats.m:
                        return $"{minorVersion}";
                    case Formats.vM_mx:
                        return $"v{majorVersion}.{minorVersion}{versionShortTagB}";
                    case Formats.M_mx:
                        return $"{majorVersion}.{minorVersion}{versionShortTagB}";
                }
            }
        }
        private static readonly int majorVersion = FileVersionInfo.GetVersionInfo(@"C:\DENEOS\core\deneOS.exe").FileMajorPart;
        private static readonly int minorVersion = FileVersionInfo.GetVersionInfo(@"C:\DENEOS\core\deneOS.exe").FileMinorPart;
        private static readonly int buildVersion = FileVersionInfo.GetVersionInfo(@"C:\DENEOS\core\deneOS.exe").FileBuildPart;
        private static readonly string versionTag = buildVersion == 0 ? "Release" : (buildVersion == 1 ? "Beta" : "Alpha");
        private static readonly char versionShortTagA = buildVersion == 0 ? 'r' : (buildVersion == 1 ? 'b' : 'a');
        private static readonly char versionShortTagB = buildVersion == 0 ? char.MinValue : (buildVersion == 1 ? 'b' : 'a');
        public static readonly string Version = $"{majorVersion}.{minorVersion}";
        public static readonly string Build = DateTime.Now.ToString("yyyyMMdd");

        public static string GetVersionInfo() =>
            $"deneOS {Version} - {versionTag} Channel";
    }
    public static class BootAnims {
        public class Animate
        {
            private readonly string[] bootAnillas =
            [
                "\uE052", "\uE053", "\uE054", "\uE055", "\uE056", "\uE057", "\uE058", "\uE059",
                "\uE05A", "\uE05B", "\uE05C", "\uE05D", "\uE05E", "\uE05F", "\uE060", "\uE061",
                "\uE062", "\uE063", "\uE064", "\uE065", "\uE066", "\uE067", "\uE068", "\uE069",
                "\uE06A", "\uE06B", "\uE06C", "\uE06D", "\uE06E", "\uE06F", "\uE070", "\uE071",
                "\uE072", "\uE073", "\uE074", "\uE075", "\uE076", "\uE077", "\uE078", "\uE079",
                "\uE07A", "\uE07B", "\uE07C", "\uE07D", "\uE07E", "\uE07F", "\uE080", "\uE081",
                "\uE082", "\uE083", "\uE084", "\uE085", "\uE086", "\uE087", "\uE088", "\uE089",
                "\uE08A", "\uE08B", "\uE08C", "\uE08D", "\uE08E", "\uE08F", "\uE090", "\uE091",
                "\uE092", "\uE093", "\uE094", "\uE095", "\uE096", "\uE097", "\uE098", "\uE099",
                "\uE09A", "\uE09B", "\uE09C", "\uE09D", "\uE09E", "\uE09F", "\uE0A0", "\uE0A1",
                "\uE0A2", "\uE0A3", "\uE0A4", "\uE0A5", "\uE0A6", "\uE0A7", "\uE0A8", "\uE0A9",
                "\uE0AA", "\uE0AB", "\uE0AC", "\uE0AD", "\uE0AE", "\uE0AF", "\uE0B0", "\uE0B1",
                "\uE0B2", "\uE0B3", "\uE0B4", "\uE0B5", "\uE0B6", "\uE0B7", "\uE0B8", "\uE0B9",
                "\uE0BA", "\uE0BB", "\uE0BC", "\uE0BD", "\uE0BE", "\uE0BF", "\uE0C0", "\uE0C1",
                "\uE0C2", "\uE0C3", "\uE0C4", "\uE0C5", "\uE0C6", "\uE0C7"
            ];
            private int bootIndex;
            private readonly Sys.Windows.Forms.Timer bootTimer;
            private readonly int secondsx;
            private int secondsy;
            private readonly Label boot;
            public Animate(int seconds, Label bootText, Action? action)
            {
                if (seconds <= 0)
                    throw new ArgumentException("La duración debe ser mayor que cero.");
                BootCompleted = action!;
                secondsx = seconds;
                bootTimer = new Sys.Windows.Forms.Timer();
                boot = bootText;
                boot = bootText;

                if (boot.Font.FontFamily.Name != "Segoe Boot Semilight")
                {
                    throw new InvalidOperationException("La fuente del Label debe ser 'Segoe Boot Semilight'.");
                } 
            }
            public void StartBoot()
            {
                bootTimer.Interval = 30; // Puedes ajustar la velocidad aquí
                bootTimer.Tick += BootTimer_Tick;
                bootTimer.Start();
            }
            private readonly Action BootCompleted;

            private void BootTimer_Tick(object? sender, EventArgs e)
            { 
                if (secondsy <= secondsx)
                {
                    boot.Text = bootAnillas[bootIndex];
                    bootIndex = (bootIndex + 1) % bootAnillas.Length;
                    secondsy++;
                }
                else
                {
                    bootTimer.Stop();
                    BootCompleted.Invoke(); // Llama la acción si está asignada
                }
            }
        }
    }
}

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

    [AllowNull] public sealed override string Text
    {
        get => base.Text;
        set => base.Text = value;
    }

    public static DialogResult Show(string message, string title, Icon customIcon)
    {
        using var box = new CustomMessageBox(message, title, customIcon);
        return box.ShowDialog();
    }
}

public static class Paths
{
    public static string DeneOSCorePath => @"C:\DENEOS\core";
    public static string DeneOSSystemAppsPath => @"C:\DENEOS\systemApps";
    public static string DeneOSUserPath => @"C:\DNUSR";
    public static string DeneOSSystemConfPath => @"C:\DENEOS\sysconf";
    public static string DeneOSLangPath => @"C:\DENEOS\lang";
}

public static class UniversalConfiguration
{
    public static string GetLang()
    {
        string langFilePath = Path.Combine(Paths.DeneOSSystemConfPath, "lang.ini");
        if (File.Exists(langFilePath))
        {
            try
            {
                string[] lines = File.ReadAllLines(langFilePath);
                if (lines.Length > 1)
                {
                    return lines[1].Trim(); // Retorna el segundo elemento que es el idioma
                }
            }
            catch (Exception ex)
            {
                Utils.SysMsg($"{(string)MUI.T("errreadlangcfgfile")}{ex.Message}", 0x01, 0x01, (string)MUI.T("cfgerr"));
            }
        }
        return Sys.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName.ToLower().TrimEnd();
    }

    public static void SetLang(string lang)
    {
        string langFilePath = Path.Combine(Paths.DeneOSSystemConfPath, "lang.ini");
        try
        {
            File.WriteAllLines(langFilePath, new[] { "[Language]", lang });
        }
        catch (Exception ex)
        {
            Utils.SysMsg($"{MUI.T("errwritlangcfgfile")}{ex.Message}", 0x01, 0x01, (string)MUI.T("cfgerr"));
        }
    }

    public struct User
    {
        public string Username;
        public string Password;
    }

    public static User GetUser()
    {
        string userFilePath = Path.Combine(Paths.DeneOSSystemConfPath, "config.ini");
        if (File.Exists(userFilePath))
        {
            try
            {
                string[] lines = File.ReadAllLines(userFilePath);
                if (lines.Length >= 3)
                {
                    return new User
                    {
                        Username = lines[1].Trim()[11..],
                        Password = lines[2].Trim()[11..]
                    };
                }
            }
            catch (Exception ex)
            {
                Utils.SysMsg($"{(string)MUI.T("errreadlangcfgfile")}{ex.Message}", 0x01, 0x01, "Error de Usuario");
            }
        }
        return new User { Username = "", Password = "" }; // Usuario por defecto si no se encuentra el archivo o hay un error
    }

    public static void SetUser(User user)
    {
        string userFilePath = Path.Combine(Paths.DeneOSSystemConfPath, "config.ini");
        try
        {
            File.WriteAllLines(userFilePath, new[] 
            { 
                "[deneOS]", 
                "username = " + user.Username, 
                "password = " + user.Password 
            });
        }
        catch (Exception ex)
        {
            Utils.SysMsg($"Error al escribir el archivo de usuario: {ex.Message}", 0x01, 0x01, "Error de Usuario");
        }
    }

    public static void ResetUser()
    {
        string userFilePath = Path.Combine(Paths.DeneOSSystemConfPath, "config.ini");
        try
        {
            if (File.Exists(userFilePath))
            {
                File.Delete(userFilePath);
            }
        }
        catch (Exception ex)
        {
            Utils.SysMsg($"Error al eliminar el archivo de usuario: {ex.Message}", 0x01, 0x01, "Error de Usuario");
        }
    }
        
    public static string GetWallpaper() 
    {
        const string registryPath = @"HKEY_CURRENT_USER\Software\deneOS\desktop\wallpaper";
        try
        {
            object value = Microsoft.Win32.Registry.GetValue(registryPath, "", null);
            if (value != null)
            {
                return value.ToString()!;
            }
        }
        catch (Exception ex)
        {
            Utils.SysMsg($"Error al leer el wallpaper del registro: {ex.Message}", 0x01, 0x01, "Error de Configuración");
        }
        return @"c:\windows\web\4k\Wallpaper\windows\img0_1920x1200.jpg"; // Ruta por defecto
    }

    public static void SetWallpaper(string wallpaperPath)
    {
        string registryPath = @"HKEY_CURRENT_USER\Software\deneOS\desktop\wallpaper";
        try
        {
            Microsoft.Win32.Registry.SetValue(registryPath, "", wallpaperPath);
        }
        catch (Exception ex)
        {
            Utils.SysMsg($"Error al escribir el wallpaper en el registro: {ex.Message}", 0x01, 0x01, "Error de Configuración");
        }
    }

    public static bool ShowingIconsOnDesktop()
    {
        string registryPath = @"HKEY_CURRENT_USER\Software\deneOS\desktop\showIcons";
        try
        {
            object value = Microsoft.Win32.Registry.GetValue(registryPath, "", null);
            if (value != null)
            {
                return Convert.ToBoolean(value);
            }
        }
        catch (Exception ex)
        {
            Utils.SysMsg($"Error al leer la configuración de iconos del escritorio: {ex.Message}", 0x01, 0x01, "Error de Configuración");
        }
        return true; // Por defecto mostrar iconos
    }

    public static void SetShowingIconsOnDesktop(bool showIcons)
    {
        string registryPath = @"HKEY_CURRENT_USER\Software\deneOS\desktop\showIcons";
        try
        {
            Microsoft.Win32.Registry.SetValue(registryPath, "", showIcons);
        }
        catch (Exception ex)
        {
            Utils.SysMsg($"Error al escribir la configuración de iconos del escritorio: {ex.Message}", 0x01, 0x01, "Error de Configuración");
        }
    }

    public static class Region
    {
        public enum Regions
        {
            ES,
            US,
            AD,
            EN,
            DE,
            FR,
            System
        }
        public static string RegionToString(Regions region)
        {
            return region.ToString().ToLower();
        }
        public static Regions StringToRegion(string regionStr)
        {
            return regionStr.ToUpper() switch
            {
                "ES" => Regions.ES,
                "US" => Regions.US,
                "AD" => Regions.AD,
                "EN" => Regions.EN,
                "DE" => Regions.DE,
                "FR" => Regions.FR,
                _ => Regions.System,
            };
        }
        public static Regions GetRegion()
        {
            const string registryPath = @"HKEY_CURRENT_USER\Software\deneOS\system\region";
            try
            {
                object value = Microsoft.Win32.Registry.GetValue(registryPath, "", null);
                if (value != null)
                {
                    return StringToRegion(value.ToString()!);
                }
            }
            catch (Exception ex)
            {
                Utils.SysMsg($"Error al leer la configuración de región del registro: {ex.Message}", 0x01, 0x01, "Error de Configuración");
            }
            return Regions.System; // Por defecto usar región del sistema
        }
        public static void SetRegion(Regions region)
        {
            string registryPath = @"HKEY_CURRENT_USER\Software\deneOS\system\region";
            try
            {
                Microsoft.Win32.Registry.SetValue(registryPath, "", RegionToString(region));
            }
            catch (Exception ex)
            {
                Utils.SysMsg($"Error al escribir la configuración de región en el registro: {ex.Message}", 0x01, 0x01, "Error de Configuración");
            }
        }
        public static Regions getFromSystem()
        {
            string systemRegion = Sys.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName.ToUpper();
            return StringToRegion(systemRegion);
        }
        public static string getFromSystemSUp()
        {
            string systemRegion = Sys.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName.ToUpper();
            return systemRegion;
        }
        public static string getFromSystemSLo()
        {
            string systemRegion = Sys.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName.ToLower();
            return systemRegion;
        }
    }
}

public static class UI
{
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
            int code = int.Parse(glyph, Sys.Globalization.NumberStyles.HexNumber);
            char unicode = (char)code;
            control.Text = unicode.ToString();
        }
        public static char GetGlyph(AvailableFonts font, string glyph)
        {
            int code = int.Parse(glyph, Sys.Globalization.NumberStyles.HexNumber);
            char unicode = (char)code;
            return unicode;
        }
        public static string GetGlyphAsString(AvailableFonts font, string glyph)
        {
            return GetGlyph(font, glyph).ToString();
        }
        public static List<AvailableFonts> GetFonts() {
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
                Fonts.AgencyFB =>           "Agency FB",
                Fonts.AgencyFBBold =>       "Agency FB",
                Fonts.ProductSansBold =>    "Product Sans",
                Fonts.ProductSansBoldItalic=>"Product Sans",
                Fonts.ProductSansItalic =>  "Product Sans",
                Fonts.ProductSans =>        "Product Sans",
                Fonts.SegoeUIVD =>          "Segoe UI Variable Display",
                Fonts.SegoeUIVDLight =>     "Segoe UI Variable Display Light",
                Fonts.SegoeUIVDSemibold =>  "Segoe UI Variable Display Semib",
                Fonts.SegoeUIVDSemilight => "Segoe UI Variable Display Semil",
                Fonts.SegoeUIVS =>          "Segoe UI Variable Small",
                Fonts.SegoeUIVSLight =>     "Segoe UI Variable Small Light",
                Fonts.SegoeUIVSSemibold =>  "Segoe UI Variable Small Semibol",
                Fonts.SegoeUIVSSemilight => "Segoe UI Variable Small Semilig",
                Fonts.SegoeUIVT =>          "Segoe UI Variable Text",
                Fonts.SegoeUIVTLight =>     "Segoe UI Variable Text Light",
                Fonts.SegoeUIVTSemibold =>  "Segoe UI Variable Text Semibold",
                Fonts.SegoeUIVTSemilight => "Segoe UI Variable Text Semiligh",
                _ =>                        "Segoe UI Variable Text"
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
                            string[] procesosInternos = { 
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
                    catch { /* procesos protegidos o de sistema */ }
                }
            }

            return lista;
        }

    }

}
public static class Audio
{
    public class VolumeManager
    {
        public static int GetSystemVolume()
        {
            var enumerator = new MMDeviceEnumerator();
            var device = enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
            return (int)(device.AudioEndpointVolume.MasterVolumeLevelScalar * 100);
        }
        public static void SetSystemVolume(float level) // de 0.0f a 1.0f
        {
            var deviceEnumerator = new MMDeviceEnumerator();
            var device = deviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
            device.AudioEndpointVolume.MasterVolumeLevelScalar = level;
        }
        public void ChangeVolume(float delta)
        {
            var deviceEnumerator = new MMDeviceEnumerator();
            var device = deviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
            float newVol = Sys.Math.Clamp(device.AudioEndpointVolume.MasterVolumeLevelScalar + delta, 0f, 1f);
            device.AudioEndpointVolume.MasterVolumeLevelScalar = newVol;
        }

    }
}
public static class MUI
{
    private static Dictionary<string, string> traducciones = new Dictionary<string, string>();
    public static bool UN_ST = false;
    public static void Cargar(string idioma)
    {
        UN_ST = false;
        string ruta = Path.Combine(@"C:\DENEOS\", "lang", $"{idioma}.json");
        string lang = idioma;
        string localPath = $@"C:\DENEOS\lang\{lang}.json";



        string remoteUrl = $"https://repoficialx.xyz/deneOS/api/{lang}.json";

        // Descarga desde internet si hay diferencias
        if (NeedsUpdate(localPath, remoteUrl))
        {
            File.WriteAllText(localPath, new WebClient().DownloadString(remoteUrl));
            MessageBox.Show("🔄 Traducciones actualizadas correctamente.", "Actualización de idioma");
        }

        if (File.Exists(ruta))
        {
            string json = File.ReadAllText(ruta);
            traducciones = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
        }
        else
        {
            traducciones = new Dictionary<string, string>(); // vacío si no existe  
        }
    }
    static bool NeedsUpdate(string localPath, string remoteUrl)
    {
        string remoteContent = null;
        using (WebClient wc = new WebClient())
        {
            remoteContent = wc.DownloadString(remoteUrl);
        }
        var localContent = File.ReadAllText(localPath);
        return remoteContent != localContent;
    }
    public static object T(string clave)
    {
        if (UN_ST)
        {
            return clave;
        }
        if (traducciones.TryGetValue(clave, out var valor))
        {
            // Intentar interpretar el valor como booleano
            if (bool.TryParse(valor, out bool boolResult))
            {
                return boolResult;
            }

            // Intentar interpretar el valor como número (opcional)
            if (int.TryParse(valor, out int intResult))
            {
                return intResult;
            }

            // Si no es booleano ni número, devolver como cadena
            return valor;
        }
        //MessageBox.Show($"nº trads: {traducciones.Count}, estado sisfct: {traducciones.ContainsKey("sisfct")}");

        return $"[{clave}]"; // Clave no encontrada
    }

}

public static class Math
{
    /// <summary>
    /// Represents a single binary value that can be either 0 or 1, providing type safety and convenient conversions
    /// between Boolean and bit representations.
    /// </summary>
    /// <remarks>The Bit struct is useful when a value must be explicitly stored as a bit (0 or 1)
    /// rather than a Boolean, such as for interoperability with binary data or low-level protocols. It supports
    /// implicit conversions to and from Boolean and integer types for ease of use in expressions and assignments.
    /// Bit is a value type and does not allocate heap memory. Using <code>using bit = dosu.Math.Bit;</code> is recommended.</remarks>
    public struct Bit
    {
        private byte value; // 0 o 1

        // Constructor desde bool
        /// <summary>
        /// Initializes a new instance of the Bit class using the specified Boolean value.
        /// </summary>
        /// <param name="b">The Boolean value to represent. <see langword="true"/> creates a Bit with value 1; <see
        /// langword="false"/> creates a Bit with value 0.</param>
        public Bit(bool b) => value = (byte)(b ? 1 : 0);

        // Conversiones implícitas
        /// <summary>
        /// Defines an implicit conversion from a Bit to a Boolean value.
        /// </summary>
        /// <remarks>This operator enables a Bit instance to be used in Boolean expressions. The
        /// conversion returns <see langword="true"/> if the Bit represents a nonzero value; otherwise, it returns
        /// <see langword="false"/>.</remarks>
        /// <param name="b"></param>
        public static implicit operator bool(Bit b) => b.value != 0;

        /// <summary>
        /// Defines an implicit conversion from a Boolean value to a Bit instance.
        /// </summary>
        /// <remarks>This operator enables assigning a Boolean value directly to a Bit variable
        /// without explicit casting. The resulting Bit will represent the same logical value as the input
        /// Boolean.</remarks>
        /// <param name="b">The Boolean value to convert to a Bit.</param>
        public static implicit operator Bit(bool b) => new Bit(b);

        // Conversion implícita desde int
        /// <summary>
        /// Defines an implicit conversion from an integer to a Bit instance.
        /// </summary>
        /// <remarks>This conversion allows an integer to be assigned directly to a Bit. The
        /// conversion treats zero as false and any nonzero value as true, following common C# conventions for
        /// boolean conversion.</remarks>
        /// <param name="i">The integer value to convert. A value of 0 is converted to a Bit representing false; any other value is
        /// converted to a Bit representing true.</param>
        public static implicit operator Bit(int i) => new Bit(i != 0);

        /// <summary>
        /// Returns a string that represents the boolean value of this instance.
        /// </summary>
        /// <returns>A string representation of the boolean value; either "True" or "False".</returns>
        public override string ToString() => (value != 0).ToString();

        // Métodos de ayuda
        /// <summary>
        /// Sets the value using a Boolean representation.
        /// </summary>
        /// <param name="b">The Boolean value to assign. If <see langword="true"/>, the value is set to 1; otherwise, it is set to
        /// 0.</param>
        public void Set(bool b) => value = (byte)(b ? 1 : 0);

        /// <summary>
        /// Toggles the current value between two states.
        /// </summary>
        /// <remarks>This method switches the value from one state to the other, such as from
        /// enabled to disabled or vice versa. The specific meaning of each state depends on the context in which
        /// this method is used.</remarks>
        public void Toggle() => value ^= 1;
    }
    public static class Integers
    {
        #region MaxValues
        public static readonly Bit MaxValue1 = 1;
        public static readonly sbyte MaxValue8 = 127;
        public static readonly byte MaxValueU8 = 255;
        public static readonly short MaxValue16 = 32767;
        public static readonly ushort MaxValueU16 = 65535;
        public static readonly int MaxValue32 = 2147483647;
        public static readonly uint MaxValueU32 = 4294967295;
        public static readonly long MaxValue64 = 9223372036854775807;
        public static readonly ulong MaxValueU64 = 18446744073709551615;
        public static readonly Int128 MaxValue128 = Int128.Parse("170141183460469231731687303715884105727");
        public static readonly UInt128 MaxValueU128 = UInt128.Parse("340282366920938463463374607431768211455");
        #endregion
        #region MinValuues
        public static readonly Bit MinValue1 = 0;
        public static readonly sbyte MinValue8 = -128;
        public static readonly byte MinValueU8 = 0;
        public static readonly short MinValue16 = -32768;
        public static readonly ushort MinValueU16 = 0;
        public static readonly int MinValue32 = -2147483648;
        public static readonly uint MinValueU32 = 0;
        public static readonly long MinValue64 = -9223372036854775808;
        public static readonly ulong MinValueU64 = 0;
        public static readonly Int128 MinValue128 = Int128.Parse("-170141183460469231731687303715884105728");
        public static readonly UInt128 MinValueU128 = UInt128.Parse("0");
        #endregion
        /// <summary>
        /// Provides constants representing powers of two as integer and long values for use in bitwise operations
        /// and calculations.
        /// </summary>
        /// <remarks>This class offers predefined fields for powers of two from 2^0 up to 2^62, as
        /// well as maximum values for 32-bit and 64-bit signed integers. It is intended to simplify bit
        /// manipulation and flag operations by providing named constants for common binary values. The class is
        /// static and cannot be instantiated.</remarks>
        public static class BinaryPowers
        {
            #region Int32
            public static readonly int _0 = 1;
            public static readonly int _1 = 2;
            public static readonly int _2 = 4;
            public static readonly int _3 = 8;
            public static readonly int _4 = 16;
            public static readonly int _5 = 32;
            public static readonly int _6 = 64;
            public static readonly int _7 = 128;
            public static readonly int _8 = 256;
            public static readonly int _9 = 512;
            public static readonly int _10 = 1024;
            public static readonly int _11 = 2048;
            public static readonly int _12 = 4096;
            public static readonly int _13 = 8192;
            public static readonly int _14 = 16384;
            public static readonly int _15 = 1 << 15;
            public static readonly int _16 = 1 << 16;
            public static readonly int _17 = 1 << 17;
            public static readonly int _18 = 1 << 18;
            public static readonly int _19 = 1 << 19;
            public static readonly int _20 = 1 << 20;
            public static readonly int _21 = 1 << 21;
            public static readonly int _22 = 1 << 22;
            public static readonly int _23 = 1 << 23;
            public static readonly int _24 = 1 << 24;
            public static readonly int _25 = 1 << 25;
            public static readonly int _26 = 1 << 26;
            public static readonly int _27 = 1 << 27;
            public static readonly int _28 = 1 << 28;
            public static readonly int _29 = 1 << 29;
            public static readonly int _30 = 1 << 30;
            public static readonly int _max32 = 2147483647;
            #endregion
            #region Int64
            public static readonly long _31 = 1L << 31;
            public static readonly long _32 = 1L << 32;
            public static readonly long _33 = 1L << 33;
            public static readonly long _34 = 1L << 34;
            public static readonly long _35 = 1L << 35;
            public static readonly long _36 = 1L << 36;
            public static readonly long _37 = 1L << 37;
            public static readonly long _38 = 1L << 38;
            public static readonly long _39 = 1L << 39;
            public static readonly long _40 = 1L << 40;
            public static readonly long _41 = 1L << 41;
            public static readonly long _42 = 1L << 42;
            public static readonly long _43 = 1L << 43;
            public static readonly long _44 = 1L << 44;
            public static readonly long _45 = 1L << 45;
            public static readonly long _46 = 1L << 46;
            public static readonly long _47 = 1L << 47;
            public static readonly long _48 = 1L << 48;
            public static readonly long _49 = 1L << 49;
            public static readonly long _50 = 1L << 50;
            public static readonly long _51 = 1L << 51;
            public static readonly long _52 = 1L << 52;
            public static readonly long _53 = 1L << 53;
            public static readonly long _54 = 1L << 54;
            public static readonly long _55 = 1L << 55;
            public static readonly long _56 = 1L << 56;
            public static readonly long _57 = 1L << 57;
            public static readonly long _58 = 1L << 58;
            public static readonly long _59 = 1L << 59;
            public static readonly long _60 = 1L << 60;
            public static readonly long _61 = 1L << 61;
            public static readonly long _62 = 1L << 62;
            public static readonly long _max64 = 9223372036854775807;
            #endregion
            /// <summary>
            /// Calculates 2 raised to the specified exponent as a 64-bit integer.
            /// </summary>
            /// <param name="exponent">The exponent to raise 2 to. Must be in the range 0 to 62, inclusive.</param>
            /// <returns>A 64-bit integer equal to 2 raised to the power of the specified exponent.</returns>
            /// <exception cref="ArgumentOutOfRangeException">Thrown when exponent is less than 0 or greater than 62.</exception>
            public static long bPower(int exponent)
            {
                if (exponent<0 || exponent>62)
                {
                    throw new ArgumentOutOfRangeException(nameof(exponent));
                }
                return 1L << exponent;
            }
        }
    }
}

public static class Food
{
    public class FInfo
    {
        public string Name { get; set; }

        public string Brand { get; set; }

        public string ImageURL { get; set; }

        public string IngredientsText { get; set; }

        public List<string> Additives { get; set; }

        public List<string> Allergens { get; set; }

        public List<string> Tags { get; set; }

        public float SugarPer100g { get; set; }

        public float FatPer100g { get; set; }

        public float SaltPer100g { get; set; }

        public float EnergyKcal { get; set; }

        public string NutriScore { get; set; }

        public string EcoScore { get; set; }

        public string Barcode { get; set; }
    }

    public static async Task<FInfo?> GetFoodInfo(string barcode)
    {
        string url = "https://world.openfoodfacts.org/api/v2/product/" + barcode;
        using HttpClient client = new HttpClient();
        try
        {
            JsonNode product = JsonNode.Parse(await client.GetStringAsync(url))?["product"];
            if (product == null)
            {
                return null;
            }
            return new FInfo
            {
                Name = (((string?)product["abbreviated_product_name"]) ?? ((string?)product["product_name"]) ?? "Desconocido"),
                Brand = (((string?)product["brands"]) ?? "Sin marca"),
                ImageURL = (string?)product["image_front_url"],
                IngredientsText = (((string?)product["ingredients_text"]) ?? ""),
                Additives = ((from x in product["additives_tags"]?.AsArray()
                    select x.ToString()).ToList() ?? new List<string>()),
                Allergens = ((from x in product["allergens_tags"]?.AsArray()
                    select x.ToString()).ToList() ?? new List<string>()),
                Tags = ((from x in product["ingredients_analysis_tags"]?.AsArray()
                    select x.ToString()).ToList() ?? new List<string>()),
                SugarPer100g = ((float?)product["nutriments"]?["sugars_100g"]).GetValueOrDefault(),
                FatPer100g = ((float?)product["nutriments"]?["fat_100g"]).GetValueOrDefault(),
                SaltPer100g = ((float?)product["nutriments"]?["salt_100g"]).GetValueOrDefault(),
                EnergyKcal = ((float?)product["nutriments"]?["energy-kcal_100g"]).GetValueOrDefault(),
                NutriScore = (((string?)product["nutriscore_grade"]) ?? "?"),
                EcoScore = (((string?)product["ecoscore_grade"]) ?? "?"),
                Barcode = barcode
            };
        }
        catch
        {
            return null;
        }
    }
}

public static class InternalAppsExec
{
    public static class InternalUseApps
    {
        public class setConfig : IApplication
        {
            public string appLocation { get; } = @"C:\DENEOS\core\setConfig.exe";
            public string arguments { get; set; } = "";
            public bool useArgs { get; set; } = false;
            public void Execute(string[]? args = null)
            {
                useArgs = args is { Length: > 0 };
                arguments = arguments = useArgs ? string.Join(" ", args) : this.arguments;
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = appLocation,
                    Arguments = arguments,
                    UseShellExecute = true,
                    CreateNoWindow = true,
                    Verb = "runas"
                };
                Process.Start(startInfo);
            }
        }

        private interface IApplication
        {
            public string arguments { get; set; }
            public string appLocation { get; }
            public bool useArgs { get; set; }
            public void Execute(string[]? args = null);
        }
        public class AboutDialogs : IApplication
        {
            public string appLocation { get; } = @"C:\DENEOS\core\AboutDialogs.exe";
            public string arguments { get; set; } = "";
            public bool useArgs { get; set; } = true;
            public void Execute(string[]? appName)
            {
                if (appName == null || appName.Length == 0)
                    appName = ["dOSU"];

                arguments = appName.ToString();
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = appLocation,
                    Arguments = arguments,
                    UseShellExecute = true,
                    Verb = "runas"
                };
                Process.Start(startInfo);
            }
        }

        public class dpkxt : IApplication
        {
            public string appLocation { get; } = @"C:\DENEOS\core\dpkxt.exe";
            public string arguments { get; set; } = "";
            public bool useArgs { get; set; }

            public void Execute(string[]? args = null)
            {
                useArgs = args is { Length: > 0 };
                if (!useArgs)
                {
                    throw new ArgumentException("Arguments required to execute dpkxt.");
                }

                arguments = string.Join(" ", args);
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = appLocation,
                    Arguments = arguments,
                    UseShellExecute = true,
                    Verb = "runas"
                };
                Process.Start(startInfo);
            }
        }
    }

    public static class OpenUseApps
    {
        public enum ControlCenterOptions
        {
            InstallUpdate,
            GotoScreen,
            GotoAdvanced,
            GotoGeneral,
            GotoSoftware,
            GotoHome,
            GotoAbout,
            GotoCustom,
            _default
        }
        public static void OpenControlCenter(ControlCenterOptions option = ControlCenterOptions._default, string? args = null)
        {
            const string ccPath = @"C:\DENEOS\systemApps\controlcenter\controlcenter.exe";
            var argument = getArgumentForOption(option, args);

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = ccPath,
                UseShellExecute = true,
                Arguments = argument,
                Verb = "runas"
            };

            Process.Start(startInfo);
        }
        private static string getArgumentForOption(ControlCenterOptions option, string? args = null)
        {
            if (option == ControlCenterOptions.InstallUpdate && string.IsNullOrEmpty(args))
                throw new ArgumentException("Arguments required for InstallUpdate option.");

            return option switch
            {
                ControlCenterOptions.InstallUpdate => "/installUpdate:"+args,
                ControlCenterOptions.GotoScreen => "page:screen",
                ControlCenterOptions.GotoAdvanced => "page:advanced",
                ControlCenterOptions.GotoGeneral => "page:general",
                ControlCenterOptions.GotoSoftware => "page:software",
                ControlCenterOptions.GotoHome => "page:home",
                ControlCenterOptions.GotoAbout => "page:about",
                ControlCenterOptions.GotoCustom => "page:custom",
                _ => "page:home"
            };
        }
        public static void OpenDeneFiles()
        {
            const string dfPath = @"C:\DENEOS\systemApps\denefiles\denefiles.exe";
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = dfPath,
                UseShellExecute = true,
                Verb = "runas"
            };
            Process.Start(startInfo);
        }
        public static void OpenDeneStore()
        {
            const string dsPath = @"C:\DENEOS\systemApps\denestore\denestore.exe";
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = dsPath,
                UseShellExecute = true,
                Verb = "runas"
            };
            Process.Start(startInfo);
        }
        public static void OpenDeneNotes()
        {
            const string dnPath = @"C:\DENEOS\systemApps\denenotes\denenotes.exe";
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = dnPath,
                UseShellExecute = true,
                Verb = "runas"
            };
            Process.Start(startInfo);
        }
        public static void OpenDeneTerm()
        {
            const string dtPath = @"C:\DENEOS\systemApps\deneterm\deneterm.exe";
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = dtPath,
                UseShellExecute = true,
                Verb = "runas"
            };
            Process.Start(startInfo);
        }
    }
}