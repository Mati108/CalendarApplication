using CalendarApplication.UI.Event;
using CalendarApplication.UI.View.Services;
using Prism.Commands;
using Prism.Events;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CalendarApplication.UI.ViewModel
{
    /// <summary>Klasa obsługująca logikę modelu widoku głównego. Dziedziczy po <see cref="ViewModelBase"/>.</summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>Zbiór zdarzeń.</summary>
        private IEventAggregator _eventAggregator;

        /// <summary>Delegata tworzenia szczegółowego modelu widoku dla wydarzenia.</summary>
        private Func<IIncidentDetailViewModel> _incidentDetailViewModelCreator;

        /// <summary>Deklaracja instancji interfejsu <see cref="IIncidentDetailViewModel"/>.</summary>
        private IIncidentDetailViewModel _incidentDetailViewModel;

        /// <summary>Instancja interfejsu <see cref="IMessageDialogService"/> do obsługi okienek dialogowych.</summary>
        private IMessageDialogService _messageDialogService;

        /// <summary>Deklaracja komendy utworzenia nowego wydarzenia.</summary>
        /// <value>Komenda utworzenia nowego wydarzenia.</value>
        public ICommand CreateNewIncidentCommand { get; }

        /// <summary>Deklaracja instancji interfejsu <see cref="INavigationViewModel"/>.</summary>
        /// <value>Instancja interfejsu <see cref="INavigationViewModel"/>.</value>
        public INavigationViewModel NavigationViewModel { get; }

        /// <summary>Konstrukto klasy <see cref="MainViewModel" />.</summary>
        /// <param name="navigationViewModel">Model widoku dla okienka nawigacyjnego.</param>
        /// <param name="incidentDetailViewModelCreator">Szczegółowy model widoku dla wydarzenia.</param>
        /// <param name="eventAggregator">Zbiór zdarzeń.</param>
        /// <param name="messageDialogService">Okno dialogowe.</param>
        public MainViewModel(INavigationViewModel navigationViewModel,
            Func<IIncidentDetailViewModel> incidentDetailViewModelCreator,
            IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService)
        {
            _eventAggregator = eventAggregator;
            _incidentDetailViewModelCreator = incidentDetailViewModelCreator;
            _messageDialogService = messageDialogService;

            _eventAggregator.GetEvent<OpenIncidentDetailViewEvent>()
            .Subscribe(OnOpenIncidentDetailView);
            _eventAggregator.GetEvent<AfterIncidentDeletedEvent>().Subscribe(AfterIncidentDeleted);

            CreateNewIncidentCommand = new DelegateCommand(OnCreateNewIncidentExecute);

            NavigationViewModel = navigationViewModel;
        }

        /// <summary>Metoda wywoływana podczas próby opuszczenia edycji bieżącego wydarzenia. Wyświetla okno dialogowe z zapytaniem.</summary>
        /// <param name="incidentId">Identyfikator wydarzenia.</param>
        private async void OnOpenIncidentDetailView(int? incidentId)
        {
            if (IncidentDetailViewModel != null && IncidentDetailViewModel.HasChanges)
            {
                var result = await _messageDialogService.ShowOkCancelDialogAsync("Poczyniłeś pewne zmiany w wydarzeniu, które nie zostaną zapisane. Na pewno chcesz porzucić to wydarzenie?", "Pytanko");
                if (result == MessageDialogResult.Anuluj)
                {
                    return;
                }
            }
            IncidentDetailViewModel = _incidentDetailViewModelCreator();
            await IncidentDetailViewModel.LoadAsync(incidentId);
        }

        /// <summary>Metoda wywoływana podczas próby opuszczenia edycji nowo tworzonego wydarzenia. Przekazuje dalej wartość id, jako NULL. Kolejna metoda wyświetla okno dialogowe z zapytaniem.</summary>
        private void OnCreateNewIncidentExecute()
        {
            OnOpenIncidentDetailView(null);
        }

        /// <summary>Metoda wywoływana po usunięciu wydarzenia. Ustawia instancję interfejsu <see cref="IIncidentDetailViewModel"/> na wartość NULL.</summary>
        /// <param name="incidentId">Identyfikator wydarzenia.</param>
        private void AfterIncidentDeleted(int incidentId)
        {
            IncidentDetailViewModel = null;
        }

        /// <summary>Metoda asynchroniczna, pobierająca wydarzenia, które będą wyświetlane posortowane w menu nawigacyjnym.</summary>
        public async Task LoadAsync()
        {
            await NavigationViewModel.LoadAsync();
        }

        /// <summary>Deklaracja właściwości szegółowego modelu widoku dla wydarzenia.</summary>
        /// <value>Szegółowy modelu widoku dla wydarzenia.</value>
        public IIncidentDetailViewModel IncidentDetailViewModel
        {
            get { return _incidentDetailViewModel; }
            private set
            {
                _incidentDetailViewModel = value;
                OnPropertyChanged();
            }
        }
    }
}
