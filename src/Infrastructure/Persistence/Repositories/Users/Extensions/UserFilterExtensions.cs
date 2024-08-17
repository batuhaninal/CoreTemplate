using System.Linq.Expressions;
using Application.Models.RequestParameters.Users;
using Application.Utilities.Helpers;
using Domain.Entities;

namespace Persistence.Repositories.Users.Extensions
{
    public static class UserFilterExtensions
    {
        public static IQueryable<User> Filter(this IQueryable<User> source, UserRequestParameter parameter)
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

            
            string normalizedConditiom = orderBy.TrimStart().TrimEnd().ToLower();

            string[] orderByQUery = orderBy.Split('_');

            Expression<Func<User, object>> keySelector = orderByQUery[0] switch
            {
                "fname" => user => user.FirstName,
                "lname" => user => user.LastName,
                "email" => user => user.Email,
                "name" => user => string.Join(' ', user.FirstName, user.LastName),
                "created" => user => user.CreatedDate,
                _ => user => user.Id
            };

            if(normalizedConditiom.Contains("_desc"))
                source = source.OrderByDescending(keySelector);
            else
                source = source.OrderBy(keySelector);
            
            return source;
        }
    }
}
