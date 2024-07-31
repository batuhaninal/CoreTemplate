using Application.Models.DTOs.Categories;
using Application.Models.DTOs.Writers;

namespace Application.Models.DTOs.Articles
{
    public record ArticleInfoDto
    {
        public string ArticleId { get; init; } = null!;
        public string Title { get; init; } = null!;
        public string Content { get; init; } = null!;
        public int LikeCount { get; init; }
        public int FavCount { get; init; }
        public CategoryInfoDto Category { get; init; } = null!;
        public WriterInfoDto Writer { get; init; } = null!;
        public DateTime CreatedDate { get; init; }
    }
}
