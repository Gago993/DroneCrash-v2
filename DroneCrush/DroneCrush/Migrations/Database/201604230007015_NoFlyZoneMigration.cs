namespace DroneCrush.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NoFlyZoneMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "library.NoFlyZones",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Latitude = c.String(),
                        Longitude = c.String(),
                        NoFlyZoneName = c.String(),
                        NoFlyCategory = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("library.NoFlyZones");
        }
    }
}
