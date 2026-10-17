
using Microsoft.Win32;
using System.Runtime.InteropServices;

namespace deneOS.Launcher;

internal static class SystemFontInstaller
{
    private const string FontsDirectory =
        @"C:\DENEOS\sysfonts";

    private const string UserFontsDirectory =
        @"Microsoft\Windows\Fonts";

    private const string RegistryPath =
        @"Software\Microsoft\Windows NT\CurrentVersion\Fonts";

    private const uint WM_FONTCHANGE = 0x001D;
    private const uint SMTO_ABORTIFHUNG = 0x0002;

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    private static extern nint SendMessageTimeoutW(
        nint hWnd,
        uint msg,
        nuint wParam,
        nint lParam,
        uint flags,
        uint timeout,
        out nuint result);

    public static int InstallAll()
    {
        if (!Directory.Exists(FontsDirectory))
            throw new DirectoryNotFoundException(
                $"No existe {FontsDirectory}");

        string localAppData = Environment.GetFolderPath(
            Environment.SpecialFolder.LocalApplicationData);

        string destinationDirectory = Path.Combine(
            localAppData, UserFontsDirectory);

        Directory.CreateDirectory(destinationDirectory);

        using RegistryKey registry =
            Registry.CurrentUser.CreateSubKey(RegistryPath)
            ?? throw new InvalidOperationException(
                "No se pudo abrir el registro de fuentes.");

        int installed = 0;
        bool changed = false;

        foreach (string source in Directory.EnumerateFiles(
                     FontsDirectory, "*.ttf"))
        {
            string fileName = Path.GetFileName(source);
            string destination = Path.Combine(
                destinationDirectory, fileName);

            // Usamos el nombre del archivo para evitar
            // depender de nombres de familia desconocidos.
            string registryName =
                Path.GetFileNameWithoutExtension(fileName)
                + " (TrueType)";

            bool needsCopy = !File.Exists(destination)
                || !FilesAreEqual(source, destination);

            if (needsCopy)
            {
                File.Copy(source, destination, overwrite: true);
                changed = true;
            }

            string? currentValue =
                registry.GetValue(registryName) as string;

            if (!string.Equals(
                    currentValue,
                    destination,
                    StringComparison.OrdinalIgnoreCase))
            {
                registry.SetValue(
                    registryName,
                    destination,
                    RegistryValueKind.String);

                changed = true;
            }

            if (needsCopy || currentValue != destination)
                installed++;
        }

        if (changed)
        {
            _ = SendMessageTimeoutW(
                (nint)0xFFFF,
                WM_FONTCHANGE,
                0,
                0,
                SMTO_ABORTIFHUNG,
                5000,
                out _);
        }

        return installed;
    }

    private static bool FilesAreEqual(
        string first, string second)
    {
        using var a = File.OpenRead(first);
        using var b = File.OpenRead(second);

        if (a.Length != b.Length)
            return false;

        return System.Security.Cryptography.CryptographicOperations
            .FixedTimeEquals(
                System.Security.Cryptography.SHA256.HashData(a),
                System.Security.Cryptography.SHA256.HashData(b));
    }
}
