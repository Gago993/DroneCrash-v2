using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace DroneCrush.Models
{
    public class EnviromentInfoViewModel
    {
        public Wind Wind { get; set; }
        public Unit Unit { get; set; }
        public Location Location { get; set; }
        public Atmosphere Atmosphere { get; set; }
        public Elevation Elevation { get; set; }
        public Coordinate Coordinate { get; set; }
    }
}