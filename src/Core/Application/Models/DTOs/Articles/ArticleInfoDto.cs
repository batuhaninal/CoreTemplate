using Application.Models.DTOs.Categories;

namespace Application.Models.DTOs.Articles
{
    public record ArticleInfoDto
    {
        public string ArticleId { get; init; } = null!;
        public string Title { get; init; } = null!;
        public string Content { get; init; } = null!;
        public CategoryInfoDto Category { get; init; } = null!;
        public DateTime CreatedDate { get; init; }
    }
}
