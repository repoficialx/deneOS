namespace controlcenter
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
            ApplicationConfiguration.Initialize();
            

            if (args.Length > 0)
            {
                formAjustes.CurrentPage = args[0] switch
                {
                    "page:screen" => (formAjustes.Pages?)formAjustes.Pages.Pantalla,
                    "page:advanced" => (formAjustes.Pages?)formAjustes.Pages.Avanzado,
                    "page:general" => (formAjustes.Pages?)formAjustes.Pages.General,
                    "page:software" => (formAjustes.Pages?)formAjustes.Pages.Software,
                    "page:home" => (formAjustes.Pages?)formAjustes.Pages.Inicio,
                    "page:about" => (formAjustes.Pages?)formAjustes.Pages.AcercaDe,
                    _ => (formAjustes.Pages?)formAjustes.Pages.Inicio,
                };
            }

            Application.Run(new formAjustes());
        }
    }
}