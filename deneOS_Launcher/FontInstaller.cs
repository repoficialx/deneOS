using System;
using System.IO;
using System.Runtime.InteropServices;

class FontInstaller
{
    // Función de Windows para instalar la fuente
    [DllImport("gdi32.dll")]
    private static extern int AddFontResource(string lpszFilename);

    [DllImport("user32.dll")]
    private static extern int SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

    private const int WM_FONTCHANGE = 0x001D;
    private static readonly IntPtr HWND_BROADCAST = new IntPtr(0xffff);

    public static void InstallFont(string fontFilePath)
    {
        string fontsFolder = Environment.GetFolderPath(Environment.SpecialFolder.Fonts);
        string dest = Path.Combine(fontsFolder, Path.GetFileName(fontFilePath));

        // Copiar archivo a carpeta Fonts
        if (!File.Exists(dest))
        {
            File.Copy(fontFilePath, dest);
        }

        // Agregar la fuente al sistema
        AddFontResource(dest);

        // Notificar a todas las ventanas que la fuente cambió
        SendMessage(HWND_BROADCAST, WM_FONTCHANGE, IntPtr.Zero, IntPtr.Zero);

        Console.WriteLine("Fuente instalada correctamente.");
    }
}
