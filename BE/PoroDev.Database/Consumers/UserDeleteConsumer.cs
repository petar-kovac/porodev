using AutoMapper;
using MassTransit;
using PoroDev.Common.Contracts.DeleteUser;
using static PoroDev.Common.Extensions.CreateResponseExtension;
using PoroDev.Common.Models.UserModels.DeleteUser;
using PoroDev.Database.Repositories.Contracts;

namespace PoroDev.Database.Consumers
{
    public class UserDeleteConsumer : IConsumer<UserDeleteRequestServiceToDatabase>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserDeleteConsumer(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Consume(ConsumeContext<UserDeleteRequestServiceToDatabase> context)
        {
            var userToDelete = await _unitOfWork.Users.FindSingleAsync(user => user.Email.Equals(context.Message.Email));
            
            _unitOfWork.Users.Delete(userToDelete);
            await _unitOfWork.SaveChanges();

            DeleteUserModel deleteUserModel = new DeleteUserModel() { Deleted = true };

            var returnModel = CreateResponseModel<UserDeleteResponseDatabaseToService, DeleteUserModel>(deleteUserModel);
            await context.RespondAsync(returnModel);
        }
    }
}
