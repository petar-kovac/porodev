using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.BillingReport.TotalDownload;
using PoroDev.Common.Exceptions;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.DatabaseService.Repositories.Contracts;
using static PoroDev.Common.Constants.Constants;

namespace PoroDev.DatabaseService.Consumers.BillingReportConsumers
{
    public class TotalDownloadConsumer : IConsumer<TotalDownloadRequestServiceToDatabase>
    {
        private IUnitOfWork _unitOfWork;

        public TotalDownloadConsumer(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private async Task<CommunicationModel<TotalDownloadResponse>> TotalDownload(TotalDownloadRequestServiceToDatabase totalDownload)
        {
            DataUserModel admin = await _unitOfWork.Users.GetByIdAsync(totalDownload.AdminId);

            if (admin.Role != 0)
                return new CommunicationModel<TotalDownloadResponse>(new UserPermissionException());

            var user = await _unitOfWork.Users.GetByIdAsync(totalDownload.UserId);
            var totalDownloadSize = Convert.ToDouble(user.FileDownloadTotal / 1024.0);
            var price = totalDownloadSize * PRICE_PER_MB;

            TotalDownloadResponse downloadSize = new()
            {
                DownloadSize = totalDownloadSize,
                DownloadPrice = price
            };

            return new CommunicationModel<TotalDownloadResponse>(downloadSize);
        }

        public async Task Consume(ConsumeContext<TotalDownloadRequestServiceToDatabase> context)
        {
            var respondModel = await TotalDownload(context.Message);

            await context.RespondAsync(respondModel);
        }
    }
}