using Application.Abstractions.Repositories.ArticleFavorites;
using Application.Abstractions.Repositories.Articles;
using Application.Abstractions.Repositories.Categories;
using Application.Abstractions.Repositories.Commons;
using Application.Abstractions.Repositories.Roles;
using Application.Abstractions.Repositories.UserRoles;
using Application.Abstractions.Repositories.Users;
using Application.Abstractions.Repositories.Writers;
using Persistence.Contexts;
using Persistence.Repositories.ArticleFavorites;
using Persistence.Repositories.Articles;
using Persistence.Repositories.Categories;
using Persistence.Repositories.Roles;
using Persistence.Repositories.UserRoles;
using Persistence.Repositories.Users;
using Persistence.Repositories.Writers;

namespace Persistence.Repositories.Commons
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TemplateContext _context;
        private readonly CategoryReadRepository _categoryReadRepository;
        private readonly CategoryWriteRepository _categoryWriteRepository;
        private readonly ArticleReadRepository _articleReadRepository;
        private readonly ArticleWriteRepository _articleWriteRepository;
        private readonly UserWriteRepository _userWriteRepository;
        private readonly UserReadRepository _userReadRepository;
        private readonly RoleReadRepository _roleReadRepository;
        private readonly RoleWriteRepository _roleWriteRepository;
        private readonly UserRoleReadRepository _userRoleReadRepository;
        private readonly UserRoleWriteRepository _userRoleWriteRepository;
        private readonly WriterReadRepository _writerReadRepository;
        private readonly WriterWriteRepository _writerWriteRepository;
        private readonly ArticleFavoriteReadRepository _articleFavoriteReadRepository;
        private readonly ArticleFavoriteWriteRepository _articleFavoriteWriteRepository;
        public UnitOfWork(TemplateContext templateContext)
        {
            _context = templateContext;
        }

        public ICategoryReadRepository CategoryReadRepository => _categoryReadRepository ?? new CategoryReadRepository(_context);

        public ICategoryWriteRepository CategoryWriteRepository => _categoryWriteRepository ?? new CategoryWriteRepository(_context);

        public IArticleReadRepository ArticleReadRepository => _articleReadRepository ?? new ArticleReadRepository(_context);

        public IArticleWriteRepository ArticleWriteRepository => _articleWriteRepository ?? new ArticleWriteRepository(_context);

        public IUserReadRepository UserReadRepository => _userReadRepository ?? new UserReadRepository(_context);

        public IUserWriteRepository UserWriteRepository => _userWriteRepository ?? new UserWriteRepository(_context);

        public IRoleReadRepository RoleReadRepository => _roleReadRepository ?? new RoleReadRepository(_context);

        public IRoleWriteRepository RoleWriteRepository => _roleWriteRepository ?? new RoleWriteRepository(_context);

        public IUserRoleReadRepository UserRoleReadRepository => _userRoleReadRepository ?? new UserRoleReadRepository(_context);

        public IUserRoleWriteRepository UserRoleWriteRepository => _userRoleWriteRepository ?? new UserRoleWriteRepository(_context);

        public IWriterReadRepository WriterReadRepository => _writerReadRepository ?? new WriterReadRepository(_context);

        public IWriterWriteRepository WriterWriteRepository => _writerWriteRepository ?? new WriterWriteRepository(_context);

        public IArticleFavoriteReadRepository ArticleFavoriteReadRepository => _articleFavoriteReadRepository ?? new ArticleFavoriteReadRepository(_context);

        public IArticleFavoriteWriteRepository ArticleFavoriteWriteRepository => _articleFavoriteWriteRepository ?? new ArticleFavoriteWriteRepository(_context);

        public IDatabaseTransaction BeginTransaction() => 
            new DatabaseTransaction(_context);

        public void Dispose() => _context.Dispose();

        public async ValueTask DisposeAsync() => await _context.DisposeAsync();

        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
