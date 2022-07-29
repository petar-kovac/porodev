using AutoMapper;
using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.StorageService.DownloadFile;
using PoroDev.Common.Exceptions;
using PoroDev.Common.Models.SharedSpaces;
using PoroDev.DatabaseService.Repositories.Contracts;
using PoroDev.DatabaseService.Services.Contracts;
using static PoroDev.Common.MassTransit.Extensions;
using static PoroDev.DatabaseService.Constants.Constants;

namespace PoroDev.DatabaseService.Consumers.StorageServiceConsumer
{
    public class FileDownloadConsumer : BaseDbConsumer, IConsumer<FileDownloadRequestServiceToDatabase>
    {
        private readonly IEncryptionService _encryptionService;

        public FileDownloadConsumer(IUnitOfWork unitOfWork, IMapper mapper, IFileRepository fileRepository, IEncryptionService encryptionService) : base(unitOfWork, mapper, fileRepository)
        {
            _encryptionService = encryptionService;
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

            var userModel = (await _unitOfWork.Users.FindAsync(user => user.Id.Equals(downloadRequest.UserId))).Entity;

            //If the user isn't an admin and if he doesn't own the file
            if (!fileEntry.CurrentUserId.Equals(downloadRequest.UserId) && (int)userModel.Role == 1)
            {
                //Check if user is in the same shared space as the file

                var userSharedSpaces =
                    await _unitOfWork.SharedSpacesUsers.FindAllAsync(userSpace =>
                        userSpace.UserId.Equals(userModel.Id));

                var fileSharedSpace =
                    await _unitOfWork.SharedSpacesWithFiles.FindAllAsync(spaceFile =>
                        spaceFile.FileId.Equals(fileEntry.FileId));

                if(!userSharedSpaces.Any() && !fileSharedSpace.Any())
                    return new CommunicationModel<FileDownloadMessage>(new UserPermissionException());

                SharedSpacesFiles? intersect = fileSharedSpace.FirstOrDefault(fileSpace =>
                    userSharedSpaces.Select(userSpace => userSpace.SharedSpaceId).Contains(fileSpace.SharedSpaceId));

                if (intersect is null)
                    return new CommunicationModel<FileDownloadMessage>(new UserPermissionException());
            }

            //If the user isn't an admin and the file is deleted
            if ((int)userModel.Role == 1 && fileEntry.IsDeleted)
                return new CommunicationModel<FileDownloadMessage>(new UserPermissionException());

            try
            {
                var downloadedFile = await _fileRepository.DownloadFile(downloadRequest.FileId);

                downloadedFile.File = _encryptionService.DecryptBytes(downloadedFile.File);

                var downloadedFileAsMessage = _mapper.Map<FileDownloadMessage>(downloadedFile);

                downloadedFileAsMessage.File = await messageDataRepository.PutBytes(downloadedFile.File);

                userModel.FileDownloadTotal += (ulong)downloadedFile.File.LongLength;

                await _unitOfWork.SaveChanges();

                return new CommunicationModel<FileDownloadMessage>(downloadedFileAsMessage);
            }
            catch (Exception)
            {
                return new CommunicationModel<FileDownloadMessage>(new DatabaseException(InternalDatabaseError));
            }
        }
    }
}