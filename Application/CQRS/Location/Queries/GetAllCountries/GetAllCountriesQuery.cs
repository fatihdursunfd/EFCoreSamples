using Application.Common.ResponseModels.Interfaces;
using Application.Interfaces;
using Domain.Dtos.Location;
using Domain.Enums;
using MediatR;

namespace Application.CQRS.Location.Queries.GetAllCountries
{
    public class GetAllCountriesQuery : IRequest<IDataResponse<List<CountryDto>>>
    {
        public Language Language { get; set; }
    }

    public class GetAllCountriesQueryHandler : IRequestHandler<GetAllCountriesQuery, IDataResponse<List<CountryDto>>>
    {
        private readonly ILocationService _locationService;
        public GetAllCountriesQueryHandler(ILocationService locationService)
        {
            _locationService = locationService;
        }
        public async Task<IDataResponse<List<CountryDto>>> Handle(GetAllCountriesQuery request, CancellationToken cancellationToken)
        {
            return await _locationService.GetAllCountriesAsync(request, cancellationToken);
        }
    }
}