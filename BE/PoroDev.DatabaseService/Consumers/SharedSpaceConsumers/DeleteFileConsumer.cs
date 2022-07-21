using AutoMapper;
using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.SharedSpace.DeleteFile;
using PoroDev.Common.Models.SharedSpaces;
using PoroDev.DatabaseService.Repositories.Contracts;

namespace PoroDev.DatabaseService.Consumers.SharedSpaceConsumers
{
    public class DeleteFileConsumer : BaseDbConsumer, IConsumer<IDeleteFileRequest>
    {
        public DeleteFileConsumer(IUnitOfWork unitOfWork, IMapper mapper, IFileRepository fileRepository) : base(unitOfWork, mapper, fileRepository)
        {
        }

        public async Task Consume(ConsumeContext<IDeleteFileRequest> context)
        {
            var model = await _unitOfWork.SharedSpacesWithFiles.FindAsync(x => x.FileId.Equals(context.Message.FileId) && x.SharedSpaceId.Equals(context.Message.SpaceId));
            await _unitOfWork.SharedSpacesWithFiles.Delete(model.Entity);
            await _unitOfWork.SaveChanges();

            await context.RespondAsync(new CommunicationModel<SharedSpacesFiles>(new SharedSpacesFiles()));
        }
    }
}
