using Application.Models.DTOs.Categories;
using Application.Models.DTOs.Writers;

namespace Application.Models.DTOs.Articles
{
    public record ArticleItemDto
    {
        public Guid ArticleId { get; init; }
        public string Title { get; init; } = null!;
        public int LikeCount { get; init; }
        public int FavCount { get; init; }
        public CategoryToolDto Category { get; init; } = null!;
        public WriterItemDto Writer { get; init; } = null!;
        public DateTime CreatedDate { get; init; }
    }
}
