using AutoMapper;
using PoroDev.StorageService.Services.Contracts;

namespace PoroDev.StorageService.Consumers
{
    public abstract class ConsumerBase
    {
        protected readonly IStorageService _storageService;
        protected readonly IMapper _mapper;

        public ConsumerBase(IStorageService storageService, IMapper mapper)
        {
            _storageService = storageService;
            _mapper = mapper;
        }
    }
}
