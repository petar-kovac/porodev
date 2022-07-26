using AutoMapper;
using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.SharedSpace.AddUser;
using PoroDev.Common.Exceptions;
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
            var requestModel = _mapper.Map<SharedSpacesUsers>(context.Message);

            var returnModel = await AddUserToSharedSpace(requestModel);
            
            await context.RespondAsync(returnModel);
        }

        private async Task<CommunicationModel<SharedSpacesUsers>> AddUserToSharedSpace(SharedSpacesUsers modelForDb)
        {
            var exists = await _unitOfWork.SharedSpacesUsers.FindAsync(spaceUser => modelForDb.Compare(spaceUser.UserId, spaceUser.SharedSpaceId));

            if (exists is not null)
                return new CommunicationModel<SharedSpacesUsers>(new SharedSpaceException("User already exists in that shared space."));

            try
            {
                var response = await _unitOfWork.SharedSpacesUsers.CreateAsync(modelForDb);
                await _unitOfWork.SaveChanges();

                var returnModel = _mapper.Map<CommunicationModel<SharedSpacesUsers>>(response);

                return returnModel;

            }
            catch (Exception ex)
            { 
                var dataException = (DatabaseException)ex;
                dataException.HumanReadableErrorMessage = "Exception happened in DatabaseConsumer add user to shared space.";

                return new CommunicationModel<SharedSpacesUsers>(dataException);
            }           
        }
    }
}
