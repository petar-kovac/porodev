using AutoMapper;
using MassTransit;
using PoroDev.Common.Contracts.SharedSpace.QueryFiles;
using PoroDev.SharedSpaceService.Services.Contracts;

namespace PoroDev.SharedSpaceService.Consumers
{
    public class QueryFilesConsumer : BaseConsumer<QueryFilesGatewayToService>
    {
        public QueryFilesConsumer(ISharedSpaceService sharedSpaceService, IMapper mapper) : base(sharedSpaceService, mapper)
        {
        }

        public override async Task Consume(ConsumeContext<QueryFilesGatewayToService> context)
        {
            var requestModel = _mapper.Map<QueryFilesServiceToDatabase>(context.Message);

            var returnModel = await _sharedSpaceService.QueryFiles(requestModel);

            await context.RespondAsync(returnModel);
        }
    }
}