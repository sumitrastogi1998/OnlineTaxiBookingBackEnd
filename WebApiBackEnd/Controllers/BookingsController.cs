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
    [RoutePrefix("api/Booking")]
    public class BookingController : ApiController
    {
        private taxi_booking_systemEntities db = new taxi_booking_systemEntities();


        [Route("GetAllBookings")]
        public IQueryable<Booking> GetAllBookings()
        {
            return db.Bookings;
        }
        // GET: api/Booking
        [Route("GetAllFeedbacks")]
        public IQueryable<Booking> GetBookings()
        {
            return db.Bookings.Where(b=>b.FeedBack != null);
        }



        [Route("SendFeedback/{id}/{feedback}")]
        public IHttpActionResult GetBooking(int id,string feedback)
        {
            try
            {
                Booking booking = db.Bookings.FirstOrDefault(e => e.customerId == id);
                if (booking == null)
                {
                    return Content(HttpStatusCode.NotFound, "Customer Id can't be found");
                }

                booking.FeedBack = feedback;
                db.SaveChanges();
                return Ok(booking);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.InternalServerError, "Sql Server Error");
            }
        }

        [Route("GetBookings/{userName}/{pass}")]
        public IHttpActionResult GetBooking(string userName, string pass)
        {
            try
            {
                Employee emp = db.Employees.FirstOrDefault(e => e.Email == userName && e.Password == pass);

                if(emp == null)
                {
                    return Content(HttpStatusCode.NotFound, "Invalid Username or password");
                }
                var data = (from c in db.Customers
                            join b in db.Bookings on c.CustomerID equals b.customerId
                            where b.employeeId == emp.EmployeeID
                            select new CustomerBooking
                            {
                                CustomerID = c.CustomerID,
                                CustomerPassword = c.CustomerPassword,
                                CustomerName = c.CustomerName,
                                MobileNumber = c.MobileNumber,
                                Email = c.Email,
                                BookingID = b.BookingID,
                                FromDate = b.FromDate,
                                ToDate = b.ToDate,
                                BookTime = b.BookTime,
                                DropTime = b.DropTime,
                                PickupPoint = b.PickupPoint,
                                DropPoint = b.DropPoint,
                                Fare = b.Fare,
                                employeeId = b.employeeId,
                                customerId = b.customerId
                            }).ToList();
                return Ok(data);
            }
            catch(Exception)
            {
                return Content(HttpStatusCode.InternalServerError, "Sql Server not connected");
            }
        }

        [Route("GetCustomerBookings/{userName}/{pass}")]
        public IHttpActionResult GetCustomerBooking(string userName, string pass)
        {
            try
            {
                Customer customer = db.Customers.FirstOrDefault(c => c.Email == userName && c.CustomerPassword == pass);
                if(customer == null)
                {
                    return Content(HttpStatusCode.NotFound, "Invalid Username or password");
                }
                var data = (from c in db.Customers
                            join b in db.Bookings on c.CustomerID equals b.customerId
                            where b.customerId == customer.CustomerID
                            select new CustomerBooking
                            {
                                CustomerID = c.CustomerID,
                                CustomerPassword = c.CustomerPassword,
                                CustomerName = c.CustomerName,
                                MobileNumber = c.MobileNumber,
                                Email = c.Email,
                                BookingID = b.BookingID,
                                FromDate = b.FromDate,
                                ToDate = b.ToDate,
                                BookTime = b.BookTime,
                                DropTime = b.DropTime,
                                PickupPoint = b.PickupPoint,
                                DropPoint = b.DropPoint,
                                FeedBack = b.FeedBack,
                                Fare = b.Fare,
                                employeeId = b.employeeId,
                                customerId = b.customerId
                            }).ToList();
                return Ok(data);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.InternalServerError, "Sql Server not connected");
            }
            
        }

        // GET: api/Booking/5
        [ResponseType(typeof(Booking))]
        public IHttpActionResult GetBooking(int id)
        {
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return NotFound();
            }

            return Ok(booking);
        }

        // PUT: api/Booking/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBooking(int id, Booking booking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != booking.BookingID)
            {
                return BadRequest();
            }

            db.Entry(booking).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingExists(id))
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

        // POST: api/Booking
        [ResponseType(typeof(Booking))]
        public IHttpActionResult PostBooking(Booking booking)
        {
            try
            {
                db.Bookings.Add(booking);
                db.SaveChanges();

                return CreatedAtRoute("DefaultApi", new { id = booking.BookingID }, booking);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.InternalServerError, "Sql server not connected");
            }
        }

        // DELETE: api/Booking/5
        [HttpDelete]
        [Route("CancelBooking")]
        [ResponseType(typeof(Booking))]
        public IHttpActionResult DeleteBooking(int id)
        {
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return NotFound();
            }
            db.Bookings.Remove(booking);
            db.SaveChanges();

            return Ok(booking);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BookingExists(int id)
        {
            return db.Bookings.Count(e => e.BookingID == id) > 0;
        }
    }
}