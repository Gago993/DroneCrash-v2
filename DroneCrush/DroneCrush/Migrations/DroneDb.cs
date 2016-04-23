using DroneCrush.Models;
using DroneCrush.Models.NotFlyZone;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DroneCrush.DataContext
{
    public class DroneDb : DbContext
    {
        public DbSet<Drone> Drone { get; set; }
        public DbSet<NoFlyZone> NoFlyZones { get; set; }

        public DroneDb() : base("DefaultConnection")
        {
           
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.HasDefaultSchema("library");

            base.OnModelCreating(modelBuilder);
        }
    
    }

}