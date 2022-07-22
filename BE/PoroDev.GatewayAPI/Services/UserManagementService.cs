using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.UserManagement.Create;
using PoroDev.Common.Contracts.UserManagement.DeleteAllUsers;
using PoroDev.Common.Contracts.UserManagement.DeleteUser;
using PoroDev.Common.Contracts.UserManagement.LoginUser;
using PoroDev.Common.Contracts.UserManagement.Query;
using PoroDev.Common.Contracts.UserManagement.ReadById;
using PoroDev.Common.Contracts.UserManagement.ReadByIdWithRuntime;
using PoroDev.Common.Contracts.UserManagement.ReadUser;
using PoroDev.Common.Contracts.UserManagement.Update;
using PoroDev.Common.Contracts.UserManagement.Verify;
using PoroDev.Common.Exceptions;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.Common.Models.UserModels.DeleteUser;
using PoroDev.Common.Models.UserModels.LoginUser;
using PoroDev.Common.Models.UserModels.RegisterUser;
using PoroDev.GatewayAPI.Services.Contracts;
using static PoroDev.GatewayAPI.Constants.Constats;
using static PoroDev.GatewayAPI.Helpers.ExceptionFactory;

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
        private readonly IRequestClient<UserReadByIdWithRuntimeRequestGatewayToService> _readUserByIdWithRuntimedataRequestClient;
        private readonly IRequestClient<UserDeleteAllRequestGatewayToService> _deleteAllRequestClient;
        private readonly IRequestClient<VerifyEmailRequestGatewayToService> _verifyUserRequestClient;
        private readonly IRequestClient<QueryAllUsersRequestGatewayToService> _queryAllUsers;

        public UserManagementService(
            IRequestClient<UserCreateRequestGatewayToService> createRequestClient,
            IRequestClient<UserReadByEmailRequestGatewayToService> readUserByEmailRequestClient,
            IRequestClient<UserLoginRequestGatewayToService> loginRequestClient,
            IRequestClient<UserDeleteRequestGatewayToService> deleteRequestClient,
            IRequestClient<UserUpdateRequestGatewayToService> updateRequestClient,
            IRequestClient<RegisterUserRequestGatewayToService> registerClient,
            IRequestClient<UserReadByIdWithRuntimeRequestGatewayToService> readUserByIdWithRuntimedataRequestClient,
            IRequestClient<UserReadByIdRequestGatewayToService> readUserByIdRequestClient,
            IRequestClient<UserDeleteAllRequestGatewayToService> deleteAllUsers,
            IRequestClient<QueryAllUsersRequestGatewayToService> queryAllUsers,
            IRequestClient<VerifyEmailRequestGatewayToService> verifyUserRequestClient
            )
        {
            _createRequestClient = createRequestClient;
            _deleteRequestClient = deleteRequestClient;
            _updateRequestClient = updateRequestClient;
            _readUserByEmailRequestClient = readUserByEmailRequestClient;
            _loginRequestClient = loginRequestClient;
            _registerClient = registerClient;
            _readUserByIdRequestClient = readUserByIdRequestClient;
            _deleteAllRequestClient = deleteAllUsers;
            _queryAllUsers = queryAllUsers;
            _readUserByIdWithRuntimedataRequestClient = readUserByIdWithRuntimedataRequestClient;
            _verifyUserRequestClient = verifyUserRequestClient;
        }

        public async Task<DataUserModel> CreateUser(UserCreateRequestGatewayToService createModel)
        {
            var requestReturnContext = await _createRequestClient.GetResponse<CommunicationModel<DataUserModel>>(createModel, CancellationToken.None, RequestTimeout.After(m: 5));

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

            var responseContext = await _deleteRequestClient.GetResponse<CommunicationModel<DeleteUserModel>>(deleteModel, CancellationToken.None, RequestTimeout.After(m: 5));

            if (responseContext.Message.ExceptionName != null)
                ThrowException(responseContext.Message.ExceptionName, responseContext.Message.HumanReadableMessage);

            return responseContext.Message.Entity;
        }

        public async Task<DeleteUserModel> DeleteAllUsers(UserDeleteAllRequestGatewayToService model)
        {
            var responseContext = await _deleteAllRequestClient.GetResponse<CommunicationModel<DeleteUserModel>>(model, CancellationToken.None, RequestTimeout.After(m: 5));

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
            var responseContext = await _loginRequestClient.GetResponse<CommunicationModel<LoginUserModel>>(loginModel, CancellationToken.None, RequestTimeout.After(m: 5));

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

            var requestResponseContext = await _readUserByEmailRequestClient.GetResponse<CommunicationModel<DataUserModel>>(readUserByEmail, CancellationToken.None, RequestTimeout.After(m: 5));

            if (requestResponseContext.Message.ExceptionName != null)
                ThrowException(requestResponseContext.Message.ExceptionName, requestResponseContext.Message.HumanReadableMessage);

            var returnUser = requestResponseContext.Message.Entity;

            return returnUser;
        }

        public async Task<DataUserModel> ReadUserById(UserReadByIdRequestGatewayToService model)
        {
            var requestResponseContext = await _readUserByIdRequestClient.GetResponse<CommunicationModel<DataUserModel>>(model, CancellationToken.None, RequestTimeout.After(m: 5));
            if (requestResponseContext.Message.ExceptionName != null)
                ThrowException(requestResponseContext.Message.ExceptionName, requestResponseContext.Message.HumanReadableMessage);

            var returnUser = requestResponseContext.Message.Entity;
            return returnUser;
        }

        public async Task<DataUserModel> ReadUserByIdWithRuntimeData(UserReadByIdWithRuntimeRequestGatewayToService readModel)
        {
            var requestResponseContext = await _readUserByIdWithRuntimedataRequestClient.GetResponse<CommunicationModel<DataUserModel>>(readModel, CancellationToken.None, RequestTimeout.After(m: 5));
            if (requestResponseContext.Message.ExceptionName != null)
                ThrowException(requestResponseContext.Message.ExceptionName, requestResponseContext.Message.HumanReadableMessage);

            var returnUser = requestResponseContext.Message.Entity;
            return returnUser;
        }

        public async Task<RegisterUserResponse> RegisterUser(RegisterUserRequestGatewayToService registerModel)
        {
            if (registerModel is null)
                ThrowException(nameof(RequestNullException), NullRequest);

            var requestResponseContext = await _registerClient.GetResponse<CommunicationModel<RegisterUserResponse>>(registerModel, CancellationToken.None, RequestTimeout.After(m: 5));

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

            var requestReturnContext = await _updateRequestClient.GetResponse<CommunicationModel<DataUserModel>>(updateModel, CancellationToken.None, RequestTimeout.After(m: 5));

            if (requestReturnContext.Message.ExceptionName != null)
            {
                ThrowException(requestReturnContext.Message.ExceptionName, requestReturnContext.Message.HumanReadableMessage);
            }

            return requestReturnContext.Message.Entity;
        }

        public async Task<DataUserModel> VerifyEmail(VerifyEmailRequestGatewayToService verifyModel)
        {
            if (string.IsNullOrEmpty(verifyModel.Token.Trim()))
            {
                ThrowException(nameof(InvalidVerificationTokenException), InvalidToken);
            }

            var requestResponseContext = await _verifyUserRequestClient.GetResponse<CommunicationModel<DataUserModel>>(verifyModel);

            if (requestResponseContext.Message.ExceptionName != null)
            {
                ThrowException(requestResponseContext.Message.ExceptionName, requestResponseContext.Message.HumanReadableMessage);
            }

            return requestResponseContext.Message.Entity;
        }

        public async Task<List<DataUserModel>> QueryAll()
        {
            var responseContext = await _queryAllUsers.GetResponse<CommunicationModel<List<DataUserModel>>>(new QueryAllUsersRequestGatewayToService());

            if (responseContext.Message.ExceptionName != null)
                ThrowException(responseContext.Message.ExceptionName, responseContext.Message.HumanReadableMessage);

            return responseContext.Message.Entity;
        }
    }
}