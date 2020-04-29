using CalendarApplication.Model;
using CalendarApplication.UI.Data;
using CalendarApplication.UI.Event;
using CalendarApplication.UI.View.Services;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CalendarApplication.UI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private IEventAggregator _eventAggregator;
        private Func<IIncidentDetailViewModel> _incidentDetailViewModelCreator;
        private IIncidentDetailViewModel _incidentDetailViewModel;
        private IMessageDialogService _messageDialogService;
        public ICommand CreateNewIncidentCommand { get; }
        public INavigationViewModel NavigationViewModel { get; }

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
        private async void OnOpenIncidentDetailView(int? incidentId)
        {
            if (IncidentDetailViewModel != null && IncidentDetailViewModel.HasChanges)
            {
                var result = _messageDialogService.ShowOkCancelDialog("Poczyniłeś pewne zmiany w wydarzeniu, które nie zostaną zapisane. Na pewno chcesz porzucić to wydarzenie?", "Pytanko");
                if (result == MessageDialogResult.Anuluj)
                {
                    return;
                }
            }
            IncidentDetailViewModel = _incidentDetailViewModelCreator();
            await IncidentDetailViewModel.LoadAsync(incidentId);
        }
        private void OnCreateNewIncidentExecute()
        {
            OnOpenIncidentDetailView(null);
        }
        private void AfterIncidentDeleted(int incidentId)
        {
            IncidentDetailViewModel = null;
        }
        public async Task LoadAsync()
        {
            await NavigationViewModel.LoadAsync();
        }

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
