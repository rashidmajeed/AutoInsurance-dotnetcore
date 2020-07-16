using System;
using System.ComponentModel.DataAnnotations;

namespace AutoInsurance.API.DTOs
{
    public class UserInfo
    {
        [EmailAddress]
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public string Password { get; set; }
    }
}