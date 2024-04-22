using Application.Abstractions.Repositories.Categories;
using Domain.Entities;
using Persistence.Contexts;
using Persistence.Repositories.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Categories
{
    public class CategoryReadRepository(TemplateContext context) : ReadRepository<Category>(context), ICategoryReadRepository
    {
    }
}
