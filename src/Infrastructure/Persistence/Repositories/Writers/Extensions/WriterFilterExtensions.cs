using System.Linq.Expressions;
using Application.Models.RequestParameters.Writers;
using Application.Utilities.Helpers;
using Domain.Entities;

namespace Persistence.Repositories.Writers.Extensions
{
    public static class WriterFilterExtensions
    {
        public static IQueryable<Writer> Filter(this IQueryable<Writer> source, WriterRequestParameter parameter)
        {
            var predicate = PredicateBuilderHelper.True<Writer>();

            if (parameter.IsActive is not null)
                predicate = predicate.And(x => x.IsActive == parameter.IsActive);

            if (parameter.MinDate is not null)
                predicate = predicate.And(x => x.CreatedDate >= parameter.MinDate);

            if (parameter.MaxDate is not null)
                predicate = predicate.And(x => x.CreatedDate <= parameter.MaxDate);

            if(parameter.Level.HasValue && parameter.Level.Value >= 0 && parameter.Level.Value <= 4)
                predicate = predicate.And(x=> x.Level == parameter.Level.Value);

            source = source.Where(predicate);

            source = source.Search(parameter.Condition);

            return source.OrderQuery(parameter.OrderBy);
        }

        public static IQueryable<Writer> Search(this IQueryable<Writer> source, string? condition)
        {
            if (string.IsNullOrWhiteSpace(condition))
                return source;

            string normalizedCondition = condition.TrimStart().TrimEnd().ToLower();

            return source.Where(s =>
                s.Nick.ToLower().Contains(normalizedCondition) ||
                s.User!.FirstName.ToLower().Contains(normalizedCondition) ||
                s.User.LastName.ToLower().Contains(normalizedCondition) ||
                s.User.Email.ToLower().Contains(normalizedCondition) ||
                (s.User != null ? string.Join(' ', s.User.FirstName.ToLower(), s.User.LastName.ToLower()).Contains(normalizedCondition) : 1 == 0)
            );
        }

        public static IQueryable<Writer> OrderQuery(this IQueryable<Writer> source, string? orderBy)
        {
            if (string.IsNullOrWhiteSpace(orderBy))
                return source;

            string normalizedConditiom = orderBy.TrimStart().TrimEnd().ToLower();

            string[] orderByQuery = orderBy.Split('_');

            Expression<Func<Writer, object>> keySelector = orderByQuery[0] switch 
            {
                "nick" => writer => writer.Nick,
                "name" => writer => string.Join(' ', writer.User!.FirstName, writer.User!.LastName),
                // "fname" => writer => writer.User!.FirstName,
                // "lname" => writer => writer.User!.LastName,
                "email" => writer => writer.User!.Email,
                "created" => writer => writer.CreatedDate,
                _ => writer => writer.Id
            };

            if(normalizedConditiom.Contains("_desc"))
                source = source.OrderByDescending(keySelector);
            else
                source = source.OrderBy(keySelector); 

            return source;
        }
    }
}
