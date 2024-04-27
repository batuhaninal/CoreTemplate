using Application.Abstractions.Repositories.Categories;
using Application.Abstractions.Repositories.Commons;
using Application.Abstractions.Repositories.Products;
using Application.Abstractions.Repositories.Roles;
using Application.Abstractions.Repositories.UserRoles;
using Application.Abstractions.Repositories.Users;
using Persistence.Contexts;
using Persistence.Repositories.Categories;
using Persistence.Repositories.Products;
using Persistence.Repositories.Roles;
using Persistence.Repositories.UserRoles;
using Persistence.Repositories.Users;

namespace Persistence.Repositories.Commons
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TemplateContext _context;
        private CategoryReadRepository _categoryReadRepository;
        private CategoryWriteRepository _categoryWriteRepository;
        private ProductReadRepository _productReadRepository;
        private ProductWriteRepository _productWriteRepository;
        private UserWriteRepository _userWriteRepository;
        private UserReadRepository _userReadRepository;
        private RoleReadRepository _roleReadRepository;
        private RoleWriteRepository _roleWriteRepository;
        private UserRoleReadRepository _userRoleReadRepository;
        private UserRoleWriteRepository _userRoleWriteRepository;
        public UnitOfWork(TemplateContext templateContext)
        {
            _context = templateContext;
        }

        public ICategoryReadRepository CategoryReadRepository => _categoryReadRepository ?? new CategoryReadRepository(_context);

        public ICategoryWriteRepository CategoryWriteRepository => _categoryWriteRepository ?? new CategoryWriteRepository(_context);

        public IProductReadRepository ProductReadRepository => _productReadRepository ?? new ProductReadRepository(_context);

        public IProductWriteRepository ProductWriteRepository => _productWriteRepository ?? new ProductWriteRepository(_context);

        public IUserReadRepository UserReadRepository => _userReadRepository ?? new UserReadRepository(_context);

        public IUserWriteRepository UserWriteRepository => _userWriteRepository ?? new UserWriteRepository(_context);

        public IRoleReadRepository RoleReadRepository => _roleReadRepository ?? new RoleReadRepository(_context);

        public IRoleWriteRepository RoleWriteRepository => _roleWriteRepository ?? new RoleWriteRepository(_context);

        public IUserRoleReadRepository UserRoleReadRepository => _userRoleReadRepository ?? new UserRoleReadRepository(_context);

        public IUserRoleWriteRepository UserRoleWriteRepository => _userRoleWriteRepository ?? new UserRoleWriteRepository(_context);

        public IDatabaseTransaction BeginTransaction() => 
            new DatabaseTransaction(_context);

        public void Dispose() => _context.Dispose();

        public async ValueTask DisposeAsync() => await _context.DisposeAsync();

        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
