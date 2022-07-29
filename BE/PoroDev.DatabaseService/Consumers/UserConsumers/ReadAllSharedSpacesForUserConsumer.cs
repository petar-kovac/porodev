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
            var listOfSharedSpaces =
                sharedSpacesForUser.Select(userSharedSpace => userSharedSpace.SharedSpace)
                    .OrderBy(sharedSpace => sharedSpace.Name).ToList();
                    

            var returnModel = CreateResponseModel<CommunicationModel<List<SharedSpace>>, List<SharedSpace>>(listOfSharedSpaces);
            await context.RespondAsync<CommunicationModel<List<SharedSpace>>>(returnModel);
        }
    }
}