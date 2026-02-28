using Microsoft.Win32;
using System.Net;
using System.Runtime.InteropServices;

class FontInstaller
{
    // P/Invoke para registrar fuente en memoria
    [DllImport("gdi32.dll", EntryPoint = "AddFontResourceExW", SetLastError = true)]
    private static extern int AddFontResourceEx([MarshalAs(UnmanagedType.LPWStr)] string lpFileName, uint fl, IntPtr pdv);

    private const uint FR_PRIVATE = 0x10;
    private const uint FR_NOT_ENUM = 0x20;

    public static void InstallFonts()
    {
        WebClient client = new WebClient();
        string baseUri = "https://repoficialx.xyz/fonts/";
        string fontsPath = @"C:\Windows\Fonts\";
        string dnosFonts = @"C:\DENEOS\sysfonts";

        string[] fonts = new string[]
        {
            "Material Icons.ttf",
            "SegoeUI-VF.ttf",
            "segoe-fluent-icons.ttf",
            "segoe_slboot.ttf",
            "FluentSystemIcons-Regular.ttf",
            "agency-fb-regular.ttf",
            "AGENCYB.TTF",
            "FluentSystemIcons-Regular.ttf",
            "Product Sans Bold Italic.ttf",
            "Product Sans Bold.ttf",
            "Product Sans Italic.ttf",
            "Product Sans Regular.ttf",
            "Segoe MDL2 Assets.ttf",
            "agency-fb-regular.ttf"
        };

        foreach (string font in fonts)
        {
            if (!Directory.Exists(@"C:\DENEOS\sysfonts"))
                Directory.CreateDirectory(@"C:\DENEOS\sysfonts");
            string fontUri = baseUri + font;
            string destPath = Path.Combine(fontsPath, font);
            string _desPath = Path.Combine(dnosFonts, font);

            // Descargar fuente si no existe
            if (!File.Exists(destPath))
            {
                client.DownloadFile(fontUri, destPath);
                Console.WriteLine($@"Downloaded {font} to {destPath}");
                client.DownloadFile(fontUri, _desPath);
                Console.WriteLine($@"Downloaded {font} to {_desPath}");
            }

            // Añadir al registro para instalación permanente
            try
            {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(
                    @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", true))
                {
                    if (key != null)
                    {
                        string fontName = Path.GetFileNameWithoutExtension(font) + " (TrueType)";
                        key.SetValue(fontName, font);
                        Console.WriteLine($@"{font} registrado en el sistema.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($@"Error al registrar {font} en el registro: {ex.Message}");
            }

            // Registrar para uso inmediato
            int res = AddFontResourceEx(destPath, FR_NOT_ENUM, IntPtr.Zero);
            if (res > 0)
                Console.WriteLine($@"{font} cargada para uso inmediato.");
            else
                Console.WriteLine($@"Error cargando {font}, código: {Marshal.GetLastWin32Error()}");
        }

        Console.WriteLine(@"Todas las fuentes procesadas.");
    }
}