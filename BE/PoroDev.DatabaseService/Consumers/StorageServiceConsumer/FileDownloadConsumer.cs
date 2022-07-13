using AutoMapper;
using MassTransit;
using PoroDev.Common;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.StorageService.DownloadFile;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.DatabaseService.Repositories.Contracts;

namespace PoroDev.DatabaseService.Consumers.StorageServiceConsumer
{
    public class FileDownloadConsumer : BaseDbConsumer, IConsumer<FileDownloadRequestServiceToDatabase>
    {
        public FileDownloadConsumer(IUnitOfWork unitOfWork, IMapper mapper, IFileRepository fileRepository) : base(unitOfWork, mapper, fileRepository)
        {
        }


        public async Task Consume(ConsumeContext<FileDownloadRequestServiceToDatabase> context)
        {
            var entity = await _unitOfWork.UserFiles.GetByStringIdAsync(context.Message.FileId);

            var isIDFormatInvalid = (await _unitOfWork.UserFiles.FindAsync(userFiles => userFiles.FileId.Equals(context.Message.FileId)));

            if (isIDFormatInvalid.ExceptionName != null)
            {
                var responseException = new CommunicationModel<FileDownloadMessage>(new PoroDev.Common.Exceptions.FileNotFoundException("File with that file id not found"));

                await context.RespondAsync(responseException);
            }
            else
            {

                if (entity.IsDeleted == true)
                {
                    DataUserModel user = await _unitOfWork.Users.GetByIdAsync(context.Message.UserId);

                    //super admin = 0; normal user = 1; admin can download deleted file
                    if (user.Role == 0)
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
                    else
                    {
                        var responseException = new CommunicationModel<FileDownloadMessage>(new PoroDev.Common.Exceptions.FileNotFoundException("File with that file id not found"));
                        await context.RespondAsync(responseException);
                    }
                }
                else
                {
                    var downloadedFile = await _fileRepository.DownloadFile(context.Message.FileId, context.Message.UserId);

                    var response = new CommunicationModel<FileDownloadMessage>() { Entity = downloadedFile, ExceptionName = null, HumanReadableMessage = null };

                    await context.RespondAsync(response);
                }

            }
            //FileDownloadMessage model = new()
            //{
            //    File = downloadedFile.File,
            //    FileName = downloadedFile.FileName,
            //    ContentType = downloadedFile.ContentType
            //};

            //var response = new CommunicationModel<FileDownloadMessage>() { Entity = model, ExceptionName = null, HumanReadableMessage = null };

            //await context.RespondAsync(response);
        }
    }
}