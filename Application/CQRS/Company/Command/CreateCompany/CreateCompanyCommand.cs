using Application.Common.ResponseModels.Interfaces;
using Application.Interfaces;
using MediatR;

namespace Application.CQRS.Company.Command.CreateCompany
{
    public class CreateCompanyCommand : IRequest<IResponse>
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int EmployeeCount { get; set; }
    }

    public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, IResponse>
    {
        private readonly ICompanyService _companyService;

        public CreateCompanyCommandHandler(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        public async Task<IResponse> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            return await _companyService.CreateAsync(request, cancellationToken);
        }
    }

}