namespace CalendarApplication.DataAccess.Migrations
{
    using CalendarApplication.Model;
    using System.Data.Entity.Migrations;

    /// <summary>Klasa opisująca bazę danych. Utworzona dzięki podejściu <b>code-first</b>. Dziedziczy po klasie <see cref="DbMigration"/>.</summary>
    public partial class CalendarApplicationDatabase : DbMigration
    {
        /// <summary>Wygenerowana na podstawie modelu <see cref="Incident"/> definicja tabeli.</summary>
        public override void Up()
        {
            CreateTable(
                "dbo.Incident",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Title = c.String(nullable: false, maxLength: 50),
                    DateStart = c.DateTime(nullable: false),
                    DateStop = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.Id);
        }

        /// <summary>Operacje do wykonania podczas procesu usuwania tabeli.</summary>
        public override void Down()
        {
            DropTable("dbo.Incident");
        }
    }
}
