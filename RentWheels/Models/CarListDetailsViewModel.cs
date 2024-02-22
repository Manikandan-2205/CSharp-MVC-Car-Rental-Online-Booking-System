using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentWheels.Models
{
    public class CarListDetailsViewModel
    {
        public int Id { get; set; }
        public string CarNo { get; set; }
        public string ModelName { get; set; }
        public string BrandName { get; set; }
        public string Color { get; set; }
        public string RideSelection { get; set; }
        public int Fees { get; set; }
        public string FuelType { get; set; }
        public int Travel { get; set; }
        public string Available { get; set; }
        public string ImageUrl { get; set; }
    }
}