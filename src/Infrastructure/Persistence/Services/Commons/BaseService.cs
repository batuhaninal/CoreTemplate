using Application.Abstractions.Commons.Caching;
using Application.Abstractions.Repositories.Commons;
using AutoMapper;

namespace Persistence.Services.Commons
{
    public class BaseService
    {
        public IUnitOfWork UnitOfWork { get; }
        public IMapper Mapper { get; }
        public ICacheService Cache { get; }

        public BaseService(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cache)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
            Cache = cache;
        }
    }
}
