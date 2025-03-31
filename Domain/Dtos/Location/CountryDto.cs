namespace Domain.Dtos.Location
{
    public class CountryDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Capital { get; set; }
        public string? Currency { get; set; }
        public string? Nationality { get; set; }
        public List<StateDto>? States { get; set; }
        public List<ProvienceDto>? Proviences { get; set; }
    }
}