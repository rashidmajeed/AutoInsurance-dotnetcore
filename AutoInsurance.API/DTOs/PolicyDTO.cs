using System;
using System.Collections.Generic;
using AutoInsurance.API.Models;

namespace AutoInsurance.API.DTOs
{
    public class PolicyDTO
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string PolicyNumber { get; set; }
        public string Description { get; set; }
        public string PolicyPlan { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedDate { get; set; }
        public CustomerDTO Customer { get; set; }
        public IList<PolicyCoverage> PolicyCoverages { get; set; }
    }
}