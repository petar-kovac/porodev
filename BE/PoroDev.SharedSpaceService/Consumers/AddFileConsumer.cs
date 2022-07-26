using AutoMapper;
using MassTransit;
using PoroDev.Common.Contracts.SharedSpace.AddFile;
using PoroDev.SharedSpaceService.Services.Contracts;

namespace PoroDev.SharedSpaceService.Consumers
{
    public class AddFileConsumer : BaseConsumer<AddFileToSharedSpaceGatewayToService>
    {
        public AddFileConsumer(ISharedSpaceService sharedSpaceService, IMapper mapper) : base(sharedSpaceService, mapper)
        {
        }

        public override async Task Consume(ConsumeContext<AddFileToSharedSpaceGatewayToService> context)
        {
            var resultModel = await _sharedSpaceService.AddFile(context.Message);

            await context.RespondAsync(resultModel);
        }
    }
}