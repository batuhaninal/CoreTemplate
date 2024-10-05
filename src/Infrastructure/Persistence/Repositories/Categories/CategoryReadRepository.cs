using Application.Abstractions.Repositories.Categories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Persistence.Repositories.Commons;

namespace Persistence.Repositories.Categories
{
    public class CategoryReadRepository : ReadRepository<Category>, ICategoryReadRepository
    {
        private readonly TemplateContext _context;
        public CategoryReadRepository(TemplateContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Guid>?> GetAllChildrensId(Guid categoryId)
        {
            List<Guid> result = new();

            var firstChildIds = await _context.Categories
                .AsNoTracking()
                .Where(x => x.Id == categoryId)
                .Include(x => x.Childrens)
                .SelectMany(x => x.Childrens.Select(x => x.Id))
                .ToListAsync();

            if(firstChildIds is null || !firstChildIds.Any())
                return result;

            result.Add(categoryId);
            result.AddRange(firstChildIds);

            //await RecursiveChildIds(result);

            foreach (var item in firstChildIds)
            {
                await GetChildrensRecursive(item, result);
            }

            return result;
        }

        public async Task<List<Guid>?> GetAllChildrensId(Guid categoryId, bool isActive)
        {
            List<Guid> result = new List<Guid>() { categoryId };

            var firstChildIds = await _context.Categories
                .AsNoTracking()
                .Where(x=> x.Id == categoryId)
                .Include(x => x.Childrens.Where(x=> x.IsActive == isActive))
                .SelectMany(x=> x.Childrens.Select(x=> x.Id))
                .ToListAsync();
            
            result.AddRange(firstChildIds);

            foreach (var item in firstChildIds)
            {
                await GetChildrensRecursive(item, result, isActive);
            }

            return result;
        }

        private async Task GetChildrensRecursive(Guid categoryId, List<Guid> data)
        {
            if(data is not null && data.Any())
            {
                var categoryIds = await _context.Categories
                    .AsNoTracking()
                    .Where(x => x.Id == categoryId)
                    .Include(x => x.Childrens)
                    .SelectMany(x => x.Childrens)
                    .Select(c => c.Id)
                    .ToListAsync();

                if(categoryIds is not null && categoryIds.Any())
                {
                    foreach (var item in categoryIds)
                    {
                        await GetChildrensRecursive(item, data);
                    }
                    data.AddRange(categoryIds);
                }
            }
        }

        private async Task GetChildrensRecursive(Guid categoryId, List<Guid> data, bool isActive)
        {
            if (data is null || !data.Any())
                return;

            if (data is not null && data.Any())
            {
                var categoryIds = await _context.Categories
                    .AsNoTracking()
                    .Where(x => x.Id == categoryId && x.IsActive == isActive)
                    //.Include(x => x.Childrens.Where(x => x.IsActive == isActive))
                    .SelectMany(x => x.Childrens.Where(x => x.IsActive == isActive).Select(c => c.Id))
                    .ToListAsync();

                if (categoryIds is not null && categoryIds.Any())
                {
                    foreach (var item in categoryIds)
                    {
                        await GetChildrensRecursive(item, data, isActive);
                    }
                    data.AddRange(categoryIds);
                }
            }
        }

        public async Task GetChildrenRecursiveAsync(Category category)
        {
            if (category.Childrens != null && category.Childrens.Any())
            {
                foreach (var child in category.Childrens)
                {
                    // Alt children'ları veritabanından çekmek için sorgu atıyoruz
                    var loadedChild = await _context.Categories
                        .AsNoTracking()
                        .Where(c => c.Id == child.Id)
                        .Include(c => c.Childrens) // Alt children'ları yüklüyoruz
                        .FirstOrDefaultAsync();

                    if (loadedChild != null && loadedChild.Childrens != null && loadedChild.Childrens.Any())
                    {
                        // Child kategorinin childrens listesini dolduruyoruz
                        await GetChildrenRecursiveAsync(loadedChild);

                        // Child'ı orijinal child'a ekliyoruz
                        child.Childrens = loadedChild.Childrens;
                    }
                }
            }
        }

        public async Task GetChildrenRecursiveAsync(Category category, bool isActive)
        {
            // Eğer mevcut kategorinin children'ları varsa devam ediyoruz
            if (category.Childrens != null && category.Childrens.Any())
            {
                // Sadece aktif olan child'lar üzerinde işlem yapıyoruz
                var activeChildren = category.Childrens.Where(c => c.IsActive == isActive).ToList();

                // Hiyerarşik yapı için her bir child üzerinde işlem
                foreach (var child in activeChildren)
                {
                    // Alt children'ları veritabanından çekmek için sorgu atıyoruz
                    var loadedChild = await _context.Categories
                        .AsNoTracking()
                        .Where(c => c.Id == child.Id && c.IsActive == isActive)  // Sadece aktif child
                        .Include(c => c.Childrens.Where(x => x.IsActive == isActive))  // Sadece aktif alt children'ları yüklüyoruz
                        .FirstOrDefaultAsync();

                    if (loadedChild != null && loadedChild.Childrens.Any())
                    {
                        // Özyineleme ile alt children'ları getiriyoruz
                        await GetChildrenRecursiveAsync(loadedChild, isActive);

                        // Child'ı orijinal child'a ekliyoruz, sadece aktif children'ları getiriyoruz
                        child.Childrens = loadedChild.Childrens.Where(x => x.IsActive == isActive).ToList();
                    }
                }

                // Son olarak kategorinin children listesini sadece aktif children'larla güncelliyoruz
                category.Childrens = activeChildren;
            }
        }
    }
}
