using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.StorageService.Query;
using PoroDev.Common.Contracts.UserManagement.ReadById;
using PoroDev.Common.Exceptions;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.GatewayAPI.Services.Contracts;
using static PoroDev.Common.Constants.Constants;

namespace PoroDev.GatewayAPI.Services
{
    public class LimitValidatorService : ILimitValidatorService
    {
        private readonly IRequestClient<UserReadByIdRequestGatewayToService> _userReadClient;
        private readonly IRequestClient<FileQueryServiceToDatabase> _queryClient;

        public LimitValidatorService(IRequestClient<UserReadByIdRequestGatewayToService> userReadClient, IRequestClient<FileQueryServiceToDatabase> queryClient)
        {
            _userReadClient = userReadClient;
            _queryClient = queryClient;
        }

        public async Task ValidateUpload(Guid userId, long uploadRequestSize)
        {
            if (uploadRequestSize > 1000000000)
                throw new UserLimitException("Cannot upload file larger than 1gb.");

            var currentTotal = (await _userReadClient.GetResponse<CommunicationModel<DataUserModel>>(new UserReadByIdRequestGatewayToService(userId))).Message
                                                                                                                                                     .Entity
                                                                                                                                                     .FileUploadTotal;

            if (currentTotal + (ulong)uploadRequestSize > MAX_UPLOAD_TOTAL)
                throw new UserLimitException("Upload limit reached.");
            return;
        }

        public async Task ValidateDownload(Guid userId, string fileId)
        {
            var downloadRequestSize = (await _queryClient.GetResponse<CommunicationModel<List<FileQueryModel>>>(new FileQueryServiceToDatabase(userId, fileId))).Message.Entity.First().Length;

            var currentTotal = (await _userReadClient.GetResponse<CommunicationModel<DataUserModel>>(new UserReadByIdRequestGatewayToService(userId))).Message
                                                                                                                                                     .Entity
                                                                                                                                                     .FileDownloadTotal;

            if (currentTotal + downloadRequestSize > MAX_DOWNLOAD_TOTAL)
                throw new UserLimitException("Download limit reached.");
            return;
        }

        public async Task ValidateRuntime(Guid userId)
        {
            var currentTotal = (await _userReadClient.GetResponse<CommunicationModel<DataUserModel>>(new UserReadByIdRequestGatewayToService(userId))).Message
                                                                                                                                                     .Entity
                                                                                                                                                     .RuntimeTotal;

            if (currentTotal + 1 > MAX_RUNTIME_TOTAL)
                throw new UserLimitException("Runtime limit reached.");

            return;
        }
    }
}