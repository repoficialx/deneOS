using NAudio.CoreAudioApi;
using System.Data;
using System.Diagnostics;
using System.Net;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace dosu
{
    public static class Utils
    {
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
        }
        public static int SysMsg(string msg, IntPtr level = 0x01, IntPtr Icon = 0xA0, string title = "", string pathToCustomIcon = "")
        {
            MessageBoxIcon icon = MessageBoxIcon.None;
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
                    System.Drawing.Icon icon_ = new Icon(pathToCustomIcon);
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
            private static int majorVersion = FileVersionInfo.GetVersionInfo(@"C:\DENEOS\core\deneOS.exe").FileMajorPart;
            private static int minorVersion = FileVersionInfo.GetVersionInfo(@"C:\DENEOS\core\deneOS.exe").FileMinorPart;
            private static int buildVersion = FileVersionInfo.GetVersionInfo(@"C:\DENEOS\core\deneOS.exe").FileBuildPart;
            private static string versionTag = buildVersion == 0 ? "Release" : (buildVersion == 1 ? "Beta" : "Alpha");
            public static readonly string Version = $"{majorVersion}.{minorVersion}";
            public static readonly string Build = DateTime.Now.ToString("yyyyMMdd");

            public static string GetVersionInfo() =>
                $"deneOS {Version} - {versionTag} Channel";
        }
		public static class BootAnims {
            public class Animate
            {
                private string[] bootAnillas = new string[]
                {
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
                };
                private int bootIndex = 0;
                private System.Windows.Forms.Timer bootTimer;
                private int secondsx;
                private int secondsy = 0;
                private Label boot;
                public Animate(int seconds, Label bootText, Action? action)
                {
                    if (seconds <= 0)
                        throw new ArgumentException("La duración debe ser mayor que cero.");
                    BootCompleted = action;
                    secondsx = seconds;
                    bootTimer = new System.Windows.Forms.Timer();
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
                private Action BootCompleted;

                private void BootTimer_Tick(object sender, EventArgs e)
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
                        BootCompleted?.Invoke(); // Llama la acción si está asignada
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

        public static DialogResult Show(string message, string title, Icon customIcon)
        {
            using (var box = new CustomMessageBox(message, title, customIcon))
            {
                return box.ShowDialog();
            }
        }
    }

    public static class Initializers
    {
        public static void StartdOH(string args="")
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = @"C:\DENEOS\core\deneOS_Home.exe",
                Arguments = args,
                UseShellExecute = true,
                WorkingDirectory = @"C:\DENEOS\core",
                Verb = "runas" // Ejecutar como administrador
            };
            try
            {
                Process.Start(startInfo);
            }
            catch (Exception ex)
            {
                Utils.SysMsg($"Error al iniciar deneOS Home: {ex.Message}", 0x01, 0x01, "Error de Inicio");
            }
        }

        public static void StartCC()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = @"C:\DENEOS\systemApps\controlcenter.exe",
                UseShellExecute = true,
                WorkingDirectory = @"C:\DENEOS\systemApps",
                Verb = "runas" // Ejecutar como administrador
            };
            try
            {
                Process.Start(startInfo);
            }
            catch (Exception ex)
            {
                Utils.SysMsg($"Error al iniciar el Centro de Control: {ex.Message}", 0x01, 0x01, "Error de Inicio");
            }
        }

        public static void UpdatedOH(string url)
        {
            // Iniciar el Control Center con argumento /installUpdate: seguido de la URL de descarga
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = @"C:\DENEOS\systemApps\controlcenter.exe",
                Arguments = $"/installUpdate:{url}",
                UseShellExecute = true,
                WorkingDirectory = @"C:\DENEOS\systemApps",
                Verb = "runas" // Ejecutar como administrador
            };
            try
            {
                Process.Start(startInfo);
            }
            catch (Exception ex)
            {
                Utils.SysMsg($"Error al iniciar el Control Center para la actualización: {ex.Message}", 0x01, 0x01, "Error de Inicio");
            }
        }

        public static void StartDF(bool root = false, string path="USER")
        {
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = @"C:\DENEOS\systemApps\deneFiles\deneFiles.exe",
                UseShellExecute = true,
                Arguments = root ? "/dangerZone:enableRoot" : "",
                WorkingDirectory = @"C:\DENEOS\systemApps",
                Verb = "runas" // Ejecutar como administrador
            };
            try
            {
                Process.Start(psi);
            }
            catch (Exception ex)
            {
                Utils.SysMsg($"Error al iniciar deneFiles: {ex.Message}", 0x01, 0x01, "Error de Inicio");
            }
        }

        public static void StartDN()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = @"C:\DENEOS\systemApps\deneNotes\deneNotes.exe",
                UseShellExecute = true,
                WorkingDirectory = @"C:\DENEOS\systemApps",
                Verb = "runas" // Ejecutar como administrador
            };
            try
            {
                Process.Start(startInfo);
            }
            catch (Exception ex)
            {
                Utils.SysMsg($"Error al iniciar deneNotes: {ex.Message}", 0x01, 0x01, "Error de Inicio");
            }
        }

        public static void KilldOH()
        {
            try
            {
                Process[] processes = Process.GetProcessesByName("deneOS_Home");
                foreach (Process process in processes)
                {
                    process.Kill();
                }
            }
            catch (Exception ex)
            {
                Utils.SysMsg($"Error al cerrar deneOS Home: {ex.Message}", 0x01, 0x01, "Error de Cierre");
            }
        }

        public static void KillAll()
        {
            string[] processNames = {
                "deneOS_Home",
                "controlcenter",
                "deneFiles",
                "deneNotes"
            };
            foreach (string processName in processNames)
            {
                try
                {
                    Process[] processes = Process.GetProcessesByName(processName);
                    foreach (Process process in processes)
                    {
                        process.Kill();
                    }
                }
                catch (Exception ex)
                {
                    Utils.SysMsg($"Error al cerrar {processName}: {ex.Message}", 0x01, 0x01, "Error de Cierre");
                }
            }
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
                    Utils.SysMsg($"Error al leer el archivo de configuración de idioma: {ex.Message}", 0x01, 0x01, "Error de Configuración");
                }
            }
            return System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName.ToLower().TrimEnd();
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
                Utils.SysMsg($"Error al escribir el archivo de configuración de idioma: {ex.Message}", 0x01, 0x01, "Error de Configuración");
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
                            Username = lines[1].Trim(),
                            Password = lines[2].Trim()
                        };
                    }
                }
                catch (Exception ex)
                {
                    Utils.SysMsg($"Error al leer el archivo de usuario: {ex.Message}", 0x01, 0x01, "Error de Usuario");
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
                    "[deneOS Home]", 
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
            string registryPath = @"HKEY_CURRENT_USER\Software\deneOS\desktop\wallpaper";
            try
            {
                object value = Microsoft.Win32.Registry.GetValue(registryPath, "", null);
                if (value != null)
                {
                    return value.ToString();
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
                string registryPath = @"HKEY_CURRENT_USER\Software\deneOS\system\region";
                try
                {
                    object value = Microsoft.Win32.Registry.GetValue(registryPath, "", null);
                    if (value != null)
                    {
                        return StringToRegion(value.ToString());
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
                string systemRegion = System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName.ToUpper();
                return StringToRegion(systemRegion);
            }

            public static string getFromSystemSU()
            {
                string systemRegion = System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName.ToUpper();
                return systemRegion;
            }

            public static string getFromSystemSL()
            {
                string systemRegion = System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName.ToLower();
                return systemRegion;
            }
        }
    }

    public static class Power
    {
        public static class BatteryStatus
        {
            [DllImport("powrprof.dll", SetLastError = true)]
            static extern uint PowerGetActiveScheme(IntPtr UserRootPowerKey, out IntPtr ActivePolicyGuid);

            [DllImport("powrprof.dll", SetLastError = true)]
            static extern uint PowerReadACValue(
                IntPtr RootPowerKey,
                ref Guid SchemeGuid,
                ref Guid SubGroupOfPowerSettingsGuid,
                ref Guid PowerSettingGuid,
                out uint Type,
                IntPtr Buffer,
                ref uint BufferSize);

            static Guid GUID_POWER_SAVING = new Guid("DE830923-A562-41AF-A086-E3A2C6BAD2DA"); // ahorro de batería
            static Guid GUID_ENERGY_SAVER = new Guid("E69653CA-CF7F-4F05-AA73-CB833FA90AD4"); // modo de energía

            public static bool IsBatterySaverOn()
            {
                IntPtr pActiveScheme;
                uint res = PowerGetActiveScheme(IntPtr.Zero, out pActiveScheme);
                if (res != 0) return false;

                Guid activeScheme = (Guid)Marshal.PtrToStructure(pActiveScheme, typeof(Guid));
                uint valType = 0;
                uint buffSize = 4;

                // Esto se usa para leer el estado del ahorro
                uint result = PowerReadACValue(IntPtr.Zero,
                    ref activeScheme,
                    ref GUID_POWER_SAVING,
                    ref GUID_ENERGY_SAVER,
                    out valType,
                    IntPtr.Zero,
                    ref buffSize);

                return result == 0 && valType == 0; // Si devuelve 0 y tipo 0, normalmente es ON
            }
        }
    }
    public static class Network
    {
        public static class WiFiStatus
        {
            public static async Task<int> GetWifiSignalStrengthAsync()
            {
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = "netsh",
                    Arguments = "wlan show interfaces",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                try
                {
                    string output;
                    using (Process process = Process.Start(psi))
                    {
                        // Leer la salida de forma asíncrona para evitar el interbloqueo
                        output = await process.StandardOutput.ReadToEndAsync();
                        await Task.Run(() => process.WaitForExit());
                    }

                    Console.Write(output);
                    Match match = Regex.Match(output, @"Señal\s*:\s*(\d+)%");
                    if (match.Success)
                    {
                        int signalStrength = int.Parse(match.Groups[1].Value);
                        Console.WriteLine($"{T("wifistrength")} {signalStrength}%");
                        return signalStrength;
                    }
                    else
                    {
                        Console.WriteLine(T("nowifisignal"));
                        return -1;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al obtener la señal de WiFi: {ex.Message}");
                    return -1;
                }
            }
            private static object T(string v)

            {

                return MUI.T(v);

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
                int screenHeight = primaryScreen.Bounds.Height;

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
            private const int DesignWidth = 707;   // Ancho del PCWelcome en designer
            private const int DesignHeight = 403;  // Alto del PCWelcome en designer

            // Factor de escala base (1.0 = 100% DPI, 1.25 = 125% DPI)
            private const float DesignDpiScale = 1.25f; // ✅ Ajusta según tu DPI de diseño

            /// <summary>
            /// Obtiene el factor de escala actual del sistema
            /// </summary>
            private static float GetSystemDpiScale()
            {
                using (Graphics g = Graphics.FromHwnd(IntPtr.Zero))
                {
                    return g.DpiX / 96f; // 96 DPI = 100%, 120 DPI = 125%, etc.
                }
            }

            /// <summary>
            /// Escala un tamaño desde el diseño base al DPI actual
            /// </summary>
            static Size ScaleSize(Size originalSize)
            {
                float currentDpi = GetSystemDpiScale();
                float scaleFactor = currentDpi / DesignDpiScale;

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

                    // ✅ Escalar desde el tamaño ORIGINAL, no el actual
                    ctrl.Location = ScalePoint(original.Location);
                    ctrl.Size = ScaleSize(original.Size);

                    // Escalar fuente también
                    if (ctrl.Font != null)
                    {
                        float currentDpi = GetSystemDpiScale();
                        float scaleFactor = currentDpi / DesignDpiScale;
                        float newSize = ctrl.Font.Size * scaleFactor;

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

                // ✅ Escalar el formulario desde su tamaño original
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
            public string Titulo { get; set; }
            public Icon Icono { get; set; }
            public Process Proceso { get; set; }
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
                                string[] procesosInternos = { "deneOS_Home", "explorerdna", "tbar", "Shell de experiencia de Windows", "sm", "volSlider", "Host de experiencia del shell de Windows" };
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
                float newVol = Math.Clamp(device.AudioEndpointVolume.MasterVolumeLevelScalar + delta, 0f, 1f);
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

        /*public static string T(string clave)
        {
            return traducciones.TryGetValue(clave, out var valor) ? valor : $"[{clave}]";
        }*/
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
    public static class BrightnessMGMT
    {
        [DllImport("gdi32.dll")]
        private static extern bool SetDeviceGammaRamp(IntPtr hdc, ref RAMP lpRamp);
        [DllImport("dxva2.dll", SetLastError = true)]
        private static extern bool GetMonitorBrightness(IntPtr hMonitor, out uint minimumBrightness, out uint currentBrightness, out uint maximumBrightness);

        [DllImport("user32.dll")]
        private static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);
        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr MonitorFromWindow(IntPtr hwnd, uint dwFlags);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr GetDesktopWindow();

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        
        private struct RAMP
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
            public byte[] Red;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
            public byte[] Green;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
            public byte[] Blue;
        }
        public static bool isLaptop()
        {
            try
            {
                using var process = new Process();
                process.StartInfo.FileName = "powershell";
                process.StartInfo.Arguments = "-Command \"(Get-CimInstance -ClassName Win32_SystemEnclosure).ChassisTypes\"";
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;

                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                // Chassis types para laptops: 8 (portable), 9 (laptop), 10 (notebook), 14 (sub notebook)
                var laptopTypes = new[] { "8", "9", "10", "14" };

                foreach (var type in laptopTypes)
                    if (output.Contains(type))
                        return true;
            }
            catch { }

            return false;
        }
        public static void SetBrightness(int brightness)
        {
            if (isLaptop())
            {
                BrightnessLaptopMGMT.SetBrightness((byte)brightness);
                return;
            }
            IntPtr hdc = GetDC(IntPtr.Zero);
            try
            {
                RAMP ramp = new RAMP
                {
                    Red = new byte[256],
                    Green = new byte[256],
                    Blue = new byte[256]
                };

                int adjustedBrightness = (brightness * 256) / 100;

                for (int i = 0; i < 256; i++)
                {
                    ushort temp = (ushort)(i * adjustedBrightness);
                    byte value = (byte)Math.Min((ushort)255, (ushort)temp);
                    ramp.Red[i] = ramp.Green[i] = ramp.Blue[i] = value;
                }

                SetDeviceGammaRamp(hdc, ref ramp);
            }
            finally
            {
                ReleaseDC(IntPtr.Zero, hdc);
            }
        }
        public static int? GetBrightness()
        {
            if (isLaptop())
            {
                return BrightnessLaptopMGMT.GetBrightness();
            }
            IntPtr desktopWindow = GetDesktopWindow();
            IntPtr monitor = MonitorFromWindow(desktopWindow, 0);

            if (monitor == IntPtr.Zero)
            {
                Console.WriteLine("Failed to get monitor handle.");
                return -1;
            }

            if (GetMonitorBrightness(monitor, out uint min, out uint current, out uint max))
            {
                Console.WriteLine($"Brightness: {current} (Min: {min}, Max: {max})");
                return Convert.ToInt32(current);
            }
            else
            {
                Console.WriteLine("Failed to retrieve brightness.");
                return -2;
            }
        }
    }
    public static class BrightnessLaptopMGMT
    {
        public static int? GetBrightness()
        {
            try
            {
                using Process process = new Process();
                process.StartInfo.FileName = "powershell";
                process.StartInfo.Arguments = "-Command \"(Get-WmiObject -Namespace root/wmi -Class WmiMonitorBrightness).CurrentBrightness\"";
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;

                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                //process.WaitForExit();

                if (int.TryParse(output.Trim(), out int brightness))
                    return brightness;
            }
            catch { }

            return -1;
        }

        public static void SetBrightness(byte brightness)
        {
            try
            {
                using Process process = new Process();
                process.StartInfo.FileName = "powershell";
                process.StartInfo.Arguments = $"-Command \"(Get-WmiObject -Namespace root/wmi -Class WmiMonitorBrightnessMethods).WmiSetBrightness(1,{brightness})\"";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

                process.Start();
                //process.WaitForExit();
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR: " + ex.Message);
            }

        }

        public static async Task SetBrightnessAsync(byte brightness)
        {
            await Task.Run(() =>
            {
                using Process process = new Process();
                process.StartInfo.FileName = "powershell";
                process.StartInfo.Arguments =
                    $"-WindowStyle Hidden -Command \"(Get-CimInstance -Namespace root/wmi -ClassName WmiMonitorBrightnessMethods).WmiSetBrightness(1,{brightness})\"";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.Start();
                process.WaitForExit();
            });
        }

    }
}