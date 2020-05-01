using Autofac;
using CalendarApplication.UI.Startup;
using System;
using System.Windows;

namespace CalendarApplication.UI
{
    /// <summary>
    /// Logika interakcji dla klasy App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var bootstrapper = new Bootstrapper();
            var containter = bootstrapper.Bootstrap();
            var mainWindow = containter.Resolve<MainWindow>();
            mainWindow.Show();
        }
        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show("Wystąpił niespodziewany błąd. Skontaktuj się z administratorem"
                + Environment.NewLine + e.Exception.Message, "Niespodziewany błąd");

            e.Handled = true;
        }
    }
}
