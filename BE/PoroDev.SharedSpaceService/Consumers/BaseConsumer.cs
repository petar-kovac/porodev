using AutoMapper;
using MassTransit;
using PoroDev.SharedSpaceService.Services.Contracts;

namespace PoroDev.SharedSpaceService.Consumers
{

    public abstract class BaseConsumer<T> : IConsumer<T> where T : class, new()
    {
        protected readonly ISharedSpaceService _sharedSpaceService;
        protected readonly IMapper _mapper;

        public BaseConsumer(ISharedSpaceService sharedSpaceService, IMapper mapper)
        {
            _sharedSpaceService = sharedSpaceService;
            _mapper = mapper;
        }

        public virtual Task Consume(ConsumeContext<T> context)
        {
            throw new NotImplementedException();
        }
    }
}