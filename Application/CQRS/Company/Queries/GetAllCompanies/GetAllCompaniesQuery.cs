using Application.Common.ResponseModels.Interfaces;
using Application.Interfaces;
using Domain.Dtos;
using MediatR;

namespace Application.CQRS.Company.Queries.GetAllCompanies
{
    public class GetAllCompaniesQuery : IRequest<IDataResponse<List<CompanyDto>>>
    {
    }

    public class GetAllCompaniesQueryHandler : IRequestHandler<GetAllCompaniesQuery, IDataResponse<List<CompanyDto>>>
    {
        private readonly ICompanyService _companyService;

        public GetAllCompaniesQueryHandler(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        public async Task<IDataResponse<List<CompanyDto>>> Handle(GetAllCompaniesQuery request, CancellationToken cancellationToken)
        {
            return await _companyService.GetAllAsync(cancellationToken);
        }
    }
}