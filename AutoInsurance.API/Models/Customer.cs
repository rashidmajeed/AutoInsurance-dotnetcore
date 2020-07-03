using System;
using System.ComponentModel.DataAnnotations;

namespace AutoInsurance.API.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        public string Image { get; set; }
        public string Bio { get; set; }
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "The email is required")]
        [EmailAddress(ErrorMessage = "The email is required")]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string phone { get; set; }
        public string Address { get; set; }

        [Required]
        [StringLength(200)]
        public int LicenseNumber { get; set; }

    }
}