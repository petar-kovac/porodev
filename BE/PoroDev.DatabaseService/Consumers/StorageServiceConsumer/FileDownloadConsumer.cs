using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.StorageService.DownloadFile;
using PoroDev.DatabaseService.Repositories.Contracts;

namespace PoroDev.DatabaseService.Consumers.StorageServiceConsumer
{
    public class FileDownloadConsumer : IConsumer<FileDownloadMsg>
    {
        private IFileRepository _fileRepository;

        public FileDownloadConsumer(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        public async Task Consume(ConsumeContext<FileDownloadMsg> context)
        {
            await _fileRepository.DownloadFile(context.Message.FileName);

            FileDownloadMsg model = new(context.Message.FileName);
            var response = new CommunicationModel<FileDownloadMsg>() { Entity = model, ExceptionName = null, HumanReadableMessage = null };

            await context.RespondAsync(response);
        }
    }
}