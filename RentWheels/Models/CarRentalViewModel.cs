using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentWheels.Models
{
    public class CarRentalViewModel
    {
        public int Id { get; set; }
        public string PhoneNo { get; set; }
        public string CarNo { get; set; }
        public int Fees { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public int TotalAmount { get; set; }
        public string CustomerName { get; set; }        
    }
}