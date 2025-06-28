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
            private static int majorVersion = FileVersionInfo.GetVersionInfo(@"C:\DENEOS\core\deneOS_Home.exe").FileMajorPart;
            private static int minorVersion = FileVersionInfo.GetVersionInfo(@"C:\DENEOS\core\deneOS_Home.exe").FileMinorPart;
            private static int buildVersion = FileVersionInfo.GetVersionInfo(@"C:\DENEOS\core\deneOS_Home.exe").FileBuildPart;
            private static string versionTag = buildVersion == 0 ? "Release" : (buildVersion == 1 ? "Beta" : "Alpha");
            public static readonly string Version = $"{majorVersion}.{minorVersion}";
            public static readonly string Build = DateTime.Now.ToString("yyyyMMdd");

            public static string GetVersionInfo() =>
                $"deneOS {Version} - {versionTag} Channel";
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
            return "en"; // Idioma por defecto si no se encuentra el archivo o hay un error
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
            public static int GetWifiSignalStrength()
            {

                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = "netsh",
                    Arguments = "wlan show interfaces",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                string output;
                using (Process process = Process.Start(psi))
                {
                    output = process.StandardOutput.ReadToEnd();
                    process.WaitForExit();
                }
                Console.Write(output);
                // Buscar "Señal : 96%" usando regex)
                Match match = Regex.Match(output, @"(\d+)%");
                if (match.Success)
                {
                    int signalStrength = int.Parse(match.Groups[1].Value);
                    Console.WriteLine($"{T("wifistrength")} {signalStrength}%");
                    //MessageBox.Show($"Intensidad de señal WiFi: {signalStrength}%");
                    return signalStrength;
                }
                else
                {
                    Console.WriteLine(T("nowifisignal"));
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

}