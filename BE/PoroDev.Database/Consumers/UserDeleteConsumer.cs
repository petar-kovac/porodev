using AutoMapper;
using MassTransit;
using PoroDev.Common.Contracts.DeleteUser;
using static PoroDev.Common.Extensions.CreateResponseExtension;
using PoroDev.Common.Models.UserModels.DeleteUser;
using PoroDev.Database.Repositories.Contracts;
using PoroDev.Common.Exceptions;
using static PoroDev.Database.Constants.Constants;
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
            UserDeleteResponseDatabaseToService returnModel;
            var userToDelete = await _unitOfWork.Users.FindAsync(user => user.Email.Equals(context.Message.Email.Trim()));

            if(userToDelete == null)
            {
                returnModel = CreateResponseModel<UserDeleteResponseDatabaseToService, DeleteUserModel>(nameof(UserNotFoundException), UserNotFoundExceptionMessage);
                await context.RespondAsync(returnModel);
            }


            try
            {
                _unitOfWork.Users.Delete(userToDelete);
                await _unitOfWork.SaveChanges();
            }
            catch (Exception exception)
            {
                returnModel = CreateResponseModel<UserDeleteResponseDatabaseToService, DeleteUserModel>(nameof(exception), InternalDatabaseError);
                await context.RespondAsync(returnModel);
            }

            DeleteUserModel deleteUserModel = new DeleteUserModel() { Deleted = true };

            returnModel = CreateResponseModel<UserDeleteResponseDatabaseToService, DeleteUserModel>(deleteUserModel);
            await context.RespondAsync(returnModel);
        }
    }
}
