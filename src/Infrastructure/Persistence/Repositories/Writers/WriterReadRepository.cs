using Application.Abstractions.Repositories.Writers;
using Domain.Entities;
using Persistence.Contexts;
using Persistence.Repositories.Commons;

namespace Persistence.Repositories.Writers
{
    public class WriterReadRepository : ReadRepository<Writer>, IWriterReadRepository
    {
        public WriterReadRepository(TemplateContext context) : base(context)
        {
        }
    }
}
