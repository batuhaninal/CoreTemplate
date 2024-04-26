using Application.Abstractions.Repositories.Products;
using Application.Utilities.Exceptions.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Services.Products
{
    public class ProductBusinessRule
    {
        private readonly IProductReadRepository _repository;

        public ProductBusinessRule(IProductReadRepository repository)
        {
            _repository = repository;
        }

        public async Task CheckProductExist(string productId) 
        {
            bool result = await _repository.AnyAsync(x => x.Id == Guid.Parse(productId));
            if (!result)
                throw new NotFoundException("Product");
        }
    }
}
