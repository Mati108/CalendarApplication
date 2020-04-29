using CalendarApplication.DataAccess;
using CalendarApplication.Model;
using System.Data.Entity;
using System.Threading.Tasks;

namespace CalendarApplication.UI.Data.Repositories
{
    public class IncidentRepository : IIncidentRepository
    {
        private CalendarApplicationDbContext _context;
        public IncidentRepository(CalendarApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Incident> GetByIdAsync(int incidentId)
        {
            return await _context.Incidents.SingleAsync(i => i.Id == incidentId);
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }
        public void Add(Incident incident)
        {
            _context.Incidents.Add(incident);
        }

        public void Remove(Incident model)
        {
            _context.Incidents.Remove(model);
        }
    }
}
