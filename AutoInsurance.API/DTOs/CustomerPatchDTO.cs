using System;
using System.ComponentModel.DataAnnotations;

namespace AutoInsurance.API.DTOs
{
    public class CustomerPatchDTO
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        public string Bio { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string phone { get; set; }
        public string Address { get; set; }
        public string LicenseNumber { get; set; }

    }
}