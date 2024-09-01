using Application.Abstractions.Services.Articles;
using Application.Models.DTOs.Articles;
using Application.Models.RequestParameters;
using Application.Models.RequestParameters.Articles;
using Microsoft.AspNetCore.Mvc;

namespace API.Endpoints.v2
{
    public static class ArticleEndpoints
    {

        public static async Task<IResult> GetAll([AsParameters] PaginationRequestParameter parameter, [FromServices] IArticleService articleService)
        {
            var data = await articleService.GetAllAsync(parameter.PageIndex, parameter.PageSize);
            return Results.Ok(data);
        }

        public static async Task<IResult> GetById([FromRoute] string articleId, [FromServices] IArticleService articleService)
        {
            var data = await articleService.GetByIdAsync(articleId);

            return Results.Ok(data);
        }

        public static async Task<IResult> GetAllFiltered([AsParameters] ArticleRequestParameter parameter, [AsParameters] PaginationRequestParameter pagination, [FromServices] IArticleService articleService)
        {
            var data = await articleService.GetAllAsync(parameter, pagination);
            return Results.Ok(data);
        }

        public static async Task<IResult> Update([FromRoute] string articleId, [FromBody] UpdateArticleDto updateArticleDto, [FromServices] IArticleService articleService)
        {
            await articleService.UpdateAsync(articleId, updateArticleDto);
            return Results.NoContent();
        }

        public static async Task<IResult> Remove([FromRoute] string articleId, [FromServices] IArticleService articleService)
        {
            await articleService.RemoveAsync(articleId);

            return Results.NoContent();
        }

        public static async Task<IResult> Create([FromBody] CreateArticleDto createArticleDto, IArticleService articleService)
        {
            await articleService.CreateAsync(createArticleDto);

            return Results.Created();
        }
    }
}
