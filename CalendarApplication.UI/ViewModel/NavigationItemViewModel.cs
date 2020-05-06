using CalendarApplication.UI.Event;
using Prism.Commands;
using Prism.Events;
using System;
using System.Windows.Input;

namespace CalendarApplication.UI.ViewModel
{
    /// <summary>Klasa opisująca szczegółowy widok wyświetlanego wydarzenia, dziedzicząca po <see cref="ViewModelBase"/>.</summary>
    public class NavigationItemViewModel : ViewModelBase
    {
        /// <summary>Deklaracja właściwości nazwy wyświetlanego wydarzenia</summary>
        private string _displayIncident;

        /// <summary>Deklaracja właściwości zbioru zdarzeń.</summary>
        private IEventAggregator _eventAggregator;

        /// <summary>Deklaracja właściwości identyfikatora wydarzenia.</summary>
        /// <value>Identyfikator wydarzenia.</value>
        public int Id { get; }

        /// <summary>Data wydarzenia potrzebna do ich sortowania.</summary>
        public DateTime _displayDate;

        /// <summary>Deklaracja polecenia szczegółowego widoku wyświetlanego wydarzenia.</summary>
        /// <value>Polecenie szczegółowego widoku wyświetlanego wydarzenia.</value>
        public ICommand OpenIncidentDetailViewCommand { get; }

        /// <summary>Konstruktor klasy <see cref="NavigationItemViewModel" />inicjalizujący oprócz wymienionych pól, również widok szczegółowy wyświetlanego wydarzenia.</summary>
        /// <param name="id">Identyfikator wydarzenia.</param>
        /// <param name="displayIncident">Wyświetlana nazwa wydarzenia.</param>
        /// <param name="displayDate">Wyświetlana data wydarzenia.</param>
        /// <param name="eventAggregator">Zbiór zdarzeń.</param>
        public NavigationItemViewModel(int id, string displayIncident, DateTime displayDate,
            IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            Id = id;
            DisplayIncident = displayIncident;

            DisplayDate = displayDate;
            OpenIncidentDetailViewCommand = new DelegateCommand(OnOpenIncidentDetailView);
        }

        /// <summary>Metoda pobierająca szczegóły o wyświetlanym wydarzeniu.</summary>
        private void OnOpenIncidentDetailView()
        {
            _eventAggregator.GetEvent<OpenIncidentDetailViewEvent>()
            .Publish(Id);
        }

        /// <summary>Deklaracja właściwości wyświetlanej nazwy wydarzenia.</summary>
        /// <value>Wyświetlana nazwa wydarzenia.</value>
        public string DisplayIncident
        {
            get { return _displayIncident; }
            set
            {
                _displayIncident = value;
                OnPropertyChanged();
            }
        }

        /// <summary>Deklaracja właściwości wyświetlanej daty, która jest potrzebna do posortowania wydarzeń.</summary>
        /// <value>Data po której następuje sortowanie.</value>
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

