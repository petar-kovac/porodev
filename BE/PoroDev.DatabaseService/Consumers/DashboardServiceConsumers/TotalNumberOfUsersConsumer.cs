using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.DashboardService.TotalNumberOfUsers;
using PoroDev.Common.Exceptions;
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
            DataUserModel user = await _unitOfWork.Users.GetByIdAsync(context.Message.UserId);

            if(user.Role == 0)
            {
                List<DataUserModel> users = (await _unitOfWork.Users.FindAllAsync(users => users.Email.Contains(""))).ToList<DataUserModel>();
                TotalNumberOfUsersModel returnModel = new TotalNumberOfUsersModel();
                returnModel.NumberOfUsers = users.Count; 

                var response = new CommunicationModel<TotalNumberOfUsersModel>()
                {
                    Entity = returnModel,
                    ExceptionName = null,
                    HumanReadableMessage = null
                };

                await context.RespondAsync<CommunicationModel<TotalNumberOfUsersModel>>(response);
            }
            else
            {
                string exceptionType = nameof(UserIsNotAdminException);
                string humanReadableMessage = "User must be admin!";

                var resposneException = new CommunicationModel<TotalNumberOfUsersModel>()
                {
                    Entity = null,
                    ExceptionName = exceptionType,
                    HumanReadableMessage = humanReadableMessage
                };

                await context.RespondAsync(resposneException);
            }
        }
    }
}
