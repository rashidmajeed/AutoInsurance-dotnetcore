using System;
using System.Collections.Generic;
using AutoInsurance.API.Models;

namespace AutoInsurance.API.DTOs
{
    public class VehicleCreationDTO
    {
        public int PolicyId { get; set; }
        public int Year { get; set; }
        public string Make { get; set; }
        public int Model { get; set; }
        public string NumberPlate { get; set; }
        public string RegisteredState { get; set; }
        public DateTime CreatedDate { get; set; }
        public IList<int> CoverageIds { get; set; }
    }
}