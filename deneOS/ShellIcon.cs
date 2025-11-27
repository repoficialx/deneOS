using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using System;

public static class ShellIcon
{
    [DllImport("Shell32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr ExtractAssociatedIcon(IntPtr hInst, string lpIconPath, out ushort lpiIcon);

    public static Icon GetIconFromLink(string path)
    {
        ushort index = 0;
        IntPtr hInst = Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().GetModules()[0]);
        IntPtr hIcon = ExtractAssociatedIcon(hInst, path, out index);
        return Icon.FromHandle(hIcon);
    }
}
