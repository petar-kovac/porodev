using AutoMapper;
using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.UserManagement.SetMonthlyReportTime;
using PoroDev.Common.Exceptions;
using PoroDev.Common.Models.NotificationServiceModels;
using PoroDev.DatabaseService.Repositories.Contracts;
using static PoroDev.Common.Extensions.CreateResponseExtension;
using static PoroDev.DatabaseService.Constants.Constants;

namespace PoroDev.DatabaseService.Consumers.UserConsumers
{
    public class SetMonthlyReportTimeConsumer : BaseDbConsumer, IConsumer<SetMonthlyReportTimeRequestServiceToDatabase>
    {
        public SetMonthlyReportTimeConsumer(IUnitOfWork unitOfWork, IMapper mapper, IFileRepository fileRepository) : base(unitOfWork, mapper, fileRepository)
        {
        }

        public async Task Consume(ConsumeContext<SetMonthlyReportTimeRequestServiceToDatabase> context)
        {
            var addModel = _mapper.Map<NotificationDataModel>(context.Message);

            var user = await _unitOfWork.Users.FindAsync(user => user.Id.Equals(context.Message.UserId));

            if(user == null)
            {
                var returnModel = new CommunicationModel<NotificationDataModel>(new UserNotFoundException(UserNotVerifiedExceptionMessage));
                await context.RespondAsync<CommunicationModel<NotificationDataModel>>(returnModel);
                return;
            }

            var objectToUpdate = await _unitOfWork.NotificationData.FindAsync(model => model.UserId.Equals(context.Message.UserId));
            if(objectToUpdate == null)
            {
                addModel.Id = Guid.NewGuid();
                var returnModel = await _unitOfWork.NotificationData.CreateAsync(addModel);
                await context.RespondAsync<CommunicationModel<NotificationDataModel>>(returnModel);
                return;
            }
            addModel.Id = objectToUpdate.Entity.Id;
            var response = await _unitOfWork.NotificationData.UpdateAsync(addModel, objectToUpdate.Entity.Id);

            await context.RespondAsync<CommunicationModel<NotificationDataModel>>(response);
        }
    }
}
