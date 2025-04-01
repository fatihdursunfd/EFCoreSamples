using Domain.Dtos.Location;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public static class CompiledQueries
    {
        /// <summary>
        /// Gönderilen id'ye ait ülke bilgisini çekmek için kullanılır.
        /// </summary>
        public static readonly Func<AppDbContext, int, Language, Task<CountryDto?>> GetCountryByIdQuery =
            EF.CompileAsyncQuery((AppDbContext _context, int id, Language language) =>
                _context.Countries
                    .AsNoTracking()
                    .Where(x => x.Id == id)
                    .Select(x => new CountryDto()
                    {
                        Id = x.Id,
                        Name = x.Translations != null 
                            ? (x.Translations.FirstOrDefault(y => y.Language == language)!.Name ?? x.Name) 
                            : x.Name,
                        Capital = x.Capital,
                        Currency = x.Currency,
                        Nationality = x.Nationality,
                        States = x.States != null ? x.States
                            .Select(y => new StateDto()
                            {
                                Id = y.Id,
                                Name = y.Name,
                                Proviences = y.Proviences != null ? y.Proviences
                                    .Select(y => new ProvienceDto()
                                    {
                                        Id = y.Id,
                                        Name = y.Name,
                                    })
                                    .ToList() : new()
                            })
                            .ToList() : new(),
                        Proviences = x.Proviences != null ? x.Proviences
                            .Select(y => new ProvienceDto()
                            {
                                Id = y.Id,
                                Name = y.Name,
                            })
                            .ToList() : new()
                    })
                    .FirstOrDefault());


        /// <summary>
        /// Bütün ülke bilgilerini çekmek için kullanılır.
        /// </summary>
        public static readonly Func<AppDbContext, Language, IAsyncEnumerable<CountryDto>> GetAllCountriesQuery =
            EF.CompileAsyncQuery((AppDbContext _context, Language language) =>
                _context.Countries
                    .AsNoTracking()
                    .Where(x => x.Region != null)
                    .Select(x => new CountryDto()
                    {
                        Id = x.Id,
                        Name = x.Translations != null 
                            ? (x.Translations.FirstOrDefault(y => y.Language == language)!.Name ?? x.Name) 
                        : x.Name,
                        Capital = x.Capital,
                        Currency = x.Currency,
                        Nationality = x.Nationality,
                    }));

    }
}
