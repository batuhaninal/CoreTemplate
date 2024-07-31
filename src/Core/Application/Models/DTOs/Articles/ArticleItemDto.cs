using Application.Models.DTOs.Categories;
using Application.Models.DTOs.Writers;

namespace Application.Models.DTOs.Articles
{
    public record ArticleItemDto
    {
        public string ArticleId { get; init; } = null!;
        public string Title { get; init; } = null!;
        public int LikeCount { get; init; }
        public int FavCount { get; init; }
        public CategoryItemDto Category { get; init; } = null!;
        public WriterItemDto Writer { get; init; } = null!;
        public DateTime CreatedDate { get; init; }
    }
}
