using Application.Common.ResponseModels.Interfaces;
using Application.CQRS.Company.Command.CreateCompany;
using Application.CQRS.Company.Command.DeleteCompany;
using Application.CQRS.Company.Command.UpdateCompany;
using Domain.Dtos;

namespace Application.Interfaces
{
    public interface ICompanyService
    {
        Task<IResponse> CreateAsync(CreateCompanyCommand command, CancellationToken cancellationToken);
        Task<IResponse> UpdateAsync(UpdateCompanyCommand command, CancellationToken cancellationToken);
        Task<IResponse> DeleteAsync(DeleteCompanyCommand command, CancellationToken cancellationToken);

        Task<IDataResponse<List<CompanyDto>>> GetAllAsync(CancellationToken cancellationToken);
    }
}