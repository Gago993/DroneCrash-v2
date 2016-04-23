namespace DroneCrush.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "library.Coordinates",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "library.Drones",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DeviceToken = c.String(),
                        LastActive = c.DateTime(nullable: false),
                        Coordinate_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("library.Coordinates", t => t.Coordinate_ID)
                .Index(t => t.Coordinate_ID);
            
            CreateTable(
                "library.NoFlyZones",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NoFlyZoneName = c.String(),
                        NoFlyCategory = c.Int(nullable: false),
                        Coordinate_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("library.Coordinates", t => t.Coordinate_ID)
                .Index(t => t.Coordinate_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("library.NoFlyZones", "Coordinate_ID", "library.Coordinates");
            DropForeignKey("library.Drones", "Coordinate_ID", "library.Coordinates");
            DropIndex("library.NoFlyZones", new[] { "Coordinate_ID" });
            DropIndex("library.Drones", new[] { "Coordinate_ID" });
            DropTable("library.NoFlyZones");
            DropTable("library.Drones");
            DropTable("library.Coordinates");
        }
    }
}
