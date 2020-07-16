using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoInsurance.API.Models
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Claim")]
        public int ClaimId { get; set; }

        [Display(Name = "Customer")]
        public int CustomerId { get; set; }
        [Required]
        [StringLength(200)]
        public string AccountNumber { get; set; }

        [ForeignKey("ClaimId")]
        public CustClaim CustClaim { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}