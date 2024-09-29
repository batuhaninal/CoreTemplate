using System.Linq.Expressions;
using Application.Models.RequestParameters.Articles;
using Application.Utilities.Helpers;
using Domain.Entities;

namespace Persistence.Repositories.Articles.Extensions
{
    public static class ArticleFilterExtensions
    {
        public static IQueryable<Article> FilterAllConditions(this IQueryable<Article> source, ArticleRequestParameter parameter)
        {
            var predicate = PredicateBuilderHelper.True<Article>();

            if (parameter.IsActive is not null)
                predicate = predicate.And(x => x.IsActive == parameter.IsActive);

            if(parameter.MinDate is not null)
                predicate = predicate.And(x => x.CreatedDate >= parameter.MinDate);

            if(parameter.MaxDate is not null)
                predicate = predicate.And(x=> x.CreatedDate <= parameter.MaxDate);

            source = source.Where(predicate);

            source = source.Search(parameter.Condition);

            return source.OrderQuery(parameter.OrderBy);
        }

        public static IQueryable<Article> Filter(this IQueryable<Article> source, ArticleRequestParameter parameter)
        {
            var predicate = PredicateBuilderHelper.True<Article>();

            if (parameter.IsActive is not null)
                predicate = predicate.And(x => x.IsActive == parameter.IsActive);

            if (parameter.MinDate is not null)
                predicate = predicate.And(x => x.CreatedDate >= parameter.MinDate);

            if (parameter.MaxDate is not null)
                predicate = predicate.And(x => x.CreatedDate <= parameter.MaxDate);

            return source.Where(predicate);
        }

        public static IQueryable<Article> Search(this IQueryable<Article> source, string? condition)
        {
            if (string.IsNullOrWhiteSpace(condition))
                return source;

            string normalizedCondition = condition.TrimStart().TrimEnd().ToLower();

            return source.Where(s=> 
                s.Title.ToLower().Contains(normalizedCondition) ||
                s.Writer!.Nick.ToLower().Contains(normalizedCondition) ||
                s.Writer!.User!.FirstName.ToLower().Contains(normalizedCondition) ||
                s.Writer.User.LastName.ToLower().Contains(normalizedCondition) ||
                s.Writer.User.Email.ToLower().Contains(normalizedCondition)
            );
        }

        public static IQueryable<Article> OrderQuery(this IQueryable<Article> source, string? orderBy)
        {
            if(string.IsNullOrWhiteSpace(orderBy))
                return source;

            string normalizedConditiom = orderBy.TrimStart().TrimEnd().ToLower();

            string[] orderByCondtion = orderBy.Split('_');

            Expression<Func<Article, object>> keySelector = orderByCondtion[0] switch 
            {
                "title" => article => article.Title,
                "created" => article => article.CreatedDate,
                _ => article=> article.Id
            };

            if(normalizedConditiom.Contains("_desc"))
                source = source.OrderByDescending(keySelector);
            else
                source = source.OrderBy(keySelector);

            // switch (orderBy.Trim())
            // {
            //     case "title":
            //         source = source.OrderBy(x => x.Title);
            //         break;
            //     case "title_desc":
            //         source = source.OrderByDescending(x => x.Title);
            //         break;
            //     case "created":
            //         source = source.OrderBy(x => x.CreatedDate);
            //         break;
            //     case "created_desc":
            //         source = source.OrderByDescending(x => x.CreatedDate);
            //         break;
            //     default:
            //         source = source.OrderByDescending(x => x.CreatedDate);
            //         break;
            // }

            return source;
        }
    }
}
