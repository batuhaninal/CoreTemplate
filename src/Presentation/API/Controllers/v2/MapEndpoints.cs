using API.Endpoints;
using Application.Abstractions.Services.Articles;
using Application.Models.DTOs.Articles;

namespace API.Controllers.v2
{
    public static class MapEndpoints
    {
        public static void MapArticlesEndpoints(this IEndpointRouteBuilder app)
        {
            var articleGroup = app.MapGroup("articles");
            //articleGroup.MapPost("create", async ([FromBody]CreateArticleDto createArticleDto, IArticleService articleService) =>
            //{
            //    await articleService.CreateAsync(createArticleDto);

            //    return Results.Created();
            //})
            //    .RequireRateLimiting("Test")
            //    .RequireAuthorization(x => x.RequireRole("admin"))
            //    .MapToApiVersion(2);

            articleGroup.MapGet("getall", ArticleEndpoints.GetAll)
                .RequireRateLimiting("Api")
                .MapToApiVersion(2);

            articleGroup.MapGet("getbyid/{articleId}", ArticleEndpoints.GetById)
                .RequireRateLimiting("Api")
                .MapToApiVersion(2);

            articleGroup.MapGet("getallfiltered", ArticleEndpoints.GetAllFiltered)
                .RequireRateLimiting("Test")
                .MapToApiVersion(2);

            articleGroup.MapPost("create", ArticleEndpoints.Create)
                .RequireRateLimiting("Api")
                .RequireAuthorization(x => x.RequireRole("admin"))
                .MapToApiVersion(2);

            articleGroup.MapPut("update/{articleId}", ArticleEndpoints.Update)
                .RequireRateLimiting("Test")
                .RequireAuthorization(x => x.RequireRole("admin"))
                .WithName(nameof(ArticleEndpoints.Update))
                .MapToApiVersion(2);

            articleGroup.MapDelete("remove/{articleId}", ArticleEndpoints.Remove)
                .RequireRateLimiting("Test")
                .RequireAuthorization(x=> x.RequireRole("admin"))
                .WithName(nameof(ArticleEndpoints.Remove))
                .MapToApiVersion(2);
        }
    }
}
