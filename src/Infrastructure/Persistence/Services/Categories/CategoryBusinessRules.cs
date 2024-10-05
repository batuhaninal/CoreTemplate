using Application.Abstractions.Repositories.Categories;
using Application.Utilities.Exceptions.Commons;

namespace Persistence.Services.Categories
{
    internal class CategoryBusinessRules
    {
        private ICategoryReadRepository _categoryReadRepository;
        public CategoryBusinessRules(ICategoryReadRepository categoryReadRepository)
        {
            _categoryReadRepository = categoryReadRepository;
        }

        public async Task CheckCategoryExist(Guid categoryId)
        {
            bool result = await _categoryReadRepository.AnyAsync(x=> x.Id == categoryId);
            if (!result)
                throw new NotFoundException("Category");
        }

        public async Task CheckTitleDuplicate(string title)
        {
            bool result = await _categoryReadRepository.AnyAsync(x=> x.Title.ToLower() == title.ToLower());
            if (result)
                throw new DuplicateException("Title", title);
        }
    }
}
