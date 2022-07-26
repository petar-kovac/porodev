using AutoMapper;
using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.SharedSpace.QueryFiles;
using PoroDev.Common.Contracts.StorageService.Query;
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
                var fileData = (await _fileRepository.QueryFiles(new FileQueryServiceToDatabase() { FileId = data.FileId, UserId = data.File.CurrentUserId })).First();

                responseList.Add(new QueryFilesResponse(data.FileId,
                                                        fileData.Filename,
                                                        data.File.CurrentUserId,
                                                        fileData.UserName,
                                                        fileData.UserLastname));
            }

            await context.RespondAsync(new CommunicationModel<List<QueryFilesResponse>>(responseList));
        }
    }
}