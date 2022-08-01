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

            var responseModel = new List<ReadAllSharedSpacesResponse>();

            foreach(var obj in sharedSpacesForUser)
            {
                responseModel.Add(new ReadAllSharedSpacesResponse(obj.SharedSpaceId, obj.UserId, obj.User.Name, obj.User.Lastname, obj.SharedSpace.Name));
            }

            await context.RespondAsync(new CommunicationModel<List<ReadAllSharedSpacesResponse>>(responseModel));
        }
    }
}