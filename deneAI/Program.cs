namespace deneAI
{
    internal static class Program
    {
        static ApplicationContext appContext;
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            //appContext = new ApplicationContext(new Ollama());
            //appContext.MainForm!.FormClosed += (s, e) => new Form1().Show();
            var mainForm = new Ollama();
            appContext = new ApplicationContext(mainForm);

            mainForm.FormClosing += (s, e) => { if (e.Cancel) return; Form1 f = new(); appContext.MainForm = f; f.Show(); };

            Application.Run(appContext);
        }
    }
}