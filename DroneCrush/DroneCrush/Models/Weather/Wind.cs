using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DroneCrush.Models
{
    public class Wind
    {
        public int chill { get; set; }
        public int direction { get; set; }
        public double speed { get; set; }
        public string directionSymbol { get; set; }
    }
}