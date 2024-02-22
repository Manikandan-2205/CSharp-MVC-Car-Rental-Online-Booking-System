//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RentWheels.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CarDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CarDetail()
        {
            this.CarRentals = new HashSet<CarRental>();
            this.CarReturns = new HashSet<CarReturn>();
        }
    
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
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CarRental> CarRentals { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CarReturn> CarReturns { get; set; }
    }
}