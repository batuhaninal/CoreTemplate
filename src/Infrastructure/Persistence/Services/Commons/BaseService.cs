using Application.Abstractions.Commons.Caching;
using Application.Abstractions.Commons.MessageBrokers.Publishers;
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
        public BaseService(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cache, IRabbitMQPublisherService publisher)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
            Cache = cache;
            Publisher = publisher;
        }
    }
}
