namespace Domain.Common.Models
{
    public class AccessToken
    {
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        public string? UserName { get; set; }
        public DateTime Expiration { get; set; }
    }
}