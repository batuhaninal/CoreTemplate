using Application.Abstractions.Services.Users;
using Application.Models.RequestParameters;
using Application.Models.RequestParameters.Users;

namespace API.Endpoints.v2
{
    public static class UserEndpoints
    {
        public static async Task<IResult> GetAll([AsParameters] PaginationRequestParameter pagination, IUserService userService)
        {
            var result = await userService.GetAllAsync(pagination);

            return Results.Ok(result);
        }

        public static async Task<IResult> GetAllFiltered([AsParameters] UserRequestParameter parameter, [AsParameters] PaginationRequestParameter pagination, IUserService userService)
        {
            var result = await userService.GetAllAsync(parameter, pagination);

            return Results.Ok(result);
        }
    }
}
