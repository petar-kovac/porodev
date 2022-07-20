using AutoMapper;
using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.UserManagement.ReadAllSharedSpacesForUser;
using PoroDev.Common.Models.SharedSpaces;
using PoroDev.DatabaseService.Repositories.Contracts;
using static PoroDev.Common.Extensions.CreateResponseExtension;

namespace PoroDev.DatabaseService.Consumers.UserConsumers
{
    public class ReadAllSharedSpacesForUserConsumer : BaseDbConsumer, IConsumer<ReadAllSharedSpacesForUserRequestServiceToDatabase>
    {
        public ReadAllSharedSpacesForUserConsumer(IUnitOfWork unitOfWork, IMapper mapper, IFileRepository fileRepository) : base(unitOfWork, mapper, fileRepository)
        {

        }
        public async Task Consume(ConsumeContext<ReadAllSharedSpacesForUserRequestServiceToDatabase> context)
        {
            var sharedSpacesForUser = await _unitOfWork.SharedSpacesUsers.GetSharedSpacesByUserId(context.Message.UserId);
            List<SharedSpace> listOfSharedSpaces = new List<SharedSpace>();
            foreach (var item in sharedSpacesForUser)
            {
                listOfSharedSpaces.Add(item.SharedSpace);
            }
            var returnModel = CreateResponseModel<CommunicationModel<List<SharedSpace>>, List<SharedSpace>>(listOfSharedSpaces);
            await context.RespondAsync<CommunicationModel<List<SharedSpace>>>(returnModel);
        }
    }
}
