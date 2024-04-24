using Application.Abstractions.Repositories.Categories;
using Application.Abstractions.Repositories.Commons;
using Application.Utilities.Exceptions.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Services.Categories
{
    internal class CategoryBusinessRules
    {
        private ICategoryReadRepository _categoryReadRepository;
        public CategoryBusinessRules(IUnitOfWork unitOfWork)
        {
            _categoryReadRepository = unitOfWork.CategoryReadRepository;
        }

        public async Task CheckCategoryExist(string categoryId)
        {
            bool result = await _categoryReadRepository.AnyAsync(x=> x.Id == Guid.Parse(categoryId));
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
