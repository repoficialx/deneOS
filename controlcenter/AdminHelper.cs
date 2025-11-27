using System.Diagnostics;
using System.Security.Principal;
using System.Windows.Forms;

public static class AdminHelper
{
    public static void RestartAsAdmin(string args = "")
    {
        var exeName = Application.ExecutablePath;

        var startInfo = new ProcessStartInfo(exeName)
        {
            UseShellExecute = true,
            Verb = "runas", // <-- Esto muestra el UAC
            Arguments = args
        };

        try
        {
            Process.Start(startInfo);
            Application.Exit(); // Salimos del proceso actual
        }
        catch
        {
            MessageBox.Show("Se necesitan permisos de administrador para continuar.", "Permisos necesarios", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    public static bool IsRunningAsAdmin()
    {
        var identity = WindowsIdentity.GetCurrent();
        var principal = new WindowsPrincipal(identity);
        return principal.IsInRole(WindowsBuiltInRole.Administrator);
    }
}
