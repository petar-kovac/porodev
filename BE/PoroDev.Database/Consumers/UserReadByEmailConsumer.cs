using MassTransit;
using PoroDev.Common.Contracts.ReadUser;
using PoroDev.Common.Extensions;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.Database.Repositories.Contracts;

namespace PoroDev.Database.Consumers
{
    public class UserReadByEmailConsumer : IConsumer<UserReadByEmailRequestServiceToDatabase>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserReadByEmailConsumer(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Consume(ConsumeContext<UserReadByEmailRequestServiceToDatabase> context)
        {
            var fetchUser = await _unitOfWork.Users.FindSingleAsync(user => user.Email.Equals(context.Message.Email));

            var returnUser = CreateResponse<UserReadByEmailResponseDatabaseToService, DataUserModel>.CreateResponseModel(fetchUser);

            await context.RespondAsync<UserReadByEmailResponseDatabaseToService>(returnUser);
        }
    }
}