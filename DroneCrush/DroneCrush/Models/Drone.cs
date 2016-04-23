using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DroneCrush.Models
{
    public class Drone
    {
        public int ID { get; set; }
        public string DeviceToken { get; set; }
        public Coordinate Coordinate { get; set; }
        public DateTime LastActive { get; set; }
    }
}