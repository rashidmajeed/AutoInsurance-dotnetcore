using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoInsurance.API.Models
{
    public class CustClaim
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Policy")]
        public int PolicyId { get; set; }

        [Display(Name = "Vehicle")]
        public int VehicleId { get; set; }

        [StringLength(200)]
        public string Approval { get; set; }

        [ForeignKey("PolicyId")]
        public Policy Policy { get; set; }

        [ForeignKey("VehicleId")]
        public Vehicle Vehicle { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}