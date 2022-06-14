using MassTransit;
using PoroDev.Common.Contracts.Create;
using PoroDev.Common.Contracts.DeleteUser;
using PoroDev.Common.Contracts.Update;
using PoroDev.Common.Exceptions;
using PoroDev.Common.Contracts.LoginUser;
using PoroDev.Common.Contracts.ReadUser;
using PoroDev.Common.Exceptions;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.Common.Models.UserModels.DeleteUser;
using PoroDev.Common.Models.UserModels.LoginUser;
using PoroDev.GatewayAPI.Services.Contracts;
using static PoroDev.GatewayAPI.Helpers.ExceptionFactory;
using static PoroDev.GatewayAPI.Constants.Constats;
using PoroDev.Common.Contracts;

namespace PoroDev.GatewayAPI.Services
{
    public class UserManagementService : IUserManagementService
    {
        private readonly IRequestClient<UserCreateRequestGatewayToService> _createRequestClient;
        private readonly IRequestClient<UserDeleteRequestGatewayToService> _deleteRequestClient;
        private readonly IRequestClient<UserUpdateRequestGatewayToService> _updateRequestClient;
        private readonly IRequestClient<UserReadByEmailRequestGatewayToService> _readUserByEmailRequestClient;
        private readonly IRequestClient<UserLoginRequestGatewayToService> _loginRequestClient;

        public UserManagementService(IRequestClient<UserCreateRequestGatewayToService> createRequestClient, 
            IRequestClient<UserReadByEmailRequestGatewayToService> readUserByEmailRequestClient, 
            IRequestClient<UserLoginRequestGatewayToService> loginRequestClient, 
            IRequestClient<UserDeleteRequestGatewayToService> deleteRequestClient,
            IRequestClient<UserUpdateRequestGatewayToService> updateRequestClient)
        {
            _createRequestClient = createRequestClient;
            _deleteRequestClient = deleteRequestClient;
            _updateRequestClient = updateRequestClient;
            _readUserByEmailRequestClient = readUserByEmailRequestClient;
            _loginRequestClient = loginRequestClient;
        }



        public async Task<DataUserModel> CreateUser(UserCreateRequestGatewayToService createModel)
        {
            var requestReturnContext = await _createRequestClient.GetResponse<CommunicationModel<DataUserModel>>(createModel);

            if (requestReturnContext.Message.ExceptionName != null)
                ThrowException(requestReturnContext.Message.ExceptionName, requestReturnContext.Message.HumanReadableMessage);         

            var returnModel = requestReturnContext.Message.Entity;

            return returnModel;
        }
     

        public async Task<DeleteUserModel> DeleteUser(UserDeleteRequestGatewayToService deleteModel)
        {
            if(string.IsNullOrEmpty(deleteModel.Email.Trim()))
            {
                ThrowException(nameof(EmailFormatException), EmptyEmail);
            }    

            var responseContext = await _deleteRequestClient.GetResponse<UserDeleteResponseServiceToGateway>(deleteModel);

            if (responseContext.Message.ExceptionName != null)
                ThrowException(responseContext.Message.ExceptionName, responseContext.Message.HumanReadableMessage);

            return responseContext.Message.Entity;
        }

        public async Task<LoginUserModel> LoginUser(UserLoginRequestGatewayToService loginModel)
        {
            var responseContext = await _loginRequestClient.GetResponse<UserLoginResponseServiceToGateway>(loginModel);
            return responseContext.Message.Entity;
        }

        public async Task<DataUserModel> ReadUserByEmail(string email)
        {
            var readUserByEmail = new UserReadByEmailRequestGatewayToService()
            {
                Email = email
            };

            var requestResponseContext = await _readUserByEmailRequestClient.GetResponse<UserReadByEmailResponseServiceToGateway>(readUserByEmail);

            if (requestResponseContext.Message.ExceptionName != null)
                ThrowException(requestResponseContext.Message.ExceptionName, requestResponseContext.Message.HumanReadableMessage);

            var returnUser = requestResponseContext.Message.Entity;

            return returnUser;
        }

        public async Task<DataUserModel> UpdateUser(UserUpdateRequestGatewayToService updateModel)
        {
            if (string.IsNullOrEmpty(updateModel.Email.Trim()))
            {
                ThrowException(nameof(EmailFormatException), EmptyEmail);
            }

            var requestReturnContext = await _updateRequestClient.GetResponse<UserUpdateResponseServiceToGateway>(updateModel);

            if (requestReturnContext.Message.ExceptionName != null)
            {
                ThrowException(requestReturnContext.Message.ExceptionName, requestReturnContext.Message.HumanReadableMessage);
            }

            var returnModel = requestReturnContext.Message.Entity;
            return returnModel;
        }
    }
}