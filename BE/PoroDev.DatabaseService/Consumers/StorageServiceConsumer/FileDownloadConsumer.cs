using AutoMapper;
using MassTransit;
using PoroDev.Common;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.StorageService.DownloadFile;
using PoroDev.Common.Exceptions;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.DatabaseService.Repositories.Contracts;
using static PoroDev.Common.Enums.UserEnums;
using static PoroDev.DatabaseService.Constants.Constants;

namespace PoroDev.DatabaseService.Consumers.StorageServiceConsumer
{
    public class FileDownloadConsumer : BaseDbConsumer, IConsumer<FileDownloadRequestServiceToDatabase>
    {
        public FileDownloadConsumer(IUnitOfWork unitOfWork, IMapper mapper, IFileRepository fileRepository) : base(unitOfWork, mapper, fileRepository)
        {
        }


        public async Task Consume(ConsumeContext<FileDownloadRequestServiceToDatabase> context)
        {
            var respondModel = await DownloadFile(context.Message);

            await context.RespondAsync(respondModel);         
        }

        private async Task<CommunicationModel<FileDownloadMessage>> DownloadFile(FileDownloadRequestServiceToDatabase downloadRequest)
        {
            var fileEntry = await _unitOfWork.UserFiles.GetByStringIdAsync(downloadRequest.FileId);

            if (fileEntry == null)
                return new CommunicationModel<FileDownloadMessage>(new Common.Exceptions.FileNotFoundException("File with that file id not found"));

            UserRole userRole = (await _unitOfWork.Users.FindAsync(user => user.Id.Equals(downloadRequest.UserId))).Entity.Role;

            //If the user isn't an admin and if he doesn't own the file
            if (!fileEntry.CurrentUserId.Equals(downloadRequest.UserId) && !(userRole == 0))
                return new CommunicationModel<FileDownloadMessage>(new UserPermissionException());

            //If the user isn't an admin and the file is deleted
            if (((int)userRole) == 1 && fileEntry.IsDeleted)
                return new CommunicationModel<FileDownloadMessage>(new UserPermissionException());

            try
            {
                var downloadedFile = await _fileRepository.DownloadFile(downloadRequest.FileId);

                return new CommunicationModel<FileDownloadMessage>(downloadedFile);
            }
            catch (Exception)
            {

                return new CommunicationModel<FileDownloadMessage>(new DatabaseException(InternalDatabaseError));
            }

        }
    }
}