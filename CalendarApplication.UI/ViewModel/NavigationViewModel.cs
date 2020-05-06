using CalendarApplication.UI.Data;
using CalendarApplication.UI.Event;
using Prism.Events;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace CalendarApplication.UI.ViewModel
{
    /// <summary>Klasa obsługująca widok menu nawigacyjnego, dziedzicząca po <see cref="ViewModelBase"/> oraz <see cref="INavigationViewModel"/></summary>
    public class NavigationViewModel : ViewModelBase, INavigationViewModel
    {
        /// <summary>Deklaracja właściwości obsługi pogrupowanych wydarzeń, które są przygotowane do wyświetlenia w menu nawigacyjnym</summary>
        private IIncidentLookupDataService _incidentLookupService;

        /// <summary>Deklaracja właściwości agregatora zdarzeń</summary>
        private IEventAggregator _eventAggregator;

        /// <summary>Deklaracja kolekcji wydarzeń.</summary>
        /// <value>The incidents.</value>
        public ObservableCollection<NavigationItemViewModel> Incidents { get; set; }

        /// <summary>Konstruktor klasy <see cref="NavigationViewModel" /> inicjalizujący kolekcję wydarzeń, a także pola przechowujące zdarzenia i pogrupowane wydarzenia do wyświetlenia.</summary>
        /// <param name="incidentLookupService">Pogrupowane wydarzenia na potrzeby wyświetlania w menu nawigacyjnym.</param>
        /// <param name="eventAggregator">Zbiór zdarzeń.</param>
        public NavigationViewModel(IIncidentLookupDataService incidentLookupService,
            IEventAggregator eventAggregator)
        {
            _incidentLookupService = incidentLookupService;
            _eventAggregator = eventAggregator;
            Incidents = new ObservableCollection<NavigationItemViewModel>();
            _eventAggregator.GetEvent<AfterIncidentSavedEvent>().Subscribe(AfterIncidentSaved);
            _eventAggregator.GetEvent<AfterIncidentDeletedEvent>().Subscribe(AfterIncidentDeleted);
        }

        /// <summary>Metoda wywoływana po usunięciu wydarzenia, która usuwa je z kolekcji <b>Incidents</b>.</summary>
        /// <param name="incidentId">Identyfikator usuwanego wydarzenia.</param>
        private void AfterIncidentDeleted(int incidentId)
        {
            var incident = Incidents.SingleOrDefault(i => i.Id == incidentId);
            if (incident != null)
            {
                Incidents.Remove(incident);
            }
        }

        /// <summary>Metoda wywoływana po zapisaniu wydarzenia, która dodaje takowe do kolekcji <b>Incidents</b>, bądź je edytuje, jeśli już istniało, a na koniec sortuje wydarzenia po dacie początkowej.</summary>
        /// <param name="obj">Instancja <see cref="AfterIncidentSavedEventArgs" /> zawierająca dane zdarzenia.</param>
        private void AfterIncidentSaved(AfterIncidentSavedEventArgs obj)
        {
            var lookupItem = Incidents.SingleOrDefault(l => l.Id == obj.Id);
            if (lookupItem == null)
            {
                Incidents.Add(new NavigationItemViewModel(obj.Id, obj.DisplayIncident, obj.DisplayDate,
                    _eventAggregator));
            }
            else
            {
                lookupItem.DisplayIncident = obj.DisplayIncident;
                lookupItem.DisplayDate = obj.DisplayDate;
            }
            SortIncidentsByDate(Incidents);
        }

        /// <summary>Metoda sortująca wydarzenia po dacie początkowej, od nastarszego do najnowszego.</summary>
        /// <param name="incidents">Wydarzenia do posortowania.</param>
        /// <returns>Posortowana kolekcja wydarzeń</returns>
        public static ObservableCollection<NavigationItemViewModel> SortIncidentsByDate(ObservableCollection<NavigationItemViewModel> incidents)
        {
            ObservableCollection<NavigationItemViewModel> nonSortIncidents;
            nonSortIncidents = new ObservableCollection<NavigationItemViewModel>(incidents.OrderBy(i => i.DisplayDate));
            incidents.Clear();
            foreach (var incident in nonSortIncidents) incidents.Add(incident);
            return incidents;
        }

        /// <summary>Metoda asynchroniczna, pobierająca wydarzenia, które będą wyświetlane posortowane w menu nawigacyjnym.</summary>
        public async Task LoadAsync()
        {
            var lookup = await _incidentLookupService.GetIncidentLookupAsync();
            Incidents.Clear();
            foreach (var item in lookup)
            {
                Incidents.Add(new NavigationItemViewModel(item.Id, item.DisplayIncident, item.DisplayDate,
                             _eventAggregator));
            }
            SortIncidentsByDate(Incidents);
        }
    }
}
