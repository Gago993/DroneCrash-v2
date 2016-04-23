using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DroneCrush.Models.NotFlyZone
{
    public enum NoFlyCategory {
        A = 5, B = 1
    }

    public class NoFlyZone
    {
        [Key]
        public int ID { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public string NoFlyZoneName { get; set; }

        public NoFlyCategory NoFlyCategory { get; set; }
    }
}