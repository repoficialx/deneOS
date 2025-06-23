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
            if (args.Length > 1 && args[1] == "--silent")
            {
                Console.WriteLine("Modo consola activado (instalación silenciosa)");
                ApplicationConfiguration.Initialize();
                Form1.InstalarDPK(args[0]);
                Environment.Exit(0);
            }

            string? rutaDpk = args.Length > 0 ? args[0] : null;
            if (args.Length == 0 || string.IsNullOrEmpty(rutaDpk) || !System.IO.File.Exists(rutaDpk))
            {
                Console.WriteLine("Debe proporcionar la ruta de un archivo .dpk válido.");
                Environment.Exit(-1);
            }

            ApplicationConfiguration.Initialize();
            Application.Run(new Form1(rutaDpk));
        }

    }
}
