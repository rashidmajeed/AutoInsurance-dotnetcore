using System;
using System.Collections.Generic;
using AutoInsurance.API.Helper;
using AutoInsurance.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace AutoInsurance.API.DTOs
{
    public class PolicyCreationDTO
    {
        public int CustomerId { get; set; }
        public string PolicyNumber { get; set; }
        public string Description { get; set; }
        public string PolicyPlan { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedDate { get; set; }
        public CustomerDTO Customer { get; set; }

        [ModelBinder(BinderType = typeof(TypeBinder<List<int>>))]
        public IList<int> CoverageIds { get; set; }
    }
}