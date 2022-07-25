using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.BillingReport.TotalDownload;
using PoroDev.Common.Exceptions;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.Common.Models.UserReportsModels.Data;
using PoroDev.DatabaseService.Repositories.Contracts;
using static PoroDev.Common.Constants.Constants;
using static PoroDev.DatabaseService.Constants.Constants;
using static PoroDev.DatabaseService.Helpers.CurrentMonth;

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
                return new CommunicationModel<TotalDownloadResponse>(new UserPermissionException(UserIsNotAdminExceptionMessage));

            var user = await _unitOfWork.Users.GetByIdAsync(totalDownload.UserId);

            if (user == null)
                return new CommunicationModel<TotalDownloadResponse>(new UserNotFoundException(UserNotFoundExceptionMessage));

            if (totalDownload.Month == GetMonthName())
            {
                var totalDownloadSize = Convert.ToDouble(user.FileDownloadTotal / 1024.0);
                var downloadSize = CreateDownloadResponse(totalDownloadSize);

                return new CommunicationModel<TotalDownloadResponse>(downloadSize);
            }

            List<UserReportsData> findUser = (await _unitOfWork.UserReports.FindAllAsync(
                user => user.CurrentUserId.Equals(totalDownload.UserId) &&
                user.Month.Equals(totalDownload.Month))).ToList<UserReportsData>();

            if (findUser == null)
                return new CommunicationModel<TotalDownloadResponse>(new UserNotFoundException(UserNotFoundExceptionMessage));

            var totalSizeDownload = Convert.ToDouble(findUser.FirstOrDefault().FileDownloadTotal / 1024.0);
            var sizeDownload = CreateDownloadResponse(totalSizeDownload);

            return new CommunicationModel<TotalDownloadResponse>(sizeDownload);
        }

        private TotalDownloadResponse CreateDownloadResponse(double totalDownloadSize)
        {
            var price = totalDownloadSize * PRICE_PER_MB;

            TotalDownloadResponse downloadSize = new()
            {
                DownloadSize = totalDownloadSize,
                DownloadPrice = price
            };

            return downloadSize;
        }

        public async Task Consume(ConsumeContext<TotalDownloadRequestServiceToDatabase> context)
        {
            var respondModel = await TotalDownload(context.Message);

            await context.RespondAsync(respondModel);
        }
    }
}