using Application.Common.Helpers;
using Application.Interfaces.Data;
using Domain.Common.Models;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TokenOptions = Domain.Common.Models.TokenOptions;

namespace Infrastructure.Utilities
{
    public class TokenHelper : ITokenHelper
    {
        private IConfiguration _configuration { get; }

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly TokenOptions _tokenOptions;
        private readonly DateTime _accessTokenExpiration;

        public TokenHelper(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;

            _tokenOptions = new()
            {
                Audience = _configuration.GetSection("TokenOptions:Audience").Value,
                Issuer = _configuration.GetSection("TokenOptions:Issuer").Value,
                AccessTokenExpiration = Convert.ToInt32(_configuration.GetSection("TokenOptions:AccessTokenExpiration").Value),
                SecurityKey = _configuration.GetSection("TokenOptions:SecurityKey").Value
            };

            _accessTokenExpiration = DateTime.Now.AddYears(_tokenOptions.AccessTokenExpiration);

        }

        public AccessToken CreateToken(ApplicationUser user, List<Claim> claims)
        {
            var securityKey = Application.Common.Helpers.SecurityHelper.CreateSecurityKey(_tokenOptions.SecurityKey ?? string.Empty);
            var signingCredentials = CreateSigningCredentials(securityKey);
            var jwt = CreateJwtSecurityToken(_tokenOptions, signingCredentials, claims);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);
            var refreshToken = _userManager.GenerateUserTokenAsync(user, nameof(RefreshTokenProvider<ApplicationUser>), RefreshTokenProvider<ApplicationUser>.Purpose).Result;

            return new AccessToken
            {
                Token = token,
                Expiration = _accessTokenExpiration,
                RefreshToken = refreshToken,
                UserName = user.UserName
            };
        }

        private SigningCredentials CreateSigningCredentials(SecurityKey securityKey)
        {
            return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
        }
        private JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, SigningCredentials signingCredentials, List<Claim> claims)
        {
            return new JwtSecurityToken(tokenOptions.Issuer, tokenOptions.Audience, claims, DateTime.Now, _accessTokenExpiration, signingCredentials);
        }
    }
}