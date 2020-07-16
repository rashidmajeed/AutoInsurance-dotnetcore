using System;

namespace AutoInsurance.API.DTOs
{
    public class CustClaimDTO
    {
        public int Id { get; set; }
        public int PolicyId { get; set; }
        public int VehicleId { get; set; }
        public string Approval { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}