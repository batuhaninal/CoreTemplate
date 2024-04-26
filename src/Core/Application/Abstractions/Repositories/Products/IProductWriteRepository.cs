using Application.Abstractions.Repositories.Commons;
using Domain.Entities;

namespace Application.Abstractions.Repositories.Products
{
    public interface IProductWriteRepository : IWriteRepository<Product> 
    {
    }
}
