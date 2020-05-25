using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiBackEnd.Models
{
    public class EmployeeTaxi
    {
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string MobileNumber { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public int LicenseNumber { get; set; }
        public string Password { get; set; }
        public Nullable<bool> Available { get; set; }
        public int TaxiID { get; set; }
        public string TaxiNumber { get; set; }
        public string TaxiModel { get; set; }
        public int TaxiOwnerId { get; set; }
    }
}