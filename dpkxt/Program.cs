using System;
using System.Windows.Forms;

namespace DPKXT
{
    internal static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            // Si recibe "--silent", modo consola sin UI
            if (args.Length > 0 && args[0] == "--silent")
            {
                Console.WriteLine("Modo consola activado (instalación silenciosa)");
                // lógica de instalación automática
                Environment.Exit(0);
            }

            string rutaDpk = args.Length > 0 ? args[0] : null;

            ApplicationConfiguration.Initialize();
            Application.Run(new Form1(rutaDpk));
        }

    }
}
