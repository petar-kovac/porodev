using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.DashboardService.TotalRunTimeForAllUsers;
using PoroDev.Common.Exceptions;
using PoroDev.Common.Models.RuntimeModels.Data;
using PoroDev.Common.Models.StorageModels.Data;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.DatabaseService.Repositories.Contracts;

namespace PoroDev.DatabaseService.Consumers.DashboardServiceConsumers
{
    public class TotalRunTimeForAllUsersConsumer : IConsumer<TotalRunTimeForAllUsersRequestServiceToDatabase>
    {
        private IUnitOfWork _unitOfWork;

        public TotalRunTimeForAllUsersConsumer(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Consume(ConsumeContext<TotalRunTimeForAllUsersRequestServiceToDatabase> context)
        {
            DataUserModel user = await _unitOfWork.Users.GetByIdAsync(context.Message.UserId);

            if(user.Role == 0)
            {
                TotalRunTimeForAllUsersModel returnModel = new TotalRunTimeForAllUsersModel();
                returnModel.NumberOfTotalRunTimeForAllUsers = await countNumberOfRunTimeExecutions();

                var response = new CommunicationModel<TotalRunTimeForAllUsersModel>()
                {
                    Entity = returnModel,
                    ExceptionName = null,
                    HumanReadableMessage = null
                };

                await context.RespondAsync<CommunicationModel<TotalRunTimeForAllUsersModel>>(response);
            }
            else
            {
                string exceptionType = nameof(UserIsNotAdminException);
                string humanReadableMessage = "User must be admin!";

                var resposneException = new CommunicationModel<TotalRunTimeForAllUsersModel>()
                {
                    Entity = null,
                    ExceptionName = exceptionType,
                    HumanReadableMessage = humanReadableMessage
                };

                await context.RespondAsync(resposneException);
            }
        }

        public async Task<int> countNumberOfRunTimeExecutions()
        {
            List<RuntimeData> runTimeDataFiles = (await _unitOfWork.RuntimeData.FindAllAsync(userRuntime => userRuntime.User.Email.Contains(""))).ToList<RuntimeData>();
            
            int countTotalRunTimeForAllUsers = 0;
            countTotalRunTimeForAllUsers = runTimeDataFiles.Count;
            
            return countTotalRunTimeForAllUsers;
        }
    }
}
