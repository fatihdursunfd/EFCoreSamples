using Domain.Common.Models;
using Domain.Entities.Identity;
using System.Security.Claims;

namespace Application.Interfaces.Data
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(ApplicationUser user, List<Claim> claims);
    }
}