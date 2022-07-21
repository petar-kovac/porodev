using AutoMapper;
using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.StorageService.Query;
using PoroDev.StorageService.Services.Contracts;

namespace PoroDev.StorageService.Consumers
{
    public class FileQueryConsumer : ConsumerBase, IConsumer<FileQueryGatewayToService>
    {
        public FileQueryConsumer(IStorageService storageService, IMapper mapper) : base(storageService, mapper)
        {
        }

        public async Task Consume(ConsumeContext<FileQueryGatewayToService> context)
        {
            var queryRequest = _mapper.Map<FileQueryServiceToDatabase>(context.Message);

            var queryResponse = await _storageService.Query(queryRequest);

            await context.RespondAsync<CommunicationModel<List<FileQueryModel>>>(queryResponse);
        }
    }
}
