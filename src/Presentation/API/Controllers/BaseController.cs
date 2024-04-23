using Application.Abstractions.Commons.Results;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        public static IActionResult CreateResponse(IBaseResult data)
        {
            return new ObjectResult(data)
            {
                StatusCode = data.StatusCode
            };
        }
    }
}
