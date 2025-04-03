using Application.Common.ResponseModels.Interfaces;
using Application.Interfaces;
using MediatR;

namespace Application.CQRS.Company.Command.UpdateCompany
{
    public class UpdateCompanyCommand : IRequest<IResponse>
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int EmployeeCount { get; set; }
    }

    public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand, IResponse>
    {
        private readonly ICompanyService _companyService;

        public UpdateCompanyCommandHandler(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        public async Task<IResponse> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            return await _companyService.UpdateAsync(request, cancellationToken);
        }
    }
}