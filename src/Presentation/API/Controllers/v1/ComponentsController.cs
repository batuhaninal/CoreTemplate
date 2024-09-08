using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.v1
{
    [Route("api/v{v:apiVersion}/[controller]/[action]")]
    [ApiController]
    public class ComponentsController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> SearchComponent([FromQuery] string condition)
        {
            return Ok();
        }
    }
}
