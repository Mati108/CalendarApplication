using Prism.Events;

namespace CalendarApplication.UI.Event
{
    public class AfterIncidentSavedEvent : PubSubEvent<AfterIncidentSavedEventArgs>
    {
    }
    public class AfterIncidentSavedEventArgs
    {
        public int Id { get; set; }
        public string DisplayIncident { get; set; }
    }
}
