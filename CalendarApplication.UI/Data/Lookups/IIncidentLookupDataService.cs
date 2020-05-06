using CalendarApplication.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CalendarApplication.UI.Data
{
    /// <summary>Interfejs do obsługi wyszukiwania danych wydarzeń.</summary>
    public interface IIncidentLookupDataService
    {
        /// <summary>Metoda asynchronicznie wyszukująca wydarzenia, pobierając je za pomocą metody <b>AsNoTracking</b>,
        /// która niepotrzebnie ich nie przechowuje w pamięci, a następnie przekazująca je asynchronicznie do listy.</summary>
        /// <returns>Kontekst bazy z listą zadań.</returns>
        Task<IEnumerable<LookupItem>> GetIncidentLookupAsync();
    }
}
