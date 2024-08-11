using Application.Models.RequestParameters.Categories;
using Application.Utilities.Helpers;
using Domain.Entities;

namespace Persistence.Repositories.Categories.Extensions
{
    public static class CategoryFilterExtensions
    {
        public static IQueryable<Category> Filter(this IQueryable<Category> source, CategoryRequestParameter parameter)
        {
            var predicate = PredicateBuilderHelper.True<Category>();

            if (parameter.IsActive is not null)
                predicate = predicate.And(x => x.IsActive == parameter.IsActive);

            if (parameter.MinDate is not null)
                predicate = predicate.And(x => x.CreatedDate >= parameter.MinDate);

            if (parameter.MaxDate is not null)
                predicate = predicate.And(x => x.CreatedDate <= parameter.MaxDate);

            source = source.Where(predicate);

            source = source.Search(parameter.Condition);

            return source.OrderQuery(parameter.OrderBy);
        }

        public static IQueryable<Category> Search(this IQueryable<Category> source, string? condition)
        {
            if (string.IsNullOrWhiteSpace(condition))
                return source;

            string normalizedCondition = condition.TrimStart().TrimEnd().ToUpper();

            return source.Where(s =>
                s.Title.ToUpper().Contains(normalizedCondition)
            );
        }

        public static IQueryable<Category> OrderQuery(this IQueryable<Category> source, string? orderBy)
        {
            if (string.IsNullOrWhiteSpace(orderBy))
                return source;

            switch (orderBy.Trim())
            {
                case "title":
                    source = source.OrderBy(x => x.Title);
                    break;
                case "title_desc":
                    source = source.OrderByDescending(x => x.Title);
                    break;
                case "created":
                    source = source.OrderBy(x => x.CreatedDate);
                    break;
                case "created_desc":
                    source = source.OrderByDescending(x => x.CreatedDate);
                    break;
                default:
                    source = source.OrderByDescending(x => x.CreatedDate);
                    break;
            }

            return source;
        }
    }
}
