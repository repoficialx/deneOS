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
            string title = null!;
            string icon = null!;
            string url = null!;

            foreach (string arg in args)
            {
                if (arg.StartsWith("/title:"))
                    title = arg.Substring("/title:".Length);
                else if (arg.StartsWith("/icon:"))
                    icon = arg.Substring("/icon:".Length);
                else if (arg.StartsWith("/url:"))
                    url = arg.Substring("/url:".Length);
            }
            Application.Run(new Form1(title!, icon!, url!));
        }
    }
}