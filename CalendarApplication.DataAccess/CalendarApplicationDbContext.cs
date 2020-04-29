using CalendarApplication.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarApplication.DataAccess
{
    public class CalendarApplicationDbContext : DbContext
    {
        public CalendarApplicationDbContext():base("CalendarApplicationDb")
        {

        }

        public DbSet<Incident> Incidents { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
