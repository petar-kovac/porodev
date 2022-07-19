using AutoMapper;
using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.StorageService.Query;
using PoroDev.DatabaseService.Repositories.Contracts;

namespace PoroDev.DatabaseService.Consumers.StorageServiceConsumer.Query
{
    public class SingleFileQueryConsumer : BaseDbConsumer, IConsumer<FileQueryServiceToDatabase>
    {
        public SingleFileQueryConsumer(IUnitOfWork unitOfWork, IMapper mapper, IFileRepository fileRepository) : base(unitOfWork, mapper, fileRepository)
        {
        }

        public async Task Consume(ConsumeContext<FileQueryServiceToDatabase> context)
        {
            var queryResult = await _fileRepository.QueryFiles(context.Message);

            var reponseModel = new CommunicationModel<List<FileQueryModel>>(queryResult);

            await context.RespondAsync(reponseModel);
        }
    }
}
