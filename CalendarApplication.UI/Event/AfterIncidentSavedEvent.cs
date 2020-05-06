using Prism.Events;
using System;

namespace CalendarApplication.UI.Event
{
    /// <summary>Klasa obsługująca zdarzenie po zapisaniu wydarzenia. Dziedziczy po <see cref="PubSubEvent"/>.</summary>
    public class AfterIncidentSavedEvent : PubSubEvent<AfterIncidentSavedEventArgs>
    {
    }
    public class AfterIncidentSavedEventArgs
    {
        /// <summary>Deklaracja identyfikatora wydarzenia.</summary>
        /// <value>Identyfikator wydarzenia.</value>
        public int Id { get; set; }

        /// <summary>Deklaracja wyświetlanej nazwy wydarzenia.</summary>
        /// <value>Wyświetlana nazwa wydarzenia.</value>
        public string DisplayIncident { get; set; }

        /// <summary>Deklaracja wyświetlanej daty wydarzenia.</summary>
        /// <value>Wyświetlana data wydarzenia.</value>
        public DateTime DisplayDate { get; set; }
    }
}
