using Domain.Entities.Commons;

namespace Domain.Entities
{
    public class Writer : BaseEntity
    {
        public string Nick { get; set; } = null!;
        public byte Level { get; set; }
        public Guid UserId { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<Article>? Articles { get; set; }
    }
}
