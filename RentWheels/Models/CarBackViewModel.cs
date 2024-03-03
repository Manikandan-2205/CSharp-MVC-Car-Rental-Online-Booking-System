using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RentWheels.Models
{
    public class CarBackViewModel
    {
        [Required(ErrorMessage = "Car number is required")]
        [Display(Name = "Car Number")]
        public string CarNo { get; set; }

        [Required(ErrorMessage = "Return date is required")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Return Date")]
        public DateTime ReturnDate { get; set; }

        [Required(ErrorMessage = "Expired days is required")]
        [Display(Name = "Expired Days")]
        public int ExpiredDays { get; set; }

        [Required(ErrorMessage = "Total amount is required")]
        [Display(Name = "Total Amount")]
        public int TotalAmount { get; set; }

        public int Fees { get; set; }
    }
}