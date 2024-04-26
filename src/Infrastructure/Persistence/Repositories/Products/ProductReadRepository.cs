using Application.Abstractions.Repositories.Products;
using Domain.Entities;
using Persistence.Contexts;
using Persistence.Repositories.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Products
{
    public class ProductReadRepository : ReadRepository<Product>, IProductReadRepository
    {
        public ProductReadRepository(TemplateContext context) : base(context)
        {
        }
    }
}
