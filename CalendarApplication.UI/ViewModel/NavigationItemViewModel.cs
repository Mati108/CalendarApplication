using CalendarApplication.UI.Event;
using Prism.Commands;
using Prism.Events;
using System;
using System.Windows.Input;

namespace CalendarApplication.UI.ViewModel
{
    public class NavigationItemViewModel : ViewModelBase
    {
        private string _displayIncident;
        private IEventAggregator _eventAggregator;
        public int Id { get; }
        public DateTime _displayDate;
        public ICommand OpenIncidentDetailViewCommand { get; }

        public NavigationItemViewModel(int id, string displayIncident, DateTime displayDate,
            IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            Id = id;
            DisplayIncident = displayIncident;

            DisplayDate = displayDate;
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
        public DateTime DisplayDate
        {
            get { return _displayDate; }
            set
            {
                _displayDate = value;
                OnPropertyChanged();
            }
        }
    }
}

