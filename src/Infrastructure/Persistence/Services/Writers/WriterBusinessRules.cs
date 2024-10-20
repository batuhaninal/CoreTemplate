using Application.Abstractions.Repositories.UserWriterFavorites;
using Application.Abstractions.Repositories.Writers;
using Application.Utilities.Exceptions.Commons;

namespace Persistence.Services.Writers
{
    public class WriterBusinessRules
    {
        private readonly IWriterReadRepository _writerReadRepository;
        private readonly IUserWriterFavoriteReadRepository _userWriterFavoriteReadRepository;

        public WriterBusinessRules(IWriterReadRepository writerReadRepository, IUserWriterFavoriteReadRepository userWriterFavoriteReadRepository)
        {
            _writerReadRepository = writerReadRepository;
            _userWriterFavoriteReadRepository = userWriterFavoriteReadRepository;
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

        public async Task CheckWriterAlreadyFavorited(Guid writerId, Guid userId)
        {
            bool result = await _userWriterFavoriteReadRepository.AnyAsync(x=> x.WriterId == writerId && x.UserId == userId);
            if (result)
                throw new BusinessException("Writer already favorited!");
        }
    }
}
