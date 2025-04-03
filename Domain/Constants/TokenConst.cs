using System.Security.Claims;

namespace Domain.Constants
{
    public static class TokenConst
    {
        public static string UserId = ClaimTypes.NameIdentifier;
        public static string Lang = "Lang";
        public static string Role = ClaimTypes.Role;
        public static string Email = ClaimTypes.Email;
        public static string TenantId = "TenantId";
    }
}