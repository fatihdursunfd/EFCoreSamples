using Domain.Dtos.Location;
using Domain.Entities;
using Domain.Entities.Identity;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces.Data
{
    public interface IAppDbContext
    {
        #region DbSet
        
        DbSet<ApplicationUser> ApplicationUsers { get; set; }
        DbSet<ApplicationRole> ApplicationRoles { get; set; }
        DbSet<ApplicationUserRole> ApplicationUserRoles { get; set; }
        DbSet<ApplicationRoleClaim> ApplicationRoleClaims { get; set; }
        DbSet<ApplicationUserClaim> ApplicationUserClaims { get; set; }
        DbSet<ApplicationUserLogin> ApplicationUserLogins { get; set; }
        DbSet<ApplicationUserToken> ApplicationUserTokens { get; set; }


        DbSet<Country> Countries { get; set; }
        DbSet<State> States { get; set; }
        DbSet<Provience> Provinces { get; set; }
        DbSet<District> Districts { get; set; }

        DbSet<Company> Companies { get; set; }

        #endregion

        #region Compiled Queries

        Task<CountryDto?> GetCountryByIdAsync(int id, Language language);
        Task<List<CountryDto>> GetAllCountriesAsync(Language language);

        #endregion

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    }
}