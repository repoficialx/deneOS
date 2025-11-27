namespace deneFiles
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            ApplicationConfiguration.Initialize();
            if (args.Length > 0)
            {
                if (!string.IsNullOrEmpty(args[0]) && !string.IsNullOrWhiteSpace(args[0]))
                {
                    switch (args[0])
                    {
                        case "/dangerZone:enableRoot":
                            if (args.Length > 1 && args[1].StartsWith("/folder:"))
                            {
                                switch (args[1])
                                {
                                    case "/folder:SOFTWARE":
                                        Application.Run(new Form1(true, Form1.FolderType.SOFTWARE)); 
                                        break;
                                    case "/folder:DNUSR":
                                        Application.Run(new Form1(true, Form1.FolderType.DNUSR));
                                        break;
                                    default:
                                        MessageBox.Show("Carpeta no reconocida. Ejecutando con configuración por defecto.");
                                        Application.Run(new Form1(true));
                                        break;
                                } 
                            }
                            else
                                Application.Run(new Form1(true));
                            break;
                        case "/folder:SOFTWARE":
                            if (args.Length > 1 && args[1] == "/dangerZone:enableRoot")
                            {
                                Application.Run(new Form1(true, Form1.FolderType.SOFTWARE)); 
                            }
                            else
                                Application.Run(new Form1(false, Form1.FolderType.SOFTWARE)); 
                            break;
                        case "/folder:DNUSR":
                            if (args.Length > 1 && args[1] == "/dangerZone:enableRoot")
                            {
                                Application.Run(new Form1(true, Form1.FolderType.DNUSR));
                            }
                            else
                                Application.Run(new Form1(false, Form1.FolderType.DNUSR));
                            break;
                        default:
                            MessageBox.Show("Argumento no reconocido. Ejecutando con configuración por defecto.");
                            Application.Run(new Form1());
                            break;
                    }
                }
            }
            else
            {
                Application.Run(new Form1());
            }
        }
    }
}