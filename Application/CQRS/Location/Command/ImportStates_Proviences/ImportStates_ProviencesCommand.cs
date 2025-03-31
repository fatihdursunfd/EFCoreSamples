using Application.Common.ResponseModels.Interfaces;
using Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.CQRS.Location.Command.ImportStates_Proviences
{
    public class ImportStates_ProviencesCommand : IRequest<IResponse>
    {
        public IFormFile File { get; set; } = null!;
    }

    public class ImportStates_ProviencesCommandHandler : IRequestHandler<ImportStates_ProviencesCommand, IResponse>
    {
        private readonly ILocationService _locationService;

        public ImportStates_ProviencesCommandHandler(ILocationService locationService)
        {
            _locationService = locationService;
        }

        public async Task<IResponse> Handle(ImportStates_ProviencesCommand request, CancellationToken cancellationToken)
        {
            return await _locationService.ImportState_ProviencesAsync(request, cancellationToken);  
        }
    }
}