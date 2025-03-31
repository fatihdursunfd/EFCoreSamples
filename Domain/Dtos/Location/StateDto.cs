namespace Domain.Dtos.Location
{
    public class StateDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? StateCode { get; set; }

        public List<ProvienceDto>? Proviences { get; set; }
    }
}