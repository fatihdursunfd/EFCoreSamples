using Application.Common.ResponseModels.Interfaces;
using Application.Common.ResponseModels.Models;
using Application.CQRS.Location.Command.ImportCountries;
using Application.CQRS.Location.Command.ImportProvience_Districts;
using Application.CQRS.Location.Command.ImportStates_Proviences;
using Application.CQRS.Location.Queries.GetAllCountries;
using Application.CQRS.Location.Queries.GetCountries;
using Application.CQRS.Location.Queries.GetCountryById;
using Application.Interfaces;
using Domain.Dtos.Location;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Application.Services
{
    public class LocationService : ILocationService
    {
        private readonly IAppDbContext _context;

        public LocationService(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<IDataResponse<CountryDto>> GetCountryByIdAsync(GetCountryByIdQuery query, CancellationToken cancellationToken)
        {
            var _query = _context.Countries
                .AsNoTracking()
                .Where(x => x.Id == query.Id && x.Region != null)
                .Select(x => new CountryDto()
                {
                    Id = x.Id,
                    Name = x.Translations != null
                        ? (x.Translations.FirstOrDefault(y => y.Language == query.Language)!.Name ?? x.Name)
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
                });

            var entity = await _query.FirstOrDefaultAsync(cancellationToken);

            if (entity is null)
            {
                return new ErrorDataResponse<CountryDto>();
            }

            return new SuccessDataResponse<CountryDto>(entity);
        }

        public async Task<IDataResponse<CountryDto>> GetCountryByIdWithCompileQueryAsync(GetCountryByIdWithCompiledQuery query, CancellationToken cancellationToken)
        {
            var entity = await _context.GetCountryByIdAsync(query.Id, query.Language);

            if (entity is null)
            {
                return new ErrorDataResponse<CountryDto>();
            }

            return new SuccessDataResponse<CountryDto>(entity);
        }

        public async Task<IDataResponse<List<CountryDto>>> GetAllCountriesAsync(GetAllCountriesQuery request, CancellationToken cancellationToken)
        {
            var _query = _context.Countries
                .AsNoTracking()
                .Select(x => new CountryDto()
                {
                    Id = x.Id,
                    Name = x.Translations != null
                        ? (x.Translations.FirstOrDefault(y => y.Language == request.Language)!.Name ?? x.Name)
                        : x.Name,
                    Capital = x.Capital,
                    Currency = x.Currency,
                    Nationality = x.Nationality,
                });

            var entities = await _query.ToListAsync(cancellationToken);

            return new SuccessDataResponse<List<CountryDto>>(entities);
        }

        public async Task<IDataResponse<List<CountryDto>>> GetAllCountriesWithCompiledQueryAsync(GetAllCountriesWithCompiledQuery request, CancellationToken cancellationToken)
        {
            var entities = await _context.GetAllCountriesAsync(request.Language);

            return new SuccessDataResponse<List<CountryDto>>(entities);
        }

        #region Import Services

        /// <summary>
        /// Ülkeleri eklemek için kullanılır. Aşağıdaki url'deki dosya kullanılmaktadır
        /// https://data.world/dr5hn/country-state-city/workspace/file?filename=countries.json
        /// </summary>
        public async Task<IResponse> ImportCountriesAsync(ImportCountriesCommand command, CancellationToken cancellationToken)
        {
            string jsonFileContent = string.Empty;

            using (var reader = new StreamReader(command.File.OpenReadStream()))
            {
                jsonFileContent = reader.ReadToEnd();
            }

            var countries = JsonConvert.DeserializeObject<List<CountriesJsonDto>>(jsonFileContent);

            var entities = countries?
                .Select(x => new Country()
                {
                    Capital = x.capital,
                    Currency = x.currency,
                    CurrencyName = x.currency_name,
                    CurrencySymbol = x.currency_symbol,
                    NumericCode = x.numeric_code,
                    Iso2 = x.iso2,
                    Iso3 = x.iso3,
                    Latitude = x.latitude,
                    Longitude = x.longitude,
                    Name = x.name,
                    Nationality = x.nationality,
                    PhoneCode = x.phone_code,
                    Region = x.region,
                    Subregion = x.subregion,
                    Tld = x.tld,
                    HasState = false,
                    Timezones = x.timezones != null && x.timezones.Length > 0 ? x.timezones
                        .Select(y => new Domain.Entities.Timezone()
                        {
                            Abbreviation = y.abbreviation,
                            GmtOffset = y.gmtOffset,
                            GmtOffsetName = y.gmtOffsetName,
                            TzName = y.tzName,
                            ZoneName = y.zoneName ?? string.Empty,
                        })
                        .ToList() : new(),
                    Translations = GetCountryNameTranslations(x.translations ?? new(), x.name)
                })
                .ToList();

            if (entities != null && entities.Any())
            {
                await _context.Countries.AddRangeAsync(entities, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
            }

            return new SuccessResponse();
        }


        /// <summary>
        /// Eyalet ve şehirleri eklemek için kullanılır. Aşağıdaki url'deki dosya kullanılmaktadır
        /// https://data.world/dr5hn/country-state-city/workspace/file?filename=states.json
        /// </summary>
        public async Task<IResponse> ImportState_ProviencesAsync(ImportStates_ProviencesCommand command, CancellationToken cancellationToken)
        {
            var countries = await _context.Countries
                .ToListAsync(cancellationToken);

            string jsonFileContent = string.Empty;

            using (var reader = new StreamReader(command.File.OpenReadStream()))
            {
                jsonFileContent = reader.ReadToEnd();
            }

            var data = JsonConvert.DeserializeObject<List<StateJsonDto>>(jsonFileContent);

            var stateEntities = new List<State>();
            var provienceEntities = new List<Provience>();

            foreach (var item in data!)
            {
                var country = countries.FirstOrDefault(x => x.Iso2 == item.country_code);
                if (country is null) continue;

                if (item.type == "state")
                {
                    stateEntities.Add(new()
                    {
                        StateCode = item.state_code,
                        Latitude = item.latitude,
                        Longitude = item.longitude,
                        Name = item.name ?? string.Empty,
                        CountryId = country.Id,
                    });

                    country.HasState = true;
                }
                else
                {
                    provienceEntities.Add(new()
                    {
                        Name = item.name ?? string.Empty,
                        Latitude = item.latitude,
                        Longitude = item.longitude,
                        CountryId = country.Id,
                    });
                }
            }

            await _context.States.AddRangeAsync(stateEntities, cancellationToken);
            await _context.Provinces.AddRangeAsync(provienceEntities, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return new SuccessResponse();
        }


        /// <summary>
        /// Şehirleri ve ilçeleri  eklemek için kullanılır. Aşağıdaki url'deki dosya kullanılmaktadır
        /// https://data.world/dr5hn/country-state-city/workspace/file?filename=cities.json
        /// </summary>
        public async Task<IResponse> ImportProvience_DistrictsAsync(ImportProvience_DistrictsCommand command, CancellationToken cancellationToken)
        {
            var countries = await _context.Countries
                .ToListAsync(cancellationToken);

            var states = await _context.States
               .ToListAsync(cancellationToken);

            var proviences = await _context.Provinces
                .ToListAsync(cancellationToken);

            string jsonFileContent = string.Empty;

            using (var reader = new StreamReader(command.File.OpenReadStream()))
            {
                jsonFileContent = reader.ReadToEnd();
            }

            var data = JsonConvert.DeserializeObject<List<ProvienceJsonDto>>(jsonFileContent);

            var provienceEntities = new List<Provience>();
            var districtEntities = new List<District>();

            foreach (var item in data!)
            {
                var country = countries.FirstOrDefault(x => x.Iso2 == item.country_code);
                if (country is null) continue;

                // Provience ekle
                if (country.HasState)
                {
                    var state = states.FirstOrDefault(x => x.StateCode == item.state_code);
                    if (state is null) continue;

                    provienceEntities.Add(new()
                    {
                        StateId = state.Id,
                        Name = item.name ?? string.Empty,
                        Latitude = item.latitude,
                        Longitude = item.longitude
                    });
                }

                // district ekle
                else
                {
                    var provience = proviences.FirstOrDefault(x => x.Name == item.state_name);
                    if (provience is null) continue;

                    districtEntities.Add(new()
                    {
                        ProvienceId = provience.Id,
                        Name = item.name ?? string.Empty,
                        Latitude = item.latitude,
                        Longitude = item.longitude,
                    });
                }
            }

            await _context.Provinces.AddRangeAsync(provienceEntities);
            await _context.Districts.AddRangeAsync(districtEntities, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return new SuccessResponse();
        }


        /// <summary>
        /// Ülke isimlerinin farklı dillerdeki çevirilerini oluşturmak için kullanılır
        /// </summary>
        private List<CountryTranslation> GetCountryNameTranslations(Translations translations, string countryName)
        {
            var type = typeof(Translations);

            var props = type.GetProperties();

            var entities = new List<CountryTranslation>();

            var lang = new Language();

            foreach (var prop in props)
            {
                if (Enum.TryParse(prop.Name, true, out lang))
                {
                    entities.Add(new CountryTranslation()
                    {
                        Language = lang,
                        Name = prop.GetValue(translations, null)?.ToString() ?? string.Empty,
                    });
                }
            }

            // İngilizce default olarak eklenir
            entities.Add(new CountryTranslation() { Language = Language.en, Name = countryName });

            return entities;
        }

        #endregion

    }
}