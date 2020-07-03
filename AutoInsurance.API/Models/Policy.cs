using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoInsurance.API.Models
{
    public class Policy
    {
        [Key]
        public int Id { get; set; }
        public int Customer_Id { get; set; }
        [Required]
        [StringLength(40)]
        public string PolicyNumber { get; set; }
        public string Description { get; set; }
        [Required]
        public string PolicyPlan { get; set; }
        
        [Required]
        [Column(TypeName = "decimal(18,4)")]
        public decimal Amount { get; set; }
        public DateTime CreatedDate { get; set; }
        public IList<PolicyCoverage> PolicyCoverages { get; set; }

    }
}