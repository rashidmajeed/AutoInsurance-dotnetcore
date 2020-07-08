using System;
using AutoInsurance.API.Models;

namespace AutoInsurance.API.DTOs
{
    public class PaymentDTO
    {
        public int Id { get; set; }
        public int ClaimId { get; set; }
        public int CustomerId { get; set; }
        public string AccountNumber { get; set; }
        public Claim Claim { get; set; }
        public CustomerDTO Customer { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}