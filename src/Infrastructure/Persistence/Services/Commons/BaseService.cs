using Application.Abstractions.Commons.Caching;
using Application.Abstractions.Commons.MessageBrokers.Publishers;
using Application.Abstractions.Commons.Tokens;
using Application.Abstractions.Repositories.Commons;
using Application.Models.Constants.MessageBrokers;
using AutoMapper;

namespace Persistence.Services.Commons
{
    public abstract class BaseService
    {
        public IUnitOfWork UnitOfWork { get; }
        public IMapper Mapper { get; }
        public ICacheService Cache { get; }
        public IRabbitMQPublisherService Publisher { get; }
        public IUserTokenService UserTokenService { get; }
        public BaseService(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cache, IRabbitMQPublisherService publisher, IUserTokenService userTokenService)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
            Cache = cache;
            Publisher = publisher;
            UserTokenService = userTokenService;
        }
    }
}
