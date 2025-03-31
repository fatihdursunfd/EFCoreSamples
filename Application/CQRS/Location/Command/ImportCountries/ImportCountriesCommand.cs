using Application.Common.ResponseModels.Interfaces;
using Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.CQRS.Location.Command.ImportCountries
{
    public class ImportCountriesCommand : IRequest<IResponse>
    {
        public IFormFile File { get; set; } = null!;
    }

    public class ImportCountriesCommandHandler : IRequestHandler<ImportCountriesCommand, IResponse>
    {
        private readonly ILocationService _locationService;

        public ImportCountriesCommandHandler(ILocationService locationService)
        {
            _locationService = locationService;
        }

        public async Task<IResponse> Handle(ImportCountriesCommand request, CancellationToken cancellationToken)
        {
            return await _locationService.ImportCountriesAsync(request, cancellationToken); 
        }
    }
}