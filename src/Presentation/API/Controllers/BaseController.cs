using Application.Abstractions.Commons.Results;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BaseController : ControllerBase
    {
        /// <summary>
        /// Creates a standardized response object from the given result.
        /// </summary>
        /// <param name="data">The result data containing status codes and any other necessary information.</param>
        /// <returns>An IActionResult containing the standardized result.</returns>
        public static IActionResult CreateResponse(IBaseResult data)
        {
            return new ObjectResult(data)
            {
                StatusCode = data.StatusCode
            };

            //return data.StatusCode switch
            //{
            //    200 => new OkObjectResult(data), // 200 OK
            //    201 => new CreatedResult(string.Empty, data), // 201 Created
            //    204 => new NoContentResult(), // 204 No Content
            //    400 => new BadRequestObjectResult(data), // 400 Bad Request
            //    500 => new ObjectResult(data) { StatusCode = 500 }, // 500 Internal Server Error
            //    _ => data.Success ? new OkObjectResult(data) : new BadRequestObjectResult(data) // Diğer durumlar
            //};
        }
    }
}
