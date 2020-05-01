using CalendarApplication.Model;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

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
