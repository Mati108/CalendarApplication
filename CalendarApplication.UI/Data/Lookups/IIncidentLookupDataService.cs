using CalendarApplication.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CalendarApplication.UI.Data
{
    public interface IIncidentLookupDataService
    {
        Task<IEnumerable<LookupItem>> GetIncidentLookupAsync();
    }
}
