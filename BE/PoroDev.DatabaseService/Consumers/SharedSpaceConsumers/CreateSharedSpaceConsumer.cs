using AutoMapper;
using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.SharedSpace;
using PoroDev.Common.Models.SharedSpaces;
using PoroDev.DatabaseService.Repositories.Contracts;

namespace PoroDev.DatabaseService.Consumers.SharedSpaceConsumers
{
    public class CreateSharedSpaceConsumer : BaseDbConsumer, IConsumer<CreateSharedSpaceRequestServiceToDatabase>
    {
        public CreateSharedSpaceConsumer(IUnitOfWork unitOfWork, IMapper mapper, IFileRepository fileRepository) : base(unitOfWork, mapper, fileRepository)
        {

        }
        public async Task Consume(ConsumeContext<CreateSharedSpaceRequestServiceToDatabase> context)
        {
            var modelForDb = _mapper.Map<SharedSpace>(context.Message);
            modelForDb.Id = Guid.NewGuid();
            var dbReturn = await _unitOfWork.SharedSpaces.CreateAsync(modelForDb);
            await _unitOfWork.SaveChanges();

            var responseModel = _mapper.Map<CommunicationModel<SharedSpace>>(dbReturn);
            await context.RespondAsync(responseModel);
        }
    }
}
