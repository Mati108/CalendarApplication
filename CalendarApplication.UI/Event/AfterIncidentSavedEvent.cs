using Prism.Events;
using System;

namespace CalendarApplication.UI.Event
{
    public class AfterIncidentSavedEvent : PubSubEvent<AfterIncidentSavedEventArgs>
    {
    }
    public class AfterIncidentSavedEventArgs
    {
        public int Id { get; set; }
        public string DisplayIncident { get; set; }
        public DateTime DisplayDate { get; set; }
    }
}
