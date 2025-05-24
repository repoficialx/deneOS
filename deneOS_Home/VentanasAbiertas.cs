using System.Diagnostics;
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
