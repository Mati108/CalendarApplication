using CalendarApplication.UI.Event;
using Prism.Commands;
using Prism.Events;
using System.Windows.Input;

namespace CalendarApplication.UI.ViewModel
{
    public class NavigationItemViewModel : ViewModelBase
    {
        private string _displayIncident;
        private IEventAggregator _eventAggregator;
        public int Id { get; }
        public ICommand OpenIncidentDetailViewCommand { get; }

        public NavigationItemViewModel(int id, string displayIncident,
            IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            Id = id;
            DisplayIncident = displayIncident;
            OpenIncidentDetailViewCommand = new DelegateCommand(OnOpenIncidentDetailView);
        }
        private void OnOpenIncidentDetailView()
        {
            _eventAggregator.GetEvent<OpenIncidentDetailViewEvent>()
            .Publish(Id);
        }
        public string DisplayIncident
        {
            get { return _displayIncident; }
            set
            {
                _displayIncident = value;
                OnPropertyChanged();
            }
        }
    }
}
