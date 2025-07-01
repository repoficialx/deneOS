namespace WARun
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length < 3)
            {
                Console.WriteLine("Format: /title:(Title) /icon:(IconPath).ico /url:(Website)");
                Application.Exit();
            }
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            string title = "";
            string icon = "";
            string url = "";

            foreach (string arg in args)
            {
                if (arg.StartsWith("/title:"))
                    title = arg["/title:".Length..];
                else if (arg.StartsWith("/icon:"))
                    icon = arg["/icon:".Length..];
                else if (arg.StartsWith("/url:"))
                    url = arg["/url:".Length..];
            }
            Application.Run(new Form1(title, icon, url));
        }
    }
}