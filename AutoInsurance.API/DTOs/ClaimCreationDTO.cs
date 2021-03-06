using System;

namespace AutoInsurance.API.DTOs
{
    public class ClaimCreationDTO
    {
        public int Id { get; set; }
        public int PolicyId { get; set; }
        public int VehicleId { get; set; }
        public string Approval { get; set; }
        public PolicyDTO Policy { get; set; }
        public VehicleDTO Vehicle { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}