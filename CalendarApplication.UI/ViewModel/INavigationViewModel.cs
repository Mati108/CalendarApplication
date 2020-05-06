using System.Threading.Tasks;

namespace CalendarApplication.UI.ViewModel
{
    /// <summary>Interfejs do obsługi menu nawigacyjnego.</summary>
    public interface INavigationViewModel
    {
        /// <summary>Metoda asynchroniczna, pobierająca wydarzenia, które będą wyświetlane posortowane w menu nawigacyjnym.</summary>
        Task LoadAsync();
    }
}