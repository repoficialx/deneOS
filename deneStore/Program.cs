namespace deneStore
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
            if (args.Length > 1 && args[0] == "--install")
            {
                string appCode = args[1];

            }
            ApplicationConfiguration.Initialize();
            Application.Run(new MyApplicationContext());
        }

        class MyApplicationContext : ApplicationContext
        {
            public MyApplicationContext()
            {
                var welcome = new welcomeScreen();
                var store = new Store();
                store.FormClosed += (s, e) => ExitThread();
                welcome.FormClosed += (s, e) => store.Show();
                welcome.Show();
            }
        }
    }
}