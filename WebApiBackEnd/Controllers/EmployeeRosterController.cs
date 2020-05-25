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
using WebApiBackEnd.Models;

namespace WebApiBackEnd.Controllers
{
    [RoutePrefix("api/EmployeeRoster")]
    public class EmployeeRosterController : ApiController
    {
        private taxi_booking_systemEntities db = new taxi_booking_systemEntities();

        // GET: api/EmployeeRoster
        [HttpGet]
        [Route("GetAllRosters")]
        public IQueryable<EmployeeRoster> GetEmployeeRosters()
        {
            return db.EmployeeRosters;
        }

        [HttpGet]
        [Route("GetAllRoster/{userName}/{password}")]
        public IHttpActionResult GetRoster(string userName, string password)
        {
            try
            {
                var data = (from e in db.Employees
                            join er in db.EmployeeRosters on e.EmployeeID equals er.employeeId
                            where e.Email == userName && e.Password == password
                            select new EmployeeRosterList
                            {
                                RosterID = er.RosterID,
                                FromDate = er.FromDate,
                                ToDate = er.ToDate,
                                InTime = er.InTime,
                                OutTime = er.OutTime,
                            }).ToList();
                return Ok(data);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.InternalServerError, "Sql Server not connected");
            }
        }

        [HttpPost]
        [Route("AddRoster/{userName}/{password}")]
        public IHttpActionResult AddRoster(string userName, string password,EmployeeRoster employeeRoster)
        {
            try
            {
                var employee = db.Employees.FirstOrDefault(e => e.Email == userName && e.Password == password);
                if(employee == null)
                {
                    return Content(HttpStatusCode.NotFound, "Invalid Username or Password");
                }
                EmployeeRoster er = new EmployeeRoster();
                er.employeeId = employee.EmployeeID;
                er.FromDate = employeeRoster.FromDate;
                er.ToDate = employeeRoster.ToDate;
                er.InTime = employeeRoster.InTime;
                er.OutTime = employeeRoster.OutTime;

                db.EmployeeRosters.Add(er);
                db.SaveChanges();
                return Content(HttpStatusCode.OK, "Roster Saved Successfully");
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.InternalServerError, "Sql Server not connected");
            }
        }
        // GET: api/EmployeeRoster/5
        [ResponseType(typeof(EmployeeRoster))]
        public IHttpActionResult GetEmployeeRoster(int id)
        {
            EmployeeRoster employeeRoster = db.EmployeeRosters.Find(id);
            if (employeeRoster == null)
            {
                return NotFound();
            }

            return Ok(employeeRoster);
        }

        // PUT: api/EmployeeRoster/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEmployeeRoster(int id, EmployeeRoster employeeRoster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employeeRoster.RosterID)
            {
                return BadRequest();
            }

            db.Entry(employeeRoster).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeRosterExists(id))
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

        // POST: api/EmployeeRoster
        [ResponseType(typeof(EmployeeRoster))]
        public IHttpActionResult PostEmployeeRoster(EmployeeRoster employeeRoster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EmployeeRosters.Add(employeeRoster);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = employeeRoster.RosterID }, employeeRoster);
        }

        // DELETE: api/EmployeeRoster/5
        [ResponseType(typeof(EmployeeRoster))]
        public IHttpActionResult DeleteEmployeeRoster(int id)
        {
            EmployeeRoster employeeRoster = db.EmployeeRosters.Find(id);
            if (employeeRoster == null)
            {
                return NotFound();
            }

            db.EmployeeRosters.Remove(employeeRoster);
            db.SaveChanges();

            return Ok(employeeRoster);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmployeeRosterExists(int id)
        {
            return db.EmployeeRosters.Count(e => e.RosterID == id) > 0;
        }
    }
}