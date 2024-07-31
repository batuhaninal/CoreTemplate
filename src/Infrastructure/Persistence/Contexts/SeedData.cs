using Application.Abstractions.Commons.Security;
using Application.Models.Constants.Roles;
using Bogus;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Persistence.Contexts
{
    public class SeedData
    {
        private readonly IHashingService _hashingService;

        public SeedData(IHashingService hashingService)
        {
            _hashingService = hashingService;
        }

        private  List<User> GetWriterUserSeeds()
        {
            byte[] passwordHash, passwordSalt;
            _hashingService.CreatePasswordHash("12345", out passwordHash, out passwordSalt);


            return new Faker<User>("tr")
                .RuleFor(x => x.Id, i => Guid.NewGuid())
                .RuleFor(x => x.CreatedDate, i => i.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now))
                .RuleFor(x => x.FirstName, i => i.Person.FirstName)
                .RuleFor(x => x.LastName, i => i.Person.LastName)
                .RuleFor(x => x.Email, i => i.Internet.Email())
                .RuleFor(x => x.PasswordHash, i => passwordHash)
                .RuleFor(x => x.PasswordSalt, i => passwordSalt)
                .RuleFor(x => x.IsActive, i => i.PickRandom(true, false))
                .Generate(500);
        }

        public async Task SeedAsync(IConfiguration configuration)
        {
            var dbContextBuilder = new DbContextOptionsBuilder();
            dbContextBuilder.UseNpgsql(configuration.GetConnectionString("PgSql"));

            var context = new TemplateContext(dbContextBuilder.Options, _hashingService);

            if (context.Articles.Count() > 500)
            {
                await Task.CompletedTask;
                return;
            }

            var writerUsers = GetWriterUserSeeds();
            var userIds = writerUsers.Select(x=> x.Id).ToArray();
            int counterWriterUsers = 0;

            await context.Users.AddRangeAsync(writerUsers);

            var userRoles = new Faker<UserRole>()
                .RuleFor(x => x.Id, i => Guid.NewGuid())
                .RuleFor(x => x.CreatedDate, i => i.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now))
                .RuleFor(x => x.UserId, i => userIds[counterWriterUsers++])
                .RuleFor(x => x.RoleId, i => Guid.Parse(AppRoles.Writer))
                .RuleFor(x => x.IsActive, i => i.PickRandom(true, false))
                .Generate(500);

            await context.UserRoles.AddRangeAsync(userRoles);

            counterWriterUsers = 0;

            var writers = new Faker<Writer>()
                .RuleFor(x => x.Id, i => Guid.NewGuid())
                .RuleFor(x => x.CreatedDate, i => i.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now))
                .RuleFor(x => x.UserId, i => userIds[counterWriterUsers++])
                .RuleFor(x => x.Nick, i => i.Internet.UserName())
                .RuleFor(x => x.Level, i => (byte)i.PickRandom(0,4))
                .RuleFor(x => x.IsActive, i => i.PickRandom(true, false))
                .Generate(500);

            var writerIds = writers.Select(x => x.Id);

            await context.Writers.AddRangeAsync(writers);


            Guid writerId = Guid.Parse("7e137c28-9868-4e00-b2bd-73ab46e43bc2");

            var categoryIds = Enumerable.Range(0, 250).Select(x => Guid.NewGuid()).ToArray();
            int counter = 0;
            var categories = new Faker<Category>()
                .RuleFor(x => x.Id, i => categoryIds[counter++])
                .RuleFor(x => x.Title, i => i.Lorem.Sentence(i.PickRandom(1, 5)))
                .RuleFor(x => x.CreatedDate, i => i.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now))
                .RuleFor(x => x.IsActive, i => i.PickRandom(true, false))
                .Generate(250);

            await context.Categories.AddRangeAsync(categories);

            var articles = new Faker<Article>("tr")
                .RuleFor(x => x.Id, i => Guid.NewGuid())
                .RuleFor(x => x.CreatedDate, i => i.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now))
                .RuleFor(x => x.Title, i => i.Lorem.Sentence(5, 5))
                .RuleFor(x => x.Content, i => i.Lorem.Paragraph(2))
                .RuleFor(x => x.IsActive, i => i.PickRandom(true, false))
                .RuleFor(x => x.CategoryId, i => i.PickRandom(categoryIds))
                .RuleFor(x => x.WriterId, i => writerId)
                .RuleFor(x => x.LikeCount, i => 0)
                .RuleFor(x => x.FavCount, i => 0)
                .Generate(500);

            var articlesRandomWriters = new Faker<Article>("tr")
                .RuleFor(x => x.Id, i => Guid.NewGuid())
                .RuleFor(x => x.CreatedDate, i => i.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now))
                .RuleFor(x => x.Title, i => i.Lorem.Sentence(5, 5))
                .RuleFor(x => x.Content, i => i.Lorem.Paragraph(2))
                .RuleFor(x => x.IsActive, i => i.PickRandom(true, false))
                .RuleFor(x => x.CategoryId, i => i.PickRandom(categoryIds))
                .RuleFor(x => x.WriterId, i => i.PickRandom(writerIds))
                .Generate(5000);

            articles.AddRange(articlesRandomWriters);
            await context.Articles.AddRangeAsync(articles);


            await context.SaveChangesAsync();

        }
    }
}
