using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TddSample.Domain;

namespace TddSample.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SlotsController : ControllerBase
    {
        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            var results = await Task.FromResult(new List<Slot>
            {
                new Slot
                {
                    From = new TimeSpan(12, 0, 0),
                    To = TimeSpan.Zero,
                }
            });
            return Ok(results);
        }
    }
}
