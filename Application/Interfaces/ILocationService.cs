using Application.Common.ResponseModels.Interfaces;
using Application.CQRS.Location.Command.ImportCountries;
using Application.CQRS.Location.Command.ImportProvience_Districts;
using Application.CQRS.Location.Command.ImportStates_Proviences;
using Application.CQRS.Location.Queries.GetAllCountries;
using Application.CQRS.Location.Queries.GetCountries;
using Application.CQRS.Location.Queries.GetCountryById;
using Domain.Dtos.Location;

namespace Application.Interfaces
{
    public interface ILocationService
    {
        Task<IDataResponse<CountryDto>> GetCountryByIdAsync(GetCountryByIdQuery query, CancellationToken cancellationToken);
        Task<IDataResponse<CountryDto>> GetCountryByIdWithCompileQueryAsync(GetCountryByIdWithCompiledQuery query, CancellationToken cancellationToken);
        
        Task<IResponse> ImportCountriesAsync(ImportCountriesCommand command, CancellationToken cancellationToken);
        Task<IResponse> ImportState_ProviencesAsync(ImportStates_ProviencesCommand command, CancellationToken cancellationToken);
        Task<IResponse> ImportProvience_DistrictsAsync(ImportProvience_DistrictsCommand command, CancellationToken cancellationToken);
        Task<IDataResponse<List<CountryDto>>> GetAllCountriesAsync(GetAllCountriesQuery request, CancellationToken cancellationToken);
        Task<IDataResponse<List<CountryDto>>> GetAllCountriesWithCompiledQueryAsync(GetAllCountriesWithCompiledQuery request, CancellationToken cancellationToken);
    }
}