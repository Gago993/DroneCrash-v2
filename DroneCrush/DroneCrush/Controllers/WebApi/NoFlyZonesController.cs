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
using DroneCrush.Models.NotFlyZone;
using DroneCrush.Classes;

namespace DroneCrush.Controllers.WebApi
{
    public class NoFlyZonesController : ApiController
    {
        private DroneDb db = new DroneDb();

        // GET: api/NoFlyZones
        public IQueryable<NoFlyZone> GetNoFlyZones()
        {
            return db.NoFlyZones;
        }

        [HttpGet]
        [Route("api/noflyzones/nearby")]
        [ResponseType(typeof(IEnumerable<NoFlyZone>))]
        public IHttpActionResult GetNoFlyZonesNearby(double? lat = null, double? lng = null)
        {

            if (lat == null)
            {
                return BadRequest();
            }

            if (lng == null)
            {
                return BadRequest();
            }

            IEnumerable<NoFlyZone> zones = db.NoFlyZones.ToList();

            double lat1 = 38.897147;
            double lng1 = -77.043934;

            

            double distance = Helper.GetDistanceFromLatLonInMeters(Double.Parse(lat.ToString()), Double.Parse(lng.ToString()), lat1, lng1);

            return Ok();
        }

        // GET: api/NoFlyZones/5
        [ResponseType(typeof(NoFlyZone))]
        public IHttpActionResult GetNoFlyZone(int id)
        {
            NoFlyZone noFlyZone = db.NoFlyZones.Find(id);
            if (noFlyZone == null)
            {
                return NotFound();
            }

            return Ok(noFlyZone);
        }

        // PUT: api/NoFlyZones/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutNoFlyZone(int id, NoFlyZone noFlyZone)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != noFlyZone.ID)
            {
                return BadRequest();
            }

            db.Entry(noFlyZone).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NoFlyZoneExists(id))
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

        // POST: api/NoFlyZones
        [ResponseType(typeof(NoFlyZone))]
        public IHttpActionResult PostNoFlyZone(NoFlyZone noFlyZone)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.NoFlyZones.Add(noFlyZone);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = noFlyZone.ID }, noFlyZone);
        }

        // DELETE: api/NoFlyZones/5
        [ResponseType(typeof(NoFlyZone))]
        public IHttpActionResult DeleteNoFlyZone(int id)
        {
            NoFlyZone noFlyZone = db.NoFlyZones.Find(id);
            if (noFlyZone == null)
            {
                return NotFound();
            }

            db.NoFlyZones.Remove(noFlyZone);
            db.SaveChanges();

            return Ok(noFlyZone);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NoFlyZoneExists(int id)
        {
            return db.NoFlyZones.Count(e => e.ID == id) > 0;
        }
    }
}