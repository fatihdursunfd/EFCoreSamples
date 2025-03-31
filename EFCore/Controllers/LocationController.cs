using API.Models;
using Application.CQRS.Location.Command.ImportCountries;
using Application.CQRS.Location.Command.ImportProvience_Districts;
using Application.CQRS.Location.Command.ImportStates_Proviences;
using Application.CQRS.Location.Queries.GetAllCountries;
using Application.CQRS.Location.Queries.GetCountries;
using Application.CQRS.Location.Queries.GetCountryById;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class LocationController : BaseApiController
    {

        /// <summary>
        /// It used for import countries from given json file
        /// </summary>
        /// <returns>A newly created TodoItem</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("ImportCountries")]
        public async Task<IActionResult> ImportCountries(ImportCountriesCommand model)
        {
            var response = await Mediator.Send(model);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }


        /// <summary>
        /// It used for import countries from given json file
        /// </summary>
        /// <returns>A newly created TodoItem</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("Country/{id}/{language}")]
        public async Task<IActionResult> GetCountryById(Language language, int id)
        {
            var response = await Mediator.Send(new GetCountryByIdQuery() { Language = language, Id = id });
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        /// <summary>
        /// It used for import countries from given json file
        /// </summary>
        /// <returns>A newly created TodoItem</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("Country/CompiledQuery/{id}/{language}")]
        public async Task<IActionResult> GetCountryByIdWithCompiledQuery(Language language, int id)
        {
            var response = await Mediator.Send(new GetCountryByIdWithCompiledQuery() { Language = language, Id = id });
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        /// <summary>
        /// It used for import countries from given json file
        /// </summary>
        /// <returns>A newly created TodoItem</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("Country/All/{language}")]
        public async Task<IActionResult> GetAllCountries(Language language)
        {
            var response = await Mediator.Send(new GetAllCountriesQuery() { Language = language });
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        /// <summary>
        /// It used for import countries from given json file
        /// </summary>
        /// <returns>A newly created TodoItem</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("Country/CompiledQuery/All/{language}")]
        public async Task<IActionResult> GetallCountriesWithCompiledQuery(Language language)
        {
            var response = await Mediator.Send(new GetAllCountriesWithCompiledQuery() { Language = language });
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        /// <summary>
        /// It used for import states/proviences from given json file
        /// </summary>
        /// <returns>A newly created TodoItem</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("ImportStates")]
        public async Task<IActionResult> ImportStates(ImportStates_ProviencesCommand model)
        {
            var response = await Mediator.Send(model);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }


        /// <summary>
        /// It used for import proviences/districts from given json file
        /// </summary>
        /// <returns>A newly created TodoItem</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("ImportProviences")]
        public async Task<IActionResult> ImportProviences(ImportProvience_DistrictsCommand model)
        {
            var response = await Mediator.Send(model);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
    }
}