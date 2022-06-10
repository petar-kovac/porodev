using AutoMapper;
using MassTransit;
using PoroDev.Common.Contracts.Update;
using PoroDev.Common.Extensions;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.Database.Repositories.Contracts;

namespace PoroDev.Database.Consumers
{
    public class UserUpdateConsumer : IConsumer<UserUpdateRequestServiceToDatabase>
    {
    
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserUpdateConsumer(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public Task Consume(ConsumeContext<UserUpdateRequestServiceToDatabase> context)
        {
            var model
            if(context.Message.Email == null)
            {
                var returnModel = UpdateResponse<UserUpdateResponseDatabaseToService DataUserModel>.UpdateResponseModel(createdModel);
            }
        }
    }
}
