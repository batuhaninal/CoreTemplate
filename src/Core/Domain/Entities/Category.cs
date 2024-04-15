using Domain.Entities.Commons;

namespace Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Title { get; set; } = null!;


        // n-1 Istege bagli eklenmeyebilir
        public virtual ICollection<Product>? Products { get; set; }
    }
}
