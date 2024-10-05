using Application.Abstractions.Repositories.Commons;
using Domain.Entities;

namespace Application.Abstractions.Repositories.Categories
{
    public interface ICategoryReadRepository : IReadRepository<Category>
    {
        Task<List<Guid>?> GetAllChildrensId(Guid categoryId);
        Task<List<Guid>?> GetAllChildrensId(Guid categoryId, bool isActive);
        Task GetChildrenRecursiveAsync(Category category);
        Task GetChildrenRecursiveAsync(Category category, bool isActive);
    }
}
