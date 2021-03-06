using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AutoInsurance.API.Models
{
    public class Coverage
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        public string Description { get; set; }
        public bool isActiveCoverage { get; set; }
        public IList<PolicyCoverage> PolicyCoverage { get; set; }
        public IList<VehicleCoverage> VehicleCoverage { get; set; }


    }
}