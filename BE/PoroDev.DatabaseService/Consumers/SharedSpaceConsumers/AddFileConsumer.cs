using AutoMapper;
using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.SharedSpace.AddFile;
using PoroDev.Common.Exceptions;
using PoroDev.Common.Models.SharedSpaces;
using PoroDev.DatabaseService.Repositories.Contracts;

namespace PoroDev.DatabaseService.Consumers.SharedSpaceConsumers
{
    public class AddFileConsumer : BaseDbConsumer, IConsumer<AddFileToSharedSpaceServiceToDatabase>
    {
        public AddFileConsumer(IUnitOfWork unitOfWork, IMapper mapper, IFileRepository fileRepository) : base(unitOfWork, mapper, fileRepository)
        {
        }

        public async Task Consume(ConsumeContext<AddFileToSharedSpaceServiceToDatabase> context)
        {
            var requestModel = _mapper.Map<SharedSpacesFiles>(context.Message);

            var returnModel = await AddFileToSharedSpace(requestModel);

            await context.RespondAsync(returnModel);
        }

        private async Task<CommunicationModel<SharedSpacesFiles>> AddFileToSharedSpace(SharedSpacesFiles createModel)
        {
            var exists = await _unitOfWork.SharedSpacesWithFiles.FindAsync(spaceFile => spaceFile.FileId.Equals(createModel.FileId)
                                                                                     && spaceFile.SharedSpaceId.Equals(createModel.SharedSpaceId));

            if (exists.Entity is not null)
                return new CommunicationModel<SharedSpacesFiles>(new SharedSpaceException("File already exists in shared space"));

            try
            {
                var response = await _unitOfWork.SharedSpacesWithFiles.CreateAsync(createModel);
                await _unitOfWork.SaveChanges();

                var responseModel = _mapper.Map<CommunicationModel<SharedSpacesFiles>>(response);

                return responseModel;
            }
            catch (Exception)
            {
                var dataException = new DatabaseException("Exception happened in DatabaseConsumer add file to shared space.");
                return new CommunicationModel<SharedSpacesFiles>(dataException);
            }
        }
    }
}