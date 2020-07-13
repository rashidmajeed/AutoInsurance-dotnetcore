  
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoInsurance.API.DTOs
{
    public class VehicleDetailsDTO: VehicleDTO
    {
        public List<CoverageDTO> Coverage { get; set; }
    }
}