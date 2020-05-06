using CalendarApplication.DataAccess;
using CalendarApplication.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace CalendarApplication.UI.Data
{
    /// <summary>Klasa obsługująca wyszukiwanie danych wydarzeń. Dziedziczy po interfejsie <see cref="IIncidentLookupDataService"/>.</summary>
    public class IncidentLookupDataService : IIncidentLookupDataService
    {
        /// <summary>Deklaracja kontekstu bazy danych.</summary>
        private Func<CalendarApplicationDbContext> _contextCreator;

        /// <summary>Konstruktor klasy <see cref="IncidentLookupDataService" /> inicjaluzujący kontekst bazy danych.</summary>
        /// <param name="contextCreator">The context creator.</param>
        public IncidentLookupDataService(Func<CalendarApplicationDbContext> contextCreator)
        {
            _contextCreator = contextCreator;
        }

        /// <summary>Metoda asynchronicznie wyszukująca wydarzenia, pobierając je za pomocą metody <b>AsNoTracking</b>,
        /// która niepotrzebnie ich nie przechowuje w pamięci, a następnie przekazująca je asynchronicznie do listy.</summary>
        /// <returns>Kontekst bazy z listą zadań.</returns>
        public async Task<IEnumerable<LookupItem>> GetIncidentLookupAsync()
        {
            using (var ctx = _contextCreator())
            {
                return await ctx.Incidents.AsNoTracking()
                    .Select(i =>
                    new LookupItem
                    {
                        Id = i.Id,
                        DisplayIncident = i.Title,
                        DisplayDate = i.DateStart,
                    })
                    .ToListAsync();
            }
        }
    }
}
