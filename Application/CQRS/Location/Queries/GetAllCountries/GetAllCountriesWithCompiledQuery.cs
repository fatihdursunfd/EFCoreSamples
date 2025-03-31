using Application.Common.ResponseModels.Interfaces;
using Application.Interfaces;
using Domain.Dtos.Location;
using Domain.Enums;
using MediatR;

namespace Application.CQRS.Location.Queries.GetAllCountries
{
    public class GetAllCountriesWithCompiledQuery : IRequest<IDataResponse<List<CountryDto>>>
    {
        public Language Language { get; set; }
    }

    public class GetAllCountriesWithCompiledQueryHandler : IRequestHandler<GetAllCountriesWithCompiledQuery, IDataResponse<List<CountryDto>>>
    {
        private readonly ILocationService _locationService;
        public GetAllCountriesWithCompiledQueryHandler(ILocationService locationService)
        {
            _locationService = locationService;
        }
        public async Task<IDataResponse<List<CountryDto>>> Handle(GetAllCountriesWithCompiledQuery request, CancellationToken cancellationToken)
        {
            return await _locationService.GetAllCountriesWithCompiledQueryAsync(request, cancellationToken);
        }
    }
}