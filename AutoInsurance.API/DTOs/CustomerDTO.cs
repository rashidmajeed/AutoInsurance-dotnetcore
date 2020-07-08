using System;
using System.ComponentModel.DataAnnotations;

namespace AutoInsurance.API.DTOs
{
    public class CustomerDTO
    {
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        public string Image { get; set; }
        public string Bio { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string phone { get; set; }
        public string Address { get; set; }
        public int LicenseNumber { get; set; }
        
    }
}