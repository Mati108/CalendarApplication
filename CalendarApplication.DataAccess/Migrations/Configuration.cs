namespace CalendarApplication.DataAccess.Migrations
{
    using CalendarApplication.Model;
    using System;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<CalendarApplication.DataAccess.CalendarApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CalendarApplication.DataAccess.CalendarApplicationDbContext context)
        {
            context.Incidents.AddOrUpdate(
                i => i.Title,
                new Incident { Title = "Ogladanie zuzelka", DateStart = new DateTime(2020, 6, 12), DateStop = new DateTime(2020, 6, 12) }
                );
        }
    }
}
