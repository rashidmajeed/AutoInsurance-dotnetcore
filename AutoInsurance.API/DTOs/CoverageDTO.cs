namespace AutoInsurance.API.DTOs
{
    public class CoverageDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool isActiveCoverage { get; set; }
    }
}