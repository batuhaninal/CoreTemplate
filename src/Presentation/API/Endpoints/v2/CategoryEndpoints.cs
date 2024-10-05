using Application.Abstractions.Services.Categories;
using Application.Models.DTOs.Categories;
using Application.Models.RequestParameters;
using Application.Models.RequestParameters.Categories;
using Microsoft.AspNetCore.Mvc;

namespace API.Endpoints.v2
{
    public static class CategoryEndpoints
    {
        public static async Task<IResult> GetAll([AsParameters] PaginationRequestParameter pagination, ICategoryService categoryService)
        {
            var result = await categoryService.GetAllAsync(pagination);

            return Results.Ok(result);
        }

        public static async Task<IResult> GetAllFiltered([AsParameters] CategoryRequestParameter parameter, [AsParameters] PaginationRequestParameter pagination, ICategoryService categoryService)
        {
            var result = await categoryService.GetAllAsync(parameter, pagination);

            return Results.Ok(result);
        }

        public static async Task<IResult> GetById([FromRoute(Name = "categoryid")] Guid categoryId, ICategoryService categoryService)
        {
            var result = await categoryService.GetByIdAsync(categoryId);

            return Results.Ok(result);
        }

        public static async Task<IResult> Create([FromBody] CreateCategoryDto createCategoryDto, ICategoryService categoryService)
        {
            var result = await categoryService.CreateAsync(createCategoryDto);

            return Results.Created();
        }
     
        public static async Task<IResult> Remove([FromRoute(Name = "categoryid")] Guid categoryId, ICategoryService categoryService)
        {
            var result = await categoryService.RemoveAsync(categoryId);

            return Results.NoContent();
        }

        public static async Task<IResult> Update([FromRoute(Name = "categoryid")] Guid categoryId, [FromBody] UpdateCategoryDto updateCategoryDto, ICategoryService categoryService)
        {
            var result = await categoryService.UpdateAsync(categoryId, updateCategoryDto);

            return Results.NoContent();
        } 
    }
}
