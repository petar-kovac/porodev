using AutoMapper;
using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.StorageService.Query;
using PoroDev.DatabaseService.Repositories.Contracts;

namespace PoroDev.DatabaseService.Consumers.StorageServiceConsumer.Query
{
    public class SingleFileQueryConsumer : BaseDbConsumer, IConsumer<SingleFileQueryServiceToDatabase>
    {
        public SingleFileQueryConsumer(IUnitOfWork unitOfWork, IMapper mapper, IFileRepository fileRepository) : base(unitOfWork, mapper, fileRepository)
        {
        }

        public async Task Consume(ConsumeContext<SingleFileQueryServiceToDatabase> context)
        {
            var queryResult = await _fileRepository.QueryFiles(context.Message);

            var reponseModel = _mapper.Map<CommunicationModel<List<SingleFileQueryModel>>>(queryResult);

            await context.RespondAsync(reponseModel);
        }
    }
}