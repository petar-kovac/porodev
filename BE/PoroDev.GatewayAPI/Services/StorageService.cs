using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.StorageService.DownloadFile;
using PoroDev.Common.Contracts.StorageService.UploadFile;
using PoroDev.Common.Exceptions;
using PoroDev.GatewayAPI.Services.Contracts;
using static PoroDev.GatewayAPI.Constants.Constats;
using static PoroDev.GatewayAPI.Helpers.ExceptionFactory;

namespace PoroDev.GatewayAPI.Services
{
    public class StorageService : IStorageService
    {
        //da li bi trebali ovde dodavati ove standardne RequestFromGatewayToService i slicno ?
        private readonly IRequestClient<FileDownloadMsg> _downloadRequestClient;

        private readonly IRequestClient<FileUploadRequestGatewayToService> _uploadRequestClient;

        // da li cemo dodati ovaj model za citanje ID-a
        //       private readonly IRequestClient<UserReadByIdRequestGatewayToService> _readUserById;

        public StorageService(IRequestClient<FileDownloadMsg> downloadRequestClient, IRequestClient<FileUploadRequestGatewayToService> uploadRequestClient)
        {
            _downloadRequestClient = downloadRequestClient;
            _uploadRequestClient = uploadRequestClient;
        }

        public async Task<FileUploadModel> UploadFile(FileUploadRequestGatewayToService uploadModel)
        {
            //var readUserByIdResponseContext = await _readUserById.GetResponse<CommunicationModel<DataUserModel>>(new UserReadByIdRequestGatewayToService() { Id = Guid.Parse(id) });

            if (uploadModel.File is null)
            {
                ThrowException(nameof(FileUploadFormatException), FileUploadExceptionMessage);
            }

            //FileUploadModel model = new FileUploadModel(uploadModel.FileName, uploadModel.File, uploadModel.UserId);

            var responseContext = await _uploadRequestClient.GetResponse<CommunicationModel<FileUploadModel>>(uploadModel);

            return responseContext.Message.Entity;
        }

        public async Task<FileDownloadMsg> DownloadFile(FileDownloadMsg downloadModel)
        {
            var responseContext = await _downloadRequestClient.GetResponse<CommunicationModel<FileDownloadMsg>>(downloadModel);

            return responseContext.Message.Entity;
        }
    }
}