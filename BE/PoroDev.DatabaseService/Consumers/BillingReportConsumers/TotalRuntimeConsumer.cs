using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.BillingReport.TotalRuntime;
using PoroDev.Common.Exceptions;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.Common.Models.UserReportsModels.Data;
using PoroDev.DatabaseService.Repositories.Contracts;
using static PoroDev.Common.Constants.Constants;
using static PoroDev.DatabaseService.Constants.Constants;
using static PoroDev.DatabaseService.Helpers.CurrentMonth;

namespace PoroDev.DatabaseService.Consumers.BillingReportConsumers
{
    public class TotalRuntimeConsumer : IConsumer<TotalRuntimeRequestServiceToDatabase>
    {
        private IUnitOfWork _unitOfWork;

        public TotalRuntimeConsumer(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private async Task<CommunicationModel<TotalRuntimeResponse>> TotalRuntime(TotalRuntimeRequestServiceToDatabase totalRuntime)
        {
            DataUserModel admin = await _unitOfWork.Users.GetByIdAsync(totalRuntime.AdminId);

            if (admin.Role != 0)
                return new CommunicationModel<TotalRuntimeResponse>(new UserPermissionException(UserIsNotAdminExceptionMessage));

            var user = await _unitOfWork.Users.GetByIdAsync(totalRuntime.UserId);

            if (user == null)
                return new CommunicationModel<TotalRuntimeResponse>(new UserNotFoundException(UserNotFoundExceptionMessage));

            if (totalRuntime.Month == GetMonthName())
            {
                var runtimeNumber = CreateRuntimeResponse(user.RuntimeTotal);

                return new CommunicationModel<TotalRuntimeResponse>(runtimeNumber);
            }

            List<UserReportsData> findUser = (await _unitOfWork.UserReports.FindAllAsync(
                user => user.CurrentUserId.Equals(totalRuntime.UserId) &&
                user.Month.Equals(totalRuntime.Month))).ToList();

            if (findUser == null)
                return new CommunicationModel<TotalRuntimeResponse>(new UserNotFoundException(UserNotFoundExceptionMessage));

            var totalRuntimeNumber = CreateRuntimeResponse(findUser.FirstOrDefault().RuntimeTotal);

            return new CommunicationModel<TotalRuntimeResponse>(totalRuntimeNumber);
        }

        private TotalRuntimeResponse CreateRuntimeResponse(int totalRuntimeNumber)
        {
            var runtimePrice = totalRuntimeNumber * PRICE_RUNTIME;

            TotalRuntimeResponse runtimeNumber = new()
            {
                RuntimeNumber = totalRuntimeNumber,
                RuntimePrice = runtimePrice
            };

            return runtimeNumber;
        }

        public async Task Consume(ConsumeContext<TotalRuntimeRequestServiceToDatabase> context)
        {
            var respondModel = await TotalRuntime(context.Message);

            await context.RespondAsync(respondModel);
        }
    }
}