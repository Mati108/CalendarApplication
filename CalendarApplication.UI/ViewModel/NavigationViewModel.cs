using CalendarApplication.Model;
using CalendarApplication.UI.Data;
using CalendarApplication.UI.Event;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarApplication.UI.ViewModel
{
    public class NavigationViewModel : ViewModelBase, INavigationViewModel
    {
        private IIncidentLookupDataService _incidentLookupService;
        private IEventAggregator _eventAggregator;
        public ObservableCollection<NavigationItemViewModel> Incidents { get; }
        public NavigationViewModel(IIncidentLookupDataService incidentLookupService,
            IEventAggregator eventAggregator)
        {
            _incidentLookupService = incidentLookupService;
            _eventAggregator = eventAggregator;
            Incidents = new ObservableCollection<NavigationItemViewModel>();
            _eventAggregator.GetEvent<AfterIncidentSavedEvent>().Subscribe(AfterIncidentSaved);
            _eventAggregator.GetEvent<AfterIncidentDeletedEvent>().Subscribe(AfterIncidentDeleted);
        }
        private void AfterIncidentDeleted(int incidentId)
        {
            var incident = Incidents.SingleOrDefault(i => i.Id == incidentId);
            if (incident != null)
            {
                Incidents.Remove(incident);
            }
        }
        private void AfterIncidentSaved(AfterIncidentSavedEventArgs obj)
        {
            var lookupItem = Incidents.SingleOrDefault(l => l.Id == obj.Id);
            if (lookupItem == null)
            {
                Incidents.Add(new NavigationItemViewModel(obj.Id, obj.DisplayIncident,
                    _eventAggregator));
            }
            else
            {
                lookupItem.DisplayIncident = obj.DisplayIncident;
            }
        }
        public async Task LoadAsync()
        {
            var lookup = await _incidentLookupService.GetIncidentLookupAsync();
            Incidents.Clear();
            foreach (var item in lookup)
            {
                Incidents.Add(new NavigationItemViewModel(item.Id, item.DisplayIncident,
                    _eventAggregator));
            }
        }
    }
}
