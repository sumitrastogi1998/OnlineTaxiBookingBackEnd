using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApiBackEnd;

namespace WebApiBackEnd.Controllers
{
    [RoutePrefix("api/Taxi")]
    public class TaxiController : ApiController
    {
        private taxi_booking_systemEntities db = new taxi_booking_systemEntities();

        // GET: api/Taxi
        [Route("GetAllTaxis")]
        public IQueryable<Taxi> GetTaxis()
        {
            
                return db.Taxis;
            
        }

        // GET: api/Taxi/5
        [ResponseType(typeof(Taxi))]
        public IHttpActionResult GetTaxi(int id)
        {
            Taxi taxi = db.Taxis.Find(id);
            if (taxi == null)
            {
                return NotFound();
            }

            return Ok(taxi);
        }

        // PUT: api/Taxi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTaxi(int id, Taxi taxi)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != taxi.TaxiID)
            {
                return BadRequest();
            }

            db.Entry(taxi).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaxiExists(id))
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

        // POST: api/Taxi
        [ResponseType(typeof(Taxi))]
        public IHttpActionResult PostTaxi(Taxi taxi)
        {
            try
            {
                db.Taxis.Add(taxi);
                db.SaveChanges();

                return CreatedAtRoute("DefaultApi", new { id = taxi.TaxiID }, taxi);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.InternalServerError, "Sql server not connected");
            }
        }

        // DELETE: api/Taxi/5
        [ResponseType(typeof(Taxi))]
        public IHttpActionResult DeleteTaxi(int id)
        {
            Taxi taxi = db.Taxis.Find(id);
            if (taxi == null)
            {
                return NotFound();
            }

            db.Taxis.Remove(taxi);
            db.SaveChanges();

            return Ok(taxi);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TaxiExists(int id)
        {
            return db.Taxis.Count(e => e.TaxiID == id) > 0;
        }
    }
}