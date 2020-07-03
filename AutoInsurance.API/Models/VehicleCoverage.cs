using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoInsurance.API.Models
{
    public class VehicleCoverage
    {
        public int Id { get; set; }

        [Display(Name = "Vehicle")]
        public int VehicleId { get; set; }

        [Display(Name = "Coverage")]
        public int CoverageId { get; set; }
        
        [ForeignKey("VehicleId")]
        public Vehicle Vehicle { get; set; }

        [ForeignKey("CoverageId")]
        public Coverage Coverage { get; set; }
        public bool isActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}