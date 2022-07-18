using AutoMapper;
using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.UserManagement.DeleteAllUsers;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.Common.Models.UserModels.DeleteUser;
using PoroDev.DatabaseService.Repositories.Contracts;

namespace PoroDev.DatabaseService.Consumers.UserConsumers
{
    public class UserDeleteAllConsumer : BaseDbConsumer, IConsumer<UserDeleteAllRequestServiceToDataBase>
    {
        public UserDeleteAllConsumer(IUnitOfWork unitOfWork, IMapper mapper, IFileRepository fileRepository) : base(unitOfWork, mapper, fileRepository)
        {
        }

        public async Task Consume(ConsumeContext<UserDeleteAllRequestServiceToDataBase> context)
        {
            CommunicationModel<DeleteUserModel> returnModel = new CommunicationModel<DeleteUserModel>();
            List<DataUserModel> allUsers = (await _unitOfWork.Users.FindAllAsync(user => user.Email.Contains(""))).ToList<DataUserModel>();

            foreach (var user in allUsers)
            {
                await _unitOfWork.Users.Delete(user);
            }
            await _unitOfWork.SaveChanges();

            await context.RespondAsync(returnModel);
        }
    }
}