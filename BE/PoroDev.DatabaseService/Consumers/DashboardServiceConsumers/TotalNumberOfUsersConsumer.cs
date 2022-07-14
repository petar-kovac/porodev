using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.DashboardService.TotalNumberOfUsers;
using PoroDev.Common.Models.StorageModels.Data;
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
            List<DataUserModel> users = (await _unitOfWork.Users.FindAllAsync(users => users.Email.Contains(""))).ToList<DataUserModel>();
            TotalNumberOfUsersModel returnModel = new TotalNumberOfUsersModel();
            returnModel._numberOfUsers = users.Count;

            var response = new CommunicationModel<TotalNumberOfUsersModel>()
            {
                Entity = returnModel,
                ExceptionName = null,
                HumanReadableMessage = null
            };

            await context.RespondAsync<CommunicationModel<TotalNumberOfUsersModel>>(response);
        }
    }
}
