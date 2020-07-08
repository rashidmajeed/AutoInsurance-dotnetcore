using System;


namespace AutoInsurance.API.DTOs
{
    public class PaymentCreationDTO
    {
        public int ClaimId { get; set; }
        public int CustomerId { get; set; }
        public string AccountNumber { get; set; }
        public ClaimDTO Claim { get; set; }
        public CustomerDTO Customer { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}