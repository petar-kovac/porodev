using MassTransit;
using PoroDev.Common.Contracts.Create;
using PoroDev.Common.Models.UserModels.Create;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.Database.Repositories.Contracts;
using System.Reflection;

namespace PoroDev.Database.Consumers
{
    public class UserCreateConsumer : IConsumer<IUserCreateRequestServiceToDatabase>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserCreateConsumer(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Consume(ConsumeContext<IUserCreateRequestServiceToDatabase> context)
        {
            var model = context.Message;

            DataUserModel temp = (DataUserModel)context.Message;
           

            var createdModel = await _unitOfWork.Users.CreateAsync((DataUserModel)context.Message);
            await _unitOfWork.SaveChanges();

            UserCreateResponseDatabaseToService returnModel = new()
            {
                Entity = createdModel,
                ErrorName = null,
                ErrorMessage = null
            };

            await context.RespondAsync<IUserCreateResponseDatabaseToService>(returnModel);
        }
    }
}
