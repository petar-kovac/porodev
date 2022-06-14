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
using PoroDev.Common.Contracts;
using PoroDev.Common.Models.UserModels.DeleteUser;
using AutoMapper;

namespace PoroDev.UserManagementService.Services
{
    public class UserService : IUserService
    {
        private readonly IRequestClient<UserCreateRequestServiceToDatabase> _createRequestClient;
        private readonly IRequestClient<UserReadByEmailRequestServiceToDatabase> _readUserByEmailClient;
        private readonly IRequestClient<UserUpdateRequestServiceToDatabase> _updateRequestClient;
        private readonly IRequestClient<UserDeleteRequestServiceToDatabase> _deleteUserRequestclient;

        private readonly IMapper _mapper;

        private const int MIN_PASSWORD_LENGTH = 8;
        private const string EMAIL_DOMAIN = "boing.rs";
        private const int MIN_UPPERCASE_LETTERS = 1;
        private const int MIN_LOWERCASE_LETTERS = 1;
        private const int MIN_NUMBERS = 1;
        private const int MIN_SPECIAL_CHARACTERS = 1;
        private const int MAX_EMAIL_LENGTH = 50;
        private const int MAX_NAME_AND_LASTNAME_LENGTH = 20;
        private const int MAX_POSITION_LENGTH = 50;
        private const string SECRET_KEY = "this is a custom Secret Key for authentication";

        public UserService(IRequestClient<UserCreateRequestServiceToDatabase> createRequestClient,
                           IRequestClient<UserReadByEmailRequestServiceToDatabase> readByEmailRequestClient,
                           IRequestClient<UserUpdateRequestServiceToDatabase> updateRequestClient,
                           IRequestClient<UserDeleteRequestServiceToDatabase> deleteUserRequestClient,
                           IMapper mapper)
        {
            _createRequestClient = createRequestClient;
            _readUserByEmailClient = readByEmailRequestClient;
            _updateRequestClient = updateRequestClient;
            _deleteUserRequestclient = deleteUserRequestClient;
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

        //private void VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        //{
        //    using (var hmac = new HMACSHA512(passwordSalt))
        //    {
        //        var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        //        if (!computeHash.SequenceEqual(passwordHash))
        //            throw new FailedToLogInException("Email or password is not valid.");
        //    }
        //}

        //private void CheckPassword(string password)
        //{
        //    if (password.Length < MIN_PASSWORD_LENGTH)
        //        throw new PasswordFormatException($"Password must be at least {MIN_PASSWORD_LENGTH} characters!");

        //    if (!password.Any(char.IsUpper))
        //        throw new PasswordFormatException($"Password must contain at least {MIN_UPPERCASE_LETTERS} uppercase letter!");

        //    if (!password.Any(char.IsLower))
        //        throw new PasswordFormatException($"Password must contain at least {MIN_LOWERCASE_LETTERS} lowercase letter!");

        //    if (!password.Any(char.IsDigit))
        //        throw new PasswordFormatException($"Password must contain at least {MIN_NUMBERS} number!");

        //    if (!CheckForSpecialCharacters(password))
        //        throw new PasswordFormatException($"Password must contain at least {MIN_SPECIAL_CHARACTERS} special character!");
        //}

        //private async Task CheckEmail(string email)
        //{
        //    if (string.IsNullOrWhiteSpace(email))
        //        throw new EmailFormatException($"Email cannot be empty!");

        //    if (email.Length > MAX_EMAIL_LENGTH)
        //        throw new EmailFormatException($"Email cannot contain more than {MAX_EMAIL_LENGTH} characters!");

        //    var splitEmail = email.Split('@');

        //    if (splitEmail.Length != 2 || string.IsNullOrWhiteSpace(splitEmail[0]))
        //        throw new EmailFormatException("Email format is invalid!");

        //    if (splitEmail[0].Any(x => char.IsWhiteSpace(x)))
        //        throw new EmailFormatException("Email cannot contain whitespace!");

        //    CheckEmailSpecialCharaters(splitEmail[0]);

        //    if (!splitEmail[1].Equals(EMAIL_DOMAIN))
        //        throw new EmailFormatException("Email domain is invalid!");

        //    if (await _unitOfWork.Users.FindSingleAsync(user => user.Email.Equals(email)) != null)
        //        throw new EmailFormatException("User with that email already exists!");
        //}

        //private void CheckEmailSpecialCharaters(string emailUsername)
        //{
        //    string specialCh = @"%!@#$%^&*()?/><,:;'\|}]{[~`+=" + "\"";
        //    char[] specialChArray = specialCh.ToCharArray();
        //    bool flag = false;

        //    foreach (char ch in specialChArray)
        //    {
        //        if (emailUsername.Contains(ch))
        //            flag = true;
        //    }

        //    if (flag)
        //        throw new EmailFormatException("Email can only contain '.', '-' and '_' characters!");
        //}

        //private bool CheckForSpecialCharacters(string stringToCheck)
        //{
        //    string specialCh = @"%!@#$%^&*()?/>.<,:;'\|}]{[_~`+=-" + "\"";
        //    char[] specialChArray = specialCh.ToCharArray();
        //    bool flag = false;

        //    foreach (char ch in specialChArray)
        //    {
        //        if (stringToCheck.Contains(ch))
        //            flag = true;
        //    }

        //    return flag;
        //}

        private void GetHashAndSalt(string password, out byte[] salt, out byte[] hash)
        {
            using (var hmac = new HMACSHA512())
            {
                salt = hmac.Key;
                hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        //private void CheckFullName(string name, string lastname)
        //{
        //    if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(lastname) || name.Equals(string.Empty) || lastname.Equals(string.Empty))
        //        throw new FullNameFormatException($"Name or lastname cannot be empty!");

        //    if (name.Length > MAX_NAME_AND_LASTNAME_LENGTH || lastname.Length > MAX_NAME_AND_LASTNAME_LENGTH)
        //        throw new FullNameFormatException($"Name or lastname cannot exceed {MAX_NAME_AND_LASTNAME_LENGTH} characters!");

        //    if (name.Any(x => char.IsWhiteSpace(x)) || lastname.Any(x => char.IsWhiteSpace(x)))
        //        throw new FullNameFormatException("Name or lastname cannot contain whitespace!");

        //    if (name.Any(x => char.IsNumber(x)) || lastname.Any(x => char.IsNumber(x)))
        //        throw new FullNameFormatException("Name or lastname cannot contain numbers!");

        //    if (CheckForSpecialCharacters(name) || CheckForSpecialCharacters(lastname))
        //        throw new FullNameFormatException("Name or lastname cannot contain special characters!");
        //}

        //private void CheckPosition(string position)
        //{
        //    if (string.IsNullOrWhiteSpace(position) || position.Equals(string.Empty))
        //        throw new PositionFormatException($"Postion cannot be empty!");

        //    if (position.Length > MAX_POSITION_LENGTH)
        //        throw new PositionFormatException($"Postion cannot exceed {MAX_POSITION_LENGTH} characters!");

        //    if (position.Any(x => char.IsNumber(x)))
        //        throw new PositionFormatException("Position cannot contain numbers!");

        //    if (CheckForSpecialCharacters(position))
        //        throw new PositionFormatException("Position cannot contain special characters!");
        //}

        //private async Task CheckUserFields(UserRegisterRequestModel registerModel)
        //{
        //    await CheckEmail(registerModel.Email);

        //    CheckPassword(registerModel.Password);

        //    CheckFullName(registerModel.Name, registerModel.Lastname);

        //    CheckPosition(registerModel.Position);
        //}

        //public async Task<UserRegisterResponseModel> Register(UserRegisterRequestModel registerModel, Enums.UserRole role)
        //{
        //    await CheckUserFields(registerModel);

        //    GetHashAndSalt(registerModel.Password, out byte[] salt, out byte[] hash);

        //    DataUserModel userToAdd = new()
        //    {
        //        Id = Guid.NewGuid(),
        //        Name = registerModel.Name,
        //        Lastname = registerModel.Lastname,
        //        Email = registerModel.Email,
        //        Password = hash,
        //        Salt = salt,
        //        Department = Enums.UserDepartment.notDefined,
        //        Role = role,
        //        Position = registerModel.Position,
        //        AvatarUrl = registerModel.AvatarUrl,
        //        DateCreated = DateTime.Now
        //    };

        //    var newUser = await _unitOfWork.Users.CreateAsync(userToAdd);
        //    await _unitOfWork.SaveChanges();

        //    var returnModel = _mapper.Map<UserRegisterResponseModel>(newUser);

        //    return returnModel;
        //}

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

            if(exists != null)
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

        //public async Task<UserCreateRequestModel> DeleteUser(string mail)
        //{
        //    if (mail.Trim() == null)
        //        throw new KeyNotFoundException("Mail is null.");

        //    var userForDeletion = await _unitOfWork.Users.FindSingleAsync(user => user.Email.Trim().Equals(mail.Trim()));

        //    if (userForDeletion == null)
        //        throw new UserNotFoundException("User with that email doesn't exist.");

        //    var userReturnModel = _mapper.Map<UserCreateRequestModel>(userForDeletion);

        //    _unitOfWork.Users.Delete(userForDeletion);

        //    await _unitOfWork.SaveChanges();

        //    return userReturnModel;
        //}

        //public async Task<DataUserModel> GetUserByMail(string email)
        //{
        //    if (email.Trim() == null)
        //    {
        //        throw new KeyNotFoundException("User email has NULL value.");
        //    }

        //    var userForRead = await _unitOfWork.Users.FindSingleAsync(user => user.Email.Trim().Equals(email.Trim()));

        //    if (userForRead == null)
        //    {
        //        throw new KeyNotFoundException("User with that email doesn't exist.");
        //    }

        //    return userForRead;
        //}

        //Task<UserCreateResponseDatabaseToService>
        //public async Task<UserUpdateResponseDatabaseToService> UpdateUser(UserUpdateRequestGatewayToService model)
        //{
        //    if (model.Email.Trim() == null)
        //        throw new KeyNotFoundException("User email has NULL value.");

        //    if(model.Email.Trim() == null)
        //    {
        //    }

        //    var userToBeUpdated = await _unitOfWork.Users.FindSingleAsync(user => user.Email.Trim().Equals(model.Email.Trim()));
        //    if (userToBeUpdated == null)
        //        throw new KeyNotFoundException("User with this email doesn't exists!");

        //    var mappedUser = _mapper.Map<DataUserModel>(model);
        //    mappedUser.Id = userToBeUpdated.Id;
        //    mappedUser.DateCreated = userToBeUpdated.DateCreated;

        //    GetHashAndSalt(model.PasswordUnhashed, out byte[] salt, out byte[] hash);

        //    mappedUser.Password = hash;
        //    mappedUser.Salt = salt;

        //    await _unitOfWork.Users.UpdateAsync(mappedUser, mappedUser.Id);
        //    await _unitOfWork.SaveChanges();
        //    return _mapper.Map<UserCreateRequestModel>(mappedUser);
        //}

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
    }
}