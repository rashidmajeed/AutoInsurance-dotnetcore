using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoInsurance.API.Models
{
    public class PolicyCoverage
    {
        public int Id { get; set; }

         [Display(Name = "Policy")]
        public int PolicyId { get; set; }

         [Display(Name = "Coverage")]
        public int CoverageId { get; set; }

        [ForeignKey("PolicyId")]
        public Policy Policy { get; set; }

        [ForeignKey("CoverageId")]
        public Coverage Coverage { get; set; }
        public bool isActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}