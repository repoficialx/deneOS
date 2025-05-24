using System.Runtime.InteropServices;

namespace eMBTest
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            //ApplicationConfiguration.Initialize();
            //Application.Run(new Form1());
            [DllImport("C:\\Users\\rayel\\source\\repos\\!New\\repos\\deneOS Utilities\\ExtraMSGBox\\bin\\Debug\\net8.0\\ExtraMSGBox.dll")]
            static extern void CNMB(string title, string message, string icon, string button);
            CNMB("Title", "Message", "info", "OK");
        }
    }
}