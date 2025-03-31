using Application.Common.ResponseModels.Interfaces;
using Application.Interfaces;
using Domain.Dtos.Location;
using Domain.Enums;
using MediatR;

namespace Application.CQRS.Location.Queries.GetCountryById
{
    public class GetCountryByIdWithCompiledQuery : IRequest<IDataResponse<CountryDto>>
    {
        public int Id { get; set; }
        public Language Language { get; set; }
    }

    public class GetCountryByIdWithCompiledQueryHandler : IRequestHandler<GetCountryByIdWithCompiledQuery, IDataResponse<CountryDto>>
    {
        private readonly ILocationService _locationService;

        public GetCountryByIdWithCompiledQueryHandler(ILocationService locationService)
        {
            _locationService = locationService;
        }

        public async Task<IDataResponse<CountryDto>> Handle(GetCountryByIdWithCompiledQuery request, CancellationToken cancellationToken)
        {
            return await _locationService.GetCountryByIdWithCompileQueryAsync(request, cancellationToken);
        }
    }
}