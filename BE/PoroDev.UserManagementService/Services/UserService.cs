using MassTransit;
using PoroDev.Common.Contracts.Create;
using PoroDev.Common.Contracts.ReadUser;
using PoroDev.Common.Contracts.Update;
using PoroDev.Common.Exceptions;
using PoroDev.Common.Contracts.DeleteUser;
using PoroDev.Common.Enums;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.UserManagementService.Services.Contracts;
using System.Security.Cryptography;
using static PoroDev.Common.Extensions.CreateResponseExtension;
using static PoroDev.UserManagementService.Constants.Consts;
using PoroDev.Common.Contracts;
using PoroDev.Common.Models.UserModels.DeleteUser;
using AutoMapper;
using PoroDev.Common.Models.UserModels.RegisterUser;

namespace PoroDev.UserManagementService.Services
{
    public class UserService : IUserService
    {
        private readonly IRequestClient<UserCreateRequestServiceToDatabase> _createRequestClient;
        private readonly IRequestClient<UserReadByEmailRequestServiceToDatabase> _readUserByEmailClient;
        private readonly IRequestClient<UserUpdateRequestServiceToDatabase> _updateRequestClient;
        private readonly IRequestClient<UserDeleteRequestServiceToDatabase> _deleteUserRequestclient;
        private readonly IRequestClient<RegisterUserRequestServiceToDatabase> _registerUserClient;

        private readonly IMapper _mapper;

        public UserService(IRequestClient<UserCreateRequestServiceToDatabase> createRequestClient,
                           IRequestClient<UserReadByEmailRequestServiceToDatabase> readByEmailRequestClient,
                           IRequestClient<UserUpdateRequestServiceToDatabase> updateRequestClient,
                           IRequestClient<UserDeleteRequestServiceToDatabase> deleteUserRequestClient,
                           IRequestClient<RegisterUserRequestServiceToDatabase> registerUserClient,
                           IMapper mapper)
        {
            _createRequestClient = createRequestClient;
            _readUserByEmailClient = readByEmailRequestClient;
            _updateRequestClient = updateRequestClient;
            _deleteUserRequestclient = deleteUserRequestClient;
            _registerUserClient = registerUserClient;
            _mapper = mapper;
            
        }

        //public async Task<UserLoginResponseModel> Login(UserLoginRequestModel loginModel)
        //{
        //    DataUserModel dataUserModel;
        //    try
        //    {
        //        dataUserModel = await GetUserByMail(loginModel.Email);
        //    }
        //    catch (Exception)
        //    {
        //        throw new FailedToLogInException("Login credentials don't match");
        //    }
        //    VerifyPasswordHash(loginModel.Password, dataUserModel.Password, dataUserModel.Salt);
        //    UserLoginResponseModel response = _mapper.Map<UserLoginResponseModel>(dataUserModel);
        //    response.Jwt = CreateToken(dataUserModel);
        //    return response;
        //}

        //public string CreateToken(DataUserModel user)
        //{
        //    List<Claim> claims = new List<Claim>
        //    {
        //        new Claim("Id", user.Id.ToString()),
        //    };
        //    var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(SECRET_KEY));
        //    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        //    var token = new JwtSecurityToken(
        //        claims: claims,
        //        expires: DateTime.Now.AddHours(1),
        //        signingCredentials: creds);
        //    var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        //    return jwt;
        //}

        private void VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                if (!computeHash.SequenceEqual(passwordHash))
                    throw new FailedToLogInException("Email or password is not valid.");
            }
        }

        private void CheckPassword(string password)
        {
            if (password.Length < MIN_PASSWORD_LENGTH)
                

            if (!password.Any(char.IsUpper))
                throw new PasswordFormatException($"Password must contain at least {MIN_UPPERCASE_LETTERS} uppercase letter!");

            if (!password.Any(char.IsLower))
                throw new PasswordFormatException($"Password must contain at least {MIN_LOWERCASE_LETTERS} lowercase letter!");

            if (!password.Any(char.IsDigit))
                throw new PasswordFormatException($"Password must contain at least {MIN_NUMBERS} number!");

            if (!CheckForSpecialCharacters(password))
                throw new PasswordFormatException($"Password must contain at least {MIN_SPECIAL_CHARACTERS} special character!");
        }

        private async Task CheckEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new EmailFormatException($"Email cannot be empty!");

            if (email.Length > MAX_EMAIL_LENGTH)
                throw new EmailFormatException($"Email cannot contain more than {MAX_EMAIL_LENGTH} characters!");

            var splitEmail = email.Split('@');

            if (splitEmail.Length != 2 || string.IsNullOrWhiteSpace(splitEmail[0]))
                throw new EmailFormatException("Email format is invalid!");

            if (splitEmail[0].Any(x => char.IsWhiteSpace(x)))
                throw new EmailFormatException("Email cannot contain whitespace!");

            CheckEmailSpecialCharaters(splitEmail[0]);

            if (!splitEmail[1].Equals(EMAIL_DOMAIN))
                throw new EmailFormatException("Email domain is invalid!");

            if ((await _readUserByEmailClient.GetResponse<CommunicationModel<DataUserModel>>(new UserReadByEmailRequestServiceToDatabase() { Email = email})).Message.Entity != null)
                throw new EmailFormatException("User with that email already exists!");
        }

        private void CheckEmailSpecialCharaters(string emailUsername)
        {
            string specialCh = @"%!@#$%^&*()?/><,:;'\|}]{[~`+=" + "\"";
            char[] specialChArray = specialCh.ToCharArray();
            bool flag = false;

            foreach (char ch in specialChArray)
            {
                if (emailUsername.Contains(ch))
                    flag = true;
            }

            if (flag)
                throw new EmailFormatException("Email can only contain '.', '-' and '_' characters!");
        }

        private bool CheckForSpecialCharacters(string stringToCheck)
        {
            string specialCh = @"%!@#$%^&*()?/>.<,:;'\|}]{[_~`+=-" + "\"";
            char[] specialChArray = specialCh.ToCharArray();
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
                throw new FullNameFormatException($"Name or lastname cannot be empty!");

            if (name.Length > MAX_NAME_AND_LASTNAME_LENGTH || lastname.Length > MAX_NAME_AND_LASTNAME_LENGTH)
                throw new FullNameFormatException($"Name or lastname cannot exceed {MAX_NAME_AND_LASTNAME_LENGTH} characters!");

            if (name.Any(x => char.IsWhiteSpace(x)) || lastname.Any(x => char.IsWhiteSpace(x)))
                throw new FullNameFormatException("Name or lastname cannot contain whitespace!");

            if (name.Any(x => char.IsNumber(x)) || lastname.Any(x => char.IsNumber(x)))
                throw new FullNameFormatException("Name or lastname cannot contain numbers!");

            if (CheckForSpecialCharacters(name) || CheckForSpecialCharacters(lastname))
                throw new FullNameFormatException("Name or lastname cannot contain special characters!");
        }

        private void CheckPosition(string position)
        {
            if (string.IsNullOrWhiteSpace(position) || position.Equals(string.Empty))
                throw new PositionFormatException($"Postion cannot be empty!");

            if (position.Length > MAX_POSITION_LENGTH)
                throw new PositionFormatException($"Postion cannot exceed {MAX_POSITION_LENGTH} characters!");

            if (position.Any(x => char.IsNumber(x)))
                throw new PositionFormatException("Position cannot contain numbers!");

            if (CheckForSpecialCharacters(position))
                throw new PositionFormatException("Position cannot contain special characters!");
        }

        private async Task<CommunicationModel<RegisterUserResponse>> CheckUserFields(RegisterUserRequestGatewayToService registerModel)
        {
            try
            {
                await CheckEmail(registerModel.Email);
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

            try
            {
                CheckPassword(registerModel.Password);
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

            try
            {
                CheckFullName(registerModel.Name, registerModel.Lastname);
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

            try
            {
                CheckPosition(registerModel.Position);
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

                var responseException = new CommunicationModel<DataUserModel>() {
                    Entity = null,
                    ExceptionName = exceptionType,
                    HumanReadableMessage = humanReadableMessage
                };

                return responseException;
            }

            var exists = await _readUserByEmailClient.GetResponse<CommunicationModel<DataUserModel>>(new UserReadByEmailRequestServiceToDatabase() {  Email = model.Email });

            if(exists.Message.Entity != null)
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

        public async Task<UserUpdateResponseDatabaseToService> UpdateUser(UserUpdateRequestGatewayToService model)
        {
            GetHashAndSalt(model.PasswordUnhashed, out byte[] salt, out byte[] hash);

            UserUpdateRequestServiceToDatabase temp = new UserUpdateRequestServiceToDatabase()
            {
                AvatarUrl = model.AvatarUrl,
                Department = model.Department,
                Email = model.Email,
                Lastname = model.Lastname,
                Name = model.Name,
                Position = model.Position,
                Role = model.Role,
                Password = hash,
                Salt = salt,
            };

            var response = await _updateRequestClient.GetResponse<UserUpdateResponseDatabaseToService>(temp);

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