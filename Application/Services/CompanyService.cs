using Application.Common.ResponseModels.Interfaces;
using Application.Common.ResponseModels.Models;
using Application.CQRS.Company.Command.CreateCompany;
using Application.CQRS.Company.Command.DeleteCompany;
using Application.CQRS.Company.Command.UpdateCompany;
using Application.Interfaces;
using Application.Interfaces.Data;
using Domain.Dtos;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IAppDbContext _context;

        public CompanyService(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<IResponse> CreateAsync(CreateCompanyCommand command, CancellationToken cancellationToken)
        {
            var company = new Company
            {
                Name = command.Name,
                Description = command.Description,
                EmployeeCount = command.EmployeeCount
            };

            await _context.Companies.AddAsync(company, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return new SuccessResponse();
        }

        public async Task<IResponse> UpdateAsync(UpdateCompanyCommand command, CancellationToken cancellationToken)
        {
            var company = await _context.Companies.FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);
            if (company is null)
            {
                return new ErrorResponse { IsSuccess = false, MessageCode = "CompanyNotFound", Messages = new[] { "Company not found." } };
            }

            company.Name = command.Name;
            company.Description = command.Description;
            company.EmployeeCount = command.EmployeeCount;

            await _context.SaveChangesAsync(cancellationToken);

            return new ErrorResponse { IsSuccess = true, MessageCode = "CompanyUpdated", Messages = new[] { "Company updated successfully." } };
        }

        public async Task<IResponse> DeleteAsync(DeleteCompanyCommand command, CancellationToken cancellationToken)
        {
            var company = await _context.Companies.FindAsync(new object[] { command.Id }, cancellationToken);
            if (company is null)
            {
                return new ErrorResponse { IsSuccess = false, MessageCode = "CompanyNotFound", Messages = new[] { "Company not found." } };
            }

            _context.Companies.Remove(company);
            await _context.SaveChangesAsync(cancellationToken);

            return new ErrorResponse { IsSuccess = true, MessageCode = "CompanyDeleted", Messages = new[] { "Company deleted successfully." } };
        }

        public async Task<IDataResponse<List<CompanyDto>>> GetAllAsync(CancellationToken cancellationToken)
        {
            var entities = await _context.Companies
                .Select(x => new CompanyDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    EmployeeCount = x.EmployeeCount
                })
                .ToListAsync(cancellationToken);

            return new SuccessDataResponse<List<CompanyDto>>(entities);
        }
    }
}