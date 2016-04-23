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
            //LoadNoFlyZones(context);
            LoadDummyDrones(context);
        }

        private void LoadDummyDrones(DroneDb context)
        {
            var drones = new List<Drone>
            {
                new Drone
                {
                    DeviceToken = "Drone1",
                    LastActive = DateTime.Now,
                    Name = "Drone 2",
                    Coordinate = new Coordinate()
                    {
                        Latitude = 41.993739,
                        Longitude = 21.4359526
                    },
                },
                new Drone
                {
                    DeviceToken = "Drone2",
                    LastActive = DateTime.Now,
                    Name = "Drone 2",
                    Coordinate = new Coordinate()
                    {
                        Latitude = 41.9969603,
                        Longitude = 21.4211897
                    }
                },
                new Drone
                {
                    DeviceToken = "Drone3",
                    LastActive = DateTime.Now,
                    Name = "Drone 3",
                    Coordinate = new Coordinate()
                    {
                        Latitude = 41.9969603,
                        Longitude = 21.4211897
                    }
                },
                new Drone
                {
                    DeviceToken = "Drone4",
                    LastActive = DateTime.Now,
                    Name = "Drone 4",
                    Coordinate = new Coordinate()
                    {
                        Latitude = 42.0047346,
                        Longitude = 21.4124362
                    }
                },
                new Drone
                {
                    DeviceToken = "Drone5",
                    LastActive = DateTime.Now,
                    Name = "Drone 5",
                    Coordinate = new Coordinate()
                    {
                        Latitude = 42.0648949,
                        Longitude = 21.2218569
                    }
                }
            };

            drones.ForEach(d => context.Drone.AddOrUpdate(c => c.DeviceToken, d));
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

                  //  context.NoFlyZones.Add(new NoFlyZone { Latitude = lat, Longitude = lng, NoFlyZoneName = zoneName, NoFlyCategory = zoneCategory});
                }

                fileRead.Close();
                context.SaveChanges();
            }
        #endregion
    }
}
