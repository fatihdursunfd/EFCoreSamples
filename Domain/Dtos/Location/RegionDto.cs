namespace Domain.Dtos.Location
{
    public class RegionDto
    {
        public string Region { get; set; } = string.Empty;
        public List<SubRegionDto> SubRegions { get; set; } = new();
    }

    public class SubRegionDto
    {
        public string SubRegion { get; set; } = string.Empty;

        public List<CountryDto> Countries { get; set; } = new();
    }
}