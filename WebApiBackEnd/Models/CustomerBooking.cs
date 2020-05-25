using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiBackEnd.Models
{
    public class CustomerBooking
    {
        public int CustomerID { get; set; }
        public string CustomerPassword { get; set; }
        public string CustomerName { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public int BookingID { get; set; }
        public System.DateTime FromDate { get; set; }
        public System.DateTime ToDate { get; set; }
        public System.TimeSpan BookTime { get; set; }
        public System.TimeSpan DropTime { get; set; }
        public string PickupPoint { get; set; }
        public string DropPoint { get; set; }
        public string FeedBack { get; set; }
        public int? Fare { get; set; }
        public int? employeeId { get; set; }
        public int? customerId { get; set; }
    }
}