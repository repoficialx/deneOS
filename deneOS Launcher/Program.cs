using System.Diagnostics;

namespace deneOS_Launcher
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            if (args.Length > 0 && args[0] == "/silentrun")
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "C:\\DENEOS\\core\\deneOS.exe",
                    UseShellExecute = true,
                    Verb = "runas"
                });
                return;
            }
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}