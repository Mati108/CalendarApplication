using Prism.Events;

namespace CalendarApplication.UI.Event
{
    /// <summary>
    /// Klasa dziedzicząca po <see cref="PubSubEvent"/>, która ułatwia zrozumienie kodu w innych jedo częściach. 
    /// Zamiast pisać instancję <see cref="PubSubEvent"/>, wykorzystujemy czytelną nazwę tej klasy.
    /// </summary>
    public class OpenIncidentDetailViewEvent : PubSubEvent<int?>
    {
    }
}
