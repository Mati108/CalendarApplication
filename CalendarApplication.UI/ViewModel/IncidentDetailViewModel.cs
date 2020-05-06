using CalendarApplication.Model;
using CalendarApplication.UI.Data.Repositories;
using CalendarApplication.UI.Event;
using CalendarApplication.UI.View.Services;
using CalendarApplication.UI.Wrapper;
using Prism.Commands;
using Prism.Events;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CalendarApplication.UI.ViewModel
{
    /// <summary>Klasa obsługująca logikę wyświetlania i obsługi wydarzenia. Dziedziczy po klasie <see cref="ViewModelBase"/> oraz interfejsie <see cref="IIncidentDetailViewModel"/>.</summary>
    public class IncidentDetailViewModel : ViewModelBase, IIncidentDetailViewModel
    {
        /// <summary>Instancja intefejsu <see cref= "IIncidentRepository" />.</summary>
        private IIncidentRepository _incidentRepository;

        /// <summary>Zbiór zdarzeń.</summary>
        private IEventAggregator _eventAggregator;

        /// <summary>Instancja interfejsu <see cref="IMessageDialogService"/> do obsługi okienek dialogowych.</summary>
        private IMessageDialogService _messageDialogService;

        /// <summary>Instancja metody okalającej wydarzenie.</summary>
        private IncidentWrapper _incident;

        /// <summary>Właściwość określająca, czy zaszły zmiany, czy też nie.</summary>
        private bool _hasChanges;

        /// <summary>Deklaracja właściwości komendy zapisującej.</summary>
        /// <value>Komenda zapisująca.</value>
        public ICommand SaveCommand { get; }

        /// <summary>Deklaracja właściwości komendy usuwającej.</summary>
        /// <value>Komenda usuwająca.</value>
        public ICommand DeleteCommand { get; }

        /// <summary>Konstruktor klasy <see cref="IncidentDetailViewModel" />.</summary>
        /// <param name="incidentRepository">Wydarzenie jako instancja intefejsu <see cref="IIncidentRepository"/>.</param>
        /// <param name="eventAggregator">Zbiór zdarzeń.</param>
        /// <param name="messageDialogService">Instancja interfejsu <see cref="IMessageDialogService"/> do obsługi okienek dialogowych.</param>
        public IncidentDetailViewModel(IIncidentRepository incidentRepository,
            IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService)
        {
            _incidentRepository = incidentRepository;
            _eventAggregator = eventAggregator;
            _messageDialogService = messageDialogService;

            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
            DeleteCommand = new DelegateCommand(OnDeleteExecute);
        }

        /// <summary>Metoda tworząca nowe wydarzenie.</summary>
        /// <returns>Utworzone wydarzenie.</returns>
        private Incident CreateNewIncident()
        {
            var incident = new Incident
            {
                DateStart = DateTime.Now.Date,
                DateStop = DateTime.Now.Date
            };
            _incidentRepository.Add(incident);
            return incident;
        }

        /// <summary>Metoda sprawdzająca, czy można zapisać konkretne wydarzenie. Sprawdza, czy takowe istnieje, czy nie ma błędów i nieobsłużonych zmian.</summary>
        /// <returns><c>true</c> jeśli można zapisać wydarzenie. W przeciwnym przypadku <c>false</c>.</returns>
        private bool OnSaveCanExecute()
        {
            return Incident != null && !Incident.HasErrors && HasChanges;
        }

        /// <summary>Metoda wywoływana podczas usuwania wydarzenia. Wyświetla okno dialogowe z pytaniem potwierdzającym ten ruch użytkownika.</summary>
        private async void OnDeleteExecute()
        {
            var result = await _messageDialogService.ShowOkCancelDialogAsync($"Zmiana planów? Naprawdę chcesz usunąć wydarzenie {Incident.Title}?",
                "Pytanko");
            if (result == MessageDialogResult.OK)
            {
                _incidentRepository.Remove(Incident.Model);
                await _incidentRepository.SaveAsync();
                _eventAggregator.GetEvent<AfterIncidentDeletedEvent>().Publish(Incident.Id);
            }
        }

        /// <summary>Metoda wywoływana podczas zapisywania wydarzenia. Określa m.in. jak będą wyglądały poszczególne jego właściwości.</summary>
        private async void OnSaveExecute()
        {
            await _incidentRepository.SaveAsync();
            HasChanges = _incidentRepository.HasChanges();
            _eventAggregator.GetEvent<AfterIncidentSavedEvent>().Publish(
                new AfterIncidentSavedEventArgs
                {
                    Id = Incident.Id,
                    DisplayIncident = $"{Incident.Title}",
                    DisplayDate = Incident.DateStart,
                });

        }

        /// <summary>Metoda asynchronicznie ładująca dane o wydarzeniu. Jeśli dane wydarzenie nie istnieje, to zostaje utworzone. Jeśli w wydarzeniu zaszły zmiany to są one obsługiwane.</summary>
        /// <param name="incidentId">Identyfikator wydarzenia, który może być NULL.</param>
        public async Task LoadAsync(int? incidentId)
        {
            var incident = incidentId.HasValue
                ? await _incidentRepository.GetByIdAsync(incidentId.Value)
                : CreateNewIncident();

            Incident = new IncidentWrapper(incident);
            Incident.PropertyChanged += (s, e) =>
            {
                if (!HasChanges)
                {
                    HasChanges = _incidentRepository.HasChanges();
                }
                if (e.PropertyName == nameof(Incident.HasErrors))
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            };
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            if (Incident.Id == 0)
            {
                Incident.Title = "";
            }
        }

        /// <summary>Deklaracja flagi informującej, czy w danej instancji zaszły zmiany.</summary>
        /// <value>
        ///   <c>true</c> jeśli zaszły zmiany. W przeciwnym przypadku <c>false</c>.
        /// </value>
        public bool HasChanges
        {
            get { return _hasChanges; }
            set
            {
                if (_hasChanges != value)
                {
                    _hasChanges = value;
                    OnPropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }

        /// <summary>Deklaracja instancji klasy opakowującej <see cref="IncidentWrapper"/>.</summary>
        /// <value>Wydarzenie.</value>
        public IncidentWrapper Incident
        {
            get { return _incident; }
            private set
            {
                _incident = value;
                OnPropertyChanged();
            }
        }
    }
}
