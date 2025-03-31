using Application.Common.ResponseModels.Interfaces;
using Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Models
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        private ISender? _mediator;
        protected ISender Mediator
        {
            get
            {
                return _mediator ??= HttpContext.RequestServices.GetService<ISender>() ?? throw new NullReferenceException();
            }
        }

        protected IActionResult ApiResult<T>(T resultModel) where T : IResponse
        {
            if (!resultModel.IsSuccess)
            {
                return BadRequest(resultModel);
            }

            if (resultModel.MessageCode == MessageCodeConst.Error.NotFound)
            {
                return NoContent();
            }

            return Ok(resultModel);
        }
    }
}