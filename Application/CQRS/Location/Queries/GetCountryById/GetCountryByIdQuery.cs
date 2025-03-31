using Application.Common.ResponseModels.Interfaces;
using Application.Interfaces;
using Domain.Dtos.Location;
using Domain.Enums;
using MediatR;

namespace Application.CQRS.Location.Queries.GetCountries
{
    public class GetCountryByIdQuery : IRequest<IDataResponse<CountryDto>>
    {
        public int Id { get; set; }
        public Language Language { get; set; }
    }

    public class GetCountryByIdQueryHandler : IRequestHandler<GetCountryByIdQuery, IDataResponse<CountryDto>>
    {
        private readonly ILocationService _locationService;
        public GetCountryByIdQueryHandler(ILocationService locationService)
        {
            _locationService = locationService;
        }
        public async Task<IDataResponse<CountryDto>> Handle(GetCountryByIdQuery request, CancellationToken cancellationToken)
        {
            return await _locationService.GetCountryByIdAsync(request, cancellationToken);
        }
    }
}