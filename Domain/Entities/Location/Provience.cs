namespace Domain.Entities
{
    public class Provience
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }

        public int? CountryId { get; set; }
        public Country? Country { get; set; }

        public int? StateId { get; set; }
        public State? State { get; set; }

        public List<District>? Districts { get; set; }
    }
}