using API.Models;
using Application.CQRS.Company.Command.CreateCompany;
using Application.CQRS.Company.Command.DeleteCompany;
using Application.CQRS.Company.Command.UpdateCompany;
using Application.CQRS.Company.Queries.GetAllCompanies;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CompanyController : BaseApiController
    {
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("")]
        public async Task<IActionResult> Create(CreateCompanyCommand model)
        {
            var response = await Mediator.Send(model);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }


        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("")]
        public async Task<IActionResult> Update(UpdateCompanyCommand model)
        {
            var response = await Mediator.Send(model);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await Mediator.Send(new DeleteCompanyCommand() { Id = id });
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            var response = await Mediator.Send(new GetAllCompaniesQuery());
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
    }
}