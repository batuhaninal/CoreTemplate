using Application.Abstractions.Repositories.Writers;
using Domain.Entities;
using Persistence.Contexts;
using Persistence.Repositories.Commons;

namespace Persistence.Repositories.Writers
{
    public class WriterWriteRepository : WriteRepository<Writer>, IWriterWriteRepository
    {
        public WriterWriteRepository(TemplateContext context) : base(context)
        {
        }
    }
}
