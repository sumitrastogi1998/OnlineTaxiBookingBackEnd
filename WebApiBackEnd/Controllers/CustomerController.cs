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
    [RoutePrefix("api/Customer")]
    public class CustomerController : ApiController
    {
        private taxi_booking_systemEntities db = new taxi_booking_systemEntities();

        [Route("GetCustomer/{userName}/{Password}")]
        public IHttpActionResult GetCustomer(string userName, string Password)
        {
            try
            {
                Customer customer = db.Customers.FirstOrDefault(c => c.Email == userName && c.CustomerPassword == Password);
                if (customer == null)
                {
                    return Content(HttpStatusCode.NotFound, "Invalid UserName or Password");
                    //return NotFound();
                }
                return Ok(customer);
            }catch(Exception)
            {
                return Content(HttpStatusCode.InternalServerError, "Sql Server Not connected");
            }
        }

        [Route("Change/{CustomerEmail}/{CustomerPassword}/{NewPassword}")]
        [ResponseType(typeof(Customer))]
        [HttpGet]
        public IHttpActionResult GetCustomer(string CustomerEmail, string CustomerPassword, string NewPassword)
        {
            try
            {
                Customer customer = db.Customers.First(s => s.Email == CustomerEmail && s.CustomerPassword == CustomerPassword);
                if (customer == null)
                {
                    return Content(HttpStatusCode.NotFound, "Invalid UserName or Password");
                }
                customer.CustomerPassword = NewPassword;
                db.SaveChanges();
                return Ok(customer);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.InternalServerError, "Sql Server not connected");
            }
        }
        // GET: api/Customer
        [Route("GetCustomerReport")]
        public IQueryable<Customer> GetCustomers()
        {
                return db.Customers;
        }

        // GET: api/Customer/5
        [ResponseType(typeof(Customer))]
        public IHttpActionResult GetCustomer(int id)
        {
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        // PUT: api/Customer/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCustomer(int id, Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customer.CustomerID)
            {
                return BadRequest();
            }

            db.Entry(customer).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
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

        // POST: api/Customer
        [ResponseType(typeof(Customer))]
        public IHttpActionResult PostCustomer(Customer customer)
        {
            try
            {
                Customer checkValidCustomer = db.Customers.Where(c => c.Email == customer.Email).FirstOrDefault();
                if (checkValidCustomer == null)
                {
                    db.Customers.Add(customer);
                    db.SaveChanges();
                    return CreatedAtRoute("DefaultApi", new { id = customer.CustomerID }, customer);
                }
                else
                {
                    return Content(HttpStatusCode.NotAcceptable, "Email already registered..");
                }
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.InternalServerError, "Sql Server not connected");
            }
        }

        // DELETE: api/Customer/5
        [ResponseType(typeof(Customer))]
        public IHttpActionResult DeleteCustomer(int id)
        {
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }

            db.Customers.Remove(customer);
            db.SaveChanges();

            return Ok(customer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CustomerExists(int id)
        {
            return db.Customers.Count(e => e.CustomerID == id) > 0;
        }
    }
}