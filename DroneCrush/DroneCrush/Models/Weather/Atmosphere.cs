using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DroneCrush.Models
{
    public class Atmosphere
    {
        public int humidity { get; set; }
        public decimal pressure { get; set; }
        public int rising { get; set; }
	public decimal visibility { get; set; }

    }
}