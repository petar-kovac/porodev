using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.DashboardService.TotalNumberOfUsers;
using PoroDev.Common.Exceptions;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.DatabaseService.Repositories.Contracts;

namespace PoroDev.DatabaseService.Consumers.DashboardServiceConsumers
{
    public class TotalNumberOfUsersConsumer : IConsumer<TotalNumberOfUsersRequestServiceToDatabase>
    {
        private IUnitOfWork _unitOfWork;

        public TotalNumberOfUsersConsumer(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Consume(ConsumeContext<TotalNumberOfUsersRequestServiceToDatabase> context)
        {
            var responseModel = await TotalNumberOfUsers(context.Message);

            await context.RespondAsync<CommunicationModel<TotalNumberOfUsersModel>>(responseModel);
        }

        private async Task<CommunicationModel<TotalNumberOfUsersModel>> TotalNumberOfUsers(TotalNumberOfUsersRequestServiceToDatabase totalNumberOfUsers)
        {
            DataUserModel admin = await _unitOfWork.Users.GetByIdAsync(totalNumberOfUsers.UserId);

            if (admin.Role != 0)
            {
                return new CommunicationModel<TotalNumberOfUsersModel>(new UserIsNotAdminException());
            }

            List<DataUserModel> users = (await _unitOfWork.Users.FindAllAsync(users => users.Email.Contains(""))).ToList<DataUserModel>();
            TotalNumberOfUsersModel returnModel = new TotalNumberOfUsersModel();
            returnModel.NumberOfUsers = users.Count;

            var responseTotalNumberOfUsers = new CommunicationModel<TotalNumberOfUsersModel>()
            {
                Entity = returnModel,
                ExceptionName = null,
                HumanReadableMessage = null
            };

            return responseTotalNumberOfUsers;
        }
    }
}