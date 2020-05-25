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
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {
        private taxi_booking_systemEntities db = new taxi_booking_systemEntities();

        [Route("GetAllUsers")]
        // GET: api/User
        public IQueryable<User> GetUsers()
        {
            return db.Users;
        }

        [Route("Login/{UserName}/{Password}/{Role}")]
        [ResponseType(typeof(User))]
        [HttpGet]
        public IHttpActionResult GetUser(string UserName, string Password, string Role)
        {
            try
            {
                User user = db.Users.FirstOrDefault(s => s.UserName == UserName && s.UserPassword == Password && s.UserRole == Role);
                if (user == null)
                {
                    return Content(HttpStatusCode.NotFound,"Invalid UserName or Password");
                }
                //employee.Password = NewPassword;
                //db.SaveChanges();
                return Ok(user);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.InternalServerError, "Sql server Not Connected");
            }
        }
        // GET: api/User/5
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/User/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUser(int id, User user)
        {
            db.Entry(user).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                return Content(HttpStatusCode.InternalServerError, "Sql Server not connected");
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/User
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(User user)
        {
            try
            {
                User checkForNewUser = db.Users.Where(u => u.UserName == user.UserName).FirstOrDefault();
                if (checkForNewUser == null)
                {
                    db.Users.Add(user);
                    db.SaveChanges();

                    return CreatedAtRoute("DefaultApi", new { id = user.UserID }, user);
                }
                else
                {
                    return Content(HttpStatusCode.NotFound, "UserName already  registered");
                }
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.InternalServerError, "Can't reach to the servers");
            }
        }

        // DELETE: api/User/5
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            db.SaveChanges();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.UserID == id) > 0;
        }
    }
}