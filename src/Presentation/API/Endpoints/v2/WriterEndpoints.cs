using Application.Abstractions.Services.Writers;
using Application.Models.RequestParameters;
using Application.Models.RequestParameters.Writers;
using Microsoft.AspNetCore.Mvc;

namespace API.Endpoints.v2
{
    public static class WriterEndpoints
    {
        public static async Task<IResult> GetAll([AsParameters] PaginationRequestParameter paginationRequestParameter, IWriterService writerService)
        {
            var result = await writerService.GetAllAsync(paginationRequestParameter);

            return Results.Ok(result);
        }
        public static async Task<IResult> GetAllFiltered([AsParameters] WriterRequestParameter parameter, [AsParameters] PaginationRequestParameter pagination, IWriterService writerService)
        {
            var result = await writerService.GetAllAsync(parameter, pagination);

            return Results.Ok(result);
        }

        public static async Task<IResult> GetById([FromRoute(Name = "writerid")] Guid writerId, IWriterService writerService)
        {
            var result = await writerService.GetByIdAsync(writerId);

            return Results.Ok(result);
        }
    }
}
