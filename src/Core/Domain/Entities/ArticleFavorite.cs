using Domain.Entities.Commons;

namespace Domain.Entities
{
    public class ArticleFavorite : BaseEntity
    {
        public Guid ArticleId { get; set; }
        public virtual Article? Article { get; set; }

        public Guid UserId { get; set; }
        public virtual User? User { get; set; }
    }
}
