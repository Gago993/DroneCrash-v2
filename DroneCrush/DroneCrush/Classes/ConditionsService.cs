using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DroneCrush.Classes
{
    public class ConditionsService
    {
        public List<string> GetCloudyConditions()
        {
            return new List<string>() { "cloudy", "mostly cloudy (night)", "partly cloudy", "mostly cloudy (day)", "partly cloudy (night)", "partly cloudy (day)" };
        }

        public List<string> GetExtremeConditions()
        {
            return new List<string>() { "tornado", "tropical storm", "hurricane", "severe thunderstorms", "thunderstorms" };
        }

        public List<string> GetFoggyConditions()
        {
            return new List<string>() { "foggy", "haze", "smoky", "blustery", "dust", "windy", "cold" };
        }

        public List<string> GetSnowConditions()
        {
            return new List<string>() { "mixed snow and sleet", "snow showers", "freezing drizzle", "freezing rain", "heavy snow", "scattered snow showers", "snow flurries", "light snow showers", "blowing snow", "snow", "hail", "sleet" };
        }

        public List<string> GetClearConditions()
        {
            return new List<string>() { "clear (night)", "sunny", "not available", "fair (night)", "fair (day)", "hot" };
        }

        public List<string> GetRainConditions()
        {
            return new List<string>() { "mixed rain and snow", "isolated thundershowers", "mixed rain and sleet", "thundershowers", "drizzle", "showers", "mixed rain and hail", "isolated thunderstorms", "scattered thunderstorms", "scattered showers" };
        }

        public string GetId(string condition)
        {
            string result = "";
            if (GetCloudyConditions().Contains(condition))
                result = "cloudy";
            else if (GetExtremeConditions().Contains(condition))
                result = "extreme";
            else if (GetFoggyConditions().Contains(condition))
                result = "foggy";
            else if (GetSnowConditions().Contains(condition))
                result = "snowy";
            else if (GetRainConditions().Contains(condition))
                result = "rainy";
            else
                result = "clear";

            return result;
        }
    }
}