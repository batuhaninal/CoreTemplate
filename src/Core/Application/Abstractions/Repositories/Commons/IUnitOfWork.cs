using Application.Abstractions.Repositories.Articles;
using Application.Abstractions.Repositories.Categories;
using Application.Abstractions.Repositories.Roles;
using Application.Abstractions.Repositories.UserRoles;
using Application.Abstractions.Repositories.Users;

namespace Application.Abstractions.Repositories.Commons
{
    public interface IUnitOfWork : IDisposable, IAsyncDisposable
    {
        ICategoryReadRepository CategoryReadRepository { get; }
        ICategoryWriteRepository CategoryWriteRepository { get; }
        IArticleReadRepository ArticleReadRepository { get; }
        IArticleWriteRepository ArticleWriteRepository { get; }
        IUserReadRepository UserReadRepository { get; }
        IUserWriteRepository UserWriteRepository { get; }
        IRoleReadRepository RoleReadRepository { get; }
        IRoleWriteRepository RoleWriteRepository { get; }
        IUserRoleReadRepository UserRoleReadRepository { get; }
        IUserRoleWriteRepository UserRoleWriteRepository { get; }
        IDatabaseTransaction BeginTransaction();
        Task<int> SaveChangesAsync();
    }
}
