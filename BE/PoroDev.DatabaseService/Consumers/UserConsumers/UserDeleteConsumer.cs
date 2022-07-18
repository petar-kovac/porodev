using AutoMapper;
using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.UserManagement.DeleteUser;
using PoroDev.Common.Models.UserModels.DeleteUser;
using PoroDev.DatabaseService.Repositories.Contracts;

namespace PoroDev.DatabaseService.Consumers.UserConsumers
{
    public class UserDeleteConsumer : BaseDbConsumer, IConsumer<UserDeleteRequestServiceToDatabase>
    {
        public UserDeleteConsumer(IUnitOfWork unitOfWork, IMapper mapper, IFileRepository fileRepository) : base(unitOfWork, mapper, fileRepository)
        {
        }

        public async Task Consume(ConsumeContext<UserDeleteRequestServiceToDatabase> context)
        {
            CommunicationModel<DeleteUserModel> returnModel;
            var userToDelete = await _unitOfWork.Users.FindAsync(user => user.Email.Equals(context.Message.Email.Trim()));

            if (userToDelete.ExceptionName != null)
            {
                returnModel = _mapper.Map<CommunicationModel<DeleteUserModel>>(userToDelete);
                await context.RespondAsync(returnModel);
                return;
            }

            var deletedUser = await _unitOfWork.Users.Delete(userToDelete.Entity);
            await _unitOfWork.SaveChanges();

            returnModel = _mapper.Map<CommunicationModel<DeleteUserModel>>(deletedUser);

            await context.RespondAsync(returnModel);
        }
    }
}