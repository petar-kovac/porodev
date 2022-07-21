using AutoMapper;
using MassTransit;
using PoroDev.Common.Contracts.StorageService;
using PoroDev.Common.Contracts.StorageService.Query;
using PoroDev.DatabaseService.Repositories.Contracts;

namespace PoroDev.DatabaseService.Consumers.StorageServiceConsumer
{
    public class ChangeFileExeConsumer : BaseDbConsumer, IConsumer<IUpdateFileExe>
    {
        public ChangeFileExeConsumer(IUnitOfWork unitOfWork, IMapper mapper, IFileRepository fileRepository) : base(unitOfWork, mapper, fileRepository)
        {
        }

        public async Task Consume(ConsumeContext<IUpdateFileExe> context)
        {
            await _fileRepository.QueryFiles(new FileQueryServiceToDatabase(context.Message.UserId.Value, null, context.Message.FileName));
        }
    }
}
