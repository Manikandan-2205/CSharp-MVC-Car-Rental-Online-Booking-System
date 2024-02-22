using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RentWheels.Models;

namespace RentWheels.ViewModels
{
    public class RegisterViewModel
    {
        public RegisterDetail RegisterDetail { get; set; }
        public List<RoleDetail> RoleDetails { get; set; } // Ensure this property exists
        public int RoleId { get; set; } // Ensure this property exists
    }
}
