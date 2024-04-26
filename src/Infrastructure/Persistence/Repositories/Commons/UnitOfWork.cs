using Application.Abstractions.Repositories.Categories;
using Application.Abstractions.Repositories.Commons;
using Application.Abstractions.Repositories.Products;
using Persistence.Contexts;
using Persistence.Repositories.Categories;
using Persistence.Repositories.Products;

namespace Persistence.Repositories.Commons
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TemplateContext _context;
        private CategoryReadRepository _categoryReadRepository;
        private CategoryWriteRepository _categoryWriteRepository;
        private ProductReadRepository _productReadRepository;
        private ProductWriteRepository _productWriteRepository;

        public UnitOfWork(TemplateContext templateContext)
        {
            _context = templateContext;
        }

        public ICategoryReadRepository CategoryReadRepository => _categoryReadRepository ?? new CategoryReadRepository(_context);

        public ICategoryWriteRepository CategoryWriteRepository => _categoryWriteRepository ?? new CategoryWriteRepository(_context);

        public IProductReadRepository ProductReadRepository => _productReadRepository ?? new ProductReadRepository(_context);

        public IProductWriteRepository ProductWriteRepository => _productWriteRepository ?? new ProductWriteRepository(_context);

        public IDatabaseTransaction BeginTransaction() => 
            new DatabaseTransaction(_context);

        public void Dispose() => _context.Dispose();

        public async ValueTask DisposeAsync() => await _context.DisposeAsync();

        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
