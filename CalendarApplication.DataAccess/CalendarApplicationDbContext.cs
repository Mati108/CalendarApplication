using CalendarApplication.Model;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace CalendarApplication.DataAccess
{
    /// <summary>Klasa obsługująca kontekst bazy danych, który pozwala na jej obsługę z poziomu aplikacji. Dziedziczy po klasie <see cref="DbContext"/>.</summary>
    public class CalendarApplicationDbContext : DbContext
    {
        /// <summary>Konstruktor klasy <see cref="CalendarApplicationDbContext" />. Dziedziczy po bazie danych, dzięki czemu ma do niej dostęp.</summary>
        public CalendarApplicationDbContext() : base("CalendarApplicationDb")
        {

        }

        /// <summary>Deklaracja reprezentacji encji wydarzeń.</summary>
        /// <value>Wydarzenia.</value>
        public DbSet<Incident> Incidents { get; set; }

        /// <summary>
        /// Ta metoda jest wywoływana, gdy model kontekstu pochodnego został zainicjowany,
        /// ale zanim model został zablokowany i użyty do zainicjowania kontekstu. Domyślna implementacja tej metody nic nie robi, 
        /// ale można ją przesłonić w klasie pochodnej, aby model mógł być dalej konfigurowany przed zablokowaniem.
        /// </summary>
        /// <param name="modelBuilder">Definiuje model tworzonego kontekstu.</param>
        /// <remarks> 
        /// Zazwyczaj ta metoda jest wywoływana tylko raz, gdy tworzona jest pierwsza instancja kontekstu pochodnego. 
        /// Model tego kontekstu jest następnie buforowany i dotyczy wszystkich dalszych wystąpień kontekstu w domenie aplikacji. 
        /// To buforowanie można wyłączyć, ustawiając właściwość ModelCaching na danym ModelBuilder, ale należy pamiętać, 
        /// że może to poważnie obniżyć wydajność. Większą kontrolę nad buforowaniem zapewnia bezpośrednie użycie klas DbModelBuilder i DbContextFactory.
        /// </remarks>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
