using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.BillingReport.TotalUpload;
using PoroDev.Common.Exceptions;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.DatabaseService.Repositories.Contracts;

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
                return new CommunicationModel<TotalUploadResponse>(new UserPermissionException());

            var user = await _unitOfWork.Users.GetByIdAsync(totalUpload.UserId);
            var totalUploadSize = Convert.ToDouble(user.FileUploadTotal / 1024.0);

            TotalUploadResponse uploadSize = new()
            {
                UploadSize = totalUploadSize
            };

            return new CommunicationModel<TotalUploadResponse>(uploadSize);
        }

        public async Task Consume(ConsumeContext<TotalUploadRequestServiceToDatabase> context)
        {
            var respondModel = await TotalUpload(context.Message);

            await context.RespondAsync(respondModel);
        }
    }
}