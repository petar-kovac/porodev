using AutoMapper;
using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.StorageService.DeleteFile;
using PoroDev.DatabaseService.Repositories.Contracts;

namespace PoroDev.DatabaseService.Consumers.StorageServiceConsumer
{
    public class FileDeleteConsumer : BaseDbConsumer, IConsumer<FileDeleteRequestServiceToDatabase>
    {
        public FileDeleteConsumer(IUnitOfWork unitOfWork, IMapper mapper, IFileRepository fileRepository) : base(unitOfWork, mapper, fileRepository)
        {
        }

        public async Task Consume(ConsumeContext<FileDeleteRequestServiceToDatabase> context)
        {
            var entity = await _unitOfWork.UserFiles.GetByStringIdAsync(context.Message.FileId);

            if (entity == null)
            {
                await context.RespondAsync(new CommunicationModel<FileDeleteMessage>(new PoroDev.Common.Exceptions.FileNotFoundException("File with that file id not found")));
            }
            else
            {
                if (entity.IsDeleted == true)
                {
                    await context.RespondAsync(new CommunicationModel<FileDeleteMessage>(new PoroDev.Common.Exceptions.FileNotFoundException("File with that file id not found")));
                }
                else
                {
                    entity.IsDeleted = true;
                    await _unitOfWork.UserFiles.UpdateAsyncStringId(entity, context.Message.FileId);

                    
                }
            }

            var model = new FileDeleteMessage(context.Message.FileId);
            var response = new CommunicationModel<FileDeleteMessage>() { Entity = model, ExceptionName = null, HumanReadableMessage = null };

            await context.RespondAsync(response);
        }
    }
}