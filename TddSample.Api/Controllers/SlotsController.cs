using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TddSample.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SlotsController : ControllerBase
    {
        public async Task<IActionResult> Get()
        {
            var results = await Task.FromResult(new[] { 1, 2, });
            return Ok(results);
        }
    }
}
