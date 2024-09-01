using API.Endpoints.v2;

namespace API.Controllers.v2
{
    public static class MapEndpoints
    {
        public static void MapArticleEndpoints(this IEndpointRouteBuilder app)
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
                .RequireAuthorization(x => x.RequireRole("admin"))
                .WithName(nameof(ArticleEndpoints.Remove))
                .MapToApiVersion(2);
        }

        public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("auth");

            group.MapPost("signin", AuthEndpoints.SignIn)
                .RequireRateLimiting("Api")
                .MapToApiVersion(2);

            group.MapPost("signup", AuthEndpoints.SignUp)
                .RequireRateLimiting("Api")
                .MapToApiVersion(2);

            group.MapPost("writersignup", AuthEndpoints.WriterSignUp)
                .RequireRateLimiting("Api")
                .MapToApiVersion(2);
        }

        public static void MapCategoryEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("categories");

            group.MapGet("getall", CategoryEndpoints.GetAll)
                .RequireRateLimiting("Api")
                .MapToApiVersion(2);

            group.MapGet("getallfiltered", CategoryEndpoints.GetAllFiltered)
                .RequireRateLimiting("Api")
                .MapToApiVersion(2);

            group.MapGet("getbyid/{categoryId}", CategoryEndpoints.GetById)
                .RequireRateLimiting("Api")
                .MapToApiVersion(2);

            group.MapPost("create", CategoryEndpoints.Create)
                .RequireRateLimiting("Api")
                .RequireAuthorization(x=> x.RequireRole("admin"))
                .MapToApiVersion(2);

            group.MapDelete("remove/{categoryId}", CategoryEndpoints.Remove)
                .RequireRateLimiting("Api")
                .RequireAuthorization(x => x.RequireRole("admin"))
                .MapToApiVersion(2);

            group.MapPut("update/{categoryId}", CategoryEndpoints.Update)
                .RequireRateLimiting("Api")
                .RequireAuthorization(x => x.RequireRole("admin"))
                .MapToApiVersion(2);
        }

        public static void MapUserEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("users");

            group.MapGet("getall", UserEndpoints.GetAll)
                .RequireRateLimiting("Api")
                .RequireAuthorization()
                .MapToApiVersion(2);

            group.MapGet("getallfiltered", UserEndpoints.GetAllFiltered)
                .RequireRateLimiting("Api")
                .RequireAuthorization()
                .MapToApiVersion(2);
        }

        public static void MapWriterEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("writers");

            group.MapGet("getall", WriterEndpoints.GetAll)
                .RequireRateLimiting("Api")
                .MapToApiVersion(2);

            group.MapGet("getallfiltered", WriterEndpoints.GetAllFiltered)
                .RequireRateLimiting("Api")
                .MapToApiVersion(2);

            group.MapGet("getbyid/{writerId}", WriterEndpoints.GetById)
                .RequireRateLimiting("Api")
                .RequireAuthorization(x=> x.RequireRole("admin"))
                .MapToApiVersion(2);
        }
    }
}
