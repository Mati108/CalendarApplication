using CalendarApplication.Model;
using System.Threading.Tasks;

namespace CalendarApplication.UI.Data.Repositories
{
    /// <summary>Repozytorium do obsługi warstwy pośredniej między aplikacją, a bazą danych.</summary>
    public interface IIncidentRepository
    {
        /// <summary>Metoda asynchroniczna pobierająca dane z bazy, po id wydarzenia.</summary>
        /// <param name="incidentId">Identyfikator wydarzenia.</param>   
        Task<Incident> GetByIdAsync(int incidentId);

        /// <summary>Metoda asynchroniczna zapisująca dane do bazy.</summary>
        Task SaveAsync();

        /// <summary>Metoda sprawdzająca, czy zaszły zmiany w bazie danych.</summary>
        /// <value>
        ///   <c>true</c> jeśli zaszły zmiany. W przeciwnym przypadku <c>false</c>.
        /// </value>
        bool HasChanges();

        /// <summary>Metoda dodająca wydarzenie do bazy danych.</summary>
        /// <param name="incident">Dodawane wydarzenie.</param>
        void Add(Incident incident);

        /// <summary>Metoda usuwająca wydarzenie z bazy danych.</summary>
        /// <param name="model">Dodawane wydarzenie.</param>
        void Remove(Incident model);
    }
}