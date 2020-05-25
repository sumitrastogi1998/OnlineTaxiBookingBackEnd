using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiBackEnd.Models
{
    public class EmployeeRosterList
    {
        public int RosterID { get; set; }
        public Nullable<System.DateTime> FromDate { get; set; }
        public Nullable<System.DateTime> ToDate { get; set; }
        public Nullable<System.TimeSpan> InTime { get; set; }
        public Nullable<System.TimeSpan> OutTime { get; set; }
        public Nullable<int> employeeId { get; set; }
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string MobileNumber { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public int LicenseNumber { get; set; }
        public string Password { get; set; }
        public Nullable<bool> Available { get; set; }
    }
}