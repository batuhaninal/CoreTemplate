using Application.Models.RequestParameters.Articles;
using Application.Utilities.Helpers;
using Domain.Entities;

namespace Persistence.Repositories.Users.Extensions
{
    public static class UserFilterExtensions
    {
        public static IQueryable<User> Filter(this IQueryable<User> source, ArticleRequestParameter parameter)
        {
            var predicate = PredicateBuilderHelper.True<User>();

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

        public static IQueryable<User> Search(this IQueryable<User> source, string? condition)
        {
            if (string.IsNullOrWhiteSpace(condition))
                return source;

            string normalizedCondition = condition.TrimStart().TrimEnd().ToUpper();

            return source.Where(s =>
                s.FirstName.ToUpper().Contains(normalizedCondition) || 
                s.LastName.ToUpper().Contains(normalizedCondition) || 
                s.Email.ToUpper().Contains(normalizedCondition) || 
                string.Join(' ', s.FirstName.ToUpper(), s.LastName.ToUpper()).Contains(normalizedCondition)
            );
        }

        public static IQueryable<User> OrderQuery(this IQueryable<User> source, string? orderBy)
        {
            if (string.IsNullOrWhiteSpace(orderBy))
                return source;

            switch (orderBy.Trim())
            {
                case "name":
                    source = source.OrderBy(x => x.FirstName);
                    break;
                case "name_desc":
                    source = source.OrderByDescending(x => x.FirstName);
                    break;
                case "fname":
                    source = source.OrderBy(x => string.Join(' ', x.FirstName, x.LastName));
                    break;
                case "fname_desc":
                    source = source.OrderByDescending(x => string.Join(' ', x.FirstName, x.LastName));
                    break;
                case "lname":
                    source = source.OrderBy(x => x.LastName);
                    break;
                case "lname_desc":
                    source = source.OrderByDescending(x => x.LastName);
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
