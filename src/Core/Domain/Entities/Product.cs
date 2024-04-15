using Domain.Entities.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Title { get; set; } = null!;
        public decimal Price { get; set; }


        // 1-n eklenmesi gerekmektedir
        public Guid CategoryId { get; set; }
        public virtual Category? Category { get; set; }
    }
}
