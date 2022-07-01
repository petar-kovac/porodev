using AutoMapper;
using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.UserManagement.Create;
using PoroDev.Common.Contracts.UserManagement.DeleteUser;
using PoroDev.Common.Contracts.UserManagement.LoginUser;
using PoroDev.Common.Contracts.UserManagement.ReadById;
using PoroDev.Common.Contracts.UserManagement.ReadUser;
using PoroDev.Common.Contracts.UserManagement.Update;
using PoroDev.Common.Exceptions;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.Common.Models.UserModels.DeleteUser;
using PoroDev.Common.Models.UserModels.LoginUser;
using PoroDev.Common.Models.UserModels.RegisterUser;
using PoroDev.UserManagementService.Services.Contracts;
using System.Security.Cryptography;
using static PoroDev.Common.Extensions.CreateResponseExtension;
using static PoroDev.UserManagementService.Constants.Consts;
using PoroDev.Common.Models.UserModels.RegisterUser;
using PoroDev.Common.Contracts.UserManagement.Create;
using PoroDev.Common.Contracts.UserManagement.Update;
using PoroDev.Common.Contracts.UserManagement.DeleteUser;
using PoroDev.Common.Contracts.UserManagement.LoginUser;
using PoroDev.Common.Contracts.UserManagement.ReadUser;
using PoroDev.Common.Contracts.UserManagement.ReadById;
using PoroDev.Common.Contracts.UserManagement.ReadByIdWithRuntime;
using PoroDev.Common.Contracts.UserManagement.DeleteAllUsers;

namespace PoroDev.UserManagementService.Services
{
    public class UserService : IUserService
    {
        private readonly IRequestClient<UserCreateRequestServiceToDatabase> _createRequestClient;
        private readonly IRequestClient<UserReadByEmailRequestServiceToDatabase> _readUserByEmailClient;
        private readonly IRequestClient<UserReadByIdRequestServiceToDataBase> _readUserByIdClient;
        private readonly IRequestClient<UserUpdateRequestServiceToDatabase> _updateRequestClient;
        private readonly IRequestClient<UserDeleteRequestServiceToDatabase> _deleteUserRequestclient;
        private readonly IRequestClient<RegisterUserRequestServiceToDatabase> _registerUserClient;
        private readonly IRequestClient<UserLoginRequestServiceToDatabase> _loginUserRequestClient;
        private readonly IRequestClient<UserReadByIdWithRuntimeRequestServiceToDataBase> _readUserByIdWithRuntimeClient;
        private readonly IRequestClient<UserDeleteAllRequestServiceToDataBase> _deleteAllUserRequestclient;

        private readonly IMapper _mapper;

        public UserService(IRequestClient<UserCreateRequestServiceToDatabase> createRequestClient,
                           IRequestClient<UserReadByEmailRequestServiceToDatabase> readByEmailRequestClient,
                           IRequestClient<UserUpdateRequestServiceToDatabase> updateRequestClient,
                           IRequestClient<UserDeleteRequestServiceToDatabase> deleteUserRequestClient,
                           IRequestClient<RegisterUserRequestServiceToDatabase> registerUserClient,
                           IRequestClient<UserLoginRequestServiceToDatabase> loginUserRequestClient,
                           IRequestClient<UserReadByIdRequestServiceToDataBase> readByIdRequestClient,
                           IRequestClient<UserReadByIdWithRuntimeRequestServiceToDataBase> readUserByIdWithRuntimeClient,
                           IRequestClient<UserDeleteAllRequestServiceToDataBase> deleteAllUsersRequestClient,
                           IMapper mapper)
        {
            _createRequestClient = createRequestClient;
            _readUserByEmailClient = readByEmailRequestClient;
            _updateRequestClient = updateRequestClient;
            _deleteUserRequestclient = deleteUserRequestClient;
            _registerUserClient = registerUserClient;
            _loginUserRequestClient = loginUserRequestClient;
            _readUserByIdClient = readByIdRequestClient;
            _readUserByIdWithRuntimeClient = readUserByIdWithRuntimeClient;
            _deleteAllUserRequestclient = deleteAllUsersRequestClient;
            _mapper = mapper;
        }

        public async Task<CommunicationModel<LoginUserModel>> LoginUser(UserLoginRequestGatewayToService userToLoginModel)
        {
            var modelToReturn = await _loginUserRequestClient.GetResponse<CommunicationModel<LoginUserModel>>(userToLoginModel);
            return modelToReturn.Message;
        }

        private void GetHashAndSalt(string password, out byte[] salt, out byte[] hash)
        {
            using (var hmac = new HMACSHA512())
            {
                salt = hmac.Key;
                hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<CommunicationModel<DataUserModel>> CreateUser(UserCreateRequestGatewayToService model)
        {
            if (model.Email.Equals(String.Empty) || String.IsNullOrWhiteSpace(model.Email))
            {
                string exceptionType = nameof(EmailFormatException);
                string humanReadableMessage = "Email cannot be empty!";

                var responseException = new CommunicationModel<DataUserModel>()
                {
                    Entity = null,
                    ExceptionName = exceptionType,
                    HumanReadableMessage = humanReadableMessage
                };

                return responseException;
            }

            var exists = await _readUserByEmailClient.GetResponse<CommunicationModel<DataUserModel>>(new UserReadByEmailRequestServiceToDatabase() { Email = model.Email });

            if (exists.Message.Entity != null)
            {
                string exceptionType = nameof(UserExistsException);
                string humanReadableMessage = "User with that email already exists";

                var responseException = new CommunicationModel<DataUserModel>()
                {
                    Entity = null,
                    ExceptionName = exceptionType,
                    HumanReadableMessage = humanReadableMessage
                };

                return responseException;
            }

            GetHashAndSalt(model.PasswordUnhashed, out byte[] salt, out byte[] hash);

            var modelToCreate = _mapper.Map<UserCreateRequestServiceToDatabase>(model);

            modelToCreate.Password = hash;
            modelToCreate.Salt = salt;

            var response = await _createRequestClient.GetResponse<CommunicationModel<DataUserModel>>(modelToCreate);

            return response.Message;
        }

        public async Task<CommunicationModel<DeleteUserModel>> DeleteUser(UserDeleteRequestGatewayToService model)
        {
            var response = await _deleteUserRequestclient.GetResponse<CommunicationModel<DeleteUserModel>>(model);
            return response.Message;
        }

        public async Task<CommunicationModel<DeleteUserModel>> DeleteAllUsers(UserDeleteAllRequestGatewayToService model)
        {
            var response = await _deleteAllUserRequestclient.GetResponse<CommunicationModel<DeleteUserModel>>(model);
            return response.Message;
        }

        public async Task<CommunicationModel<DataUserModel>> ReadUserByEmail(UserReadByEmailRequestGatewayToService model)
        {
            if (model.Email.Equals(String.Empty) || String.IsNullOrWhiteSpace(model.Email))
            {
                string exceptionType = nameof(EmailFormatException);
                string humanReadableMessage = "Email cannot be empty!";

                var responseException = CreateResponseModel<CommunicationModel<DataUserModel>, DataUserModel>(exceptionType, humanReadableMessage);

                return responseException;
            }

            UserReadByEmailRequestServiceToDatabase readUser = new()
            {
                Email = model.Email
            };

            var response = await _readUserByEmailClient.GetResponse<CommunicationModel<DataUserModel>>(readUser);

            return response.Message;
        }

        public async Task<CommunicationModel<DataUserModel>> ReadUserById(UserReadByIdRequestGatewayToService model)
        {
            if (model.Id.ToString().Equals(String.Empty) || String.IsNullOrWhiteSpace(model.Id.ToString()))
            {
                string exceptionType = nameof(IdFormatException);
                string humanReadableMessage = "Id cannot be empty!";

                var responseException = CreateResponseModel<CommunicationModel<DataUserModel>, DataUserModel>(exceptionType, humanReadableMessage);

                return responseException;
            }

            UserReadByIdRequestServiceToDataBase readUser = new()
            {
                Id = model.Id
            };

            var response = await _readUserByIdClient.GetResponse<CommunicationModel<DataUserModel>>(readUser);
            return response.Message;
        }

        public async Task<CommunicationModel<DataUserModel>> ReadUserByIdWithRuntimeData(UserReadByIdWithRuntimeRequestGatewayToService model)
        {
            if (model.Id.ToString().Equals(String.Empty) || String.IsNullOrWhiteSpace(model.Id.ToString()))
            {
                string exceptionType = nameof(IdFormatException);
                string humanReadableMessage = "Id cannot be empty!";

                var responseException = CreateResponseModel<CommunicationModel<DataUserModel>, DataUserModel>(exceptionType, humanReadableMessage);

                return responseException;
            }

            var response = await _readUserByIdWithRuntimeClient.GetResponse<CommunicationModel<DataUserModel>>(new UserReadByIdRequestServiceToDataBase() { Id = model.Id});
            return response.Message;

        }

        public async Task<CommunicationModel<DataUserModel>> UpdateUser(UserUpdateRequestGatewayToService model)
        {
            var isException = await CheckUserFieldsUpdate(model);

            if (isException.ExceptionName != null)
            {
                return isException;
            }

            GetHashAndSalt(model.PasswordUnhashed, out byte[] salt, out byte[] hash);

            var updateUserRequest = _mapper.Map<UserUpdateRequestServiceToDatabase>(model);
            updateUserRequest.Password = hash;
            updateUserRequest.Salt = salt;

            var response = await _updateRequestClient.GetResponse<CommunicationModel<DataUserModel>>(updateUserRequest);

            return response.Message;
        }

        public async Task<CommunicationModel<RegisterUserResponse>> RegisterUser(RegisterUserRequestGatewayToService registerModel)
        {
            var isException = await Helpers.UserManagementValidator.Validate(registerModel, _readUserByEmailClient);

            if (isException.ExceptionName != null)
                return isException;

            GetHashAndSalt(registerModel.Password, out byte[] salt, out byte[] hash);

            var userToRegister = _mapper.Map<RegisterUserRequestServiceToDatabase>(registerModel);
            userToRegister.Salt = salt;
            userToRegister.Password = hash;

            var requestResponseContext = await _registerUserClient.GetResponse<CommunicationModel<DataUserModel>>(userToRegister);

            var returnContext = _mapper.Map<CommunicationModel<RegisterUserResponse>>(requestResponseContext.Message);

            return returnContext;
        }

        private async Task<CommunicationModel<DataUserModel>> CheckUserFieldsUpdate(UserUpdateRequestGatewayToService updateModel)
        {
            try
            {
                CheckFullName(updateModel.Name, updateModel.Lastname);
                await CheckEmailUpdate(updateModel.Email);
                CheckPassword(updateModel.PasswordUnhashed);
                CheckPosition(updateModel.Position);
            }
            catch (EmailFormatException ex)
            {

                string exceptionType = nameof(EmailFormatException);
                string humanReadableMessage = ex.HumanReadableErrorMessage;

                var responseException = new CommunicationModel<DataUserModel>()
                {
                    Entity = null,
                    ExceptionName = exceptionType,
                    HumanReadableMessage = humanReadableMessage
                };

                return responseException;
            }
            catch (PasswordFormatException ex)
            {
                string exceptionType = nameof(PasswordFormatException);
                string humanReadableMessage = ex.HumanReadableErrorMessage;

                var responseException = new CommunicationModel<DataUserModel>()
                {
                    Entity = null,
                    ExceptionName = exceptionType,
                    HumanReadableMessage = humanReadableMessage
                };

                return responseException;
            }
            catch (FullNameFormatException ex)
            {
                string exceptionType = nameof(FullNameFormatException);
                string humanReadableMessage = ex.HumanReadableErrorMessage;

                var responseException = new CommunicationModel<DataUserModel>()
                {
                    Entity = null,
                    ExceptionName = exceptionType,
                    HumanReadableMessage = humanReadableMessage
                };

                return responseException;
            }
            catch (PositionFormatException ex)
            {
                string exceptionType = nameof(PositionFormatException);
                string humanReadableMessage = ex.HumanReadableErrorMessage;

                var responseException = new CommunicationModel<DataUserModel>()
                {
                    Entity = null,
                    ExceptionName = exceptionType,
                    HumanReadableMessage = humanReadableMessage
                };

                return responseException;
            }

            return new CommunicationModel<DataUserModel>() { Entity = null, ExceptionName = null, HumanReadableMessage = null };
        }

        private async Task CheckEmailUpdate(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new EmailFormatException(EMAIL_EMPTY_ERROR);
            }

            if (email.Length > MAX_EMAIL_LENGTH)
            {
                throw new EmailFormatException(EMAIL_LENGTH_ERROR);
            }

            var splitEmail = email.Split('@');

            if (splitEmail.Length != 2 || string.IsNullOrWhiteSpace(splitEmail[0]))
            {
                throw new EmailFormatException(EMAIL_FORMAT_ERROR);
            }

            if (splitEmail[0].Any(x => char.IsWhiteSpace(x)))
            {
                throw new EmailFormatException(EMAIL_WHITESPACE_ERROR);
            }

            if (CheckStringForCharacters(splitEmail[0], SPECIAL_CHARACTERS_EMAIL_STRING))
            {
                throw new EmailFormatException(EMAIL_SPECIAL_CHARACTERS_ERROR);
            }

            if (!splitEmail[1].Equals(EMAIL_DOMAIN))
            {
                throw new EmailFormatException(EMAIL_DOMAIN_ERROR);
            }

        }


    }
}