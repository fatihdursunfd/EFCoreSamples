namespace Domain.Entities
{
    public class State
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? StateCode { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }

        public int CountryId { get; set; }
        public Country Country { get; set; } = null!;

        public List<Provience>? Proviences { get; set; }
    }
}