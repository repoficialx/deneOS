using deneOS.init;

namespace deneOS
{
    internal static class Program
    {
        public static bool showBoot = false;
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            adminMgmt.args = args;
            adminMgmt.argsCount = args.Length;
            Arguments();
            ApplicationConfiguration.Initialize();
            if (ornMgmt.GetOrientation() == Orientation.Vertical)
            {
                Application.Run(new BootScreenVertical());
            }
            else
            {
                Application.Run(new BootScreen());
            }
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration
        }
        public static void Arguments()
        {
            if (File.Exists("c:\\currentsessionflags.txt")) File.Delete("c:\\currentsessionflags.txt");
            else _ = (string)null;

#if DEBUG
            flagMgmt.EnableDebug = true;
#endif
            string[] args = Environment.GetCommandLineArgs();
            string[] argfile = { };
            foreach (string arg in args)
            {
                _ = argfile.Length < args.Length ? argfile.Append(arg + Environment.NewLine) : null;

                if (arg.StartsWith("/dangerZone:enableRoot")) flagMgmt.EnableRoot = true;
                else if (arg.StartsWith("/dangerZone:debug")) flagMgmt.EnableDebug = true;
                else if (arg.StartsWith("/dangerZone:disableLockScreen")) flagMgmt.DisableLockScreen = true;
                else if (arg.StartsWith("/dangerZone:skipBootAnim")) { flagMgmt.SkipBootAnim = true; showBoot = true; }
                else if (arg.StartsWith("/dangerZone:mockBattery")) flagMgmt.MockBattery = true;
                else if (arg.StartsWith("/dangerZone:classicMode")) flagMgmt.ClassicMode = true;
                else if (arg.StartsWith("/dangerZone:forceUpdate")) flagMgmt.ForceUpdate = true;

                else if (arg.StartsWith("/testing:untranslatedStrings")) flagMgmt.ShowUntranslatedStrings = true;
                else if (arg == "/safeMode") flagMgmt.SafeMode = true;
                else if (arg == "/safeMode:network") flagMgmt.SafeModeWithNetwork = true;
                else if (arg == "/resetPrefs") flagMgmt.ResetPrefs = true;
                else if (arg == "/recover") flagMgmt.RecoverMode = true;
                else if (arg == "/log") flagMgmt.LogSession = true;
                else if (arg == "/bypassChecks") flagMgmt.BypassChecks = true;
                else if (arg == "/sysinfo") flagMgmt.ShowSysInfo = true;
                else if (arg == "/noShell") flagMgmt.NoShell = true;
                else if (arg == "/emergencyUI") flagMgmt.EmergencyUI = true;
                else if (arg == "/offlineOnly") flagMgmt.OfflineOnly = true;
                else if (arg == "/forceVertical") flagMgmt.ForceVertical = true;

                else if (arg.StartsWith("/language:"))
                    flagMgmt.Language = arg.Split(':')[1];

                else if (arg.StartsWith("/application:"))
                    flagMgmt.LaunchAppId = arg.Split(':')[1];

                else if (arg.StartsWith("/locale:"))
                    flagMgmt.Locale = arg.Split(':')[1];

                else if (arg.StartsWith("/metricTime"))
                    flagMgmt.SelectedTimeFormat = flagMgmt.TimeFormat.Metric;
            }
            File.WriteAllLines(@"C:\CurrentSessionFlags.txt", argfile);
        }
    }
}