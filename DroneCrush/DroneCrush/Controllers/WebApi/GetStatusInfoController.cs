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
        public IHttpActionResult GetDroneInfo(double ?lat = null, double ?lon = null)
        {

            if (lat == null || lon == null)
            {
                return BadRequest("Missing Parametars");
            }

            EnviromentInfoViewModel model = new EnviromentInfoViewModel();

            model.Coordinate = new Coordinate()
            {
                Latitude = Double.Parse(lat.ToString()),
                Longitude = Double.Parse(lon.ToString())
            };

            String yahooApi = "https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20weather.forecast%20where%20woeid%20in%20(select%20woeid%20from%20geo.places%20where%20text%3D%22("+lat+"%2C"+lon+")%22)&format=json&diagnostics=true&callback=";

            String googleElevationApiKey = "https://maps.googleapis.com/maps/api/elevation/json?locations="+lat+","+lon+"&key=AIzaSyDTkIcb_EL4fQFTQMe9DA1N5gyQHgmCRGM";



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


                var googleData = wc.DownloadString(googleElevationApiKey);
                dynamic elevationResult = JsonConvert.DeserializeObject(googleData);

                JObject elevation = elevationResult.results[0];
                model.Elevation = elevation.ToObject<Elevation>();
            }


            return Ok(model);
        }


    }
}
