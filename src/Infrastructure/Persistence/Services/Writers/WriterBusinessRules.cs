using Application.Abstractions.Repositories.Writers;
using Application.Utilities.Exceptions.Commons;

namespace Persistence.Services.Writers
{
    public class WriterBusinessRules
    {
        private readonly IWriterReadRepository _writerReadRepository;

        public WriterBusinessRules(IWriterReadRepository writerReadRepository)
        {
            _writerReadRepository = writerReadRepository;
        }

        public async Task CheckNickAvailable(string nickName) 
        {
            bool result = await _writerReadRepository.AnyAsync(x=> x.Nick == nickName);
            if (result)
                throw new DuplicateException("Nick", nickName);
        }

        public async Task CheckWriterExistById(string writerId)
        {
            bool result = await _writerReadRepository.AnyAsync(x=> x.Id == Guid.Parse(writerId));
            if (!result)
                throw new NotFoundException("Writer");
        }
    }
}
