using CalendarApplication.Model;
using System.Threading.Tasks;

namespace CalendarApplication.UI.Data.Repositories
{
    public interface IIncidentRepository
    {
        Task<Incident> GetByIdAsync(int incidentId);
        Task SaveAsync();
        bool HasChanges();
        void Add(Incident incident);
        void Remove(Incident model);
    }
}