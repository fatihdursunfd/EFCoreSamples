using Application.Interfaces.Data;
using Domain.Constants;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Infrastructure.Utilities
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid UserId
        {
            get
            {
                string? userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(TokenConst.UserId);

                if (string.IsNullOrEmpty(userId))
                    return Guid.Empty;

                if (!Guid.TryParse(userId, out Guid _))
                    return Guid.Empty;

                return Guid.Parse(userId);
            }
        }

        public string? Lang => _httpContextAccessor?.HttpContext?.Request.Headers[TokenConst.Lang].FirstOrDefault();

        public string? Role => _httpContextAccessor?.HttpContext?.User?.FindFirstValue(TokenConst.Role);

        public string? Email => _httpContextAccessor?.HttpContext?.User.FindFirstValue(TokenConst.Email);

    }
}