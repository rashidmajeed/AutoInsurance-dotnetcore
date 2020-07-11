namespace AutoInsurance.API.DTOs
{
    public class CoverageCreationDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool isActiveCoverage { get; set; }
    }
}