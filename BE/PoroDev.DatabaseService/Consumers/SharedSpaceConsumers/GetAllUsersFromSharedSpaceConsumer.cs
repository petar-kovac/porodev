using AutoMapper;
using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.SharedSpace.GetAllUsers;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.DatabaseService.Repositories.Contracts;
using static PoroDev.Common.Extensions.CreateResponseExtension;

namespace PoroDev.DatabaseService.Consumers.SharedSpaceConsumers
{
    public class GetAllUsersFromSharedSpaceConsumer : BaseDbConsumer, IConsumer<GetAllUsersFromSharedSpaceRequestServiceToDatabase>
    {
        public GetAllUsersFromSharedSpaceConsumer(IUnitOfWork unitOfWork, IMapper mapper, IFileRepository fileRepository) : base(unitOfWork, mapper, fileRepository)
        {
        }

        public async Task Consume(ConsumeContext<GetAllUsersFromSharedSpaceRequestServiceToDatabase> context)
        {
            var sharedSpacesUsers = await _unitOfWork.SharedSpacesUsers.GetAllUsersBySharedSpaceId(context.Message.SharedSpaceId);
            var listOfUsers = new List<DataUserModel>();
            foreach (var item in sharedSpacesUsers)
            {
                listOfUsers.Add(item.User);
            }
            var response = CreateResponseModel<CommunicationModel<List<DataUserModel>>, List<DataUserModel>>(listOfUsers);
            await context.RespondAsync<CommunicationModel<List<DataUserModel>>>(response);
        }
    }
}
