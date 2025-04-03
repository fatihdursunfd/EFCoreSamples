using Application.Common.ResponseModels.Interfaces;
using Application.Interfaces;
using MediatR;

namespace Application.CQRS.Company.Command.DeleteCompany
{
    public class DeleteCompanyCommand : IRequest<IResponse>
    {
        public int Id { get; set; }
    }

    public class DeleteCompanyCommandHandler : IRequestHandler<DeleteCompanyCommand, IResponse>
    {
        private readonly ICompanyService _companyService;

        public DeleteCompanyCommandHandler(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        public async Task<IResponse> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
        {
            return await _companyService.DeleteAsync(request, cancellationToken);
        }
    }
}