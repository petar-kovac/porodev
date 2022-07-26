using AutoMapper;
using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.SharedSpace.AddUser;
using PoroDev.Common.Models.SharedSpaces;
using PoroDev.DatabaseService.Repositories.Contracts;

namespace PoroDev.DatabaseService.Consumers.SharedSpaceConsumers
{
    public class AddUserToSharedSpaceConsumer : BaseDbConsumer, IConsumer<AddUserToSharedSpaceRequestServiceToDatabase>
    {
        public AddUserToSharedSpaceConsumer(IUnitOfWork unitOfWork, IMapper mapper, IFileRepository fileRepository) : base(unitOfWork, mapper, fileRepository)
        {
        }

        public async Task Consume(ConsumeContext<AddUserToSharedSpaceRequestServiceToDatabase> context)
        {
            var modelForDb = new SharedSpacesUsers() { SharedSpaceId = context.Message.SharedSpaceID, UserId = context.Message.UserToAddId };
            var response = await _unitOfWork.SharedSpacesUsers.CreateAsync(modelForDb);
            await _unitOfWork.SaveChanges();
            var returnModel = _mapper.Map<CommunicationModel<SharedSpacesUsers>>(response);
            await context.RespondAsync<CommunicationModel<SharedSpacesUsers>>(returnModel);
        }
    }
}