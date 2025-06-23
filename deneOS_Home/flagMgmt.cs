namespace deneOS_Home
{
    public static class flagMgmt
    {
        // Flags de tipo bool
        public static bool EnableRoot = false;
        public static bool EnableDebug = false;
        public static bool DisableLockScreen = false;
        public static bool SkipBootAnim = false;
        public static bool MockBattery = false;
        public static bool ClassicMode = false;
        public static bool ForceUpdate = false;
        public static bool ShowUntranslatedStrings = false;
        public static bool SafeMode = false;
        public static bool SafeModeWithNetwork = false;
        public static bool ResetPrefs = false;
        public static bool RecoverMode = false;
        public static bool LogSession = false;
        public static bool BypassChecks = false;
        public static bool ShowSysInfo = false;
        public static bool NoShell = false;
        public static bool EmergencyUI = false;
        public static bool OfflineOnly = false;

        // Flags de tipo string
        public static string Language = "";              // "es", "en", etc.
        public static string LaunchAppId = "";           // ej: "deneNotes"
        public static string Locale = "";                // ej: "fr", "pt", etc.

        // Flags de tipo enum (puedes ampliarlo más adelante)
        public enum TimeFormat { SystemDefault, Metric, Military }
        public static TimeFormat SelectedTimeFormat = TimeFormat.SystemDefault;
        
        
    }
}
