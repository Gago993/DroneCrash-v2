using DroneCrush.Classes;
using DroneCrush.DataContext;
using DroneCrush.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace DroneCrush.Controllers.WebApi
{
    public class GetStatusInfoController : ApiController
    {
        private DroneDb db = new DroneDb();

        [HttpGet]
        [ResponseType(typeof(EnviromentInfoViewModel))]
        public IHttpActionResult GetDroneInfo(double ?lat = null, double? lng = null)
        {

            if (lat == null || lng == null)
            {
                return BadRequest("Missing Parametars");
            }

            EnviromentInfoViewModel model = new EnviromentInfoViewModel();

            model.Coordinate = new Coordinate()
            {
                Latitude = Double.Parse(lat.ToString()),
                Longitude = Double.Parse(lng.ToString())
            };

            String yahooApi = "https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20weather.forecast%20where%20woeid%20in%20(select%20woeid%20from%20geo.places%20where%20text%3D%22("+lat+"%2C"+lng+")%22)&format=json&diagnostics=true&callback=";

            String googleElevationApiKey = "https://maps.googleapis.com/maps/api/elevation/json?locations="+lat+","+lng+"&key=AIzaSyDTkIcb_EL4fQFTQMe9DA1N5gyQHgmCRGM";



            using (WebClient wc = new WebClient())
            {
                var yahooWeatherData = wc.DownloadString(yahooApi);
                dynamic weatherResult = JsonConvert.DeserializeObject(yahooWeatherData);

                JObject result = weatherResult.query.results;
                if (result == null)
                {
                    return InternalServerError();
                };

                JObject wind = weatherResult.query.results.channel.wind;
                model.Wind = wind.ToObject<Wind>();

                JObject unit = weatherResult.query.results.channel.units;
                model.Unit = unit.ToObject<Unit>();

                JObject location = weatherResult.query.results.channel.location;
                model.Location = location.ToObject<Location>();

                JObject atmosphere = weatherResult.query.results.channel.atmosphere;
                model.Atmosphere = atmosphere.ToObject<Atmosphere>();

                JValue item = weatherResult.query.results.channel.item.condition.text;
                model.Condition = item.ToObject<string>();

                var googleData = wc.DownloadString(googleElevationApiKey);
                dynamic elevationResult = JsonConvert.DeserializeObject(googleData);

                JObject elevation = elevationResult.results[0];
                model.Elevation = elevation.ToObject<Elevation>();
            }


            return Ok(model);
        }

        // humidity (0 - 100) %
        // windSpeed mph
        // visibility  miles
        // numberOfDrones drones
        [Route("api/GetStatusInfo/Probability")]
        public double GetProbability(double humidity = 10, double windSpeed = 50, double visibility = 1, int numberOfDrones = 40, string condition = "Sunny")
        {
            ConditionsService conditionsService = new ConditionsService();
            double result = 0;

            // Humidity
            if (humidity >= 0 && humidity <= 20 || humidity > 80 && humidity <= 100)
                result += 0.1 * 80;
            else if (humidity > 20 && humidity < 40 || humidity >= 60 && humidity <= 80)
                result += 0.1 * 90;
            else result += 0.1 * 100;

            // Wind speed
            if (windSpeed == 0)
                result += 0.3 * 100;
            else if (windSpeed <= 5)
                result += 0.3 * 85;
            else if (windSpeed <= 10)
                result += 0.3 * 60;
            else if (windSpeed <= 25)
                result += 0.3 * 20;
            else if (windSpeed <= 40)
                result += 0.3 * 5;
            else result += 0;

            // Visibility
            if (visibility <= 1)
                result += 0.2 * 5;
            else if (visibility <= 5)
                result += 0.2 * 60;
            else if (visibility <= 12)
                result += 0.2 * 90;
            else result += 0.2 * 100;

            // Number of Drones
            if (numberOfDrones == 0)
                result += 0.2 * 100;
            else if (numberOfDrones < 3)
                result += 0.2 * 70;
            else if (numberOfDrones < 8)
                result += 0.2 * 40;
            else if (numberOfDrones < 15)
                result += 0.2 * 10;
            else result += 0;

            // Weather conditions
            if (conditionsService.GetCloudyConditions().Contains(condition))
                result += 0.2 * 80;
            else if (conditionsService.GetExtremeConditions().Contains(condition))
                result += 0.2 * 0;
            else if (conditionsService.GetFoggyConditions().Contains(condition))
                result += 0.2 * 10;
            else if (conditionsService.GetSnowConditions().Contains(condition))
                result += 0.2 * 40;
            else if (conditionsService.GetRainConditions().Contains(condition))
                result += 0.2 * 30;
            else if (conditionsService.GetClearConditions().Contains(condition))
                result += 0.2 * 100;

            return result;
        }
    }
}
