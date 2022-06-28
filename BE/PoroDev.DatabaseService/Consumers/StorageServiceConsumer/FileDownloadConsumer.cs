using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.StorageService.DownloadFile;
using PoroDev.DatabaseService.Repositories.Contracts;

namespace PoroDev.DatabaseService.Consumers.StorageServiceConsumer
{
    public class FileDownloadConsumer : IConsumer<FileDownloadRequestServiceToDatabase>
    {
        private IFileRepository _fileRepository;

        public FileDownloadConsumer(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        public async Task Consume(ConsumeContext<FileDownloadRequestServiceToDatabase> context)
        {
            await _fileRepository.DownloadFile(context.Message.FileId, context.Message.FileName, context.Message.File);

            FileDownloadModel model = new(context.Message.FileId, context.Message.FileName, context.Message.File);
            var response = new CommunicationModel<FileDownloadModel>() { Entity = model, ExceptionName = null, HumanReadableMessage = null };

            await context.RespondAsync(response);
        }
    }
}