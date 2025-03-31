using Domain.Dtos.Location;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces
{
    public interface IAppDbContext
    {
        #region DbSet

        DbSet<Country> Countries { get; set; }
        DbSet<State> States { get; set; }
        DbSet<Provience> Provinces { get; set; }
        DbSet<District> Districts { get; set; }

        #endregion

        #region Compiled Queries

        Task<CountryDto?> GetCountryByIdAsync(int id, Language language);
        Task<List<CountryDto>> GetAllCountriesAsync(Language language);

        #endregion

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    }
}