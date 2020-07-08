using System;
using System.Collections.Generic;
using AutoInsurance.API.Models;

namespace AutoInsurance.API.DTOs
{
    public class VehicleDTO
    {
        public int Id { get; set; }
        public int PolicyId { get; set; }
        public int Year { get; set; }
        public string Make { get; set; }
        public int Model { get; set; }
        public string NumberPlate { get; set; }
        public string RegisteredState { get; set; }
        public DateTime CreatedDate { get; set; }
        public IList<VehicleCoverage> VehicleCoverages { get; set; }
    }
}