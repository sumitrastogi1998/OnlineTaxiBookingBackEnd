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
    [RoutePrefix("api/Employee")]
    public class EmployeeController : ApiController
    {
        private taxi_booking_systemEntities db = new taxi_booking_systemEntities();
        //private taxi_booking_systemEntities1 dbTaxi = new taxi_booking_systemEntities1();

        [Route("SearchEmployees/{SourceCity}")]
        [ResponseType(typeof(Employee))]
        [HttpGet]
        public IHttpActionResult GetEmployee(string SourceCity)
        {
            try
            {
                var data = (from e in db.Employees
                            join t in db.Taxis on e.EmployeeID equals t.TaxiOwnerId
                            where e.City == SourceCity
                            select new EmployeeTaxi
                            {
                                EmployeeID = e.EmployeeID,
                                EmployeeName = e.EmployeeName,
                                MobileNumber = e.MobileNumber,
                                City = e.City,
                                Email = e.Email,
                                LicenseNumber = e.LicenseNumber,
                                Password = e.Password,
                                Available = e.Available,
                                TaxiID = t.TaxiID,
                                TaxiNumber = t.TaxiNumber,
                                TaxiModel = t.TaxiModel,
                                TaxiOwnerId = t.TaxiOwnerId
                            }).ToList();

                if (data == null)
                {
                    return Content(HttpStatusCode.NotFound, "No records found");
                }
                //employee.Password = NewPassword;
                //db.SaveChanges();
                return Ok(data);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.InternalServerError, "Sql server not connected");
            }
        }

        [Route("Change/{EmployeeEmail}/{EmployeePassword}/{NewPassword}")]
        [ResponseType(typeof(Employee))]
        [HttpGet]
        public IHttpActionResult GetEmployee(string EmployeeEmail, string EmployeePassword, string NewPassword)
        {
            try
            {
                Employee employee = db.Employees.FirstOrDefault(s => s.Email == EmployeeEmail && s.Password == EmployeePassword);
                if (employee == null)
                {
                    return Content(HttpStatusCode.NotFound, "Invalid userName or password");
                }
                employee.Password = NewPassword;
                db.SaveChanges();
                return Ok(employee);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.InternalServerError, "Sql server not connected");
            }
        }



        // GET: api/Employee
        [Route("GetAll")]
        public IQueryable<Employee> GetEmployees()
        {
            return db.Employees;
        }

        [Route("UpdateEmployee/{Availability}/{userName}/{pass}")]
        [ResponseType(typeof(Employee))]
        [HttpGet]
        public IHttpActionResult UpdateEmployee(string Availability, string userName, string pass)
        {

            try
            { 
            Employee employee = db.Employees.First(e => e.Email == userName && e.Password == pass);
            if (employee == null)
            {
                return Content(HttpStatusCode.NotFound, "Invalid userName or password");
            }
            if (Availability == "Available")
            {
                employee.Available = true;
            }
            else
            {
                employee.Available = false;
            }
            db.SaveChanges();
            return Ok(employee);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.InternalServerError, "Sql server not connected");
            }
        }

        // GET: api/Employee/5
        [Route("updateEmployee/{id:int}")]
        [ResponseType(typeof(Employee))]
        [HttpGet]
        public IHttpActionResult GetEmployee(int id)
        {
            try
            {
                Employee employee = db.Employees.Find(id);
                if (employee == null)
                {
                    return Content(HttpStatusCode.NotFound, "Employee Not found");
                }
                employee.Available = false;
                db.SaveChanges();
                return Ok(employee);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.InternalServerError, "Sql server not connected");
            }
        }

        // PUT: api/Employee/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEmployee(int id, Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employee.EmployeeID)
            {
                return BadRequest();
            }

            db.Entry(employee).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
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

        // POST: api/Employee
        [ResponseType(typeof(Employee))]
        public IHttpActionResult PostEmployee(Employee employee)
        {
            try
            {
                Employee checkValidEmployee = db.Employees.Where(e => e.Email == employee.Email).FirstOrDefault();
                if (checkValidEmployee == null)
                {
                    db.Employees.Add(employee);
                    db.SaveChanges();

                    return CreatedAtRoute("DefaultApi", new { id = employee.EmployeeID }, employee);
                }
                else
                {
                    return Content(HttpStatusCode.NotAcceptable, "Email already registered..");
                }
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.InternalServerError, "Sql server not connected");
            }
        }

        // DELETE: api/Employee/5
        [ResponseType(typeof(Employee))]
        public IHttpActionResult DeleteEmployee(int id)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }

            db.Employees.Remove(employee);
            db.SaveChanges();

            return Ok(employee);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmployeeExists(int id)
        {
            return db.Employees.Count(e => e.EmployeeID == id) > 0;
        }
    }
}