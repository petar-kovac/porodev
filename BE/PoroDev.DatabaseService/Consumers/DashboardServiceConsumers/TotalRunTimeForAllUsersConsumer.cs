using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.DashboardService.TotalRunTimeForAllUsers;
using PoroDev.Common.Contracts.DashboardService.TotalRunTimePerMonth;
using PoroDev.Common.Exceptions;
using PoroDev.Common.Models.RuntimeModels.Data;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.Common.Models.UserReportsModels.Data;
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
            var responseModel = await TotalRunTimeForAllUsers(context.Message);

            await context.RespondAsync<CommunicationModel<TotalRunTimeForAllUsersModel>>(responseModel);
        }

        private async Task<CommunicationModel<TotalRunTimeForAllUsersModel>> TotalRunTimeForAllUsers(
            TotalRunTimeForAllUsersRequestServiceToDatabase totalMemoryUsedForRunTimePerMonthModel)
        {
            DataUserModel admin = await _unitOfWork.Users.GetByIdAsync(totalMemoryUsedForRunTimePerMonthModel.UserId);
            List<RuntimeData> runTimeDataFiles = (await _unitOfWork.RuntimeData.FindAllAsync(userRuntime => userRuntime.User.Email.Contains(""))).ToList<RuntimeData>();

            if (admin.Role != 0)
            {
                return new CommunicationModel<TotalRunTimeForAllUsersModel>(new UserIsNotAdminException());
            }

            TotalRunTimeForAllUsersModel returnModel = new TotalRunTimeForAllUsersModel();
            returnModel.NumberOfTotalRunTimeForAllUsers = runTimeDataFiles.Count;

            var responseTotalMemoryUsedForUploadPerMonth = new CommunicationModel<TotalRunTimeForAllUsersModel>()
            {
                Entity = returnModel,
                ExceptionName = null,
                HumanReadableMessage = null
            };

            return responseTotalMemoryUsedForUploadPerMonth;
        }

    }
}