using Application.Abstractions.Commons.Results;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
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
