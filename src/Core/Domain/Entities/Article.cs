using Domain.Entities.Commons;

namespace Domain.Entities
{
    public class Article : BaseEntity
    {
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        // 1-n eklenmesi gerekmektedir
        public Guid CategoryId { get; set; }
        public virtual Category? Category { get; set; }
        public Guid WriterId { get; set; }
        public virtual Writer? Writer { get; set; }

        public virtual ICollection<ArticleFavorite>? ArticleFavorites { get; set; }
    }
}
