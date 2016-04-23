using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DroneCrush.Classes
{
    public class Helper
    {
        public static double GetDistanceFromLatLonInMeters(double lat, double lng, double lat1, double lng1)
        {
            var R = 6373; // Radius of the earth in km
            double dlat = Deg2Rad(lat1 - lat);
            double dlon = Deg2Rad(lng1 - lng);


            double a = Calculate(dlat, dlon, lat, lat1);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            double d = R * c;

            return d * 1000;

        }

        private static double Calculate(double dlat, double dlon, double lat, double lat1)
        {
            return ((Math.Sin(dlat / 2) * Math.Sin(dlat / 2)) + (Math.Cos(Deg2Rad(lat)) * Math.Cos(Deg2Rad(lat1)) * Math.Sin(dlon / 2) * Math.Sin(dlon / 2)));
        }

        private static double Deg2Rad(double deg)
        {
            return deg * (Math.PI / 180);
        }
    }
}