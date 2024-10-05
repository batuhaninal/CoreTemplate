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

        public async Task CheckWriterExistById(Guid writerId)
        {
            bool result = await _writerReadRepository.AnyAsync(x=> x.Id == writerId);
            if (!result)
                throw new NotFoundException("Writer");
        }

        public async Task CheckUserIdAvailable(Guid userId)
        {
            bool result = await _writerReadRepository.AnyAsync(x=> x.UserId == userId);
            if(result)
                throw new DuplicateException("User Id", userId.ToString());
        }
    }
}
