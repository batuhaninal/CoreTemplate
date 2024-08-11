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

            string normalizedCondition = condition.TrimStart().TrimEnd().ToUpper();

            return source.Where(s =>
                s.Nick.ToUpper().Contains(normalizedCondition) ||
                s.User!.FirstName.ToUpper().Contains(normalizedCondition) ||
                s.User.LastName.ToUpper().Contains(normalizedCondition) ||
                s.User.Email.ToUpper().Contains(normalizedCondition) ||
                (s.User != null ? string.Join(' ', s.User.FirstName.ToUpper(), s.User.LastName.ToUpper()).Contains(normalizedCondition) : 1 == 0)
            );
        }

        public static IQueryable<Writer> OrderQuery(this IQueryable<Writer> source, string? orderBy)
        {
            if (string.IsNullOrWhiteSpace(orderBy))
                return source;

            switch (orderBy.Trim())
            {
                case "nick":
                    source = source.OrderBy(x => x.Nick);
                    break;
                case "nick_desc":
                    source = source.OrderByDescending(x => x.Nick);
                    break;
                case "name":
                    source = source.OrderBy(x => x.User!.FirstName);
                    break;
                case "name_desc":
                    source = source.OrderByDescending(x => x.User!.FirstName);
                    break;
                case "fname":
                    source = source.OrderBy(x => string.Join(' ', x.User!.FirstName, x.User.LastName));
                    break;
                case "fname_desc":
                    source = source.OrderByDescending(x => string.Join(' ', x.User!.FirstName, x.User.LastName));
                    break;
                case "lname":
                    source = source.OrderBy(x => x.User!.LastName);
                    break;
                case "lname_desc":
                    source = source.OrderByDescending(x => x.User!.LastName);
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
