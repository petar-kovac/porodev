using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Exceptions;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.Common.Models.UserModels.DeleteUser;
using PoroDev.Common.Models.UserModels.LoginUser;
using PoroDev.GatewayAPI.Services.Contracts;
using static PoroDev.GatewayAPI.Constants.Constats;
using static PoroDev.GatewayAPI.Helpers.ExceptionFactory;
using PoroDev.Common.Models.UserModels.RegisterUser;
using PoroDev.Common.Contracts.UserManagement.Create;
using PoroDev.Common.Contracts.UserManagement.Update;
using PoroDev.Common.Contracts.UserManagement.DeleteUser;
using PoroDev.Common.Contracts.UserManagement.LoginUser;
using PoroDev.Common.Contracts.UserManagement.ReadUser;
using PoroDev.Common.Contracts.UserMenagement.ReadById;

namespace PoroDev.GatewayAPI.Services
{
    public class UserManagementService : IUserManagementService
    {
        private readonly IRequestClient<UserCreateRequestGatewayToService> _createRequestClient;
        private readonly IRequestClient<UserDeleteRequestGatewayToService> _deleteRequestClient;
        private readonly IRequestClient<UserUpdateRequestGatewayToService> _updateRequestClient;
        private readonly IRequestClient<UserReadByEmailRequestGatewayToService> _readUserByEmailRequestClient;
        private readonly IRequestClient<UserLoginRequestGatewayToService> _loginRequestClient;
        private readonly IRequestClient<RegisterUserRequestGatewayToService> _registerClient;
        private readonly IRequestClient<UserReadByIdRequestGatewayToService> _readUserByIdRequestClient;

        public UserManagementService(
            IRequestClient<UserCreateRequestGatewayToService> createRequestClient, 
            IRequestClient<UserReadByEmailRequestGatewayToService> readUserByEmailRequestClient,
            IRequestClient<UserLoginRequestGatewayToService> loginRequestClient,
            IRequestClient<UserDeleteRequestGatewayToService> deleteRequestClient,
            IRequestClient<UserUpdateRequestGatewayToService> updateRequestClient,
            IRequestClient<RegisterUserRequestGatewayToService> registerClient,
            IRequestClient<UserReadByIdRequestGatewayToService> readUserByIdRequestClient
            )
        {
            _createRequestClient = createRequestClient;
            _deleteRequestClient = deleteRequestClient;
            _updateRequestClient = updateRequestClient;
            _readUserByEmailRequestClient = readUserByEmailRequestClient;
            _loginRequestClient = loginRequestClient;
            _registerClient = registerClient;
            _readUserByIdRequestClient = readUserByIdRequestClient;
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
            if (string.IsNullOrEmpty(deleteModel.Email.Trim()))
            {
                ThrowException(nameof(EmailFormatException), EmptyEmail);
            }

            var responseContext = await _deleteRequestClient.GetResponse<CommunicationModel<DeleteUserModel>>(deleteModel);

            if (responseContext.Message.ExceptionName != null)
                ThrowException(responseContext.Message.ExceptionName, responseContext.Message.HumanReadableMessage);

            return responseContext.Message.Entity;
        }

        public async Task<LoginUserModel> LoginUser(UserLoginRequestGatewayToService loginModel)
        {
            if (string.IsNullOrEmpty(loginModel.Email.Trim()))
            {
                ThrowException(nameof(EmailFormatException), EmptyEmail);
            }
            if (string.IsNullOrEmpty(loginModel.Password.Trim()))
            {
                ThrowException(nameof(PasswordFormatException), EmptyPassword);
            }
            var responseContext = await _loginRequestClient.GetResponse<CommunicationModel<LoginUserModel>>(loginModel);

            if (responseContext.Message.ExceptionName != null)
                ThrowException(responseContext.Message.ExceptionName, responseContext.Message.HumanReadableMessage);

            return responseContext.Message.Entity;
        }

        public async Task<DataUserModel> ReadUserByEmail(string email)
        {
            var readUserByEmail = new UserReadByEmailRequestGatewayToService()
            {
                Email = email
            };

            var requestResponseContext = await _readUserByEmailRequestClient.GetResponse<CommunicationModel<DataUserModel>>(readUserByEmail);

            if (requestResponseContext.Message.ExceptionName != null)
                ThrowException(requestResponseContext.Message.ExceptionName, requestResponseContext.Message.HumanReadableMessage);

            var returnUser = requestResponseContext.Message.Entity;

            return returnUser;
        }

        public async Task<DataUserModel> ReadUserById(UserReadByIdRequestGatewayToService model)
        {
            var requestResponseContext = await _readUserByIdRequestClient.GetResponse<CommunicationModel<DataUserModel>>(model);
            if (requestResponseContext.Message.ExceptionName != null)
                ThrowException(requestResponseContext.Message.ExceptionName, requestResponseContext.Message.HumanReadableMessage);

            var returnUser = requestResponseContext.Message.Entity;
            return returnUser;
        }

        public async Task<RegisterUserResponse> RegisterUser(RegisterUserRequestGatewayToService registerModel)
        {
            if (registerModel is null)
                ThrowException(nameof(RequestNullException), NullRequest);

            var requestResponseContext = await _registerClient.GetResponse<CommunicationModel<RegisterUserResponse>>(registerModel);

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

            var requestReturnContext = await _updateRequestClient.GetResponse<CommunicationModel<DataUserModel>>(updateModel);

            if (requestReturnContext.Message.ExceptionName != null)
            {
                ThrowException(requestReturnContext.Message.ExceptionName, requestReturnContext.Message.HumanReadableMessage);
            }

            return requestReturnContext.Message.Entity;
        }

        
    }
}