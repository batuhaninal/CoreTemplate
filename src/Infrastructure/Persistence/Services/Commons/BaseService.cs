using Application.Abstractions.Repositories.Commons;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Services.Commons
{
    public class BaseService
    {
        public IUnitOfWork UnitOfWork { get; }
        public IMapper Mapper { get; }

        public BaseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
        }
    }
}
