using Domain.Entities.Commons;

namespace Domain.Entities
{
    public class UserWriterFavorite : BaseEntity
    {
        public Guid UserId { get; set; }
        public virtual User? User { get; set; }
        public Guid WriterId { get; set; }
        public virtual Writer? Writer { get; set; }
    }
}
