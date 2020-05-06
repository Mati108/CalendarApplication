using CalendarApplication.UI.ViewModel;
using MahApps.Metro.Controls;
using System.Windows;

namespace CalendarApplication.UI
{
    /// <summary>Inicjalizacja interakcji dla klasy MainWindow.xaml. Dziedziczy po <see cref="MetroWindow"/>, które jest klasą odpowiedzialną za styl aplikacji.</summary>
    public partial class MainWindow : MetroWindow
    {
        /// <summary>Model głównego widoku.</summary>
        private MainViewModel _viewModel;

        /// <summary>Konstruktor klasy <see cref="MainWindow" />. Inicjalizuje widok oraz załadowuje elementy modelu widoku, które mają być używane w klasie xaml.</summary>
        /// <param name="viewModel">Model widoku.</param>
        public MainWindow(MainViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
            Loaded += MainWindow_Loaded;
        }

        /// <summary>Handles the Loaded event of the MainWindow control.</summary>
        /// <param name="sender">Obiekt który wywołał zdarzenie.</param>
        /// <param name="e">Instancja klasy <see cref="RoutedEventArgs" /> zawierająca dane o zdarzeniu.</param>
        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await _viewModel.LoadAsync();
        }
    }
}
