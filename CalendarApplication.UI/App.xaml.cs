using Autofac;
using CalendarApplication.UI.Startup;
using System;
using System.Windows;

namespace CalendarApplication.UI
{
    /// <summary>Inicjalizacja interakcji dla klasy App.xaml.</summary>
    public partial class App : Application
    {
        /// <summary>Metoda odpowiadająca za rozruch aplikacji. Inicjalizuje jej główne okno oraz instancję klasy <see cref="Bootstrapper"/>.</summary>
        /// <param name="sender">Obiekt który wywołał zdarzenie.</param>
        /// <param name="e">Instancja klasy <see cref="StartupEventArgs" /> zawierająca dane o zdarzeniu.</param>
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var bootstrapper = new Bootstrapper();
            var containter = bootstrapper.Bootstrap();
            var mainWindow = containter.Resolve<MainWindow>();
            mainWindow.Show();
        }

        /// <summary>Metoda przechwytująca nieobsługiwane wyjątki pojawiające się podczas działania aplikacji. Informuje o nich użytkownika poprzez okienko dialogowe.</summary>
        /// <param name="sender">Obiekt który wywołał zdarzenie.</param>
        /// <param name="e">Instancja klasy <see cref="System.Windows.Threading.DispatcherUnhandledExceptionEventArgs" /> zawierająca dane o zdarzeniu.</param>
        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show("Wystąpił niespodziewany błąd. Skontaktuj się z administratorem"
                + Environment.NewLine + e.Exception.Message, "Niespodziewany błąd");

            e.Handled = true;
        }
    }
}
