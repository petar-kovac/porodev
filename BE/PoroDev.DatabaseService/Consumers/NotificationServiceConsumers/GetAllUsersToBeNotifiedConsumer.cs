using AutoMapper;
using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.NotificationService;
using PoroDev.Common.Models.NotificationServiceModels;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.DatabaseService.Repositories.Contracts;

namespace PoroDev.DatabaseService.Consumers.NotificationServiceConsumers
{
    public class GetAllUsersToBeNotifiedConsumer : BaseDbConsumer, IConsumer<GetUsersToBeNotifiedRequestServiceToDatabase>
    {
        public GetAllUsersToBeNotifiedConsumer(IUnitOfWork unitOfWork, IMapper mapper, IFileRepository fileRepository) : base(unitOfWork, mapper, fileRepository)
        {
        }

        public async Task Consume(ConsumeContext<GetUsersToBeNotifiedRequestServiceToDatabase> context)
        {
            List<DataUserModel> listOfUsers = new List<DataUserModel>();
            if(DateTime.DaysInMonth(DateTime.UtcNow.Year, DateTime.UtcNow.Month) == DateTime.UtcNow.Day)
            {
                listOfUsers = await _unitOfWork.NotificationData.GetAllUsersToBeNotified(context.Message.Day, context.Message.Hour);
            }
            else
            {
                listOfUsers = await _unitOfWork.NotificationData.GetAllUsersToBeNotified(context.Message.Day, context.Message.Hour);
            }
            var returnModel = new CommunicationModel<List<DataUserModel>>(listOfUsers);
            await context.RespondAsync<CommunicationModel<List<DataUserModel>>>(returnModel);

        }
    }
}
