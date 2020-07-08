using System;

namespace AutoInsurance.API.DTOs
{
    public class ClaimDTO
    {
        public int Id { get; set; }
        public int PolicyId { get; set; }
        public int VehicleId { get; set; }
        public int CoverageId { get; set; }
        public string Approval { get; set; }
        public PolicyDTO Policy { get; set; }
        public CoverageDTO Coverage { get; set; }
        public VehicleDTO Vehicle { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}