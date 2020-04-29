using CalendarApplication.DataAccess;
using CalendarApplication.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace CalendarApplication.UI.Data
{
    public class IncidentLookupDataService : IIncidentLookupDataService
    {
        private Func<CalendarApplicationDbContext> _contextCreator;
        public IncidentLookupDataService(Func<CalendarApplicationDbContext> contextCreator)
        {
            _contextCreator = contextCreator;
        }
        public async Task<IEnumerable<LookupItem>> GetIncidentLookupAsync()
        {
            using (var ctx = _contextCreator())
            {
                return await ctx.Incidents.AsNoTracking()
                    .Select(i =>
                    new LookupItem
                    {
                        Id = i.Id,
                        DisplayIncident = i.Title
                    })
                    .ToListAsync();
            }
        }
    }
}
