using CalendarApplication.DataAccess;
using CalendarApplication.Model;
using System.Data.Entity;
using System.Threading.Tasks;

namespace CalendarApplication.UI.Data.Repositories
{
    /// <summary>Klasa obsługująca warstwę pośrednią między aplikacją, a bazą danych. Dziedziczy po interfejsie <see cref="IIncidentRepository"/>.</summary>
    public class IncidentRepository : IIncidentRepository
    {
        /// <summary>Kontekst bazy danych.</summary>
        private CalendarApplicationDbContext _context;

        /// <summary>Konstruktor klasy <see cref="IncidentRepository" />.</summary>
        /// <param name="context">Przekazywany kontekst bazy danych.</param>
        public IncidentRepository(CalendarApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>Metoda asynchroniczna pobierająca dane z bazy, po id wydarzenia.</summary>
        /// <param name="incidentId">Identyfikator wydarzenia.</param>   
        public async Task<Incident> GetByIdAsync(int incidentId)
        {
            return await _context.Incidents.SingleAsync(i => i.Id == incidentId);
        }

        /// <summary>Metoda asynchroniczna zapisująca dane do bazy.</summary>
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        /// <summary>Metoda sprawdzająca, czy zaszły zmiany w bazie danych.</summary>
        /// <value>
        ///   <c>true</c> jeśli zaszły zmiany. W przeciwnym przypadku <c>false</c>.
        /// </value>
        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }

        /// <summary>Metoda dodająca wydarzenie do bazy danych.</summary>
        /// <param name="incident">Dodawane wydarzenie.</param>
        public void Add(Incident incident)
        {
            _context.Incidents.Add(incident);
        }

        /// <summary>Metoda usuwająca wydarzenie z bazy danych.</summary>
        /// <param name="model">Dodawane wydarzenie.</param>
        public void Remove(Incident model)
        {
            _context.Incidents.Remove(model);
        }
    }
}
