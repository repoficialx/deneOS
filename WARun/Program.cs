namespace WARun
{
    public class AppConfig
    {
        public string Title { get; set; } = "WARun";
        public string IconPath { get; set; } = "";
        public string Url { get; set; } = "";
    }

    // Clase para parsear argumentos
    public static class ArgParser
    {
        public static AppConfig ParseArgs(string[] args)
        {
            if (args.Length < 3)
            {
                Console.WriteLine("Format: /title:(Title) /icon:(IconPath).ico /url:(Website)");
                Application.Exit();
            }

            AppConfig config = new();

            foreach (var arg in args)
            {
                if (arg.StartsWith("/title:"))
                    config.Title = arg["/title:".Length..];
                else if (arg.StartsWith("/icon:"))
                    config.IconPath = arg["/icon:".Length..];
                else if (arg.StartsWith("/url:"))
                    config.Url = arg["/url:".Length..];
            }

            return config;
        }
    }

    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            ApplicationConfiguration.Initialize();
            var config = ArgParser.ParseArgs(args);
            Application.Run(new Form1(config));
        }
    }
}