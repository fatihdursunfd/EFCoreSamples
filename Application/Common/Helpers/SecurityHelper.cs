using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;

namespace Application.Common.Helpers
{
    public static class SecurityHelper
    {
        /// <summary>
        /// Token security key.
        /// </summary>
        public static SecurityKey CreateSecurityKey(string securityKey)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
        }

        private static string RandomCreateSalt()
        {
            byte[] randomBytes = new byte[128 / 8];
            using (RandomNumberGenerator generator = RandomNumberGenerator.Create())
            {
                generator.GetBytes(randomBytes);

                return Convert.ToBase64String(randomBytes);
            }
        }

        public static string CreateHash(string value)
        {
            var valueBytes = KeyDerivation.Pbkdf2(value, Encoding.UTF8.GetBytes(RandomCreateSalt()), KeyDerivationPrf.HMACSHA512, 10000, 256 / 8);

            return Convert.ToBase64String(valueBytes);
        }

    }
}