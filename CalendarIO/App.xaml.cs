using System.Configuration;
using System.Data;
using System.Windows;

namespace CalendarIO
{
    /// <summary>  
    /// Interaction logic for App.xaml  
    /// </summary>  
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            // Ensure MainWindow is cast to the correct type before calling UpdateMonth  
            if (Current.MainWindow is MainWindow mainWindow)
            {
                mainWindow.UpdateMonth();
            }
        }
    }

}
