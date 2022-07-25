using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.BillingReport.TotalUpload;
using PoroDev.Common.Exceptions;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.Common.Models.UserReportsModels.Data;
using PoroDev.DatabaseService.Repositories.Contracts;
using static PoroDev.Common.Constants.Constants;
using static PoroDev.DatabaseService.Constants.Constants;
using static PoroDev.DatabaseService.Helpers.CurrentMonth;

namespace PoroDev.DatabaseService.Consumers.BillingReportConsumers
{
    public class TotalUploadConsumer : IConsumer<TotalUploadRequestServiceToDatabase>
    {
        private IUnitOfWork _unitOfWork;

        public TotalUploadConsumer(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private async Task<CommunicationModel<TotalUploadResponse>> TotalUpload(TotalUploadRequestServiceToDatabase totalUpload)
        {
            DataUserModel admin = await _unitOfWork.Users.GetByIdAsync(totalUpload.AdminId);

            if (admin.Role != 0)
                return new CommunicationModel<TotalUploadResponse>(new UserPermissionException(UserIsNotAdminExceptionMessage));

            var user = await _unitOfWork.Users.GetByIdAsync(totalUpload.UserId);

            if (user == null)
                return new CommunicationModel<TotalUploadResponse>(new UserNotFoundException(UserNotFoundExceptionMessage));

            if (totalUpload.Month == GetMonthName())
            {
                var totalUploadSize = Convert.ToDouble(user.FileUploadTotal / 1024.0);
                var uploadSize = CreateUploadResponse(totalUploadSize);

                return new CommunicationModel<TotalUploadResponse>(uploadSize);
            }

            List<UserReportsData> findUser = (await _unitOfWork.UserReports.FindAllAsync(
                user => user.CurrentUserId.Equals(totalUpload.UserId) &&
                user.Month.Equals(totalUpload.Month))).ToList<UserReportsData>();

            if (findUser == null)
                return new CommunicationModel<TotalUploadResponse>(new UserNotFoundException(UserNotFoundExceptionMessage));

            var totalSizeUpload = Convert.ToDouble(findUser.FirstOrDefault().FileUploadTotal / 1024.0);
            var sizeUpload = CreateUploadResponse(totalSizeUpload);

            return new CommunicationModel<TotalUploadResponse>(sizeUpload);
        }

        private TotalUploadResponse CreateUploadResponse(double totalUploadSize)
        {
            var price = totalUploadSize * PRICE_PER_MB;

            TotalUploadResponse uploadSize = new()
            {
                UploadSize = totalUploadSize,
                UploadPrice = price
            };

            return uploadSize;
        }

        public async Task Consume(ConsumeContext<TotalUploadRequestServiceToDatabase> context)
        {
            var respondModel = await TotalUpload(context.Message);

            await context.RespondAsync(respondModel);
        }
    }
}