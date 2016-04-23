using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DroneCrush.Models.NotFlyZone
{
    public enum NoFlyCategory {
        A = 80, B = 24
    }

    public class NoFlyZone
    {
        [Key]
        public int ID { get; set; }

        public Coordinate Coordinate { get; set; }

        public string NoFlyZoneName { get; set; }

        public NoFlyCategory NoFlyCategory { get; set; }

        [NotMapped]
        public double NoFlyCategoryKm { get; set; }
    }
}