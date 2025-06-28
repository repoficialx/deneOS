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

            CargarIdiomas();

            if (args.Length > 0)
            {
                if (args[0].StartsWith("/installUpdate:"))
                {
                    string downloadUrl = args[0].Substring("/installUpdate:".Length);
                    if (Uri.IsWellFormedUriString(downloadUrl, UriKind.Absolute))
                    {
                        // Aquí podrías iniciar el proceso de descarga e instalación de la actualización
                        MessageBox.Show("Iniciando actualización desde: " + downloadUrl, "Actualización", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Application.Run(new UpdateScreen(downloadUrl));
                        Application.Exit();
                        return;
                    }
                    else
                    {
                        MessageBox.Show("URL de actualización no válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
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

        public static void CargarIdiomas()
        {
            bool fileExists = File.Exists(@"C:\DENEOS\sysconf\lang.ini");
            int langLine = 1;
            string lang;
            if (fileExists)
            {
                try
                {
                    lang = File.ReadAllLines(@"C:\DENEOS\sysconf\lang.ini")[langLine];
                }
                catch
                {
                    MessageBox.Show("ERROR 0x4", "DENEOS HOME EDITION", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    lang = "en";
                    return;
                }
            }
            else
            {
                lang = "en";
            }
            Traductor.Cargar(lang);
        }
    }
}