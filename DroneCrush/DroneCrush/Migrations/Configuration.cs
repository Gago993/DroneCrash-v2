namespace DroneCrush.Migrations
{
    using DroneCrush.DataContext;
    using DroneCrush.Models;
    using DroneCrush.Models.NotFlyZone;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Reflection;

    internal sealed class Configuration : DbMigrationsConfiguration<DroneCrush.DataContext.DroneDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\Database";
        }

        protected override void Seed(DroneDb context)
        {
           LoadNoFlyZones(context);

            context.Drone.Add(new Drone()
            {
                ID = 1,
                DeviceToken = "esfads",
                Coordinate = new Coordinate(){
                    Latitude = 42.2312,
                    Longitude = 41.3433
                },
                LastActive = DateTime.Now
            });

            context.SaveChanges();
        }

        #region NoFlyZoneData
            private void LoadNoFlyZones(DroneDb context){
                context.Configuration.AutoDetectChangesEnabled = false;

                var resourceName = "DroneCrush.Models.SeedData.noFlyZones.txt";
                var assembly = Assembly.GetExecutingAssembly();
                var stream = assembly.GetManifestResourceStream(resourceName);

                var line = "";

                // Read the file and display it line by line.
                System.IO.StreamReader fileRead =
                   new System.IO.StreamReader(stream);
                while ((line = fileRead.ReadLine()) != null)
                {
                    string[] components = line.Split(';');

                    string lat = components[0];
                    string lng = components[1];
                    string zoneName = components[2];
                    zoneName.Trim();
                    string category = components[3];

                    NoFlyCategory zoneCategory;
                    if(category == "Category A")
                        zoneCategory = NoFlyCategory.A;
                    else
                        zoneCategory = NoFlyCategory.B;

                    context.NoFlyZones.Add(new NoFlyZone { Latitude = lat, Longitude = lng, NoFlyZoneName = zoneName, NoFlyCategory = zoneCategory});
                }

                fileRead.Close();
                context.SaveChanges();
            }
        #endregion
    }
}
