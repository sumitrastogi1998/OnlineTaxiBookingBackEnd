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
    [RoutePrefix("api/log")]
    public class LogController : ApiController
    {
        private taxi_booking_systemEntities db = new taxi_booking_systemEntities();

        // GET: api/logs
        [Route("DisplayLogs/{userName}/{password}")]
        [HttpGet]
        public IHttpActionResult GetLogs(string userName, string password)
        {
            try
            {
                var data = (from e in db.Employees
                            join l in db.logs on e.EmployeeID equals l.eId
                            where e.Email == userName && e.Password == password
                            select new EmployeeLog
                            {
                                EmployeeID = e.EmployeeID,
                                EmployeeName = e.EmployeeName,
                                custId = l.custId,
                                custName = l.custName,
                                source = l.source,
                                destination = l.destination,
                                fromdate = l.fromdate,
                                todate = l.todate,
                                totalfare = l.totalfare,
                                rating = l.rating,
                                eId = (int)l.eId
                            }).ToList();
                return Ok(data);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.InternalServerError, "Sql server not connected");
            }
        }
        [HttpGet]
        [Route("AllEmployeeDetails")]
        public IQueryable<log> GetEmaployee()
        {
            try
            {
                return db.logs;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("GetEmployeeDetailsById/{employeeId}")]
        public IHttpActionResult GetEmaployeeById(int employeeId)
        {
            log objEmp = new log();
            //int ID = Convert.ToInt32(employeeId);
            try
            {
                objEmp = db.logs.Find(employeeId);
                if (objEmp == null)
                {
                    return Content(HttpStatusCode.NotFound, "Employee with employeeId not found");
                }
                return Ok(objEmp);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.InternalServerError, "Sql server not connected");
            }

            
        }

        [HttpPost]
        [Route("InsertEmployeeDetails")]
        public IHttpActionResult PostEmaployee(log data)
        {
            try
            {
                db.logs.Add(data);
                db.SaveChanges();
                return Ok(data);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.InternalServerError, "Sql server not connected");
            }
            
        }

        [HttpPut]
        [Route("UpdateEmployeeDetails")]
        public IHttpActionResult PutEmaployeeMaster(log employee)
        {
            try
            {
                log objEmp = new log();
                objEmp = db.logs.Find(employee.custId);
                if(objEmp == null)
                {
                    return Content(HttpStatusCode.NotFound, "No logs available");
                }
                    objEmp.custName = employee.custName;
                    objEmp.source = employee.source;
                    objEmp.destination = employee.destination;
                    objEmp.fromdate = employee.fromdate;
                    objEmp.todate = employee.todate;
                    objEmp.totalfare = employee.totalfare;
                    objEmp.rating = employee.rating;
                
                int i = this.db.SaveChanges();
                return Ok(employee);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.InternalServerError, "Sql server not connected");
            }
            
        }

        [HttpDelete]
        [Route("DeleteEmployeeDetails")]
        public IHttpActionResult DeleteEmaployeeDelete(int id)
        {
            try
            {
                //int empId = Convert.ToInt32(id);  
                log emaployee = db.logs.Find(id);
                if (emaployee == null)
                {
                    return Content(HttpStatusCode.NotFound, "Record not deleted");
                }

                db.logs.Remove(emaployee);
                db.SaveChanges();

                return Ok(emaployee);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.InternalServerError, "Sql server not connected");
            }
        }

    }
}