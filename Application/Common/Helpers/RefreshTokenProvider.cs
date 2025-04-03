using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Application.Common.Helpers
{
    public class RefreshTokenProvider<TUser> : DataProtectorTokenProvider<TUser> where TUser : class, new()
    {
        public const string Purpose = "refresh-token";

        public RefreshTokenProvider(IDataProtectionProvider dataProtectionProvider, IOptions<RefreshTokenProviderOptions> options)
            : base(dataProtectionProvider, options)
        {
        }
    }

    public class RefreshTokenProviderOptions : DataProtectionTokenProviderOptions
    {
    }
}