using AutoMapper;
using MassTransit;
using PoroDev.Common.Contracts.SharedSpace.QueryFiles;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.DatabaseService.Repositories.Contracts;

namespace PoroDev.DatabaseService.Consumers.SharedSpaceConsumers
{
    public class QueryFilesConsumer : BaseDbConsumer, IConsumer<QueryFilesServiceToDatabase>
    {
        public QueryFilesConsumer(IUnitOfWork unitOfWork, IMapper mapper, IFileRepository fileRepository) : base(unitOfWork, mapper, fileRepository)
        {
        }

        public async Task Consume(ConsumeContext<QueryFilesServiceToDatabase> context)
        {
            var result = await _unitOfWork.SharedSpacesWithFiles.QueryFilesBySpaceId(context.Message.SpaceId);

            List<QueryFilesResponse> responseList = new();

            foreach (var data in result)
            {
                
            }

        }
    }
}
