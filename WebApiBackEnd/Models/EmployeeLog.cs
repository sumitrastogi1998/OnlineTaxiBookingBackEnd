using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiBackEnd.Models
{
    public class EmployeeLog
    {
        public int custId { get; set; }
        public string custName { get; set; }
        public string source { get; set; }
        public string destination { get; set; }
        public System.DateTime fromdate { get; set; }
        public System.DateTime todate { get; set; }
        public decimal totalfare { get; set; }
        public int rating { get; set; }
        public int eId { get; set; }
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