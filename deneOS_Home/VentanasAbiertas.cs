/*using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Collections.Generic;
using System;

public class AppVentana
{
    public IntPtr Handle;
    public string Titulo;
    public Icon Icono;
    public Process Proceso;
}

public static class VentanasActivas
{
    private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

    [DllImport("user32.dll")] private static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);
    [DllImport("user32.dll")] private static extern bool IsWindowVisible(IntPtr hWnd);
    [DllImport("user32.dll")] private static extern int GetWindowText(IntPtr hWnd, System.Text.StringBuilder text, int maxLength);
    [DllImport("user32.dll")] private static extern int GetWindowThreadProcessId(IntPtr hWnd, out int processId);
    [DllImport("user32.dll")] private static extern IntPtr GetShellWindow();

    public static List<AppVentana> ObtenerVentanas()
    {
        List<AppVentana> ventanas = new List<AppVentana>();
        IntPtr shellWindow = GetShellWindow();

        EnumWindows((hWnd, lParam) =>
        {
            if (!IsWindowVisible(hWnd) || hWnd == shellWindow)
                return true;

            var length = 256;
            var builder = new System.Text.StringBuilder(length);
            GetWindowText(hWnd, builder, length);

            string titulo = builder.ToString();
            if (string.IsNullOrWhiteSpace(titulo))
                return true;

            GetWindowThreadProcessId(hWnd, out int pid);
            Process proc = Process.GetProcessById(pid);
            Icon icon = Icon.ExtractAssociatedIcon(proc.MainModule.FileName);

            ventanas.Add(new AppVentana
            {
                Handle = hWnd,
                Titulo = titulo,
                Proceso = proc,
                Icono = icon
            });

            return true;
        }, IntPtr.Zero);

        return ventanas;
    }
}
*/
using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using System.IO;
using System.Linq;

public class AppVentana
{
    public string Titulo { get; set; }
    public Icon Icono { get; set; }
    public Process Proceso { get; set; }
}

public static class VentanasActivas
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
                        string[] procesosInternos = { "deneOS_Home", "explorerdna", "tbar", "Shell de experiencia de Windows", "sm", "volSlider", "Host de experiencia del shell de Windows"};
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
