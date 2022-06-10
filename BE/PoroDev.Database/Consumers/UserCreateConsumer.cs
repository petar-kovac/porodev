using AutoMapper;
using MassTransit;
using PoroDev.Common.Contracts.Create;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.Database.Repositories.Contracts;
using System.Reflection;

namespace PoroDev.Database.Consumers
{
    public class UserCreateConsumer : IConsumer<UserCreateRequestServiceToDatabase>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserCreateConsumer(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<UserCreateRequestServiceToDatabase> context)
        {
            var modelForDB = _mapper.Map<DataUserModel>(context.Message);
            var createdModel = await _unitOfWork.Users.CreateAsync(modelForDB);

            await _unitOfWork.SaveChanges();

            UserCreateResponseDatabaseToService returnModel = new()
            {
                Entity = createdModel,
                ExceptionName = null,
                HumanReadableMessage = null
            };

            await context.RespondAsync<UserCreateResponseDatabaseToService>(returnModel);
        }
    }
}
