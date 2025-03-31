using Application.Common.ResponseModels.Interfaces;
using Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.CQRS.Location.Command.ImportProvience_Districts
{
    public class ImportProvience_DistrictsCommand : IRequest<IResponse>
    {
        public IFormFile File { get; set; } = null!;
    }

    public class ImportProvience_DistrictsCommandHandler : IRequestHandler<ImportProvience_DistrictsCommand, IResponse>
    {
        private readonly ILocationService _locationService;

        public ImportProvience_DistrictsCommandHandler(ILocationService locationService)
        {
            _locationService = locationService;
        }

        public async Task<IResponse> Handle(ImportProvience_DistrictsCommand request, CancellationToken cancellationToken)
        {
            return await _locationService.ImportProvience_DistrictsAsync(request, cancellationToken);   
        }
    }
}