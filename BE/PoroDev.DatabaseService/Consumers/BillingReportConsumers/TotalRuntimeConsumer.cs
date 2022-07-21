using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.BillingReport.TotalRuntime;
using PoroDev.Common.Exceptions;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.DatabaseService.Repositories.Contracts;
using static PoroDev.Common.Constants.Constants;

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
                return new CommunicationModel<TotalRuntimeResponse>(new UserPermissionException());

            var user = await _unitOfWork.Users.GetByIdAsync(totalRuntime.UserId);
            var totalRuntimeNumber = user.RuntimeTotal;
            var runtimePrice = totalRuntimeNumber * PRICE_RUNTIME;

            TotalRuntimeResponse runtimeNumber = new()
            {
                RuntimeNumber = totalRuntimeNumber,
                RuntimePrice = runtimePrice
            };

            return new CommunicationModel<TotalRuntimeResponse>(runtimeNumber);
        }

        public async Task Consume(ConsumeContext<TotalRuntimeRequestServiceToDatabase> context)
        {
            var respondModel = await TotalRuntime(context.Message);

            await context.RespondAsync(respondModel);
        }
    }
}