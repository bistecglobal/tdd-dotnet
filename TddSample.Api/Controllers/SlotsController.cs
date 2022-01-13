using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TddSample.Api.Application.Query;
using TddSample.Domain;

namespace TddSample.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SlotsController : ControllerBase
    {
        private IMediator _mediator;

        public SlotsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            var results = await _mediator.Send(new GetSlotsQuery { });
            return Ok(results);
        }
    }
}
