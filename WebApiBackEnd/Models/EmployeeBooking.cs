using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiBackEnd.Models
{
    public class EmployeeBooking
    {
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string MobileNumber { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public int LicenseNumber { get; set; }
        public string Password { get; set; }
        public Nullable<bool> Available { get; set; }
        public int BookingID { get; set; }
        public System.DateTime FromDate { get; set; }
        public System.DateTime ToDate { get; set; }
        public System.TimeSpan BookTime { get; set; }
        public System.TimeSpan DropTime { get; set; }
        public string PickupPoint { get; set; }
        public string DropPoint { get; set; }
        public string FeedBack { get; set; }
        public int Fare { get; set; }
        public int employeeId { get; set; }
        public int customerId { get; set; }
    }
}