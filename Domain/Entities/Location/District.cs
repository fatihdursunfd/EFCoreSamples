namespace Domain.Entities
{
    public class District
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public string? Latitude { get; set; }
        public string? Longitude { get; set; }

        public int ProvienceId { get; set; }
        public Provience Provience { get; set; } = null!;
    }
}