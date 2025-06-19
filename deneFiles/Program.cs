﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace deneFiles
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        /// 
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (args.Length > 0)
            {
                if (!string.IsNullOrEmpty(args[0]) && !string.IsNullOrWhiteSpace(args[0]) ){ 
                switch (args[0])
                {
                    case "/dangerZone:enableRoot":
                        // Aquí podrías implementar la lógica para habilitar el acceso a la raíz del sistema
                        // Por ejemplo, podrías cambiar la URL inicial del WebBrowser a "file:///C:/"
                        Application.Run(new Form1(true)); // Ejecutar con acceso a la raíz
                        break;
                    case "/folder:SOFTWARE":
                        // Aquí podrías implementar la lógica para navegar a la carpeta SOFTWARE
                        // Por ejemplo, podrías cambiar la URL inicial del WebBrowser a "file:///C:/SOFTWARE/"
                        Application.Run(new Form1(false, Form1.FolderType.SOFTWARE)); // Ejecutar con acceso a la carpeta SOFTWARE
                        break;
                    case "/folder:DNUSR":
                        // Aquí podrías implementar la lógica para navegar a la carpeta DNUSR
                        // Por ejemplo, podrías cambiar la URL inicial del WebBrowser a "file:///C:/DNUSR/"
                        Application.Run(new Form1(false, Form1.FolderType.DNUSR)); // Ejecutar con acceso a la carpeta DNUSR
                        break;
                    default:
                        // Si no se reconoce el argumento, podrías mostrar un mensaje de error o ejecutar con la configuración por defecto
                        MessageBox.Show("Argumento no reconocido. Ejecutando con configuración por defecto.");
                        Application.Run(new Form1()); // Ejecutar con configuración por defecto
                        break;
                } // fin de switch
                } //fin de if anidado
            } //fin de if
            else
            {
                Application.Run(new Form1()); // Ejecutar sin argumentos
            } //fin de else
        }
    }
}
