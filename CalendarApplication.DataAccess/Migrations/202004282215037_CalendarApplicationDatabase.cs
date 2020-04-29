namespace CalendarApplication.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CalendarApplicationDatabase : DbMigration
    {
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
        
        public override void Down()
        {
            DropTable("dbo.Incident");
        }
    }
}
