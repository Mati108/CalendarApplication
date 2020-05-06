namespace CalendarApplication.DataAccess.Migrations
{
    using CalendarApplication.Model;
    using System;
    using System.Data.Entity.Migrations;

    /// <summary>Klasa konfiguracyjna bazy danych. Dziedziczy po <see cref="DbMigrationsConfiguration"/>.</summary>
    internal sealed class Configuration : DbMigrationsConfiguration<CalendarApplicationDbContext>
    {
        /// <summary>Konstruktor klasy <see cref="Configuration" />. Nie zezwala na automatyczne migracje bazy.</summary>
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        /// <summary>
        /// Metoda uruchamiana po każdej migracji bazy danych lub przy pierwszym uruchomieniu aplikacji.
        /// Dzięki takiemu rozwiązaniu po aktualizacji i usunięciu danych z bazy danych zostają wprowadzone dane startowe,
        /// przez co baza nie jest całkowicie pusta.
        /// </summary>
        /// <param name="context">Kontekst używany do aktualizacji danych początkowych.</param>
        /// <remarks>
        /// Wdrożenie tej metody musi sprawdzać, czy dane nasion są obecne i / lub aktualne, a następnie wprowadzać zmiany tylko w razie potrzeby i w sposób nieniszczący.
        /// Aby to wykonać można użyć <see cref="M:System.Data.Entity.Migrations.DbSetMigrationsExtensions.AddOrUpdate``1(System.Data.Entity.IDbSet{``0},``0[])">AddOrUpdate``1(System.Data.Entity.IDbSet{``0},``0[])</see>
        /// Jeśli używany jest inicjator bazy danych <see cref="T:System.Data.Entity.MigrateDatabaseToLatestVersion`2">MigrateDatabaseToLatestVersion</see> wówczas ta metoda będzie wywoływana za każdym razem, gdy uruchomi się inicjator. Jeśli używany jest jeden z inicjatorów <see cref="T:System.Data.Entity.DropCreateDatabaseAlways`1">DropCreateDatabaseAlways</see>, <see cref="T:System.Data.Entity.DropCreateDatabaseIfModelChanges`1">DropCreateDatabaseIfModelChanges</see> lub <see cref="T:System.Data.Entity.CreateDatabaseIfNotExists`1">CreateDatabaseIfNotExists</see> metoda ta nie zostanie uruchomiona zamiast tego należy użyć zdefiniowanej w inicjalizatorze.
        /// </remarks>
        protected override void Seed(CalendarApplication.DataAccess.CalendarApplicationDbContext context)
        {
            context.Incidents.AddOrUpdate(
                i => i.Title,
                new Incident { Title = "Ogladanie zuzelka", DateStart = new DateTime(2020, 6, 12), DateStop = new DateTime(2020, 6, 12) }
                );
        }
    }
}
