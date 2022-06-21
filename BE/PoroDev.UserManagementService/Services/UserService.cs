﻿using AutoMapper;
using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Exceptions;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.Common.Models.UserModels.DeleteUser;
using PoroDev.Common.Models.UserModels.LoginUser;
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
using PoroDev.Common.Contracts.UserMenagement.ReadById;

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

        private readonly IMapper _mapper;

        public UserService(IRequestClient<UserCreateRequestServiceToDatabase> createRequestClient,
                           IRequestClient<UserReadByEmailRequestServiceToDatabase> readByEmailRequestClient,
                           IRequestClient<UserUpdateRequestServiceToDatabase> updateRequestClient,
                           IRequestClient<UserDeleteRequestServiceToDatabase> deleteUserRequestClient,
                           IRequestClient<RegisterUserRequestServiceToDatabase> registerUserClient,
                           IRequestClient<UserLoginRequestServiceToDatabase> loginUserRequestClient,
                           IRequestClient<UserReadByIdRequestServiceToDataBase> readByIdRequestClient,
                           IMapper mapper)
        {
            _createRequestClient = createRequestClient;
            _readUserByEmailClient = readByEmailRequestClient;
            _updateRequestClient = updateRequestClient;
            _deleteUserRequestclient = deleteUserRequestClient;
            _registerUserClient = registerUserClient;
            _loginUserRequestClient = loginUserRequestClient;
            _readUserByIdClient = readByIdRequestClient;
            _mapper = mapper;
        }

        public async Task<CommunicationModel<LoginUserModel>> LoginUser(UserLoginRequestGatewayToService userToLoginModel)
        {
            var modelToReturn = await _loginUserRequestClient.GetResponse<CommunicationModel<LoginUserModel>>(userToLoginModel);
            return modelToReturn.Message;
        }

        private void VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                if (!computeHash.SequenceEqual(passwordHash))
                    throw new FailedToLogInException(LOGIN_FAIL_ERROR);
            }
        }

        private void CheckPassword(string password)
        {
            if (password.Length < MIN_PASSWORD_LENGTH)
                throw new PasswordFormatException(PASSWORD_MIN_LENGTH_ERROR);
            if (!password.Any(char.IsUpper))
                throw new PasswordFormatException(PASSWORD_MIN_UPPERCASE_ERROR);

            if (!password.Any(char.IsLower))
                throw new PasswordFormatException(PASSWORD_MIN_LOWERCASE_ERROR);

            if (!password.Any(char.IsDigit))
                throw new PasswordFormatException(PASSWORD_MIN_NUMBER_ERROR);

            if (!CheckStringForCharacters(password, SPECIAL_CHARACTERS_STRING))
                throw new PasswordFormatException(PASSWORD_MIN_SPECIAL_ERROR);
        }

        private async Task CheckEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new EmailFormatException(EMAIL_EMPTY_ERROR);

            if (email.Length > MAX_EMAIL_LENGTH)
                throw new EmailFormatException(EMAIL_LENGTH_ERROR);

            var splitEmail = email.Split('@');

            if (splitEmail.Length != 2 || string.IsNullOrWhiteSpace(splitEmail[0]))
                throw new EmailFormatException(EMAIL_FORMAT_ERROR);

            if (splitEmail[0].Any(x => char.IsWhiteSpace(x)))
                throw new EmailFormatException(EMAIL_WHITESPACE_ERROR);

            if(CheckStringForCharacters(splitEmail[0],SPECIAL_CHARACTERS_EMAIL_STRING))
                throw new EmailFormatException(EMAIL_SPECIAL_CHARACTERS_ERROR);

            if (!splitEmail[1].Equals(EMAIL_DOMAIN))
                throw new EmailFormatException(EMAIL_DOMAIN_ERROR);

            if ((await _readUserByEmailClient.GetResponse<CommunicationModel<DataUserModel>>(new UserReadByEmailRequestServiceToDatabase() { Email = email})).Message.Entity != null)
                throw new EmailFormatException(EMAIL_EXISTS_ERROR);
        }

        private bool CheckStringForCharacters(string stringToCheck, string specialCharacters)
        {
            char[] specialChArray = specialCharacters.ToCharArray();
            bool flag = false;

            foreach (char ch in specialChArray)
            {
                if (stringToCheck.Contains(ch))
                    flag = true;
            }
            return flag;
        }

        private void GetHashAndSalt(string password, out byte[] salt, out byte[] hash)
        {
            using (var hmac = new HMACSHA512())
            {
                salt = hmac.Key;
                hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private void CheckFullName(string name, string lastname)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(lastname) || name.Equals(string.Empty) || lastname.Equals(string.Empty))
                throw new FullNameFormatException(FULLNAME_EMPTY_ERROR);

            if (name.Length > MAX_NAME_AND_LASTNAME_LENGTH || lastname.Length > MAX_NAME_AND_LASTNAME_LENGTH)
                throw new FullNameFormatException(FULLNAME_TOO_LONG_ERROR);

            if (name.Any(x => char.IsWhiteSpace(x)) || lastname.Any(x => char.IsWhiteSpace(x)))
                throw new FullNameFormatException(FULLNAME_WHITESPACE_ERROR);

            if (name.Any(x => char.IsNumber(x)) || lastname.Any(x => char.IsNumber(x)))
                throw new FullNameFormatException(FULLNAME_NUMBER_ERROR);

            if (CheckStringForCharacters(name, SPECIAL_CHARACTERS_STRING) || CheckStringForCharacters(lastname, SPECIAL_CHARACTERS_STRING))
                throw new FullNameFormatException(FULLNAME_SPECIAL_CHARACTER_ERORR);
        }

        private void CheckPosition(string position)
        {
            if (string.IsNullOrWhiteSpace(position) || position.Equals(string.Empty))
                throw new PositionFormatException(POSITION_EMPTY_ERROR);

            if (position.Length > MAX_POSITION_LENGTH)
                throw new PositionFormatException(POSITION_TOO_LONG_ERROR);

            if (position.Any(x => char.IsNumber(x)))
                throw new PositionFormatException(POSITION_NUMBER_ERROR);

            if (CheckStringForCharacters(position, SPECIAL_CHARACTERS_STRING))
                throw new PositionFormatException(POSITION_SPECIAL_CHARACTER_ERROR);
        }

        private async Task<CommunicationModel<RegisterUserResponse>> CheckUserFields(RegisterUserRequestGatewayToService registerModel)
        {
            try
            {
                CheckFullName(registerModel.Name, registerModel.Lastname);
                await CheckEmail(registerModel.Email);
                CheckPassword(registerModel.Password);
                CheckPosition(registerModel.Position);
            }
            catch (EmailFormatException ex) 
            {

                string exceptionType = nameof(EmailFormatException);
                string humanReadableMessage = ex.HumanReadableErrorMessage;

                var responseException = new CommunicationModel<RegisterUserResponse>()
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

                var responseException = new CommunicationModel<RegisterUserResponse>()
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

                var responseException = new CommunicationModel<RegisterUserResponse>()
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

                var responseException = new CommunicationModel<RegisterUserResponse>()
                {
                    Entity = null,
                    ExceptionName = exceptionType,
                    HumanReadableMessage = humanReadableMessage
                };

                return responseException;
            }

            return new CommunicationModel<RegisterUserResponse>() { Entity = null, ExceptionName = null, HumanReadableMessage = null };
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
                string exceptionType = nameof(IdFormatexception);
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

        public async Task<CommunicationModel<DataUserModel>> UpdateUser(UserUpdateRequestGatewayToService model)
        {
            GetHashAndSalt(model.PasswordUnhashed, out byte[] salt, out byte[] hash);

            var updateUserRequest = _mapper.Map<UserUpdateRequestServiceToDatabase>(model);
            updateUserRequest.Password = hash;
            updateUserRequest.Salt = salt;

            var response = await _updateRequestClient.GetResponse<CommunicationModel<DataUserModel>>(updateUserRequest);

            return response.Message;
        }

        public async Task<CommunicationModel<RegisterUserResponse>> RegisterUser(RegisterUserRequestGatewayToService registerModel)
        {
            var isException = await CheckUserFields(registerModel);

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

        
    }
}