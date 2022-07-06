using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.StorageService.DeleteFile;
using PoroDev.Common.Exceptions;
using PoroDev.DatabaseService.Repositories.Contracts;

namespace PoroDev.DatabaseService.Consumers.StorageServiceConsumer
{
    public class FileDeleteConsumer : IConsumer<FileDeleteRequestServiceToDatabase>
    {
        private IUnitOfWork _unitOfWork;

        public FileDeleteConsumer(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Consume(ConsumeContext<FileDeleteRequestServiceToDatabase> context)
        {
            var entity = await _unitOfWork.UserFiles.GetByStringIdAsync(context.Message.FileId);
            if (entity == null)
            {
                throw new FileNotFoundException("File not found");
            }
            else
            {
                entity.IsDeleted = true;
                await _unitOfWork.UserFiles.UpdateAsyncStringId(entity, context.Message.FileId);
            }

            var model = new FileDeleteMessage(context.Message.FileId);
            var response = new CommunicationModel<FileDeleteMessage>() { Entity = model, ExceptionName = null, HumanReadableMessage = null };

            await context.RespondAsync(response);
        }
    }
}
