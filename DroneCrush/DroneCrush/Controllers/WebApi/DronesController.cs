﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using DroneCrush.DataContext;
using DroneCrush.Models;
using DroneCrush.Classes;

namespace DroneCrush.Controllers.WebApi
{
    public class DronesController : ApiController
    {
        private DroneDb db = new DroneDb();

        // GET: api/Drones
        public IEnumerable<Drone> GetDrone()
        {
            return db.Drone.Include("Coordinate").ToList();
        }

        // GET: api/Drones/5
        [ResponseType(typeof(IEnumerable<Drone>))]
        [Route("api/Drones/Nearby")]
        public IHttpActionResult GetNearbyDrones(double lat, double lng)
        {
            IEnumerable<Drone> drones = db.Drone.Include("Coordinate").ToList();
            ICollection<Drone> result = new List<Drone>();

            foreach(Drone drone in drones)
            {
                double distanceInMeters = Helper
                    .GetDistanceFromLatLonInMeters(lat, lng, drone.Coordinate.Latitude, drone.Coordinate.Longitude);

                if(distanceInMeters <= 8000)
                {
                    result.Add(drone);
                }
            }

            return Ok(result);
        }

        // GET: api/Drones/5
        [ResponseType(typeof(Drone))]
        public IHttpActionResult GetDrone(int id)
        {
            Drone drone = db.Drone.Where(d => d.ID == id).Include("Coordinate").FirstOrDefault();
            if (drone == null)
            {
                return NotFound();
            }

            return Ok(drone);
        }

        [HttpGet]
        [ResponseType(typeof(IEnumerable<Drone>))]
        [Route("api/Drones/Me")]
        public IHttpActionResult GetMyDrone(string DeviceToken)
        {
            Drone drone = db.Drone.Where(d => d.DeviceToken == DeviceToken).Include("Coordinate").FirstOrDefault();
            if (drone == null)
            {
                return NotFound();
            }

            return Ok(drone);
        }


        // PUT: api/Drones/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDrone(int id, Drone drone)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != drone.ID)
            {
                return BadRequest();
            }

            db.Entry(drone).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DroneExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Drones
        [ResponseType(typeof(Drone))]
        public IHttpActionResult PostDrone(Drone drone)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Drone dbDrone = db.Drone.Where(d => d.DeviceToken == drone.DeviceToken).FirstOrDefault();

            if (dbDrone == null)
            {
                db.Drone.Add(drone);
            }
            else
            {
                dbDrone.LastActive = DateTime.Now;
                dbDrone.Coordinate = drone.Coordinate;
                db.Entry(dbDrone).State = EntityState.Modified;
            }

            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = drone.ID }, drone);
        }

        // DELETE: api/Drones/5
        [ResponseType(typeof(Drone))]
        public IHttpActionResult DeleteDrone(int id)
        {
            Drone drone = db.Drone.Find(id);
            if (drone == null)
            {
                return NotFound();
            }

            db.Drone.Remove(drone);
            db.SaveChanges();

            return Ok(drone);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DroneExists(int id)
        {
            return db.Drone.Count(e => e.ID == id) > 0;
        }
    }
}