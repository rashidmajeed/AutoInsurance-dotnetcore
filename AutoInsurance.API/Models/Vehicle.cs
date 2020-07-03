using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AutoInsurance.API.Models
{
    public class Vehicle
    {
        [Key]
        public int Id { get; set; }
        public int PolicyId { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        [StringLength(40)]
        public string Make { get; set; }
        [Required]
        public int Model { get; set; }
        public string NumberPlate { get; set; }
        [Required]
        [StringLength(60)]
        public string RegisteredState { get; set; }
        public DateTime CreatedDate { get; set; }
        public IList<VehicleCoverage> VehicleCoverages { get; set; }

    }
}