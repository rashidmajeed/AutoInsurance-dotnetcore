namespace AutoInsurance.API.DTOs
{
    public class CoverageCreationDTO
    {
        public string Name { get; set; }
        public int Description { get; set; }
        public bool isActiveCoverage { get; set; }
    }
}