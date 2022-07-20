using AutoMapper;
using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.SharedSpace;
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
            var createModel = _mapper.Map<SharedSpacesFiles>(context.Message);

            var response = await _unitOfWork.SharedSpacesWithFiles.CreateAsync(createModel);

            await _unitOfWork.SaveChanges();

            var responseModel = _mapper.Map<CommunicationModel<SharedSpacesFiles>>(response);

            await context.RespondAsync(responseModel);
        }
    }
}
