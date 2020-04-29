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
    public class IncidentDetailViewModel : ViewModelBase, IIncidentDetailViewModel
    {
        private IIncidentRepository _incidentRepository;
        private IEventAggregator _eventAggregator;
        private IMessageDialogService _messageDialogService;
        private IncidentWrapper _incident;
        private bool _hasChanges;
        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }

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
        private bool OnSaveCanExecute()
        {
            return Incident != null && !Incident.HasErrors && HasChanges;
        }
        private async void OnDeleteExecute()
        {
            var result = _messageDialogService.ShowOkCancelDialog($"Zmiana planów? Naprawdę chcesz usunąć wydarzenie {Incident.Title}?",
                "Pytanko");
            if (result == MessageDialogResult.OK)
            {
                _incidentRepository.Remove(Incident.Model);
                await _incidentRepository.SaveAsync();
                _eventAggregator.GetEvent<AfterIncidentDeletedEvent>().Publish(Incident.Id);
            }
        }
        private async void OnSaveExecute()
        {
            await _incidentRepository.SaveAsync();
            HasChanges = _incidentRepository.HasChanges();
            _eventAggregator.GetEvent<AfterIncidentSavedEvent>().Publish(
                new AfterIncidentSavedEventArgs
                {
                    Id = Incident.Id,
                    DisplayIncident = $"{Incident.Title}"
                });

        }
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
