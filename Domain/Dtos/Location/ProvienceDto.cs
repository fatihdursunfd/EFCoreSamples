namespace Domain.Dtos.Location
{
    public class ProvienceDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<DistrictDto>? Districts { get; set; }
    }
}