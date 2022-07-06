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
            var downloadedFile = await _fileRepository.DownloadFile(context.Message.FileId, context.Message.UserId);

            FileDownloadMessage model = new()
            {
                File = downloadedFile.File,
                FileName = downloadedFile.FileName,
                ContentType = downloadedFile.ContentType
            };

            var response = new CommunicationModel<FileDownloadMessage>() { Entity = model, ExceptionName = null, HumanReadableMessage = null };

            await context.RespondAsync(response);
        }
    }
}