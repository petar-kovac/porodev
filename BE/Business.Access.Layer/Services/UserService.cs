using AutoMapper;
using Business.Access.Layer.Exceptions;
using Business.Access.Layer.Helpers.GlobalExceptionHandler;
using Business.Access.Layer.Models.UserModels;
using Business.Access.Layer.Services.Contracts;
using Data.Access.Layer.Models;
using Data.Access.Layer.Repositories.Contracts;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Business.Access.Layer.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
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

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UserLoginResponseModel> Login(UserLoginRequestModel loginModel)
        {
            DataUserModel dataUserModel;
            try
            {
                dataUserModel = await GetUserByMail(loginModel.Email);
            }
            catch (Exception)
            {
                throw new FailedToLogInException("Login credentials don't match");
            }
            VerifyPasswordHash(loginModel.Password, dataUserModel.Password, dataUserModel.Salt);
            UserLoginResponseModel response = _mapper.Map<UserLoginResponseModel>(dataUserModel);
            response.Jwt = CreateToken(dataUserModel);
            return response;
        }

        public string CreateToken(DataUserModel user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("Id", user.Id.ToString()),
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(SECRET_KEY));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

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
                throw new PasswordFormatException($"Password must be at least {MIN_PASSWORD_LENGTH} characters!");

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
            if (String.IsNullOrWhiteSpace(email))
                throw new EmailFormatException($"Email cannot be empty!");

            if (email.Length > MAX_EMAIL_LENGTH)
                throw new EmailFormatException($"Email cannot contain more than {MAX_EMAIL_LENGTH} characters!");

            var splitEmail = email.Split('@');

            if (splitEmail.Length != 2 && !String.IsNullOrWhiteSpace(splitEmail[0]) && !splitEmail[0].Equals(String.Empty))
                throw new EmailFormatException("Email format is invalid!");

            if (splitEmail[0].Any(x => Char.IsWhiteSpace(x)))
                throw new EmailFormatException("Email cannot contain whitespace!");

            CheckEmailSpecialCharaters(splitEmail[0]);

            if (!splitEmail[1].Equals(EMAIL_DOMAIN))
                throw new EmailFormatException("Email domain is invalid!");

            if (await _unitOfWork.Users.FindSingleAsync(user => user.Email.Equals(email)) != null)
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
            if (String.IsNullOrWhiteSpace(name) || String.IsNullOrWhiteSpace(lastname) || name.Equals(String.Empty) || lastname.Equals(String.Empty))
                throw new FullNameFormatException($"Name or lastname cannot be empty!");

            if (name.Length > MAX_NAME_AND_LASTNAME_LENGTH || lastname.Length > MAX_NAME_AND_LASTNAME_LENGTH)
                throw new FullNameFormatException($"Name or lastname cannot exceed {MAX_NAME_AND_LASTNAME_LENGTH} characters!");

            if (name.Any(x => Char.IsWhiteSpace(x)) || lastname.Any(x => Char.IsWhiteSpace(x)))
                throw new FullNameFormatException("Name or lastname cannot contain whitespace!");

            if (name.Any(x => Char.IsNumber(x)) || lastname.Any(x => Char.IsNumber(x)))
                throw new FullNameFormatException("Name or lastname cannot contain numbers!");

            if (CheckForSpecialCharacters(name) || CheckForSpecialCharacters(lastname))
                throw new FullNameFormatException("Name or lastname cannot contain special characters!");
        }

        private void CheckPosition(string position)
        {
            if (String.IsNullOrWhiteSpace(position) || position.Equals(String.Empty))
                throw new PositionFormatException($"Postion cannot be empty!");

            if (position.Length > MAX_POSITION_LENGTH)
                throw new PositionFormatException($"Postion cannot exceed {MAX_POSITION_LENGTH} characters!"); 

            if (position.Any(x => Char.IsNumber(x)))
                throw new PositionFormatException("Position cannot contain numbers!");

            if (CheckForSpecialCharacters(position))
                throw new PositionFormatException("Position cannot contain special characters!");
        }


        private async Task CheckUserFields(UserRegisterRequestModel registerModel)
        {
            await CheckEmail(registerModel.Email);

            CheckPassword(registerModel.Password);

            CheckFullName(registerModel.Name, registerModel.Lastname);

            CheckPosition(registerModel.Position);
        }

        public async Task<UserRegisterResponseModel> Register(UserRegisterRequestModel registerModel, Enums.UserRole role)
        {
            await CheckUserFields(registerModel);

            GetHashAndSalt(registerModel.Password, out byte[] salt, out byte[] hash);

            DataUserModel userToAdd = new()
            {
                Id = Guid.NewGuid(),
                Name = registerModel.Name,
                Lastname = registerModel.Lastname,
                Email = registerModel.Email,
                Password = hash,
                Salt = salt,
                Department = Enums.UserDepartment.notDefined,
                Role = role,
                Position = registerModel.Position,
                AvatarUrl = registerModel.AvatarUrl,
                DateCreated = DateTime.Now
            };

            var newUser = await _unitOfWork.Users.CreateAsync(userToAdd);
            await _unitOfWork.SaveChanges();

            var returnModel = _mapper.Map<UserRegisterResponseModel>(newUser);

            return returnModel;
        }

        public async Task<Guid> CreateUser(UserCreateRequestModel model)
        {
            var exists = await _unitOfWork.Users.FindSingleAsync(c => c.Email.Trim().Equals(model.Email.Trim()));

            if (exists != null)
                throw new AppException("User already exists");

            var userToCreate = _mapper.Map<DataUserModel>(model);
            GetHashAndSalt(model.PasswordUnhashed, out byte[] salt, out byte[] hash);
            userToCreate.Password = hash;
            userToCreate.Salt = salt;
            userToCreate.Id = Guid.NewGuid();
            userToCreate.DateCreated = DateTime.Now;

            var created = await _unitOfWork.Users.CreateAsync(userToCreate);

            await _unitOfWork.SaveChanges();

            if (created != null)
                return created.Id;
            return Guid.Empty;
        }

        public async Task<UserCreateRequestModel> DeleteUser(string mail)
        {
            if (mail.Trim() == null)
                throw new KeyNotFoundException("Mail is null.");

            var userForDeletion = await _unitOfWork.Users.FindSingleAsync(user => user.Email.Trim().Equals(mail.Trim()));

            if (userForDeletion == null)
                throw new UserNotFoundException("User with that email doesn't exist.");

            var userReturnModel = _mapper.Map<UserCreateRequestModel>(userForDeletion);

            _unitOfWork.Users.Delete(userForDeletion);

            await _unitOfWork.SaveChanges();

            return userReturnModel;
        }

        public async Task<DataUserModel> GetUserByMail(string email)
        {
            if (email.Trim() == null)
            {
                throw new KeyNotFoundException("User email has NULL value.");
            }

            var userForRead = await _unitOfWork.Users.FindSingleAsync(user => user.Email.Trim().Equals(email.Trim()));

            if (userForRead == null)
            {
                throw new KeyNotFoundException("User with that email doesn't exist.");
            }

            return userForRead;
        }

        public async Task<UserCreateRequestModel> UpdateUser(UserCreateRequestModel model)
        {
            if (model.Email.Trim() == null)
                throw new KeyNotFoundException("User email has NULL value.");

            var userToBeUpdated = await _unitOfWork.Users.FindSingleAsync(user => user.Email.Trim().Equals(model.Email.Trim()));
            if (userToBeUpdated == null)
                throw new KeyNotFoundException("User with this email doesn't exists!");

            var mappedUser = _mapper.Map<DataUserModel>(model);
            mappedUser.Id = userToBeUpdated.Id;
            mappedUser.DateCreated = userToBeUpdated.DateCreated;

            GetHashAndSalt(model.PasswordUnhashed, out byte[] salt, out byte[] hash);

            mappedUser.Password = hash;
            mappedUser.Salt = salt;

            await _unitOfWork.Users.UpdateAsync(mappedUser, mappedUser.Id);
            await _unitOfWork.SaveChanges();
            return _mapper.Map<UserCreateRequestModel>(mappedUser);
        }
    }
}