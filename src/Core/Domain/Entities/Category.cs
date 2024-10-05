using Domain.Entities.Commons;

namespace Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Title { get; set; } = null!;
        public Guid? ParentId { get; set; }
        public virtual Category? Parent { get; set; }
        public virtual ICollection<Category>? Childrens { get; set; }


        // n-1 Istege bagli eklenmeyebilir
        public virtual ICollection<Article>? Articles { get; set; }
    }
}
